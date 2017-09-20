using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;

//using Johnny.Library.Helper;

namespace Johnny.Kaixin.AutoUpdate
{
    public partial class FrmAutoUpdate : Form
    {
        private string _updatexml;
        private string _currentFile;
        private long _totalBytes;
        private long _totalDownloadedByte;
        private int _currentnum;
        private int _totalfilecount;
        private string _tempFolder;
        private UpdateInfo _updateOm;

        public FrmAutoUpdate()
        {
            InitializeComponent();
        }

        #region AutoUpdate_Load
        private void AutoUpdate_Load(object sender, EventArgs e)
        {
            try
            {
                lblProcess.Text = "";
                lblFile.Text = "";
                lblDownloadSize.Text = "";
                progressBarDownload.Value = 0;
                _updateOm = XmlUtility.GetUpdateInfo(_updatexml);
                //if (_updatexml != null)
                //    LogHelper.Write("FrmAutoUpdate.AutoUpdate_Load", _updatexml, LogSeverity.Info);
                //else
                //    LogHelper.Write("FrmAutoUpdate.AutoUpdate_Load", "_updatexml is null", LogSeverity.Info);
                if (_updateOm == null)
                {
                    MessageBox.Show("无法取得更新信息！", Program.MESSAGE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Quit();
                    return;
                }

                lblVersion.Text = "最新版本:" + _updateOm.Version;
                lblUpdateDate.Text = "更新日期:" + _updateOm.UpdateTime;
                backgroundWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.MESSAGE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //LogHelper.Write("FrmAutoUpdate.AutoUpdate_Load", ex, LogSeverity.Error);
                Quit();
            }
        }
        #endregion

        #region FrmAutoUpdate_FormClosing
        private void FrmAutoUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (backgroundWorker.IsBusy)
                {
                    if (MessageBox.Show("你确定中止升级吗？", "终止", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        backgroundWorker.CancelAsync();
                        Thread.Sleep(2000);
                        if (!String.IsNullOrEmpty(_tempFolder) && Directory.Exists(_tempFolder))
                        {
                            try
                            {
                                Directory.Delete(_tempFolder, true);
                            }
                            catch { }
                        }
                    }
                    else
                        e.Cancel = true;
                }
            }
            catch
            { 
            }
        }
        #endregion        

        #region btnCancel_Click
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion       

        #region backgroundWorker Event
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                if (worker != null)
                {                    
                    _totalfilecount = _updateOm.UpdateFileList.Length;
                    //create temp folder for downloading update files
                    _tempFolder = Application.StartupPath + "\\temp" + System.DateTime.Now.Date.ToString("yyyyMMdd") + "-" + System.DateTime.Now.TimeOfDay.TotalMilliseconds.ToString();
                    Directory.CreateDirectory(_tempFolder);

                    for (int ix = 0; ix < _updateOm.UpdateFileList.Length; ix++)
                    {
                        try
                        {
                            _currentnum = ix;
                            _currentFile = _updateOm.UpdateFileList[ix];
                            System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(_updateOm.UrlAddress + "/" + _updateOm.UpdateFileList[ix]);
                            System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                            _totalBytes = myrp.ContentLength;
                            System.IO.Stream st = myrp.GetResponseStream();
                            System.IO.Stream so = new System.IO.FileStream(Path.Combine(_tempFolder, _updateOm.UpdateFileList[ix]), System.IO.FileMode.Create);
                            _totalDownloadedByte = 0;
                            byte[] by = new byte[1024];
                            int osize = st.Read(by, 0, (int)by.Length);
                            while (osize > 0)
                            {
                                //Check for cancellation
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    break;
                                }
                                else
                                {
                                    worker.ReportProgress((int)(_totalDownloadedByte * 100 / _totalBytes + 1));
                                }
                                _totalDownloadedByte = osize + _totalDownloadedByte;
                                so.Write(by, 0, osize);
                                osize = st.Read(by, 0, (int)by.Length);
                            }
                            so.Close();
                            st.Close();
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Quit();
            }
        }
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                progressBarDownload.Value = e.ProgressPercentage;
                lblProcess.Text = "目前完成:" + (progressBarDownload.Value * 100 / progressBarDownload.Maximum) + "%";
                lblFile.Text = string.Format("正在下载:{0}({1}/{2})", _currentFile, _currentnum + 1, _totalfilecount);
                lblDownloadSize.Text = string.Format("{0}Kb/{1}Kb", _totalDownloadedByte / 1024, _totalBytes / 1024);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.MESSAGE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Quit();
            }
        }
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                this.TopMost = false;
                if (e.Cancelled)
                {
                    //MessageBox.Show("取消");
                }
                else if (e.Error != null)
                {
                    MessageBox.Show(e.Error.Message, Program.MESSAGE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Quit();
                }
                else
                {
                    if (!String.IsNullOrEmpty(_tempFolder) && Directory.Exists(_tempFolder))
                    {
                        try
                        {
                            //delete folders
                            for (int ix = 0; ix < _updateOm.DeletionDirectoryList.Length; ix++)
                            {
                                try
                                {
                                    if (!String.IsNullOrEmpty(_updateOm.DeletionDirectoryList[ix]))
                                    {
                                        if (Directory.Exists(Application.StartupPath + Path.DirectorySeparatorChar + _updateOm.DeletionDirectoryList[ix]))
                                            Directory.Delete(Application.StartupPath + Path.DirectorySeparatorChar + _updateOm.DeletionDirectoryList[ix], true);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    DialogResult dr = MessageBox.Show("试图删除文件夹" + _updateOm.DeletionDirectoryList[ix] + "时，发生异常：" + ex.Message, "开心助手自动升级", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);
                                    if (dr == DialogResult.Abort)
                                    {
                                        MessageBox.Show("升级失败！");
                                        Quit();
                                        return;
                                    }
                                    else if (dr == DialogResult.Retry)
                                    {
                                        ix--;
                                        continue;
                                    }
                                    else if (dr == DialogResult.Ignore)
                                        continue;
                                }
                            }
                            //delete files
                            for (int ix = 0; ix < _updateOm.DeletionFileList.Length; ix++)
                            {
                                try
                                {
                                    if (!String.IsNullOrEmpty(_updateOm.DeletionFileList[ix]))
                                    {
                                        if (File.Exists(Application.StartupPath + Path.DirectorySeparatorChar + _updateOm.DeletionFileList[ix]))
                                            File.Delete(Application.StartupPath + Path.DirectorySeparatorChar + _updateOm.DeletionFileList[ix]);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    DialogResult dr = MessageBox.Show("试图删除文件" + _updateOm.DeletionFileList[ix] + "时，发生异常：" + ex.Message, "开心助手自动升级", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);
                                    if (dr == DialogResult.Abort)
                                    {
                                        MessageBox.Show("升级失败！");
                                        Quit();
                                        return;
                                    }
                                    else if (dr == DialogResult.Retry)
                                    {
                                        ix--;
                                        continue;
                                    }
                                    else if (dr == DialogResult.Ignore)
                                        continue;
                                }
                            }
                            //copy files from temp folder to current folder.
                            try
                            {
                                copyDirectory(_tempFolder, Application.StartupPath, "");
                            }
                            catch (Exception ex)
                            {
                                string msg = string.Format("升级失败！\r\n试图从{0}\r\n复制文件到{1}时发生异常。\r\n错误：{2}", _tempFolder, Application.StartupPath, ex.Message);
                                MessageBox.Show(msg, Program.MESSAGE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Quit();
                                return;
                            }
                            Directory.Delete(_tempFolder, true);
                            MessageBox.Show("升级完成！");
                            this.Close();
                            try
                            {
                                System.Diagnostics.Process.Start(Path.Combine(Application.StartupPath, _updateOm.AppName));
                            }
                            catch { Quit(); }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, Program.MESSAGE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Quit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.MESSAGE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Quit();
            }
        }
        #endregion

        #region copyDirectory
        ///   <summary>   
        ///   Copy一个文件夹及其下面的所有文件   
        ///   </summary>   
        ///   <param   name="Src">源文件地址</param>   
        ///   <param   name="Dst">COPY到地址</param>   
        public static void copyDirectory(string Src, string Dst, string except)
        {
            String[] Files;
            if (Dst[Dst.Length - 1] != Path.DirectorySeparatorChar) Dst += Path.DirectorySeparatorChar;
            if (!Directory.Exists(Dst)) Directory.CreateDirectory(Dst);
            Files = Directory.GetFileSystemEntries(Src);
            foreach (string Element in Files)
            {
                if (Directory.Exists(Element))
                    copyDirectory(Element, Dst + Path.GetFileName(Element), except);
                else
                {
                    if (except.IndexOf(Path.GetFileName(Element)) == -1)
                    {
                        File.Copy(Element, Dst + Path.GetFileName(Element), true);
                        File.Delete(Element);
                    }
                }
            }
        }
        #endregion

        #region Properties
        public string UpdateXml
        {
            //E:\\IndependentSourceCode\\Auto Update\\AutoUpdate Test\\bin\\AutoUpdateTest.EXE|http://localhost/AutoUpdate/KaixinAssistant/|Johnny.Kaixin.Core.dll?log4net.config?开心助手V2.4.exe.config"
            set { _updatexml = value; }
        }
        #endregion

        #region Quit
        private void Quit()
        {
            this.Close();
            this.Dispose();
            Application.Exit();
        }
        #endregion

    }
}