namespace Johnny.Kaixin.WinUI
{
    partial class FrmSendMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSendMessage));
            this.grpAccounts = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnUnselectAll = new System.Windows.Forms.Button();
            this.btnUnselectOne = new System.Windows.Forms.Button();
            this.btnSelectOne = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSelectedAccounts = new System.Windows.Forms.Label();
            this.lstSelectedAccounts = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblAllAccounts = new System.Windows.Forms.Label();
            this.lstAllAccounts = new System.Windows.Forms.ListBox();
            this.lblSender = new System.Windows.Forms.Label();
            this.cmbSender = new System.Windows.Forms.ComboBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblReceivers = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblGroup = new System.Windows.Forms.Label();
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.lblSendStyle = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdbMulti = new System.Windows.Forms.RadioButton();
            this.rdbSingle = new System.Windows.Forms.RadioButton();
            this.grpAccounts.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpAccounts
            // 
            this.grpAccounts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAccounts.Controls.Add(this.tableLayoutPanel1);
            this.grpAccounts.Location = new System.Drawing.Point(115, 95);
            this.grpAccounts.Name = "grpAccounts";
            this.grpAccounts.Size = new System.Drawing.Size(473, 267);
            this.grpAccounts.TabIndex = 26;
            this.grpAccounts.TabStop = false;
            this.grpAccounts.Text = "接收者的帐号";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(467, 247);
            this.tableLayoutPanel1.TabIndex = 20;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSelectAll);
            this.panel1.Controls.Add(this.btnUnselectAll);
            this.panel1.Controls.Add(this.btnUnselectOne);
            this.panel1.Controls.Add(this.btnSelectOne);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(196, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(74, 241);
            this.panel1.TabIndex = 21;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(18, 91);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(40, 23);
            this.btnSelectAll.TabIndex = 14;
            this.btnSelectAll.Text = ">>";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnUnselectAll
            // 
            this.btnUnselectAll.Location = new System.Drawing.Point(18, 199);
            this.btnUnselectAll.Name = "btnUnselectAll";
            this.btnUnselectAll.Size = new System.Drawing.Size(40, 23);
            this.btnUnselectAll.TabIndex = 16;
            this.btnUnselectAll.Text = "<<";
            this.btnUnselectAll.UseVisualStyleBackColor = true;
            this.btnUnselectAll.Click += new System.EventHandler(this.btnUnselectAll_Click);
            // 
            // btnUnselectOne
            // 
            this.btnUnselectOne.Location = new System.Drawing.Point(18, 146);
            this.btnUnselectOne.Name = "btnUnselectOne";
            this.btnUnselectOne.Size = new System.Drawing.Size(40, 23);
            this.btnUnselectOne.TabIndex = 15;
            this.btnUnselectOne.Text = "<";
            this.btnUnselectOne.UseVisualStyleBackColor = true;
            this.btnUnselectOne.Click += new System.EventHandler(this.btnUnselectOne_Click);
            // 
            // btnSelectOne
            // 
            this.btnSelectOne.Location = new System.Drawing.Point(18, 40);
            this.btnSelectOne.Name = "btnSelectOne";
            this.btnSelectOne.Size = new System.Drawing.Size(40, 23);
            this.btnSelectOne.TabIndex = 13;
            this.btnSelectOne.Text = ">";
            this.btnSelectOne.UseVisualStyleBackColor = true;
            this.btnSelectOne.Click += new System.EventHandler(this.btnSelectOne_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.lblSelectedAccounts, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lstSelectedAccounts, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(276, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(188, 241);
            this.tableLayoutPanel2.TabIndex = 22;
            // 
            // lblSelectedAccounts
            // 
            this.lblSelectedAccounts.AutoSize = true;
            this.lblSelectedAccounts.ForeColor = System.Drawing.Color.Red;
            this.lblSelectedAccounts.Location = new System.Drawing.Point(3, 0);
            this.lblSelectedAccounts.Name = "lblSelectedAccounts";
            this.lblSelectedAccounts.Size = new System.Drawing.Size(119, 12);
            this.lblSelectedAccounts.TabIndex = 17;
            this.lblSelectedAccounts.Text = "*需要发送给下列账号";
            // 
            // lstSelectedAccounts
            // 
            this.lstSelectedAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSelectedAccounts.FormattingEnabled = true;
            this.lstSelectedAccounts.ItemHeight = 12;
            this.lstSelectedAccounts.Location = new System.Drawing.Point(3, 26);
            this.lstSelectedAccounts.Name = "lstSelectedAccounts";
            this.lstSelectedAccounts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstSelectedAccounts.Size = new System.Drawing.Size(182, 208);
            this.lstSelectedAccounts.TabIndex = 12;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.lblAllAccounts, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lstAllAccounts, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(187, 241);
            this.tableLayoutPanel3.TabIndex = 23;
            // 
            // lblAllAccounts
            // 
            this.lblAllAccounts.AutoSize = true;
            this.lblAllAccounts.ForeColor = System.Drawing.Color.Red;
            this.lblAllAccounts.Location = new System.Drawing.Point(3, 0);
            this.lblAllAccounts.Name = "lblAllAccounts";
            this.lblAllAccounts.Size = new System.Drawing.Size(83, 12);
            this.lblAllAccounts.TabIndex = 11;
            this.lblAllAccounts.Text = "*我的全部好友";
            // 
            // lstAllAccounts
            // 
            this.lstAllAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAllAccounts.FormattingEnabled = true;
            this.lstAllAccounts.ItemHeight = 12;
            this.lstAllAccounts.Location = new System.Drawing.Point(3, 26);
            this.lstAllAccounts.Name = "lstAllAccounts";
            this.lstAllAccounts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstAllAccounts.Size = new System.Drawing.Size(181, 208);
            this.lstAllAccounts.TabIndex = 8;
            // 
            // lblSender
            // 
            this.lblSender.AutoSize = true;
            this.lblSender.Location = new System.Drawing.Point(44, 40);
            this.lblSender.Name = "lblSender";
            this.lblSender.Size = new System.Drawing.Size(53, 12);
            this.lblSender.TabIndex = 27;
            this.lblSender.Text = "发送者：";
            // 
            // cmbSender
            // 
            this.cmbSender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSender.FormattingEnabled = true;
            this.cmbSender.Location = new System.Drawing.Point(115, 37);
            this.cmbSender.MaxDropDownItems = 16;
            this.cmbSender.Name = "cmbSender";
            this.cmbSender.Size = new System.Drawing.Size(141, 20);
            this.cmbSender.TabIndex = 28;
            this.cmbSender.SelectedIndexChanged += new System.EventHandler(this.cmbSender_SelectedIndexChanged);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(625, 72);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(133, 43);
            this.btnSend.TabIndex = 30;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(56, 368);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(41, 12);
            this.lblMessage.TabIndex = 31;
            this.lblMessage.Text = "内容：";
            // 
            // lblReceivers
            // 
            this.lblReceivers.AutoSize = true;
            this.lblReceivers.Location = new System.Drawing.Point(44, 103);
            this.lblReceivers.Name = "lblReceivers";
            this.lblReceivers.Size = new System.Drawing.Size(53, 12);
            this.lblReceivers.TabIndex = 32;
            this.lblReceivers.Text = "接收者：";
            // 
            // txtMessage
            // 
            this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessage.Location = new System.Drawing.Point(115, 368);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(473, 138);
            this.txtMessage.TabIndex = 33;
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(625, 127);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(133, 43);
            this.btnStop.TabIndex = 34;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(68, 14);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(29, 12);
            this.lblGroup.TabIndex = 55;
            this.lblGroup.Text = "组：";
            // 
            // cmbGroup
            // 
            this.cmbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.Location = new System.Drawing.Point(115, 11);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(141, 20);
            this.cmbGroup.TabIndex = 54;
            this.cmbGroup.SelectedIndexChanged += new System.EventHandler(this.cmbGroup_SelectedIndexChanged);
            // 
            // lblSendStyle
            // 
            this.lblSendStyle.AutoSize = true;
            this.lblSendStyle.Location = new System.Drawing.Point(44, 72);
            this.lblSendStyle.Name = "lblSendStyle";
            this.lblSendStyle.Size = new System.Drawing.Size(65, 12);
            this.lblSendStyle.TabIndex = 56;
            this.lblSendStyle.Text = "发送方式：";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rdbMulti);
            this.panel2.Controls.Add(this.rdbSingle);
            this.panel2.Location = new System.Drawing.Point(115, 67);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(401, 26);
            this.panel2.TabIndex = 57;
            // 
            // rdbMulti
            // 
            this.rdbMulti.AutoSize = true;
            this.rdbMulti.Location = new System.Drawing.Point(115, 5);
            this.rdbMulti.Name = "rdbMulti";
            this.rdbMulti.Size = new System.Drawing.Size(167, 16);
            this.rdbMulti.TabIndex = 1;
            this.rdbMulti.Text = "多人群发（一次最多30人）";
            this.rdbMulti.UseVisualStyleBackColor = true;
            // 
            // rdbSingle
            // 
            this.rdbSingle.AutoSize = true;
            this.rdbSingle.Checked = true;
            this.rdbSingle.Location = new System.Drawing.Point(3, 3);
            this.rdbSingle.Name = "rdbSingle";
            this.rdbSingle.Size = new System.Drawing.Size(95, 16);
            this.rdbSingle.TabIndex = 0;
            this.rdbSingle.TabStop = true;
            this.rdbSingle.Text = "单人循环发送";
            this.rdbSingle.UseVisualStyleBackColor = true;
            // 
            // FrmSendMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(792, 521);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblSendStyle);
            this.Controls.Add(this.lblGroup);
            this.Controls.Add(this.cmbGroup);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.lblReceivers);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.cmbSender);
            this.Controls.Add(this.lblSender);
            this.Controls.Add(this.grpAccounts);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "FrmSendMessage";
            this.TabText = "群发消息";
            this.Text = "群发消息";
            this.Load += new System.EventHandler(this.FrmSendMessage_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSendMessage_FormClosing);
            this.grpAccounts.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpAccounts;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnUnselectAll;
        private System.Windows.Forms.Button btnUnselectOne;
        private System.Windows.Forms.Button btnSelectOne;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblSelectedAccounts;
        private System.Windows.Forms.ListBox lstSelectedAccounts;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblAllAccounts;
        private System.Windows.Forms.ListBox lstAllAccounts;
        private System.Windows.Forms.Label lblSender;
        private System.Windows.Forms.ComboBox cmbSender;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblReceivers;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.Label lblSendStyle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rdbMulti;
        private System.Windows.Forms.RadioButton rdbSingle;
    }
}