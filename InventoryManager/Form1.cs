using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// using System.Data.Sql; // not used

namespace InventoryManager
{
    public partial class InventoryManagerForm : Form
    {
        private InventoryController _controller;

        public InventoryManagerForm()
        {
            InitializeComponent();
            // Ensure row click populates the edit fields
            this.InventoryDataGridView.CellClick += this.InventoryDataGridView_CellClick;
        }

        private void InventoryManagerForm_Load(object sender, EventArgs e)
        {
            // Initialize controller and load data
            // Use explicit LocalDB connection string to connect to the local database file
            var connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\gertr\Desktop\C#\PracticeSQL\InventoryManager\InventoryManager\InventoryDB\Inventory.mdf;Integrated Security=True";
            _controller = new InventoryController(txtProductName, txtProductId, txtCategory, txtPrice, txtQuantity, InventoryDataGridView, connString);
            try
            {
                _controller.Refresh();
            }
            catch (Exception ex)
            {
                // Show full exception and DataDirectory diagnostics to help debug connection issues
                var dataDir = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
                if (string.IsNullOrEmpty(dataDir)) dataDir = AppDomain.CurrentDomain.BaseDirectory;
                var mdfPath = Path.Combine(dataDir, "InventoryDatabase.mdf");
                messageBox.Text = $"Load failed: {ex}\nDataDirectory: {dataDir}\nMDF exists: {File.Exists(mdfPath)}\nPath: {mdfPath}";
            }


        }

        private void btnExit_Click(object sender, EventArgs e) //closes the form gracefully when the Exit button is clicked
        {
            Close();    
        }

        private void btnAdd_Click(object sender, EventArgs e) //adds the product to the database
        {
            try
            {
                if (_controller == null)
                {
                    messageBox.BackColor = Color.LightPink;
                    messageBox.Text = "Controller not initialized.";
                    return;
                }

                var drAdd = MessageBox.Show($"Add product '{txtProductName.Text}'?", "Confirm add", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (drAdd == DialogResult.Yes)
                {
                    _controller.Add();
                    ClearForm();
                    messageBox.BackColor = Color.LightGreen;
                    messageBox.Text = "Product added successfully.";
                }
                else
                {
                    messageBox.BackColor = Color.LightPink;
                    messageBox.Text = "Add cancelled.";
                }
            }
            catch (InvalidOperationException ex)
            {
                messageBox.Text = ex.Message;
            }
            catch (ArgumentException ex)
            {
                messageBox.Text = ex.Message;
            }
            catch (Exception ex)
            {
                messageBox.Text = $"Add failed: {ex.Message}";
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e) //clears the form and refreshes the datagridview
        {
            try
            {
                Refresh();
                ClearForm();
            }
            catch (Exception ex)
            {
                messageBox.BackColor = Color.LightPink;
                messageBox.Text = $"Refresh failed: {ex.Message}";
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (_controller == null)
                {
                    messageBox.BackColor = Color.LightPink;
                    messageBox.Text = "Controller not initialized.";
                    return;
                }

                // If the user entered an Id in the Id textbox, prefer that for deletion
                var idText = txtProductId.Text?.Trim();
                if (!string.IsNullOrWhiteSpace(idText) && int.TryParse(idText, out var typedId))
                {
                    var drTyped = MessageBox.Show($"Delete product with Id {typedId}?", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (drTyped == DialogResult.Yes)
                    {
                        _controller.DeleteById(typedId);
                        messageBox.BackColor = Color.LightGreen;
                        messageBox.Text = "Product deleted.";
                    }
                    else
                    {
                        messageBox.BackColor = Color.LightPink;
                        messageBox.Text = "Delete cancelled.";
                    }
                    return;
                }

                // Otherwise, if a row is selected, delete by selection.
                if (InventoryDataGridView.CurrentRow != null)
                {
                    var idCell = InventoryDataGridView.CurrentRow.Cells[0];
                    if (idCell != null && idCell.Value != null && int.TryParse(idCell.Value.ToString(), out var selId))
                    {
                        var dr = MessageBox.Show($"Delete product with Id {selId}?", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            _controller.DeleteById(selId);
                            messageBox.BackColor = Color.LightGreen;
                            messageBox.Text = "Product deleted (selected row).";
                        }
                        else
                        {
                            messageBox.BackColor = Color.LightPink;
                            messageBox.Text = "Delete cancelled.";
                        }
                        return;
                    }
                }

                // Nothing to delete
                messageBox.BackColor = Color.LightPink;
                messageBox.Text = "No Id provided and no row selected to delete.";
            }
            catch (InvalidOperationException ex)
            {
                messageBox.BackColor = Color.LightPink;
                messageBox.Text = ex.Message;
            }
            catch (ArgumentException ex)
            {
                messageBox.BackColor = Color.LightPink;
                messageBox.Text = ex.Message;
            }
            catch (Exception ex)
            {
                messageBox.BackColor = Color.LightPink;
                messageBox.Text = $"Delete failed: {ex.Message}";
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (_controller == null)
                {
                    messageBox.BackColor = Color.LightPink;
                    messageBox.Text = "Controller not initialized.";
                    return;
                }
                // Allow either entering an Id in txtProductId or selecting a row to load fields for update.
                // Prefer an explicit Id entered by the user.
                var idTextInput = txtProductId.Text?.Trim();
                DataGridViewRow row = null;
                int selectedId;

                if (!string.IsNullOrWhiteSpace(idTextInput) && int.TryParse(idTextInput, out var inputId))
                {
                    // user entered an id; ensure grid data is fresh then find the row
                    try
                    {
                        _controller.Refresh();
                    }
                    catch (Exception refreshEx)
                    {
                        // show refresh error but still attempt to find row in current grid
                        messageBox.BackColor = Color.LightPink;
                        messageBox.Text = $"Refresh failed: {refreshEx.Message}";
                    }

                    row = InventoryDataGridView.Rows.Cast<DataGridViewRow>().FirstOrDefault(r =>
                        r.Cells.Count > 0 && r.Cells[0].Value != null && int.TryParse(r.Cells[0].Value.ToString(), out var v) && v == inputId);

                    if (row == null)
                    {
                        messageBox.BackColor = Color.LightPink;
                        messageBox.Text = $"No product with Id {inputId} found.";
                        return;
                    }

                    selectedId = inputId;
                }
                else
                {
                    // fall back to selected row
                    row = InventoryDataGridView.CurrentRow;
                    if (row == null)
                    {
                        messageBox.BackColor = Color.LightPink;
                        messageBox.Text = "Please select a row to load for update or enter a valid Id in the Id field.";
                        return;
                    }

                    var idCell = row.Cells[0];
                    if (idCell == null || idCell.Value == null || !int.TryParse(idCell.Value.ToString(), out selectedId))
                    {
                        messageBox.BackColor = Color.LightPink;
                        messageBox.Text = "Selected row does not contain a valid Id.";
                        return;
                    }
                }

                // At this point we have a row and selectedId; populate fields for editing. Save is performed by the Save button.
                txtProductId.Text = selectedId.ToString();
                txtProductName.Text = row.Cells.Count > 1 ? row.Cells[1]?.Value?.ToString() ?? string.Empty : string.Empty;
                txtCategory.Text = row.Cells.Count > 2 ? row.Cells[2]?.Value?.ToString() ?? string.Empty : string.Empty;
                txtPrice.Text = row.Cells.Count > 3 ? row.Cells[3]?.Value?.ToString() ?? string.Empty : string.Empty;
                txtQuantity.Text = row.Cells.Count > 4 ? row.Cells[4]?.Value?.ToString() ?? string.Empty : string.Empty;

                messageBox.BackColor = Color.LightGreen;
                messageBox.Text = "Row loaded. Modify fields and click Save to apply changes.";
            }
            catch (InvalidOperationException ex)
            {
                messageBox.BackColor = Color.LightPink;
                messageBox.Text = ex.Message;
            }
            catch (ArgumentException ex)
            {
                messageBox.BackColor = Color.LightPink;
                messageBox.Text = ex.Message;
            }
            catch (Exception ex)
            {
                messageBox.BackColor = Color.LightPink;
                messageBox.Text = $"Update failed: {ex.Message}";
            }
        }


        //helper method to clear the form

        private void ClearForm()
        {
            txtProductName.Text = "";
            txtProductId.Text = "";
            txtCategory.Text = "";
            txtPrice.Text = "";
            txtQuantity.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_controller == null)
                {
                    messageBox.BackColor = Color.LightPink;
                    messageBox.Text = "Controller not initialized.";
                    return;
                }

                // Ensure an Id is present (must have loaded a row)
                if (string.IsNullOrWhiteSpace(txtProductId.Text) || !int.TryParse(txtProductId.Text, out var id))
                {
                    messageBox.BackColor = Color.LightPink;
                    messageBox.Text = "No row loaded. Select a row and click Update to load it before saving.";
                    return;
                }

                var dr = MessageBox.Show($"Save changes to product Id {id}?", "Confirm save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != DialogResult.Yes)
                {
                    messageBox.BackColor = Color.LightPink;
                    messageBox.Text = "Save cancelled.";
                    return;
                }

                _controller.Update();
                ClearForm();
                messageBox.BackColor = Color.LightGreen;
                messageBox.Text = "Product saved.";
            }
            catch (Exception ex)
            {
                messageBox.BackColor = Color.LightPink;
                messageBox.Text = $"Save failed: {ex.Message}";
            }
        }

        private void InventoryDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the click was on a valid row (not a header)
            if (e.RowIndex >= 0)
            {
                // Access the clicked row
                try
                {
                    DataGridViewRow row = InventoryDataGridView.Rows[e.RowIndex];

                    // Populate textboxes from the clicked row (guarding for missing columns)
                    if (row.Cells.Count > 0 && row.Cells[0].Value != null)
                        txtProductId.Text = row.Cells[0].Value.ToString();
                    else
                        txtProductId.Text = string.Empty;

                    txtProductName.Text = row.Cells.Count > 1 && row.Cells[1].Value != null ? row.Cells[1].Value.ToString() : string.Empty;
                    txtCategory.Text = row.Cells.Count > 2 && row.Cells[2].Value != null ? row.Cells[2].Value.ToString() : string.Empty;
                    txtPrice.Text = row.Cells.Count > 3 && row.Cells[3].Value != null ? row.Cells[3].Value.ToString() : string.Empty;
                    txtQuantity.Text = row.Cells.Count > 4 && row.Cells[4].Value != null ? row.Cells[4].Value.ToString() : string.Empty;

                    messageBox.BackColor = Color.LightGreen;
                    messageBox.Text = "Row loaded. Modify fields and click Save to apply changes.";
                }
                catch (Exception ex)
                {
                    messageBox.BackColor = Color.LightPink;
                    messageBox.Text = $"Failed to load row: {ex.Message}";
                }
            }
        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (_controller == null)
                {
                    messageBox.BackColor = Color.LightPink;
                    messageBox.Text = "Controller not initialized.";
                    return;
                }

                var confirm = MessageBox.Show("Save current database contents to Desktop\\DatabaseExport.csv  ?", "Confirm export", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes)
                {
                    messageBox.BackColor = Color.LightPink;
                    messageBox.Text = "Export cancelled.";
                    return;
                }

                // Try to get a DataTable from the grid's DataSource
                DataTable dt = null;
                var ds = InventoryDataGridView.DataSource;
                if (ds is DataTable t)
                    dt = t;
                else if (ds is DataView dv)
                    dt = dv.Table;
                else if (ds is BindingSource bs)
                {
                    if (bs.DataSource is DataTable t2) dt = t2;
                    else if (bs.DataSource is DataView dv2) dt = dv2.Table;
                }

                // If no table available, try refreshing the controller and re-check
                if (dt == null)
                {
                    _controller.Refresh();
                    ds = InventoryDataGridView.DataSource;
                    if (ds is DataTable t3)
                        dt = t3;
                    else if (ds is DataView dv3)
                        dt = dv3.Table;
                    else if (ds is BindingSource bs3)
                    {
                        if (bs3.DataSource is DataTable t4) dt = t4;
                        else if (bs3.DataSource is DataView dv4) dt = dv4.Table;
                    }
                }

                if (dt == null)
                {
                    messageBox.BackColor = Color.LightPink;
                    messageBox.Text = "No tabular data available to export.";
                    return;
                }

                var desktopPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "DatabaseExport.csv");
                using (var sw = new System.IO.StreamWriter(desktopPath, false, Encoding.UTF8))
                {
                    // header
                    sw.WriteLine(string.Join("\t", dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName)));

                    foreach (DataRow row in dt.Rows)
                    {
                        var values = dt.Columns.Cast<DataColumn>().Select(c => row[c] == DBNull.Value ? string.Empty : row[c].ToString());
                        sw.WriteLine(string.Join("\t", values));
                    }
                }

                messageBox.BackColor = Color.LightGreen;
                messageBox.Text = $"Exported {dt.Rows.Count} rows to: {desktopPath}";
            }
            catch (Exception ex)
            {
                messageBox.BackColor = Color.LightPink;
                messageBox.Text = $"Export failed: {ex.Message}";
            }

        }
    }
    }
