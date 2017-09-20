using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using OpenSmtp.Mail;

namespace Johnny.CMS.Admin
{
    public partial class Admin_SendMail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = Application["TimeStamp"].ToString();

        }

        public static string SendPassword(string strEmail)
        {

            try
            {
                string smtpHost = "smtp.163.com";
                int smtpPort = 25;
                string senderEmail = "ajohn_2000zr@163.com";
                string senderName = "zhuangrong";
                string recipientEmail = strEmail;
                string subject = "您在PowerSite的密码";
                string body = "这是来自PowerSite的密码回复邮件。";

                SmtpConfig.VerifyAddresses = false;
                EmailAddress from = new EmailAddress(senderEmail, senderName);
                EmailAddress to = new EmailAddress(recipientEmail);
                MailMessage msg = new MailMessage(from, to);
                msg.Charset = "gb2312";
                msg.Subject = subject;
                msg.Body = body;

                Smtp smtp = new Smtp(smtpHost, smtpPort);
                smtp.Username = "ajohn_2000zr";
                smtp.Password = "001266";
                smtp.SendMail(msg);
                return "OK";

            }
            catch (MalformedAddressException mfa)
            {
                return mfa.Message.Substring(0, mfa.Message.Length - 4);
            }
            catch (SmtpException se)
            {
                return se.Message.Substring(0, se.Message.Length - 4);
            }
            catch (Exception ex)
            {
                return ex.Message.Substring(0, ex.Message.Length - 4);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string ret= SendPassword(TextBox1.Text);
            if (ret != "OK")
            {
                Response.Write("<script>");
                Response.Write("alert('" + ret + "!!!');");
                Response.Write("</script>");
                return;
            }

        }
    }
}
