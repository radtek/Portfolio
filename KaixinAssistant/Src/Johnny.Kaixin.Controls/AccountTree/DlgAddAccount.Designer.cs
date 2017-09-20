namespace Johnny.Kaixin.Controls.AccountTree
{
    partial class DlgAddAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgAddAccount));
            this.lblEmail = new System.Windows.Forms.Label();
            this.grpAccount = new System.Windows.Forms.GroupBox();
            this.imgValidationCode = new System.Windows.Forms.PictureBox();
            this.txtValidationCode = new System.Windows.Forms.TextBox();
            this.lblValidationCode = new System.Windows.Forms.Label();
            this.txtGender = new System.Windows.Forms.TextBox();
            this.lblGender = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.lblUserId = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpAccount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgValidationCode)).BeginInit();
            this.SuspendLayout();
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(34, 37);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(67, 13);
            this.lblEmail.TabIndex = 2;
            this.lblEmail.Text = "邮件地址：";
            // 
            // grpAccount
            // 
            this.grpAccount.Controls.Add(this.imgValidationCode);
            this.grpAccount.Controls.Add(this.txtValidationCode);
            this.grpAccount.Controls.Add(this.lblValidationCode);
            this.grpAccount.Controls.Add(this.txtGender);
            this.grpAccount.Controls.Add(this.lblGender);
            this.grpAccount.Controls.Add(this.label2);
            this.grpAccount.Controls.Add(this.label1);
            this.grpAccount.Controls.Add(this.txtUserId);
            this.grpAccount.Controls.Add(this.lblUserId);
            this.grpAccount.Controls.Add(this.txtPassword);
            this.grpAccount.Controls.Add(this.txtUserName);
            this.grpAccount.Controls.Add(this.lblPassword);
            this.grpAccount.Controls.Add(this.lblUserName);
            this.grpAccount.Controls.Add(this.txtEmail);
            this.grpAccount.Controls.Add(this.lblEmail);
            this.grpAccount.Location = new System.Drawing.Point(22, 12);
            this.grpAccount.Name = "grpAccount";
            this.grpAccount.Size = new System.Drawing.Size(379, 246);
            this.grpAccount.TabIndex = 1;
            this.grpAccount.TabStop = false;
            this.grpAccount.Text = "用户";
            // 
            // imgValidationCode
            // 
            this.imgValidationCode.Location = new System.Drawing.Point(179, 94);
            this.imgValidationCode.Name = "imgValidationCode";
            this.imgValidationCode.Size = new System.Drawing.Size(140, 50);
            this.imgValidationCode.TabIndex = 20;
            this.imgValidationCode.TabStop = false;
            // 
            // txtValidationCode
            // 
            this.txtValidationCode.BackColor = System.Drawing.SystemColors.Window;
            this.txtValidationCode.Location = new System.Drawing.Point(105, 94);
            this.txtValidationCode.Name = "txtValidationCode";
            this.txtValidationCode.Size = new System.Drawing.Size(68, 20);
            this.txtValidationCode.TabIndex = 7;
            // 
            // lblValidationCode
            // 
            this.lblValidationCode.AutoSize = true;
            this.lblValidationCode.Location = new System.Drawing.Point(34, 97);
            this.lblValidationCode.Name = "lblValidationCode";
            this.lblValidationCode.Size = new System.Drawing.Size(55, 13);
            this.lblValidationCode.TabIndex = 6;
            this.lblValidationCode.Text = "验证码：";
            // 
            // txtGender
            // 
            this.txtGender.Location = new System.Drawing.Point(105, 211);
            this.txtGender.Name = "txtGender";
            this.txtGender.ReadOnly = true;
            this.txtGender.Size = new System.Drawing.Size(51, 20);
            this.txtGender.TabIndex = 13;
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Location = new System.Drawing.Point(34, 214);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(43, 13);
            this.lblGender.TabIndex = 12;
            this.lblGender.Text = "性别：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(248, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(335, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "*";
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(105, 181);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.ReadOnly = true;
            this.txtUserId.Size = new System.Drawing.Size(111, 20);
            this.txtUserId.TabIndex = 11;
            // 
            // lblUserId
            // 
            this.lblUserId.AutoSize = true;
            this.lblUserId.Location = new System.Drawing.Point(34, 184);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(54, 13);
            this.lblUserId.TabIndex = 10;
            this.lblUserId.Text = "用户ID：";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(105, 64);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(137, 20);
            this.txtPassword.TabIndex = 5;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(105, 151);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ReadOnly = true;
            this.txtUserName.Size = new System.Drawing.Size(154, 20);
            this.txtUserName.TabIndex = 9;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(34, 67);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(43, 13);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "密码：";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(34, 154);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(55, 13);
            this.lblUserName.TabIndex = 8;
            this.lblUserName.Text = "用户名：";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(105, 34);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(224, 20);
            this.txtEmail.TabIndex = 3;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(90, 273);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 14;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(261, 273);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // DlgAddAccount
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(422, 324);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.grpAccount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgAddAccount";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加用户";
            this.Load += new System.EventHandler(this.DlgAddAccount_Load);
            this.grpAccount.ResumeLayout(false);
            this.grpAccount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgValidationCode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.GroupBox grpAccount;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGender;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.PictureBox imgValidationCode;
        private System.Windows.Forms.TextBox txtValidationCode;
        private System.Windows.Forms.Label lblValidationCode;
    }
}