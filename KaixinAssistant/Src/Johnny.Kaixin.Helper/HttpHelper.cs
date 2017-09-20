using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;
using System.Threading;
using System.IO;
using System.Drawing;

namespace Johnny.Kaixin.Helper
{
    public class HttpHelper
    {
        // Fields
        private CookieContainer _cookie;
        private int _defaultdelay = 0;
        private int _delayedtime = 0;
        private int _timeout = 30;
        private int _trytimes = 3;
        private WebProxy _proxy;
        private string _message;

        //proxy
        private string _proxyserver;
        private int? _proxyport;
        private string _proxyusername;
        private string _proxypassword;

        public delegate void MessageChangedEventHandler(string message);
        public event MessageChangedEventHandler messageChanged;
     
        public HttpHelper()
        {
            this._cookie = new CookieContainer();
        }

        #region Get
        public string Get(string url)
        {
            return Get(url, "");
        }

        public string Get(string url, string referurl)
        {
            string query = "";
            Stream stream = null;
            return Get(url, referurl, ref query, ref stream);
        }

        public string Get(string url, ref string query)
        {
            Stream stream = null;
            return Get(url, "", ref query, ref stream);
        }

        public string Get(string url, ref Stream stream)
        {
            string query = "";
            return Get(url, "", ref query, ref stream);
        }

        public string Get(string url, string referurl, ref string query, ref Stream responseStream)
        {
            int switchtimes = 0;
            int tries = _trytimes;
            while (tries-- > 0)
            {
                try
                {
                    int delayseconds = Math.Max(this._delayedtime, this._defaultdelay);
                    if (delayseconds > 0)
                    {
                        Thread.Sleep((int)(delayseconds * 1000));
                    }
                    if (this._delayedtime != this._defaultdelay)
                        this._delayedtime = this._defaultdelay;
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
                    request.CookieContainer = this._cookie;
                    request.Method = "GET";
                    request.UserAgent = "mozilla/4.0 (compatible; msie 6.0; windows nt 5.2; sv1; maxthon; .net clr 1.1.4322; .net clr 2.0.50727; .net clr 3.0.04506.30; .net clr 3.0.04506.648; .net clr 3.5.21022; .net clr 3.0.4506.2152; .net clr 3.5.30729; ciba; infopath.2)";
                    //request.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/xaml+xml, application/vnd.ms-xpsdocument, application/x-ms-xbap, application/x-ms-application, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
                    if (!String.IsNullOrEmpty(referurl))
                    {
                        request.Referer = referurl;
                    }
                    if ((this._proxy != null) && (this._proxy.Credentials != null))
                    {
                        request.UseDefaultCredentials = true;
                    }
                    request.Proxy = this._proxy;
                    request.Timeout = this._timeout * 1000;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    if (response.ResponseUri != null)
                    {
                        query = response.ResponseUri.Query;
                    }
                    //if (url == "http://www.kaixin001.com/interface/regcreatepng.php?randnum=0.02962231445649477_1238760471986")
                    //{
                    //    string file = @"E:\FromInfosys\KaixinAssistantV2.4\Johnny.Kaixin.WinUI\bin\Release\images\" + System.DateTime.Now.Millisecond + ".png";
                    //    Bitmap image = new Bitmap(response.GetResponseStream());
                    //    image.Save(file);
                    //    return "aa";
                    //}
                    responseStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    if (_trytimes - tries > 1)
                    {
                        if (switchtimes > 0)
                            SetMessageLn("�ٴγ��Է���Get����ɹ���");
                        else
                            SetMessageLn("��" + (_trytimes - tries).ToString() + "�γ��Է���Get����ɹ���");
                    }
                    return reader.ReadToEnd();
                }
                catch (ThreadAbortException ex)
                { }
                catch (ThreadInterruptedException ex)
                { }
                catch (Exception ex)
                {
                    if (switchtimes > 0)
                        SetMessageLn("�ٴγ��Է���Get����ʧ�ܣ� ����" + ex.Message);
                    else
                        SetMessageLn("��" + (_trytimes - tries).ToString() + "�γ��Է���Get����ʧ�ܣ� ����" + ex.Message);

                    SetMessageLn("URL:" + url);

                    if (_trytimes - tries == 3)
                    {
                        SwitchNetwork(ref switchtimes, ref tries);
                    }
                    continue;
                }
            }
            return string.Empty;
        }
        #endregion

        #region GetImage
        public byte[] GetImage(string url, string referurl)
        {
            byte[] imageContent = null;
            int switchtimes = 0;
            int tries = _trytimes;
            while (tries-- > 0)
            {
                try
                {
                    int delayseconds = Math.Max(this._delayedtime, this._defaultdelay);
                    if (delayseconds > 0)
                    {
                        Thread.Sleep((int)(delayseconds * 1000));
                    }
                    if (this._delayedtime != this._defaultdelay)
                        this._delayedtime = this._defaultdelay;
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
                    request.CookieContainer = this._cookie;
                    request.Method = "GET";
                    request.UserAgent = "mozilla/4.0 (compatible; msie 6.0; windows nt 5.2; sv1; maxthon; .net clr 1.1.4322; .net clr 2.0.50727; .net clr 3.0.04506.30; .net clr 3.0.04506.648; .net clr 3.5.21022; .net clr 3.0.4506.2152; .net clr 3.5.30729; ciba; infopath.2)";
                    //request.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/xaml+xml, application/vnd.ms-xpsdocument, application/x-ms-xbap, application/x-ms-application, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
                    if (!String.IsNullOrEmpty(referurl))
                    {
                        request.Referer = referurl;
                    }
                    if ((this._proxy != null) && (this._proxy.Credentials != null))
                    {
                        request.UseDefaultCredentials = true;
                    }
                    request.Proxy = this._proxy;
                    request.Timeout = this._timeout * 1000;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    if (response.ContentType.Contains("image/"))
                    {
                       imageContent = ProcessImageByte(response);                        
                    }
                    return imageContent;
                }
                catch (ThreadAbortException ex)
                { }
                catch (ThreadInterruptedException ex)
                { }
                catch (Exception ex)
                {
                    if (switchtimes > 0)
                        SetMessageLn("�ٴγ��Է���Get����ʧ�ܣ� ����" + ex.Message);
                    else
                        SetMessageLn("��" + (_trytimes - tries).ToString() + "�γ��Է���Get����ʧ�ܣ� ����" + ex.Message);

                    SetMessageLn("URL:" + url);

                    if (_trytimes - tries == 3)
                    {
                        SwitchNetwork(ref switchtimes, ref tries);
                    }
                    continue;
                }
            }
            return imageContent;
        }
        #endregion

        #region Post
        public string Post(string url, string param)
        {
            return Post(url, "", param);
        }
        #endregion

        #region Post
        public string Post(string url, string referurl, string param)
        {
            string query = "";
            return Post(url, referurl, ref query, param);
        }
        #endregion

        #region Post
        public string Post(string url, ref string query, string param)
        {
            return Post(url, "", ref query, param);
        }
        #endregion

        #region Post
        public string Post(string url, string referurl, ref string query, string param)
        {
            int switchtimes = 0;
            int tries = _trytimes;
            while (tries-- > 0)
            {
                try
                {
                    int delayseconds = Math.Max(this._delayedtime, this._defaultdelay);
                    if (delayseconds > 0)
                    {
                        Thread.Sleep((int)(delayseconds * 1000));
                    }
                    if (this._delayedtime != this._defaultdelay)
                        this._delayedtime = this._defaultdelay;
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
                    request.CookieContainer = this._cookie;
                    byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(param);
                    request.Method = "POST";
                    request.UserAgent = "mozilla/4.0 (compatible; msie 6.0; windows nt 5.2; sv1; maxthon; .net clr 1.1.4322; .net clr 2.0.50727; .net clr 3.0.04506.30; .net clr 3.0.04506.648; .net clr 3.5.21022; .net clr 3.0.4506.2152; .net clr 3.5.30729; ciba; infopath.2)";
                    request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                    if (!String.IsNullOrEmpty(referurl))
                    {
                        request.Referer = referurl;
                    }
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = bytes.Length;
                    if ((this._proxy != null) && (this._proxy.Credentials != null))
                    {
                        request.UseDefaultCredentials = true;
                    }
                    request.Proxy = this._proxy;
                    request.Timeout = this._timeout * 1000;
                    //http://www.cnblogs.com/goody9807/archive/2008/06/11/1190370.html
                    request.ServicePoint.Expect100Continue = false; //

                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    if (response.ResponseUri != null)
                    {
                        query = response.ResponseUri.Query;
                    }
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    if (_trytimes - tries > 1)
                    {
                        if (switchtimes > 0)
                            SetMessageLn("�ٴγ��Է���Post����ɹ���");
                        else
                            SetMessageLn("��" + (_trytimes - tries).ToString() + "�γ��Է���Post����ɹ���");
                    }
                    return reader.ReadToEnd();
                }
                catch (ThreadAbortException ex)
                { }
                catch (ThreadInterruptedException ex)
                { }
                catch (Exception ex)
                {
                    if (switchtimes > 0)
                        SetMessageLn("�ٴγ��Է���Post����ʧ�ܣ� ����" + ex.Message);
                    else
                    {
                        SetMessageLn("��" + (_trytimes - tries).ToString() + "�γ��Է���Post����ʧ�ܣ� ����" + ex.Message);
                    }
                    SetMessageLn("URL:" + url);
                    SetMessage(" ����:" + param);

                    if (_trytimes - tries == 3)
                    {
                        SwitchNetwork(ref switchtimes, ref tries);
                    }
                    continue;
                }
            }
            return string.Empty;
        }
        #endregion

        #region SwitchNetwork
        private void SwitchNetwork(ref int switchtimes, ref int tries)
        {
            if (!NetworkHelper.IsConnected())
                SetMessageLn("��ĵ��Ի�û�����ӵ�Internet�����ȼ�����������Ƿ�������");
            else if (this._proxy != null)
                SetMessageLn("��⵽��Ϊ�������������˴���������������������Ƿ���ȷ��");

            //if (NetworkHelper.IsMSNetwork())
            //{
                switchtimes++;
                if (switchtimes >= 4)
                {
                    SetMessageLn("�Ѿ������л����������3�Σ�������");
                    return;
                }
                //if (NetworkHelper.IsMSProxyEnabled())
                if (this._proxy == null)
                {
                    SetMessageLn("��" + switchtimes.ToString()+ "����[ʹ��]�趨�Ĵ�����������ʣ�5���Ӻ��ط�����...");
                    Thread.Sleep(5000);
                    this.EnableProxy();
                }
                else
                {
                    SetMessageLn("��" + switchtimes.ToString()+ "����[ȡ��]�趨�Ĵ�����������ʣ�5���Ӻ��ط�����...");
                    Thread.Sleep(5000);
                    this._proxy = null;
                }

                tries = 1;
            //}
        }
        #endregion

        #region SetDelay
        public void SetDelay(int? delayedtime, int? timeout, int? trytimes)
        {
            if (delayedtime.HasValue)
            {
                this._defaultdelay = delayedtime.Value;
                this._delayedtime = delayedtime.Value;
            }
            if (timeout.HasValue)
                this._timeout = timeout.Value;
            if (trytimes.HasValue)
                this._trytimes = trytimes.Value;
        }
        #endregion

        #region SetProxy
        public void SetProxy(string server, int? port, string username, string password)
        {
            _proxyserver = server;
            _proxyport = port;
            _proxyusername = username;
            _proxypassword = password;
        }

        public void EnableProxy()
        {
            if ((_proxyserver != null) && (_proxyserver != string.Empty) && (_proxyport.HasValue) && (_proxyport.Value > 0))
            {
                this._proxy = new WebProxy(_proxyserver, _proxyport.Value);
                if ((_proxyusername != null) && (_proxypassword != null))
                {
                    this._proxy.Credentials = new NetworkCredential(_proxyusername, _proxypassword);
                    this._proxy.BypassProxyOnLocal = true;
                }
            }
        }
        #endregion

        #region SetMessageLn
        protected void SetMessageLn(string msg)
        {
            Message = TraceLog.SetMessageLn("[���������]", msg);
        }
        protected void SetMessage(string msg)
        {
            Message = TraceLog.SetMessage(msg);
        }
        #endregion

        private byte[] ProcessImageByte(HttpWebResponse resp)
        {
            Int64 iContentLength = resp.ContentLength;
            byte[] streamContent;
            MemoryStream memStream = new MemoryStream();
            const int BUFFER_SIZE = 4096;
            int iRead = 0;
            int idx = 0;
            Int64 iSize = 0;
            memStream.SetLength(BUFFER_SIZE);
            try
            {
                using (memStream)
                {
                    while (true)
                    {
                        iRead = 0;
                        byte[] respBuffer = new byte[BUFFER_SIZE];
                        iRead = resp.GetResponseStream().Read(respBuffer, 0, BUFFER_SIZE);
                        if (iRead == 0)
                        { break; }
                        iSize += iRead;
                        memStream.SetLength(iSize);
                        memStream.Write(respBuffer, 0, iRead);
                        idx += iRead;
                    }
                    streamContent = memStream.ToArray();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return streamContent;
        }

        private Stream ProcessImageStream(HttpWebResponse resp)
        {
            Int64 iContentLength = resp.ContentLength;
            MemoryStream memStream = new MemoryStream();
            const int BUFFER_SIZE = 4096;
            int iRead = 0;
            int idx = 0;
            Int64 iSize = 0;
            memStream.SetLength(BUFFER_SIZE);
            try
            {
                //using (memStream)
                //{
                    while (true)
                    {
                        iRead = 0;
                        byte[] respBuffer = new byte[BUFFER_SIZE];
                        iRead = resp.GetResponseStream().Read(respBuffer, 0, BUFFER_SIZE);
                        if (iRead == 0)
                        { break; }
                        iSize += iRead;
                        memStream.SetLength(iSize);
                        memStream.Write(respBuffer, 0, iRead);
                        idx += iRead;
                    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return memStream;
        }

        #region Properties
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                if (messageChanged != null)
                    messageChanged(_message);
            }
        }
        public int DelayedTime
        {
            get { return _delayedtime; }
            set { _delayedtime = value; }
        }        
        
        #endregion
    }
}
