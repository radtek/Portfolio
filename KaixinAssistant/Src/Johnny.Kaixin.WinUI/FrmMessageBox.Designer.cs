namespace Johnny.Kaixin.WinUI
{
    partial class FrmMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMessageBox));
            this.txtFullMessage = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.picError = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDetail = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picError)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFullMessage
            // 
            this.txtFullMessage.BackColor = System.Drawing.SystemColors.Control;
            this.txtFullMessage.Location = new System.Drawing.Point(6, 110);
            this.txtFullMessage.Multiline = true;
            this.txtFullMessage.Name = "txtFullMessage";
            this.txtFullMessage.ReadOnly = true;
            this.txtFullMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtFullMessage.Size = new System.Drawing.Size(400, 145);
            this.txtFullMessage.TabIndex = 0;
            this.txtFullMessage.WordWrap = false;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(82, 35);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(47, 12);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "Message";
            // 
            // picError
            // 
            this.picError.Image = ((System.Drawing.Image)(resources.GetObject("picError.Image")));
            this.picError.Location = new System.Drawing.Point(12, 11);
            this.picError.Name = "picError";
            this.picError.Size = new System.Drawing.Size(51, 58);
            this.picError.TabIndex = 2;
            this.picError.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(80, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "出错啦！";
            // 
            // btnDetail
            // 
            this.btnDetail.Location = new System.Drawing.Point(331, 75);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(75, 23);
            this.btnDetail.TabIndex = 4;
            this.btnDetail.Text = "详细信息";
            this.btnDetail.UseVisualStyleBackColor = true;
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(250, 75);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // FrmMessageBox
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(412, 261);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnDetail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picError);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.txtFullMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMessageBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "开心助手";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.picError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFullMessage;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.PictureBox picError;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDetail;
        private System.Windows.Forms.Button btnOK;
    }
}