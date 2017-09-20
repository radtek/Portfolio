using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;//BackgroundWorker所在的
using System.Windows.Forms;

namespace Johnny.Kaixin.AutoUpdate
{
    class ProcessBackground
    {
        private BackgroundWorker backgroundWorker = null;
        private int MaxNum;
        FrmAutoUpdate progressForm = null;

        public ProcessBackground(int Max, DoWorkEventHandler DoWork, ProgressChangedEventHandler worker_ProgressChanged)//最大值和标题
        {
            progressForm = new FrmAutoUpdate();
            progressForm.progressBarDownload.Value = 0;
            progressForm.Show();
            SetbackgroundWorker(DoWork, worker_ProgressChanged);
        }

        //把事件传进入
        private void SetbackgroundWorker(DoWorkEventHandler DoWork, ProgressChangedEventHandler worker_ProgressChanged)
        {           
            backgroundWorker.WorkerReportsProgress = true;//有进度条
            backgroundWorker.WorkerSupportsCancellation = true;//是否支持异步取消
            backgroundWorker.DoWork += new DoWorkEventHandler(DoWork);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);//做的事情
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(OnProgressChanged);//更新进度条
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);//完成事件
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OnProcessCompleted);
            backgroundWorker.RunWorkerAsync();
        }

        public void OnProcessCompleted(object sender, EventArgs e)
        {
            if (progressForm != null)
            {
                progressForm.Dispose();
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //return;
            /*if (e.Cancelled)
            {

                MessageBox.Show("取消");
            }
            else if (e.Error != null)
            {
                MessageBox.Show("错误");
            }
            else
            {
                MessageBox.Show("完成");
            }
            */
        }

        public void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressForm.progressBarDownload.Value = e.ProgressPercentage;
            progressForm.lblProcess.Text = "目前完成:" + (progressForm.progressBarDownload.Value * 100 / progressForm.progressBarDownload.Maximum) + "%";
        }
    }
}

