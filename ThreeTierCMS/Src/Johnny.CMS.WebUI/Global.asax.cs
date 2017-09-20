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
            //string error = "发生异常页: " + Request.Url.ToString() + "<br>";
            //error += "异常信息: " + objErr.Message + "<br>";
            //error += "详细描述: " + objErr.StackTrace + "<br>";
            //Server.ClearError();
            //Application["error"] = error;
            //Server.Transfer("~/ErrorPage/ErrorPage.aspx");

        }

        protected void Application_Start(object sender, EventArgs e)
        {
            ////创建一个计时器，单位：毫秒 
            //Timer aTimer = new Timer(1000);

            ////将 aTimer_Elapsed 指定为计时器的 Elapsed 事件处理程序 
            //aTimer.Elapsed += new ElapsedEventHandler(aTimer_Elapsed);

            ////AutoReset 属性为 true 时，每隔指定时间循环一次； 
            ////如果为 false，则只执行一次。 
            //aTimer.AutoReset = true;
            //aTimer.Enabled = true; 

            ////先给 Application("TimeStamp") 指定一个初值 
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
    }
}