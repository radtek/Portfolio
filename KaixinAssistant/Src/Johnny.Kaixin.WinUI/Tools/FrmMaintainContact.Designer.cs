namespace Johnny.Kaixin.WinUI
{
    partial class FrmMaintainContact
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMaintainContact));
            this.grpOutput = new System.Windows.Forms.GroupBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.lblOutputPath = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblSelectedAccounts = new System.Windows.Forms.Label();
            this.btnSaveToFile = new System.Windows.Forms.Button();
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.listBoxSelectorAccounts = new Johnny.Kaixin.Controls.ListBoxSelector.AccountListBoxSelector();
            this.grpRequestFriends = new System.Windows.Forms.GroupBox();
            this.txtRequestContent = new System.Windows.Forms.TextBox();
            this.lblRequest = new System.Windows.Forms.Label();
            this.btnSendRequest = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnStop2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnImport = new System.Windows.Forms.Button();
            this.cmbGroupAdd = new System.Windows.Forms.ComboBox();
            this.listBoxSelectorFriends = new Johnny.Kaixin.Controls.ListBoxSelector.FriendListBoxSelector();
            this.lblGroup = new System.Windows.Forms.Label();
            this.cmbAccountAdd = new System.Windows.Forms.ComboBox();
            this.lblSender = new System.Windows.Forms.Label();
            this.grpOutput.SuspendLayout();
            this.grpRequestFriends.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpOutput
            // 
            this.grpOutput.Controls.Add(this.btnSelectFolder);
            this.grpOutput.Controls.Add(this.txtOutputPath);
            this.grpOutput.Controls.Add(this.lblOutputPath);
            this.grpOutput.Controls.Add(this.btnStop);
            this.grpOutput.Controls.Add(this.lblSelectedAccounts);
            this.grpOutput.Controls.Add(this.btnSaveToFile);
            this.grpOutput.Controls.Add(this.cmbGroup);
            this.grpOutput.Controls.Add(this.listBoxSelectorAccounts);
            this.grpOutput.Location = new System.Drawing.Point(13, 11);
            this.grpOutput.Name = "grpOutput";
            this.grpOutput.Size = new System.Drawing.Size(592, 323);
            this.grpOutput.TabIndex = 0;
            this.grpOutput.TabStop = false;
            this.grpOutput.Text = "导出好友列表";
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(420, 287);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(31, 25);
            this.btnSelectFolder.TabIndex = 23;
            this.btnSelectFolder.Text = "...";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Location = new System.Drawing.Point(96, 289);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(318, 20);
            this.txtOutputPath.TabIndex = 22;
            // 
            // lblOutputPath
            // 
            this.lblOutputPath.AutoSize = true;
            this.lblOutputPath.Location = new System.Drawing.Point(25, 293);
            this.lblOutputPath.Name = "lblOutputPath";
            this.lblOutputPath.Size = new System.Drawing.Size(67, 13);
            this.lblOutputPath.TabIndex = 21;
            this.lblOutputPath.Text = "导出目录：";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(475, 150);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(88, 39);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblSelectedAccounts
            // 
            this.lblSelectedAccounts.AutoSize = true;
            this.lblSelectedAccounts.ForeColor = System.Drawing.Color.Red;
            this.lblSelectedAccounts.Location = new System.Drawing.Point(268, 28);
            this.lblSelectedAccounts.Name = "lblSelectedAccounts";
            this.lblSelectedAccounts.Size = new System.Drawing.Size(95, 13);
            this.lblSelectedAccounts.TabIndex = 20;
            this.lblSelectedAccounts.Text = "*需要执行的账号";
            // 
            // btnSaveToFile
            // 
            this.btnSaveToFile.Location = new System.Drawing.Point(475, 80);
            this.btnSaveToFile.Name = "btnSaveToFile";
            this.btnSaveToFile.Size = new System.Drawing.Size(88, 44);
            this.btnSaveToFile.TabIndex = 2;
            this.btnSaveToFile.Text = "导出所有好友";
            this.btnSaveToFile.UseVisualStyleBackColor = true;
            this.btnSaveToFile.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // cmbGroup
            // 
            this.cmbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.Location = new System.Drawing.Point(21, 20);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(153, 21);
            this.cmbGroup.TabIndex = 19;
            this.cmbGroup.SelectedIndexChanged += new System.EventHandler(this.cmbGroup_SelectedIndexChanged);
            // 
            // listBoxSelectorAccounts
            // 
            this.listBoxSelectorAccounts.AllItems = null;
            this.listBoxSelectorAccounts.Location = new System.Drawing.Point(21, 48);
            this.listBoxSelectorAccounts.Name = "listBoxSelectorAccounts";
            this.listBoxSelectorAccounts.Size = new System.Drawing.Size(431, 228);
            this.listBoxSelectorAccounts.TabIndex = 0;
            // 
            // grpRequestFriends
            // 
            this.grpRequestFriends.Controls.Add(this.txtRequestContent);
            this.grpRequestFriends.Controls.Add(this.lblRequest);
            this.grpRequestFriends.Controls.Add(this.btnSendRequest);
            this.grpRequestFriends.Controls.Add(this.label3);
            this.grpRequestFriends.Controls.Add(this.btnStop2);
            this.grpRequestFriends.Controls.Add(this.label2);
            this.grpRequestFriends.Controls.Add(this.btnImport);
            this.grpRequestFriends.Controls.Add(this.cmbGroupAdd);
            this.grpRequestFriends.Controls.Add(this.listBoxSelectorFriends);
            this.grpRequestFriends.Controls.Add(this.lblGroup);
            this.grpRequestFriends.Controls.Add(this.cmbAccountAdd);
            this.grpRequestFriends.Controls.Add(this.lblSender);
            this.grpRequestFriends.Location = new System.Drawing.Point(13, 342);
            this.grpRequestFriends.Name = "grpRequestFriends";
            this.grpRequestFriends.Size = new System.Drawing.Size(693, 335);
            this.grpRequestFriends.TabIndex = 1;
            this.grpRequestFriends.TabStop = false;
            this.grpRequestFriends.Text = "请求添加好友";
            // 
            // txtRequestContent
            // 
            this.txtRequestContent.Location = new System.Drawing.Point(475, 86);
            this.txtRequestContent.Multiline = true;
            this.txtRequestContent.Name = "txtRequestContent";
            this.txtRequestContent.Size = new System.Drawing.Size(200, 107);
            this.txtRequestContent.TabIndex = 65;
            // 
            // lblRequest
            // 
            this.lblRequest.AutoSize = true;
            this.lblRequest.Location = new System.Drawing.Point(473, 69);
            this.lblRequest.Name = "lblRequest";
            this.lblRequest.Size = new System.Drawing.Size(67, 13);
            this.lblRequest.TabIndex = 64;
            this.lblRequest.Text = "请求内容：";
            // 
            // btnSendRequest
            // 
            this.btnSendRequest.Location = new System.Drawing.Point(475, 222);
            this.btnSendRequest.Name = "btnSendRequest";
            this.btnSendRequest.Size = new System.Drawing.Size(88, 39);
            this.btnSendRequest.TabIndex = 63;
            this.btnSendRequest.Text = "发送请求";
            this.btnSendRequest.UseVisualStyleBackColor = true;
            this.btnSendRequest.Click += new System.EventHandler(this.btnSendRequest_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(269, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(203, 13);
            this.label3.TabIndex = 62;
            this.label3.Text = "*向以下列表中的人发送添加好友请求";
            // 
            // btnStop2
            // 
            this.btnStop2.Location = new System.Drawing.Point(587, 222);
            this.btnStop2.Name = "btnStop2";
            this.btnStop2.Size = new System.Drawing.Size(88, 39);
            this.btnStop2.TabIndex = 61;
            this.btnStop2.Text = "停止";
            this.btnStop2.UseVisualStyleBackColor = true;
            this.btnStop2.Click += new System.EventHandler(this.btnStop2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(24, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 60;
            this.label2.Text = "*文件中的所有好友";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(21, 303);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(53, 25);
            this.btnImport.TabIndex = 59;
            this.btnImport.Text = "导入";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // cmbGroupAdd
            // 
            this.cmbGroupAdd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroupAdd.FormattingEnabled = true;
            this.cmbGroupAdd.Location = new System.Drawing.Point(46, 22);
            this.cmbGroupAdd.Name = "cmbGroupAdd";
            this.cmbGroupAdd.Size = new System.Drawing.Size(141, 21);
            this.cmbGroupAdd.TabIndex = 56;
            this.cmbGroupAdd.SelectedIndexChanged += new System.EventHandler(this.cmbGroupAdd_SelectedIndexChanged);
            // 
            // listBoxSelectorFriends
            // 
            this.listBoxSelectorFriends.AllItems = null;
            this.listBoxSelectorFriends.Location = new System.Drawing.Point(21, 69);
            this.listBoxSelectorFriends.Name = "listBoxSelectorFriends";
            this.listBoxSelectorFriends.Size = new System.Drawing.Size(431, 228);
            this.listBoxSelectorFriends.TabIndex = 58;
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(22, 25);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(31, 13);
            this.lblGroup.TabIndex = 57;
            this.lblGroup.Text = "组：";
            // 
            // cmbAccountAdd
            // 
            this.cmbAccountAdd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccountAdd.FormattingEnabled = true;
            this.cmbAccountAdd.Location = new System.Drawing.Point(256, 22);
            this.cmbAccountAdd.MaxDropDownItems = 16;
            this.cmbAccountAdd.Name = "cmbAccountAdd";
            this.cmbAccountAdd.Size = new System.Drawing.Size(141, 21);
            this.cmbAccountAdd.TabIndex = 55;
            // 
            // lblSender
            // 
            this.lblSender.AutoSize = true;
            this.lblSender.Location = new System.Drawing.Point(218, 25);
            this.lblSender.Name = "lblSender";
            this.lblSender.Size = new System.Drawing.Size(43, 13);
            this.lblSender.TabIndex = 54;
            this.lblSender.Text = "账号：";
            // 
            // FrmMaintainContact
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(720, 685);
            this.Controls.Add(this.grpRequestFriends);
            this.Controls.Add(this.grpOutput);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMaintainContact";
            this.TabText = "维护联系人";
            this.Text = "维护联系人";
            this.Load += new System.EventHandler(this.FrmMaintainContact_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMaintainContact_FormClosing);
            this.grpOutput.ResumeLayout(false);
            this.grpOutput.PerformLayout();
            this.grpRequestFriends.ResumeLayout(false);
            this.grpRequestFriends.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpOutput;
        private Johnny.Kaixin.Controls.ListBoxSelector.AccountListBoxSelector listBoxSelectorAccounts;
        private System.Windows.Forms.GroupBox grpRequestFriends;
        private System.Windows.Forms.Button btnSaveToFile;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.Label lblSelectedAccounts;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.Label lblOutputPath;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.ComboBox cmbGroupAdd;
        private System.Windows.Forms.ComboBox cmbAccountAdd;
        private System.Windows.Forms.Label lblSender;
        private Johnny.Kaixin.Controls.ListBoxSelector.FriendListBoxSelector listBoxSelectorFriends;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnSendRequest;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnStop2;
        private System.Windows.Forms.TextBox txtRequestContent;
        private System.Windows.Forms.Label lblRequest;
    }
}