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
            // TODO: This line of code loads data into the 'inventoryDatabaseDataSet.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.inventoryDatabaseDataSet.Products);

        }
    }
}
