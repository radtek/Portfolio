namespace Johnny.Kaixin.WinUI
{
    partial class FrmUpgradeGarage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUpgradeGarage));
            this.grpAccounts = new System.Windows.Forms.GroupBox();
            this.lblSelectedAccounts = new System.Windows.Forms.Label();
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.listBoxSelectorAccounts = new Johnny.Kaixin.Controls.ListBoxSelector.AccountListBoxSelector();
            this.grpOperations = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdbExpensive = new System.Windows.Forms.RadioButton();
            this.rdbCheap = new System.Windows.Forms.RadioButton();
            this.txtMaxCars = new System.Windows.Forms.TextBox();
            this.lblAllAccountsMaxCars = new System.Windows.Forms.Label();
            this.cmbMaxCars = new System.Windows.Forms.ComboBox();
            this.lblMaxCars = new System.Windows.Forms.Label();
            this.chkBuyNewCars = new System.Windows.Forms.CheckBox();
            this.chkUpgradeFreeGarage = new System.Windows.Forms.CheckBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.grpCars = new System.Windows.Forms.GroupBox();
            this.lblBuyBlackList = new System.Windows.Forms.Label();
            this.lblCarsInMarket = new System.Windows.Forms.Label();
            this.listBoxSelectorCars = new Johnny.Kaixin.Controls.ListBoxSelector.CarListViewSelector();
            this.lblMode = new System.Windows.Forms.Label();
            this.grpAccounts.SuspendLayout();
            this.grpOperations.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpCars.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpAccounts
            // 
            this.grpAccounts.Controls.Add(this.lblSelectedAccounts);
            this.grpAccounts.Controls.Add(this.cmbGroup);
            this.grpAccounts.Controls.Add(this.listBoxSelectorAccounts);
            this.grpAccounts.Location = new System.Drawing.Point(13, 10);
            this.grpAccounts.Name = "grpAccounts";
            this.grpAccounts.Size = new System.Drawing.Size(488, 269);
            this.grpAccounts.TabIndex = 0;
            this.grpAccounts.TabStop = false;
            this.grpAccounts.Text = "账号";
            // 
            // lblSelectedAccounts
            // 
            this.lblSelectedAccounts.AutoSize = true;
            this.lblSelectedAccounts.ForeColor = System.Drawing.Color.Red;
            this.lblSelectedAccounts.Location = new System.Drawing.Point(281, 25);
            this.lblSelectedAccounts.Name = "lblSelectedAccounts";
            this.lblSelectedAccounts.Size = new System.Drawing.Size(95, 12);
            this.lblSelectedAccounts.TabIndex = 20;
            this.lblSelectedAccounts.Text = "*需要执行的账号";
            // 
            // cmbGroup
            // 
            this.cmbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.Location = new System.Drawing.Point(21, 18);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(153, 20);
            this.cmbGroup.TabIndex = 19;
            this.cmbGroup.SelectedIndexChanged += new System.EventHandler(this.cmbGroup_SelectedIndexChanged);
            // 
            // listBoxSelectorAccounts
            // 
            this.listBoxSelectorAccounts.AllItems = null;
            this.listBoxSelectorAccounts.Location = new System.Drawing.Point(21, 44);
            this.listBoxSelectorAccounts.Name = "listBoxSelectorAccounts";
            this.listBoxSelectorAccounts.Size = new System.Drawing.Size(431, 209);
            this.listBoxSelectorAccounts.TabIndex = 0;
            // 
            // grpOperations
            // 
            this.grpOperations.Controls.Add(this.lblMode);
            this.grpOperations.Controls.Add(this.panel1);
            this.grpOperations.Controls.Add(this.txtMaxCars);
            this.grpOperations.Controls.Add(this.lblAllAccountsMaxCars);
            this.grpOperations.Controls.Add(this.cmbMaxCars);
            this.grpOperations.Controls.Add(this.lblMaxCars);
            this.grpOperations.Controls.Add(this.chkBuyNewCars);
            this.grpOperations.Controls.Add(this.chkUpgradeFreeGarage);
            this.grpOperations.Location = new System.Drawing.Point(522, 29);
            this.grpOperations.Name = "grpOperations";
            this.grpOperations.Size = new System.Drawing.Size(283, 180);
            this.grpOperations.TabIndex = 1;
            this.grpOperations.TabStop = false;
            this.grpOperations.Text = "操作";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdbExpensive);
            this.panel1.Controls.Add(this.rdbCheap);
            this.panel1.Location = new System.Drawing.Point(110, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(145, 21);
            this.panel1.TabIndex = 31;
            // 
            // rdbExpensive
            // 
            this.rdbExpensive.AutoSize = true;
            this.rdbExpensive.Location = new System.Drawing.Point(80, 3);
            this.rdbExpensive.Name = "rdbExpensive";
            this.rdbExpensive.Size = new System.Drawing.Size(59, 16);
            this.rdbExpensive.TabIndex = 1;
            this.rdbExpensive.Text = "最贵的";
            this.rdbExpensive.UseVisualStyleBackColor = true;
            // 
            // rdbCheap
            // 
            this.rdbCheap.AutoSize = true;
            this.rdbCheap.Checked = true;
            this.rdbCheap.Location = new System.Drawing.Point(3, 3);
            this.rdbCheap.Name = "rdbCheap";
            this.rdbCheap.Size = new System.Drawing.Size(71, 16);
            this.rdbCheap.TabIndex = 0;
            this.rdbCheap.TabStop = true;
            this.rdbCheap.Text = "最便宜的";
            this.rdbCheap.UseVisualStyleBackColor = true;
            // 
            // txtMaxCars
            // 
            this.txtMaxCars.Location = new System.Drawing.Point(179, 121);
            this.txtMaxCars.Name = "txtMaxCars";
            this.txtMaxCars.Size = new System.Drawing.Size(74, 21);
            this.txtMaxCars.TabIndex = 30;
            // 
            // lblAllAccountsMaxCars
            // 
            this.lblAllAccountsMaxCars.AutoSize = true;
            this.lblAllAccountsMaxCars.Location = new System.Drawing.Point(45, 124);
            this.lblAllAccountsMaxCars.Name = "lblAllAccountsMaxCars";
            this.lblAllAccountsMaxCars.Size = new System.Drawing.Size(137, 12);
            this.lblAllAccountsMaxCars.TabIndex = 29;
            this.lblAllAccountsMaxCars.Text = "所有账号购买总数上限：";
            // 
            // cmbMaxCars
            // 
            this.cmbMaxCars.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMaxCars.FormattingEnabled = true;
            this.cmbMaxCars.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cmbMaxCars.Location = new System.Drawing.Point(167, 97);
            this.cmbMaxCars.Name = "cmbMaxCars";
            this.cmbMaxCars.Size = new System.Drawing.Size(52, 20);
            this.cmbMaxCars.TabIndex = 28;
            // 
            // lblMaxCars
            // 
            this.lblMaxCars.AutoSize = true;
            this.lblMaxCars.Location = new System.Drawing.Point(45, 100);
            this.lblMaxCars.Name = "lblMaxCars";
            this.lblMaxCars.Size = new System.Drawing.Size(125, 12);
            this.lblMaxCars.TabIndex = 27;
            this.lblMaxCars.Text = "单个账号汽车数上限：";
            // 
            // chkBuyNewCars
            // 
            this.chkBuyNewCars.AutoSize = true;
            this.chkBuyNewCars.Location = new System.Drawing.Point(22, 51);
            this.chkBuyNewCars.Name = "chkBuyNewCars";
            this.chkBuyNewCars.Size = new System.Drawing.Size(72, 16);
            this.chkBuyNewCars.TabIndex = 5;
            this.chkBuyNewCars.Text = "购买新车";
            this.chkBuyNewCars.UseVisualStyleBackColor = true;
            this.chkBuyNewCars.CheckedChanged += new System.EventHandler(this.chkBuyNewCars_CheckedChanged);
            // 
            // chkUpgradeFreeGarage
            // 
            this.chkUpgradeFreeGarage.AutoSize = true;
            this.chkUpgradeFreeGarage.Location = new System.Drawing.Point(22, 29);
            this.chkUpgradeFreeGarage.Name = "chkUpgradeFreeGarage";
            this.chkUpgradeFreeGarage.Size = new System.Drawing.Size(96, 16);
            this.chkUpgradeFreeGarage.TabIndex = 4;
            this.chkUpgradeFreeGarage.Text = "升级免费车位";
            this.chkUpgradeFreeGarage.UseVisualStyleBackColor = true;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(544, 215);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(109, 39);
            this.btnRun.TabIndex = 2;
            this.btnRun.Text = "运行";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(666, 215);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(109, 39);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // grpCars
            // 
            this.grpCars.Controls.Add(this.lblBuyBlackList);
            this.grpCars.Controls.Add(this.lblCarsInMarket);
            this.grpCars.Controls.Add(this.listBoxSelectorCars);
            this.grpCars.Location = new System.Drawing.Point(13, 295);
            this.grpCars.Name = "grpCars";
            this.grpCars.Size = new System.Drawing.Size(585, 281);
            this.grpCars.TabIndex = 4;
            this.grpCars.TabStop = false;
            this.grpCars.Text = "汽车";
            // 
            // lblBuyBlackList
            // 
            this.lblBuyBlackList.AutoSize = true;
            this.lblBuyBlackList.ForeColor = System.Drawing.Color.Red;
            this.lblBuyBlackList.Location = new System.Drawing.Point(321, 17);
            this.lblBuyBlackList.Name = "lblBuyBlackList";
            this.lblBuyBlackList.Size = new System.Drawing.Size(71, 12);
            this.lblBuyBlackList.TabIndex = 22;
            this.lblBuyBlackList.Text = "*购买黑名单";
            // 
            // lblCarsInMarket
            // 
            this.lblCarsInMarket.AutoSize = true;
            this.lblCarsInMarket.ForeColor = System.Drawing.Color.Red;
            this.lblCarsInMarket.Location = new System.Drawing.Point(6, 17);
            this.lblCarsInMarket.Name = "lblCarsInMarket";
            this.lblCarsInMarket.Size = new System.Drawing.Size(83, 12);
            this.lblCarsInMarket.TabIndex = 21;
            this.lblCarsInMarket.Text = "*市场上的汽车";
            // 
            // listBoxSelectorCars
            // 
            this.listBoxSelectorCars.AllItems = null;
            this.listBoxSelectorCars.Location = new System.Drawing.Point(6, 31);
            this.listBoxSelectorCars.Name = "listBoxSelectorCars";
            this.listBoxSelectorCars.Size = new System.Drawing.Size(570, 246);
            this.listBoxSelectorCars.TabIndex = 0;
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Location = new System.Drawing.Point(45, 75);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(65, 12);
            this.lblMode.TabIndex = 32;
            this.lblMode.Text = "购买方式：";
            // 
            // FrmUpgradeGarage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(872, 598);
            this.Controls.Add(this.grpCars);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.grpOperations);
            this.Controls.Add(this.grpAccounts);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmUpgradeGarage";
            this.TabText = "争车位工具";
            this.Text = "争车位工具";
            this.Load += new System.EventHandler(this.FrmBuyCards_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmBuyCards_FormClosing);
            this.grpAccounts.ResumeLayout(false);
            this.grpAccounts.PerformLayout();
            this.grpOperations.ResumeLayout(false);
            this.grpOperations.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpCars.ResumeLayout(false);
            this.grpCars.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpAccounts;
        private Johnny.Kaixin.Controls.ListBoxSelector.AccountListBoxSelector listBoxSelectorAccounts;
        private System.Windows.Forms.GroupBox grpOperations;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.Label lblSelectedAccounts;
        private System.Windows.Forms.CheckBox chkBuyNewCars;
        private System.Windows.Forms.CheckBox chkUpgradeFreeGarage;
        private System.Windows.Forms.GroupBox grpCars;
        private System.Windows.Forms.Label lblBuyBlackList;
        private System.Windows.Forms.Label lblCarsInMarket;
        private Johnny.Kaixin.Controls.ListBoxSelector.CarListViewSelector listBoxSelectorCars;
        private System.Windows.Forms.ComboBox cmbMaxCars;
        private System.Windows.Forms.Label lblMaxCars;
        private System.Windows.Forms.TextBox txtMaxCars;
        private System.Windows.Forms.Label lblAllAccountsMaxCars;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdbExpensive;
        private System.Windows.Forms.RadioButton rdbCheap;
        private System.Windows.Forms.Label lblMode;
    }
}