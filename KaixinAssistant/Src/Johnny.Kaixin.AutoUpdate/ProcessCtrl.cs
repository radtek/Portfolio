using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Johnny.Kaixin.AutoUpdate
{

    public class ProcessCtrl

    {

        //by 闫磊 Email:Landgis@126.com,yanleigis@21cn.com 2007.10.30

        private int MaxNum;

        Form progressForm=null;

        ProgressBar progressBar1=null;

        bool Stop=false;

        Label label1;

        public bool ProgressStep(int step)

        {

            if (Stop)

            {

                this.Dispose();

                return true;

            }

            if (progressBar1.Value > progressBar1.Maximum)

            {

                this.Dispose();

                return true;

            }

            

            progressBar1.Value+= step;

            label1.Text = "目前完成:" + (progressBar1.Value * 100 / progressBar1.Maximum) + "%";

            Application.DoEvents();

            

            return false;

        }

        private void btn_Click(object sender, EventArgs e)

        {

            if (MessageBox.Show("你确定终止吗", "终止", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)

            Stop = false;

            else

            Stop = true;

        }

        public ProcessCtrl(int Max, String Caption, bool IsCancel)//最大值和标题

        {

            progressForm = new Form();

            progressForm.MinimizeBox = false;

            progressForm.MaximizeBox = false;

            progressForm.StartPosition = FormStartPosition.CenterScreen;

            progressForm.Width = 326+19;

            progressForm.Height = 96+19+20;

            progressForm.Text= Caption;

            progressForm.TopMost = true;//设置窗口在上边

            label1 = new Label();

            label1.Left = 9;

            label1.Top = 15;

            label1.Parent = progressForm;

            progressBar1 = new ProgressBar();

            progressBar1.Maximum = Max;

            MaxNum = Max;

            progressBar1.Left = 9;

            progressBar1.Top = 25+15;

            progressBar1.Width = 310;

            progressBar1.Parent = progressForm;

            progressBar1.Value = 0;

            

            if (IsCancel)

            {

                Button btnCancel = new Button();

                btnCancel.Text = "取消";

                btnCancel.Left = 240;

                btnCancel.Top = 54+20;

                btnCancel.Parent = progressForm;

                btnCancel.Click += new System.EventHandler(this.btn_Click);

                

            }

            progressForm.Show();

            

        }

        public void Dispose()

        {

            if (progressForm != null)

            {

                progressBar1.Dispose();

                progressForm.Dispose();

            }

        }

    }

}