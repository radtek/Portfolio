using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace Johnny.Kaixin.Helper
{
    public class MailHelper
    {
        private static bool ssl = false;

        public static void SendMail(string smtpServer, int port, string mailFrom, string Password, string mailTo, string subject, string body)
        {
            SmtpClient mail = new SmtpClient(smtpServer, port);
            mail.UseDefaultCredentials = true;
            mail.Credentials = new System.Net.NetworkCredential(mailFrom, Password);
            mail.DeliveryMethod = SmtpDeliveryMethod.Network;
            mail.EnableSsl = ssl;

            MailMessage message = new MailMessage(mailFrom, mailTo, subject, body);
            message.SubjectEncoding = System.Text.Encoding.GetEncoding("gb2312");
            message.BodyEncoding = System.Text.Encoding.GetEncoding("gb2312");
            message.IsBodyHtml = false;

            mail.Send(message);
        }
    }
}
