namespace Johnny.Kaixin.WinUI
{
    partial class DlgSelectionBase
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgSelectionBase));
            this.lstViewFriend = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.rdbAllFriends = new System.Windows.Forms.RadioButton();
            this.rdbGameFriends = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // lstViewFriend
            // 
            this.lstViewFriend.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lstViewFriend.FullRowSelect = true;
            this.lstViewFriend.Location = new System.Drawing.Point(11, 34);
            this.lstViewFriend.Name = "lstViewFriend";
            this.lstViewFriend.Size = new System.Drawing.Size(312, 217);
            this.lstViewFriend.TabIndex = 10;
            this.lstViewFriend.UseCompatibleStateImageBehavior = false;
            this.lstViewFriend.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "好友名";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "好友ID";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "状态";
            this.columnHeader3.Width = 90;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(30, 257);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(61, 23);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(243, 257);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(62, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(136, 257);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(63, 23);
            this.btnRefresh.TabIndex = 12;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // rdbAllFriends
            // 
            this.rdbAllFriends.AutoSize = true;
            this.rdbAllFriends.Location = new System.Drawing.Point(39, 12);
            this.rdbAllFriends.Name = "rdbAllFriends";
            this.rdbAllFriends.Size = new System.Drawing.Size(71, 16);
            this.rdbAllFriends.TabIndex = 15;
            this.rdbAllFriends.TabStop = true;
            this.rdbAllFriends.Text = "所有好友";
            this.rdbAllFriends.UseVisualStyleBackColor = true;
            this.rdbAllFriends.CheckedChanged += new System.EventHandler(this.rdbAllFriends_CheckedChanged);
            // 
            // rdbGameFriends
            // 
            this.rdbGameFriends.AutoSize = true;
            this.rdbGameFriends.Location = new System.Drawing.Point(157, 12);
            this.rdbGameFriends.Name = "rdbGameFriends";
            this.rdbGameFriends.Size = new System.Drawing.Size(107, 16);
            this.rdbGameFriends.TabIndex = 16;
            this.rdbGameFriends.TabStop = true;
            this.rdbGameFriends.Text = "当前可咬的好友";
            this.rdbGameFriends.UseVisualStyleBackColor = true;
            this.rdbGameFriends.CheckedChanged += new System.EventHandler(this.rdbGameFriends_CheckedChanged);
            // 
            // DlgSelectionBase
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(335, 292);
            this.Controls.Add(this.rdbGameFriends);
            this.Controls.Add(this.rdbAllFriends);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lstViewFriend);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgSelectionBase";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择好友 By Johnny";
            this.Load += new System.EventHandler(this.DlgSelectionBase_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DlgSelectionBase_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstViewFriend;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.RadioButton rdbAllFriends;
        private System.Windows.Forms.RadioButton rdbGameFriends;
    }
}