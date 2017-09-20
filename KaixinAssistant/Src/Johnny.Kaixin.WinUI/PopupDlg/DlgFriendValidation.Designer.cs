namespace Johnny.Kaixin.WinUI
{
    partial class DlgFriendValidation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgFriendValidation));
            this.grpAccount = new System.Windows.Forms.GroupBox();
            this.txtValidationCode = new System.Windows.Forms.TextBox();
            this.imgValidationCode = new System.Windows.Forms.PictureBox();
            this.lblValidationCode = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkRemember = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblUserId = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblWarning = new System.Windows.Forms.Label();
            this.btnConnection = new System.Windows.Forms.Button();
            this.grpAccount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgValidationCode)).BeginInit();
            this.SuspendLayout();
            // 
            // grpAccount
            // 
            this.grpAccount.Controls.Add(this.txtValidationCode);
            this.grpAccount.Controls.Add(this.imgValidationCode);
            this.grpAccount.Controls.Add(this.lblValidationCode);
            this.grpAccount.Controls.Add(this.label2);
            this.grpAccount.Controls.Add(this.label1);
            this.grpAccount.Controls.Add(this.chkRemember);
            this.grpAccount.Controls.Add(this.txtPassword);
            this.grpAccount.Controls.Add(this.lblUserId);
            this.grpAccount.Controls.Add(this.txtEmail);
            this.grpAccount.Controls.Add(this.lblUserName);
            this.grpAccount.Location = new System.Drawing.Point(21, 17);
            this.grpAccount.Name = "grpAccount";
            this.grpAccount.Size = new System.Drawing.Size(331, 177);
            this.grpAccount.TabIndex = 1;
            this.grpAccount.TabStop = false;
            this.grpAccount.Text = "您的开心网信息";
            // 
            // txtValidationCode
            // 
            this.txtValidationCode.BackColor = System.Drawing.SystemColors.Window;
            this.txtValidationCode.Location = new System.Drawing.Point(84, 88);
            this.txtValidationCode.Name = "txtValidationCode";
            this.txtValidationCode.Size = new System.Drawing.Size(68, 20);
            this.txtValidationCode.TabIndex = 7;
            // 
            // imgValidationCode
            // 
            this.imgValidationCode.Location = new System.Drawing.Point(158, 88);
            this.imgValidationCode.Name = "imgValidationCode";
            this.imgValidationCode.Size = new System.Drawing.Size(140, 50);
            this.imgValidationCode.TabIndex = 14;
            this.imgValidationCode.TabStop = false;
            // 
            // lblValidationCode
            // 
            this.lblValidationCode.AutoSize = true;
            this.lblValidationCode.Location = new System.Drawing.Point(31, 91);
            this.lblValidationCode.Name = "lblValidationCode";
            this.lblValidationCode.Size = new System.Drawing.Size(55, 13);
            this.lblValidationCode.TabIndex = 6;
            this.lblValidationCode.Text = "验证码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(244, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(277, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "*";
            // 
            // chkRemember
            // 
            this.chkRemember.AutoSize = true;
            this.chkRemember.Location = new System.Drawing.Point(34, 146);
            this.chkRemember.Name = "chkRemember";
            this.chkRemember.Size = new System.Drawing.Size(98, 17);
            this.chkRemember.TabIndex = 8;
            this.chkRemember.Text = "记住登录信息";
            this.chkRemember.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.SystemColors.Window;
            this.txtPassword.Location = new System.Drawing.Point(84, 59);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(154, 20);
            this.txtPassword.TabIndex = 5;
            // 
            // lblUserId
            // 
            this.lblUserId.AutoSize = true;
            this.lblUserId.Location = new System.Drawing.Point(31, 62);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(43, 13);
            this.lblUserId.TabIndex = 4;
            this.lblUserId.Text = "密码：";
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.SystemColors.Window;
            this.txtEmail.Location = new System.Drawing.Point(84, 29);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(187, 20);
            this.txtEmail.TabIndex = 3;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(31, 33);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(44, 13);
            this.lblUserName.TabIndex = 2;
            this.lblUserName.Text = "Email：";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(65, 243);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(234, 243);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblWarning
            // 
            this.lblWarning.AutoSize = true;
            this.lblWarning.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarning.ForeColor = System.Drawing.Color.Red;
            this.lblWarning.Location = new System.Drawing.Point(57, 208);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(277, 14);
            this.lblWarning.TabIndex = 9;
            this.lblWarning.Text = "注意：请勿随意传播本外挂，谢谢合作！";
            // 
            // btnConnection
            // 
            this.btnConnection.Location = new System.Drawing.Point(150, 243);
            this.btnConnection.Name = "btnConnection";
            this.btnConnection.Size = new System.Drawing.Size(75, 23);
            this.btnConnection.TabIndex = 11;
            this.btnConnection.Text = "连接";
            this.btnConnection.UseVisualStyleBackColor = true;
            this.btnConnection.Click += new System.EventHandler(this.btnConnection_Click);
            // 
            // DlgFriendValidation
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(374, 290);
            this.Controls.Add(this.btnConnection);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.grpAccount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgFriendValidation";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "登录开心助手";
            this.Load += new System.EventHandler(this.DlgFriendValidation_Load);
            this.grpAccount.ResumeLayout(false);
            this.grpAccount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgValidationCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpAccount;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.CheckBox chkRemember;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Label lblValidationCode;
        private System.Windows.Forms.TextBox txtValidationCode;
        private System.Windows.Forms.PictureBox imgValidationCode;
        private System.Windows.Forms.Button btnConnection;
    }
}