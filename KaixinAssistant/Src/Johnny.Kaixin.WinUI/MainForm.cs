using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Collections.ObjectModel;
using System.Resources;
using System.Xml;
using System.Data;
using System.Text;
using System.Runtime.InteropServices;     

using WeifenLuo.WinFormsUI.Docking;
using Johnny.Kaixin.WinUI.Customization;
using Johnny.Kaixin.Core;
using Johnny.Kaixin.Helper;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using Johnny.Kaixin.WinUI.Utility;

namespace Johnny.Kaixin.WinUI
{
    public partial class MainForm : Form
    {
        #region Variable
        private bool _bSaveLayout = true;
		private DeserializeDockContent _deserializeDockContent;
        private FrmTaskManager _taskManagerForm = new FrmTaskManager();
        private FrmProperty _propertyForm = new FrmProperty();
		private FrmToolbox _toolboxForm = new FrmToolbox();
        private FrmOutput _outputForm = new FrmOutput();
		private DummyTaskList _taskListForm = new DummyTaskList();
        private FrmAccountManager _accountManagerForm = new FrmAccountManager();
        private FindAndReplaceForm _findForm = new FindAndReplaceForm();
        private Dictionary<string, Thread> _threadList;
        private Dictionary<string, TaskManager> _taskManagerList;
        private TextEditorControl _editor;
        private ITextEditorProperties _editorSettings;
        private FrmDocument _docform;
        private bool _hasRemindDialog;
        #endregion

        #region Ctor
        public MainForm()
        {
            InitializeComponent();
			_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
            _threadList = new Dictionary<string, Thread>();
            _taskManagerList = new Dictionary<string, TaskManager>();
            _hasRemindDialog = false;
        }
        #endregion

        #region 自动更新
        private System.Windows.Forms.Timer autoUpdateTimer;
        private void StartAutoUpdate()
        {
            autoUpdateTimer = new System.Windows.Forms.Timer();
            autoUpdateTimer.Enabled = true;
            autoUpdateTimer.Interval = 7200000; //2 hours
            autoUpdateTimer.Tick += new EventHandler(autoUpdateTimer_Tick);
            autoUpdateTimer.Start();
        }

        private void autoUpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!_hasRemindDialog)
                    CheckAndUpdateVersion(false, false);
            }
            catch
            {
                //Program.ShowMessageBox("MainForm", ex);
            }
        }
        private void CheckAndUpdateVersion(bool IsStartUp, bool IsManually)
        {
            Thread updateThread = new Thread(new System.Threading.ThreadStart(delegate
            {
                AutoUpdate autoUpdate = new AutoUpdate();
                autoUpdate.LatestVersionConfirmed += new AutoUpdate.LatestVersionConfirmedEventHandler(autoUpdate_LatestVersionConfirmed);
                autoUpdate.NewVersionFound += new AutoUpdate.NewVersionFoundEventHandler(autoUpdate_NewVersionFound);
                autoUpdate.CheckVersion(IsStartUp, IsManually);
            }));
            updateThread.IsBackground = true;            
            updateThread.Start();
        }

        private void autoUpdate_LatestVersionConfirmed(string version)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new AutoUpdate.LatestVersionConfirmedEventHandler(autoUpdate_LatestVersionConfirmed), new object[] { version });
                }
                else
                {
                    _hasRemindDialog = true;
                    MessageBox.Show(this, "当前版本V" + version + "，已经是最新版本了，无需升级！", "检查版本", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    _hasRemindDialog = false;
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void autoUpdate_NewVersionFound(string version, string exefile, string param)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new AutoUpdate.NewVersionFoundEventHandler(autoUpdate_NewVersionFound), new object[] { version, exefile, param });
                }
                else
                {
                    _hasRemindDialog = true;
                    if (MessageBox.Show(this, "检测到有新版本：" + version + "\r\n是否退出进行升级？", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        // Call the auto update program with the parameter
                        System.Diagnostics.Process.Start(exefile, param);
                        this.Close();
                        this.Dispose();
                        Application.Exit();
                    }
                    _hasRemindDialog = false;
                }
            }
            catch
            {
                //Program.ShowMessageBox("MainForm", ex);
            }
        }    
        #endregion

        #region MasterData更新
        private System.Windows.Forms.Timer checkMasterDataTimer;
        private void StarMasterDataUpdate()
        {
            checkMasterDataTimer = new System.Windows.Forms.Timer();
            checkMasterDataTimer.Enabled = true;
            checkMasterDataTimer.Interval = 7200000; //2 hours
            checkMasterDataTimer.Tick +=new EventHandler(checkMasterDataTimer_Tick);
            checkMasterDataTimer.Start();
        }

        private void checkMasterDataTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!_hasRemindDialog)
                    CheckAndDownloadMasterData(false, false);
            }
            catch
            {
                //Program.ShowMessageBox("MainForm", ex);
            }
        }
        private void CheckAndDownloadMasterData(bool IsStartUp, bool IsManually)
        {
            Thread updateThread = new Thread(new System.Threading.ThreadStart(delegate
            {
                MasterDataUpdate masterUpdate = new MasterDataUpdate();
                masterUpdate.LatestVersionConfirmed += new MasterDataUpdate.LatestVersionConfirmedEventHandler(masterUpdate_LatestVersionConfirmed);
                masterUpdate.NewVersionFound += new MasterDataUpdate.NewVersionFoundEventHandler(masterUpdate_NewVersionFound);
                masterUpdate.CheckVersion(IsStartUp, IsManually);
            }));
            updateThread.IsBackground = true;
            updateThread.Start();
        }

        private void masterUpdate_LatestVersionConfirmed()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MasterDataUpdate.LatestVersionConfirmedEventHandler(masterUpdate_LatestVersionConfirmed), new object[] { });
                }
                else
                {
                    _hasRemindDialog = true;

                    DataView dv = DynamicCtrl.GetOpenFileMenuConfigFile();
                    if (dv == null)
                        return;

                    StringBuilder sb = new StringBuilder();
                    for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                    {
                        sb.Append("\r\n");
                        //sb.Append("{");
                        sb.Append(DataConvert.GetString(dv.Table.Rows[ix]["Caption"]));
                        //sb.Append("}");
                        if (ix < dv.Table.Rows.Count - 1)
                            sb.Append("，");
                    }
                    MessageBox.Show(this, "在你本机的" + sb.ToString() + "\r\n都已经是最新的了，无需更新！", "MasterData更新", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    _hasRemindDialog = false;
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void masterUpdate_NewVersionFound(string[] newfiles)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MasterDataUpdate.NewVersionFoundEventHandler(masterUpdate_NewVersionFound), new object[] { newfiles });
                }
                else
                {
                    _hasRemindDialog = true;
                    string message = "";

                    DataView dv = DynamicCtrl.GetOpenFileMenuConfigFile();
                    if (dv == null)
                        return;

                    Dictionary<string, string> masterFiles = new Dictionary<string, string>();

                    for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                    {
                        masterFiles.Add(DataConvert.GetString(dv.Table.Rows[ix]["FileName"]), DataConvert.GetString(dv.Table.Rows[ix]["Caption"]));
                    }

                    StringBuilder sb = new StringBuilder();
                    for (int ix = 0; ix < newfiles.Length; ix++)
                    {
                        if (String.IsNullOrEmpty(newfiles[ix]))
                            continue;
                        string caption = "";
                        masterFiles.TryGetValue(newfiles[ix], out caption);
                        sb.Append("{");
                        sb.Append(caption);
                        sb.Append("}");
                        if (ix < newfiles.Length - 1)
                            sb.Append("，");
                    }
                    message = sb.ToString();
                    message = message.Substring(0, message.Length - 1);
                    if (MessageBox.Show(this, "检测到有新的" + message + "\r\n是否下载到本地？", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        // Call the auto update program with the parameter
                        MasterDataUpdate masterUpdate = new MasterDataUpdate();
                        masterUpdate.DownloadNewMasterDataFiles(newfiles);
                        MessageBox.Show(this, "下载完成！");
                    }
                    _hasRemindDialog = false;
                }
            }
            catch
            {
                //Program.ShowMessageBox("MainForm", ex);
            }
        }
        #endregion

        private void UpdateDelayTime()
        {
            if (AutoUpdate.CompareVersions("2.5.14.407", Assembly.GetExecutingAssembly().GetName().Version.ToString()))
            {
                DelayInfo delay = ConfigCtrl.GetDelay();
                delay.DelayedTime = 1;

                if (!ConfigCtrl.SetDelay(delay))
                {
                    //MessageBox.Show("UpdateDelayTime failed!", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        #region 启动画面
        private SplashScreenForm splashForm;
        private System.Windows.Forms.Timer splashTimer;
        private void ShowSplashScreen()
        {            
            splashTimer = new System.Windows.Forms.Timer();
            splashTimer.Enabled = true;
            splashTimer.Interval = 100;
            splashTimer.Tick += new System.EventHandler(this.splashTimer_Tick);
            splashTimer.Start();
            splashForm = new SplashScreenForm();
            splashForm.Opacity = 0.2;
            splashForm.Show(this);
            splashForm.TopMost = true;
        }

        private void splashTimer_Tick(object sender, System.EventArgs e)
        {
            try
            {
                this.splashTimer.Stop();
                if (splashForm.Opacity < 1)
                {
                    //spring
                    splashForm.Opacity = splashForm.Opacity + 0.04;
                    //splashForm.Opacity = splashForm.Opacity + 0.01;
                }
                else
                {
                    this.splashTimer.Enabled = false;
                    splashForm.DisposeSplash();
                }
                this.splashTimer.Start();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }
        #endregion

        #region 最小化到任务栏
        private bool _isHiding;
        private FormWindowState _beforeHideState;
        private void contextMenuOpenOrHide_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isHiding)
                {
                    this.Visible = true;
                    this.WindowState = _beforeHideState;
                    this.Show();
                    this.Activate();
                    _isHiding = false;
                }
                else
                {
                    _beforeHideState = this.WindowState;
                    this.Hide();
                    _isHiding = true;
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void contextMenuQuit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeQuit())
                    return;

                string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), MainConstants.DOCK_CONFIGFILE);
                if (_bSaveLayout)
                    dockPanel.SaveAsXml(configFile);
                else if (File.Exists(configFile))
                    File.Delete(configFile);

                this.notifyIcon.Visible = false;
                this.Close();
                this.Dispose(); //可能引发ObjectDisposedException的异常，当有任务在执行中，且强制退出的时候
                Application.Exit();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.Hide();
                    _isHiding = true;
                }
                if (this.WindowState == FormWindowState.Normal)
                {
                    _beforeHideState = this.WindowState;
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
                _beforeHideState = this.WindowState;
                this.Hide();
                _isHiding = true;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void notifyIcon_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (_isHiding)
                    {
                        this.Visible = true;
                        this.WindowState = _beforeHideState;
                        this.Show();
                        this.Activate();
                        _isHiding = false;
                    }
                    else
                    {
                        _beforeHideState = this.WindowState;
                        this.Hide();
                        _isHiding = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        #endregion

        #region OpenFile Menu
        private void BuildOpenFileMenu()
        {
            try
            {
                DataView dv = DynamicCtrl.GetOpenFileMenuConfigFile();
                if (dv == null)
                    return;

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    ToolStripMenuItem menuitem = new ToolStripMenuItem();
                    menuitem.Text = DataConvert.GetString(dv.Table.Rows[ix]["Caption"]);
                    menuitem.Tag = DataConvert.GetString(dv.Table.Rows[ix]["Key"]) + "|" + DataConvert.GetString(dv.Table.Rows[ix]["FileName"]);
                    menuitem.Click += new EventHandler(OpenMasterData_Click);
                    menuItemOpen.DropDownItems.Insert(menuItemOpen.DropDownItems.Count - 2, menuitem);

                    ToolStripMenuItem toolitem = new ToolStripMenuItem();
                    toolitem.Text = DataConvert.GetString(dv.Table.Rows[ix]["Caption"]);
                    toolitem.Tag = DataConvert.GetString(dv.Table.Rows[ix]["Key"]) + "|" + DataConvert.GetString(dv.Table.Rows[ix]["FileName"]);
                    toolitem.Click += new EventHandler(OpenMasterData_Click);
                    toolBarButtonDropDownOpen.DropDownItems.Insert(toolBarButtonDropDownOpen.DropDownItems.Count - 2, toolitem);
                }                
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("BuildOpenFileMenu()", ex);
            }
        }

        private void OpenMasterData_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem menuitem = sender as ToolStripMenuItem;
                if (menuitem != null)
                {
                    string[] info = menuitem.Tag.ToString().Split('|');
                    switch (info[0].ToLower())
                    { 
                        case "cars":
                            ConfigCtrl.GetCarsInMarket();
                            break;
                        case "matches":
                            ConfigCtrl.GetMatches();
                            break;
                        case "seeds":
                            ConfigCtrl.GetSeedsInShop();
                            break;
                        case "rankseeds":
                            ConfigCtrl.GetRankSeeds();
                            break;
                        case "fruits":
                            ConfigCtrl.GetFruits();
                            break;
                        case "calves":
                            ConfigCtrl.GetCalvesInShop();
                            break;
                        case "animalproduct":
                            ConfigCtrl.GetAnimalProducts();
                            break;
                        case "fishfrys":
                            ConfigCtrl.GetFishFrysInShop();
                            break;
                        case "fishtackles":
                            ConfigCtrl.GetFishTacklesInShop();
                            break;
                        case "fishmatured":
                            ConfigCtrl.GetFishMaturedInMarket();
                            break;
                        case "assets":
                            ConfigCtrl.GetAssetsInShop();
                            break;
                        case "advancedpurchase":
                            ConfigCtrl.GetAdvancedPurchaseMD();
                            break;
                        case "cafedishes":
                            ConfigCtrl.GetDishesInMenu();
                            break;
                        case "cafedishestransaction":
                            ConfigCtrl.GetTransactionDishes();
                            break;
                    }
                    string fullName = Path.Combine(Path.Combine(Application.StartupPath, Constants.FOLDER_MASTERDATA), info[1]);                    
                    OpenFile(fullName, menuitem.Text);
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("OpenMasterData_Click", ex);
            }
        }
        #endregion

        #region MainForm_Load
        private void MainForm_Load(object sender, System.EventArgs e)
        {
            try
            {
               //throw new Exception("test");
                this.notifyIcon.Visible = false;
                //spring
                this.Text = "开心助手V" + typeof(MainForm).Assembly.GetName().Version.ToString() + " 正式版 By Johnny";
                notifyIcon.Text = "开心助手V" + typeof(MainForm).Assembly.GetName().Version.ToString();        
                //this.Text = "新春特别版（开心餐厅） By Johnny";
                //notifyIcon.Text = "新春特别版";              


                //show tips(update info of current version)
                //spring
                if (!Properties.Settings.Default.NeverDisplay)
                    menuItemUpdateInfo_Click(null, null);

                //log4netConfigFile
                string log4netConfigFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), MainConstants.LOG4NET_CONFIGFILE);

                if (!File.Exists(log4netConfigFile))
                {
                    string resname = "Johnny.Kaixin.WinUI.Resources.log4net.config";
                    using (StreamReader streamReader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resname)))
                    {
                        string configContent = streamReader.ReadToEnd();
                        FileStream fs = new FileStream(log4netConfigFile, FileMode.Create);
                        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("GB2312"));
                        sw.Write(configContent);
                        sw.Close();
                        sw = null;
                    }
                }
//#if !DEBUG
//                //check credentials
//                DlgFriendValidation dlgLogin = new DlgFriendValidation();
//                if (dlgLogin.ShowDialog(this) != DialogResult.OK)
//                {
//                    this.Close();
//                    this.Dispose();
//                    Application.Exit();
//                    return;
//                }

                //spring
                //check once startup
                CheckAndUpdateVersion(true, false);

                //auto check update every 2 hours
                StartAutoUpdate();

                CheckAndDownloadMasterData(true, false);
                StarMasterDataUpdate();
//#endif
                //update delay time to 1 second
                UpdateDelayTime();

                //dockpanel
                string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), MainConstants.DOCK_CONFIGFILE);

                if (!File.Exists(configFile))
                {
                    string resname = "Johnny.Kaixin.WinUI.Resources.DockPanel.config";
                    using (StreamReader streamReader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resname)))
                    {
                        string configContent = streamReader.ReadToEnd();
                        FileStream fs = new FileStream(configFile, FileMode.Create);
                        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Unicode);
                        sw.Write(configContent);
                        sw.Close();
                        sw = null;
                    }
                }
                dockPanel.LoadFromXml(configFile, _deserializeDockContent);

                _isHiding = false;
                this.notifyIcon.Visible = true;

                //启动画面
                ShowSplashScreen();

                //打开文件菜单
                BuildOpenFileMenu();

                //开始页
                menuItemHomePage_Click(null, null);
                MenuToolEnabled();
                dockPanel.ShowDocumentIcon = menuItemShowDocumentIcon.Checked = !menuItemShowDocumentIcon.Checked;
                dockPanel.ActiveContentChanged += new EventHandler(dockPanel_ActiveContentChanged);
                _beforeHideState = this.WindowState;

            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }

        }        
        #endregion

        #region BeforeQuit
        private bool BeforeQuit()
        {
            //document
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                    form.Close();
            }
            else
            {
                IDockContent[] documents = dockPanel.DocumentsToArray();
                foreach (IDockContent content in documents)
                {
                    FrmDocument frmDoc = content as FrmDocument;
                    if (frmDoc != null)
                    {
                        if (!String.IsNullOrEmpty(frmDoc.Text) && frmDoc.Text.EndsWith("*"))
                        {
                            DialogResult result = MessageBox.Show("文件 " + frmDoc.Text.Substring(0, frmDoc.Text.Length - 1) + " 的文字已经改变。\r\n想保存文件吗？", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                            if (result == DialogResult.Yes)
                            {
                                TextEditorControl editor = frmDoc.Controls[0] as TextEditorControl;
                                if (editor != null)
                                    DoSave(editor);
                            }
                            else if (result == DialogResult.Cancel)
                            {
                                return false;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
            }

            //task
            foreach (Thread thread in _threadList.Values)
            {
                if (thread != null && thread.ThreadState != ThreadState.Stopped &&
                    (thread.ThreadState == ThreadState.Running ||
                    (thread.ThreadState & (ThreadState.WaitSleepJoin)) == 0 ||
                    (thread.ThreadState & (ThreadState.WaitSleepJoin & ThreadState.Background)) == 0))
                {
                    DialogResult result = MessageBox.Show("有任务正在执行。\r\n确定要退出吗？", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                    if (result == DialogResult.Yes)
                    {
                        StopAllTasks();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        #endregion

        #region StopAllTasks
        private void StopAllTasks()
        {
            try
            {
                //get all tasks
                Collection<TaskInfo> tasks = ConfigCtrl.GetSimpleTasks();

                if (tasks != null && tasks.Count > 0)
                {
                    foreach (TaskInfo task in tasks)
                    {
                        //try to stop the thread if it exists
                        Thread currentThread;
                        if (_threadList.TryGetValue(task.TaskId, out currentThread))
                        {
                            currentThread.Abort();
                            currentThread.Interrupt();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }
        #endregion

        #region dockPanel_ActiveContentChanged
        private void dockPanel_ActiveContentChanged(object sender, EventArgs e)
        {
            try
            {
                MenuToolEnabled();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }
        #endregion

        #region MainMenu

        #region File

        //判断菜单按钮是否可用
        private void menuItemFile_DropDownOpening(object sender, EventArgs e)
        {
            try
            {
                if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
                {
                    menuItemClose.Enabled = menuItemCloseAll.Enabled = (ActiveMdiChild != null);
                }
                else
                {
                    menuItemClose.Enabled = (dockPanel.ActiveDocument != null);
                    menuItemCloseAll.Enabled = (dockPanel.DocumentsCount > 0);
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemNew_Click(object sender, EventArgs e)
        {
            try
            {
                FrmDocument dummyDoc = CreateNewDocument("新文档");
                TextEditorControl editor = dummyDoc.Controls[0] as TextEditorControl;
                editor.Document.DocumentChanged += new DocumentEventHandler(Document_DocumentChanged);
                // Modified flag is set during loading because the document 
                // "changes" (from nothing to something). So, clear it again.
                SetModifiedFlag(editor, false);
                if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
                {
                    dummyDoc.MdiParent = this;
                    dummyDoc.Show();
                }
                else
                    dummyDoc.Show(dockPanel);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemOpenAssistantConfig_Click(object sender, EventArgs e)
        {
            try
            {
                ConfigCtrl.GetAssistantConfigFile();
                string fullName = Path.Combine(Application.StartupPath, Constants.FILE_ASSISTANTCONFIG);
                OpenFile(fullName, "主配置文件");
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }
       
        private void menuItemOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog.InitialDirectory = Application.ExecutablePath;
                openFileDialog.Filter = "cs files (*.cs)|*.cs|txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 3;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    // Try to open chosen file
                    OpenFiles(openFileDialog.FileNames);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemSave_Click(object sender, EventArgs e)
        {
            try
            {
                TextEditorControl editor = ActiveEditor;
                if (editor != null)
                    DoSave(editor);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }


        private void menuItemSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                TextEditorControl editor = ActiveEditor;
                if (editor != null)
                    DoSaveAs(editor);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                IDockContent[] documents = dockPanel.DocumentsToArray();
                foreach (IDockContent content in documents)
                {
                    FrmDocument form = content as FrmDocument;
                    if (form != null)
                    {
                        TextEditorControl editor = form.Controls[0] as TextEditorControl;
                        if (editor != null)
                            DoSaveAs(editor);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        public void menuItemClose_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
                    ActiveMdiChild.Close();
                else if (dockPanel.ActiveDocument != null)
                    dockPanel.ActiveDocument.DockHandler.Close();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        public void menuItemCloseAll_Click(object sender, System.EventArgs e)
        {
            try
            {
                CloseAllDocuments();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        public void menuItemCloseAllButThisOne_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
                {
                    Form activeMdi = ActiveMdiChild;
                    foreach (Form form in MdiChildren)
                    {
                        if (form != activeMdi)
                            form.Close();
                    }
                }
                else
                {
                    //foreach (IDockContent document in dockPanel.Documents)
                    //{
                    //    if (!document.DockHandler.IsActivated)
                    //        document.DockHandler.Close();
                    //}
                    IDockContent[] documents = dockPanel.DocumentsToArray();
                    foreach (IDockContent content in documents)
                        if (!content.DockHandler.IsActivated)
                            content.DockHandler.Close();
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemExit_Click(object sender, System.EventArgs e)
        {
            try
            {
                contextMenuQuit_Click(null, null);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void exitWithoutSavingLayout_Click(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeQuit())
                    return;

                _bSaveLayout = false;
                Close();
                _bSaveLayout = true;
                this.Dispose();
                Application.Exit();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }
        #endregion

        #region Edit

        //判断菜单按钮是否可用
        private void menuItemEdit_DropDownOpening(object sender, EventArgs e)
        {
            try
            {
                MenuToolEnabled();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemUndo_Click(object sender, EventArgs e)
        {
            try
            {
                DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.Undo());
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemRedo_Click(object sender, EventArgs e)
        {
            try
            {
                DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.Redo());
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemCut_Click(object sender, EventArgs e)
        {
            try
            {
                if (HaveSelection())
                    DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.Cut());
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemCopy_Click(object sender, EventArgs e)
        {
            try
            {
                if (HaveSelection())
                    DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.Copy());
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemPaste_Click(object sender, EventArgs e)
        {
            try
            {
                DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.Paste());
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (HaveSelection())
                    DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.Delete());
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemFind_Click(object sender, EventArgs e)
        {
            try
            {
                TextEditorControl editor = ActiveEditor;
                if (editor == null) return;
                _findForm.ShowFor(editor, false);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }


        private void menuItemFindAndReplace_Click(object sender, EventArgs e)
        {
            try
            {
                TextEditorControl editor = ActiveEditor;
                if (editor == null) return;
                _findForm.ShowFor(editor, true);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemFindNext_Click(object sender, EventArgs e)
        {
            try
            {
                _findForm.FindNext(true, false,
                string.Format("找不到\"{0}\"", _findForm.LookFor));
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemFindPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                _findForm.FindNext(true, true,
                string.Format("找不到\"{0}\"", _findForm.LookFor));
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }
            

        private void menuitemToggleBookmark_Click(object sender, EventArgs e)
        {
            try
            {
                TextEditorControl editor = ActiveEditor;
                if (editor != null)
                {
                    DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.ToggleBookmark());
                    editor.IsIconBarVisible = editor.Document.BookmarkManager.Marks.Count > 0;
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuitemGoToPrevBookmark_Click(object sender, EventArgs e)
        {
            try
            {
                DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.GotoPrevBookmark(
                   delegate(Bookmark bookmark)
                   {
                       return true;
                   }));
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuitemGoToNextBookmark_Click(object sender, EventArgs e)
        {
            try
            {
                DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.GotoNextBookmark(
                   delegate(Bookmark bookmark)
                   {
                       return true;
                   }));
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }
        private void menuitemGoToClearAllBookmark_Click(object sender, EventArgs e)
        {
            try
            {
                DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.ClearAllBookmarks(
                   delegate(Bookmark bookmark)
                   {
                       return true;
                   }));
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }
        #endregion

        #region View

        private void menuItemAccountManager_Click(object sender, EventArgs e)
        {
            try
            {
                _accountManagerForm.Show(dockPanel);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemTaskManager_Click(object sender, EventArgs e)
        {
            try
            {
                _taskManagerForm.Show(dockPanel);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemPropertyWindow_Click(object sender, System.EventArgs e)
        {
            try
            {
                _propertyForm.Show(dockPanel);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemToolbox_Click(object sender, System.EventArgs e)
        {
            try
            {
                _toolboxForm.Show(dockPanel);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemOutputWindow_Click(object sender, System.EventArgs e)
        {
            try
            {
                _outputForm.DockState = DockState.DockBottom;
                _outputForm.DockAreas = DockAreas.DockBottom;
                 _outputForm.Show(dockPanel);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemTaskList_Click(object sender, System.EventArgs e)
        {
            try
            {
                _taskListForm.Show(dockPanel);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemToolBar_Click(object sender, System.EventArgs e)
        {
            try
            {
                toolBar.Visible = menuItemToolBar.Checked = !menuItemToolBar.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemStatusBar_Click(object sender, System.EventArgs e)
        {
            try
            {
                statusBar.Visible = menuItemStatusBar.Checked = !menuItemStatusBar.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemHomePage_Click(object sender, EventArgs e)
        {
            try
            {
                FrmWebBrowser frmBrowser = new FrmWebBrowser();
                frmBrowser.StartUrl = MainConstants.SUPPORT_HOMEPAGE;
                frmBrowser.Text = "开始页";
                if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
                {
                    frmBrowser.MdiParent = this;
                    frmBrowser.Show();
                }
                else
                    frmBrowser.Show(dockPanel);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemWebBrowser_Click(object sender, EventArgs e)
        {
            try
            {
                FrmWebBrowser frmBrowser = new FrmWebBrowser();
                frmBrowser.Text = "浏览器";
                if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
                {
                    frmBrowser.MdiParent = this;
                    frmBrowser.Show();
                }
                else
                    frmBrowser.Show(dockPanel);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }
        #endregion

        #region Option

        private void menuItemProxySetting_Click(object sender, EventArgs e)
        {
            try
            {
                DlgProxySetting frmProxy = new DlgProxySetting();
                frmProxy.ShowDialog();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemDelaySetting_Click(object sender, EventArgs e)
        {
            try
            {
                DlgDelaySetting frmDelay = new DlgDelaySetting();
                frmDelay.ShowDialog();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }


        private void menuItemSmtpSetting_Click(object sender, EventArgs e)
        {
            try
            {
                DlgSmtpSetting frmSmtp = new DlgSmtpSetting();
                frmSmtp.ShowDialog();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        #endregion

        #region Tools

        private void menuItemTools_Popup(object sender, System.EventArgs e)
        {
            try
            {
                menuItemLockLayout.Checked = !this.dockPanel.AllowEndUserDocking;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemLockLayout_Click(object sender, System.EventArgs e)
        {
            try
            {
                dockPanel.AllowEndUserDocking = !dockPanel.AllowEndUserDocking;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemShowDocumentIcon_Click(object sender, System.EventArgs e)
        {
            try
            {
                dockPanel.ShowDocumentIcon = menuItemShowDocumentIcon.Checked = !menuItemShowDocumentIcon.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemAddFriends_Click(object sender, EventArgs e)
        {
            try
            {
                ShowToolForm("Johnny.Kaixin.WinUI.FrmAddFriends");
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemSendMessage_Click(object sender, EventArgs e)
        {
            try
            {
                ShowToolForm("Johnny.Kaixin.WinUI.FrmSendMessage");
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemBuildTeam_Click(object sender, EventArgs e)
        {
            try
            {
                ShowToolForm("Johnny.Kaixin.WinUI.FrmBuildTeam");
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemBuyCards_Click(object sender, EventArgs e)
        {
            try
            {
                ShowToolForm("Johnny.Kaixin.WinUI.FrmBuyCards");
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemUpgradeGarage_Click(object sender, EventArgs e)
        {
            try
            {
                ShowToolForm("Johnny.Kaixin.WinUI.FrmUpgradeGarage");
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemMaintainContact_Click(object sender, EventArgs e)
        {
            try
            {
                ShowToolForm("Johnny.Kaixin.WinUI.FrmMaintainContact");
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemUpdateData_Click(object sender, EventArgs e)
        {
            try
            {
                ShowToolForm("Johnny.Kaixin.WinUI.FrmUpdateData");
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemDownloadMasterData_Click(object sender, EventArgs e)
        {
            try
            {
                CheckAndDownloadMasterData(false, true);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }
        #endregion

        #region Window
        private void menuItemNewWindow_Click(object sender, System.EventArgs e)
        {
            try
            {
                MainForm newWindow = new MainForm();
                newWindow.Text = newWindow.Text + " - New";
                newWindow.Show();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }
        #endregion

        #region Help
        private void menuItemRelatedInfo_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(MainConstants.SUPPORT_HOMEPAGE);
            }
            catch (Exception ex)
            {
                MessageBox.Show("无法打开链接。错误信息：" + ex.Message, MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuItemDownload_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://kaixintools.ys168.com/");
            }
            catch (Exception ex)
            {
                MessageBox.Show("无法打开链接。错误信息：" + ex.Message, MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuItemDownloadFramework_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.microsoft.com/downloads/details.aspx?familyid=0856EACB-4362-4B0D-8EDD-AAB15C5E04F5&displaylang=zh-cn");
            }
            catch (Exception ex)
            {
                MessageBox.Show("无法打开链接。错误信息：" + ex.Message, MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuItemUpdateInfo_Click(object sender, EventArgs e)
        {
            try
            {
                DlgTips dlgTips = new DlgTips();
                dlgTips.ShowDialog(this);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemCheckVersion_Click(object sender, EventArgs e)
        {
            try
            {
                CheckAndUpdateVersion(false, true);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        private void menuItemAbout_Click(object sender, System.EventArgs e)
        {
            try
            {
                AboutDialog aboutDialog = new AboutDialog();
                aboutDialog.ShowDialog(this);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }
        #endregion

        #endregion

        #region ToolBar Event
        private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolStripItemClickedEventArgs e)
        {
            try
            {
                if (e.ClickedItem == toolBarButtonNew)
                    menuItemNew_Click(null, null);
                if (e.ClickedItem == toolBarButtonSave)
                    menuItemSave_Click(null, null);
                else if (e.ClickedItem == toolBarButtonSaveAll)
                    menuItemSaveAll_Click(null, null);
                else if (e.ClickedItem == toolBarButtonCut)
                    menuItemCut_Click(null, null);
                else if (e.ClickedItem == toolBarButtonCopy)
                    menuItemCopy_Click(null, null);
                else if (e.ClickedItem == toolBarButtonPaste)
                    menuItemPaste_Click(null, null);
                else if (e.ClickedItem == toolBarButtonUndo)
                    menuItemUndo_Click(null, null);
                else if (e.ClickedItem == toolBarButtonRedo)
                    menuItemRedo_Click(null, null);
                if (e.ClickedItem == toolBarButtonSolutionExplorer)
                    menuItemTaskManager_Click(null, null);
                else if (e.ClickedItem == toolBarButtonPropertyWindow)
                    menuItemPropertyWindow_Click(null, null);
                else if (e.ClickedItem == toolBarButtonToolbox)
                    menuItemToolbox_Click(null, null);
                else if (e.ClickedItem == toolBarButtonOutputWindow)
                    menuItemOutputWindow_Click(null, null);
                else if (e.ClickedItem == toolBarButtonTaskList)
                    menuItemTaskList_Click(null, null);
                else if (e.ClickedItem == toolBarButtonAccountsManagerment)
                    menuItemAccountManager_Click(null, null);
                else if (e.ClickedItem == toolBarButtonHomePage)
                    menuItemHomePage_Click(null, null);
                else if (e.ClickedItem == toolBarButtonWebBrowser)
                    menuItemWebBrowser_Click(null, null);
                else if (e.ClickedItem == toolBarButtonAddFriends)
                    menuItemAddFriends_Click(null, null);
                else if (e.ClickedItem == toolBarButtonSendMessage)
                    menuItemSendMessage_Click(null, null);
                else if (e.ClickedItem == toolBarButtonBuildTeam)
                    menuItemBuildTeam_Click(null, null);
                else if (e.ClickedItem == toolBarButtonBuyCards)
                    menuItemBuyCards_Click(null, null);
                else if (e.ClickedItem == toolBarButtonUpgradeGarage)
                    menuItemUpgradeGarage_Click(null, null);
                else if (e.ClickedItem == toolBarButtonMaintainContact)
                    menuItemMaintainContact_Click(null, null);
                else if (e.ClickedItem == toolBarButtonUpdateData)
                    menuItemUpdateData_Click(null, null);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }        

        private void toolBarButtonDropDownOpen_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                if (e.ClickedItem == toolBarButtonOpenFile)
                    menuItemOpenFile_Click(null, null);
                else if (e.ClickedItem == toolBarButtonOpenAssistantConfig)
                    menuItemOpenAssistantConfig_Click(null, null);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }
        #endregion

        #region Private Methods

        private IDockContent FindDocument(string text)
		{
			if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
			{
				foreach (Form form in MdiChildren)
					if (form.Text == text)
						return form as IDockContent;
				
				return null;
			}
			else
			{
				foreach (IDockContent content in dockPanel.Documents)
					if (content.DockHandler.TabText == text)
						return content;

				return null;
			}
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(FrmTaskManager).ToString())
                return _taskManagerForm;
            else if (persistString == typeof(FrmProperty).ToString())
                return _propertyForm;
            else if (persistString == typeof(FrmToolbox).ToString())
                return _toolboxForm;
            else if (persistString == typeof(FrmOutput).ToString())
                return _outputForm;
            else if (persistString == typeof(DummyTaskList).ToString())
                return _taskListForm;
            else if (persistString == typeof(FrmAccountManager).ToString())
                return _accountManagerForm;
            else
            {
                string[] parsedStrings = persistString.Split(new char[] { ',' });
                if (parsedStrings.Length == 3)
                {
                    if (parsedStrings[0] != typeof(FrmDocument).ToString())
                        return null;

                    FrmDocument dummyDoc = new FrmDocument();
                    TextEditorControl editor;
                    if (parsedStrings[1] != string.Empty && parsedStrings[2] != string.Empty)
                    {
                        if (!File.Exists(parsedStrings[1]))
                            return null;

                        dummyDoc = CreateNewDocument(Path.GetFileName(parsedStrings[1]));
                        editor = dummyDoc.Controls[0] as TextEditorControl;
                        editor.LoadFile(parsedStrings[1]);
                        editor.Document.DocumentChanged += new DocumentEventHandler(Document_DocumentChanged);
                        // Modified flag is set during loading because the document 
                        // "changes" (from nothing to something). So, clear it again.
                        SetModifiedFlag(editor, false);
                        if (!String.IsNullOrEmpty(parsedStrings[2]))
                            dummyDoc.Text = parsedStrings[2];
                        dummyDoc.LastWriteTime = new FileInfo(parsedStrings[1]).LastWriteTime;
                        dummyDoc.Show(dockPanel);
                    }
                    else
                    {
                        dummyDoc = CreateNewDocument("新文档");
                        editor = dummyDoc.Controls[0] as TextEditorControl;
                        editor.Document.DocumentChanged += new DocumentEventHandler(Document_DocumentChanged);
                        // Modified flag is set during loading because the document 
                        // "changes" (from nothing to something). So, clear it again.
                        SetModifiedFlag(editor, false);
                        dummyDoc.Show(dockPanel);
                    }
                    //文档已经打开，只是为了符合函数返回类型               
                    return dummyDoc;
                }
                else if (parsedStrings.Length == 5)
                {
                    if (parsedStrings[0] != typeof(FrmTaskEditor).ToString())
                        return null;

                    if (parsedStrings[1] != string.Empty && parsedStrings[2] != string.Empty && parsedStrings[3] != string.Empty && parsedStrings[4] != string.Empty)
                    {
                        Collection<TaskInfo> tasks = ConfigCtrl.GetSimpleTasks();
                        foreach (TaskInfo task in tasks)
                        {
                            if (task.TaskId == parsedStrings[1] &&
                                task.TaskName == parsedStrings[2] &&
                                task.GroupName == parsedStrings[3])
                            {
                                FrmTaskEditor frmtaskeditor = new FrmTaskEditor();
                                frmtaskeditor.TaskId = parsedStrings[1];
                                frmtaskeditor.TaskName = parsedStrings[2];
                                frmtaskeditor.GroupName = parsedStrings[3];
                                frmtaskeditor.Text = parsedStrings[4];
                                frmtaskeditor.taskSaved += new FrmTaskEditor.TaskSavedEventHandler(frmtaskeditor_taskSaved);
                                frmtaskeditor.Show(dockPanel);
                                return frmtaskeditor;
                            }
                        }                        
                    }
                }
                else
                {
                    return null;
                }
            }

            return null;
        }
        #endregion

        #region Public Methods

        public void OpenGroupConfigFile(string group)
        {
            string fullName = Path.Combine(Application.StartupPath + Constants.CHAR_DOUBLEBACKSLASH + Constants.FOLDER_ACCOUNTS + Constants.CHAR_DOUBLEBACKSLASH + group, Constants.FILE_GROUPCONFIG);
            OpenFile(fullName, group + "[组]");
        }

        public void OpenOperationConfigFile(string group, AccountInfo account)
        {
            string folder = Path.Combine(Application.StartupPath, Constants.FOLDER_ACCOUNTS) + Constants.CHAR_DOUBLEBACKSLASH + group;
            string fullName = folder + Constants.CHAR_DOUBLEBACKSLASH + account.Email + ".xml";
            OpenFile(fullName, account.UserName + "[黑白名单]");
        }

        public void ShowUserDetail(string group, AccountInfo account)
        {
            FrmUserDetail frmuser = new FrmUserDetail();
            frmuser.Group = group;
            frmuser.Account = account.Clone();
            frmuser.Text = account.UserName;
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                frmuser.MdiParent = this;
                frmuser.Show();
            }
            else
            {
                foreach (IDockContent content in dockPanel.Documents)
                {
                    FrmUserDetail form = content as FrmUserDetail;
                    if (form != null && form.Account.Email == account.Email)
                    {
                        content.DockHandler.Show();
                        return;
                    }
                }

                frmuser.Show(dockPanel);
            }
        }

        public void ShowTaskEditor(TaskInfo task)
        {
            FrmTaskEditor frmtaskeditor = new FrmTaskEditor();
            frmtaskeditor.TaskId = task.TaskId;
            frmtaskeditor.TaskName = task.TaskName;
            frmtaskeditor.GroupName = task.GroupName;
            frmtaskeditor.Text = task.TaskName;
            frmtaskeditor.taskSaved += new FrmTaskEditor.TaskSavedEventHandler(frmtaskeditor_taskSaved);
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                frmtaskeditor.MdiParent = this;
                frmtaskeditor.Show();
            }
            else
            {
                foreach (IDockContent content in dockPanel.Documents)
                {
                    FrmTaskEditor form = content as FrmTaskEditor;
                    if (form != null && form.TaskId == task.TaskId)
                    {
                        content.DockHandler.Show();
                        return;
                    }
                }
                frmtaskeditor.Show(dockPanel);
            }
        }

        public void OpenTaskConfigFile(TaskInfo task)
        {
            string folder = Path.Combine(Application.StartupPath, Constants.FOLDER_TASKS);
            string fullName = folder + Constants.CHAR_DOUBLEBACKSLASH + task.TaskId + ".xml";
            OpenFile(fullName, task.TaskName + "[任务]");
        }

        private void frmtaskeditor_taskSaved(string taskid, string taskname)
        {
            _taskManagerForm.RefreshTaskNode(taskid, taskname);
        }

        public void ShowWebBrowser(AccountInfo account)
        {            
            FrmWebBrowser frmbrowser = new FrmWebBrowser();
            frmbrowser.Account = account;
            frmbrowser.NeedsPost = true;
            frmbrowser.Text = account.UserName;
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                frmbrowser.MdiParent = this;
                frmbrowser.Show();
            }
            else
                frmbrowser.Show(dockPanel);
        }
        
        public void ShowChineseWord()
        {
            FrmChineseWord frmchineseword = new FrmChineseWord();
            frmchineseword.Text = "汉字转拼音(全拼)";            

            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                frmchineseword.MdiParent = this;
                frmchineseword.Show();
            }
            else
            {
                foreach (IDockContent content in dockPanel.Documents)
                {
                    FrmChineseWord form = content as FrmChineseWord;
                    if (form != null)
                    {
                        content.DockHandler.Show();
                        return;
                    }
                }
                frmchineseword.Show(dockPanel);
            }
        }

        public void ShowToolForm(string uniqueId)
        {
            FrmToolBase toolform = FormManager.Instance.GetWindow(uniqueId, Output_MessageChanged);
            toolform.Show(dockPanel);
        }
        
        #endregion

        #region Output_MessageChanged
        private void Output_MessageChanged(string caption, string key, string message)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new KaixinBase.MessageChangedEventHandler(Output_MessageChanged), new object[] { caption, key, message });
                }
                else
                {
                    _outputForm.SetMessage(caption, key, message, dockPanel);
                }
            }
            catch (ObjectDisposedException)
            {
                //do nothing
            }
            catch (ThreadAbortException)
            {
                //do nothing
            }
            catch (ThreadInterruptedException)
            {
                //do nothing
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }
        #endregion

        #region Task

        public void StartTask(string taskid, string taskname)
        {
            Thread startThread;
            //if the thread is existing.
            if (_threadList.TryGetValue(taskid, out startThread))
            {
                _threadList.Remove(taskid);
            }
            TaskManager task;
            if (_taskManagerList.TryGetValue(taskid, out task))
            {
                _taskManagerList.Remove(taskid);
            }

            startThread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {                
                task = new TaskManager(taskid, taskname);
                task.ValidateCodeNeeded += new KaixinBase.ValidateCodeNeededEventHandler(task_ValidateCodeNeeded);
                task.MessageChanged += new KaixinBase.MessageChangedEventHandler(Output_MessageChanged);
                _taskManagerList.Add(taskid, task);
                task.TaskStart();
            }));

            startThread.IsBackground = true;
            startThread.Start();
            _threadList.Add(taskid, startThread);
        }

        void task_ValidateCodeNeeded(byte[] image, string taskid, string taskname)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new KaixinBase.ValidateCodeNeededEventHandler(task_ValidateCodeNeeded), new object[] { image, taskid, taskname });
                }
                else
                {
                    DlgPicCode picCode = new DlgPicCode();
                    picCode.ValidationImage = image;
                    picCode.WindowsCaption = "任务：" + taskname;
                    if (picCode.ShowDialog() == DialogResult.OK)
                    {
                        TaskManager currentTaskManager;
                        if (_taskManagerList.TryGetValue(taskid, out currentTaskManager))
                        {
                            currentTaskManager.ValidationCode = picCode.ValidationCode;
                        }
                    }
                    else
                    {
                        TaskManager currentTaskManager;
                        if (_taskManagerList.TryGetValue(taskid, out currentTaskManager))
                        {
                            currentTaskManager.ValidationCode = null;
//                            currentTaskManager.RetryLogin = false;
                        }
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                //do nothing
            }
            catch (ThreadAbortException)
            {
                //do nothing
            }
            catch (ThreadInterruptedException)
            {
                //do nothing
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }

        public void StopTask(string taskid, string taskname)
        {            
            _outputForm.SetSelection(taskname, taskid);
            Thread currentThread;
            if (_threadList.TryGetValue(taskid, out currentThread))
            {
                _taskManagerList.Remove(taskid);
                currentThread.Abort();
                currentThread.Interrupt();
            }
        }        

        public void ChangeTaskEditTabName(TaskInfo task)
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                
            }
            else
            {
                foreach (IDockContent content in dockPanel.Documents)
                {
                    FrmTaskEditor form = content as FrmTaskEditor;
                    if (form != null && form.TaskId == task.TaskId)
                    {
                        form.TaskName = task.TaskName;
                        form.Text = task.TaskName;
                        return;
                    }
                }
            }
        }

        #endregion

        #region Task Tree
        public void TaskNodeSelected(TaskInfo task, Collection<OperationInfo> operations)
        {
            if (!IsHiding())
            {
                _propertyForm.Task = task;
                _propertyForm.Operations = operations;
                _propertyForm.Show(dockPanel);
            }
        }

        public void RootNodeSelected(Collection<TaskInfo> tasks)
        {
            if (!IsHiding())
            {
                _propertyForm.Tasks = tasks;
                _propertyForm.Show(dockPanel);
            }
        }

        public void OperationNodeSelected(OperationInfo operation)
        {
            if (!IsHiding())
            {
                _propertyForm.Operation = operation;
                _propertyForm.Show(dockPanel);
            }
        }
        #endregion

        #region Account Tree
        public void AccountNodeSelected(AccountInfo account)
        {
            if (!IsHiding())
            {
                _propertyForm.Account = account;
                _propertyForm.Show(dockPanel);
            }
        }

        public void AccountGroupNodeSelected(Collection<AccountInfo> accounts, string group)
        {
            if (!IsHiding())
            {
                _propertyForm.Accounts = accounts;
                _propertyForm.Group = group;
                _propertyForm.Show(dockPanel);
            }
        }

        public void AccountRootNodeSelected(string[] groups)
        {
            if (!IsHiding())
            {
                _propertyForm.Groups = groups;
                _propertyForm.Show(dockPanel);
            }
        }

        private bool IsHiding()
        {
            if (_propertyForm.DockState == DockState.DockBottomAutoHide || _propertyForm.DockState == DockState.DockLeftAutoHide || _propertyForm.DockState == DockState.DockRightAutoHide || _propertyForm.DockState == DockState.DockTopAutoHide)
                return true;
            else
                return false;
        }

        #endregion
        
        #region Edit Functionality

        #region File
        private FrmDocument CreateNewDocument(string text)
        {
            FrmDocument dummyDoc = new FrmDocument();
            dummyDoc.Text = text;

            _editor = dummyDoc.Controls[0] as TextEditorControl;           
            if (_editorSettings == null)
            {
                _editorSettings = _editor.TextEditorProperties;
                OnSettingsChanged();
            }
            else
                _editor.TextEditorProperties = _editorSettings;

            return dummyDoc;
        }

        //private void OpenFile(string filename)
        //{
        //    string[] files = new string[1];
        //    files[0] = filename;
        //    OpenFiles(files);
        //}

        private void OpenFile(string filename, string caption)
        {
            string[] files = new string[1];
            files[0] = filename;
            OpenFiles(files, caption);
        }

        private void OpenFiles(string[] fns)
        {
            OpenFiles(fns, "");
        }

        private void OpenFiles(string[] fns, string caption)
        {
            // Open file(s)
            foreach (string fn in fns)
            {
                FrmDocument dummyDoc = new FrmDocument(); ;
                TextEditorControl editor = new TextEditorControl();

                try
                {
                    if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
                    {
                        dummyDoc = CreateNewDocument(Path.GetFileName(fn));
                        dummyDoc.MdiParent = this;
                        dummyDoc.Show();
                    }
                    else
                    {
                        foreach (IDockContent content in dockPanel.Documents)
                        {
                            dummyDoc = content as FrmDocument;
                            if (dummyDoc != null)
                            {
                                editor = dummyDoc.Controls[0] as TextEditorControl;
                                if (editor.FileName == fn)
                                {
                                    content.DockHandler.Show();
                                    return;
                                }
                            }
                        }

                        dummyDoc = CreateNewDocument(Path.GetFileName(fn));
                        editor = dummyDoc.Controls[0] as TextEditorControl;
                        editor.LoadFile(fn);
                        editor.Document.DocumentChanged += new DocumentEventHandler(Document_DocumentChanged);
                        // Modified flag is set during loading because the document 
                        // "changes" (from nothing to something). So, clear it again.
                        SetModifiedFlag(editor, false);
                        if (!String.IsNullOrEmpty(caption))
                            dummyDoc.Text = caption;
                        dummyDoc.LastWriteTime = new FileInfo(fn).LastWriteTime;
                        dummyDoc.Show(dockPanel);
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().Name);
                    dummyDoc.Close();
                    dummyDoc.Dispose();
                    return;
                }

                // ICSharpCode.TextEditor doesn't have any built-in code folding
                // strategies, so I've included a simple one. Apparently, the
                // foldings are not updated automatically, so in this demo the user
                // cannot add or remove folding regions after loading the file.
                editor.Document.FoldingManager.FoldingStrategy = new RegionFoldingStrategy();
                editor.Document.FoldingManager.UpdateFoldings(null, null);
            }
        }

        private void Document_DocumentChanged(object sender, DocumentEventArgs e)
        {
            if (ActiveEditor != null)
                SetModifiedFlag(ActiveEditor, true);            
        }

        public bool DoSave(TextEditorControl editor)
        {
            if (string.IsNullOrEmpty(editor.FileName))
                return DoSaveAs(editor);
            else
            {
                try
                {                    
                    editor.SaveFile(editor.FileName);
                    SetModifiedFlag(editor, false);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().Name);
                    return false;
                }
            }
        }
        private bool DoSaveAs(TextEditorControl editor)
        {
            saveFileDialog.FileName = editor.FileName;
            saveFileDialog.InitialDirectory = Application.ExecutablePath;
            saveFileDialog.Filter = "XML files (*.xml)|*.xml|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    editor.SaveFile(saveFileDialog.FileName);
                    _docform.LastWriteTime = new FileInfo(saveFileDialog.FileName).LastWriteTime;
                    editor.Parent.Text = Path.GetFileName(editor.FileName);
                    SetModifiedFlag(editor, false);

                    // The syntax highlighting strategy doesn't change
                    // automatically, so do it manually.
                    editor.Document.HighlightingStrategy =
                        HighlightingStrategyFactory.CreateHighlightingStrategyForFile(editor.FileName);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().Name);
                }
            }
            return false;
        }
        private bool IsModified(TextEditorControl editor)
        {            
            return editor.Parent.Text.EndsWith("*");
        }
        private void SetModifiedFlag(TextEditorControl editor, bool flag)
        {
            if (IsModified(editor) != flag)
            {
                Control p = editor.Parent;
                if (IsModified(editor))
                {
                    p.Text = p.Text.Substring(0, p.Text.Length - 1);
                    _docform.LastWriteTime = new FileInfo(editor.FileName).LastWriteTime;
                }
                else
                    p.Text += "*";
            }
        }

        private void CloseAllDocuments()
        {
            try
            {
                if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
                {
                    foreach (Form form in MdiChildren)
                        form.Close();
                }
                else
                {
                    IDockContent[] documents = dockPanel.DocumentsToArray();
                    foreach (IDockContent content in documents)
                        content.DockHandler.Close();
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("MainForm", ex);
            }
        }
        #endregion

        #region Edit
        private void DoEditAction(TextEditorControl editor, ICSharpCode.TextEditor.Actions.IEditAction action)
        {
            if (editor != null && action != null)
            {
                TextArea area = editor.ActiveTextAreaControl.TextArea;
                editor.BeginUpdate();
                try
                {
                    lock (editor.Document)
                    {
                        action.Execute(area);
                        if (area.SelectionManager.HasSomethingSelected && area.AutoClearSelection /*&& caretchanged*/)
                        {
                            if (area.Document.TextEditorProperties.DocumentSelectionMode == DocumentSelectionMode.Normal)
                            {
                                area.SelectionManager.ClearSelection();
                            }
                        }
                    }
                }
                finally
                {
                    editor.EndUpdate();
                    area.Caret.UpdateCaretPosition();
                }
            }
        }
        private bool HaveSelection()
        {
            TextEditorControl editor = ActiveEditor;
            return editor != null &&
                editor.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected;
        }
        private bool IsTextEditor()
        {
            return ActiveEditor != null;
        }
        #endregion

        #region Option
        /// <summary>Show current settings on the Options menu</summary>
        /// <remarks>We don't have to sync settings between the editors because 
        /// they all share the same DefaultTextEditorProperties object.</remarks>
        private void OnSettingsChanged()
        {
            //menuItemShowSpacesAndTabs.Checked = _editorSettings.ShowSpaces;
            //menuItemShowNewlines.Checked = _editorSettings.ShowEOLMarker;
            //menuItemShowLineNumbers.Checked = _editorSettings.ShowLineNumbers;
            //menuItemHighlightCurrentRow.Checked = _editorSettings.LineViewerStyle == LineViewerStyle.FullRow;
            //menuItemHhighlightMatchingBracketsWhenCursorIsAfter.Checked = _editorSettings.BracketMatchingStyle == BracketMatchingStyle.After;
            //menuItemAllowCursorPastEndofline.Checked = _editorSettings.AllowCaretBeyondEOL;
        }
        #endregion        

        #region PropertyChangedInTextEditor
        public void PropertyChangedInTextEditor()
        {
            MenuToolEnabled();
        }
        #endregion

        #region MenuToolEnabled
        private void MenuToolEnabled()
        {
            bool isTextEditor = IsTextEditor();
            bool isHaveSelection = HaveSelection();

            //menu
            menuItemSave.Enabled = isTextEditor;
            menuItemSaveAs.Enabled = isTextEditor;
            menuItemUndo.Enabled = isTextEditor && ActiveEditor.Document.UndoStack.CanUndo;
            menuItemRedo.Enabled = isTextEditor && ActiveEditor.Document.UndoStack.CanRedo;
            menuItemCut.Enabled = isTextEditor && isHaveSelection;
            menuItemCopy.Enabled = isTextEditor && isHaveSelection;
            menuItemPaste.Enabled = isTextEditor && ActiveEditor.ActiveTextAreaControl.TextArea.ClipboardHandler.EnablePaste;
            menuItemDelete.Enabled = isTextEditor && isHaveSelection;
            menuItemFind.Enabled = isTextEditor;
            menuItemFindAndReplace.Enabled = isTextEditor;
            menuItemFindNext.Enabled = isTextEditor;
            menuItemFindPrevious.Enabled = isTextEditor;
            menuitemToggleBookmark.Enabled = isTextEditor;
            menuitemGoToPrevBookmark.Enabled = isTextEditor;
            menuitemGoToNextBookmark.Enabled = isTextEditor;
            menuitemGoToClearAllBookmark.Enabled = isTextEditor;

            //toolbar
            toolBarButtonSave.Enabled = menuItemSave.Enabled;
            toolBarButtonCut.Enabled = menuItemCut.Enabled;
            toolBarButtonCopy.Enabled = menuItemCopy.Enabled;
            toolBarButtonPaste.Enabled = menuItemPaste.Enabled;
            toolBarButtonUndo.Enabled = menuItemUndo.Enabled;
            toolBarButtonRedo.Enabled = menuItemRedo.Enabled;
           
        }
        #endregion

        #endregion

        #region Property
        private TextEditorControl ActiveEditor
        {
            get
            {
                IDockContent[] documents = dockPanel.DocumentsToArray();
                foreach (IDockContent content in documents)
                {
                    if (content.DockHandler.IsActivated)
                    {
                        _docform = content as FrmDocument;
                        if (_docform != null)
                            return _docform.Controls[0] as TextEditorControl;
                        else
                            return null;
                    }
                }
                return null;
            }
        }
        #endregion        
        
    }

    #region RegionFoldingStrategy
    /// <summary>
    /// The class to generate the foldings, it implements ICSharpCode.TextEditor.Document.IFoldingStrategy
    /// </summary>
    public class RegionFoldingStrategy : IFoldingStrategy
    {
        /// <summary>
        /// Generates the foldings for our document.
        /// </summary>
        /// <param name="document">The current document.</param>
        /// <param name="fileName">The filename of the document.</param>
        /// <param name="parseInformation">Extra parse information, not used in this sample.</param>
        /// <returns>A list of FoldMarkers.</returns>
        public List<FoldMarker> GenerateFoldMarkers(IDocument document, string fileName, object parseInformation)
        {
            List<FoldMarker> list = new List<FoldMarker>();

            Stack<int> startLines = new Stack<int>();

            // Create foldmarkers for the whole document, enumerate through every line.
            for (int i = 0; i < document.TotalNumberOfLines; i++)
            {
                LineSegment seg = document.GetLineSegment(i);
                int offs, end = document.TextLength;
                char c;
                for (offs = seg.Offset; offs < end && ((c = document.GetCharAt(offs)) == ' ' || c == '\t'); offs++)
                { }
                if (offs == end)
                    break;
                int spaceCount = offs - seg.Offset;

                // now offs points to the first non-whitespace char on the line
                if (document.GetCharAt(offs) == '#')
                {
                    string text = document.GetText(offs, seg.Length - spaceCount);
                    if (text.StartsWith("#region"))
                        startLines.Push(i);
                    if (text.StartsWith("#endregion") && startLines.Count > 0)
                    {
                        // Add a new FoldMarker to the list.
                        int start = startLines.Pop();
                        list.Add(new FoldMarker(document, start,
                            document.GetLineSegment(start).Length,
                            i, spaceCount + "#endregion".Length));
                    }
                }
            }

            return list;
        }
    }
    #endregion
}