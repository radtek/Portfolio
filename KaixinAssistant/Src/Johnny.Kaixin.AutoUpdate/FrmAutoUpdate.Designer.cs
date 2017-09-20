namespace Johnny.Kaixin.AutoUpdate
{
    partial class FrmAutoUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAutoUpdate));
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblProcess = new System.Windows.Forms.Label();
            this.progressBarDownload = new System.Windows.Forms.ProgressBar();
            this.lblFile = new System.Windows.Forms.Label();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.lblDownloadSize = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblUpdateDate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(300, 98);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblProcess
            // 
            this.lblProcess.AutoSize = true;
            this.lblProcess.Location = new System.Drawing.Point(9, 39);
            this.lblProcess.Name = "lblProcess";
            this.lblProcess.Size = new System.Drawing.Size(78, 13);
            this.lblProcess.TabIndex = 1;
            this.lblProcess.Text = "目前完成:75%";
            // 
            // progressBarDownload
            // 
            this.progressBarDownload.Location = new System.Drawing.Point(9, 66);
            this.progressBarDownload.Name = "progressBarDownload";
            this.progressBarDownload.Size = new System.Drawing.Size(366, 25);
            this.progressBarDownload.TabIndex = 2;
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(9, 103);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(171, 13);
            this.lblFile.TabIndex = 3;
            this.lblFile.Text = "正在下载:开心助手V2.4.exe(1/8)";
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            // 
            // lblDownloadSize
            // 
            this.lblDownloadSize.AutoSize = true;
            this.lblDownloadSize.Location = new System.Drawing.Point(300, 39);
            this.lblDownloadSize.Name = "lblDownloadSize";
            this.lblDownloadSize.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblDownloadSize.Size = new System.Drawing.Size(80, 13);
            this.lblDownloadSize.TabIndex = 4;
            this.lblDownloadSize.Text = "324Kb/1076Kb";
            this.lblDownloadSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(9, 12);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(103, 13);
            this.lblVersion.TabIndex = 5;
            this.lblVersion.Text = "最新版本:2.4.1.330";
            // 
            // lblUpdateDate
            // 
            this.lblUpdateDate.AutoSize = true;
            this.lblUpdateDate.Location = new System.Drawing.Point(154, 12);
            this.lblUpdateDate.Name = "lblUpdateDate";
            this.lblUpdateDate.Size = new System.Drawing.Size(112, 13);
            this.lblUpdateDate.TabIndex = 6;
            this.lblUpdateDate.Text = "更新日期:2009-03-30";
            // 
            // FrmAutoUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(383, 129);
            this.Controls.Add(this.lblUpdateDate);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.progressBarDownload);
            this.Controls.Add(this.lblProcess);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblDownloadSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAutoUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "升级开心助手";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AutoUpdate_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAutoUpdate_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Label lblProcess;
        public System.Windows.Forms.ProgressBar progressBarDownload;
        private System.Windows.Forms.Label lblFile;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        public System.Windows.Forms.Label lblDownloadSize;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblUpdateDate;
    }
}

