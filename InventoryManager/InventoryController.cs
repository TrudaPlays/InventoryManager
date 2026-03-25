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
        private readonly TextBox _txtCategory;
        private readonly TextBox _txtPrice;
        private readonly TextBox _txtQuantity;
        private readonly DataGridView _grid;
        private readonly string _connectionString;

        public InventoryController(
            TextBox txtProductName,
            TextBox txtCategory,
            TextBox txtPrice,
            TextBox txtQuantity,
            DataGridView grid,
            string connectionString)
        {
            _txtProductName = txtProductName ?? throw new ArgumentNullException(nameof(txtProductName));
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
                var exists = Convert.ToInt32(cmd.ExecuteScalar() ?? 0) > 0;
                if (exists)
                {
                    // Inform caller that product already exists; instruct to use Update instead
                    throw new InvalidOperationException($"Product '{name}' already exists. Use the Update button to modify it.");
                }
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
            // TODO: 1) Determine target Id (e.g. GetSelectedId())
            //       2) Parse/validate inputs
            //       3) Use parameterized UPDATE command to update DB
            //       4) Call Refresh()
        }

        // Delete the selected row in the grid (or by id).
        public void Delete()
        {
            // TODO: 1) Determine target Id (e.g. GetSelectedId())
            //       2) Confirm/ask user if needed
            //       3) Use parameterized DELETE command to remove row
            //       4) Call Refresh()
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
