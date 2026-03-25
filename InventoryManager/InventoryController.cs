using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace InventoryManager
{
    // Responsible for reading values from the form controls and performing CRUD + refresh.
    public class InventoryController
    {
        private readonly TextBox _txtProductName;
        private readonly TextBox _txtId;
        private readonly TextBox _txtCategory;
        private readonly TextBox _txtPrice;
        private readonly TextBox _txtQuantity;
        private readonly DataGridView _grid;
        private readonly string _connectionString;

        public InventoryController(
            TextBox txtProductName,
            TextBox txtProductId,
            TextBox txtCategory,
            TextBox txtPrice,
            TextBox txtQuantity,
            DataGridView grid,
            string connectionString)
        {
            _txtProductName = txtProductName ?? throw new ArgumentNullException(nameof(txtProductName));
            _txtId = txtProductId ?? throw new ArgumentNullException(nameof(txtProductId));
            _txtCategory = txtCategory ?? throw new ArgumentNullException(nameof(txtCategory));
            _txtPrice = txtPrice ?? throw new ArgumentNullException(nameof(txtPrice));
            _txtQuantity = txtQuantity ?? throw new ArgumentNullException(nameof(txtQuantity));
            _grid = grid ?? throw new ArgumentNullException(nameof(grid));
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        // Load data from database and bind to DataGridView.
        public void Refresh()
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(_connectionString))
            using (var da = new SqlDataAdapter("SELECT Id, ProductName, Category, Price, Quantity FROM Products", conn))
            {
                da.Fill(dt);
            }

            // Bind DataTable to grid on UI thread
            if (_grid.InvokeRequired)
            {
                _grid.Invoke(new Action(() => _grid.DataSource = dt));
            }
            else
            {
                _grid.DataSource = dt;
            }
        }

        // Read textboxes, validate, insert new product row into DB.
        public void Add()
        {
            // Read and validate inputs
            var name = _txtProductName.Text?.Trim();
            var category = _txtCategory.Text?.Trim();

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("ProductName is required.", nameof(_txtProductName));

            if (!TryParsePrice(out var price))
                throw new ArgumentException("Price is invalid.", nameof(_txtPrice));

            if (!TryParseQuantity(out var quantity))
                throw new ArgumentException("Quantity is invalid.", nameof(_txtQuantity));

            if (price <= 0)
                throw new ArgumentException("Price must be greater than 0.", nameof(_txtPrice));

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than 0.", nameof(_txtQuantity));

            // Check for existing product with same name (case-insensitive)
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT COUNT(1) FROM Products WHERE LOWER(ProductName) = LOWER(@name)", conn))
            {
                cmd.Parameters.AddWithValue("@name", name);
                conn.Open();
            }

            // Insert new product
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("INSERT INTO Products (ProductName, Category, Price, Quantity) VALUES (@name, @category, @price, @quantity)", conn))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@category", (object)category ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@quantity", quantity);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            // Refresh grid after successful insert
            Refresh();
        }

        // Update the selected row in the grid (or by id). Reads values from textboxes.
        public void Update()
        {
            // Update requires the Id to be provided in the txtId textbox (populated by the form when a row is selected)
            var idText = _txtId.Text?.Trim();
            if (string.IsNullOrWhiteSpace(idText))
                throw new ArgumentException("Id is required for update. Select a row to populate fields.", nameof(_txtId));

            if (!int.TryParse(idText, out var id))
                throw new ArgumentException("Id must be a valid integer.", nameof(_txtId));

            // Read and validate other inputs
            var name = _txtProductName.Text?.Trim();
            var category = _txtCategory.Text?.Trim();

            if (string.IsNullOrWhiteSpace(name))
            { throw new ArgumentException("ProductName is required.", nameof(_txtProductName)); }

            if (!TryParsePrice(out var price))
            { throw new ArgumentException("Price is invalid.", nameof(_txtPrice)); }

            if (!TryParseQuantity(out var quantity))
            { throw new ArgumentException("Quantity is invalid.", nameof(_txtQuantity)); }

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("UPDATE Products SET ProductName=@name, Category=@category, Price=@price, Quantity=@quantity WHERE Id=@id", conn))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@category", (object)category ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                var rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                {
                    throw new InvalidOperationException($"No product with Id {id} was found to update.");
                }
            }

            Refresh();
        }

        // Delete the selected row in the grid (or by id).
        public void Delete()
        {
            // Prefer deleting the currently selected row in the grid
            var selectedId = GetSelectedId();
            if (selectedId != null)
            {
                DeleteById(selectedId.Value);
                return;
            }

            // Fall back to reading id from dedicated id textbox
            var idText = _txtId.Text?.Trim();
            if (string.IsNullOrWhiteSpace(idText))
                throw new ArgumentException("Id is required for deletion (select a row or enter Id).", nameof(_txtId));

            if (!int.TryParse(idText, out var id))
                throw new ArgumentException("Id must be a valid integer.", nameof(_txtId));

            DeleteById(id);
        }

        // Delete by explicit id
        public void DeleteById(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("DELETE FROM Products WHERE Id = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                var rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                {
                    throw new InvalidOperationException($"No product with Id {id} was found.");
                }
            }

            // Refresh grid after successful delete
            Refresh();
        }

        // Helper: returns Id from selected DataGridView row or null if none.
        private int? GetSelectedId()
        {
            if (_grid.CurrentRow == null) return null;

            var cell = _grid.CurrentRow.Cells["Id"];
            if (cell == null || cell.Value == null) return null;

            if (int.TryParse(cell.Value.ToString(), out var id)) return id;
            return null;
        }

        // Helper: parse decimal price safely
        private bool TryParsePrice(out decimal price)
        {
            return decimal.TryParse(_txtPrice.Text, out price);
        }

        // Helper: parse int quantity safely
        private bool TryParseQuantity(out int qty)
        {
            return int.TryParse(_txtQuantity.Text, out qty);
        }
    }
}
