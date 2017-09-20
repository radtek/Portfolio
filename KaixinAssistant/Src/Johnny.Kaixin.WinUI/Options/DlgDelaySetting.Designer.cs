namespace Johnny.Kaixin.WinUI
{
    partial class DlgDelaySetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgDelaySetting));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpDelay = new System.Windows.Forms.GroupBox();
            this.lblHelpTryTimes = new System.Windows.Forms.Label();
            this.txtTryTimes = new System.Windows.Forms.TextBox();
            this.lblTryTimes = new System.Windows.Forms.Label();
            this.lblTimeOutSeconds = new System.Windows.Forms.Label();
            this.lblHelpTimeOut = new System.Windows.Forms.Label();
            this.txtTimeOut = new System.Windows.Forms.TextBox();
            this.lblTimeOut = new System.Windows.Forms.Label();
            this.lblDelayedTimeSeconds = new System.Windows.Forms.Label();
            this.lblHelpDelayedTime = new System.Windows.Forms.Label();
            this.txtDelayedTime = new System.Windows.Forms.TextBox();
            this.lblDelayedTime = new System.Windows.Forms.Label();
            this.btnDefault = new System.Windows.Forms.Button();
            this.grpDelay.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(57, 245);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(275, 245);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // grpDelay
            // 
            this.grpDelay.Controls.Add(this.lblHelpTryTimes);
            this.grpDelay.Controls.Add(this.txtTryTimes);
            this.grpDelay.Controls.Add(this.lblTryTimes);
            this.grpDelay.Controls.Add(this.lblTimeOutSeconds);
            this.grpDelay.Controls.Add(this.lblHelpTimeOut);
            this.grpDelay.Controls.Add(this.txtTimeOut);
            this.grpDelay.Controls.Add(this.lblTimeOut);
            this.grpDelay.Controls.Add(this.lblDelayedTimeSeconds);
            this.grpDelay.Controls.Add(this.lblHelpDelayedTime);
            this.grpDelay.Controls.Add(this.txtDelayedTime);
            this.grpDelay.Controls.Add(this.lblDelayedTime);
            this.grpDelay.Location = new System.Drawing.Point(12, 12);
            this.grpDelay.Name = "grpDelay";
            this.grpDelay.Size = new System.Drawing.Size(399, 209);
            this.grpDelay.TabIndex = 10;
            this.grpDelay.TabStop = false;
            this.grpDelay.Text = "延迟";
            // 
            // lblHelpTryTimes
            // 
            this.lblHelpTryTimes.AutoSize = true;
            this.lblHelpTryTimes.ForeColor = System.Drawing.Color.Red;
            this.lblHelpTryTimes.Location = new System.Drawing.Point(20, 175);
            this.lblHelpTryTimes.Name = "lblHelpTryTimes";
            this.lblHelpTryTimes.Size = new System.Drawing.Size(251, 12);
            this.lblHelpTryTimes.TabIndex = 20;
            this.lblHelpTryTimes.Text = "*每次访问时的尝试次数。至少为1，默认为3。";
            // 
            // txtTryTimes
            // 
            this.txtTryTimes.Location = new System.Drawing.Point(92, 146);
            this.txtTryTimes.Name = "txtTryTimes";
            this.txtTryTimes.Size = new System.Drawing.Size(73, 21);
            this.txtTryTimes.TabIndex = 3;
            // 
            // lblTryTimes
            // 
            this.lblTryTimes.AutoSize = true;
            this.lblTryTimes.Location = new System.Drawing.Point(20, 149);
            this.lblTryTimes.Name = "lblTryTimes";
            this.lblTryTimes.Size = new System.Drawing.Size(65, 12);
            this.lblTryTimes.TabIndex = 18;
            this.lblTryTimes.Text = "重试次数：";
            // 
            // lblTimeOutSeconds
            // 
            this.lblTimeOutSeconds.AutoSize = true;
            this.lblTimeOutSeconds.Location = new System.Drawing.Point(171, 88);
            this.lblTimeOutSeconds.Name = "lblTimeOutSeconds";
            this.lblTimeOutSeconds.Size = new System.Drawing.Size(167, 12);
            this.lblTimeOutSeconds.TabIndex = 17;
            this.lblTimeOutSeconds.Text = "秒（必须大于0，默认为30秒）";
            // 
            // lblHelpTimeOut
            // 
            this.lblHelpTimeOut.AutoSize = true;
            this.lblHelpTimeOut.ForeColor = System.Drawing.Color.Red;
            this.lblHelpTimeOut.Location = new System.Drawing.Point(20, 115);
            this.lblHelpTimeOut.Name = "lblHelpTimeOut";
            this.lblHelpTimeOut.Size = new System.Drawing.Size(359, 12);
            this.lblHelpTimeOut.TabIndex = 16;
            this.lblHelpTimeOut.Text = "*限定每次请求的超时时间。若等待的时间超过该值则定义为失败。";
            // 
            // txtTimeOut
            // 
            this.txtTimeOut.Location = new System.Drawing.Point(92, 86);
            this.txtTimeOut.Name = "txtTimeOut";
            this.txtTimeOut.Size = new System.Drawing.Size(73, 21);
            this.txtTimeOut.TabIndex = 2;
            // 
            // lblTimeOut
            // 
            this.lblTimeOut.AutoSize = true;
            this.lblTimeOut.Location = new System.Drawing.Point(20, 89);
            this.lblTimeOut.Name = "lblTimeOut";
            this.lblTimeOut.Size = new System.Drawing.Size(65, 12);
            this.lblTimeOut.TabIndex = 14;
            this.lblTimeOut.Text = "超时时间：";
            // 
            // lblDelayedTimeSeconds
            // 
            this.lblDelayedTimeSeconds.AutoSize = true;
            this.lblDelayedTimeSeconds.Location = new System.Drawing.Point(171, 20);
            this.lblDelayedTimeSeconds.Name = "lblDelayedTimeSeconds";
            this.lblDelayedTimeSeconds.Size = new System.Drawing.Size(137, 12);
            this.lblDelayedTimeSeconds.TabIndex = 13;
            this.lblDelayedTimeSeconds.Text = "秒（若为空表示不延迟）";
            // 
            // lblHelpDelayedTime
            // 
            this.lblHelpDelayedTime.ForeColor = System.Drawing.Color.Red;
            this.lblHelpDelayedTime.Location = new System.Drawing.Point(20, 47);
            this.lblHelpDelayedTime.Name = "lblHelpDelayedTime";
            this.lblHelpDelayedTime.Size = new System.Drawing.Size(348, 32);
            this.lblHelpDelayedTime.TabIndex = 12;
            this.lblHelpDelayedTime.Text = "*如果开心网服务器频繁拒绝访问，请加大此数值。建议设置为5秒以上，否则容易被判为使用外挂作弊。";
            // 
            // txtDelayedTime
            // 
            this.txtDelayedTime.Location = new System.Drawing.Point(92, 18);
            this.txtDelayedTime.Name = "txtDelayedTime";
            this.txtDelayedTime.Size = new System.Drawing.Size(73, 21);
            this.txtDelayedTime.TabIndex = 1;
            // 
            // lblDelayedTime
            // 
            this.lblDelayedTime.AutoSize = true;
            this.lblDelayedTime.Location = new System.Drawing.Point(20, 21);
            this.lblDelayedTime.Name = "lblDelayedTime";
            this.lblDelayedTime.Size = new System.Drawing.Size(65, 12);
            this.lblDelayedTime.TabIndex = 4;
            this.lblDelayedTime.Text = "延迟时间：";
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(165, 245);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(75, 23);
            this.btnDefault.TabIndex = 5;
            this.btnDefault.Text = "默认值";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // DlgDelaySetting
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(423, 292);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.grpDelay);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgDelaySetting";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "延迟设置";
            this.Load += new System.EventHandler(this.DlgDelaySetting_Load);
            this.grpDelay.ResumeLayout(false);
            this.grpDelay.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grpDelay;
        private System.Windows.Forms.TextBox txtDelayedTime;
        private System.Windows.Forms.Label lblDelayedTime;
        private System.Windows.Forms.Label lblHelpDelayedTime;
        private System.Windows.Forms.Label lblDelayedTimeSeconds;
        private System.Windows.Forms.Label lblHelpTryTimes;
        private System.Windows.Forms.TextBox txtTryTimes;
        private System.Windows.Forms.Label lblTryTimes;
        private System.Windows.Forms.Label lblTimeOutSeconds;
        private System.Windows.Forms.Label lblHelpTimeOut;
        private System.Windows.Forms.TextBox txtTimeOut;
        private System.Windows.Forms.Label lblTimeOut;
        private System.Windows.Forms.Button btnDefault;
    }
}