namespace Johnny.Kaixin.Controls.ListBoxSelector
{
    partial class AssetListViewSelector
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnUnselectAll = new System.Windows.Forms.Button();
            this.btnUnselectOne = new System.Windows.Forms.Button();
            this.btnSelectOne = new System.Windows.Forms.Button();
            this.lstAllItems = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.lstSelectedItems = new System.Windows.Forms.ListView();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.lblAllAssets = new System.Windows.Forms.Label();
            this.lblBuyAssets = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(266, 110);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(40, 23);
            this.btnSelectAll.TabIndex = 18;
            this.btnSelectAll.Text = ">>";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnUnselectAll
            // 
            this.btnUnselectAll.Location = new System.Drawing.Point(266, 210);
            this.btnUnselectAll.Name = "btnUnselectAll";
            this.btnUnselectAll.Size = new System.Drawing.Size(40, 23);
            this.btnUnselectAll.TabIndex = 20;
            this.btnUnselectAll.Text = "<<";
            this.btnUnselectAll.UseVisualStyleBackColor = true;
            this.btnUnselectAll.Click += new System.EventHandler(this.btnUnselectAll_Click);
            // 
            // btnUnselectOne
            // 
            this.btnUnselectOne.Location = new System.Drawing.Point(266, 160);
            this.btnUnselectOne.Name = "btnUnselectOne";
            this.btnUnselectOne.Size = new System.Drawing.Size(40, 23);
            this.btnUnselectOne.TabIndex = 19;
            this.btnUnselectOne.Text = "<";
            this.btnUnselectOne.UseVisualStyleBackColor = true;
            this.btnUnselectOne.Click += new System.EventHandler(this.btnUnselectOne_Click);
            // 
            // btnSelectOne
            // 
            this.btnSelectOne.Location = new System.Drawing.Point(266, 60);
            this.btnSelectOne.Name = "btnSelectOne";
            this.btnSelectOne.Size = new System.Drawing.Size(40, 23);
            this.btnSelectOne.TabIndex = 17;
            this.btnSelectOne.Text = ">";
            this.btnSelectOne.UseVisualStyleBackColor = true;
            this.btnSelectOne.Click += new System.EventHandler(this.btnSelectOne_Click);
            // 
            // lstAllItems
            // 
            this.lstAllItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lstAllItems.FullRowSelect = true;
            this.lstAllItems.Location = new System.Drawing.Point(3, 33);
            this.lstAllItems.Name = "lstAllItems";
            this.lstAllItems.Size = new System.Drawing.Size(250, 240);
            this.lstAllItems.TabIndex = 37;
            this.lstAllItems.UseCompatibleStateImageBehavior = false;
            this.lstAllItems.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "资产ID";
            this.columnHeader1.Width = 52;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "资产名称";
            this.columnHeader2.Width = 104;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "基准价";
            this.columnHeader3.Width = 70;
            // 
            // lstSelectedItems
            // 
            this.lstSelectedItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lstSelectedItems.FullRowSelect = true;
            this.lstSelectedItems.Location = new System.Drawing.Point(320, 33);
            this.lstSelectedItems.Name = "lstSelectedItems";
            this.lstSelectedItems.Size = new System.Drawing.Size(250, 240);
            this.lstSelectedItems.TabIndex = 38;
            this.lstSelectedItems.UseCompatibleStateImageBehavior = false;
            this.lstSelectedItems.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "资产ID";
            this.columnHeader4.Width = 52;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "资产名称";
            this.columnHeader5.Width = 104;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "基准价";
            this.columnHeader6.Width = 70;
            // 
            // lblAllAssets
            // 
            this.lblAllAssets.AutoSize = true;
            this.lblAllAssets.ForeColor = System.Drawing.Color.Red;
            this.lblAllAssets.Location = new System.Drawing.Point(6, 12);
            this.lblAllAssets.Name = "lblAllAssets";
            this.lblAllAssets.Size = new System.Drawing.Size(65, 12);
            this.lblAllAssets.TabIndex = 39;
            this.lblAllAssets.Text = "所有资产：";
            // 
            // lblBuyAssets
            // 
            this.lblBuyAssets.AutoSize = true;
            this.lblBuyAssets.ForeColor = System.Drawing.Color.Red;
            this.lblBuyAssets.Location = new System.Drawing.Point(318, 12);
            this.lblBuyAssets.Name = "lblBuyAssets";
            this.lblBuyAssets.Size = new System.Drawing.Size(149, 12);
            this.lblBuyAssets.TabIndex = 40;
            this.lblBuyAssets.Text = "只购买以下列表中的资产：";
            // 
            // AssetListViewSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblBuyAssets);
            this.Controls.Add(this.lblAllAssets);
            this.Controls.Add(this.lstSelectedItems);
            this.Controls.Add(this.lstAllItems);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnUnselectAll);
            this.Controls.Add(this.btnUnselectOne);
            this.Controls.Add(this.btnSelectOne);
            this.Name = "AssetListViewSelector";
            this.Size = new System.Drawing.Size(575, 282);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnUnselectAll;
        private System.Windows.Forms.Button btnUnselectOne;
        private System.Windows.Forms.Button btnSelectOne;
        private System.Windows.Forms.ListView lstAllItems;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ListView lstSelectedItems;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Label lblAllAssets;
        private System.Windows.Forms.Label lblBuyAssets;
    }
}
