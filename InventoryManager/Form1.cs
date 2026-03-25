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
            // Use explicit LocalDB connection string provided by the user to connect to the real database file
            var connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\gertr\Desktop\C#\PracticeSQL\InventoryManager\InventoryManager\InventoryDB\Inventory.mdf;Integrated Security=True";
            _controller = new InventoryController(txtProductName, txtCategory, txtPrice, txtQuantity, InventoryDataGridView, connString);
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();    
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (_controller == null)
                {
                    messageBox.Text = "Controller not initialized.";
                    return;
                }

                _controller.Add();
                messageBox.Text = "Product added successfully.";
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
    }
}
