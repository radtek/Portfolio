using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;
using System.Net.Cache;

namespace Johnny.Kaixin.Helper
{
    public class WebClientHelper
    {
        private WebProxy _proxy;

        //proxy
        private string _proxyserver;
        private int? _proxyport;
        private string _proxyusername;
        private string _proxypassword;

        public WebClientHelper()
        {
        }

        public Stream OpenRead(string url)
        {
            int switchtimes = 0;
            int tries = 3;
            while (tries-- > 0)
            {
                try
                {
                    WebClient myWebClient = new WebClient();
                    //不缓存任何访问资源
                    myWebClient.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
                    return myWebClient.OpenRead(url);
                }
                catch (ThreadAbortException ex)
                { }
                catch (ThreadInterruptedException ex)
                { }
                catch (Exception ex)
                {
                    if (tries == 0)
                    {
                        SwitchNetwork(ref switchtimes, ref tries);
                    }
                    continue;
                }
            }
            return null;
        }

        public bool DownloadFile(string url, string filename)
        {
            int switchtimes = 0;
            int tries = 3;
            while (tries-- > 0)
            {
                try
                {
                    WebClient myWebClient = new WebClient();
                    //不缓存任何访问资源
                    myWebClient.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
                    myWebClient.DownloadFile(url, filename);
                    return true;
                }
                catch (ThreadAbortException ex)
                { }
                catch (ThreadInterruptedException ex)
                { }
                catch (Exception ex)
                {
                    if (tries == 0)
                    {
                        SwitchNetwork(ref switchtimes, ref tries);
                    }
                    continue;
                }
            }
            return false;
        }

        #region SwitchNetwork
        private void SwitchNetwork(ref int switchtimes, ref int tries)
        {
            if (!NetworkHelper.IsConnected())
                return;

            switchtimes++;
            if (switchtimes >= 4)
                return;

            if (this._proxy == null)
            {
                Thread.Sleep(5000);
                this.EnableProxy();
            }
            else
            {
                Thread.Sleep(5000);
                this._proxy = null;
            }

            tries = 1;
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
            if (!string.IsNullOrEmpty(_proxyserver) && (_proxyport.HasValue) && (_proxyport.Value > 0))
            {
                this._proxy = new WebProxy(_proxyserver, _proxyport.Value);
                if (!string.IsNullOrEmpty(_proxyusername) && !string.IsNullOrEmpty(_proxypassword))
                {
                    this._proxy.Credentials = new NetworkCredential(_proxyusername, _proxypassword);
                    this._proxy.BypassProxyOnLocal = true;
                }
            }
        }
        #endregion
    }
}
