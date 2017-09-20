namespace Johnny.Kaixin.WinUI
{
    partial class DlgTips
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgTips));
            this.txtUpdateInfo = new System.Windows.Forms.TextBox();
            this.chkNeverDisplay = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.cmbVersion = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtUpdateInfo
            // 
            this.txtUpdateInfo.Location = new System.Drawing.Point(12, 29);
            this.txtUpdateInfo.Multiline = true;
            this.txtUpdateInfo.Name = "txtUpdateInfo";
            this.txtUpdateInfo.ReadOnly = true;
            this.txtUpdateInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtUpdateInfo.Size = new System.Drawing.Size(398, 172);
            this.txtUpdateInfo.TabIndex = 0;
            this.txtUpdateInfo.WordWrap = false;
            // 
            // chkNeverDisplay
            // 
            this.chkNeverDisplay.AutoSize = true;
            this.chkNeverDisplay.Location = new System.Drawing.Point(12, 207);
            this.chkNeverDisplay.Name = "chkNeverDisplay";
            this.chkNeverDisplay.Size = new System.Drawing.Size(98, 17);
            this.chkNeverDisplay.TabIndex = 1;
            this.chkNeverDisplay.Text = "以后不再显示";
            this.chkNeverDisplay.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.Location = new System.Drawing.Point(335, 207);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 25);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cmbVersion
            // 
            this.cmbVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVersion.FormattingEnabled = true;
            this.cmbVersion.Location = new System.Drawing.Point(85, 4);
            this.cmbVersion.Name = "cmbVersion";
            this.cmbVersion.Size = new System.Drawing.Size(136, 21);
            this.cmbVersion.TabIndex = 3;
            this.cmbVersion.SelectedIndexChanged += new System.EventHandler(this.cmbVersion_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "历史版本：";
            // 
            // DlgTips
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 244);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbVersion);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.chkNeverDisplay);
            this.Controls.Add(this.txtUpdateInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgTips";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "版本更新信息";
            this.Load += new System.EventHandler(this.DlgTips_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUpdateInfo;
        private System.Windows.Forms.CheckBox chkNeverDisplay;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox cmbVersion;
        private System.Windows.Forms.Label label1;
    }
}