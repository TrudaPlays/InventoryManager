using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
// using System.Data.Sql; // not used

namespace InventoryManager
{
    public partial class InventoryManagerForm : Form
    {
        private InventoryController _controller;

        public InventoryManagerForm()
        {
            InitializeComponent();
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

                // If a row is selected, delete by selection. Otherwise, use txtProductId.
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
                // Confirm deletion when using the Id textbox fallback
                var idText = txtProductId.Text?.Trim();
                var confirmMsg = string.IsNullOrWhiteSpace(idText) ? "Delete product?" : $"Delete product with Id {idText}?";
                var dr2 = MessageBox.Show(confirmMsg, "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr2 == DialogResult.Yes)
                {
                    _controller.Delete();
                    messageBox.BackColor = Color.LightGreen;
                    messageBox.Text = "Product deleted.";
                }
                else
                {
                    messageBox.BackColor = Color.LightPink;
                    messageBox.Text = "Delete cancelled.";
                }
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
    }
    }
