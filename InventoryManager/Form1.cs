using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManager
{
    public partial class InventoryManagerForm : Form
    {
        public InventoryManagerForm()
        {
            InitializeComponent();
        }

        private void InventoryManagerForm_Load(object sender, EventArgs e)
        {
            // Skip loading at design time
            if (this.DesignMode) return;

            try
            {
                // Ensure required designer components are available
                if (productsTableAdapter1 == null || inventoryDatabaseDataSet1 == null)
                    return;

                // Clear existing rows to ensure a fresh load
                inventoryDatabaseDataSet1.Products.Clear();

                // Load products into dataset
                this.productsTableAdapter1.Fill(this.inventoryDatabaseDataSet1.Products);
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to load products. Please contact support.", "Load error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Consider logging the exception details to a file or telemetry
            }

        }
    }
}
