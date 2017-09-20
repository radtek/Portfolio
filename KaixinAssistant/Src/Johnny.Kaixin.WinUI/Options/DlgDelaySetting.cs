using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Johnny.Kaixin.Helper;
using Johnny.Kaixin.Core;

namespace Johnny.Kaixin.WinUI
{
    public partial class DlgDelaySetting : Form
    {
        public DlgDelaySetting()
        {
            InitializeComponent();
        }

        private void DlgDelaySetting_Load(object sender, EventArgs e)
        {
            try
            {
                //load config info
                DelayInfo delay = ConfigCtrl.GetDelay();
                if (delay != null)
                {
                    txtDelayedTime.Text = delay.DelayedTime.ToString();
                    txtTimeOut.Text = delay.TimeOut.ToString();
                    txtTryTimes.Text = delay.TryTimes.ToString();
                }

                txtDelayedTime.Select();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgDelaySetting", ex);
                this.Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDelayedTime.Text != string.Empty && !DataValidation.IsNaturalNumber(txtDelayedTime.Text))
                {
                    MessageBox.Show("�ӳ�ʱ�����Ϊ������", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDelayedTime.Select();
                    return;
                }

                if (DataValidation.IsNullOrEmpty(txtTimeOut.Text) || !DataValidation.IsNaturalNumber(txtTimeOut.Text))
                {
                    MessageBox.Show("��ʱʱ�䲻��Ϊ���ұ�����������", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTimeOut.Select();
                    return;
                }

                if (DataValidation.IsNullOrEmpty(txtTryTimes.Text) || !DataValidation.IsNaturalNumber(txtTryTimes.Text) || DataConvert.GetInt32(txtTryTimes.Text) < 1)
                {
                    MessageBox.Show("���Դ�������Ϊ����1��������", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTryTimes.Select();
                    return;
                }

                DelayInfo delay = new DelayInfo();

                if (txtDelayedTime.Text != string.Empty)
                    delay.DelayedTime = DataConvert.GetInt32(txtDelayedTime.Text);
                else
                    delay.DelayedTime = null;

                delay.TimeOut = DataConvert.GetInt32(txtTimeOut.Text);
                delay.TryTimes = DataConvert.GetInt32(txtTryTimes.Text);

                if (!ConfigCtrl.SetDelay(delay))
                {
                    MessageBox.Show("����ʧ�ܣ�", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgDelaySetting", ex);
            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            txtDelayedTime.Text = "4";
            txtTimeOut.Text = "30";
            txtTryTimes.Text = "3";
        }
    }
}