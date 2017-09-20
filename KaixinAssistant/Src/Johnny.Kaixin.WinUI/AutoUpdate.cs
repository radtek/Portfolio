using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;
using System.IO;

using Johnny.Kaixin.Helper;
using Johnny.Library.Helper;
using Johnny.Kaixin.Core;

namespace Johnny.Kaixin.WinUI
{
    internal class AutoUpdate
    {
        private const string REMOTE_URI = "http://updateassistant.googlecode.com/files/";
        private const string UPDATE_FILE = "update.xml";

        internal delegate void NewVersionFoundEventHandler(string version, string exefile, string param);
        internal event NewVersionFoundEventHandler NewVersionFound;

        internal delegate void LatestVersionConfirmedEventHandler(string version);
        internal event LatestVersionConfirmedEventHandler LatestVersionConfirmed;

        public AutoUpdate()
        {
        }

        #region CheckVersion
        internal void CheckVersion(bool IsStartUp, bool IsManually)
        {
            try
            {
                if (IsStartUp)
                    Thread.Sleep(7000);

                WebClientHelper webclientHelper = new WebClientHelper();
                ProxyInfo proxyinfo = ConfigCtrl.GetProxy();
                webclientHelper.SetProxy(proxyinfo.Server, proxyinfo.Port, proxyinfo.UserName, proxyinfo.Password);
                if (proxyinfo.Enable)
                    webclientHelper.EnableProxy();
                Stream updatestream = null;

                try
                {
                    // Download the update info file to the memory, 
                    updatestream = webclientHelper.OpenRead(REMOTE_URI + UPDATE_FILE);
                }
                catch (Exception ex)
                {
                    LogHelper.Write("Download update.xml", REMOTE_URI + UPDATE_FILE, ex, LogSeverity.Error);
                    return;
                }

                if (updatestream == null)
                {
                    LogHelper.Write("Download update.xml", REMOTE_URI + UPDATE_FILE, LogSeverity.Error);
                    return;
                }

                // read and close the stream 
                using (System.IO.StreamReader streamReader = new System.IO.StreamReader(updatestream, System.Text.Encoding.GetEncoding("GB2312")))
                {
                    string updateInfo = streamReader.ReadToEnd();
                    // if something was read 
                    if (!string.IsNullOrEmpty(updateInfo))
                    {
                        //LogHelper.Write("Johnny.Kaixin.WinUI.AutoUpdate.CheckVersion.updateInfo:", updateInfo, LogSeverity.Info);
                        string newVersion = JsonHelper.GetMid(updateInfo, "<Version Num = \"", "\"/>");
                        if (String.IsNullOrEmpty(newVersion))
                        {
                            LogHelper.Write("Get Version", "newVersion is null", LogSeverity.Info);
                            return;
                        }

                        if (CompareVersions(Assembly.GetExecutingAssembly().GetName().Version.ToString(), newVersion))
                        {
                            // Download the auto update program to the application 
                            // path, so you always have the last version runing
                            if (webclientHelper.DownloadFile(REMOTE_URI + "AutoUpdate.exe", Application.StartupPath + "\\AutoUpdate.exe"))
                            {
                                if (NewVersionFound != null)
                                    NewVersionFound(newVersion, Application.StartupPath + "\\AutoUpdate.exe", System.Web.HttpUtility.UrlEncode(updateInfo));
                            }
                            else
                            {
                                LogHelper.Write("Download AutoUpdate.exe failed", REMOTE_URI + "AutoUpdate.exe", LogSeverity.Error);
                                return;
                            }
                        }
                        else if (IsManually)
                        {
                            if (LatestVersionConfirmed != null)
                                LatestVersionConfirmed(Assembly.GetExecutingAssembly().GetName().Version.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Write("AutoUpdate.CheckVersion", ex, LogSeverity.Error);
            }
        }
        #endregion

        #region CompareVersions
        public static bool CompareVersions(string strA, string strB)
        {
            Version vA = new Version(strA.Replace(",", "."));
            Version vB = new Version(strB.Replace(",", "."));

            if (vA.CompareTo(vB) >= 0)
                return false;
            else
                return true;
        }
        #endregion

        #region IsNewVersion
        private static bool IsNewVersion(string oldVersion, string newVersion)
        {
            try
            {
                //2.4.1.316 VS 2.4.1.330

                string[] strold = oldVersion.Split('.');
                string[] strnew = newVersion.Split('.');
                int[] iOld = new int[strold.Length];
                for (int ix = 0; ix < strold.Length; ix++)
                {
                    iOld[ix] = DataConvert.GetInt32(strold[ix]);
                }
                int[] iNew = new int[strnew.Length];
                for (int ix = 0; ix < strnew.Length; ix++)
                {
                    iNew[ix] = DataConvert.GetInt32(strnew[ix]);
                }

                for (int ix = 0; ix < 4; ix++)
                {
                    if (iNew[ix] > iOld[ix])
                        return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
