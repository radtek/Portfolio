using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class SmtpInfo
    {
        private string _smtphost;
        private int _smtpport;
        private string _sendername;
        private string _senderemail;
        private string _username;
        private string _password;
        private string _receiveremail;
        private string _subject;
        private string _body;
        
        public SmtpInfo()
        {
            _smtpport = 25;
            _subject = "开心助手运行日志--{0}";
            _body = "";
        }

        public string SmtpHost
        {
            get { return _smtphost; }
            set { _smtphost = value; }
        }

        public int SmtpPort
        {
            get { return _smtpport; }
            set { _smtpport = value; }
        }

        public string SenderName
        {
            get { return _sendername; }
            set { _sendername = value; }
        }

        public string SenderEmail
        {
            get { return _senderemail; }
            set { _senderemail = value; }
        }

        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string ReceiverEmail
        {
            get { return _receiveremail; }
            set { _receiveremail = value; }
        }
        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }
    }
}
