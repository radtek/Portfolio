using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;

using Johnny.Kaixin.Helper;
using Johnny.Library.Helper;

namespace Johnny.Kaixin.WinUI
{
    internal class AutoUpdate
    {
        private const string REMOTE_URI = "http://updateassistant.googlecode.com/files/";
        private const string UPDATE_FILE = "update.xml";

        public delegate void NewVersionFoundEventHandler();
        public event NewVersionFoundEventHandler NewVersionFound;


        public void CheckVersionThread()
        {
            Thread threadMain = new Thread(new System.Threading.ThreadStart(CheckVersion));
            threadMain.IsBackground = true;
            threadMain.Start();
        }

        #region CheckVersion
        internal static bool CheckVersion()
        {
            bool ret = false;
            try
            {
                //the webclient 
                System.Net.WebClient myWebClient = new System.Net.WebClient();
                System.IO.Stream updatestream;
                try
                {
                    // Download the update info file to the memory, 
                    updatestream = myWebClient.OpenRead(REMOTE_URI + UPDATE_FILE);
                }
                catch(Exception ex)
                {
                    LogHelper.Write("Download update.xml", REMOTE_URI + UPDATE_FILE, ex, LogSeverity.Error);
                    return ret;
                }

                // read and close the stream 
                using (System.IO.StreamReader streamReader = new System.IO.StreamReader(updatestream, System.Text.Encoding.GetEncoding("GB2312")))
                {
                    string updateInfo = streamReader.ReadToEnd();
                    // if something was read 
                    if (!string.IsNullOrEmpty(updateInfo))
                    {
                        LogHelper.Write("Johnny.Kaixin.WinUI.AutoUpdate.CheckVersion.updateInfo:", updateInfo, LogSeverity.Info);
                        string newVersion = JsonHelper.GetMid(updateInfo, "<Version Num = \"", "\"/>");
                        if (String.IsNullOrEmpty(newVersion))
                            return ret;

                        if (CompareVersions(Assembly.GetExecutingAssembly().GetName().Version.ToString(), newVersion))
                        {
                            if (MessageBox.Show("检测到有新版本：" + newVersion + "\r\n是否退出进行升级？", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            {
                                // assembly the parameter to be passed to the auto 
                                // update program 

                                // Download the auto update program to the application 
                                // path, so you always have the last version runing                                 
                                myWebClient.DownloadFile(REMOTE_URI + "AutoUpdate.exe", Application.StartupPath + "\\AutoUpdate.exe");
                                // Call the auto update program with all the parameters 
                                System.Diagnostics.Process.Start(Application.StartupPath + "\\AutoUpdate.exe", System.Web.HttpUtility.UrlEncode(updateInfo));
                                // return true - auto update in progress 
                                ret = true;
                            }
                            else
                                ret = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // if there is an error return true, 
                // what means that the application 
                // should be closed 
                ret = true;
                // something went wrong... 
                Program.ShowMessageBox("AutoUpdate", ex);
            }
            return ret;
        }
        #endregion

        #region CompareVersions
        private static bool CompareVersions(string strA, string strB)
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
