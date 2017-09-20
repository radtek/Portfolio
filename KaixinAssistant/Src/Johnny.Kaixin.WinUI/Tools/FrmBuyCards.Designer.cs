namespace Johnny.Kaixin.WinUI
{
    partial class FrmBuyCards
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBuyCards));
            this.grpAccounts = new System.Windows.Forms.GroupBox();
            this.lblSelectedAccounts = new System.Windows.Forms.Label();
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.listBoxSelectorAccounts = new Johnny.Kaixin.Controls.ListBoxSelector.AccountListBoxSelector();
            this.grpCards = new System.Windows.Forms.GroupBox();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblCards = new System.Windows.Forms.Label();
            this.cmbCards = new System.Windows.Forms.ComboBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.grpAccounts.SuspendLayout();
            this.grpCards.SuspendLayout();
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
            this.listBoxSelectorAccounts.Size = new System.Drawing.Size(431, 210);
            this.listBoxSelectorAccounts.TabIndex = 0;
            // 
            // grpCards
            // 
            this.grpCards.Controls.Add(this.txtCount);
            this.grpCards.Controls.Add(this.lblCount);
            this.grpCards.Controls.Add(this.lblCards);
            this.grpCards.Controls.Add(this.cmbCards);
            this.grpCards.Location = new System.Drawing.Point(13, 294);
            this.grpCards.Name = "grpCards";
            this.grpCards.Size = new System.Drawing.Size(488, 133);
            this.grpCards.TabIndex = 1;
            this.grpCards.TabStop = false;
            this.grpCards.Text = "道具";
            // 
            // txtCount
            // 
            this.txtCount.Location = new System.Drawing.Point(88, 70);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(100, 21);
            this.txtCount.TabIndex = 3;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(27, 73);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(41, 12);
            this.lblCount.TabIndex = 2;
            this.lblCount.Text = "数量：";
            // 
            // lblCards
            // 
            this.lblCards.AutoSize = true;
            this.lblCards.Location = new System.Drawing.Point(27, 34);
            this.lblCards.Name = "lblCards";
            this.lblCards.Size = new System.Drawing.Size(41, 12);
            this.lblCards.TabIndex = 1;
            this.lblCards.Text = "道具：";
            // 
            // cmbCards
            // 
            this.cmbCards.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCards.FormattingEnabled = true;
            this.cmbCards.Location = new System.Drawing.Point(88, 31);
            this.cmbCards.Name = "cmbCards";
            this.cmbCards.Size = new System.Drawing.Size(152, 20);
            this.cmbCards.TabIndex = 0;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(558, 309);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 21);
            this.btnRun.TabIndex = 2;
            this.btnRun.Text = "运行";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(558, 366);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 21);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // FrmBuyCards
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(680, 454);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.grpCards);
            this.Controls.Add(this.grpAccounts);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmBuyCards";
            this.TabText = "购买道具";
            this.Text = "购买道具";
            this.Load += new System.EventHandler(this.FrmBuyCards_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmBuyCards_FormClosing);
            this.grpAccounts.ResumeLayout(false);
            this.grpAccounts.PerformLayout();
            this.grpCards.ResumeLayout(false);
            this.grpCards.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpAccounts;
        private Johnny.Kaixin.Controls.ListBoxSelector.AccountListBoxSelector listBoxSelectorAccounts;
        private System.Windows.Forms.GroupBox grpCards;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblCards;
        private System.Windows.Forms.ComboBox cmbCards;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.Label lblSelectedAccounts;
    }
}