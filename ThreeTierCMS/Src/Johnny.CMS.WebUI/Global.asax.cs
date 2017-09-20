using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Timers;
using OpenSmtp.Mail;

namespace Johnny.CMS.WebUI
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Error(object sender, EventArgs e)
        {
            //Exception objErr = Server.GetLastError().GetBaseException();
            //string error = "�����쳣ҳ: " + Request.Url.ToString() + "<br>";
            //error += "�쳣��Ϣ: " + objErr.Message + "<br>";
            //error += "��ϸ����: " + objErr.StackTrace + "<br>";
            //Server.ClearError();
            //Application["error"] = error;
            //Server.Transfer("~/ErrorPage/ErrorPage.aspx");

        }

        protected void Application_Start(object sender, EventArgs e)
        {
            ////����һ����ʱ������λ������ 
            //Timer aTimer = new Timer(1000);

            ////�� aTimer_Elapsed ָ��Ϊ��ʱ���� Elapsed �¼�������� 
            //aTimer.Elapsed += new ElapsedEventHandler(aTimer_Elapsed);

            ////AutoReset ����Ϊ true ʱ��ÿ��ָ��ʱ��ѭ��һ�Σ� 
            ////���Ϊ false����ִֻ��һ�Ρ� 
            //aTimer.AutoReset = true;
            //aTimer.Enabled = true; 

            ////�ȸ� Application("TimeStamp") ָ��һ����ֵ 
            //Application.Lock();
            //Application["TimeStamp"] = DateTime.Now.ToString();
            //Application.UnLock();

        }

        void aTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Application.Lock();
            Application["TimeStamp"] = DateTime.Now.ToString();
            Application.UnLock();

            if (DateTime.Now.Hour == 22 && DateTime.Now.Minute == 51 & DateTime.Now.Second == 0)
            {
                string ret = SendPassword("ajohn_2000zr@hotmail.com");
                if (ret != "OK")
                {
                    Response.Write("<script>");
                    Response.Write("alert('" + ret + "!!!');");
                    Response.Write("</script>");
                    return;
                }
            }
        }

        protected void Application_End(object sender, EventArgs e)
        {

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
                string subject = "����PowerSite������";
                string body = "��������PowerSite������ظ��ʼ���";

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
    }
}