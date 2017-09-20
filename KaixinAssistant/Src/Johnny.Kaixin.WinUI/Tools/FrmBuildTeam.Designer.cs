namespace Johnny.Kaixin.WinUI
{
    partial class FrmBuildTeam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBuildTeam));
            this.btnCarsInMarket = new System.Windows.Forms.Button();
            this.btnCars = new System.Windows.Forms.Button();
            this.cmbAccount = new System.Windows.Forms.ComboBox();
            this.lblSender = new System.Windows.Forms.Label();
            this.lstViewCarsInMarket = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.grpCarInMarket = new System.Windows.Forms.GroupBox();
            this.grpMyCars = new System.Windows.Forms.GroupBox();
            this.lstViewMyCars = new System.Windows.Forms.ListView();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.grpValidCars = new System.Windows.Forms.GroupBox();
            this.lstViewValidCars = new System.Windows.Forms.ListView();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.btnBuildTeam = new System.Windows.Forms.Button();
            this.grpSettings = new System.Windows.Forms.GroupBox();
            this.lblWarning = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdbStop = new System.Windows.Forms.RadioButton();
            this.rdbCheap = new System.Windows.Forms.RadioButton();
            this.rdbExpensive = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbMaxCarCount = new System.Windows.Forms.ComboBox();
            this.lblMaxCarCount = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblCash = new System.Windows.Forms.Label();
            this.txtCash = new System.Windows.Forms.TextBox();
            this.lblCarPrice = new System.Windows.Forms.Label();
            this.txtCarPrice = new System.Windows.Forms.TextBox();
            this.lblSum = new System.Windows.Forms.Label();
            this.txtSum = new System.Windows.Forms.TextBox();
            this.lblAverage = new System.Windows.Forms.Label();
            this.txtAveragePrice = new System.Windows.Forms.TextBox();
            this.txtAllAsset = new System.Windows.Forms.TextBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.lblGroup = new System.Windows.Forms.Label();
            this.grpCarInMarket.SuspendLayout();
            this.grpMyCars.SuspendLayout();
            this.grpValidCars.SuspendLayout();
            this.grpSettings.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCarsInMarket
            // 
            this.btnCarsInMarket.Location = new System.Drawing.Point(7, 287);
            this.btnCarsInMarket.Name = "btnCarsInMarket";
            this.btnCarsInMarket.Size = new System.Drawing.Size(75, 23);
            this.btnCarsInMarket.TabIndex = 23;
            this.btnCarsInMarket.Text = "刷新";
            this.btnCarsInMarket.UseVisualStyleBackColor = true;
            this.btnCarsInMarket.Click += new System.EventHandler(this.btnCarsInMarket_Click);
            // 
            // btnCars
            // 
            this.btnCars.Location = new System.Drawing.Point(7, 287);
            this.btnCars.Name = "btnCars";
            this.btnCars.Size = new System.Drawing.Size(75, 23);
            this.btnCars.TabIndex = 30;
            this.btnCars.Text = "刷新";
            this.btnCars.UseVisualStyleBackColor = true;
            this.btnCars.Click += new System.EventHandler(this.btnCars_Click);
            // 
            // cmbAccount
            // 
            this.cmbAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccount.FormattingEnabled = true;
            this.cmbAccount.Location = new System.Drawing.Point(61, 40);
            this.cmbAccount.MaxDropDownItems = 16;
            this.cmbAccount.Name = "cmbAccount";
            this.cmbAccount.Size = new System.Drawing.Size(141, 21);
            this.cmbAccount.TabIndex = 35;
            // 
            // lblSender
            // 
            this.lblSender.AutoSize = true;
            this.lblSender.Location = new System.Drawing.Point(14, 43);
            this.lblSender.Name = "lblSender";
            this.lblSender.Size = new System.Drawing.Size(43, 13);
            this.lblSender.TabIndex = 34;
            this.lblSender.Text = "账号：";
            // 
            // lstViewCarsInMarket
            // 
            this.lstViewCarsInMarket.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lstViewCarsInMarket.FullRowSelect = true;
            this.lstViewCarsInMarket.Location = new System.Drawing.Point(7, 22);
            this.lstViewCarsInMarket.MultiSelect = false;
            this.lstViewCarsInMarket.Name = "lstViewCarsInMarket";
            this.lstViewCarsInMarket.Size = new System.Drawing.Size(245, 260);
            this.lstViewCarsInMarket.TabIndex = 36;
            this.lstViewCarsInMarket.UseCompatibleStateImageBehavior = false;
            this.lstViewCarsInMarket.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "No.";
            this.columnHeader1.Width = 35;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "汽车名";
            this.columnHeader2.Width = 110;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "价格";
            this.columnHeader3.Width = 70;
            // 
            // grpCarInMarket
            // 
            this.grpCarInMarket.Controls.Add(this.lstViewCarsInMarket);
            this.grpCarInMarket.Controls.Add(this.btnCarsInMarket);
            this.grpCarInMarket.Location = new System.Drawing.Point(12, 78);
            this.grpCarInMarket.Name = "grpCarInMarket";
            this.grpCarInMarket.Size = new System.Drawing.Size(260, 320);
            this.grpCarInMarket.TabIndex = 37;
            this.grpCarInMarket.TabStop = false;
            this.grpCarInMarket.Text = "市场上的汽车";
            // 
            // grpMyCars
            // 
            this.grpMyCars.Controls.Add(this.lstViewMyCars);
            this.grpMyCars.Controls.Add(this.btnCars);
            this.grpMyCars.Location = new System.Drawing.Point(278, 78);
            this.grpMyCars.Name = "grpMyCars";
            this.grpMyCars.Size = new System.Drawing.Size(260, 320);
            this.grpMyCars.TabIndex = 38;
            this.grpMyCars.TabStop = false;
            this.grpMyCars.Text = "我的汽车";
            // 
            // lstViewMyCars
            // 
            this.lstViewMyCars.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lstViewMyCars.FullRowSelect = true;
            this.lstViewMyCars.Location = new System.Drawing.Point(7, 22);
            this.lstViewMyCars.Name = "lstViewMyCars";
            this.lstViewMyCars.Size = new System.Drawing.Size(245, 260);
            this.lstViewMyCars.TabIndex = 36;
            this.lstViewMyCars.UseCompatibleStateImageBehavior = false;
            this.lstViewMyCars.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "No.";
            this.columnHeader4.Width = 35;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "汽车名";
            this.columnHeader5.Width = 110;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "价格";
            this.columnHeader6.Width = 70;
            // 
            // grpValidCars
            // 
            this.grpValidCars.Controls.Add(this.lstViewValidCars);
            this.grpValidCars.Location = new System.Drawing.Point(544, 78);
            this.grpValidCars.Name = "grpValidCars";
            this.grpValidCars.Size = new System.Drawing.Size(260, 320);
            this.grpValidCars.TabIndex = 39;
            this.grpValidCars.TabStop = false;
            this.grpValidCars.Text = "我能组建车队的车型";
            // 
            // lstViewValidCars
            // 
            this.lstViewValidCars.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.lstViewValidCars.FullRowSelect = true;
            this.lstViewValidCars.Location = new System.Drawing.Point(7, 22);
            this.lstViewValidCars.Name = "lstViewValidCars";
            this.lstViewValidCars.Size = new System.Drawing.Size(245, 260);
            this.lstViewValidCars.TabIndex = 36;
            this.lstViewValidCars.UseCompatibleStateImageBehavior = false;
            this.lstViewValidCars.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "No.";
            this.columnHeader7.Width = 35;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "汽车名";
            this.columnHeader8.Width = 110;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "价格";
            this.columnHeader9.Width = 70;
            // 
            // btnBuildTeam
            // 
            this.btnBuildTeam.Location = new System.Drawing.Point(567, 451);
            this.btnBuildTeam.Name = "btnBuildTeam";
            this.btnBuildTeam.Size = new System.Drawing.Size(75, 23);
            this.btnBuildTeam.TabIndex = 37;
            this.btnBuildTeam.Text = "组建";
            this.btnBuildTeam.UseVisualStyleBackColor = true;
            this.btnBuildTeam.Click += new System.EventHandler(this.btnBuildTeam_Click);
            // 
            // grpSettings
            // 
            this.grpSettings.Controls.Add(this.lblWarning);
            this.grpSettings.Controls.Add(this.panel1);
            this.grpSettings.Controls.Add(this.label1);
            this.grpSettings.Controls.Add(this.cmbMaxCarCount);
            this.grpSettings.Controls.Add(this.lblMaxCarCount);
            this.grpSettings.Location = new System.Drawing.Point(12, 404);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Size = new System.Drawing.Size(444, 108);
            this.grpSettings.TabIndex = 40;
            this.grpSettings.TabStop = false;
            this.grpSettings.Text = "设定";
            // 
            // lblWarning
            // 
            this.lblWarning.AllowDrop = true;
            this.lblWarning.ForeColor = System.Drawing.Color.Red;
            this.lblWarning.Location = new System.Drawing.Point(198, 18);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(220, 18);
            this.lblWarning.TabIndex = 43;
            this.lblWarning.Text = "* 当实际汽车数小于此值时购买新车";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdbStop);
            this.panel1.Controls.Add(this.rdbCheap);
            this.panel1.Controls.Add(this.rdbExpensive);
            this.panel1.Location = new System.Drawing.Point(23, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(340, 26);
            this.panel1.TabIndex = 30;
            // 
            // rdbStop
            // 
            this.rdbStop.AutoSize = true;
            this.rdbStop.Location = new System.Drawing.Point(250, 5);
            this.rdbStop.Name = "rdbStop";
            this.rdbStop.Size = new System.Drawing.Size(73, 17);
            this.rdbStop.TabIndex = 6;
            this.rdbStop.Text = "停止操作";
            this.rdbStop.UseVisualStyleBackColor = true;
            // 
            // rdbCheap
            // 
            this.rdbCheap.AutoSize = true;
            this.rdbCheap.Location = new System.Drawing.Point(118, 5);
            this.rdbCheap.Name = "rdbCheap";
            this.rdbCheap.Size = new System.Drawing.Size(121, 17);
            this.rdbCheap.TabIndex = 5;
            this.rdbCheap.Text = "换购最便宜的汽车";
            this.rdbCheap.UseVisualStyleBackColor = true;
            // 
            // rdbExpensive
            // 
            this.rdbExpensive.AutoSize = true;
            this.rdbExpensive.Checked = true;
            this.rdbExpensive.Location = new System.Drawing.Point(3, 5);
            this.rdbExpensive.Name = "rdbExpensive";
            this.rdbExpensive.Size = new System.Drawing.Size(109, 17);
            this.rdbExpensive.TabIndex = 4;
            this.rdbExpensive.TabStop = true;
            this.rdbExpensive.Text = "换购最贵的汽车";
            this.rdbExpensive.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "达到最大汽车数量时：";
            // 
            // cmbMaxCarCount
            // 
            this.cmbMaxCarCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMaxCarCount.FormattingEnabled = true;
            this.cmbMaxCarCount.Items.AddRange(new object[] {
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cmbMaxCarCount.Location = new System.Drawing.Point(101, 15);
            this.cmbMaxCarCount.Name = "cmbMaxCarCount";
            this.cmbMaxCarCount.Size = new System.Drawing.Size(77, 21);
            this.cmbMaxCarCount.TabIndex = 28;
            // 
            // lblMaxCarCount
            // 
            this.lblMaxCarCount.AutoSize = true;
            this.lblMaxCarCount.Location = new System.Drawing.Point(6, 18);
            this.lblMaxCarCount.Name = "lblMaxCarCount";
            this.lblMaxCarCount.Size = new System.Drawing.Size(91, 13);
            this.lblMaxCarCount.TabIndex = 27;
            this.lblMaxCarCount.Text = "最大汽车数量：";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(709, 451);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 37;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblCash
            // 
            this.lblCash.AutoSize = true;
            this.lblCash.Location = new System.Drawing.Point(283, 13);
            this.lblCash.Name = "lblCash";
            this.lblCash.Size = new System.Drawing.Size(43, 13);
            this.lblCash.TabIndex = 41;
            this.lblCash.Text = "现金：";
            // 
            // txtCash
            // 
            this.txtCash.Location = new System.Drawing.Point(330, 10);
            this.txtCash.Name = "txtCash";
            this.txtCash.ReadOnly = true;
            this.txtCash.Size = new System.Drawing.Size(100, 20);
            this.txtCash.TabIndex = 42;
            // 
            // lblCarPrice
            // 
            this.lblCarPrice.AutoSize = true;
            this.lblCarPrice.Location = new System.Drawing.Point(433, 13);
            this.lblCarPrice.Name = "lblCarPrice";
            this.lblCarPrice.Size = new System.Drawing.Size(52, 13);
            this.lblCarPrice.TabIndex = 43;
            this.lblCarPrice.Text = "+ 车价：";
            // 
            // txtCarPrice
            // 
            this.txtCarPrice.Location = new System.Drawing.Point(492, 10);
            this.txtCarPrice.Name = "txtCarPrice";
            this.txtCarPrice.ReadOnly = true;
            this.txtCarPrice.Size = new System.Drawing.Size(100, 20);
            this.txtCarPrice.TabIndex = 44;
            // 
            // lblSum
            // 
            this.lblSum.AutoSize = true;
            this.lblSum.Location = new System.Drawing.Point(598, 13);
            this.lblSum.Name = "lblSum";
            this.lblSum.Size = new System.Drawing.Size(64, 13);
            this.lblSum.TabIndex = 45;
            this.lblSum.Text = "= 总资产：";
            // 
            // txtSum
            // 
            this.txtSum.Location = new System.Drawing.Point(669, 10);
            this.txtSum.Name = "txtSum";
            this.txtSum.ReadOnly = true;
            this.txtSum.Size = new System.Drawing.Size(100, 20);
            this.txtSum.TabIndex = 46;
            // 
            // lblAverage
            // 
            this.lblAverage.AutoSize = true;
            this.lblAverage.Location = new System.Drawing.Point(283, 49);
            this.lblAverage.Name = "lblAverage";
            this.lblAverage.Size = new System.Drawing.Size(55, 13);
            this.lblAverage.TabIndex = 47;
            this.lblAverage.Text = "总资产：";
            // 
            // txtAveragePrice
            // 
            this.txtAveragePrice.Location = new System.Drawing.Point(551, 46);
            this.txtAveragePrice.Name = "txtAveragePrice";
            this.txtAveragePrice.ReadOnly = true;
            this.txtAveragePrice.Size = new System.Drawing.Size(100, 20);
            this.txtAveragePrice.TabIndex = 48;
            // 
            // txtAllAsset
            // 
            this.txtAllAsset.Location = new System.Drawing.Point(339, 46);
            this.txtAllAsset.Name = "txtAllAsset";
            this.txtAllAsset.ReadOnly = true;
            this.txtAllAsset.Size = new System.Drawing.Size(100, 20);
            this.txtAllAsset.TabIndex = 50;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(445, 49);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(93, 13);
            this.lblCount.TabIndex = 51;
            this.lblCount.Text = "/ 6 = 平均车价：";
            // 
            // cmbGroup
            // 
            this.cmbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.Location = new System.Drawing.Point(61, 10);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(141, 21);
            this.cmbGroup.TabIndex = 52;
            this.cmbGroup.SelectedIndexChanged += new System.EventHandler(this.cmbGroup_SelectedIndexChanged);
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(14, 13);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(31, 13);
            this.lblGroup.TabIndex = 53;
            this.lblGroup.Text = "组：";
            // 
            // FrmBuildTeam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(883, 544);
            this.Controls.Add(this.lblGroup);
            this.Controls.Add(this.cmbGroup);
            this.Controls.Add(this.btnBuildTeam);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.txtAllAsset);
            this.Controls.Add(this.txtAveragePrice);
            this.Controls.Add(this.lblAverage);
            this.Controls.Add(this.txtSum);
            this.Controls.Add(this.lblSum);
            this.Controls.Add(this.txtCarPrice);
            this.Controls.Add(this.lblCarPrice);
            this.Controls.Add(this.txtCash);
            this.Controls.Add(this.lblCash);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.grpSettings);
            this.Controls.Add(this.grpValidCars);
            this.Controls.Add(this.grpMyCars);
            this.Controls.Add(this.grpCarInMarket);
            this.Controls.Add(this.cmbAccount);
            this.Controls.Add(this.lblSender);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmBuildTeam";
            this.TabText = "组建车队";
            this.Text = "组建车队";
            this.Load += new System.EventHandler(this.FrmBuildTeam_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmBuildTeam_FormClosing);
            this.grpCarInMarket.ResumeLayout(false);
            this.grpMyCars.ResumeLayout(false);
            this.grpValidCars.ResumeLayout(false);
            this.grpSettings.ResumeLayout(false);
            this.grpSettings.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCars;
        private System.Windows.Forms.ComboBox cmbAccount;
        private System.Windows.Forms.Label lblSender;
        private System.Windows.Forms.Button btnCarsInMarket;
        private System.Windows.Forms.ListView lstViewCarsInMarket;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.GroupBox grpCarInMarket;
        private System.Windows.Forms.GroupBox grpMyCars;
        private System.Windows.Forms.ListView lstViewMyCars;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.GroupBox grpValidCars;
        private System.Windows.Forms.Button btnBuildTeam;
        private System.Windows.Forms.ListView lstViewValidCars;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.GroupBox grpSettings;
        private System.Windows.Forms.ComboBox cmbMaxCarCount;
        private System.Windows.Forms.Label lblMaxCarCount;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblCash;
        private System.Windows.Forms.TextBox txtCash;
        private System.Windows.Forms.Label lblCarPrice;
        private System.Windows.Forms.TextBox txtCarPrice;
        private System.Windows.Forms.Label lblSum;
        private System.Windows.Forms.TextBox txtSum;
        private System.Windows.Forms.Label lblAverage;
        private System.Windows.Forms.TextBox txtAveragePrice;
        private System.Windows.Forms.TextBox txtAllAsset;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdbCheap;
        private System.Windows.Forms.RadioButton rdbExpensive;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdbStop;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.Label lblGroup;
    }
}