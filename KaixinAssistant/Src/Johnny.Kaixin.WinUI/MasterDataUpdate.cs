using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;
using System.Xml;
using System.Data;
using System.IO;

using Johnny.Kaixin.Core;
using Johnny.Kaixin.Helper;
using Johnny.Library.Helper;

namespace Johnny.Kaixin.WinUI
{
    internal class MasterDataUpdate
    {
        private const string REMOTE_URI = "http://updateassistant.googlecode.com/files/";
        private const string UPDATE_FILE = "masterdata.xml";
        WebClientHelper _webclientHelper;
        ProxyInfo _proxyinfo;

        internal delegate void NewVersionFoundEventHandler(string[] newfiles);
        internal event NewVersionFoundEventHandler NewVersionFound;

        internal delegate void LatestVersionConfirmedEventHandler();
        internal event LatestVersionConfirmedEventHandler LatestVersionConfirmed;

        public MasterDataUpdate()
        {
            _webclientHelper = new WebClientHelper();
        }

        #region CheckVersion
        internal void CheckVersion(bool IsStartUp, bool IsManually)
        {
            try
            {
                //load config info
                string folder = Path.Combine(Application.StartupPath, Constants.FOLDER_MASTERDATA);                
                if (!Directory.Exists(folder))
                    return;

                string configFile = "";
                string newVersion = "";
                string currentVersion = "";

                if (IsStartUp)
                    Thread.Sleep(7000);

                _proxyinfo = ConfigCtrl.GetProxy();
                _webclientHelper.SetProxy(_proxyinfo.Server, _proxyinfo.Port, _proxyinfo.UserName, _proxyinfo.Password);
                if (_proxyinfo.Enable)
                    _webclientHelper.EnableProxy();
                Stream updatestream = null;

                try
                {
                    // Download the update info file to the memory, 
                    updatestream = _webclientHelper.OpenRead(REMOTE_URI + UPDATE_FILE);
                }
                catch (Exception ex)
                {
                    LogHelper.Write("Download masterdata.xml", REMOTE_URI + UPDATE_FILE, ex, LogSeverity.Error);
                    return;
                }

                if (updatestream == null)
                {
                    LogHelper.Write("Download masterdata.xml", REMOTE_URI + UPDATE_FILE, LogSeverity.Error);
                    return;
                }

                // read and close the stream 
                using (System.IO.StreamReader streamReader = new System.IO.StreamReader(updatestream, System.Text.Encoding.GetEncoding("GB2312")))
                {
                    string updateInfo = streamReader.ReadToEnd();
                    // if something was read 
                    if (!string.IsNullOrEmpty(updateInfo))
                    {
                        XmlDocument objXmlDoc = new XmlDocument();
                        objXmlDoc.LoadXml(updateInfo);

                        DataView dv = GetData(objXmlDoc, "MasterData/UpdateFileList");

                        string[] arr = new string[dv.Table.Rows.Count];
                        for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                        {
                            //load config info
                            folder = Path.Combine(Application.StartupPath, Constants.FOLDER_MASTERDATA);
                            configFile = folder + Constants.CHAR_DOUBLEBACKSLASH + dv.Table.Rows[ix][0].ToString();
                            if (!File.Exists(configFile))
                            {
                                arr[ix] = dv.Table.Rows[ix][0].ToString();
                                continue;
                            }

                            newVersion = dv.Table.Rows[ix][1].ToString();
                            if (String.IsNullOrEmpty(newVersion))
                            {
                                LogHelper.Write("Get newVersion", dv.Table.Rows[ix][0].ToString(), LogSeverity.Warn);
                                continue;
                            }

                            currentVersion = GetCurrentVersion(configFile);
                            if (String.IsNullOrEmpty(currentVersion))
                            {
                                arr[ix] = dv.Table.Rows[ix][0].ToString();
                                continue;
                            }

                            if (CompareVersions(currentVersion, newVersion))
                            {
                                arr[ix] = dv.Table.Rows[ix][0].ToString();
                            }
                        }

                        bool needDownload = false;
                        for (int ix = 0; ix < arr.Length; ix++)
                        {
                            if (!String.IsNullOrEmpty(arr[ix]))
                            {
                                needDownload = true;
                                break;
                            }
                        }

                        if (needDownload)
                        {
                            if (NewVersionFound != null)
                                NewVersionFound(arr);
                        }
                        else
                        {
                            if (IsManually)
                            {
                                if (LatestVersionConfirmed != null)
                                    LatestVersionConfirmed();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Write("MasterDataUpdate.CheckVersion", ex, LogSeverity.Error);
            }
        }
        #endregion

        #region DownloadNewMasterDataFiles
        internal void DownloadNewMasterDataFiles(string[] files)
        {
            if (files == null || files.Length == 0)
                return;

            Stream updatestream = null;

            for (int ix = 0; ix < files.Length; ix++)
            {
                if (String.IsNullOrEmpty(files[ix]))
                    continue;
              
                try
                {
                    // Download the update info file to the memory, 
                    updatestream = _webclientHelper.OpenRead(REMOTE_URI + files[ix]);
                }
                catch (Exception ex)
                {
                    LogHelper.Write("Download masterdata.xml", REMOTE_URI + files[ix], ex, LogSeverity.Error);
                    continue;
                }

                if (updatestream == null)
                {
                    LogHelper.Write("Download masterdata.xml", REMOTE_URI + files[ix], LogSeverity.Error);
                    return;
                }

                try
                {
                    // read and close the stream 
                    using (StreamReader streamReader = new StreamReader(updatestream, System.Text.Encoding.UTF8))
                    {
                        SaveMasterDataFile(files[ix], streamReader.ReadToEnd());
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Write("SaveMasterDataFile", files[ix], ex, LogSeverity.Error);
                    continue;
                }
            }
        }
        #endregion

        #region GetCurrentVersion
        private string GetCurrentVersion(string filename)
        {
            try
            {
                StreamReader sr = new StreamReader(filename);
                string version = JsonHelper.GetMid(sr.ReadToEnd(), "<data Version=\"", "\" Key=\"");
                
                sr.Close();
                sr.Dispose();
                sr = null;

                return version;
            }
            catch 
            { 
                return ""; 
            }
        }
        #endregion

        #region SaveMasterDataFile
        private bool SaveMasterDataFile(string filename, string content)
        {
            try
            {
                string folder = Path.Combine(Application.StartupPath, Constants.FOLDER_MASTERDATA);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                string configFile = folder + Constants.CHAR_DOUBLEBACKSLASH + filename;

                StreamWriter sw = new StreamWriter(configFile);
                sw.Write(content);
                sw.Close();
                sw = null;
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Write("MasterDataUpdate.SaveMasterDataFile", filename, ex, LogSeverity.Error);
                return false;
            }
        }
        #endregion
        
        #region CompareVersions
        private static bool CompareVersions(string currentVersion, string newVersion)
        {
            try
            {
                string[] current = currentVersion.Split('-');
                DateTime currentDate = new DateTime(DataConvert.GetInt32(current[0]), DataConvert.GetInt32(current[1]), DataConvert.GetInt32(current[2]));
                string[] newly = newVersion.Split('-');
                DateTime newDate = new DateTime(DataConvert.GetInt32(newly[0]), DataConvert.GetInt32(newly[1]), DataConvert.GetInt32(newly[2]));
                return (newDate.CompareTo(currentDate) > 0) ? true : false;
            }
            catch
            {
                return true;
            }
        }
        #endregion

        #region Private Methods
        private static DataView GetData(XmlDocument xmldoc, string XmlPathNode)
        {
            //get data from xml file
            DataSet ds = new DataSet();
            DataView dv = new DataView();

            XmlNode node = xmldoc.SelectSingleNode(XmlPathNode);
            if (node == null)
                dv.Table = new DataTable("table0");
            else
            {
                StringReader read = new StringReader(node.OuterXml);

                ds.ReadXml(read);
                if (ds.Tables.Count < 1)
                    dv.Table = new DataTable("table0");
                else
                    dv = ds.Tables[0].DefaultView;
            }

            return dv;
        }
        #endregion
    }
}
