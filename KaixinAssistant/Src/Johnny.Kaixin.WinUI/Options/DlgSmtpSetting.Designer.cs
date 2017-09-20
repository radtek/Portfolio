namespace Johnny.Kaixin.WinUI
{
    partial class DlgSmtpSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgSmtpSetting));
            this.btnOK = new System.Windows.Forms.Button();
            this.grpSmtp = new System.Windows.Forms.GroupBox();
            this.txtSenderEmail = new System.Windows.Forms.TextBox();
            this.lblSenderEmail = new System.Windows.Forms.Label();
            this.txtSenderName = new System.Windows.Forms.TextBox();
            this.lblSenderName = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtSmtpPort = new System.Windows.Forms.TextBox();
            this.lblSmtpPort = new System.Windows.Forms.Label();
            this.txtSmtpHost = new System.Windows.Forms.TextBox();
            this.lblSmtpHost = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.grpSmtp.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(83, 257);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // grpSmtp
            // 
            this.grpSmtp.Controls.Add(this.txtSenderEmail);
            this.grpSmtp.Controls.Add(this.lblSenderEmail);
            this.grpSmtp.Controls.Add(this.txtSenderName);
            this.grpSmtp.Controls.Add(this.lblSenderName);
            this.grpSmtp.Controls.Add(this.txtPassword);
            this.grpSmtp.Controls.Add(this.lblPassword);
            this.grpSmtp.Controls.Add(this.txtUserName);
            this.grpSmtp.Controls.Add(this.lblUserName);
            this.grpSmtp.Controls.Add(this.txtSmtpPort);
            this.grpSmtp.Controls.Add(this.lblSmtpPort);
            this.grpSmtp.Controls.Add(this.txtSmtpHost);
            this.grpSmtp.Controls.Add(this.lblSmtpHost);
            this.grpSmtp.Location = new System.Drawing.Point(12, 13);
            this.grpSmtp.Name = "grpSmtp";
            this.grpSmtp.Size = new System.Drawing.Size(398, 218);
            this.grpSmtp.TabIndex = 1;
            this.grpSmtp.TabStop = false;
            this.grpSmtp.Text = "Smtp设置";
            // 
            // txtSenderEmail
            // 
            this.txtSenderEmail.Location = new System.Drawing.Point(97, 116);
            this.txtSenderEmail.Name = "txtSenderEmail";
            this.txtSenderEmail.Size = new System.Drawing.Size(229, 20);
            this.txtSenderEmail.TabIndex = 4;
            // 
            // lblSenderEmail
            // 
            this.lblSenderEmail.AutoSize = true;
            this.lblSenderEmail.Location = new System.Drawing.Point(20, 119);
            this.lblSenderEmail.Name = "lblSenderEmail";
            this.lblSenderEmail.Size = new System.Drawing.Size(80, 13);
            this.lblSenderEmail.TabIndex = 10;
            this.lblSenderEmail.Text = "发送者Email：";
            // 
            // txtSenderName
            // 
            this.txtSenderName.Location = new System.Drawing.Point(97, 87);
            this.txtSenderName.Name = "txtSenderName";
            this.txtSenderName.Size = new System.Drawing.Size(165, 20);
            this.txtSenderName.TabIndex = 3;
            // 
            // lblSenderName
            // 
            this.lblSenderName.AutoSize = true;
            this.lblSenderName.Location = new System.Drawing.Point(20, 90);
            this.lblSenderName.Name = "lblSenderName";
            this.lblSenderName.Size = new System.Drawing.Size(79, 13);
            this.lblSenderName.TabIndex = 8;
            this.lblSenderName.Text = "发送者姓名：";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(97, 174);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(165, 20);
            this.txtPassword.TabIndex = 6;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(20, 178);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(43, 13);
            this.lblPassword.TabIndex = 6;
            this.lblPassword.Text = "密码：";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(97, 145);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(165, 20);
            this.txtUserName.TabIndex = 5;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(20, 148);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(55, 13);
            this.lblUserName.TabIndex = 4;
            this.lblUserName.Text = "用户名：";
            // 
            // txtSmtpPort
            // 
            this.txtSmtpPort.Location = new System.Drawing.Point(97, 57);
            this.txtSmtpPort.Name = "txtSmtpPort";
            this.txtSmtpPort.Size = new System.Drawing.Size(73, 20);
            this.txtSmtpPort.TabIndex = 2;
            // 
            // lblSmtpPort
            // 
            this.lblSmtpPort.AutoSize = true;
            this.lblSmtpPort.Location = new System.Drawing.Point(20, 61);
            this.lblSmtpPort.Name = "lblSmtpPort";
            this.lblSmtpPort.Size = new System.Drawing.Size(67, 13);
            this.lblSmtpPort.TabIndex = 2;
            this.lblSmtpPort.Text = "Smtp端口：";
            // 
            // txtSmtpHost
            // 
            this.txtSmtpHost.Location = new System.Drawing.Point(97, 28);
            this.txtSmtpHost.Name = "txtSmtpHost";
            this.txtSmtpHost.Size = new System.Drawing.Size(261, 20);
            this.txtSmtpHost.TabIndex = 1;
            // 
            // lblSmtpHost
            // 
            this.lblSmtpHost.AutoSize = true;
            this.lblSmtpHost.Location = new System.Drawing.Point(20, 31);
            this.lblSmtpHost.Name = "lblSmtpHost";
            this.lblSmtpHost.Size = new System.Drawing.Size(79, 13);
            this.lblSmtpHost.TabIndex = 0;
            this.lblSmtpHost.Text = "Smtp服务器：";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(263, 257);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(173, 257);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 25);
            this.btnTest.TabIndex = 8;
            this.btnTest.Text = "测试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // DlgSmtpSetting
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(423, 298);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grpSmtp);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgSmtpSetting";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SMTP设置";
            this.Load += new System.EventHandler(this.DlgSmtpSetting_Load);
            this.grpSmtp.ResumeLayout(false);
            this.grpSmtp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox grpSmtp;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox txtSmtpPort;
        private System.Windows.Forms.Label lblSmtpPort;
        private System.Windows.Forms.TextBox txtSmtpHost;
        private System.Windows.Forms.Label lblSmtpHost;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtSenderEmail;
        private System.Windows.Forms.Label lblSenderEmail;
        private System.Windows.Forms.TextBox txtSenderName;
        private System.Windows.Forms.Label lblSenderName;
        private System.Windows.Forms.Button btnTest;
    }
}