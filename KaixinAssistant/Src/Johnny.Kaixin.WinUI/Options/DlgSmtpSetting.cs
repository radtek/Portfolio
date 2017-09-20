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
    public partial class DlgSmtpSetting : Form
    {
        public DlgSmtpSetting()
        {
            InitializeComponent();
        }

        private void DlgSmtpSetting_Load(object sender, EventArgs e)
        {
            try
            {
                //load config info
                SmtpInfo smtp = ConfigCtrl.GetSmtp();
                if (smtp != null)
                {
                    txtSmtpHost.Text = smtp.SmtpHost;
                    txtSmtpPort.Text = smtp.SmtpPort.ToString();
                    txtSenderName.Text = smtp.SenderName;
                    txtSenderEmail.Text = smtp.SenderEmail;
                    txtUserName.Text = smtp.UserName;
                    txtPassword.Text = smtp.Password;
                }

                txtSmtpHost.Select();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgSmtpSetting", ex);
                this.Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckData())
                    return;

                SmtpInfo smtp = new SmtpInfo();
                smtp.SmtpHost = txtSmtpHost.Text;
                smtp.SmtpPort = DataConvert.GetInt32(txtSmtpPort.Text);
                smtp.SenderName = txtSenderName.Text;
                smtp.SenderEmail = txtSenderEmail.Text;
                smtp.UserName = txtUserName.Text;
                smtp.Password = txtPassword.Text;

                if (!ConfigCtrl.SetSmtp(smtp))
                {
                    MessageBox.Show("保存失败！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgSmtpSetting", ex);
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckData())
                    return;
                MailHelper.SendMail(txtSmtpHost.Text, DataConvert.GetInt32(txtSmtpPort.Text), txtSenderEmail.Text, txtPassword.Text, txtSenderEmail.Text, "开心助手Smtp配置的测试邮件", "测试成功！");
                MessageBox.Show("测试成功！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (Exception ex)
            {
                MessageBox.Show("测试失败！错误：" + ex.Message, MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckData()
        {
            //if (DataValidation.IsNullOrEmpty(txtSmtpHost.Text))
            //{
            //    MessageBox.Show("Smtp服务器不能为空！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtSmtpHost.Select();
            //    return false;
            //}
            //if (DataValidation.IsNullOrEmpty(txtSmtpPort.Text) || !DataValidation.IsNaturalNumber(txtSmtpPort.Text))
            //{
            //    MessageBox.Show("Smtp端口不能为空，而且必须为整数！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtSmtpPort.Select();
            //    return false;
            //}

            //if (DataValidation.IsNullOrEmpty(txtSenderEmail.Text))
            //{
            //    MessageBox.Show("发送者Email不能为空！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtSenderEmail.Select();
            //    return false;
            //}

            //if (!DataValidation.IsEmail(txtSenderEmail.Text))
            //{
            //    MessageBox.Show("Email格式不正确！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtSenderEmail.Select();
            //    return false;
            //}

            //if (DataValidation.IsNullOrEmpty(txtUserName.Text))
            //{
            //    MessageBox.Show("用户名不能为空！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtUserName.Select();
            //    return false;
            //}

            //if (DataValidation.IsNullOrEmpty(txtPassword.Text))
            //{
            //    MessageBox.Show("密码不能为空！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtPassword.Select();
            //    return false;
            //}

            if (!DataValidation.IsNullOrEmpty(txtSmtpPort.Text) && !DataValidation.IsNaturalNumber(txtSmtpPort.Text))
            {
                MessageBox.Show("Smtp端口必须为整数！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSmtpPort.Select();
                return false;
            }

            return true;
        }

    }
}