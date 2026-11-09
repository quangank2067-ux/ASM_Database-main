namespace ASM_Database_Final
{
    partial class AdminForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnManageEmployee = new System.Windows.Forms.Button();
            this.btnManageProduct = new System.Windows.Forms.Button();
            this.btnManageImport = new System.Windows.Forms.Button();
            this.btnManageCategory = new System.Windows.Forms.Button();
            this.btnManageOrder = new System.Windows.Forms.Button();
            this.btnViewStatistic = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnViewStatistic);
            this.groupBox1.Controls.Add(this.btnManageOrder);
            this.groupBox1.Controls.Add(this.btnManageCategory);
            this.groupBox1.Controls.Add(this.btnManageImport);
            this.groupBox1.Controls.Add(this.btnManageProduct);
            this.groupBox1.Controls.Add(this.btnManageEmployee);
            this.groupBox1.Location = new System.Drawing.Point(29, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(882, 508);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // btnManageEmployee
            // 
            this.btnManageEmployee.Location = new System.Drawing.Point(92, 82);
            this.btnManageEmployee.Name = "btnManageEmployee";
            this.btnManageEmployee.Size = new System.Drawing.Size(200, 70);
            this.btnManageEmployee.TabIndex = 0;
            this.btnManageEmployee.Text = "Manage Employee";
            this.btnManageEmployee.UseVisualStyleBackColor = true;
            this.btnManageEmployee.Click += new System.EventHandler(this.btnManageEmployee_Click);
            // 
            // btnManageProduct
            // 
            this.btnManageProduct.Location = new System.Drawing.Point(510, 111);
            this.btnManageProduct.Name = "btnManageProduct";
            this.btnManageProduct.Size = new System.Drawing.Size(200, 70);
            this.btnManageProduct.TabIndex = 1;
            this.btnManageProduct.Text = "Manage Product";
            this.btnManageProduct.UseVisualStyleBackColor = true;
            // 
            // btnManageImport
            // 
            this.btnManageImport.Location = new System.Drawing.Point(92, 298);
            this.btnManageImport.Name = "btnManageImport";
            this.btnManageImport.Size = new System.Drawing.Size(200, 70);
            this.btnManageImport.TabIndex = 2;
            this.btnManageImport.Text = "Manage Import";
            this.btnManageImport.UseVisualStyleBackColor = true;
            // 
            // btnManageCategory
            // 
            this.btnManageCategory.Location = new System.Drawing.Point(92, 189);
            this.btnManageCategory.Name = "btnManageCategory";
            this.btnManageCategory.Size = new System.Drawing.Size(200, 70);
            this.btnManageCategory.TabIndex = 3;
            this.btnManageCategory.Text = "Manage Category";
            this.btnManageCategory.UseVisualStyleBackColor = true;
            // 
            // btnManageOrder
            // 
            this.btnManageOrder.Location = new System.Drawing.Point(510, 242);
            this.btnManageOrder.Name = "btnManageOrder";
            this.btnManageOrder.Size = new System.Drawing.Size(200, 70);
            this.btnManageOrder.TabIndex = 4;
            this.btnManageOrder.Text = "Manage Order";
            this.btnManageOrder.UseVisualStyleBackColor = true;
            // 
            // btnViewStatistic
            // 
            this.btnViewStatistic.Location = new System.Drawing.Point(276, 411);
            this.btnViewStatistic.Name = "btnViewStatistic";
            this.btnViewStatistic.Size = new System.Drawing.Size(300, 70);
            this.btnViewStatistic.TabIndex = 5;
            this.btnViewStatistic.Text = "View Statistic";
            this.btnViewStatistic.UseVisualStyleBackColor = true;
            // 
            // AdminForm
            // 
            this.ClientSize = new System.Drawing.Size(956, 579);
            this.Controls.Add(this.groupBox1);
            this.Name = "AdminForm";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnManageEmployee;
        private System.Windows.Forms.Button btnViewStatistic;
        private System.Windows.Forms.Button btnManageOrder;
        private System.Windows.Forms.Button btnManageCategory;
        private System.Windows.Forms.Button btnManageImport;
        private System.Windows.Forms.Button btnManageProduct;
    }
}