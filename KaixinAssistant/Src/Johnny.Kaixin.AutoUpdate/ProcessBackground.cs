using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;//BackgroundWorker���ڵ�
using System.Windows.Forms;

namespace Johnny.Kaixin.AutoUpdate
{
    class ProcessBackground
    {
        private BackgroundWorker backgroundWorker = null;
        private int MaxNum;
        FrmAutoUpdate progressForm = null;

        public ProcessBackground(int Max, DoWorkEventHandler DoWork, ProgressChangedEventHandler worker_ProgressChanged)//���ֵ�ͱ���
        {
            progressForm = new FrmAutoUpdate();
            progressForm.progressBarDownload.Value = 0;
            progressForm.Show();
            SetbackgroundWorker(DoWork, worker_ProgressChanged);
        }

        //���¼�������
        private void SetbackgroundWorker(DoWorkEventHandler DoWork, ProgressChangedEventHandler worker_ProgressChanged)
        {           
            backgroundWorker.WorkerReportsProgress = true;//�н�����
            backgroundWorker.WorkerSupportsCancellation = true;//�Ƿ�֧���첽ȡ��
            backgroundWorker.DoWork += new DoWorkEventHandler(DoWork);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);//��������
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(OnProgressChanged);//���½�����
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);//����¼�
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

                MessageBox.Show("ȡ��");
            }
            else if (e.Error != null)
            {
                MessageBox.Show("����");
            }
            else
            {
                MessageBox.Show("���");
            }
            */
        }

        public void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressForm.progressBarDownload.Value = e.ProgressPercentage;
            progressForm.lblProcess.Text = "Ŀǰ���:" + (progressForm.progressBarDownload.Value * 100 / progressForm.progressBarDownload.Maximum) + "%";
        }
    }
}

