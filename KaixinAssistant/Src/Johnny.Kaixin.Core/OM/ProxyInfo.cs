using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class ProxyInfo
    {
        // Proxy
        private bool _enable;
        private string _server;
        private int? _port;
        private string _username;
        private string _password;        

        public ProxyInfo()
        { }

        public bool Enable
        {
            get { return _enable; }
            set { _enable = value; }
        }

        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }

        public int? Port
        {
            get { return _port; }
            set { _port = value; }
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
    }
}
