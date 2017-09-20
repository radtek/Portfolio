namespace Johnny.Kaixin.WinUI
{
    partial class FrmAddFriends
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddFriends));
            this.btnRun = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.chkDeleteAllMessage = new System.Windows.Forms.CheckBox();
            this.grpMode = new System.Windows.Forms.GroupBox();
            this.chkExecuteConfirmRequest = new System.Windows.Forms.CheckBox();
            this.chkExecuteSendRequest = new System.Windows.Forms.CheckBox();
            this.lblHelpMode = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            this.rdbManual = new System.Windows.Forms.RadioButton();
            this.rdbAuto = new System.Windows.Forms.RadioButton();
            this.grpDeleteAllMessage = new System.Windows.Forms.GroupBox();
            this.lblHelpDeleteAllMessage = new System.Windows.Forms.Label();
            this.grpNewAccounts = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnUnselectAll = new System.Windows.Forms.Button();
            this.btnUnselectOne = new System.Windows.Forms.Button();
            this.btnSelectOne = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblNewAccounts = new System.Windows.Forms.Label();
            this.lstNewAccounts = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblAllNewAccounts = new System.Windows.Forms.Label();
            this.lstAllNewAccounts = new System.Windows.Forms.ListBox();
            this.grpOldAccounts = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSelectAllOld = new System.Windows.Forms.Button();
            this.btnUnselectAllOld = new System.Windows.Forms.Button();
            this.btnUnselectOneOld = new System.Windows.Forms.Button();
            this.btnSelectOneOld = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.lblOldAccounts = new System.Windows.Forms.Label();
            this.lstOldAccounts = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.lblAllOldAccounts = new System.Windows.Forms.Label();
            this.lstAllOldAccounts = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanelParent = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblGroup = new System.Windows.Forms.Label();
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.grpMode.SuspendLayout();
            this.grpDeleteAllMessage.SuspendLayout();
            this.grpNewAccounts.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.grpOldAccounts.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanelParent.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Location = new System.Drawing.Point(24, 219);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 33);
            this.btnRun.TabIndex = 21;
            this.btnRun.Text = "运行";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(126, 220);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 31);
            this.btnStop.TabIndex = 27;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // chkDeleteAllMessage
            // 
            this.chkDeleteAllMessage.AutoSize = true;
            this.chkDeleteAllMessage.Location = new System.Drawing.Point(9, 22);
            this.chkDeleteAllMessage.Name = "chkDeleteAllMessage";
            this.chkDeleteAllMessage.Size = new System.Drawing.Size(182, 17);
            this.chkDeleteAllMessage.TabIndex = 29;
            this.chkDeleteAllMessage.Text = "先清空所有账号内的系统消息";
            this.chkDeleteAllMessage.UseVisualStyleBackColor = true;
            // 
            // grpMode
            // 
            this.grpMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMode.Controls.Add(this.chkExecuteConfirmRequest);
            this.grpMode.Controls.Add(this.chkExecuteSendRequest);
            this.grpMode.Controls.Add(this.lblHelpMode);
            this.grpMode.Controls.Add(this.lblMode);
            this.grpMode.Controls.Add(this.rdbManual);
            this.grpMode.Controls.Add(this.rdbAuto);
            this.grpMode.Location = new System.Drawing.Point(11, 0);
            this.grpMode.Name = "grpMode";
            this.grpMode.Size = new System.Drawing.Size(200, 125);
            this.grpMode.TabIndex = 30;
            this.grpMode.TabStop = false;
            this.grpMode.Text = "添加模式";
            // 
            // chkExecuteConfirmRequest
            // 
            this.chkExecuteConfirmRequest.AutoSize = true;
            this.chkExecuteConfirmRequest.Location = new System.Drawing.Point(107, 92);
            this.chkExecuteConfirmRequest.Name = "chkExecuteConfirmRequest";
            this.chkExecuteConfirmRequest.Size = new System.Drawing.Size(74, 17);
            this.chkExecuteConfirmRequest.TabIndex = 34;
            this.chkExecuteConfirmRequest.Text = "同意请求";
            this.chkExecuteConfirmRequest.UseVisualStyleBackColor = true;
            // 
            // chkExecuteSendRequest
            // 
            this.chkExecuteSendRequest.AutoSize = true;
            this.chkExecuteSendRequest.Location = new System.Drawing.Point(13, 92);
            this.chkExecuteSendRequest.Name = "chkExecuteSendRequest";
            this.chkExecuteSendRequest.Size = new System.Drawing.Size(74, 17);
            this.chkExecuteSendRequest.TabIndex = 33;
            this.chkExecuteSendRequest.Text = "发送请求";
            this.chkExecuteSendRequest.UseVisualStyleBackColor = true;
            // 
            // lblHelpMode
            // 
            this.lblHelpMode.AutoSize = true;
            this.lblHelpMode.ForeColor = System.Drawing.Color.Red;
            this.lblHelpMode.Location = new System.Drawing.Point(7, 59);
            this.lblHelpMode.Name = "lblHelpMode";
            this.lblHelpMode.Size = new System.Drawing.Size(143, 13);
            this.lblHelpMode.TabIndex = 32;
            this.lblHelpMode.Text = "*手动模式具有更好的效率";
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Location = new System.Drawing.Point(7, 27);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(43, 13);
            this.lblMode.TabIndex = 31;
            this.lblMode.Text = "模式：";
            // 
            // rdbManual
            // 
            this.rdbManual.AutoSize = true;
            this.rdbManual.Location = new System.Drawing.Point(107, 27);
            this.rdbManual.Name = "rdbManual";
            this.rdbManual.Size = new System.Drawing.Size(49, 17);
            this.rdbManual.TabIndex = 1;
            this.rdbManual.TabStop = true;
            this.rdbManual.Text = "手动";
            this.rdbManual.UseVisualStyleBackColor = true;
            this.rdbManual.CheckedChanged += new System.EventHandler(this.rdbManual_CheckedChanged);
            // 
            // rdbAuto
            // 
            this.rdbAuto.AutoSize = true;
            this.rdbAuto.Location = new System.Drawing.Point(54, 27);
            this.rdbAuto.Name = "rdbAuto";
            this.rdbAuto.Size = new System.Drawing.Size(49, 17);
            this.rdbAuto.TabIndex = 0;
            this.rdbAuto.TabStop = true;
            this.rdbAuto.Text = "自动";
            this.rdbAuto.UseVisualStyleBackColor = true;
            this.rdbAuto.CheckedChanged += new System.EventHandler(this.rdbAuto_CheckedChanged);
            // 
            // grpDeleteAllMessage
            // 
            this.grpDeleteAllMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDeleteAllMessage.Controls.Add(this.lblHelpDeleteAllMessage);
            this.grpDeleteAllMessage.Controls.Add(this.chkDeleteAllMessage);
            this.grpDeleteAllMessage.Location = new System.Drawing.Point(11, 134);
            this.grpDeleteAllMessage.Name = "grpDeleteAllMessage";
            this.grpDeleteAllMessage.Size = new System.Drawing.Size(200, 75);
            this.grpDeleteAllMessage.TabIndex = 31;
            this.grpDeleteAllMessage.TabStop = false;
            this.grpDeleteAllMessage.Text = "删除系统消息";
            // 
            // lblHelpDeleteAllMessage
            // 
            this.lblHelpDeleteAllMessage.AutoSize = true;
            this.lblHelpDeleteAllMessage.ForeColor = System.Drawing.Color.Red;
            this.lblHelpDeleteAllMessage.Location = new System.Drawing.Point(11, 55);
            this.lblHelpDeleteAllMessage.Name = "lblHelpDeleteAllMessage";
            this.lblHelpDeleteAllMessage.Size = new System.Drawing.Size(179, 13);
            this.lblHelpDeleteAllMessage.TabIndex = 33;
            this.lblHelpDeleteAllMessage.Text = "*删除系统消息将具有更高的效率";
            // 
            // grpNewAccounts
            // 
            this.grpNewAccounts.Controls.Add(this.tableLayoutPanel1);
            this.grpNewAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpNewAccounts.Location = new System.Drawing.Point(3, 33);
            this.grpNewAccounts.Name = "grpNewAccounts";
            this.grpNewAccounts.Size = new System.Drawing.Size(556, 272);
            this.grpNewAccounts.TabIndex = 32;
            this.grpNewAccounts.TabStop = false;
            this.grpNewAccounts.Text = "选择新帐号";
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(550, 253);
            this.tableLayoutPanel1.TabIndex = 20;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSelectAll);
            this.panel1.Controls.Add(this.btnUnselectAll);
            this.panel1.Controls.Add(this.btnUnselectOne);
            this.panel1.Controls.Add(this.btnSelectOne);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(238, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(74, 247);
            this.panel1.TabIndex = 21;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(20, 91);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(40, 25);
            this.btnSelectAll.TabIndex = 14;
            this.btnSelectAll.Text = ">>";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnUnselectAll
            // 
            this.btnUnselectAll.Location = new System.Drawing.Point(20, 171);
            this.btnUnselectAll.Name = "btnUnselectAll";
            this.btnUnselectAll.Size = new System.Drawing.Size(40, 25);
            this.btnUnselectAll.TabIndex = 16;
            this.btnUnselectAll.Text = "<<";
            this.btnUnselectAll.UseVisualStyleBackColor = true;
            this.btnUnselectAll.Click += new System.EventHandler(this.btnUnselectAll_Click);
            // 
            // btnUnselectOne
            // 
            this.btnUnselectOne.Location = new System.Drawing.Point(20, 129);
            this.btnUnselectOne.Name = "btnUnselectOne";
            this.btnUnselectOne.Size = new System.Drawing.Size(40, 25);
            this.btnUnselectOne.TabIndex = 15;
            this.btnUnselectOne.Text = "<";
            this.btnUnselectOne.UseVisualStyleBackColor = true;
            this.btnUnselectOne.Click += new System.EventHandler(this.btnUnselectOne_Click);
            // 
            // btnSelectOne
            // 
            this.btnSelectOne.Location = new System.Drawing.Point(20, 53);
            this.btnSelectOne.Name = "btnSelectOne";
            this.btnSelectOne.Size = new System.Drawing.Size(40, 25);
            this.btnSelectOne.TabIndex = 13;
            this.btnSelectOne.Text = ">";
            this.btnSelectOne.UseVisualStyleBackColor = true;
            this.btnSelectOne.Click += new System.EventHandler(this.btnSelectOne_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.lblNewAccounts, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lstNewAccounts, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(318, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(229, 247);
            this.tableLayoutPanel2.TabIndex = 22;
            // 
            // lblNewAccounts
            // 
            this.lblNewAccounts.AutoSize = true;
            this.lblNewAccounts.ForeColor = System.Drawing.Color.Red;
            this.lblNewAccounts.Location = new System.Drawing.Point(3, 0);
            this.lblNewAccounts.Name = "lblNewAccounts";
            this.lblNewAccounts.Size = new System.Drawing.Size(47, 13);
            this.lblNewAccounts.TabIndex = 17;
            this.lblNewAccounts.Text = "*新账号";
            // 
            // lstNewAccounts
            // 
            this.lstNewAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstNewAccounts.FormattingEnabled = true;
            this.lstNewAccounts.Location = new System.Drawing.Point(3, 28);
            this.lstNewAccounts.Name = "lstNewAccounts";
            this.lstNewAccounts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstNewAccounts.Size = new System.Drawing.Size(223, 212);
            this.lstNewAccounts.TabIndex = 12;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.lblAllNewAccounts, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lstAllNewAccounts, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(229, 247);
            this.tableLayoutPanel3.TabIndex = 23;
            // 
            // lblAllNewAccounts
            // 
            this.lblAllNewAccounts.AutoSize = true;
            this.lblAllNewAccounts.ForeColor = System.Drawing.Color.Red;
            this.lblAllNewAccounts.Location = new System.Drawing.Point(3, 0);
            this.lblAllNewAccounts.Name = "lblAllNewAccounts";
            this.lblAllNewAccounts.Size = new System.Drawing.Size(59, 13);
            this.lblAllNewAccounts.TabIndex = 11;
            this.lblAllNewAccounts.Text = "*全部账号";
            // 
            // lstAllNewAccounts
            // 
            this.lstAllNewAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAllNewAccounts.FormattingEnabled = true;
            this.lstAllNewAccounts.Location = new System.Drawing.Point(3, 28);
            this.lstAllNewAccounts.Name = "lstAllNewAccounts";
            this.lstAllNewAccounts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstAllNewAccounts.Size = new System.Drawing.Size(223, 212);
            this.lstAllNewAccounts.TabIndex = 8;
            // 
            // grpOldAccounts
            // 
            this.grpOldAccounts.Controls.Add(this.tableLayoutPanel4);
            this.grpOldAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpOldAccounts.Location = new System.Drawing.Point(3, 311);
            this.grpOldAccounts.Name = "grpOldAccounts";
            this.grpOldAccounts.Size = new System.Drawing.Size(556, 272);
            this.grpOldAccounts.TabIndex = 33;
            this.grpOldAccounts.TabStop = false;
            this.grpOldAccounts.Text = "选择旧帐号";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel6, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(550, 253);
            this.tableLayoutPanel4.TabIndex = 20;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSelectAllOld);
            this.panel2.Controls.Add(this.btnUnselectAllOld);
            this.panel2.Controls.Add(this.btnUnselectOneOld);
            this.panel2.Controls.Add(this.btnSelectOneOld);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(238, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(74, 247);
            this.panel2.TabIndex = 21;
            // 
            // btnSelectAllOld
            // 
            this.btnSelectAllOld.Location = new System.Drawing.Point(20, 98);
            this.btnSelectAllOld.Name = "btnSelectAllOld";
            this.btnSelectAllOld.Size = new System.Drawing.Size(40, 25);
            this.btnSelectAllOld.TabIndex = 14;
            this.btnSelectAllOld.Text = ">>";
            this.btnSelectAllOld.UseVisualStyleBackColor = true;
            this.btnSelectAllOld.Click += new System.EventHandler(this.btnSelectAllOld_Click);
            // 
            // btnUnselectAllOld
            // 
            this.btnUnselectAllOld.Location = new System.Drawing.Point(20, 184);
            this.btnUnselectAllOld.Name = "btnUnselectAllOld";
            this.btnUnselectAllOld.Size = new System.Drawing.Size(40, 25);
            this.btnUnselectAllOld.TabIndex = 16;
            this.btnUnselectAllOld.Text = "<<";
            this.btnUnselectAllOld.UseVisualStyleBackColor = true;
            this.btnUnselectAllOld.Click += new System.EventHandler(this.btnUnselectAllOld_Click);
            // 
            // btnUnselectOneOld
            // 
            this.btnUnselectOneOld.Location = new System.Drawing.Point(20, 140);
            this.btnUnselectOneOld.Name = "btnUnselectOneOld";
            this.btnUnselectOneOld.Size = new System.Drawing.Size(40, 25);
            this.btnUnselectOneOld.TabIndex = 15;
            this.btnUnselectOneOld.Text = "<";
            this.btnUnselectOneOld.UseVisualStyleBackColor = true;
            this.btnUnselectOneOld.Click += new System.EventHandler(this.btnUnselectOneOld_Click);
            // 
            // btnSelectOneOld
            // 
            this.btnSelectOneOld.Location = new System.Drawing.Point(20, 57);
            this.btnSelectOneOld.Name = "btnSelectOneOld";
            this.btnSelectOneOld.Size = new System.Drawing.Size(40, 25);
            this.btnSelectOneOld.TabIndex = 13;
            this.btnSelectOneOld.Text = ">";
            this.btnSelectOneOld.UseVisualStyleBackColor = true;
            this.btnSelectOneOld.Click += new System.EventHandler(this.btnSelectOneOld_Click);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.lblOldAccounts, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.lstOldAccounts, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(318, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(229, 247);
            this.tableLayoutPanel5.TabIndex = 22;
            // 
            // lblOldAccounts
            // 
            this.lblOldAccounts.AutoSize = true;
            this.lblOldAccounts.ForeColor = System.Drawing.Color.Red;
            this.lblOldAccounts.Location = new System.Drawing.Point(3, 0);
            this.lblOldAccounts.Name = "lblOldAccounts";
            this.lblOldAccounts.Size = new System.Drawing.Size(47, 13);
            this.lblOldAccounts.TabIndex = 17;
            this.lblOldAccounts.Text = "*旧账号";
            // 
            // lstOldAccounts
            // 
            this.lstOldAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstOldAccounts.FormattingEnabled = true;
            this.lstOldAccounts.Location = new System.Drawing.Point(3, 28);
            this.lstOldAccounts.Name = "lstOldAccounts";
            this.lstOldAccounts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstOldAccounts.Size = new System.Drawing.Size(223, 212);
            this.lstOldAccounts.TabIndex = 12;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.lblAllOldAccounts, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.lstAllOldAccounts, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(229, 247);
            this.tableLayoutPanel6.TabIndex = 23;
            // 
            // lblAllOldAccounts
            // 
            this.lblAllOldAccounts.AutoSize = true;
            this.lblAllOldAccounts.ForeColor = System.Drawing.Color.Red;
            this.lblAllOldAccounts.Location = new System.Drawing.Point(3, 0);
            this.lblAllOldAccounts.Name = "lblAllOldAccounts";
            this.lblAllOldAccounts.Size = new System.Drawing.Size(59, 13);
            this.lblAllOldAccounts.TabIndex = 11;
            this.lblAllOldAccounts.Text = "*全部账号";
            // 
            // lstAllOldAccounts
            // 
            this.lstAllOldAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAllOldAccounts.FormattingEnabled = true;
            this.lstAllOldAccounts.Location = new System.Drawing.Point(3, 28);
            this.lstAllOldAccounts.Name = "lstAllOldAccounts";
            this.lstAllOldAccounts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstAllOldAccounts.Size = new System.Drawing.Size(223, 212);
            this.lstAllOldAccounts.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnRun);
            this.panel3.Controls.Add(this.btnStop);
            this.panel3.Controls.Add(this.grpDeleteAllMessage);
            this.panel3.Controls.Add(this.grpMode);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(565, 33);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(214, 272);
            this.panel3.TabIndex = 34;
            // 
            // tableLayoutPanelParent
            // 
            this.tableLayoutPanelParent.ColumnCount = 2;
            this.tableLayoutPanelParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanelParent.Controls.Add(this.panel3, 1, 1);
            this.tableLayoutPanelParent.Controls.Add(this.grpNewAccounts, 0, 1);
            this.tableLayoutPanelParent.Controls.Add(this.grpOldAccounts, 0, 2);
            this.tableLayoutPanelParent.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanelParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelParent.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelParent.Name = "tableLayoutPanelParent";
            this.tableLayoutPanelParent.RowCount = 3;
            this.tableLayoutPanelParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelParent.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelParent.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelParent.Size = new System.Drawing.Size(782, 556);
            this.tableLayoutPanelParent.TabIndex = 35;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblGroup);
            this.panel4.Controls.Add(this.cmbGroup);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(556, 24);
            this.panel4.TabIndex = 35;
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(8, 4);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(31, 13);
            this.lblGroup.TabIndex = 55;
            this.lblGroup.Text = "组：";
            // 
            // cmbGroup
            // 
            this.cmbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.Location = new System.Drawing.Point(55, 1);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(141, 21);
            this.cmbGroup.TabIndex = 54;
            this.cmbGroup.SelectedIndexChanged += new System.EventHandler(this.cmbGroup_SelectedIndexChanged);
            // 
            // FrmAddFriends
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(782, 556);
            this.Controls.Add(this.tableLayoutPanelParent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(790, 301);
            this.Name = "FrmAddFriends";
            this.TabText = "互加好友";
            this.Text = "互加好友";
            this.Load += new System.EventHandler(this.FrmAddFriends_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmAddFriends_FormClosed);
            this.grpMode.ResumeLayout(false);
            this.grpMode.PerformLayout();
            this.grpDeleteAllMessage.ResumeLayout(false);
            this.grpDeleteAllMessage.PerformLayout();
            this.grpNewAccounts.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.grpOldAccounts.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanelParent.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.CheckBox chkDeleteAllMessage;
        private System.Windows.Forms.GroupBox grpMode;
        private System.Windows.Forms.RadioButton rdbManual;
        private System.Windows.Forms.RadioButton rdbAuto;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.Label lblHelpMode;
        private System.Windows.Forms.GroupBox grpDeleteAllMessage;
        private System.Windows.Forms.Label lblHelpDeleteAllMessage;
        private System.Windows.Forms.GroupBox grpNewAccounts;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnUnselectAll;
        private System.Windows.Forms.Button btnUnselectOne;
        private System.Windows.Forms.Button btnSelectOne;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblNewAccounts;
        private System.Windows.Forms.ListBox lstNewAccounts;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ListBox lstAllNewAccounts;
        private System.Windows.Forms.GroupBox grpOldAccounts;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSelectAllOld;
        private System.Windows.Forms.Button btnUnselectAllOld;
        private System.Windows.Forms.Button btnUnselectOneOld;
        private System.Windows.Forms.Button btnSelectOneOld;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label lblOldAccounts;
        private System.Windows.Forms.ListBox lstOldAccounts;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label lblAllOldAccounts;
        private System.Windows.Forms.ListBox lstAllOldAccounts;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelParent;
        private System.Windows.Forms.CheckBox chkExecuteConfirmRequest;
        private System.Windows.Forms.CheckBox chkExecuteSendRequest;
        private System.Windows.Forms.Label lblAllNewAccounts;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.ComboBox cmbGroup;

    }
}