namespace InventoryManager
{
    partial class InventoryManagerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.messageBox = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.InventoryDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.inventoryDataSet = new InventoryManager.InventoryDataSet();
            this.productsTableAdapter = new InventoryManager.InventoryDataSetTableAdapters.ProductsTableAdapter();
            this.lblId = new System.Windows.Forms.Label();
            this.txtProductId = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSaveToFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.InventoryDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inventoryDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(187, 99);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(209, 26);
            this.txtProductName.TabIndex = 0;
            // 
            // txtCategory
            // 
            this.txtCategory.Location = new System.Drawing.Point(187, 176);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(209, 26);
            this.txtCategory.TabIndex = 1;
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(187, 255);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(110, 26);
            this.txtPrice.TabIndex = 2;
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(187, 326);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(110, 26);
            this.txtQuantity.TabIndex = 3;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(43, 410);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(110, 60);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(187, 410);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(110, 60);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(329, 410);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(110, 60);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(1131, 493);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(110, 60);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(473, 410);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(110, 60);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // messageBox
            // 
            this.messageBox.AutoSize = true;
            this.messageBox.Location = new System.Drawing.Point(428, 38);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(183, 20);
            this.messageBox.TabIndex = 9;
            this.messageBox.Text = "Messages will show here";
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Location = new System.Drawing.Point(39, 105);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(110, 20);
            this.lblProductName.TabIndex = 11;
            this.lblProductName.Text = "Product Name";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(39, 182);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(73, 20);
            this.lblCategory.TabIndex = 12;
            this.lblCategory.Text = "Category";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(43, 255);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(44, 20);
            this.lblPrice.TabIndex = 13;
            this.lblPrice.Text = "Price";
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(39, 329);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(68, 20);
            this.lblQuantity.TabIndex = 14;
            this.lblQuantity.Text = "Quantity";
            // 
            // InventoryDataGridView
            // 
            this.InventoryDataGridView.AutoGenerateColumns = false;
            this.InventoryDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.InventoryDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.InventoryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InventoryDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            this.InventoryDataGridView.DataSource = this.productsBindingSource;
            this.InventoryDataGridView.Location = new System.Drawing.Point(432, 85);
            this.InventoryDataGridView.Name = "InventoryDataGridView";
            this.InventoryDataGridView.RowHeadersWidth = 62;
            this.InventoryDataGridView.RowTemplate.Height = 28;
            this.InventoryDataGridView.Size = new System.Drawing.Size(809, 299);
            this.InventoryDataGridView.TabIndex = 15;
            this.InventoryDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InventoryDataGridView_CellClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "id";
            this.dataGridViewTextBoxColumn1.HeaderText = "id";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ProductName";
            this.dataGridViewTextBoxColumn2.HeaderText = "ProductName";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Category";
            this.dataGridViewTextBoxColumn3.HeaderText = "Category";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Price";
            this.dataGridViewTextBoxColumn4.HeaderText = "Price";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Quantity";
            this.dataGridViewTextBoxColumn5.HeaderText = "Quantity";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // productsBindingSource
            // 
            this.productsBindingSource.DataMember = "Products";
            this.productsBindingSource.DataSource = this.inventoryDataSet;
            // 
            // inventoryDataSet
            // 
            this.inventoryDataSet.DataSetName = "InventoryDataSet";
            this.inventoryDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // productsTableAdapter
            // 
            this.productsTableAdapter.ClearBeforeFill = true;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(39, 35);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(85, 20);
            this.lblId.TabIndex = 17;
            this.lblId.Text = "Product ID";
            // 
            // txtProductId
            // 
            this.txtProductId.Location = new System.Drawing.Point(187, 32);
            this.txtProductId.Name = "txtProductId";
            this.txtProductId.Size = new System.Drawing.Size(209, 26);
            this.txtProductId.TabIndex = 16;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(614, 410);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 60);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "Save Changes";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSaveToFile
            // 
            this.btnSaveToFile.Location = new System.Drawing.Point(987, 493);
            this.btnSaveToFile.Name = "btnSaveToFile";
            this.btnSaveToFile.Size = new System.Drawing.Size(110, 60);
            this.btnSaveToFile.TabIndex = 19;
            this.btnSaveToFile.Text = "Save to txt File";
            this.btnSaveToFile.UseVisualStyleBackColor = true;
            this.btnSaveToFile.Click += new System.EventHandler(this.btnSaveToFile_Click);
            // 
            // InventoryManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1431, 593);
            this.Controls.Add(this.btnSaveToFile);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.txtProductId);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblProductName);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.txtCategory);
            this.Controls.Add(this.txtProductName);
            this.Controls.Add(this.InventoryDataGridView);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "InventoryManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InventoryManager";
            this.Load += new System.EventHandler(this.InventoryManagerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.InventoryDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inventoryDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label messageBox;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.DataGridView InventoryDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn productNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn categoryDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantityDataGridViewTextBoxColumn;
        private InventoryDataSet inventoryDataSet;
        private System.Windows.Forms.BindingSource productsBindingSource;
        private InventoryDataSetTableAdapters.ProductsTableAdapter productsTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtProductId;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSaveToFile;
    }
}

