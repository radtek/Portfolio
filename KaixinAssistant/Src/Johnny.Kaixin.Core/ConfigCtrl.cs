using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Forms;

using Johnny.Kaixin.Helper;
using Johnny.Library.Helper;

namespace Johnny.Kaixin.Core
{
    public sealed class ConfigCtrl
    {
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

        private static string GetFolderName(string path)
        {
            if (path == null || path == string.Empty)
                return "";
            return path.Substring(path.LastIndexOf("\\") + 1);
        }

        private static XmlNode GetAppendNode(XmlNode parentNode, XmlDocument objXmlDoc, string nodename, string defaultvalue)
        {
            XmlNode target = parentNode.SelectSingleNode(nodename);
            if (target == null)
            {
                XmlElement objChildNode = objXmlDoc.CreateElement(nodename);
                if (!String.IsNullOrEmpty(defaultvalue))
                    objChildNode.InnerText = defaultvalue;
                parentNode.AppendChild(objChildNode);
                target = parentNode.SelectSingleNode(nodename);
            }
            return target;
        }

        private static long GetCash(string cashtip)
        {
            //我的现金余额3260590
            int index = cashtip.IndexOf("现金余额");
            index = index + 4;
            return DataConvert.GetInt64(cashtip.Substring(index, cashtip.Length - index));
        }

        private static EnumRunMode GetRunMode(string runmode)
        {
            if (String.IsNullOrEmpty(runmode))
                return EnumRunMode.MultiLoop;
            else if (runmode.ToLower().Equals("true"))
                return EnumRunMode.MultiLoop;
            else if (runmode.ToLower().Equals("false"))
                return EnumRunMode.Timing;
            else if (runmode.Equals("SingleLoop"))
                return EnumRunMode.SingleLoop;
            else if (runmode.Equals("MultiLoop"))
                return EnumRunMode.MultiLoop;
            else if (runmode.Equals("Timing"))
                return EnumRunMode.Timing;
            else
                return EnumRunMode.MultiLoop;
        }

        private static bool ConvertIntToBool(int input)
        {
            if (input == 1)
                return true;
            else
                return false;
        }
        #endregion

        #region GetMasterDataFile
        public static XmlDocument GetMasterDataFile(string filename)
        {
            try
            {
                //load config info
                string folder = Path.Combine(Application.StartupPath, Constants.FOLDER_MASTERDATA);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                string configFile = folder + Constants.CHAR_DOUBLEBACKSLASH + filename;

                if (!File.Exists(configFile))
                {
                    string configContent = Utility.GetMasterDataFile(filename);
                    StreamWriter sw = new StreamWriter(configFile);
                    sw.Write(configContent);
                    sw.Close();
                    sw = null;
                }

                XmlDocument objXmlDoc = new XmlDocument();

                objXmlDoc.Load(configFile);

                return objXmlDoc;
            }
            catch (Exception ex)
            {
                LogHelper.Write("GetMasterDataFile:" + filename, ex);
                return null;
            }
        }
        #endregion

        #region SetMasterDataFile
        private static bool SetMasterDataFile(XmlDocument xmldoc, string filename)
        {
            try
            {
                string folder = Path.Combine(Application.StartupPath, Constants.FOLDER_MASTERDATA);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                string configFile = folder + Constants.CHAR_DOUBLEBACKSLASH + filename;
                xmldoc.Save(configFile);
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Write("SetMasterDataFile:" + filename, ex);
                return false;
            }
        }
        #endregion

        #region GetAssistantConfigFile
        public static XmlDocument GetAssistantConfigFile()
        {
            try
            {
                //load config info
                string configFile = Path.Combine(Application.StartupPath, Constants.FILE_ASSISTANTCONFIG);
                if (!File.Exists(configFile))
                {
                    string configContent = Utility.GetAssistantConfig();
                    StreamWriter sw = new StreamWriter(configFile);
                    sw.Write(configContent);
                    sw.Close();
                    sw = null;
                }

                XmlDocument objXmlDoc = new XmlDocument();

                objXmlDoc.Load(configFile);

                return objXmlDoc;
            }
            catch (Exception ex)
            {
                LogHelper.Write("读取主配置文件", ex);
                throw;
            }
        }
        #endregion

        #region SetAssistantConfigFile
        private static bool SetAssistantConfigFile(XmlDocument xmldoc)
        {
            try
            {
                string configFile = Path.Combine(Application.StartupPath, Constants.FILE_ASSISTANTCONFIG);
                xmldoc.Save(configFile);
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Write("保存主配置文件", ex);
                return false;
            }
        }
        #endregion

        #region GetProxy
        public static ProxyInfo GetProxy()
        {
            try
            {
                XmlDocument objXmlDoc = GetAssistantConfigFile();
                if (objXmlDoc == null)
                    return null;

                ProxyInfo proxy = new ProxyInfo();

                XmlNode objNode = objXmlDoc.SelectSingleNode(Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.PROXY_PROXY);
                if (objNode == null)
                    return null;
                if (objNode.Attributes[Constants.PROXY_ENABLE] != null)
                    proxy.Enable = DataConvert.GetBool(objNode.Attributes[Constants.PROXY_ENABLE].Value);
                else
                    proxy.Enable = false;
                proxy.Server = objNode.SelectSingleNode(Constants.PROXY_SERVER).InnerText;
                if (objNode.SelectSingleNode(Constants.PROXY_PORT).InnerText == string.Empty)
                    proxy.Port = null;
                else
                    proxy.Port = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.PROXY_PORT).InnerText);
                proxy.UserName = objNode.SelectSingleNode(Constants.PROXY_USER).InnerText;
                proxy.Password = objNode.SelectSingleNode(Constants.PROXY_PASS).InnerText;
                return proxy;
            }
            catch (Exception ex)
            {
                LogHelper.Write("读取代理设置", ex);
                throw;
            }
        }
        #endregion

        #region SetProxy
        public static bool SetProxy(ProxyInfo proxy)
        {
            try
            {
                XmlDocument objXmlDoc = GetAssistantConfigFile();
                if (objXmlDoc == null)
                    return false;

                XmlNode objNode = objXmlDoc.SelectSingleNode(Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.PROXY_PROXY);
                if (objNode == null)
                    return false;
                if (objNode.Attributes[Constants.PROXY_ENABLE] == null)
                    objNode.Attributes.Append(objXmlDoc.CreateAttribute(Constants.PROXY_ENABLE));
                objNode.Attributes[Constants.PROXY_ENABLE].InnerText = proxy.Enable.ToString();
                objNode.SelectSingleNode(Constants.PROXY_SERVER).InnerText = proxy.Server;
                objNode.SelectSingleNode(Constants.PROXY_PORT).InnerText = proxy.Port.ToString();
                objNode.SelectSingleNode(Constants.PROXY_USER).InnerText = proxy.UserName;
                objNode.SelectSingleNode(Constants.PROXY_PASS).InnerText = proxy.Password;

                return SetAssistantConfigFile(objXmlDoc);
            }
            catch (Exception ex)
            {
                LogHelper.Write("保存代理设置", ex);
                return false;
            }
        }
        #endregion
        
        #region GetDelay
        public static DelayInfo GetDelay()
        {
            try
            {
                XmlDocument objXmlDoc = GetAssistantConfigFile();
                if (objXmlDoc == null)
                    return null;

                DelayInfo delay = new DelayInfo();

                XmlNode objNode = objXmlDoc.SelectSingleNode(Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.DELAY_DELAY);
                if (objNode == null)
                    return null;

                if (objNode.SelectSingleNode(Constants.DELAY_DELAYEDTIME).InnerText == string.Empty)
                    delay.DelayedTime = null;
                else
                    delay.DelayedTime = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.DELAY_DELAYEDTIME).InnerText);
                if (objNode.SelectSingleNode(Constants.DELAY_TIMEOUT).InnerText == string.Empty)
                    delay.TimeOut = null;
                else
                    delay.TimeOut = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.DELAY_TIMEOUT).InnerText);
                if (objNode.SelectSingleNode(Constants.DELAY_TRYTIMES).InnerText == string.Empty)
                    delay.TryTimes = null;
                else
                    delay.TryTimes = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.DELAY_TRYTIMES).InnerText);
                return delay;
            }
            catch (Exception ex)
            {
                LogHelper.Write("读取延迟设置", ex);
                throw;
            }
        }
        #endregion

        #region SetDelay
        public static bool SetDelay(DelayInfo delay)
        {
            try
            {
                XmlDocument objXmlDoc = GetAssistantConfigFile();
                if (objXmlDoc == null)
                    return false;

                XmlNode objNode = objXmlDoc.SelectSingleNode(Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.DELAY_DELAY);
                if (objNode == null)
                    return false;

                objNode.SelectSingleNode(Constants.DELAY_DELAYEDTIME).InnerText = delay.DelayedTime.ToString();
                objNode.SelectSingleNode(Constants.DELAY_TIMEOUT).InnerText = delay.TimeOut.ToString();
                objNode.SelectSingleNode(Constants.DELAY_TRYTIMES).InnerText = delay.TryTimes.ToString();

                return SetAssistantConfigFile(objXmlDoc);
            }
            catch (Exception ex)
            {
                LogHelper.Write("保存延迟设置", ex);   
                return false;
            }
        }
        #endregion

        #region GetSmtp
        public static SmtpInfo GetSmtp()
        {
            try
            {
                XmlDocument objXmlDoc = GetAssistantConfigFile();
                if (objXmlDoc == null)
                    return null;

                SmtpInfo smtp = new SmtpInfo();

                XmlNode objNode = objXmlDoc.SelectSingleNode(Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.SMTP_SMTP);
                if (objNode == null)
                    return null;

                smtp.SmtpHost = DataConvert.GetString(objNode.SelectSingleNode(Constants.SMTP_HOST).InnerText);
                smtp.SmtpPort = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.SMTP_PORT).InnerText);
                smtp.SenderName = DataConvert.GetString(objNode.SelectSingleNode(Constants.SMTP_SENDERNAME).InnerText);
                smtp.SenderEmail = DataConvert.GetString(objNode.SelectSingleNode(Constants.SMTP_SENDEREMAIL).InnerText);
                smtp.UserName = DataConvert.GetString(objNode.SelectSingleNode(Constants.SMTP_USERNAME).InnerText);
                smtp.Password = DataConvert.GetString(objNode.SelectSingleNode(Constants.SMTP_PASSWORD).InnerText);

                return smtp;
            }
            catch (Exception ex)
            {
                LogHelper.Write("读取SMTP设置", ex);
                throw;
            }
        }
        #endregion

        #region SetSmtp
        public static bool SetSmtp(SmtpInfo smtp)
        {
            try
            {
                XmlDocument objXmlDoc = GetAssistantConfigFile();
                if (objXmlDoc == null)
                    return false;

                XmlNode objNode = objXmlDoc.SelectSingleNode(Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.SMTP_SMTP);
                if (objNode == null)
                    return false;

                objNode.SelectSingleNode(Constants.SMTP_HOST).InnerText = smtp.SmtpHost;
                objNode.SelectSingleNode(Constants.SMTP_PORT).InnerText = smtp.SmtpPort.ToString();
                objNode.SelectSingleNode(Constants.SMTP_SENDERNAME).InnerText = smtp.SenderName;
                objNode.SelectSingleNode(Constants.SMTP_SENDEREMAIL).InnerText = smtp.SenderEmail;
                objNode.SelectSingleNode(Constants.SMTP_USERNAME).InnerText = smtp.UserName;
                objNode.SelectSingleNode(Constants.SMTP_PASSWORD).InnerText = smtp.Password;

                return SetAssistantConfigFile(objXmlDoc);

            }
            catch (Exception ex)
            {
                LogHelper.Write("保存SMTP设置", ex);
                return false;
            }
        }
        #endregion        
                
        #region GetAccounts
        public static Collection<AccountInfo> GetAccounts(string groupname)
        {
            try
            {
                XmlDocument objXmlDoc = GetGroupConfigFile(groupname);
                if (objXmlDoc == null)
                    return null;

                DataView dv = GetData(objXmlDoc, Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.ACCOUNT_ACCOUNTS);

                Collection<AccountInfo> accounts = new Collection<AccountInfo>();
                accounts.Clear();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    AccountInfo account = new AccountInfo();
                    account.Email = dv.Table.Rows[ix][0].ToString();
                    account.Password = dv.Table.Rows[ix][1].ToString();
                    account.UserName = dv.Table.Rows[ix][2].ToString();
                    account.UserId = dv.Table.Rows[ix][3].ToString();
                    account.Gender = DataConvert.GetBool(dv.Table.Rows[ix][4]);
                    accounts.Add(account);
                }

                return accounts;
            }
            catch (Exception ex)
            {
                LogHelper.Write("读取组内账号" + groupname, ex);
                return null;
            }
        }
        #endregion

        #region Account Tree
        public static string EditAccount(string groupname, AccountInfo account, string oldemail)
        {
            try
            {
                XmlDocument objXmlDoc = GetGroupConfigFile(groupname);
                if (objXmlDoc == null)
                    return "配置文件不存在！";

                if (oldemail == null || oldemail == string.Empty)
                {
                    if (IsExistAccountNode(objXmlDoc, account))
                        return "该用户已经存在！";
                    else
                        InsertAccounNode(objXmlDoc, account);
                }
                else
                {
                    if (account.Email != oldemail && IsExistAccountNode(objXmlDoc, account))
                        return "该用户已经存在！";
                    else
                        EditAccountNode(objXmlDoc, oldemail, account);
                }

                return SetGroupConfigFile(objXmlDoc, groupname);
            }
            catch (Exception ex)
            {
                LogHelper.Write("编辑账号", ex);
                return Constants.STATUS_FAIL;
            }
        }

        public static string DeleteAccount(string groupname, AccountInfo account)
        {
            try
            {
                XmlDocument objXmlDoc = GetGroupConfigFile(groupname);
                if (objXmlDoc == null)
                    return "配置文件不存在！";

                DeleteAccountNode(objXmlDoc, account);

                return SetGroupConfigFile(objXmlDoc, groupname);
            }
            catch (Exception ex)
            {
                LogHelper.Write("删除账号", ex);
                return Constants.STATUS_FAIL;
            }
        }


        private static void InsertAccounNode(XmlDocument xmldoc, AccountInfo account)
        {

            XmlNode objRootNode = xmldoc.SelectSingleNode(Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.ACCOUNT_ACCOUNTS);
            XmlElement objChildNode = xmldoc.CreateElement(Constants.ACCOUNT_ACCOUNT);
            objRootNode.AppendChild(objChildNode);
            XmlElement emtEmail = xmldoc.CreateElement(Constants.ACCOUNT_EMAIL);
            emtEmail.InnerText = account.Email;
            XmlElement emtPassword = xmldoc.CreateElement(Constants.ACCOUNT_PASSWORD);
            emtPassword.InnerText = account.Password;
            XmlElement emtUserName = xmldoc.CreateElement(Constants.ACCOUNT_USERNAME);
            emtUserName.InnerText = account.UserName;
            XmlElement emtUserId = xmldoc.CreateElement(Constants.ACCOUNT_USERID);
            emtUserId.InnerText = account.UserId;
            XmlElement emtGender = xmldoc.CreateElement(Constants.ACCOUNT_GENDER);
            emtGender.InnerText = account.Gender.ToString();
            objChildNode.AppendChild(emtEmail);
            objChildNode.AppendChild(emtPassword);
            objChildNode.AppendChild(emtUserName);
            objChildNode.AppendChild(emtUserId);
            objChildNode.AppendChild(emtGender);
        }

        private static void EditAccountNode(XmlDocument xmldoc, string email, AccountInfo account)
        {
            XmlNode objRootNode = xmldoc.SelectSingleNode(Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.ACCOUNT_ACCOUNTS);
            foreach (XmlNode xn in objRootNode.ChildNodes)
            {
                if (xn.SelectSingleNode(Constants.ACCOUNT_EMAIL).InnerText == email)
                {
                    xn.SelectSingleNode(Constants.ACCOUNT_EMAIL).InnerText = account.Email;
                    xn.SelectSingleNode(Constants.ACCOUNT_PASSWORD).InnerText = account.Password;
                    xn.SelectSingleNode(Constants.ACCOUNT_USERNAME).InnerText = account.UserName;
                    xn.SelectSingleNode(Constants.ACCOUNT_USERID).InnerText = account.UserId;
                    if (xn.SelectSingleNode(Constants.ACCOUNT_GENDER) == null)
                    {
                        XmlElement emtGender = xmldoc.CreateElement(Constants.ACCOUNT_GENDER);                        
                        xn.AppendChild(emtGender);                        
                    }
                    xn.SelectSingleNode(Constants.ACCOUNT_GENDER).InnerText = account.Gender.ToString();
                    break;
                }
            }
        }

        private static void DeleteAccountNode(XmlDocument xmldoc, AccountInfo user)
        {
            XmlNode objRootNode = xmldoc.SelectSingleNode(Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.ACCOUNT_ACCOUNTS);
            foreach (XmlNode xn in objRootNode.ChildNodes)
            {
                if (xn.SelectSingleNode(Constants.ACCOUNT_EMAIL).InnerText == user.Email)
                {
                    objRootNode.RemoveChild(xn);
                    break;
                }
            }
        }

        private static bool IsExistAccountNode(XmlDocument xmldoc, AccountInfo user)
        {
            XmlNode objRootNode = xmldoc.SelectSingleNode(Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.ACCOUNT_ACCOUNTS);
            foreach (XmlNode xn in objRootNode.ChildNodes)
            {
                if (xn.SelectSingleNode(Constants.ACCOUNT_EMAIL).InnerText == user.Email)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region GetGroups
        public static string[] GetGroups()
        {
            try
            {
                string folder = Path.Combine(Application.StartupPath, Constants.FOLDER_ACCOUNTS);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                string[] groups = Directory.GetDirectories(folder);
                for (int ix = 0; ix < groups.Length; ix++)
                {
                    groups[ix] = GetFolderName(groups[ix]);
                }

                return groups;
            }
            catch (Exception ex)
            {
                LogHelper.Write("读取组信息", ex);
                return null;
            }
        }
        #endregion

        #region AddGroup
        public static string AddGroup(string groupname)
        {
            try
            {
                string folder = Path.Combine(Application.StartupPath, Constants.FOLDER_ACCOUNTS);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                if (!Directory.Exists(folder + Constants.CHAR_DOUBLEBACKSLASH + groupname))
                    Directory.CreateDirectory(folder + Constants.CHAR_DOUBLEBACKSLASH + groupname);
                else
                    return "组名(" + groupname + ")已存在，请使用其他组名！";
            
                return Constants.STATUS_SUCCESS;
            }
            catch (Exception ex)
            {
                LogHelper.Write("添加新组" + groupname, ex);
                return Constants.STATUS_FAIL;
            }
        }
        #endregion     
   
        #region DeleteGroup
        public static string DeleteGroup(string groupname)
        {
            try
            {
                string folder = Path.Combine(Application.StartupPath, Constants.FOLDER_ACCOUNTS);                
                Directory.Delete(folder + Constants.CHAR_DOUBLEBACKSLASH + groupname, true);

                return Constants.STATUS_SUCCESS;
            }
            catch (Exception ex)
            {
                LogHelper.Write("删除组" + groupname, ex);
                return Constants.STATUS_FAIL;
            }
        }
        #endregion

        #region GetGroupConfigFile
        private static XmlDocument GetGroupConfigFile(string groupname)
        {
            try
            {
                //load config info
                //string configFile = Path.Combine(Application.StartupPath + Constants.CHAR_DOUBLEBACKSLASH + Constants.FOLDER_ACCOUNTS + Constants.CHAR_DOUBLEBACKSLASH + groupname, Constants.FILE_GROUPCONFIG);
                string configFile = Path.Combine(Application.StartupPath + Constants.CHAR_DOUBLEBACKSLASH + Constants.FOLDER_ACCOUNTS + Constants.CHAR_DOUBLEBACKSLASH + groupname, Constants.FILE_GROUPCONFIG);
                if (!File.Exists(configFile))
                {
                    string configContent = Utility.GetGroupConfig();
                    StreamWriter sw = new StreamWriter(configFile);
                    sw.Write(configContent);
                    sw.Close();
                    sw = null;
                }

                XmlDocument objXmlDoc = new XmlDocument();

                objXmlDoc.Load(configFile);

                return objXmlDoc;
            }
            catch (Exception ex)
            {
                LogHelper.Write("读取组配置文件" + groupname, ex);
                return null;
            }
        }
        #endregion

        #region SetGroupConfigFile
        private static string SetGroupConfigFile(XmlDocument xmldoc, string groupname)
        {
            try
            {
                string configFile = Path.Combine(Application.StartupPath + Constants.CHAR_DOUBLEBACKSLASH + Constants.FOLDER_ACCOUNTS + Constants.CHAR_DOUBLEBACKSLASH + groupname, Constants.FILE_GROUPCONFIG);
                xmldoc.Save(configFile);
                return Constants.STATUS_SUCCESS;
            }
            catch (Exception ex)
            {
                LogHelper.Write("保存组配置文件" + groupname, ex);
                return Constants.STATUS_FAIL;
            }
        }
        #endregion

        #region GetSimpleTasks
        public static Collection<TaskInfo> GetSimpleTasks()
        {
            try
            {
                XmlDocument objXmlDoc = GetAssistantConfigFile();
                if (objXmlDoc == null)
                    return null;

                DataView dv = GetData(objXmlDoc, Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.TASK_TASKS);

                Collection<TaskInfo> tasks = new Collection<TaskInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    TaskInfo task = new TaskInfo();
                    task.TaskId = dv.Table.Rows[ix][0].ToString();
                    task.TaskName = dv.Table.Rows[ix][1].ToString();
                    if (dv.Table.Columns.Count > 2)
                        task.GroupName = dv.Table.Rows[ix][2].ToString();
                    else
                        task.GroupName = "";
                    tasks.Add(task);
                }

                return tasks;
            }
            catch (Exception ex)
            {
                LogHelper.Write("读取简单任务列表", ex);
                return null;
            }
        }
        #endregion

        #region GetTasks
        public static Collection<TaskInfo> GetTasks()
        {
            try
            {
                XmlDocument objXmlDoc = GetAssistantConfigFile();
                if (objXmlDoc == null)
                    return null;

                DataView dv = GetData(objXmlDoc, Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.TASK_TASKS);

                Collection<TaskInfo> tasks = new Collection<TaskInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    try
                    {
                        TaskInfo task = new TaskInfo();
                        task = GetTask(dv.Table.Rows[ix][0].ToString(), dv.Table.Rows[ix][1].ToString(), false);
                        tasks.Add(task);
                    }
                    catch
                    {
                        continue;
                    }
                }

                return tasks;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetTasks", ex, LogSeverity.Fatal);
                throw;
            }
        }
        #endregion

        #region Task Tree
        public static string EditTask(TaskInfo task)
        {
            try
            {
                XmlDocument objXmlDoc = GetAssistantConfigFile();
                if (objXmlDoc == null)
                    return "配置文件不存在！";

                if (task.TaskId == null || task.TaskId == string.Empty)
                {
                    Random rd = new Random();
                    int num = rd.Next(1, 999999999);
                    DateTime now = DateTime.Now; //获取系统时间   
                    string taskId = now.Year.ToString() + now.Month.ToString() + now.Day.ToString() + now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString() + now.Millisecond.ToString() + "_" + num.ToString();

                    task.TaskId = taskId;
                    InsertTaskNode(objXmlDoc, task);
                }
                else
                {
                    EditTaskNode(objXmlDoc, task);
                }

                bool ret = SetAssistantConfigFile(objXmlDoc);
                if (ret)
                    return Constants.STATUS_SUCCESS;
                else
                    return Constants.STATUS_FAIL;
            }
            catch (Exception ex)
            {
                LogHelper.Write("编辑任务" + task.TaskName, ex);
                return Constants.STATUS_FAIL;
            }
        }

        public static string DeleteTask(TaskInfo task)
        {
            try
            {
                XmlDocument objXmlDoc = GetAssistantConfigFile();
                if (objXmlDoc == null)
                    return "配置文件不存在！";

                DeleteTaskNode(objXmlDoc, task);

                SetAssistantConfigFile(objXmlDoc);

                //delete task file
                string folder = Path.Combine(Application.StartupPath, Constants.FOLDER_TASKS);
                string taskFile = folder + Constants.CHAR_DOUBLEBACKSLASH + task.TaskId + ".xml";
                File.Delete(taskFile);

                return Constants.STATUS_SUCCESS;
            }
            catch (Exception ex)
            {
                LogHelper.Write("删除任务" + task.TaskName, ex);
                return Constants.STATUS_FAIL;
            }
        }
        private static void InsertTaskNode(XmlDocument xmldoc, TaskInfo task)
        {
            XmlNode objTasksNode = xmldoc.SelectSingleNode(Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.TASK_TASKS);
            XmlElement objChildNode = xmldoc.CreateElement(Constants.TASK_TASK);
            objTasksNode.AppendChild(objChildNode);
            XmlElement emtTaskId = xmldoc.CreateElement(Constants.TASK_TASKID);
            emtTaskId.InnerText = task.TaskId;
            XmlElement emtTaskName = xmldoc.CreateElement(Constants.TASK_TASKNAME);
            emtTaskName.InnerText = task.TaskName;
            objChildNode.AppendChild(emtTaskId);
            objChildNode.AppendChild(emtTaskName);
        }

        private static void EditTaskNode(XmlDocument xmldoc, TaskInfo task)
        {
            XmlNode objTasksNode = xmldoc.SelectSingleNode(Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.TASK_TASKS);
            foreach (XmlNode xn in objTasksNode.ChildNodes)
            {
                if (xn.SelectSingleNode(Constants.TASK_TASKID).InnerText == task.TaskId)
                {
                    xn.SelectSingleNode(Constants.TASK_TASKNAME).InnerText = task.TaskName;
                    if (xn.SelectSingleNode(Constants.TASK_GROUPNAME) == null)
                    {
                        XmlElement objGroupNameNode = xmldoc.CreateElement(Constants.TASK_GROUPNAME);
                        xn.AppendChild(objGroupNameNode);
                    }
                    xn.SelectSingleNode(Constants.TASK_GROUPNAME).InnerText = task.GroupName;
                    break;
                }
            }
        }

        private static void DeleteTaskNode(XmlDocument xmldoc, TaskInfo task)
        {
            XmlNode objTasksNode = xmldoc.SelectSingleNode(Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.TASK_TASKS);
            foreach (XmlNode xn in objTasksNode.ChildNodes)
            {
                if (xn.SelectSingleNode(Constants.TASK_TASKID).InnerText == task.TaskId)
                {
                    objTasksNode.RemoveChild(xn);
                    break;
                }
            }
        }

        private static bool IsExistTaskNode(XmlDocument xmldoc, TaskInfo task)
        {
            XmlNode objTasksNode = xmldoc.SelectSingleNode(Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.TASK_TASKS);
            foreach (XmlNode xn in objTasksNode.ChildNodes)
            {
                if (xn.SelectSingleNode(Constants.TASK_TASKID).InnerText == task.TaskId)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region GetTaskConfigFile
        private static XmlDocument GetTaskConfigFile(string taskid, string taskname)
        {
            try
            {
                //load config info
                string folder = Path.Combine(Application.StartupPath, Constants.FOLDER_TASKS);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                string configFile = folder + Constants.CHAR_DOUBLEBACKSLASH + taskid + ".xml";
                if (!File.Exists(configFile))
                {
                    string configContent = Utility.GetTaskConfig(taskid, taskname);
                    StreamWriter sw = new StreamWriter(configFile);
                    sw.Write(configContent);
                    sw.Close();
                    sw = null;
                }

                XmlDocument objXmlDoc = new XmlDocument();

                objXmlDoc.Load(configFile);

                return objXmlDoc;
            }
            catch (Exception ex)
            {
                LogHelper.Write("读取任务配置文件" + taskname, ex);
                throw;
            }
        }
        #endregion

        #region SetTaskConfigFile
        private static bool SetTaskConfigFile(XmlDocument xmldoc, string taskid)
        {
            try
            {
                string folder = Path.Combine(Application.StartupPath, Constants.FOLDER_TASKS);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                string configFile = folder + Constants.CHAR_DOUBLEBACKSLASH + taskid + ".xml";
                xmldoc.Save(configFile);
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Write("保存任务配置文件" + taskid, ex);
                return false;
            }
        }
        #endregion

        #region GetTask
        public static TaskInfo GetTask(string taskid, string taskname, bool IsDefault)
        {
            try
            {
                XmlDocument objXmlDoc;
                TaskInfo task = new TaskInfo();

                if (IsDefault)
                {
                    string configContent = Utility.GetTaskConfig("default", "defaultpassword");

                    objXmlDoc = new XmlDocument();

                    objXmlDoc.LoadXml(configContent);
                }
                else
                {
                    if (taskid == null || taskid == string.Empty)
                        return null;

                    objXmlDoc = GetTaskConfigFile(taskid, taskname);
                    if (objXmlDoc == null)
                        return null;

                    task.TaskId = taskid;
                    task.TaskName = taskname;
                }

                //root node
                XmlNode objTaskNode = objXmlDoc.SelectSingleNode(Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.TASK_TASK);
                if (objTaskNode == null)
                    return null;

                XmlNode objNode;
                DataView dv;
                /*------------------------------ExecutingMode-----------------------------------*/
                objNode = objTaskNode.SelectSingleNode(Constants.TASK_NODE_EXECUTINGMODE);
                if (objNode != null)
                {
                    task.RunMode = GetRunMode(DataConvert.GetString(objNode.SelectSingleNode(Constants.TASK_EXECUTINGMODE_RUNMODE).InnerText));
                    XmlNode nodeRunInLoop = objNode.SelectSingleNode(Constants.TASK_EXECUTINGMODE_RUNINLOOP);
                    task.RoundTime = DataConvert.GetInt32(nodeRunInLoop.SelectSingleNode(Constants.TASK_RUNINLOOP_ROUNDTIME).InnerText);
                    task.Forbidden = DataConvert.GetBool(nodeRunInLoop.SelectSingleNode(Constants.TASK_RUNINLOOP_FOBIDDEN).InnerText);
                    string strtime = DataConvert.GetString(nodeRunInLoop.SelectSingleNode(Constants.TASK_RUNINLOOP_FOBIDDENSTART).InnerText);
                    string[] atime = strtime.Split(':');
                    if (strtime == string.Empty)
                        task.ForbiddenStart = new TimeInfo();
                    else
                    {
                        task.ForbiddenStart = new TimeInfo(DataConvert.GetInt32(atime[0]), DataConvert.GetInt32(atime[1]));
                    }
                    strtime = DataConvert.GetString(nodeRunInLoop.SelectSingleNode(Constants.TASK_RUNINLOOP_FOBIDDENEND).InnerText);
                    atime = strtime.Split(':');
                    if (strtime == string.Empty)
                        task.ForbiddenEnd = new TimeInfo();
                    else
                    {
                        task.ForbiddenEnd = new TimeInfo(DataConvert.GetInt32(atime[0]), DataConvert.GetInt32(atime[1]));
                    }
                    //StartTimes
                    dv = GetData(objXmlDoc, Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.TASK_TASK + Constants.CHAR_SLASH + Constants.TASK_NODE_EXECUTINGMODE + Constants.CHAR_SLASH + Constants.TASK_EXECUTINGMODE_RUNINTIME + Constants.CHAR_SLASH + Constants.TASK_RUNINTIME_STARTTIMES);

                    Collection<TimeInfo> starttimes = new Collection<TimeInfo>();

                    for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                    {
                        string strTime = dv.Table.Rows[ix][0].ToString();
                        string[] temp = strTime.Split(':');
                        starttimes.Add(new TimeInfo(DataConvert.GetInt32(temp[0]), DataConvert.GetInt32(temp[1])));
                    }
                    task.StartTimes = starttimes;
                }
                /*------------------------------ExecutingMode-----------------------------------*/

                /*------------------------------Park-----------------------------------*/
                objNode = objTaskNode.SelectSingleNode(Constants.GAME_PARK);
                if (objNode != null)
                {
                    task.ExecutePark = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_PARK_EXECUTEPARK).InnerText);
                    task.ParkMyCars = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_PARK_PARKMYCARS).InnerText);
                    task.PostOthersCars = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_PARK_POSTOTHERSCARS).InnerText);
                    task.JoinMatch = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_PARK_JOINMATCH).InnerText);
                    task.OriginateMatch = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_PARK_ORIGINATEMATCH).InnerText);
                    task.OriginateMatchId = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_PARK_ORIGINATEMATCHID, "1").InnerText);
                    task.OriginateTeamNum = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_PARK_ORIGINATETEAMNUM, "2").InnerText);
                    task.StartCar = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_PARK_STARTCAR).InnerText);
                    task.CheerUp = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_PARK_CHEERUP).InnerText);
                    string strcartime = DataConvert.GetString(GetAppendNode(objNode, objXmlDoc, Constants.TASK_PARK_STARTCARTIME, "8:30").InnerText);
                    string[] atime = strcartime.Split(':');
                    task.StartCarTime = new TimeInfo(DataConvert.GetInt32(atime[0]), DataConvert.GetInt32(atime[1]));
                }
                /*------------------------------Park-----------------------------------*/

                /*------------------------------Bite-----------------------------------*/
                objNode = objTaskNode.SelectSingleNode(Constants.GAME_BITE);
                if (objNode != null)
                {
                    task.ExecuteBite = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_BITE_EXECUTEBITE).InnerText);
                    task.ApproveRecovery = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_BITE_APPROVERECOVERY).InnerText);
                    task.BiteOthers = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_BITE_BITEOTHERS).InnerText);
                    task.AutoRecover = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_BITE_AUTORECOVER).InnerText);
                    if (objNode.SelectSingleNode(Constants.TASK_BITE_PROTECTFRIEND) != null)
                        task.ProtectFriend = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_BITE_PROTECTFRIEND).InnerText);
                    else
                        task.ProtectFriend = false;
                }
                /*------------------------------Bite-----------------------------------*/

                /*------------------------------Slave-----------------------------------*/
                objNode = objTaskNode.SelectSingleNode(Constants.GAME_SLAVE);
                if (objNode != null)
                {
                    task.ExecuteSlave = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_SLAVE_EXECUTESLAVE).InnerText);
                    task.MaxSlaves = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.TASK_SLAVE_MAXSLAVES).InnerText);
                    task.NickName = DataConvert.GetString(objNode.SelectSingleNode(Constants.TASK_SLAVE_NICKNAME).InnerText);
                    task.BuySlave = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_SLAVE_BUYSLAVE).InnerText);
                    task.BuyLowPriceSlave = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_SLAVE_BUYLOWPRICESLAVE).InnerText);
                    task.FawnMaster = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_SLAVE_FAWNMASTER).InnerText);
                    task.PropitiateSlave = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_SLAVE_PROPITIATESLAVE).InnerText);
                    task.AfflictSlave = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_SLAVE_AFFLICTSLAVE).InnerText);
                    task.ReleaseSlave = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_SLAVE_RELEASESLAVE).InnerText);
                }
                /*------------------------------Slave-----------------------------------*/

                /*------------------------------House-----------------------------------*/
                objNode = objTaskNode.SelectSingleNode(Constants.GAME_HOUSE);
                if (objNode != null)
                {
                    task.ExecuteHouse = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_HOUSE_EXECUTEHOUSE).InnerText);
                    task.DoJob = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_HOUSE_DOJOB).InnerText);
                    task.StayHouse = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_HOUSE_STAYHOUSE).InnerText);
                    task.RobFriends = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_HOUSE_ROBFRIENDS).InnerText);
                    task.RobFreeFriends = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_HOUSE_ROBFREEFRIENDS).InnerText);
                    task.DriveFriends = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_HOUSE_DRIVEFRIENDS).InnerText);                    
                }
                /*------------------------------House-----------------------------------*/

                /*------------------------------Garden-----------------------------------*/
                objNode = objTaskNode.SelectSingleNode(Constants.GAME_GARDEN);                
                if (objNode != null)
                {
                    task.ExecuteGarden = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_GARDEN_EXECUTEGARDEN).InnerText);                    
                    task.FarmSelf = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_GARDEN_FARMSELF).InnerText);
                    task.ExpensiveFarmSelf = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_GARDEN_EXPENSIVEFARMSELF).InnerText);
                    task.CustomFarmSelf = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.TASK_GARDEN_CUSTOMFARMSELF).InnerText);
                    task.FarmShared = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_GARDEN_FARMSHARED).InnerText);
                    task.ExpensiveFarmShared = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_GARDEN_EXPENSIVEFARMSHARED).InnerText);
                    task.CustomFarmShared = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.TASK_GARDEN_CUSTOMFARMSHARED).InnerText);
                    task.HarvestFruit = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_GARDEN_HARVESTFRUIT).InnerText);
                    task.BuySeed = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_GARDEN_BUYSEED).InnerText);
                    task.BuySeedCount = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.TASK_GARDEN_BUYSEEDCOUNT).InnerText);
                    task.HelpOthers = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_GARDEN_HELPOTHERS).InnerText);                    
                    task.StealFruit = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_GARDEN_STEALFRUIT).InnerText);
                    task.PresentFruit = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_PRESENTFRUIT, "False").InnerText);
                    task.PresentFruitByPrice = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_PRESENTFRUITBYPRICE, "True").InnerText);
                    task.PresentFruitCheckValue = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_PRESENTFRUITCHECKVALUE, "True").InnerText);
                    task.PresentFruitValue = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_PRESENTFRUITVALUE, "100").InnerText);
                    task.PresentFruitId = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_PRESENTFRUITID, "11").InnerText);
                    task.PresentFruitCheckNum = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_PRESENTFRUITCHECKNUM, "True").InnerText);
                    task.PresentFruitNum = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_PRESENTFRUITNUM, "1000").InnerText);
                    task.SellFruit = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_SELLFRUIT, "False").InnerText);
                    task.LowCash = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_LOWCASH, "True").InnerText);
                    task.LowCashLimit = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_LOWCASHLIMIT, "100").InnerText);
                    task.SellAllFruit = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_SELLALLFRUIT, "False").InnerText);
                    task.MaxSellLimit = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_MAXSELLLIMIT, "300").InnerText);
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_SELLFORBIDDENNFRUITSLIST, "");
                    dv = GetData(objXmlDoc, Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.TASK_TASK + Constants.CHAR_SLASH + Constants.GAME_GARDEN + Constants.CHAR_SLASH + Constants.TASK_GARDEN_SELLFORBIDDENNFRUITSLIST);
                    Collection<int> sellfruits = new Collection<int>();
                    for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                    {
                        sellfruits.Add(DataConvert.GetInt32(dv.Table.Rows[ix][0]));
                    }
                    task.SellForbiddennFruitsList = sellfruits;
                    task.SowMySeedsFirst = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_SOWMYSEEDSFIRST, "False").InnerText);
                    task.StealUnknowFruit = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_STEALUNKNOWFRUIT, "True").InnerText);

                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_STEALFORBIDDENFRUITSLIST, "");
                    dv = GetData(objXmlDoc, Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.TASK_TASK + Constants.CHAR_SLASH + Constants.GAME_GARDEN + Constants.CHAR_SLASH + Constants.TASK_GARDEN_STEALFORBIDDENFRUITSLIST);
                    Collection<int> fruits = new Collection<int>();

                    for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                    {
                        fruits.Add(DataConvert.GetInt32(dv.Table.Rows[ix][0]));
                    }
                    task.StealForbiddenFruitsList = fruits;
                }
                /*------------------------------Garden-----------------------------------*/

                /*------------------------------Ranch-----------------------------------*/
                objNode = GetAppendNode(objTaskNode, objXmlDoc, Constants.GAME_RANCH, "");                
                if (objNode != null)
                {
                    task.ExecuteRanch = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_EXECUTERANCH, "True").InnerText);
                    task.HarvestProduct = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_HARVESTPRODUCT, "True").InnerText);
                    task.HarvestAnimal = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_HARVESTANIMAL, "True").InnerText);
                    task.AddWater = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_ADDWATER, "True").InnerText);
                    task.HelpAddWater = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_HELPADDWATER, "False").InnerText);
                    task.AddGrass = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_ADDGRASS, "True").InnerText);
                    task.HelpAddGrass = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_HELPADDGRASS, "False").InnerText);
                    task.BuyCalf = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_BUYCALF, "True").InnerText);
                    task.BuyCalfByPrice = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_BUYCALFBYPRICE, "True").InnerText);
                    task.BuyCalfCustom = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_BUYCALFCUSTOM, "1").InnerText);
                    task.StealProduct = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_STEALPRODUCT, "False").InnerText);
                    task.MakeProduct = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_MAKEPRODUCT, "True").InnerText);
                    task.HelpMakeProduct = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_HELPMAKEPRODUCT, "False").InnerText);
                    task.BreedAnimal = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_BREEDANIMAL, "False").InnerText);
                    task.FoodNum = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_FOODNUM, "200").InnerText);
                    task.PresentProduct = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_PRESENTPRODUCT, "False").InnerText);
                    task.PresentProductByPrice = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_PRESENTPRODUCTBYPRICE, "True").InnerText);
                    task.PresentProductCheckValue = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_PRESENTPRODUCTCHECKVALUE, "True").InnerText);
                    task.PresentProductValue = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_PRESENTPRODUCTVALUE, "100").InnerText);
                    task.PresentProductAid = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_PRESENTPRODUCTAID, "1").InnerText);
                    task.PresentProductType = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_PRESENTPRODUCTTYPE, "0").InnerText);
                    task.PresentProductCheckNum = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_PRESENTPRODUCTCHECKNUM, "True").InnerText);
                    task.PresentProductNum = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_PRESENTPRODUCTNUM, "100").InnerText);
                    task.SellProduct = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_SELLPRODUCT, "False").InnerText);
                    task.SellProductLowCash = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_SELLPRODUCTLOWCASH, "True").InnerText);
                    task.SellProductLowCashLimit = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_SELLPRODUCTLOWCASHLIMIT, "100").InnerText);
                    task.SellAllProducts = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_SELLALLPRODUCTS, "False").InnerText);
                    task.SellProductMaxLimit = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_SELLPRODUCTMAXLIMIT, "300").InnerText);
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_SELLPRODUCTFORBIDDENLIST, "");
                    dv = GetData(objXmlDoc, Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.TASK_TASK + Constants.CHAR_SLASH + Constants.GAME_RANCH + Constants.CHAR_SLASH + Constants.TASK_RANCH_SELLPRODUCTFORBIDDENLIST);
                    Collection<ProductInfo> sellproducts = new Collection<ProductInfo>();
                    for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                    {
                        ProductInfo product = new ProductInfo();
                        product.Aid = DataConvert.GetInt32(dv.Table.Rows[ix][0]);
                        product.Type = DataConvert.GetInt32(dv.Table.Rows[ix][1]);
                        sellproducts.Add(product);
                    }
                    task.SellProductForbiddenList = sellproducts;
                    task.AddCarrot = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_ADDCARROT, "True").InnerText);
                    task.HelpAddCarrot = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_HELPADDCARROT, "False").InnerText);
                    task.CarrotNum = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_CARROTNUM, "200").InnerText);
                    task.AddBamboo = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_ADDBAMBOO, "True").InnerText);
                    task.HelpAddBamboo = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_HELPADDBAMBOO, "False").InnerText);
                    task.BambooNum = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_BAMBOONUM, "200").InnerText);

                }
                /*------------------------------Ranch-----------------------------------*/

                /*------------------------------Fish-----------------------------------*/
                objNode = GetAppendNode(objTaskNode, objXmlDoc, Constants.GAME_FISH, "");
                if (objNode != null)
                {
                    task.ExecuteFish = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_EXECUTEFISH, "True").InnerText);
                    task.Shake = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_SHAKE, "True").InnerText);
                    task.TreatFish = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_TREATFISH, "True").InnerText);
                    task.UpdateFishPond = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_UPDATEFISHPOND, "True").InnerText);
                    task.BangKeJing = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_BANGKEJING, "True").InnerText);
                    task.BuyFish = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_BUYFISH, "True").InnerText);
                    task.MaxFishes = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_MAXFISHES, "20").InnerText);
                    task.BuyFishByRank = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_BUYFISHBYRANK, "True").InnerText);
                    task.BuyFishFishId = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_BUYFISHFISHID, "1").InnerText);
                    task.Fishing = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_FISHING, "True").InnerText);
                    task.BuyUpdateTackle = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_BUYUPDATETACKLE, "False").InnerText);
                    task.MaxTackles = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_MAXTACKLES, "5").InnerText);
                    task.HarvestFish = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_HARVESTFISH, "True").InnerText);
                    task.NetSelfFish = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_NETSELFFISH, "False").InnerText);
                    task.NetSelfFishCheap = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_NETSELFFISHCHEAP, "False").InnerText);
                    task.NetSelfFishMature = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_NETSELFFISHMATURE, "80").InnerText);
                    task.HelpFish = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_HELPFISH, "True").InnerText);
                    task.PresentFish = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_PRESENTFISH, "False").InnerText);
                    task.PresentFishCheap = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_PRESENTFISHCHEAP, "False").InnerText);
                    task.PresentFishCheckValue = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_PRESENTFISHCHECKVALUE, "True").InnerText);
                    task.PresentFishValue = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_PRESENTFISHVALUE, "10000").InnerText);
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_PRESENTFISHFORBIDDENLIST, "");
                    dv = GetData(objXmlDoc, Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.TASK_TASK + Constants.CHAR_SLASH + Constants.GAME_FISH + Constants.CHAR_SLASH + Constants.TASK_FISH_PRESENTFISHFORBIDDENLIST);
                    Collection<int> presentfishes = new Collection<int>();

                    for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                    {
                        presentfishes.Add(DataConvert.GetInt32(dv.Table.Rows[ix][0]));
                    }
                    task.PresentFishForbiddenList = presentfishes;
                    task.SellFish = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_SELLFISH, "False").InnerText);
                    task.SellFishLowCash = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_SELLFISHLOWCASH, "False").InnerText);
                    task.SellFishLowCashLimit = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_SELLFISHLOWCASHLIMIT, "10").InnerText);
                    task.SellAllFish = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_SELLALLFISH, "False").InnerText);
                    task.SellFishCheckValue = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_SELLFISHCHECKVALUE, "False").InnerText);
                    task.SellFishValue = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_SELLFISHVALUE, "10000").InnerText);
                    task.SellFishMaxLimit = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_SELLFISHMAXLIMIT, "20").InnerText);
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_SELLFISHFORBIDDENLIST, "");
                    dv = GetData(objXmlDoc, Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.TASK_TASK + Constants.CHAR_SLASH + Constants.GAME_FISH + Constants.CHAR_SLASH + Constants.TASK_FISH_SELLFISHFORBIDDENLIST);
                    Collection<int> sellfishes = new Collection<int>();

                    for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                    {
                        sellfishes.Add(DataConvert.GetInt32(dv.Table.Rows[ix][0]));
                    }
                    task.SellFishForbiddenList = sellfishes;
                }
                /*------------------------------Fish-----------------------------------*/

                /*------------------------------Rich-----------------------------------*/
                objNode = GetAppendNode(objTaskNode, objXmlDoc, Constants.GAME_RICH, "");
                if (objNode != null)
                {
                    task.ExecuteRich = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RICH_EXECUTERICH, "True").InnerText);
                    task.SellAsset = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RICH_SELLASSET, "True").InnerText);
                    task.BuyAsset = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RICH_BUYASSET, "True").InnerText);
                    task.BuyAssetCheap = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RICH_BUYASSETCHEAP, "False").InnerText);
                    task.GiveUpIfRatio = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RICH_GIVEUPIFRATIO, "True").InnerText);
                    task.GiveUpRatio = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RICH_GIVEUPRATIO, "50").InnerText);
                    task.GiveUpIfMinimum = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RICH_GIVEUPIFMINIMUM, "True").InnerText);
                    task.GiveUpMinimum = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RICH_GIVEUPMINIMUM, "5").InnerText);
                    task.GiveUpIfMyAsset = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RICH_GIVEUPIFMYASSET, "False").InnerText);
                    task.GiveUpAssetCount = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RICH_GIVEUPASSETCOUNT, "3").InnerText);                    
                    task.AdvancedPurchase = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_RICH_ADVANCEDPURCHASE, "False").InnerText);
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RICH_BUYASSETSLIST, "");
                    dv = GetData(objXmlDoc, Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.TASK_TASK + Constants.CHAR_SLASH + Constants.GAME_RICH + Constants.CHAR_SLASH + Constants.TASK_RICH_BUYASSETSLIST);
                    Collection<int> buyassets = new Collection<int>();

                    for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                    {
                        buyassets.Add(DataConvert.GetInt32(dv.Table.Rows[ix][0]));
                    }
                    task.BuyAssetsList = buyassets;
                }
                /*------------------------------Rich-----------------------------------*/

                /*------------------------------Cafe-----------------------------------*/
                objNode = GetAppendNode(objTaskNode, objXmlDoc, Constants.GAME_CAFE, "");
                if (objNode != null)
                {
                    task.ExecuteCafe = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_EXECUTECAFE, "True").InnerText);
                    task.BoxClean = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_BOXCLEAN, "True").InnerText);
                    task.Cook = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_COOK, "True").InnerText);
                    task.CookTomatoFirst = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_COOKTOMATOFIRST, "True").InnerText);
                    task.CookMedlarFirst = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_COOKMEDLARFIRST, "False").InnerText);
                    task.CookCrabFirst = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_COOKCRABFIRST, "False").InnerText);
                    task.CookPineappleFirst = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_COOKPINEAPPLEFIRST, "False").InnerText);
                    task.CookDishId = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_COOKDISHID, "4").InnerText);
                    task.CookLowCash = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_COOKLOWCASH, "True").InnerText);
                    task.CookLowCashLimit = DataConvert.GetInt64(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_COOKLOWCASHLIMIT, "2000").InnerText);
                    task.Hire = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_HIRE, "True").InnerText);
                    task.MaxEmployees = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_MAXEMPLOYEES, "12").InnerText);
                    task.HelpFriend = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_HELPFRIEND, "True").InnerText);
                    task.PresentFood = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_PRESENTFOOD, "False").InnerText);
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_PRESENTFORBIDDENFOODLIST, "");
                    dv = GetData(objXmlDoc, Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.TASK_TASK + Constants.CHAR_SLASH + Constants.GAME_CAFE + Constants.CHAR_SLASH + Constants.TASK_CAFE_PRESENTFORBIDDENFOODLIST);
                    Collection<int> presentdishes = new Collection<int>();
                    for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                    {
                        presentdishes.Add(DataConvert.GetInt32(dv.Table.Rows[ix][0]));
                    }
                    task.PresentForbiddenFoodList = presentdishes;
                    task.PresentFoodByCount = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_PRESENTFOODBYCOUNT, "True").InnerText);
                    task.PresentFoodDishId = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_PRESENTFOODDISHID, "4").InnerText);
                    task.PresentFoodMessage = DataConvert.GetString(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_PRESENTFOODMESSAGE, "送你食物啦！").InnerText);
                    task.PresentFoodRatio = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_PRESENTFOODRATIO, "50").InnerText);
                    task.PresentLowCash = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_PRESENTLOWCASH, "True").InnerText);
                    task.PresentLowCashLimit = DataConvert.GetInt64(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_PRESENTLOWCASHLIMIT, "2000").InnerText);
                    task.PresentFoodLowCount = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_PRESENTFOODLOWCOUNT, "True").InnerText);
                    task.PresentFoodLowCountLimit = DataConvert.GetInt32(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_PRESENTFOODLOWCOUNTLIMIT, "2").InnerText);
                    task.PurchaseFood = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_PURCHASEFOOD, "False").InnerText);
                    task.PurchaseFoodByRefPrice = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_PURCHASEFOODBYREFPRICE, "True").InnerText);
                    task.SellFood = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_SELLFOOD, "False").InnerText);
                    task.SellFoodByRefPrice = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_SELLFOODBYREFPRICE, "True").InnerText);
                }
                /*------------------------------Cafe-----------------------------------*/

                /*------------------------------Miscellaneous-----------------------------------*/
                objNode = objTaskNode.SelectSingleNode(Constants.TASK_NODE_MISCELLANEOUS);
                if (objNode != null)
                {
                    task.GroupName = DataConvert.GetString(objNode.SelectSingleNode(Constants.TASK_MISCELLANEOUS_GROUP).InnerText);
                    task.SendLog = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_MISCELLANEOUS_SENDLOG).InnerText);                    
                    task.ReceiverEmail = DataConvert.GetString(objNode.SelectSingleNode(Constants.TASK_MISCELLANEOUS_RECEIVEREMAIL).InnerText);
                    if (objNode.SelectSingleNode(Constants.TASK_MISCELLANEOUS_WRITELOGTOFILE) != null)
                        task.WriteLogToFile = DataConvert.GetBool(objNode.SelectSingleNode(Constants.TASK_MISCELLANEOUS_WRITELOGTOFILE).InnerText);
                    else
                        task.WriteLogToFile = false;
                    task.SkipValidation = DataConvert.GetBool(GetAppendNode(objNode, objXmlDoc, Constants.TASK_MISCELLANEOUS_SKIPVALIDATION, "False").InnerText);
                }
                /*------------------------------Miscellaneous-----------------------------------*/

                /*------------------------------Accounts-----------------------------------*/
                dv = GetData(objXmlDoc, Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.TASK_TASK + Constants.CHAR_SLASH + Constants.ACCOUNT_ACCOUNTS);

                Collection<AccountInfo> accounts = new Collection<AccountInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    AccountInfo account = new AccountInfo();
                    account.Email = dv.Table.Rows[ix][0].ToString();
                    account.Password = dv.Table.Rows[ix][1].ToString();
                    account.UserName = dv.Table.Rows[ix][2].ToString();
                    account.UserId = dv.Table.Rows[ix][3].ToString();
                    account.Gender = DataConvert.GetBool(dv.Table.Rows[ix][4]);
                    accounts.Add(account);
                }
                task.Accounts = accounts;
                /*------------------------------Accounts-----------------------------------*/

                return task;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetTask", taskname + "(" + taskid.ToString() + ")", ex, LogSeverity.Fatal);
                throw;
            }
        }
        #endregion

        #region SetTask
        public static bool SetTask(string taskid, string taskname, TaskInfo taskitem)
        {
            try
            {
                if (taskid == null || taskid == string.Empty)
                    return false;

                XmlDocument objXmlDoc = GetTaskConfigFile(taskid, taskname);
                if (objXmlDoc == null)
                    return false;

                //root node
                XmlNode objTaskNode = objXmlDoc.SelectSingleNode(Constants.CONFIG_ROOT + Constants.CHAR_SLASH + Constants.TASK_TASK);
                if (objTaskNode == null)
                    return false;

                XmlNode objNode;
                /*------------------------------ExecutingMode-----------------------------------*/
                objNode = objTaskNode.SelectSingleNode(Constants.TASK_NODE_EXECUTINGMODE);
                if (objNode != null)
                {
                    if (taskitem.RunMode == EnumRunMode.SingleLoop)
                        objNode.SelectSingleNode(Constants.TASK_EXECUTINGMODE_RUNMODE).InnerText = "SingleLoop";
                    else if (taskitem.RunMode == EnumRunMode.MultiLoop)
                        objNode.SelectSingleNode(Constants.TASK_EXECUTINGMODE_RUNMODE).InnerText = "MultiLoop";
                    else
                        objNode.SelectSingleNode(Constants.TASK_EXECUTINGMODE_RUNMODE).InnerText = "Timing";
                    XmlNode objRunInLoop = objNode.SelectSingleNode(Constants.TASK_EXECUTINGMODE_RUNINLOOP);
                    objRunInLoop.SelectSingleNode(Constants.TASK_RUNINLOOP_ROUNDTIME).InnerText = taskitem.RoundTime.ToString();
                    objRunInLoop.SelectSingleNode(Constants.TASK_RUNINLOOP_FOBIDDEN).InnerText = taskitem.Forbidden.ToString();
                    objRunInLoop.SelectSingleNode(Constants.TASK_RUNINLOOP_FOBIDDENSTART).InnerText = taskitem.ForbiddenStart.Hour + ":" + taskitem.ForbiddenStart.Minute;
                    objRunInLoop.SelectSingleNode(Constants.TASK_RUNINLOOP_FOBIDDENEND).InnerText = taskitem.ForbiddenEnd.Hour + ":" + taskitem.ForbiddenEnd.Minute;
                    //StartTimes
                    XmlNode objStartTimesNode = objNode.SelectSingleNode(Constants.TASK_EXECUTINGMODE_RUNINTIME + Constants.CHAR_SLASH + Constants.TASK_RUNINTIME_STARTTIMES);
                    if (objStartTimesNode == null)
                        return false;

                    objStartTimesNode.RemoveAll();
                    foreach (TimeInfo time in taskitem.StartTimes)
                    {
                        XmlElement objChildNode = objXmlDoc.CreateElement(Constants.TASK_RUNINTIME_DATETIME);
                        objChildNode.InnerText = time.Hour + ":" + time.Minute;
                        objStartTimesNode.AppendChild(objChildNode);
                    }
                }
                /*------------------------------ExecutingMode-----------------------------------*/

                /*------------------------------Park-----------------------------------*/
                objNode = objTaskNode.SelectSingleNode(Constants.GAME_PARK);
                if (objNode != null)
                {
                    objNode.SelectSingleNode(Constants.TASK_PARK_EXECUTEPARK).InnerText = taskitem.ExecutePark.ToString();
                    objNode.SelectSingleNode(Constants.TASK_PARK_PARKMYCARS).InnerText = taskitem.ParkMyCars.ToString();
                    objNode.SelectSingleNode(Constants.TASK_PARK_POSTOTHERSCARS).InnerText = taskitem.PostOthersCars.ToString();
                    objNode.SelectSingleNode(Constants.TASK_PARK_JOINMATCH).InnerText = taskitem.JoinMatch.ToString();
                    objNode.SelectSingleNode(Constants.TASK_PARK_ORIGINATEMATCH).InnerText = taskitem.OriginateMatch.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_PARK_ORIGINATEMATCHID, "1").InnerText = taskitem.OriginateMatchId.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_PARK_ORIGINATETEAMNUM, "2").InnerText = taskitem.OriginateTeamNum.ToString();
                    objNode.SelectSingleNode(Constants.TASK_PARK_STARTCAR).InnerText = taskitem.StartCar.ToString();
                    objNode.SelectSingleNode(Constants.TASK_PARK_CHEERUP).InnerText = taskitem.CheerUp.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_PARK_STARTCARTIME, "8:30").InnerText = taskitem.StartCarTime.Hour + ":" + taskitem.StartCarTime.Minute;
                }
                /*------------------------------Park-----------------------------------*/

                /*------------------------------Bite-----------------------------------*/
                objNode = objTaskNode.SelectSingleNode(Constants.GAME_BITE);
                if (objNode != null)
                {
                    objNode.SelectSingleNode(Constants.TASK_BITE_EXECUTEBITE).InnerText = taskitem.ExecuteBite.ToString();
                    objNode.SelectSingleNode(Constants.TASK_BITE_APPROVERECOVERY).InnerText = taskitem.ApproveRecovery.ToString();
                    objNode.SelectSingleNode(Constants.TASK_BITE_BITEOTHERS).InnerText = taskitem.BiteOthers.ToString();
                    objNode.SelectSingleNode(Constants.TASK_BITE_AUTORECOVER).InnerText = taskitem.AutoRecover.ToString();
                    if (objNode.SelectSingleNode(Constants.TASK_BITE_PROTECTFRIEND) == null)
                    {
                        XmlElement objChildNode = objXmlDoc.CreateElement(Constants.TASK_BITE_PROTECTFRIEND);
                        objNode.AppendChild(objChildNode);
                    }
                    objNode.SelectSingleNode(Constants.TASK_BITE_PROTECTFRIEND).InnerText = taskitem.ProtectFriend.ToString();
                }
                /*------------------------------Bite-----------------------------------*/

                /*------------------------------Slave-----------------------------------*/
                objNode = objTaskNode.SelectSingleNode(Constants.GAME_SLAVE);
                if (objNode != null)
                {
                    objNode.SelectSingleNode(Constants.TASK_SLAVE_EXECUTESLAVE).InnerText = taskitem.ExecuteSlave.ToString();
                    objNode.SelectSingleNode(Constants.TASK_SLAVE_MAXSLAVES).InnerText = taskitem.MaxSlaves.ToString();
                    objNode.SelectSingleNode(Constants.TASK_SLAVE_NICKNAME).InnerText = taskitem.NickName;
                    objNode.SelectSingleNode(Constants.TASK_SLAVE_BUYSLAVE).InnerText = taskitem.BuySlave.ToString();
                    objNode.SelectSingleNode(Constants.TASK_SLAVE_BUYLOWPRICESLAVE).InnerText = taskitem.BuyLowPriceSlave.ToString();
                    objNode.SelectSingleNode(Constants.TASK_SLAVE_FAWNMASTER).InnerText = taskitem.FawnMaster.ToString();
                    objNode.SelectSingleNode(Constants.TASK_SLAVE_PROPITIATESLAVE).InnerText = taskitem.PropitiateSlave.ToString();
                    objNode.SelectSingleNode(Constants.TASK_SLAVE_AFFLICTSLAVE).InnerText = taskitem.AfflictSlave.ToString();
                    objNode.SelectSingleNode(Constants.TASK_SLAVE_RELEASESLAVE).InnerText = taskitem.ReleaseSlave.ToString();
                }
                /*------------------------------Slave-----------------------------------*/

                /*------------------------------House-----------------------------------*/
                objNode = objTaskNode.SelectSingleNode(Constants.GAME_HOUSE);
                if (objNode != null)
                {
                    objNode.SelectSingleNode(Constants.TASK_HOUSE_EXECUTEHOUSE).InnerText = taskitem.ExecuteHouse.ToString();
                    objNode.SelectSingleNode(Constants.TASK_HOUSE_DOJOB).InnerText = taskitem.DoJob.ToString();
                    objNode.SelectSingleNode(Constants.TASK_HOUSE_STAYHOUSE).InnerText = taskitem.StayHouse.ToString();
                    objNode.SelectSingleNode(Constants.TASK_HOUSE_ROBFRIENDS).InnerText = taskitem.RobFriends.ToString();
                    objNode.SelectSingleNode(Constants.TASK_HOUSE_ROBFREEFRIENDS).InnerText = taskitem.RobFreeFriends.ToString();
                    objNode.SelectSingleNode(Constants.TASK_HOUSE_DRIVEFRIENDS).InnerText = taskitem.DriveFriends.ToString();                    
                }
                /*------------------------------House-----------------------------------*/

                /*------------------------------Garden-----------------------------------*/
                objNode = objTaskNode.SelectSingleNode(Constants.GAME_GARDEN);                
                if (objNode != null)
                {
                    objNode.SelectSingleNode(Constants.TASK_GARDEN_EXECUTEGARDEN).InnerText = taskitem.ExecuteGarden.ToString();
                    objNode.SelectSingleNode(Constants.TASK_GARDEN_FARMSELF).InnerText = taskitem.FarmSelf.ToString();
                    objNode.SelectSingleNode(Constants.TASK_GARDEN_EXPENSIVEFARMSELF).InnerText = taskitem.ExpensiveFarmSelf.ToString();
                    objNode.SelectSingleNode(Constants.TASK_GARDEN_CUSTOMFARMSELF).InnerText = taskitem.CustomFarmSelf.ToString();
                    objNode.SelectSingleNode(Constants.TASK_GARDEN_FARMSHARED).InnerText = taskitem.FarmShared.ToString();
                    objNode.SelectSingleNode(Constants.TASK_GARDEN_EXPENSIVEFARMSHARED).InnerText = taskitem.ExpensiveFarmShared.ToString();
                    objNode.SelectSingleNode(Constants.TASK_GARDEN_CUSTOMFARMSHARED).InnerText = taskitem.CustomFarmShared.ToString();                    
                    objNode.SelectSingleNode(Constants.TASK_GARDEN_HARVESTFRUIT).InnerText = taskitem.HarvestFruit.ToString();
                    objNode.SelectSingleNode(Constants.TASK_GARDEN_BUYSEED).InnerText = taskitem.BuySeed.ToString();
                    objNode.SelectSingleNode(Constants.TASK_GARDEN_BUYSEEDCOUNT).InnerText = taskitem.BuySeedCount.ToString();
                    objNode.SelectSingleNode(Constants.TASK_GARDEN_HELPOTHERS).InnerText = taskitem.HelpOthers.ToString();                    
                    objNode.SelectSingleNode(Constants.TASK_GARDEN_STEALFRUIT).InnerText = taskitem.StealFruit.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_PRESENTFRUIT, "False").InnerText = taskitem.PresentFruit.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_PRESENTFRUITBYPRICE, "True").InnerText = taskitem.PresentFruitByPrice.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_PRESENTFRUITCHECKVALUE, "True").InnerText = taskitem.PresentFruitCheckValue.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_PRESENTFRUITVALUE, "100").InnerText = taskitem.PresentFruitValue.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_PRESENTFRUITID, "11").InnerText = taskitem.PresentFruitId.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_PRESENTFRUITCHECKNUM, "True").InnerText = taskitem.PresentFruitCheckNum.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_PRESENTFRUITNUM, "1000").InnerText = taskitem.PresentFruitNum.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_SELLFRUIT, "True").InnerText = taskitem.SellFruit.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_LOWCASH, "True").InnerText = taskitem.LowCash.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_LOWCASHLIMIT, "100").InnerText = taskitem.LowCashLimit.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_SELLALLFRUIT, "False").InnerText = taskitem.SellAllFruit.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_MAXSELLLIMIT, "300").InnerText = taskitem.MaxSellLimit.ToString();
                    XmlNode objSellFobiddenFruitsNode = GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_SELLFORBIDDENNFRUITSLIST, ""); ;
                    if (objSellFobiddenFruitsNode == null)
                        return false;
                    objSellFobiddenFruitsNode.RemoveAll();
                    foreach (int fruitid in taskitem.SellForbiddennFruitsList)
                    {
                        XmlElement objFruitIdNode = objXmlDoc.CreateElement("fruitid");
                        objFruitIdNode.InnerText = fruitid.ToString();
                        objSellFobiddenFruitsNode.AppendChild(objFruitIdNode);
                    }
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_SOWMYSEEDSFIRST, "False").InnerText = taskitem.SowMySeedsFirst.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_STEALUNKNOWFRUIT, "True").InnerText = taskitem.StealUnknowFruit.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_GARDEN_STEALFORBIDDENFRUITSLIST, "");
                    XmlNode objStealForbiddenFruitsNode = objNode.SelectSingleNode(Constants.TASK_GARDEN_STEALFORBIDDENFRUITSLIST);
                    if (objStealForbiddenFruitsNode == null)
                        return false;
                    objStealForbiddenFruitsNode.RemoveAll();
                    foreach (int fruitid in taskitem.StealForbiddenFruitsList)
                    {
                        XmlElement objFruitIdNode = objXmlDoc.CreateElement("fruitid");
                        objFruitIdNode.InnerText = fruitid.ToString();
                        objStealForbiddenFruitsNode.AppendChild(objFruitIdNode);
                    }
                }
                /*------------------------------Garden-----------------------------------*/

                /*------------------------------Ranch-----------------------------------*/
                objNode = GetAppendNode(objTaskNode, objXmlDoc, Constants.GAME_RANCH, "");
                if (objNode != null)
                {
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_EXECUTERANCH, "True").InnerText = taskitem.ExecuteRanch.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_HARVESTPRODUCT, "True").InnerText = taskitem.HarvestProduct.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_HARVESTANIMAL, "True").InnerText = taskitem.HarvestAnimal.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_ADDWATER, "True").InnerText = taskitem.AddWater.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_HELPADDWATER, "False").InnerText = taskitem.HelpAddWater.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_ADDGRASS, "True").InnerText = taskitem.AddGrass.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_HELPADDGRASS, "False").InnerText = taskitem.HelpAddGrass.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_BUYCALF, "True").InnerText = taskitem.BuyCalf.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_BUYCALFBYPRICE, "True").InnerText = taskitem.BuyCalfByPrice.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_BUYCALFCUSTOM, "1").InnerText = taskitem.BuyCalfCustom.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_STEALPRODUCT, "False").InnerText = taskitem.StealProduct.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_MAKEPRODUCT, "True").InnerText = taskitem.MakeProduct.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_HELPMAKEPRODUCT, "False").InnerText = taskitem.HelpMakeProduct.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_BREEDANIMAL, "False").InnerText = taskitem.BreedAnimal.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_FOODNUM, "200").InnerText = taskitem.FoodNum.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_PRESENTPRODUCT, "False").InnerText = taskitem.PresentProduct.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_PRESENTPRODUCTBYPRICE, "True").InnerText = taskitem.PresentProductByPrice.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_PRESENTPRODUCTCHECKVALUE, "True").InnerText = taskitem.PresentProductCheckValue.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_PRESENTPRODUCTVALUE, "100").InnerText = taskitem.PresentProductValue.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_PRESENTPRODUCTAID, "1").InnerText = taskitem.PresentProductAid.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_PRESENTPRODUCTTYPE, "0").InnerText = taskitem.PresentProductType.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_PRESENTPRODUCTCHECKNUM, "True").InnerText = taskitem.PresentProductCheckNum.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_PRESENTPRODUCTNUM, "100").InnerText = taskitem.PresentProductNum.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_SELLPRODUCT, "False").InnerText = taskitem.SellProduct.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_SELLPRODUCTLOWCASH, "True").InnerText = taskitem.SellProductLowCash.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_SELLPRODUCTLOWCASHLIMIT, "100").InnerText = taskitem.SellProductLowCashLimit.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_SELLALLPRODUCTS, "True").InnerText = taskitem.SellAllProducts.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_SELLPRODUCTMAXLIMIT, "300").InnerText = taskitem.SellProductMaxLimit.ToString();
                    XmlNode objSellProductFobiddenNode = GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_SELLPRODUCTFORBIDDENLIST, ""); ;
                    objSellProductFobiddenNode.RemoveAll();
                    foreach (ProductInfo product in taskitem.SellProductForbiddenList)
                    {
                        XmlElement objChildNode = objXmlDoc.CreateElement("item");
                        objSellProductFobiddenNode.AppendChild(objChildNode);
                        XmlElement emtProductAIdNode = objXmlDoc.CreateElement(Constants.TASK_RANCH_PRODUCTAID);
                        emtProductAIdNode.InnerText = product.Aid.ToString();
                        XmlElement emtProductTypeNode = objXmlDoc.CreateElement(Constants.TASK_RANCH_PRODUCTTYPE);
                        emtProductTypeNode.InnerText = product.Type.ToString();
                        objChildNode.AppendChild(emtProductAIdNode);
                        objChildNode.AppendChild(emtProductTypeNode);                        
                    }
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_ADDCARROT, "True").InnerText = taskitem.AddCarrot.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_HELPADDCARROT, "False").InnerText = taskitem.HelpAddCarrot.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_CARROTNUM, "200").InnerText = taskitem.CarrotNum.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_ADDBAMBOO, "True").InnerText = taskitem.AddBamboo.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_HELPADDBAMBOO, "False").InnerText = taskitem.HelpAddBamboo.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RANCH_BAMBOONUM, "200").InnerText = taskitem.BambooNum.ToString();
                }
                /*------------------------------Ranch-----------------------------------*/

                /*------------------------------Fish-----------------------------------*/
                objNode = GetAppendNode(objTaskNode, objXmlDoc, Constants.GAME_FISH, "");
                if (objNode != null)
                {
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_EXECUTEFISH, "True").InnerText = taskitem.ExecuteFish.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_SHAKE, "True").InnerText = taskitem.Shake.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_TREATFISH, "True").InnerText = taskitem.TreatFish.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_UPDATEFISHPOND, "True").InnerText = taskitem.UpdateFishPond.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_BANGKEJING, "True").InnerText = taskitem.BangKeJing.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_BUYFISH, "True").InnerText = taskitem.BuyFish.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_MAXFISHES, "20").InnerText = taskitem.MaxFishes.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_BUYFISHBYRANK, "True").InnerText = taskitem.BuyFishByRank.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_BUYFISHFISHID, "1").InnerText = taskitem.BuyFishFishId.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_FISHING, "True").InnerText = taskitem.Fishing.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_BUYUPDATETACKLE, "False").InnerText = taskitem.BuyUpdateTackle.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_MAXTACKLES, "5").InnerText = taskitem.MaxTackles.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_HARVESTFISH, "True").InnerText = taskitem.HarvestFish.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_NETSELFFISH, "False").InnerText = taskitem.NetSelfFish.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_NETSELFFISHCHEAP, "False").InnerText = taskitem.NetSelfFishCheap.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_NETSELFFISHMATURE, "80").InnerText = taskitem.NetSelfFishMature.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_HELPFISH, "True").InnerText = taskitem.HelpFish.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_PRESENTFISH, "False").InnerText = taskitem.PresentFish.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_PRESENTFISHCHEAP, "False").InnerText = taskitem.PresentFishCheap.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_PRESENTFISHCHECKVALUE, "True").InnerText = taskitem.PresentFishCheckValue.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_PRESENTFISHVALUE, "10000").InnerText = taskitem.PresentFishValue.ToString();
                    XmlNode objPresentFishesNode = GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_PRESENTFISHFORBIDDENLIST, "");
                    objPresentFishesNode.RemoveAll();
                    foreach (int fishid in taskitem.PresentFishForbiddenList)
                    {
                        XmlElement objFishIdNode = objXmlDoc.CreateElement(Constants.TASK_FISH_FISHID);
                        objFishIdNode.InnerText = fishid.ToString();
                        objPresentFishesNode.AppendChild(objFishIdNode);
                    }
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_SELLFISH, "False").InnerText = taskitem.SellFish.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_SELLFISHLOWCASH, "False").InnerText = taskitem.SellFishLowCash.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_SELLFISHLOWCASHLIMIT, "10").InnerText = taskitem.SellFishLowCashLimit.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_SELLALLFISH, "False").InnerText = taskitem.SellAllFish.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_SELLFISHCHECKVALUE, "False").InnerText = taskitem.SellFishCheckValue.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_SELLFISHVALUE, "10000").InnerText = taskitem.SellFishValue.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_SELLFISHMAXLIMIT, "20").InnerText = taskitem.SellFishMaxLimit.ToString();
                    XmlNode objSellFishesNode = GetAppendNode(objNode, objXmlDoc, Constants.TASK_FISH_SELLFISHFORBIDDENLIST, "");
                    objSellFishesNode.RemoveAll();
                    foreach (int fishid in taskitem.SellFishForbiddenList)
                    {
                        XmlElement objFishIdNode = objXmlDoc.CreateElement(Constants.TASK_FISH_FISHID);
                        objFishIdNode.InnerText = fishid.ToString();
                        objSellFishesNode.AppendChild(objFishIdNode);
                    }
                }
                /*------------------------------Fish-----------------------------------*/

                /*------------------------------Rich-----------------------------------*/
                objNode = GetAppendNode(objTaskNode, objXmlDoc, Constants.GAME_RICH, "");
                if (objNode != null)
                {
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RICH_EXECUTERICH, "True").InnerText = taskitem.ExecuteRich.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RICH_SELLASSET, "True").InnerText = taskitem.SellAsset.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RICH_BUYASSET, "True").InnerText = taskitem.BuyAsset.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RICH_BUYASSETCHEAP, "False").InnerText = taskitem.BuyAssetCheap.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RICH_GIVEUPIFRATIO, "True").InnerText = taskitem.GiveUpIfRatio.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RICH_GIVEUPRATIO, "50").InnerText = taskitem.GiveUpRatio.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RICH_GIVEUPIFMINIMUM, "True").InnerText = taskitem.GiveUpIfMinimum.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RICH_GIVEUPMINIMUM, "5").InnerText = taskitem.GiveUpMinimum.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RICH_GIVEUPIFMYASSET, "False").InnerText = taskitem.GiveUpIfMyAsset.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RICH_GIVEUPASSETCOUNT, "3").InnerText = taskitem.GiveUpAssetCount.ToString();                    
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RICH_ADVANCEDPURCHASE, "False").InnerText = taskitem.AdvancedPurchase.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_RICH_BUYASSETSLIST, "");
                    XmlNode objBuyAssetsNode = objNode.SelectSingleNode(Constants.TASK_RICH_BUYASSETSLIST);
                    if (objBuyAssetsNode == null)
                        return false;
                    objBuyAssetsNode.RemoveAll();
                    foreach (int assetid in taskitem.BuyAssetsList)
                    {
                        XmlElement objAssetIdNode = objXmlDoc.CreateElement(Constants.TASK_RICH_ASSETID);
                        objAssetIdNode.InnerText = assetid.ToString();
                        objBuyAssetsNode.AppendChild(objAssetIdNode);
                    }
                }
                /*------------------------------Rich-----------------------------------*/

                /*------------------------------Cafe-----------------------------------*/
                objNode = GetAppendNode(objTaskNode, objXmlDoc, Constants.GAME_CAFE, "");
                if (objNode != null)
                {
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_EXECUTECAFE, "True").InnerText = taskitem.ExecuteCafe.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_BOXCLEAN, "True").InnerText = taskitem.BoxClean.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_COOK, "True").InnerText = taskitem.Cook.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_COOKTOMATOFIRST, "True").InnerText = taskitem.CookTomatoFirst.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_COOKMEDLARFIRST, "False").InnerText = taskitem.CookMedlarFirst.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_COOKCRABFIRST, "False").InnerText = taskitem.CookCrabFirst.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_COOKPINEAPPLEFIRST, "False").InnerText = taskitem.CookPineappleFirst.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_COOKDISHID, "4").InnerText = taskitem.CookDishId.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_COOKLOWCASH, "True").InnerText = taskitem.CookLowCash.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_COOKLOWCASHLIMIT, "2000").InnerText = taskitem.CookLowCashLimit.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_HIRE, "True").InnerText = taskitem.Hire.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_MAXEMPLOYEES, "12").InnerText = taskitem.MaxEmployees.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_HELPFRIEND, "True").InnerText = taskitem.HelpFriend.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_PRESENTFOOD, "False").InnerText = taskitem.PresentFood.ToString();
                    XmlNode objPresentForbiddenDishesNode = GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_PRESENTFORBIDDENFOODLIST, "");
                    if (objPresentForbiddenDishesNode == null)
                        return false;
                    objPresentForbiddenDishesNode.RemoveAll();
                    foreach (int dishid in taskitem.PresentForbiddenFoodList)
                    {
                        XmlElement objDishIdNode = objXmlDoc.CreateElement("DishId");
                        objDishIdNode.InnerText = dishid.ToString();
                        objPresentForbiddenDishesNode.AppendChild(objDishIdNode);
                    }
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_PRESENTFOODBYCOUNT, "True").InnerText = taskitem.PresentFoodByCount.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_PRESENTFOODDISHID, "4").InnerText = taskitem.PresentFoodDishId.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_PRESENTFOODMESSAGE, "送你食物啦！").InnerText = taskitem.PresentFoodMessage.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_PRESENTFOODRATIO, "50").InnerText = taskitem.PresentFoodRatio.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_PRESENTLOWCASH, "True").InnerText = taskitem.PresentLowCash.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_PRESENTLOWCASHLIMIT, "2000").InnerText = taskitem.PresentLowCashLimit.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_PRESENTFOODLOWCOUNT, "True").InnerText = taskitem.PresentFoodLowCount.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_PRESENTFOODLOWCOUNTLIMIT, "2").InnerText = taskitem.PresentFoodLowCountLimit.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_PURCHASEFOOD, "False").InnerText = taskitem.PurchaseFood.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_PURCHASEFOODBYREFPRICE, "True").InnerText = taskitem.PurchaseFoodByRefPrice.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_SELLFOOD, "False").InnerText = taskitem.SellFood.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_CAFE_SELLFOODBYREFPRICE, "True").InnerText = taskitem.SellFoodByRefPrice.ToString();
                }
                /*------------------------------Cafe-----------------------------------*/

                /*------------------------------Miscellaneous-----------------------------------*/
                objNode = objTaskNode.SelectSingleNode(Constants.TASK_NODE_MISCELLANEOUS);
                if (objNode != null)
                {
                    objNode.SelectSingleNode(Constants.TASK_MISCELLANEOUS_GROUP).InnerText = taskitem.GroupName;
                    objNode.SelectSingleNode(Constants.TASK_MISCELLANEOUS_SENDLOG).InnerText = taskitem.SendLog.ToString();
                    objNode.SelectSingleNode(Constants.TASK_MISCELLANEOUS_RECEIVEREMAIL).InnerText = taskitem.ReceiverEmail.ToString();
                    if (objNode.SelectSingleNode(Constants.TASK_MISCELLANEOUS_WRITELOGTOFILE) == null)
                    {
                        XmlElement objChildNode = objXmlDoc.CreateElement(Constants.TASK_MISCELLANEOUS_WRITELOGTOFILE);
                        objNode.AppendChild(objChildNode);
                    }
                    objNode.SelectSingleNode(Constants.TASK_MISCELLANEOUS_WRITELOGTOFILE).InnerText = taskitem.WriteLogToFile.ToString();
                    GetAppendNode(objNode, objXmlDoc, Constants.TASK_MISCELLANEOUS_SKIPVALIDATION, "False").InnerText = taskitem.SkipValidation.ToString();
                }
                /*------------------------------Miscellaneous-----------------------------------*/

                /*------------------------------Accounts-----------------------------------*/
                XmlNode objAccountsNode = objTaskNode.SelectSingleNode(Constants.ACCOUNT_ACCOUNTS);
                if (objAccountsNode == null)
                    return false;

                objAccountsNode.RemoveAll();
                foreach (AccountInfo account in taskitem.Accounts)
                {
                    XmlElement objChildNode = objXmlDoc.CreateElement(Constants.ACCOUNT_ACCOUNT);
                    objAccountsNode.AppendChild(objChildNode);
                    XmlElement emtEmail = objXmlDoc.CreateElement(Constants.ACCOUNT_EMAIL);
                    emtEmail.InnerText = account.Email;
                    XmlElement emtPassword = objXmlDoc.CreateElement(Constants.ACCOUNT_PASSWORD);
                    emtPassword.InnerText = account.Password;
                    XmlElement emtUserName = objXmlDoc.CreateElement(Constants.ACCOUNT_USERNAME);
                    emtUserName.InnerText = account.UserName;
                    XmlElement emtUserId = objXmlDoc.CreateElement(Constants.ACCOUNT_USERID);
                    emtUserId.InnerText = account.UserId;
                    XmlElement emtGender = objXmlDoc.CreateElement(Constants.ACCOUNT_GENDER);
                    emtGender.InnerText = account.Gender.ToString();
                    objChildNode.AppendChild(emtEmail);
                    objChildNode.AppendChild(emtPassword);
                    objChildNode.AppendChild(emtUserName);
                    objChildNode.AppendChild(emtUserId);
                    objChildNode.AppendChild(emtGender);
                }
                /*------------------------------Accounts-----------------------------------*/

                //主配置文件
                if (EditTask(taskitem) == Constants.STATUS_FAIL)
                    return false;

                return SetTaskConfigFile(objXmlDoc, taskid);
                
            }
            catch (Exception ex)
            {
                LogHelper.Write("保存任务" + taskname, ex);
                return false;
            }
        }
        #endregion        

        #region GetOperationConfigFile
        private static XmlDocument GetOperationConfigFile(string groupname, string email, string password)
        {
            try
            {
                //load config info
                string folder = Path.Combine(Application.StartupPath, Constants.FOLDER_ACCOUNTS) + Constants.CHAR_DOUBLEBACKSLASH + groupname;
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                string configFile = folder + Constants.CHAR_DOUBLEBACKSLASH + email + ".xml";
                if (!File.Exists(configFile))
                {
                    string configContent = Utility.GetAccountConfig(email, password);
                    StreamWriter sw = new StreamWriter(configFile);
                    sw.Write(configContent);
                    sw.Close();
                    sw = null;
                }

                XmlDocument objXmlDoc = new XmlDocument();

                objXmlDoc.Load(configFile);

                return objXmlDoc;
            }
            catch (Exception ex)
            {
                LogHelper.Write("读取黑白名单配置文件" + email, ex);
                return null;
            }
        }
        #endregion

        #region SetOperationConfigFile
        private static bool SetOperationConfigFile(XmlDocument xmldoc, string groupname, string email)
        {
            try
            {
                string folder = Path.Combine(Application.StartupPath, Constants.FOLDER_ACCOUNTS) + Constants.CHAR_DOUBLEBACKSLASH + groupname;
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                string configFile = folder + Constants.CHAR_DOUBLEBACKSLASH + email + ".xml";

                xmldoc.Save(configFile);
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Write("读取黑白名单配置文件" + email, ex);
                return false;
            }
        }
        #endregion

        #region GetOperation
        public static OperationInfo GetOperation(string groupname, AccountInfo account)
        {
            try
            {
                if (account.Email == null || account.Email == string.Empty)
                    return null;

                XmlDocument objXmlDoc = GetOperationConfigFile(groupname, account.Email, account.Password);
                if (objXmlDoc == null)
                    return null;

                OperationInfo operation = new OperationInfo();

                operation.Account = account;

                //email
                operation.Email = GetInnerText(objXmlDoc, Constants.ACCOUNT_EMAIL);
                //password
                operation.Password = GetInnerText(objXmlDoc, Constants.ACCOUNT_PASSWORD);

                //park
                operation.ParkWhiteList = GetAccountList(objXmlDoc, Constants.PARK_PARKWHITE);
                operation.ParkBlackList = GetAccountList(objXmlDoc, Constants.PARK_PARKBLACK);
                operation.PostList = GetAccountList(objXmlDoc, Constants.PARK_POSTLIST);
                operation.PostAll = DataConvert.GetBool(GetInnerText(objXmlDoc, Constants.PARK_POSTALL));
                //operation.ParkingDelayTime = DataConvert.GetInt32(GetInnerText(objXmlDoc, Constants.CONFIG_PARKINGDELAYTIME));
                //operation.ParkMyCars = DataConvert.GetBool(GetInnerText(objXmlDoc, Constants.CONFIG_PARKMYCARS));
                //operation.PostOthersCars = DataConvert.GetBool(GetInnerText(objXmlDoc, Constants.CONFIG_POSTOTHERSCARS));
                //bite
                operation.BiteWhiteList = GetAccountList(objXmlDoc, Constants.BITE_BITEWHITE);
                operation.BiteBlackList = GetAccountList(objXmlDoc, Constants.BITE_BITEBLACK);
                operation.RecoverWhiteList = GetAccountList(objXmlDoc, Constants.BITE_RECOVERWHITE);
                operation.RecoverBlackList = GetAccountList(objXmlDoc, Constants.BITE_RECOVERBLACK);
                operation.BiteAll = DataConvert.GetBool(GetInnerText(objXmlDoc, Constants.BITE_BITEALL));
                operation.ProtectId = DataConvert.GetInt32(GetInnerText(objXmlDoc, Constants.BITE_PROTECTID));
                //operation.ApproveRecovery = DataConvert.GetBool(GetInnerText(objXmlDoc, Constants.CONFIG_APPROVERECOVERY));
                //operation.BiteOthers = DataConvert.GetBool(GetInnerText(objXmlDoc, Constants.CONFIG_BITEOTHERS));
                //operation.AutoRecover = DataConvert.GetBool(GetInnerText(objXmlDoc, Constants.CONFIG_AUTORECOVER));

                //slave
                operation.BuyWhiteList = GetAccountList(objXmlDoc, Constants.SLAVE_BUYWHITE);
                operation.BuyBlackList = GetAccountList(objXmlDoc, Constants.SLAVE_BUYBLACK);
                //operation.MaxSlaves = DataConvert.GetInt32(GetInnerText(objXmlDoc, Constants.CONFIG_MAXSLAVES));
                //operation.NickName = GetInnerText(objXmlDoc, Constants.CONFIG_NICKNAME);
                //operation.BuySlave = DataConvert.GetBool(GetInnerText(objXmlDoc, Constants.CONFIG_BUYSLAVE));
                //operation.BuyLowPriceSlave = DataConvert.GetBool(GetInnerText(objXmlDoc, Constants.CONFIG_BUYLOWPRICESLAVE));
                //operation.PropitiateSlave = DataConvert.GetBool(GetInnerText(objXmlDoc, Constants.CONFIG_PROPITIATESLAVE));
                //operation.AfflictSlave = DataConvert.GetBool(GetInnerText(objXmlDoc, Constants.CONFIG_AFFLICTSLAVE));
                //operation.ReleaseSlave = DataConvert.GetBool(GetInnerText(objXmlDoc, Constants.CONFIG_RELEASESLAVE));

                //house
                operation.StayWhiteList = GetAccountList(objXmlDoc, Constants.HOUSE_STAYWHITE);
                operation.StayBlackList = GetAccountList(objXmlDoc, Constants.HOUSE_STAYBLACK);
                operation.RobWhiteList = GetAccountList(objXmlDoc, Constants.HOUSE_ROBWHITE);
                operation.RobBlackList = GetAccountList(objXmlDoc, Constants.HOUSE_ROBBLACK);

                //garden
                operation.StealWhiteList = GetAccountList(objXmlDoc, Constants.GARDEN_STEALWHITE);
                operation.StealBlackList = GetAccountList(objXmlDoc, Constants.GARDEN_STEALBLACK);
                operation.StealAll = DataConvert.GetBool(GetInnerText(objXmlDoc, Constants.GARDEN_STEALALL));
                operation.FarmWhiteList = GetAccountList(objXmlDoc, Constants.GARDEN_FARMWHITE);
                operation.FarmBlackList = GetAccountList(objXmlDoc, Constants.GARDEN_FARMBLACK);
                operation.FarmAll = DataConvert.GetBool(GetInnerText(objXmlDoc, Constants.GARDEN_FARMALL));
                operation.PresentId = DataConvert.GetInt32(GetInnerText(objXmlDoc, Constants.GARDEN_PRESENTID));

                //ranch
                operation.HelpRanchWhiteList = GetAccountList(objXmlDoc, Constants.RANCH_HELPRANCHWHITE);
                operation.HelpRanchBlackList = GetAccountList(objXmlDoc, Constants.RANCH_HELPRANCHBLACK);
                operation.HelpRanchAll = DataConvert.GetBool(GetInnerText(objXmlDoc, Constants.RANCH_HELPRANCHALL));
                operation.StealProductWhiteList = GetAccountList(objXmlDoc, Constants.RANCH_STEALPRODUCTWHITE);
                operation.StealProductBlackList = GetAccountList(objXmlDoc, Constants.RANCH_STEALPRODUCTBLACK);
                operation.StealProductAll = DataConvert.GetBool(GetInnerText(objXmlDoc, Constants.RANCH_STEALPRODUCTALL));
                operation.PresentProductId = DataConvert.GetInt32(GetInnerText(objXmlDoc, Constants.RANCH_PRESENTPRODUCTID));

                //fish
                operation.FishingWhiteList = GetAccountList(objXmlDoc, Constants.FISH_FISHINGWHITE);
                operation.FishingBlackList = GetAccountList(objXmlDoc, Constants.FISH_FISHINGBLACK);
                operation.FishingAll = DataConvert.GetBool(GetInnerText(objXmlDoc, Constants.FISH_FISHINGALL));
                operation.HelpFishWhiteList = GetAccountList(objXmlDoc, Constants.FISH_HELPFISHWHITE);
                operation.HelpFishBlackList = GetAccountList(objXmlDoc, Constants.FISH_HELPFISHBLACK);
                operation.HelpFishAll = DataConvert.GetBool(GetInnerText(objXmlDoc, Constants.FISH_HELPFISHALL));
                operation.PresentFishId = DataConvert.GetInt32(GetInnerText(objXmlDoc, Constants.FISH_PRESENTFISHID));

                //cafe
                operation.HireWhiteList = GetAccountList(objXmlDoc, Constants.CAFE_HIREWHITE);
                operation.HireBlackList = GetAccountList(objXmlDoc, Constants.CAFE_HIREBLACK);
                operation.HireAll = DataConvert.GetBool(GetInnerText(objXmlDoc, Constants.CAFE_HIREALL));
                operation.PurchaseWhiteList = GetAccountList(objXmlDoc, Constants.CAFE_PURCHASEWHITE);
                operation.PurchaseBlackList = GetAccountList(objXmlDoc, Constants.CAFE_PURCHASEBLACK);
                operation.PurchaseAll = DataConvert.GetBool(GetInnerText(objXmlDoc, Constants.CAFE_PURCHASEALL));
                operation.PresentFoodId = DataConvert.GetInt32(GetInnerText(objXmlDoc, Constants.CAFE_PRESENTFOODID));

                return operation;
            }
            catch (Exception ex)
            {
                LogHelper.Write("读取黑白名单配置" + account.UserName, ex);
                return null;
            }
        }
        #endregion

        #region SetOperation
        public static bool SetOperation(string groupname, OperationInfo operation)
        {
            try
            {
                if (operation.Email == null || operation.Email == string.Empty)
                    return false;

                XmlDocument objXmlDoc = GetOperationConfigFile(groupname, operation.Email, operation.Password);
                if (objXmlDoc == null)
                    return false;

                //email
                SetInnerText(objXmlDoc, Constants.ACCOUNT_EMAIL, operation.Email);
                //password
                SetInnerText(objXmlDoc, Constants.ACCOUNT_PASSWORD, operation.Password);

                //park
                SetAccountList(objXmlDoc, Constants.PARK_PARKWHITE, operation.ParkWhiteList);
                SetAccountList(objXmlDoc, Constants.PARK_PARKBLACK, operation.ParkBlackList);
                SetAccountList(objXmlDoc, Constants.PARK_POSTLIST, operation.PostList);
                SetInnerText(objXmlDoc, Constants.PARK_POSTALL, operation.PostAll.ToString());
                //SetInnerText(objXmlDoc, Constants.CONFIG_PARKINGDELAYTIME, operation.ParkingDelayTime.ToString());
                //SetInnerText(objXmlDoc, Constants.CONFIG_PARKMYCARS, operation.ParkMyCars.ToString());
                //SetInnerText(objXmlDoc, Constants.CONFIG_POSTOTHERSCARS, operation.PostOthersCars.ToString());
                //bite
                SetAccountList(objXmlDoc, Constants.BITE_BITEWHITE, operation.BiteWhiteList);
                SetAccountList(objXmlDoc, Constants.BITE_BITEBLACK, operation.BiteBlackList);
                SetAccountList(objXmlDoc, Constants.BITE_RECOVERWHITE, operation.RecoverWhiteList);
                SetAccountList(objXmlDoc, Constants.BITE_RECOVERBLACK, operation.RecoverBlackList);
                SetInnerText(objXmlDoc, Constants.BITE_BITEALL, operation.BiteAll.ToString());
                SetInnerText(objXmlDoc, Constants.BITE_PROTECTID, operation.ProtectId.ToString());
                //SetInnerText(objXmlDoc, Constants.CONFIG_APPROVERECOVERY, operation.ApproveRecovery.ToString());
                //SetInnerText(objXmlDoc, Constants.CONFIG_BITEOTHERS, operation.BiteOthers.ToString());
                //SetInnerText(objXmlDoc, Constants.CONFIG_AUTORECOVER, operation.AutoRecover.ToString());
                 
                //slave
                SetAccountList(objXmlDoc, Constants.SLAVE_BUYWHITE, operation.BuyWhiteList);
                SetAccountList(objXmlDoc, Constants.SLAVE_BUYBLACK, operation.BuyBlackList);
                //SetInnerText(objXmlDoc, Constants.CONFIG_MAXSLAVES, operation.MaxSlaves.ToString());
                //SetInnerText(objXmlDoc, Constants.CONFIG_NICKNAME, operation.NickName.ToString());
                //SetInnerText(objXmlDoc, Constants.CONFIG_BUYSLAVE, operation.BuySlave.ToString());
                //SetInnerText(objXmlDoc, Constants.CONFIG_BUYLOWPRICESLAVE, operation.BuyLowPriceSlave.ToString());
                //SetInnerText(objXmlDoc, Constants.CONFIG_PROPITIATESLAVE, operation.PropitiateSlave.ToString());
                //SetInnerText(objXmlDoc, Constants.CONFIG_AFFLICTSLAVE, operation.AfflictSlave.ToString());
                //SetInnerText(objXmlDoc, Constants.CONFIG_RELEASESLAVE, operation.ReleaseSlave.ToString());

                //house
                SetAccountList(objXmlDoc, Constants.HOUSE_STAYWHITE, operation.StayWhiteList);
                SetAccountList(objXmlDoc, Constants.HOUSE_STAYBLACK, operation.StayBlackList);
                SetAccountList(objXmlDoc, Constants.HOUSE_ROBWHITE, operation.RobWhiteList);
                SetAccountList(objXmlDoc, Constants.HOUSE_ROBBLACK, operation.RobBlackList);

                //garden
                SetAccountList(objXmlDoc, Constants.GARDEN_STEALWHITE, operation.StealWhiteList);
                SetAccountList(objXmlDoc, Constants.GARDEN_STEALBLACK, operation.StealBlackList);
                SetInnerText(objXmlDoc, Constants.GARDEN_STEALALL, operation.StealAll.ToString());
                SetAccountList(objXmlDoc, Constants.GARDEN_FARMWHITE, operation.FarmWhiteList);
                SetAccountList(objXmlDoc, Constants.GARDEN_FARMBLACK, operation.FarmBlackList);
                SetInnerText(objXmlDoc, Constants.GARDEN_FARMALL, operation.FarmAll.ToString());
                SetInnerText(objXmlDoc, Constants.GARDEN_PRESENTID, operation.PresentId.ToString());

                //ranch
                SetAccountList(objXmlDoc, Constants.RANCH_HELPRANCHWHITE, operation.HelpRanchWhiteList);
                SetAccountList(objXmlDoc, Constants.RANCH_HELPRANCHBLACK, operation.HelpRanchBlackList);
                SetInnerText(objXmlDoc, Constants.RANCH_HELPRANCHALL, operation.HelpRanchAll.ToString());
                SetAccountList(objXmlDoc, Constants.RANCH_STEALPRODUCTWHITE, operation.StealProductWhiteList);
                SetAccountList(objXmlDoc, Constants.RANCH_STEALPRODUCTBLACK, operation.StealProductBlackList);
                SetInnerText(objXmlDoc, Constants.RANCH_STEALPRODUCTALL, operation.StealProductAll.ToString());
                SetInnerText(objXmlDoc, Constants.RANCH_PRESENTPRODUCTID, operation.PresentProductId.ToString());

                //fish
                SetAccountList(objXmlDoc, Constants.FISH_FISHINGWHITE, operation.FishingWhiteList);
                SetAccountList(objXmlDoc, Constants.FISH_FISHINGBLACK, operation.FishingBlackList);
                SetInnerText(objXmlDoc, Constants.FISH_FISHINGALL, operation.FishingAll.ToString());
                SetAccountList(objXmlDoc, Constants.FISH_HELPFISHWHITE, operation.HelpFishWhiteList);
                SetAccountList(objXmlDoc, Constants.FISH_HELPFISHBLACK, operation.HelpFishBlackList);
                SetInnerText(objXmlDoc, Constants.FISH_HELPFISHALL, operation.HelpFishAll.ToString());
                SetInnerText(objXmlDoc, Constants.FISH_PRESENTFISHID, operation.PresentFishId.ToString());

                //cafe
                SetAccountList(objXmlDoc, Constants.CAFE_HIREWHITE, operation.HireWhiteList);
                SetAccountList(objXmlDoc, Constants.CAFE_HIREBLACK, operation.HireBlackList);
                SetInnerText(objXmlDoc, Constants.CAFE_HIREALL, operation.HireAll.ToString());
                SetAccountList(objXmlDoc, Constants.CAFE_PURCHASEWHITE, operation.PurchaseWhiteList);
                SetAccountList(objXmlDoc, Constants.CAFE_PURCHASEBLACK, operation.PurchaseBlackList);
                SetInnerText(objXmlDoc, Constants.CAFE_PURCHASEALL, operation.PurchaseAll.ToString());
                SetInnerText(objXmlDoc, Constants.CAFE_PRESENTFOODID, operation.PresentFoodId.ToString());

                return SetOperationConfigFile(objXmlDoc, groupname, operation.Email);
            }
            catch (Exception ex)
            {
                LogHelper.Write("读取黑白名单配置" + operation.Account.UserName, ex);
                return false;
            }
        }
        #endregion

        #region GetNode
        private static XmlNode GetNode(XmlDocument xmldoc, string nodename)
        {
            XmlNode ret = null;
            XmlNode objRootNode = xmldoc.SelectSingleNode(Constants.CONFIG_ROOT);

            //fish
            GetAppendNode(objRootNode, xmldoc, Constants.GAME_FISH, "");
            //cafe
            GetAppendNode(objRootNode, xmldoc, Constants.GAME_CAFE, "");

            switch (nodename)
            {
                case Constants.ACCOUNT_EMAIL:
                    ret = objRootNode.SelectSingleNode(Constants.ACCOUNT_ACCOUNT + Constants.CHAR_SLASH + Constants.ACCOUNT_EMAIL);
                    break;
                case Constants.ACCOUNT_PASSWORD:
                    ret = objRootNode.SelectSingleNode(Constants.ACCOUNT_ACCOUNT + Constants.CHAR_SLASH + Constants.ACCOUNT_PASSWORD);
                    break;
                case Constants.PARK_PARKWHITE:
                    ret = objRootNode.SelectSingleNode(Constants.GAME_PARK + Constants.CHAR_SLASH + Constants.PARK_PARKWHITE);
                    break;
                case Constants.PARK_PARKBLACK:
                    ret = objRootNode.SelectSingleNode(Constants.GAME_PARK + Constants.CHAR_SLASH + Constants.PARK_PARKBLACK);
                    break;
                case Constants.PARK_POSTLIST:
                    ret = objRootNode.SelectSingleNode(Constants.GAME_PARK + Constants.CHAR_SLASH + Constants.PARK_POSTLIST);
                    break;
                case Constants.PARK_POSTALL:
                    ret = objRootNode.SelectSingleNode(Constants.GAME_PARK + Constants.CHAR_SLASH + Constants.PARK_POSTALL);
                    break;
                case Constants.BITE_BITEWHITE:
                    ret = objRootNode.SelectSingleNode(Constants.GAME_BITE + Constants.CHAR_SLASH + Constants.BITE_BITEWHITE);
                    break;
                case Constants.BITE_BITEBLACK:
                    ret = objRootNode.SelectSingleNode(Constants.GAME_BITE + Constants.CHAR_SLASH + Constants.BITE_BITEBLACK);
                    break;
                case Constants.BITE_RECOVERWHITE:
                    ret = objRootNode.SelectSingleNode(Constants.GAME_BITE + Constants.CHAR_SLASH + Constants.BITE_RECOVERWHITE);
                    break;
                case Constants.BITE_RECOVERBLACK:
                    ret = objRootNode.SelectSingleNode(Constants.GAME_BITE + Constants.CHAR_SLASH + Constants.BITE_RECOVERBLACK);
                    break;
                case Constants.BITE_BITEALL:
                    ret = objRootNode.SelectSingleNode(Constants.GAME_BITE + Constants.CHAR_SLASH + Constants.BITE_BITEALL);
                    break;
                case Constants.BITE_PROTECTID:
                    if (objRootNode.SelectSingleNode(Constants.GAME_BITE + Constants.CHAR_SLASH + Constants.BITE_PROTECTID) == null)
                    {
                        XmlNode objBiteNode = objRootNode.SelectSingleNode(Constants.GAME_BITE);
                        XmlElement objProtectIdNode = xmldoc.CreateElement(Constants.BITE_PROTECTID);
                        objBiteNode.AppendChild(objProtectIdNode);
                    }
                    ret = objRootNode.SelectSingleNode(Constants.GAME_BITE + Constants.CHAR_SLASH + Constants.BITE_PROTECTID);
                    break;
                case Constants.SLAVE_BUYWHITE:
                    ret = objRootNode.SelectSingleNode(Constants.GAME_SLAVE + Constants.CHAR_SLASH + Constants.SLAVE_BUYWHITE);
                    break;
                case Constants.SLAVE_BUYBLACK:
                    ret = objRootNode.SelectSingleNode(Constants.GAME_SLAVE + Constants.CHAR_SLASH + Constants.SLAVE_BUYBLACK);
                    break;
                case Constants.HOUSE_STAYWHITE:
                    if (objRootNode.SelectSingleNode(Constants.GAME_HOUSE) == null)
                    {
                        XmlElement objHouseNode = xmldoc.CreateElement(Constants.GAME_HOUSE);
                        objRootNode.AppendChild(objHouseNode);
                        XmlElement objStayWhiteNode = xmldoc.CreateElement(Constants.HOUSE_STAYWHITE);
                        objHouseNode.AppendChild(objStayWhiteNode);
                        XmlElement objRobWhiteNode = xmldoc.CreateElement(Constants.HOUSE_ROBWHITE);
                        objHouseNode.AppendChild(objRobWhiteNode);
                    }
                    ret = objRootNode.SelectSingleNode(Constants.GAME_HOUSE + Constants.CHAR_SLASH + Constants.HOUSE_STAYWHITE);
                    break;
                case Constants.HOUSE_STAYBLACK:
                    if (objRootNode.SelectSingleNode(Constants.GAME_HOUSE + Constants.CHAR_SLASH + Constants.HOUSE_STAYBLACK) == null)
                    {
                        XmlNode objHouseNode = objRootNode.SelectSingleNode(Constants.GAME_HOUSE);
                        XmlElement objStayBlackNode = xmldoc.CreateElement(Constants.HOUSE_STAYBLACK);
                        objHouseNode.AppendChild(objStayBlackNode);
                    }
                    ret = objRootNode.SelectSingleNode(Constants.GAME_HOUSE + Constants.CHAR_SLASH + Constants.HOUSE_STAYBLACK);
                    break;
                case Constants.HOUSE_ROBWHITE:
                    if (objRootNode.SelectSingleNode(Constants.GAME_HOUSE) == null)
                    {
                        XmlElement objHouseNode = xmldoc.CreateElement(Constants.GAME_HOUSE);
                        objRootNode.AppendChild(objHouseNode);
                        XmlElement objStayWhiteNode = xmldoc.CreateElement(Constants.HOUSE_STAYWHITE);
                        objHouseNode.AppendChild(objStayWhiteNode);
                        XmlElement objRobWhiteNode = xmldoc.CreateElement(Constants.HOUSE_ROBWHITE);
                        objHouseNode.AppendChild(objRobWhiteNode);
                    }
                    ret = objRootNode.SelectSingleNode(Constants.GAME_HOUSE + Constants.CHAR_SLASH + Constants.HOUSE_ROBWHITE);
                    break;
                case Constants.HOUSE_ROBBLACK:
                    if (objRootNode.SelectSingleNode(Constants.GAME_HOUSE + Constants.CHAR_SLASH + Constants.HOUSE_ROBBLACK) == null)
                    {
                        XmlNode objHouseNode = objRootNode.SelectSingleNode(Constants.GAME_HOUSE);
                        XmlElement objRobBlackNode = xmldoc.CreateElement(Constants.HOUSE_ROBBLACK);
                        objHouseNode.AppendChild(objRobBlackNode);
                    }
                    ret = objRootNode.SelectSingleNode(Constants.GAME_HOUSE + Constants.CHAR_SLASH + Constants.HOUSE_ROBBLACK);
                    break;
                case Constants.GARDEN_STEALWHITE:
                    if (objRootNode.SelectSingleNode(Constants.GAME_GARDEN) == null)
                    {
                        XmlElement objGardenNode = xmldoc.CreateElement(Constants.GAME_GARDEN);
                        objRootNode.AppendChild(objGardenNode);
                        XmlElement objStealWhiteNode = xmldoc.CreateElement(Constants.GARDEN_STEALWHITE);
                        objGardenNode.AppendChild(objStealWhiteNode);
                        XmlElement objStealBlackNode = xmldoc.CreateElement(Constants.GARDEN_STEALBLACK);
                        objGardenNode.AppendChild(objStealBlackNode);
                        XmlElement objStealAllNode = xmldoc.CreateElement(Constants.GARDEN_STEALALL);
                        objStealAllNode.InnerText = "True";
                        objGardenNode.AppendChild(objStealAllNode);
                        XmlElement objFarmWhiteNode = xmldoc.CreateElement(Constants.GARDEN_FARMWHITE);
                        objGardenNode.AppendChild(objFarmWhiteNode);
                        XmlElement objFarmBlackNode = xmldoc.CreateElement(Constants.GARDEN_FARMBLACK);
                        objGardenNode.AppendChild(objFarmBlackNode);
                        XmlElement objFarmAllNode = xmldoc.CreateElement(Constants.GARDEN_FARMALL);
                        objFarmAllNode.InnerText = "True";
                        objGardenNode.AppendChild(objFarmAllNode);
                    }
                    ret = objRootNode.SelectSingleNode(Constants.GAME_GARDEN + Constants.CHAR_SLASH + Constants.GARDEN_STEALWHITE);
                    break;
                case Constants.GARDEN_STEALBLACK:                    
                    ret = objRootNode.SelectSingleNode(Constants.GAME_GARDEN + Constants.CHAR_SLASH + Constants.GARDEN_STEALBLACK);
                    break;
                case Constants.GARDEN_STEALALL:                    
                    ret = objRootNode.SelectSingleNode(Constants.GAME_GARDEN + Constants.CHAR_SLASH + Constants.GARDEN_STEALALL);
                    break;
                case Constants.GARDEN_FARMWHITE:
                    ret = objRootNode.SelectSingleNode(Constants.GAME_GARDEN + Constants.CHAR_SLASH + Constants.GARDEN_FARMWHITE);
                    break;
                case Constants.GARDEN_FARMBLACK:
                    ret = objRootNode.SelectSingleNode(Constants.GAME_GARDEN + Constants.CHAR_SLASH + Constants.GARDEN_FARMBLACK);
                    break;
                case Constants.GARDEN_FARMALL:
                    ret = objRootNode.SelectSingleNode(Constants.GAME_GARDEN + Constants.CHAR_SLASH + Constants.GARDEN_FARMALL);
                    break;
                case Constants.GARDEN_PRESENTID:
                    if (objRootNode.SelectSingleNode(Constants.GAME_GARDEN + Constants.CHAR_SLASH + Constants.GARDEN_PRESENTID) == null)
                    {
                        XmlNode objGardenNode = objRootNode.SelectSingleNode(Constants.GAME_GARDEN);
                        XmlElement objPresentIdNode = xmldoc.CreateElement(Constants.GARDEN_PRESENTID);
                        objGardenNode.AppendChild(objPresentIdNode);
                    }
                    ret = objRootNode.SelectSingleNode(Constants.GAME_GARDEN + Constants.CHAR_SLASH + Constants.GARDEN_PRESENTID);
                    break;
                case Constants.RANCH_HELPRANCHWHITE:
                    if (objRootNode.SelectSingleNode(Constants.GAME_RANCH) == null)
                    {
                        XmlElement objRanchNode = xmldoc.CreateElement(Constants.GAME_RANCH);
                        objRootNode.AppendChild(objRanchNode);
                        XmlElement objHelpRanchWhiteNode = xmldoc.CreateElement(Constants.RANCH_HELPRANCHWHITE);
                        objRanchNode.AppendChild(objHelpRanchWhiteNode);
                        XmlElement objHelpRanchBlackNode = xmldoc.CreateElement(Constants.RANCH_HELPRANCHBLACK);
                        objRanchNode.AppendChild(objHelpRanchBlackNode);
                        XmlElement objHelpRanchAllNode = xmldoc.CreateElement(Constants.RANCH_HELPRANCHALL);
                        objHelpRanchAllNode.InnerText = "True";
                        objRanchNode.AppendChild(objHelpRanchAllNode);
                        XmlElement objStealProductWhiteNode = xmldoc.CreateElement(Constants.RANCH_STEALPRODUCTWHITE);
                        objRanchNode.AppendChild(objStealProductWhiteNode);
                        XmlElement objStealProductBlackNode = xmldoc.CreateElement(Constants.RANCH_STEALPRODUCTBLACK);
                        objRanchNode.AppendChild(objStealProductBlackNode);
                        XmlElement objStealProductAllNode = xmldoc.CreateElement(Constants.RANCH_STEALPRODUCTALL);
                        objStealProductAllNode.InnerText = "True";
                        objRanchNode.AppendChild(objStealProductAllNode);
                    }
                    ret = objRootNode.SelectSingleNode(Constants.GAME_RANCH + Constants.CHAR_SLASH + Constants.RANCH_HELPRANCHWHITE);
                    break;
                case Constants.RANCH_HELPRANCHBLACK:
                    ret = objRootNode.SelectSingleNode(Constants.GAME_RANCH + Constants.CHAR_SLASH + Constants.RANCH_HELPRANCHBLACK);
                    break;
                case Constants.RANCH_HELPRANCHALL:
                    ret = objRootNode.SelectSingleNode(Constants.GAME_RANCH + Constants.CHAR_SLASH + Constants.RANCH_HELPRANCHALL);
                    break;
                case Constants.RANCH_STEALPRODUCTWHITE:
                    ret = objRootNode.SelectSingleNode(Constants.GAME_RANCH + Constants.CHAR_SLASH + Constants.RANCH_STEALPRODUCTWHITE);
                    break;
                case Constants.RANCH_STEALPRODUCTBLACK:
                    ret = objRootNode.SelectSingleNode(Constants.GAME_RANCH + Constants.CHAR_SLASH + Constants.RANCH_STEALPRODUCTBLACK);
                    break;
                case Constants.RANCH_STEALPRODUCTALL:
                    ret = objRootNode.SelectSingleNode(Constants.GAME_RANCH + Constants.CHAR_SLASH + Constants.RANCH_STEALPRODUCTALL);
                    break;
                case Constants.RANCH_PRESENTPRODUCTID:
                    ret = GetAppendNode(objRootNode.SelectSingleNode(Constants.GAME_RANCH), xmldoc, Constants.RANCH_PRESENTPRODUCTID, "");
                    break;
                case Constants.FISH_FISHINGWHITE:
                    ret = GetAppendNode(objRootNode.SelectSingleNode(Constants.GAME_FISH), xmldoc, Constants.FISH_FISHINGWHITE, "");
                    break;
                case Constants.FISH_FISHINGBLACK:
                    ret = GetAppendNode(objRootNode.SelectSingleNode(Constants.GAME_FISH), xmldoc, Constants.FISH_FISHINGBLACK, "");
                    break;
                case Constants.FISH_FISHINGALL:
                    ret = GetAppendNode(objRootNode.SelectSingleNode(Constants.GAME_FISH), xmldoc, Constants.FISH_FISHINGALL, "True");
                    break;
                case Constants.FISH_HELPFISHWHITE:
                    ret = GetAppendNode(objRootNode.SelectSingleNode(Constants.GAME_FISH), xmldoc, Constants.FISH_HELPFISHWHITE, "");
                    break;
                case Constants.FISH_HELPFISHBLACK:
                    ret = GetAppendNode(objRootNode.SelectSingleNode(Constants.GAME_FISH), xmldoc, Constants.FISH_HELPFISHBLACK, "");
                    break;
                case Constants.FISH_HELPFISHALL:
                    ret = GetAppendNode(objRootNode.SelectSingleNode(Constants.GAME_FISH), xmldoc, Constants.FISH_HELPFISHALL, "True");
                    break;
                case Constants.FISH_PRESENTFISHID:
                    ret = GetAppendNode(objRootNode.SelectSingleNode(Constants.GAME_FISH), xmldoc, Constants.FISH_PRESENTFISHID, "");
                    break;
                case Constants.CAFE_HIREWHITE:
                    ret = GetAppendNode(objRootNode.SelectSingleNode(Constants.GAME_CAFE), xmldoc, Constants.CAFE_HIREWHITE, "");
                    break;
                case Constants.CAFE_HIREBLACK:
                    ret = GetAppendNode(objRootNode.SelectSingleNode(Constants.GAME_CAFE), xmldoc, Constants.CAFE_HIREBLACK, "");
                    break;
                case Constants.CAFE_HIREALL:
                    ret = GetAppendNode(objRootNode.SelectSingleNode(Constants.GAME_CAFE), xmldoc, Constants.CAFE_HIREALL, "True");
                    break;
                case Constants.CAFE_PURCHASEWHITE:
                    ret = GetAppendNode(objRootNode.SelectSingleNode(Constants.GAME_CAFE), xmldoc, Constants.CAFE_PURCHASEWHITE, "");
                    break;
                case Constants.CAFE_PURCHASEBLACK:
                    ret = GetAppendNode(objRootNode.SelectSingleNode(Constants.GAME_CAFE), xmldoc, Constants.CAFE_PURCHASEBLACK, "");
                    break;
                case Constants.CAFE_PURCHASEALL:
                    ret = GetAppendNode(objRootNode.SelectSingleNode(Constants.GAME_CAFE), xmldoc, Constants.CAFE_PURCHASEALL, "True");
                    break;
                case Constants.CAFE_PRESENTFOODID:
                    ret = GetAppendNode(objRootNode.SelectSingleNode(Constants.GAME_CAFE), xmldoc, Constants.CAFE_PRESENTFOODID, "");
                    break;

                default:
                    ret = null;
                    break;
            }
            return ret;
        }
        #endregion

        #region GetAccountList SetAccountList
        private static string GetInnerText(XmlDocument xmldoc, string nodename)
        {
            XmlNode node = GetNode(xmldoc, nodename);
            return node.InnerText;
        }

        private static void SetInnerText(XmlDocument xmldoc, string nodename, string newvalue)
        {
            XmlNode node = GetNode(xmldoc, nodename);
            node.InnerText = newvalue;
        }

        private static Collection<int> GetAccountList(XmlDocument xmldoc, string nodename)
        {
            Collection<int> accounts = new Collection<int>();
            XmlNode node = GetNode(xmldoc, nodename);
            foreach (XmlNode xn in node.ChildNodes)
            {
                accounts.Add(Convert.ToInt32(xn.ChildNodes[0].InnerText));
            }
            return accounts;
        }

        private static void SetAccountList(XmlDocument xmldoc, string nodename, Collection<int> list)
        {
            Collection<int> accounts = list;
            XmlNode node = GetNode(xmldoc, nodename);
            node.RemoveAll();
            foreach (int userid in accounts)
            {
                XmlElement objChildNode = xmldoc.CreateElement(Constants.ACCOUNT_USERID);
                node.AppendChild(objChildNode);
                objChildNode.InnerText = userid.ToString();
            }
        }
        #endregion

        #region Park

        #region GetCarsInMarket
        public static Collection<NewCarInfo> GetCarsInMarket()
        {            
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_CARSINMARKETMASTERDATA);
                if (objXmlDoc == null)
                    return null;

                DataView dv = GetData(objXmlDoc, "data/Cars");

                Collection<NewCarInfo> cars = new Collection<NewCarInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    NewCarInfo car = new NewCarInfo();
                    car.CarId = DataConvert.GetInt32(dv.Table.Rows[ix][0]);
                    car.CarName = dv.Table.Rows[ix][1].ToString();
                    car.CarPrice = DataConvert.GetInt32(dv.Table.Rows[ix][2]);
                    cars.Add(car);
                }
                return cars;
            }
            catch (Exception ex)
            {
                LogHelper.Write("读取市场上汽车列表", ex);
                return null;
            }
        }
        #endregion

        #region SetCarsInMarket
        public static bool SetCarsInMarket(Collection<NewCarInfo> cars)
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_CARSINMARKETMASTERDATA);
                if (objXmlDoc == null)
                    return false;

                XmlNode objCarsNode = objXmlDoc.SelectSingleNode("data/Cars");
                objCarsNode.RemoveAll();
                foreach (NewCarInfo car in cars)
                {
                    XmlElement objChildNode = objXmlDoc.CreateElement(Constants.CARSINMARKET_CAR);
                    objCarsNode.AppendChild(objChildNode);
                    XmlElement emtCarId = objXmlDoc.CreateElement(Constants.CARSINMARKET_CARID);
                    emtCarId.InnerText = car.CarId.ToString();
                    XmlElement emtCarName = objXmlDoc.CreateElement(Constants.CARSINMARKET_CARNAME);
                    emtCarName.InnerText = car.CarName;
                    XmlElement emtCarPrice = objXmlDoc.CreateElement(Constants.CARSINMARKET_CARPRICE);
                    emtCarPrice.InnerText = car.CarPrice.ToString();
                    objChildNode.AppendChild(emtCarId);
                    objChildNode.AppendChild(emtCarName);
                    objChildNode.AppendChild(emtCarPrice);
                }

                return SetMasterDataFile(objXmlDoc, Constants.FILE_CARSINMARKETMASTERDATA);

            }
            catch (Exception ex)
            {
                LogHelper.Write("SetCarsInMarket", ex);
                return false;
            }
        }
        #endregion
        
        #region GetMatches
        public static Collection<MatchInfo> GetMatches()
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_MATCHESMASTERDATA);
                if (objXmlDoc == null)
                    return null;

                DataView dv = GetData(objXmlDoc, "data/matches");

                Collection<MatchInfo> matches = new Collection<MatchInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    MatchInfo match = new MatchInfo();
                    match.MatchId = DataConvert.GetInt32(dv.Table.Rows[ix]["matchid"]);
                    match.Name = DataConvert.GetString(dv.Table.Rows[ix]["name"]);
                    match.ShortName = DataConvert.GetString(dv.Table.Rows[ix]["shortname"]);
                    match.Distance = DataConvert.GetInt32(dv.Table.Rows[ix]["distance"]);
                    matches.Add(match);
                }
                return matches;
            }
            catch (Exception ex)
            {
                LogHelper.Write("读取拉力赛列表", ex);
                return null;
            }
        }
        #endregion       

        #endregion

        #region Garden

        #region GetGarden
        public static GardenInfo GetGarden(string strxml)
        {
            try
            {
                if (String.IsNullOrEmpty(strxml))
                    return null;

                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(strxml);
                if (objXmlDoc == null)
                    return null;

                GardenInfo garden = new GardenInfo();

                //garden
                XmlNode objNode = objXmlDoc.SelectSingleNode(Constants.GARDEN_ROOT + Constants.CHAR_SLASH + Constants.GARDEN_ACCOUNT);
                if (objNode == null)
                    return null;

                garden.Rank = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.GARDEN_RANK).InnerText);
                garden.RankTip = objNode.SelectSingleNode(Constants.GARDEN_RANKTIP).InnerText;
                garden.Name = objNode.SelectSingleNode(Constants.GARDEN_NAME).InnerText;
                garden.CashTip = objNode.SelectSingleNode(Constants.GARDEN_CASHTIP).InnerText;
                garden.Cash = GetCash(garden.CashTip);
                garden.TCharms = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.GARDEN_TCHARMS).InnerText);
                if (objNode.SelectSingleNode(Constants.GARDEN_CAREURL) == null)
                    garden.HasMonitor = false;
                else if (!String.IsNullOrEmpty(objNode.SelectSingleNode(Constants.GARDEN_CAREURL).InnerText))
                    garden.HasMonitor = true;
                else
                    garden.HasMonitor = false;

                //plots
                DataView dv = GetData(objXmlDoc, Constants.GARDEN_ROOT + Constants.CHAR_SLASH + Constants.GARDEN_GARDEN);

                Collection<PlotInfo> plots = new Collection<PlotInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    PlotInfo plot = new PlotInfo();
                    plot.Water = DataConvert.GetInt32(dv.Table.Rows[ix]["water"]);
                    plot.FarmNum = DataConvert.GetInt32(dv.Table.Rows[ix]["farmnum"]);
                    plot.Vermin = DataConvert.GetInt32(dv.Table.Rows[ix]["vermin"]);
                    plot.CropsId = DataConvert.GetInt64(dv.Table.Rows[ix]["cropsid"]);
                    plot.Fuid = DataConvert.GetInt32(dv.Table.Rows[ix]["fuid"]);
                    plot.Status = DataConvert.GetInt32(dv.Table.Rows[ix]["status"]);
                    plot.Grass = DataConvert.GetInt32(dv.Table.Rows[ix]["grass"]);
                    plot.Shared = DataConvert.GetInt32(dv.Table.Rows[ix]["shared"]);
                    if (dv.Table.Columns.Contains("cropsstatus"))
                        plot.CropsStatus = DataConvert.GetInt32(dv.Table.Rows[ix]["cropsstatus"]);
                    else
                        plot.CropsStatus = -2;
                    if (dv.Table.Columns.Contains("seedid"))
                        plot.SeedId = DataConvert.GetInt32(dv.Table.Rows[ix]["seedid"]);
                    else
                        plot.SeedId = 0;
                    if (dv.Table.Columns.Contains("crops"))
                        plot.Crops = JsonHelper.FiltrateHtmlTags(dv.Table.Rows[ix]["crops"].ToString());
                    else
                        plot.Crops = "";
                    if (dv.Table.Columns.Contains("farm"))
                        plot.Farm = JsonHelper.FiltrateHtmlTags(dv.Table.Rows[ix]["farm"].ToString());
                    else
                        plot.Farm = "";
                    plots.Add(plot);
                }

                garden.Plots = plots;

                return garden;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetGarden", strxml, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion
       
        #region GetSeedsInShop
        public static Collection<SeedInfo> GetSeedsInShop()
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_SEEDSLISTMASTERDATA);
                if (objXmlDoc == null)
                    return null;

                DataView dv = GetData(objXmlDoc, "data/seed");

                Collection<SeedInfo> seeds = new Collection<SeedInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    SeedInfo seed = new SeedInfo();
                    seed.SeedId = DataConvert.GetInt32(dv.Table.Rows[ix]["seedid"]);
                    seed.Name = DataConvert.GetString(dv.Table.Rows[ix]["name"]);
                    seed.Price = DataConvert.GetInt32(dv.Table.Rows[ix]["price"]);                    
                    seeds.Add(seed);
                }                
                
                return seeds;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetSeedsInShop", ex);
                return null;
            }
        }
        #endregion

        #region SetSeedsInShop
        public static bool SetSeedsInShop(Collection<SeedInfo> seeds)
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_SEEDSLISTMASTERDATA);
                if (objXmlDoc == null)
                    return false;

                XmlNode objSeedNode = objXmlDoc.SelectSingleNode("data/seed");
                objSeedNode.RemoveAll();
                foreach (SeedInfo seed in seeds)
                {
                    XmlElement objChildNode = objXmlDoc.CreateElement(Constants.GARDEN_SEED_ITEM);
                    objSeedNode.AppendChild(objChildNode);
                    XmlElement emtSeedId = objXmlDoc.CreateElement(Constants.GARDEN_SEED_SEEDID);
                    emtSeedId.InnerText = seed.SeedId.ToString();
                    XmlElement emtName = objXmlDoc.CreateElement(Constants.GARDEN_SEED_NAME);
                    emtName.InnerText = seed.Name;
                    XmlElement emtPrice = objXmlDoc.CreateElement(Constants.GARDEN_SEED_PRICE);
                    emtPrice.InnerText = seed.Price.ToString();
                    objChildNode.AppendChild(emtSeedId);
                    objChildNode.AppendChild(emtName);
                    objChildNode.AppendChild(emtPrice);
                }

                return SetMasterDataFile(objXmlDoc, Constants.FILE_SEEDSLISTMASTERDATA);

            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.SetSeedsInShop", ex);
                return false;
            }
        }        
        #endregion

        #region GetSeedsToTable
        public static DataTable GetSeedsToTable()
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_SEEDSLISTMASTERDATA);
                if (objXmlDoc == null)
                    return null;

                DataSet ds = new DataSet();
                DataTable dt;

                XmlNode node = objXmlDoc.SelectSingleNode("data/seed");
                if (node == null)
                    dt = new DataTable("table0");
                else
                {
                    StringReader read = new StringReader(node.OuterXml);

                    ds.ReadXml(read);
                    if (ds.Tables.Count < 1)
                        dt = new DataTable("table0");
                    else
                        dt = ds.Tables[0];
                }                
                return dt;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetSeedsToTable", ex);
                return null;
            }
        }
        #endregion

        #region GetOriginalSeedsList
        public static Collection<SeedInfo> GetOriginalSeedsList(string content)
        {
            try
            {
                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(content);
                if (objXmlDoc == null)
                    return null;

                if (objXmlDoc.SelectSingleNode("data/seed") == null)
                {
                    return new Collection<SeedInfo>();
                }

                DataView dv = GetData(objXmlDoc, "data/seed");

                Collection<SeedInfo> seeds = new Collection<SeedInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    SeedInfo seed = new SeedInfo();
                    seed.SeedId = DataConvert.GetInt32(dv.Table.Rows[ix]["seedid"]);                    
                    seed.Name = DataConvert.GetString(dv.Table.Rows[ix]["name"]);
                    seed.Price = DataConvert.GetInt32(dv.Table.Rows[ix]["price"]);
                    seeds.Add(seed);
                }
                return seeds;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetOriginalSeedsList", content, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion
        
        #region GetRankSeedsToTable
        public static DataTable GetRankSeedsToTable()
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_RANKSEEDSMASTERDATA);
                if (objXmlDoc == null)
                    return null;

                DataSet ds = new DataSet();
                DataTable dt;

                XmlNode node = objXmlDoc.SelectSingleNode(Constants.GARDEN_SEED_ROOT + Constants.CHAR_SLASH + Constants.GARDEN_SEED_SEED);
                if (node == null)
                    dt = new DataTable("table0");
                else
                {
                    StringReader read = new StringReader(node.OuterXml);

                    ds.ReadXml(read);
                    if (ds.Tables.Count < 1)
                        dt = new DataTable("table0");
                    else
                        dt = ds.Tables[0];
                }
                return dt;
            }
            catch (Exception ex)
            {
                LogHelper.Write("读取种子列表", ex);
                return null;
            }
        }
        #endregion

        #region GetRankSeeds
        public static Collection<RankSeedInfo> GetRankSeeds()
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_RANKSEEDSMASTERDATA);
                if (objXmlDoc == null)
                    return null;

                DataView dv = GetData(objXmlDoc, Constants.GARDEN_RANK_DATA + Constants.CHAR_SLASH + Constants.GARDEN_RANK_SEED);

                Collection<RankSeedInfo> rankseeds = new Collection<RankSeedInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    RankSeedInfo seed = new RankSeedInfo();
                    seed.Rank = DataConvert.GetInt32(dv.Table.Rows[ix][0].ToString());
                    seed.SeedId = DataConvert.GetInt32(dv.Table.Rows[ix][1].ToString());
                    seed.Name = dv.Table.Rows[ix][2].ToString();
                    rankseeds.Add(seed);
                }

                return rankseeds;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetRankSeeds", ex);
                return null;
            }
        }
        #endregion

        #region SetRankSeeds
        public static bool SetRankSeeds(DataTable dt)
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_RANKSEEDSMASTERDATA);
                if (objXmlDoc == null)
                    return false;

                XmlNode objSeedNode = objXmlDoc.SelectSingleNode(Constants.GARDEN_RANK_DATA + Constants.CHAR_SLASH + Constants.GARDEN_RANK_SEED);
                objSeedNode.RemoveAll();

                for (int ix = 0; ix < dt.Rows.Count; ix++)
                {
                    XmlElement objChildNode = objXmlDoc.CreateElement(Constants.GARDEN_RANK_ITEM);
                    objSeedNode.AppendChild(objChildNode);
                    XmlElement emtRank = objXmlDoc.CreateElement(Constants.GARDEN_RANK_RANK);
                    emtRank.InnerText = dt.Rows[ix][0].ToString();
                    XmlElement emtSeedId = objXmlDoc.CreateElement(Constants.GARDEN_SEED_SEEDID);
                    emtSeedId.InnerText = dt.Rows[ix][1].ToString();
                    XmlElement emtSeedName = objXmlDoc.CreateElement(Constants.GARDEN_RANK_SEEDNAME);
                    emtSeedName.InnerText = dt.Rows[ix][2].ToString();
                    objChildNode.AppendChild(emtRank);
                    objChildNode.AppendChild(emtSeedId);
                    objChildNode.AppendChild(emtSeedName);
                }

                return SetMasterDataFile(objXmlDoc, Constants.FILE_RANKSEEDSMASTERDATA);

            }
            catch (Exception ex)
            {
                LogHelper.Write("保存种子列表", ex);
                return false;
            }
        }
        #endregion

        #region GetFruits
        public static Collection<FruitInfo> GetFruits()
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_FRUITSMASTERDATA);
                if (objXmlDoc == null)
                    return null;

                DataView dv = GetData(objXmlDoc, "data/fruit");

                Collection<FruitInfo> fruits = new Collection<FruitInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    FruitInfo fruit = new FruitInfo();
                    fruit.FruitId = DataConvert.GetInt32(dv.Table.Rows[ix]["fruitid"]);
                    fruit.Name = DataConvert.GetString(dv.Table.Rows[ix]["name"]);
                    fruit.SellPrice = DataConvert.GetInt32(dv.Table.Rows[ix]["sellprice"]);
                    fruits.Add(fruit);
                }

                return fruits;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetFruits", ex);
                return null;
            }
        }
        #endregion       

        #region ConvertToStealObject
        public static StealInfo ConvertToStealObject(string feedback)
        {
            //<data><seedid>11</seedid><anti>1</anti><ret>succ</ret></data>
            //<data><seedid>229</seedid><chgseedid>235</chgseedid><anti>1</anti><ret>succ</ret><mammon_award_url>http://img.kaixin001.com.cn/swf/house/garden/festival/mammon-1.swf</mammon_award_url><mammon_award_id>0</mammon_award_id><mammon_award_msg>&lt;img src='http://img.kaixin001.com.cn/i2/house/garden/crop8/yuanbao.swf'&gt;|财神送你价值15000元的金银财宝，已充入你的账户中，恭祝你新年发大财</mammon_award_msg></data>

            //<data>
            //  <tips>这块地是你与沈致冰共种的，刚才收获的果实你们俩1人1半</tips>
            //  <leftnum>25</leftnum>
            //  <stealnum>0</stealnum>
            //  <num>25</num>
            //  <seedname>牧草</seedname>
            //  <fruitpic>http://img.kaixin001.com.cn//i2/house/garden/crop3/gouweiba.swf</fruitpic>
            //  <ret>succ</ret>
            //</data>

            //菜老伯
            //<data>
            //  <caretips>被我逮到了吧，下次别再偷了，小心被我送你去派出所！</caretips>
            //  <caretips2>你偷果实被菜老伯抓住，魅力值减少30</caretips2>
            //  <ret>succ</ret>
            //</data>

            //<data>
            //  <anti>0</anti>
            //  <leftnum>2</leftnum>
            //  <stealnum>2</stealnum>
            //  <num>2</num>
            //  <seedname>冬虫夏草</seedname>
            //  <fruitpic>http://img.kaixin001.com.cn//i2/house/garden/crop2/dongchongxiacao.swf</fruitpic>
            //  <ret>succ</ret>
            //</data>
            try
            {
                if (feedback.IndexOf("菜老伯") > -1 || feedback.IndexOf("逮到了") > -1)
                    return null;

                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(feedback);
                if (objXmlDoc == null)
                    return null;

                StealInfo steal = new StealInfo();

                //garden
                XmlNode objNode = objXmlDoc.SelectSingleNode(Constants.GARDEN_STEAL_ROOT);
                if (objNode == null)
                    return null;

                if (feedback.IndexOf("<anti>") > -1)
                    steal.Anti = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.GARDEN_STEAL_ANTI).InnerText);

                steal.LeftNum = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.GARDEN_STEAL_LEFTNUM).InnerText);
                steal.StealNum = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.GARDEN_STEAL_STEALNUM).InnerText);
                steal.Num = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.GARDEN_STEAL_NUM).InnerText);
                steal.SeedName = objNode.SelectSingleNode(Constants.GARDEN_STEAL_SEEDNAME).InnerText;
                steal.FruitPic = objNode.SelectSingleNode(Constants.GARDEN_STEAL_FRUITPIC).InnerText;
                steal.Ret = objNode.SelectSingleNode(Constants.GARDEN_STEAL_RET).InnerText;

                return steal;
            }
            catch (Exception ex)
            {
                LogHelper.Write("转换成StealObject-" + feedback, ex);
                return null;
            }
        }
        #endregion

        #region ConvertToPresentObject
        public static PresentInfo ConvertToPresentObject(string feedback)
        {
            //<data>
            //  <ret>succ</ret>
            //  <name>绿玫瑰</name>
            //  <fruitpic>http://img.kaixin001.com.cn//i2/house/garden/crop/meigui_g.swf</fruitpic>
            //  <fruit_minprice>10000</fruit_minprice>
            //  <fruit_maxprice>10100</fruit_maxprice>
            //  <fruitnum>4</fruitnum>
            //  <selfnum>4</selfnum>
            //  <bpresent>1</bpresent>
            //  <fruitprice>10050</fruitprice>
            //</data>

            //<data>
            //  <ret>succ</ret>
            //  <name>黑玫瑰</name>
            //  <fruitpic>http://img.kaixin001.com.cn//i2/house/garden/crop/meigui_b.swf</fruitpic>
            //  <fruit_minprice>5900</fruit_minprice>
            //  <fruit_maxprice>6100</fruit_maxprice>
            //  <fruitnum>3</fruitnum>
            //  <selfnum>3</selfnum>
            //  <bpresent>1</bpresent>
            //  <fruitprice>6000</fruitprice>
            //</data>

            //<data>
            //  <ret>succ</ret>
            //  <name>蓝色妖姬</name>
            //  <fruitpic>http://img.kaixin001.com.cn//i2/house/garden/crop/meigui_bl.swf</fruitpic>
            //  <fruit_minprice>5900</fruit_minprice>
            //  <fruit_maxprice>6100</fruit_maxprice>
            //  <fruitnum>3</fruitnum>
            //  <selfnum>3</selfnum>
            //  <bpresent>1</bpresent>
            //  <fruitprice>6000</fruitprice>
            //</data>

            //<data>
            //  <ret>succ</ret>
            //  <name>双色玫瑰</name>
            //  <fruitpic>http://img.kaixin001.com.cn//i2/house/garden/crop/meigui_s.swf</fruitpic>
            //  <fruit_minprice>5400</fruit_minprice>
            //  <fruit_maxprice>5600</fruit_maxprice>
            //  <fruitnum>6</fruitnum>
            //  <selfnum>6</selfnum>
            //  <bpresent>1</bpresent>
            //  <fruitprice>5500</fruitprice>
            //</data>

            //<data>
            //  <ret>succ</ret>
            //  <name>白玫瑰</name>
            //  <fruitpic>http://img.kaixin001.com.cn//i2/house/garden/crop/meigui_w.swf</fruitpic>
            //  <fruit_minprice>4900</fruit_minprice>
            //  <fruit_maxprice>5100</fruit_maxprice>
            //  <fruitnum>18</fruitnum>
            //  <selfnum>18</selfnum>
            //  <bpresent>1</bpresent>
            //  <fruitprice>5000</fruitprice>
            //</data>

            //<data>
            //  <ret>succ</ret>
            //  <name>粉玫瑰</name>
            //  <fruitpic>http://img.kaixin001.com.cn//i2/house/garden/crop/meigui_f.swf</fruitpic>
            //  <fruit_minprice>4400</fruit_minprice>
            //  <fruit_maxprice>4600</fruit_maxprice>
            //  <fruitnum>18</fruitnum>
            //  <selfnum>18</selfnum>
            //  <bpresent>1</bpresent>
            //  <fruitprice>4500</fruitprice>
            //</data>

            //<data>
            //  <ret>succ</ret>
            //  <name>黄玫瑰</name>
            //  <fruitpic>http://img.kaixin001.com.cn//i2/house/garden/crop/meigui_y.swf</fruitpic>
            //  <fruit_minprice>4000</fruit_minprice>
            //  <fruit_maxprice>4100</fruit_maxprice>
            //  <fruitnum>15</fruitnum>
            //  <selfnum>15</selfnum>
            //  <bpresent>1</bpresent>
            //  <fruitprice>4050</fruitprice>
            //</data>

            try
            {
                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(feedback);
                if (objXmlDoc == null)
                    return null;

                PresentInfo present = new PresentInfo();

                //garden
                XmlNode objNode = objXmlDoc.SelectSingleNode(Constants.GARDEN_PRESENT_ROOT);
                if (objNode == null)
                    return null;

                present.Ret = objNode.SelectSingleNode(Constants.GARDEN_PRESENT_RET).InnerText;
                present.Name = objNode.SelectSingleNode(Constants.GARDEN_PRESENT_NAME).InnerText;
                present.FruitPic = objNode.SelectSingleNode(Constants.GARDEN_PRESENT_FRUITPIC).InnerText;
                present.FruitMinPrice = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.GARDEN_PRESENT_FRUITMINPRICE).InnerText);
                present.FruitMaxPrice = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.GARDEN_PRESENT_FRUITMAXPRICE).InnerText);
                present.FruitNum = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.GARDEN_PRESENT_FRUITNUM).InnerText);
                present.SelfNum = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.GARDEN_PRESENT_SELFNUM).InnerText);
                present.BPresent = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.GARDEN_PRESENT_BPRESENT).InnerText);
                present.FruitPrice = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.GARDEN_PRESENT_FRUITPRICE).InnerText);

                return present;
            }
            catch (Exception ex)
            {
                LogHelper.Write("转换成StealObject-" + feedback, ex);
                return null;
            }
        }
        #endregion

        #region ConvertToSellObject
        public static SellInfo ConvertToSellObject(string feedback)
        {
            //<data>
            //  <ret>succ</ret>
            //  <goodsname>胡萝卜</goodsname>
            //  <totalprice>70</totalprice>
            //  <num>2</num>
            //  <pic>http://img.kaixin001.com.cn//i2/house/garden/crop/huluobo.swf</pic>
            //  <all>0</all>
            //</data>
            
            //<data>
            //  <ret>succ</ret>
            //  <goodsname>
            //  </goodsname>
            //  <totalprice>0</totalprice>
            //  <num>0</num>
            //  <pic>
            //  </pic>
            //  <all>1</all>
            //</data>
            try
            {
                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(feedback);
                if (objXmlDoc == null)
                    return null;

                SellInfo sell = new SellInfo();

                //garden
                XmlNode objNode = objXmlDoc.SelectSingleNode(Constants.GARDEN_SELL_ROOT);
                if (objNode == null)
                    return null;

                sell.Ret = objNode.SelectSingleNode(Constants.GARDEN_SELL_RET).InnerText;
                sell.GoodsName = objNode.SelectSingleNode(Constants.GARDEN_SELL_GOODSNAME).InnerText;
                sell.TotalPrice = DataConvert.GetInt64(objNode.SelectSingleNode(Constants.GARDEN_SELL_TOTALPRICE).InnerText);
                sell.Num = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.GARDEN_SELL_NUM).InnerText);
                sell.Pic = objNode.SelectSingleNode(Constants.GARDEN_SELL_PIC).InnerText;
                sell.All = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.GARDEN_SELL_ALL).InnerText);

                return sell;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.ConvertToSellObject", feedback, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #region GetMySeeds
        public static Collection<SeedInfo> GetMySeeds(string strxml, ref int totalpage)
        {
            try
            {
                totalpage = 0;

                //<data><seed><item><seedid>1</seedid><num>10</num><fruitpic>http://img.kaixin001.com.cn//i2/house/garden/crop/huluobo.swf</fruitpic><name>胡萝卜</name></item><item><seedid>2</seedid><num>2</num><fruitpic>http://img.kaixin001.com.cn//i2/house/garden/crop/dabaicai.swf</fruitpic><name>大白菜</name></item><item><seedid>3</seedid><num>2</num><fruitpic>http://img.kaixin001.com.cn//i2/house/garden/crop/tudou.swf</fruitpic><name>土豆</name></item><item><seedid>4</seedid><num>2</num><fruitpic>http://img.kaixin001.com.cn//i2/house/garden/crop/qianniuhua.swf</fruitpic><name>牵牛花</name></item></seed><ret>succ</ret><totalpage>2</totalpage></data>
                //<data><seed><item><seedid>5</seedid><num>2</num><fruitpic>http://img.kaixin001.com.cn//i2/house/garden/crop/huanggua.swf</fruitpic><name>黄瓜</name></item><item><seedid>6</seedid><num>2</num><fruitpic>http://img.kaixin001.com.cn//i2/house/garden/crop/lajiao.swf</fruitpic><name>辣椒</name></item><item><seedid>7</seedid><num>2</num><fruitpic>http://img.kaixin001.com.cn//i2/house/garden/crop/yumi.swf</fruitpic><name>玉米</name></item></seed><ret>succ</ret><totalpage>2</totalpage></data>
                //<data><ret>succ</ret><totalpage>2</totalpage></data>
                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(strxml);
                if (objXmlDoc == null)
                    return null;                

                if (objXmlDoc.SelectSingleNode(Constants.GARDEN_SEED_ROOT + Constants.CHAR_SLASH + Constants.GARDEN_SEED_SEED) == null)
                {
                    return new Collection<SeedInfo>();
                }

                totalpage = DataConvert.GetInt32(objXmlDoc.SelectSingleNode(Constants.GARDEN_SEED_ROOT + Constants.CHAR_SLASH + Constants.GARDEN_SEED_TOTALPAGE).InnerText);

                DataView dv = GetData(objXmlDoc, Constants.GARDEN_SEED_ROOT + Constants.CHAR_SLASH + Constants.GARDEN_SEED_SEED);

                Collection<SeedInfo> seeds = new Collection<SeedInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    SeedInfo seed = new SeedInfo();
                    seed.SeedId = DataConvert.GetInt32(dv.Table.Rows[ix][0]);
                    seed.Num = DataConvert.GetInt32(dv.Table.Rows[ix][1]);
                    //seed.FruitPic = dv.Table.Rows[ix][2].ToString();
                    seed.Name = dv.Table.Rows[ix][3].ToString();
                    seeds.Add(seed);
                }
                return seeds;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtr.GetMySeeds", strxml, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #region GetMyGardenWarehouse
        public static Collection<FruitInfo> GetMyGardenWarehouse(string strxml)
        {
            try
            {
                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(strxml);
                if (objXmlDoc == null)
                    return null;

                if (objXmlDoc.SelectSingleNode("data/fruit") == null)
                {
                    return new Collection<FruitInfo>();
                }

                DataView dv = GetData(objXmlDoc, "data/fruit");

                Collection<FruitInfo> fruits = new Collection<FruitInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    FruitInfo fruit = new FruitInfo();
                    fruit.FruitId = DataConvert.GetInt32(dv.Table.Rows[ix]["seedid"]);
                    fruit.Num = DataConvert.GetInt32(dv.Table.Rows[ix]["num"]);
                    fruit.Name = DataConvert.GetString(dv.Table.Rows[ix]["name"]);
                    fruits.Add(fruit);
                }
                return fruits;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetMyGardenWarehouse", strxml, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #endregion

        #region Ranch

        #region GetRanch
        public static RanchInfo GetRanch(string strxml)
        {
            try
            {
                if (String.IsNullOrEmpty(strxml))
                    return null;

                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(strxml);
                if (objXmlDoc == null)
                    return null;

                RanchInfo ranch = new RanchInfo();

                //ranch
                XmlNode objNode = objXmlDoc.SelectSingleNode(Constants.RANCH_ROOT + Constants.CHAR_SLASH + Constants.RANCH_ACCOUNT);
                if (objNode == null)
                    return null;

                ranch.Rank = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.RANCH_RANK).InnerText);
                ranch.RankTip = objNode.SelectSingleNode(Constants.RANCH_RANKTIP).InnerText;
                ranch.Name = objNode.SelectSingleNode(Constants.RANCH_NAME).InnerText;
                ranch.CashTip = objNode.SelectSingleNode(Constants.RANCH_CASHTIP).InnerText;
                ranch.Cash = GetCash(ranch.CashTip);
                ranch.TCharms = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.RANCH_TCHARMS).InnerText);

                //水量：0格/<font color='#FF0000'>需加水</font> 牧草：30棵<font color='#FF0000'>(需加草)</font><br><font color='#666666'>距吃光还有约20小时</font>
                ranch.Water = DataConvert.GetInt32(objXmlDoc.SelectSingleNode(Constants.RANCH_ROOT + Constants.CHAR_SLASH + Constants.RANCH_WATER).InnerText);
                ranch.WaterTips = DataConvert.GetString(objXmlDoc.SelectSingleNode(Constants.RANCH_ROOT + Constants.CHAR_SLASH + Constants.RANCH_WATERTIPS).InnerText);
                ranch.Grass = DataConvert.GetInt32(objXmlDoc.SelectSingleNode(Constants.RANCH_ROOT + Constants.CHAR_SLASH + Constants.RANCH_GRASS).InnerText);
                ranch.GrassTips = DataConvert.GetString(objXmlDoc.SelectSingleNode(Constants.RANCH_ROOT + Constants.CHAR_SLASH + Constants.RANCH_GRASSTIPS).InnerText);

                //animal products
                DataView dv = GetData(objXmlDoc, Constants.RANCH_ROOT + Constants.CHAR_SLASH + Constants.RANCH_PRODUCT2);

                Collection<AnimalProductInfo> animalProducts = new Collection<AnimalProductInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    try
                    {
                        AnimalProductInfo animalProduct = new AnimalProductInfo();
                        animalProduct.Uid = DataConvert.GetInt32(dv.Table.Rows[ix]["uid"]);
                        animalProduct.Aid = DataConvert.GetInt32(dv.Table.Rows[ix]["aid"]);
                        animalProduct.Type = DataConvert.GetInt32(dv.Table.Rows[ix]["type"]);
                        animalProduct.Num = DataConvert.GetInt32(dv.Table.Rows[ix]["num"]);
                        animalProduct.StealNum = DataConvert.GetInt32(dv.Table.Rows[ix]["stealnum"]);
                        animalProduct.MTtime = DataConvert.GetString(dv.Table.Rows[ix]["mtime"]);
                        animalProduct.Ppic = DataConvert.GetString(dv.Table.Rows[ix]["ppic"]);
                        animalProduct.TName = DataConvert.GetString(dv.Table.Rows[ix]["tname"]);
                        animalProduct.SKey = DataConvert.GetString(dv.Table.Rows[ix]["skey"]);
                        animalProduct.PName = DataConvert.GetString(dv.Table.Rows[ix]["pname"]);
                        animalProduct.Tips = DataConvert.GetString(dv.Table.Rows[ix]["tips"]);
                        animalProduct.Oa = DataConvert.GetString(dv.Table.Rows[ix]["oa"]);
                        animalProducts.Add(animalProduct);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Write("ConfigCtrl.GetRanch(Get animal products)", strxml, ex, LogSeverity.Warn);
                        continue;
                    }
                }
                ranch.AnimalProducts = animalProducts;

                //Animals
                dv = GetData(objXmlDoc, Constants.RANCH_ROOT + Constants.CHAR_SLASH + Constants.RANCH_ANIMALS);

                Collection<AnimalInfo> animals = new Collection<AnimalInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    try
                    {
                        AnimalInfo animal = new AnimalInfo();
                        animal.AnimalSid = DataConvert.GetInt64(dv.Table.Rows[ix]["animalsid"]);
                        animal.BProduct = DataConvert.GetInt32(dv.Table.Rows[ix]["bproduct"]);
                        animal.BStat = DataConvert.GetInt32(dv.Table.Rows[ix]["bstat"]);
                        animal.Tips = DataConvert.GetString(dv.Table.Rows[ix]["tips"]);
                        animal.Status = DataConvert.GetInt32(dv.Table.Rows[ix]["status"]);
                        animal.AName = DataConvert.GetString(dv.Table.Rows[ix]["aname"]);                        
                        animal.PAction = DataConvert.GetString(dv.Table.Rows[ix]["paction"]);
                        animals.Add(animal);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Write("ConfigCtrl.GetRanch(Get animals)", strxml, ex, LogSeverity.Error);
                        continue;
                    }
                }
                ranch.Animals = animals;

                //Foods
                dv = GetData(objXmlDoc, Constants.RANCH_ROOT + Constants.CHAR_SLASH + Constants.RANCH_FOODS);

                Collection<FoodItemInfo> foods = new Collection<FoodItemInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    try
                    {
                        FoodItemInfo food = new FoodItemInfo();
                        food.Tips = DataConvert.GetString(dv.Table.Rows[ix][1]);
                        food.SeedId = DataConvert.GetInt32(dv.Table.Rows[ix][4]);
                        food.Grass = DataConvert.GetInt32(dv.Table.Rows[ix][5]);
                        foods.Add(food);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Write("ConfigCtrl.GetRanch(Get Foods)", strxml, ex, LogSeverity.Warn);
                        continue;
                    }
                }
                ranch.Foods = foods;

                return ranch;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetRanch", strxml, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #region GetOriginalCalves
        public static Collection<CalfInfo> GetOriginalCalves(string content)
        {
            try
            {
                if (String.IsNullOrEmpty(content))
                    return new Collection<CalfInfo>();

                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(content);

                DataView dv = GetData(objXmlDoc, Constants.RANCH_CALF_ROOT + Constants.CHAR_SLASH + Constants.RANCH_CALF_ANIMALS);

                Collection<CalfInfo> calves = new Collection<CalfInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    CalfInfo calf = new CalfInfo();
                    calf.Name = dv.Table.Rows[ix][0].ToString();
                    calf.AId = DataConvert.GetInt32(dv.Table.Rows[ix][2]);
                    calf.SKey = DataConvert.GetString(dv.Table.Rows[ix][3]);
                    calf.Price = DataConvert.GetInt32(dv.Table.Rows[ix][4]);
                    calves.Add(calf);
                }

                return calves;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetOriginalCalves", content, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #region GetCalvesInShop
        public static Collection<CalfInfo> GetCalvesInShop()
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_CALFSLISTMASTERDATA);
                if (objXmlDoc == null)
                    return null;

                DataView dv = GetData(objXmlDoc, Constants.RANCH_CALF_ROOT + Constants.CHAR_SLASH + Constants.RANCH_CALF_ANIMALS);

                Collection<CalfInfo> calfs = new Collection<CalfInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    CalfInfo calf = new CalfInfo();
                    calf.Name = dv.Table.Rows[ix][0].ToString();
                    calf.AId = DataConvert.GetInt32(dv.Table.Rows[ix][1]);
                    calf.Price = DataConvert.GetInt32(dv.Table.Rows[ix][2]);
                    calfs.Add(calf);
                }

                return calfs;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetCalvesInShop", ex);
                return null;
            }
        }
        #endregion

        #region SetCalvesInShop
        public static bool SetCalvesInShop(Collection<CalfInfo> calves)
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_CALFSLISTMASTERDATA);
                if (objXmlDoc == null)
                    return false;

                XmlNode objAnimalNode = objXmlDoc.SelectSingleNode(Constants.RANCH_CALF_ROOT + Constants.CHAR_SLASH + Constants.RANCH_CALF_ANIMALS);
                objAnimalNode.RemoveAll();
                foreach (CalfInfo calf in calves)
                {
                    XmlElement objChildNode = objXmlDoc.CreateElement(Constants.RANCH_CALF_ITEM);
                    objAnimalNode.AppendChild(objChildNode);
                    XmlElement emtName = objXmlDoc.CreateElement(Constants.RANCH_CALF_NAME);
                    emtName.InnerText = calf.Name;
                    XmlElement emtAId = objXmlDoc.CreateElement(Constants.RANCH_CALF_AID);
                    emtAId.InnerText = calf.AId.ToString();
                    XmlElement emtPrice = objXmlDoc.CreateElement(Constants.RANCH_CALF_PRICE);
                    emtPrice.InnerText = calf.Price.ToString();
                    objChildNode.AppendChild(emtName);
                    objChildNode.AppendChild(emtAId);
                    objChildNode.AppendChild(emtPrice);
                }

                return SetMasterDataFile(objXmlDoc, Constants.FILE_CALFSLISTMASTERDATA);
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.SetCalfsInShop", ex);
                return false;
            }
        }        
        #endregion

        #region GetAnimalProducts
        public static Collection<ProductInfo> GetAnimalProducts()
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_ANIMALPRODUCTMASTERDATA);
                if (objXmlDoc == null)
                    return null;

                DataView dv = GetData(objXmlDoc, "data/animalproducts");

                Collection<ProductInfo> products = new Collection<ProductInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    ProductInfo product = new ProductInfo();
                    product.Aid = DataConvert.GetInt32(dv.Table.Rows[ix]["aid"]);
                    product.Type = DataConvert.GetInt32(dv.Table.Rows[ix]["type"]);
                    product.Name = dv.Table.Rows[ix]["name"].ToString();
                    product.Price = DataConvert.GetInt32(dv.Table.Rows[ix]["price"]);
                    products.Add(product);
                }

                return products;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetAnimalProducts", ex);
                return null;
            }
        }
        #endregion

        #region GetMyFoods
        public static Collection<FoodInfo> GetMyFoods(string strxml, ref int totalpage)
        {
            try
            {
                totalpage = 0;

                //<data><food><item><seedid>63</seedid><num>16</num><pic>http://img.kaixin001.com.cn//i2/house/garden/crop3/gouweiba.swf</pic><name>牧草</name></item></food><ret>succ</ret><totalpage>1</totalpage></data>
                //<data>
                //  <food>
                //    <item>
                //      <seedid>63</seedid>
                //      <num>16</num>
                //      <pic>http://img.kaixin001.com.cn//i2/house/garden/crop3/gouweiba.swf</pic>
                //      <name>牧草</name>
                //    </item>
                //  </food>
                //  <ret>succ</ret>
                //  <totalpage>1</totalpage>
                //</data>
                //<data><ret>succ</ret><totalpage>0</totalpage></data>
                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(strxml);
                if (objXmlDoc == null)
                    return null;

                if (objXmlDoc.SelectSingleNode(Constants.RANCH_FOOD_ROOT + Constants.CHAR_SLASH + Constants.RANCH_FOOD_FOOD) == null)
                {
                    return new Collection<FoodInfo>();
                }

                totalpage = DataConvert.GetInt32(objXmlDoc.SelectSingleNode(Constants.RANCH_FOOD_ROOT + Constants.CHAR_SLASH + Constants.RANCH_FOOD_TOTALPAGE).InnerText);

                DataView dv = GetData(objXmlDoc, Constants.RANCH_FOOD_ROOT + Constants.CHAR_SLASH + Constants.RANCH_FOOD_FOOD);

                Collection<FoodInfo> foods = new Collection<FoodInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    FoodInfo food = new FoodInfo();
                    food.SeedId = DataConvert.GetInt32(dv.Table.Rows[ix]["seedid"]);
                    food.Num = DataConvert.GetInt32(dv.Table.Rows[ix]["num"]);
                    food.Name = dv.Table.Rows[ix]["name"].ToString();
                    foods.Add(food);
                }
                return foods;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtr.GetMyFoods", strxml, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #region GetBreedAnimals
        public static Collection<BreedableInfo> GetBreedAnimals(string strxml)
        {
            try
            {
                if (String.IsNullOrEmpty(strxml))
                    return null;

                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(strxml);
                if (objXmlDoc == null)
                    return null;

                //animals
                DataView dv = GetData(objXmlDoc, Constants.RANCH_BREED_ROOT + Constants.CHAR_SLASH + Constants.RANCH_BREED_BREED);

                Collection<BreedableInfo> breedanimals = new Collection<BreedableInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    BreedableInfo animal = new BreedableInfo();
                    animal.Uid = DataConvert.GetInt32(dv.Table.Rows[ix][0]);
                    animal.AnimalSid = DataConvert.GetInt32(dv.Table.Rows[ix][1]);
                    animal.Aid = DataConvert.GetInt32(dv.Table.Rows[ix][2]);
                    animal.Status = DataConvert.GetInt32(dv.Table.Rows[ix][3]);
                    animal.BTime = DataConvert.GetString(dv.Table.Rows[ix][4]);
                    animal.BUid = DataConvert.GetInt32(dv.Table.Rows[ix][5]);
                    animal.BNum = DataConvert.GetInt32(dv.Table.Rows[ix][6]);
                    animal.CTime = DataConvert.GetString(dv.Table.Rows[ix][7]);
                    animal.GTime = DataConvert.GetString(dv.Table.Rows[ix][8]);
                    animal.FTime = DataConvert.GetString(dv.Table.Rows[ix][9]);
                    animal.Grow = DataConvert.GetInt32(dv.Table.Rows[ix][10]);
                    animal.PTime = DataConvert.GetString(dv.Table.Rows[ix][11]);
                    animal.PNum = DataConvert.GetInt32(dv.Table.Rows[ix][12]);
                    animal.Ptype = DataConvert.GetInt32(dv.Table.Rows[ix][13]);
                    animal.DayNum = DataConvert.GetInt32(dv.Table.Rows[ix][14]);
                    animal.FStatus = DataConvert.GetInt32(dv.Table.Rows[ix][15]);
                    animal.Puid = DataConvert.GetInt32(dv.Table.Rows[ix][16]);
                    animal.BsKey = DataConvert.GetString(dv.Table.Rows[ix][17]);
                    animal.Pic = DataConvert.GetString(dv.Table.Rows[ix][18]);
                    animal.TipsText = DataConvert.GetString(dv.Table.Rows[ix][19]);
                    breedanimals.Add(animal);
                }

                return breedanimals;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetBreedAnimals", strxml, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #region GetBreedCards
        public static Collection<BreedCardInfo> GetBreedCards(string strxml)
        {
            try
            {
                if (String.IsNullOrEmpty(strxml))
                    return null;

                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(strxml);
                if (objXmlDoc == null)
                    return null;

                //animals
                DataView dv = GetData(objXmlDoc, Constants.RANCH_BREEDCARD_ROOT);

                Collection<BreedCardInfo> breedcards = new Collection<BreedCardInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    BreedCardInfo breedcard = new BreedCardInfo();
                    breedcard.Fuid = DataConvert.GetInt32(dv.Table.Rows[ix][0]);
                    breedcard.Num = DataConvert.GetInt32(dv.Table.Rows[ix][1]);
                    breedcard.TName = DataConvert.GetString(dv.Table.Rows[ix][3]);
                    breedcard.RealName = DataConvert.GetString(dv.Table.Rows[ix][4]);
                    breedcard.TipsText = DataConvert.GetString(dv.Table.Rows[ix][5]);
                    breedcards.Add(breedcard);
                }

                return breedcards;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetBreedCards", strxml, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #region ConvertToFeedObject
        public static FeedInfo ConvertToFeedObject(string feedback)
        {
            //<data>
            //  <ret>succ</ret>
            //  <grasstips>牧草：72棵&lt;font color='#FF0000'&gt;(需加草)&lt;/font&gt;&lt;br&gt;&lt;font color='#666666'&gt;距吃光还有约288小时&lt;/font&gt;</grasstips>
            //  <grass>72</grass>
            //  <animalstips>
            //  </animalstips>
            //</data>
            try
            {
                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(feedback);
                if (objXmlDoc == null)
                    return null;

                FeedInfo feed = new FeedInfo();

                //garden
                XmlNode objNode = objXmlDoc.SelectSingleNode(Constants.RANCH_FEED_ROOT);
                if (objNode == null)
                    return null;

                feed.Ret = objNode.SelectSingleNode(Constants.RANCH_FEED_RET).InnerText;
                feed.GrassTips = objNode.SelectSingleNode(Constants.RANCH_FEED_GRASSTIPS).InnerText;
                feed.Grass = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.RANCH_FEED_GRASS).InnerText);
                feed.AnimalsTips = objNode.SelectSingleNode(Constants.RANCH_FEED_ANIMALSTIPS).InnerText;

                return feed;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.ConvertToFeedObject", feedback, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #region ConvertToWaterObject
        public static WaterInfo ConvertToWaterObject(string feedback)
        {
            //<data>
            //  <ret>succ</ret>
            //  <watertips>水量：100格&lt;br&gt;&lt;font color='#666666'&gt;距喝光还有约400小时&lt;/font&gt;</watertips>
            //  <tips>
            //  </tips>
            //</data>
            try
            {
                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(feedback);
                if (objXmlDoc == null)
                    return null;

                WaterInfo water = new WaterInfo();

                //garden
                XmlNode objNode = objXmlDoc.SelectSingleNode(Constants.RANCH_WATER_ROOT);
                if (objNode == null)
                    return null;

                water.Ret = objNode.SelectSingleNode(Constants.RANCH_WATER_RET).InnerText;
                water.WaterTips = objNode.SelectSingleNode(Constants.RANCH_WATER_WATERTIPS).InnerText;
                water.Tips = objNode.SelectSingleNode(Constants.RANCH_WATER_TIPS).InnerText;

                return water;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.ConvertToFeedObject", feedback, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #region ConvertToStealProductObject
        public static StealProductInfo ConvertToStealProductObject(string feedback)
        {
            //<data>
            //  <ptype>1</ptype>
            //  <skey>hen</skey>
            //  <action>steal</action>
            //  <num>1</num>
            //  <ppic>http://img.kaixin001.com.cn//i2/house/ranch/animals/egg.swf</ppic>
            //  <ret>succ</ret>
            //</data>

            //<data>
            //  <ptype>1</ptype>
            //  <skey>hen</skey>
            //  <action>steal</action>
            //  <ret>fail</ret>
            //  <reason>已偷过，做人要厚道</reason>
            //</data>
            try
            {
                if (String.IsNullOrEmpty(feedback))
                    return null;

                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(feedback);
                if (objXmlDoc == null)
                    return null;

                StealProductInfo stealproduct = new StealProductInfo();

                //garden
                XmlNode objNode = objXmlDoc.SelectSingleNode(Constants.RANCH_STEAL_ROOT);
                if (objNode == null)
                    return null;

                stealproduct.PType = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.RANCH_STEAL_PTYPE).InnerText);
                stealproduct.SKey = objNode.SelectSingleNode(Constants.RANCH_STEAL_SKEY).InnerText;
                stealproduct.Action = objNode.SelectSingleNode(Constants.RANCH_STEAL_ACTION).InnerText;
                stealproduct.Ret = objNode.SelectSingleNode(Constants.RANCH_STEAL_RET).InnerText;
                if (stealproduct.Ret == "succ")
                    stealproduct.Num = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.RANCH_STEAL_NUM).InnerText);
                else
                    stealproduct.Reason = objNode.SelectSingleNode(Constants.RANCH_STEAL_REASON).InnerText;

                return stealproduct;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.ConvertToStealProductObject", feedback, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #region ConvertToMakeProductObject
        public static MakeProductInfo ConvertToMakeProductObject(string feedback)
        {
            //<data>
            //  <action>product</action>
            //  <ret>succ</ret>
            //  <skey>hen</skey>
            //  <ptips>已成功将朱自克的芦花母鸡赶去产蛋&lt;br&gt;产蛋需10分种，10分钟后再来偷</ptips>
            //  <bproduct>1</bproduct>
            //  <leftptime>10</leftptime>
            //  <tips>&lt;font color='#FF0000'&gt;产蛋中&lt;/font&gt;&lt;br&gt;预计产量：10&lt;br&gt;&lt;font color='#666666'&gt;距离可收获还有10分&lt;/font&gt;</tips>
            //  <pic>http://img.kaixin001.com.cn//i2/house/ranch/animals/hen.swf</pic>
            //  <tname>&lt;img src='http://img.kaixin001.com.cn//i2/house/ranch/animals/hen_logo1.swf' width='25' height='25' hspace='0' vspace='0'&gt;&lt;br&gt;&lt;br&gt;芦花母鸡</tname>
            //</data>

            //<data>
            //  <action>product</action>
            //  <ret>fail</ret>
            //  <reason>该动物挨饿中，不能生产</reason>
            //</data>
            try
            {
                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(feedback);
                if (objXmlDoc == null)
                    return null;

                MakeProductInfo makeproduct = new MakeProductInfo();

                //garden
                XmlNode objNode = objXmlDoc.SelectSingleNode(Constants.RANCH_MAKEPRODUCT_ROOT);
                if (objNode == null)
                    return null;

                makeproduct.Action = objNode.SelectSingleNode(Constants.RANCH_MAKEPRODUCT_ACTION).InnerText;
                makeproduct.Ret = objNode.SelectSingleNode(Constants.RANCH_MAKEPRODUCT_RET).InnerText;
                if (makeproduct.Ret == "fail")
                    makeproduct.Reason = objNode.SelectSingleNode(Constants.RANCH_MAKEPRODUCT_REASON).InnerText;
                else
                {
                    makeproduct.SKey = objNode.SelectSingleNode(Constants.RANCH_MAKEPRODUCT_SKEY).InnerText;
                    makeproduct.PTips = objNode.SelectSingleNode(Constants.RANCH_MAKEPRODUCT_PTIPS).InnerText;
                    makeproduct.BProduct = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.RANCH_MAKEPRODUCT_BPRODUCT).InnerText);
                    makeproduct.LeftPTime = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.RANCH_MAKEPRODUCT_LEFTPTIME).InnerText);
                    makeproduct.Tips = objNode.SelectSingleNode(Constants.RANCH_MAKEPRODUCT_TIPS).InnerText;
                    makeproduct.Pic = objNode.SelectSingleNode(Constants.RANCH_MAKEPRODUCT_PIC).InnerText;
                    makeproduct.TName = objNode.SelectSingleNode(Constants.RANCH_MAKEPRODUCT_TNAME).InnerText;
                }

                return makeproduct;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.ConvertToMakeProductObject", feedback, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #region ConvertToBreedAnimalObject
        public static BreedAnimalInfo ConvertToBreedAnimalObject(string feedback)
        {
            //<data>
            //  <ret>succ</ret>
            //  <succtips>你的芦花母鸡和朱自克公鸡卡在产蛋期配种成功!&lt;br&gt;24小时内将产下柴鸡蛋（每只30元）</succtips>
            //  <bproduct>0</bproduct>
            //  <leftptime>0</leftptime>
            //  <tips>产蛋期&lt;font color='#FF0000'&gt;(已配种)&lt;/font&gt;&lt;br&gt;距离下次产蛋：11分&lt;br&gt;预计产量：10&lt;br&gt;&lt;font color='#666666'&gt;距不能产蛋还有16小时52分&lt;/font&gt;</tips>
            //  <skey>hen</skey>
            //  <pic>http://img.kaixin001.com.cn//i2/house/ranch/animals/hen.swf</pic>
            //  <tname>&lt;img src='http://img.kaixin001.com.cn//i2/house/ranch/animals/hen_logo1.swf' width='25' height='25' hspace='0' vspace='0'&gt;&lt;br&gt;&lt;br&gt;芦花母鸡</tname>
            //  <animalsid>2441805</animalsid>
            //</data>

            try
            {
                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(feedback);
                if (objXmlDoc == null)
                    return null;

                BreedAnimalInfo breedanimal = new BreedAnimalInfo();

                //root node
                XmlNode objNode = objXmlDoc.SelectSingleNode(Constants.RANCH_BREEDANIMAL_ROOT);
                if (objNode == null)
                    return null;

                breedanimal.Ret = objNode.SelectSingleNode(Constants.RANCH_BREEDANIMAL_RET).InnerText;
                breedanimal.Succtips = objNode.SelectSingleNode(Constants.RANCH_BREEDANIMAL_SUCCTIPS).InnerText;
                breedanimal.BProduct = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.RANCH_BREEDANIMAL_BPRODUCT).InnerText);
                breedanimal.LeftPTime = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.RANCH_BREEDANIMAL_LEFTPTIME).InnerText);
                breedanimal.Tips = objNode.SelectSingleNode(Constants.RANCH_BREEDANIMAL_TIPS).InnerText;
                breedanimal.SKey = objNode.SelectSingleNode(Constants.RANCH_BREEDANIMAL_SKEY).InnerText;
                breedanimal.Pic = objNode.SelectSingleNode(Constants.RANCH_BREEDANIMAL_PIC).InnerText;
                breedanimal.TName = objNode.SelectSingleNode(Constants.RANCH_BREEDANIMAL_TNAME).InnerText;
                breedanimal.AnimalSid = DataConvert.GetInt32(objNode.SelectSingleNode(Constants.RANCH_BREEDANIMAL_ANIMALSID).InnerText);


                return breedanimal;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.ConvertToBreedAnimalObject", feedback, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #region GetMyWarehouseProduct
        public static Collection<ProductInfo> GetMyWarehouseProduct(string strxml)
        {
            try
            {
                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(strxml);
                if (objXmlDoc == null)
                    return null;

                if (objXmlDoc.SelectSingleNode(Constants.GARDEN_WAREHOUSE_ROOT + Constants.CHAR_SLASH + Constants.GARDEN_WAREHOUSE_FRUIT) == null)
                {
                    return new Collection<ProductInfo>();
                }

                DataView dv = GetData(objXmlDoc, Constants.GARDEN_WAREHOUSE_ROOT + Constants.CHAR_SLASH + Constants.GARDEN_WAREHOUSE_FRUIT);

                Collection<ProductInfo> products = new Collection<ProductInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    ProductInfo product = new ProductInfo();
                    product.Aid = DataConvert.GetInt32(dv.Table.Rows[ix]["aid"]);
                    product.Num = DataConvert.GetInt32(dv.Table.Rows[ix]["num"]);
                    product.Type = DataConvert.GetInt32(dv.Table.Rows[ix]["type"]);
                    product.Pic = dv.Table.Rows[ix]["pic"].ToString();
                    product.Name = dv.Table.Rows[ix]["name"].ToString();
                    products.Add(product);
                }
                return products;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetMyWarehouseProduct", strxml, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #endregion

        #region Fish

        #region GetPond
        public static PondInfo GetPond(string strxml)
        {
            try
            {
                if (String.IsNullOrEmpty(strxml))
                    return null;

                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(strxml);
                if (objXmlDoc == null)
                    return null;

                PondInfo pond = new PondInfo();

                pond.Shakable = DataConvert.GetInt32(objXmlDoc.SelectSingleNode("conf/shake").InnerText) == 1 ? true : false;
                pond.Netable = DataConvert.GetInt32(objXmlDoc.SelectSingleNode("conf/netable").InnerText) == 1 ? true : false;
                pond.SickTips = DataConvert.GetString(objXmlDoc.SelectSingleNode("conf/sicktips").InnerText);
                pond.NnetFishTips = DataConvert.GetString(objXmlDoc.SelectSingleNode("conf/nnetfishtips").InnerText);

                //pond
                XmlNode objNode = objXmlDoc.SelectSingleNode("conf/account");
                if (objNode == null)
                    return null;

                pond.Title = DataConvert.GetString(objNode.SelectSingleNode("title").InnerText);
                pond.Rank = DataConvert.GetInt32(objNode.SelectSingleNode("rank").InnerText.Replace("级", ""));
                pond.RankTip = JsonHelper.FiltrateHtmlTags(DataConvert.GetString(objNode.SelectSingleNode("ranktips").InnerText));                
                pond.CashTips = JsonHelper.FiltrateHtmlTags(DataConvert.GetString(objNode.SelectSingleNode("cashtips").InnerText));
                pond.Cash = DataConvert.GetInt64(JsonHelper.GetMid(pond.CashTips, "现金：", "元"));
                pond.Fish = DataConvert.GetString(objNode.SelectSingleNode("fish").InnerText);
                pond.FishTips = DataConvert.GetString(objNode.SelectSingleNode("fishtips").InnerText);
                
                //fishs
                DataView dv = GetData(objXmlDoc, "conf/fish");

                Collection<FishInfo> fishs = new Collection<FishInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    try
                    {
                        FishInfo fish = new FishInfo();
                        fish.FishId = DataConvert.GetInt64(dv.Table.Rows[ix]["fishid"]);
                        fish.BProduct = DataConvert.GetInt32(dv.Table.Rows[ix]["bproduct"]);
                        fish.Tips = JsonHelper.FiltrateHtmlTags(DataConvert.GetString(dv.Table.Rows[ix]["tips"]));
                        fish.CurrentWeight = DataConvert.GetDecimal(JsonHelper.GetMid(fish.Tips, "当前重", "斤"));
                        fish.MaxWeight = DataConvert.GetDecimal(JsonHelper.GetMid(fish.Tips, "最大", "斤"));
                        fish.ProductUrl = DataConvert.GetString(dv.Table.Rows[ix]["producturl"]);
                        if (fish.Tips.Contains("千年蚌精") && !String.IsNullOrEmpty(fish.ProductUrl))
                        {
                            pond.BangKeJing = true;
                            pond.BangKeJingFishId = fish.FishId;
                        }
                        fishs.Add(fish);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Write("ConfigCtrl.GetPond(Get fish list)", strxml, ex, LogSeverity.Error);
                        continue;
                    }
                }
                pond.Fishs = fishs;

                //fishers
                dv = GetData(objXmlDoc, "conf/fisher");

                Collection<FisherInfo> fishers = new Collection<FisherInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    try
                    {
                        FisherInfo fisher = new FisherInfo();
                        fisher.UId = DataConvert.GetInt32(dv.Table.Rows[ix]["uid"]);
                        fisher.Name = DataConvert.GetString(dv.Table.Rows[ix]["real_name"]);
                        fisher.Pos = DataConvert.GetInt32(dv.Table.Rows[ix]["pos"]);
                        fisher.TackleId = DataConvert.GetInt64(dv.Table.Rows[ix]["tackleid"]);
                        fisher.FStat = DataConvert.GetInt32(dv.Table.Rows[ix]["fstat"]);
                        fisher.BFish = DataConvert.GetInt32(dv.Table.Rows[ix]["bfish"]);
                        fisher.TTitle = JsonHelper.FiltrateHtmlTags(DataConvert.GetString(dv.Table.Rows[ix]["ttitle"]));
                        fishers.Add(fisher);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Write("ConfigCtrl.GetPond(Get fishers)", strxml, ex, LogSeverity.Error);
                        continue;
                    }
                }
                pond.Fishers = fishers;

                return pond;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetPond", strxml, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #region GetOriginalFishFrys
        public static Collection<FishFryInfo> GetOriginalFishFrys(string content)
        {
            try
            {
                if (String.IsNullOrEmpty(content))
                    return new Collection<FishFryInfo>();

                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(content);

                DataView dv = GetData(objXmlDoc, "data/fish");

                Collection<FishFryInfo> fishfrys = new Collection<FishFryInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    FishFryInfo fishfry = new FishFryInfo();
                    fishfry.FId = DataConvert.GetInt32(dv.Table.Rows[ix]["fid"]);
                    fishfry.Name = DataConvert.GetString(dv.Table.Rows[ix]["name"]);
                    fishfry.Price = DataConvert.GetInt32(dv.Table.Rows[ix]["price"]);
                    fishfry.MPrice = DataConvert.GetDecimal(dv.Table.Rows[ix]["mprice"]) * 10;
                    fishfry.FWeight = DataConvert.GetDecimal(dv.Table.Rows[ix]["fweight"]) / 10;
                    fishfry.Rank = DataConvert.GetInt32(dv.Table.Rows[ix]["rank"]);
                    fishfry.MaxWeight = DataConvert.GetDecimal(dv.Table.Rows[ix]["maxweight"]) / 10;
                    fishfrys.Add(fishfry);
                }

                return fishfrys;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetOriginalFishFrys", content, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #region GetFishFrysInShop
        public static Collection<FishFryInfo> GetFishFrysInShop()
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_FISHFRYSMASTERDATA);
                if (objXmlDoc == null)
                    return null;

                DataView dv = GetData(objXmlDoc, "data/fishfrys");

                Collection<FishFryInfo> fishfrys = new Collection<FishFryInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    FishFryInfo fishfry = new FishFryInfo();
                    fishfry.FId = DataConvert.GetInt32(dv.Table.Rows[ix]["fid"]);
                    fishfry.Name = DataConvert.GetString(dv.Table.Rows[ix]["name"]);
                    fishfry.Price = DataConvert.GetInt32(dv.Table.Rows[ix]["price"]);
                    fishfry.MPrice = DataConvert.GetDecimal(dv.Table.Rows[ix]["mprice"]);
                    fishfry.FWeight = DataConvert.GetDecimal(dv.Table.Rows[ix]["fweight"]);
                    fishfry.Rank = DataConvert.GetInt32(dv.Table.Rows[ix]["rank"]);
                    fishfry.MaxWeight = DataConvert.GetDecimal(dv.Table.Rows[ix]["maxweight"]);
                    fishfrys.Add(fishfry);
                }

                return fishfrys;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetFishFrysInShop", ex);
                return null;
            }
        }
        #endregion

        #region SetFishFrysInShop
        public static bool SetFishFrysInShop(Collection<FishFryInfo> fishfrys)
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_FISHFRYSMASTERDATA);
                if (objXmlDoc == null)
                    return false;

                XmlNode objNode = objXmlDoc.SelectSingleNode("data/fishfrys");
                objNode.RemoveAll();
                foreach (FishFryInfo fishfry in fishfrys)
                {
                    XmlElement objChildNode = objXmlDoc.CreateElement("item");
                    objNode.AppendChild(objChildNode);
                    XmlElement emtFId = objXmlDoc.CreateElement("fid");
                    emtFId.InnerText = fishfry.FId.ToString();
                    XmlElement emtName = objXmlDoc.CreateElement("name");
                    emtName.InnerText = fishfry.Name.ToString();
                    XmlElement emtPrice = objXmlDoc.CreateElement("price");
                    emtPrice.InnerText = fishfry.Price.ToString();
                    XmlElement emtMPrice = objXmlDoc.CreateElement("mprice");
                    emtMPrice.InnerText = fishfry.MPrice.ToString();
                    XmlElement emtFWeight = objXmlDoc.CreateElement("fweight");
                    emtFWeight.InnerText = fishfry.FWeight.ToString();
                    XmlElement emtRank = objXmlDoc.CreateElement("rank");
                    emtRank.InnerText = fishfry.Rank.ToString();
                    XmlElement emtMaxWeight = objXmlDoc.CreateElement("maxweight");
                    emtMaxWeight.InnerText = fishfry.MaxWeight.ToString();
                    objChildNode.AppendChild(emtFId);
                    objChildNode.AppendChild(emtName);
                    objChildNode.AppendChild(emtPrice);
                    objChildNode.AppendChild(emtMPrice);
                    objChildNode.AppendChild(emtFWeight);
                    objChildNode.AppendChild(emtRank);
                    objChildNode.AppendChild(emtMaxWeight);
                }

                return SetMasterDataFile(objXmlDoc, Constants.FILE_FISHFRYSMASTERDATA);

            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.SetFishFrysInShop", ex);
                return false;
            }
        }
        #endregion
        
        #region GetOriginalFishTackles
        public static Collection<FishTackleInfo> GetOriginalFishTackles(string content)
        {
            try
            {
                if (String.IsNullOrEmpty(content))
                    return new Collection<FishTackleInfo>();

                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(content);

                DataView dv = GetData(objXmlDoc, "data/tackle");

                Collection<FishTackleInfo> fishtackles = new Collection<FishTackleInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    FishTackleInfo fishtackle = new FishTackleInfo();
                    fishtackle.TId = DataConvert.GetInt32(dv.Table.Rows[ix]["tid"]);
                    fishtackle.Name = DataConvert.GetString(dv.Table.Rows[ix]["name"]);
                    fishtackle.Price = DataConvert.GetInt32(dv.Table.Rows[ix]["price"]);
                    fishtackle.Rank = DataConvert.GetInt32(dv.Table.Rows[ix]["rank"]);
                    fishtackle.FMWeight = DataConvert.GetInt32(dv.Table.Rows[ix]["fmweight"]) / 10;
                    fishtackles.Add(fishtackle);
                }

                return fishtackles;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetOriginalFishTackles", content, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #region GetFishTacklesInShop
        public static Collection<FishTackleInfo> GetFishTacklesInShop()
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_FISHTACKLESMASTERDATA);
                if (objXmlDoc == null)
                    return null;

                DataView dv = GetData(objXmlDoc, "data/fishtackles");

                Collection<FishTackleInfo> fishtackles = new Collection<FishTackleInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    FishTackleInfo fishtackle = new FishTackleInfo();
                    fishtackle.TId = DataConvert.GetInt32(dv.Table.Rows[ix]["tid"]);
                    fishtackle.Name = DataConvert.GetString(dv.Table.Rows[ix]["name"]);
                    fishtackle.Price = DataConvert.GetInt32(dv.Table.Rows[ix]["price"]);
                    fishtackle.Rank = DataConvert.GetInt32(dv.Table.Rows[ix]["rank"]);
                    fishtackle.FMWeight = DataConvert.GetInt32(dv.Table.Rows[ix]["fmweight"]);                    
                    fishtackles.Add(fishtackle);
                }
                return fishtackles;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetFishTacklesInShop", ex);
                return null;
            }
        }
        #endregion

        #region SetFishTacklesInShop
        public static bool SetFishTacklesInShop(Collection<FishTackleInfo> fishtackles)
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_FISHTACKLESMASTERDATA);
                if (objXmlDoc == null)
                    return false;

                XmlNode objNode = objXmlDoc.SelectSingleNode("data/fishtackles");
                objNode.RemoveAll();
                foreach (FishTackleInfo fishtackle in fishtackles)
                {
                    XmlElement objChildNode = objXmlDoc.CreateElement("item");
                    objNode.AppendChild(objChildNode);
                    XmlElement emtTId = objXmlDoc.CreateElement("tid");
                    emtTId.InnerText = fishtackle.TId.ToString();
                    XmlElement emtName = objXmlDoc.CreateElement("name");
                    emtName.InnerText = fishtackle.Name.ToString();
                    XmlElement emtPrice = objXmlDoc.CreateElement("price");
                    emtPrice.InnerText = fishtackle.Price.ToString();
                    XmlElement emtRank = objXmlDoc.CreateElement("rank");
                    emtRank.InnerText = fishtackle.Rank.ToString();
                    XmlElement emtFMWeight = objXmlDoc.CreateElement("fmweight");
                    emtFMWeight.InnerText = fishtackle.FMWeight.ToString();
                    objChildNode.AppendChild(emtTId);
                    objChildNode.AppendChild(emtName);
                    objChildNode.AppendChild(emtRank);
                    objChildNode.AppendChild(emtPrice);
                    objChildNode.AppendChild(emtFMWeight);
                }

                return SetMasterDataFile(objXmlDoc, Constants.FILE_FISHTACKLESMASTERDATA);

            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.SetFishTacklesInShop", ex);
                return false;
            }
        }
        #endregion

        #region GetMyTackles
        public static Collection<FishTackleInfo> GetMyTackles(string content)
        {
            try
            {
                if (String.IsNullOrEmpty(content))
                    return new Collection<FishTackleInfo>();

                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(content);

                DataView dv = GetData(objXmlDoc, "data/tackle");

                Collection<FishTackleInfo> fishtackles = new Collection<FishTackleInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    FishTackleInfo fishtackle = new FishTackleInfo();
                    fishtackle.Status = DataConvert.GetInt32(dv.Table.Rows[ix]["status"]);
                    if (fishtackle.Status == 1)
                    {
                        fishtackle.BUse = DataConvert.GetInt32(dv.Table.Rows[ix]["buse"]);
                        fishtackle.TackleId = DataConvert.GetInt32(dv.Table.Rows[ix]["tackleid"]);
                        fishtackle.TId = DataConvert.GetInt32(dv.Table.Rows[ix]["tid"]);
                        fishtackle.Title = JsonHelper.FiltrateHtmlTags(DataConvert.GetString(dv.Table.Rows[ix]["title"]));
                    }
                    fishtackles.Add(fishtackle);
                }

                return fishtackles;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetMyTackles", content, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #region GetHelpFriend
        public static Collection<FriendInfo> GetHelpFriend(string strxml)
        {
            try
            {
                //<data>
                //  <help>
                //    <item>
                //      <uid>7995015</uid>
                //      <realname>石智星</realname>
                //      <online>0</online>
                //      <icon20>http://pic.kaixin001.com/logo/73/34/20_6733418_1.jpg</icon20>
                //      <extext>需帮忙</extext>
                //    </item>
                //    <item>
                //      <uid>6209015</uid>
                //      <realname>沈炳</realname>
                //      <online>0</online>
                //      <icon20>http://img.kaixin001.com.cn/i/20_0_0.gif</icon20>
                //      <extext>需帮忙</extext>
                //    </item>
                //    <item>
                //      <uid>6209093</uid>
                //      <realname>陈江铸</realname>
                //      <online>0</online>
                //      <icon20>http://img.kaixin001.com.cn/i/20_0_0.gif</icon20>
                //      <extext>需帮忙</extext>
                //    </item>
                //    <item>
                //      <uid>6209093</uid>
                //      <realname>李富县</realname>
                //      <online>0</online>
                //      <icon20>http://img.kaixin001.com.cn/i/20_0_0.gif</icon20>
                //      <extext>需帮忙</extext>
                //    </item>
                //  </help>
                //</data>
                if (String.IsNullOrEmpty(strxml))
                    return null;

                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(strxml);
                if (objXmlDoc == null)
                    return null;
               
                //fishs
                DataView dv = GetData(objXmlDoc, "data/help");
                Collection<FriendInfo> friends = new Collection<FriendInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    try
                    {
                        FriendInfo friend = new FriendInfo();
                        friend.Id = DataConvert.GetInt32(dv.Table.Rows[ix]["uid"]);
                        friend.Name = DataConvert.GetString(dv.Table.Rows[ix]["realname"]);
                        friends.Add(friend);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Write("ConfigCtrl.GetHelpFriend", strxml, ex, LogSeverity.Error);
                        continue;
                    }
                }

                return friends;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetHelpFriend", strxml, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #region GetMyWarehouseFish
        public static Collection<FishMaturedInfo> GetMyWarehouseFish(string strxml)
        {
            try
            {
                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(strxml);
                if (objXmlDoc == null)
                    return null;

                DataView dv = GetData(objXmlDoc, "data/fish");

                Collection<FishMaturedInfo> fishes = new Collection<FishMaturedInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    FishMaturedInfo fish = new FishMaturedInfo();
                    fish.UId = DataConvert.GetInt32(dv.Table.Rows[ix]["uid"]);
                    fish.FId = DataConvert.GetInt32(dv.Table.Rows[ix]["fid"]);
                    fish.TNnum = DataConvert.GetInt32(dv.Table.Rows[ix]["tnum"]);
                    fish.Weight = DataConvert.GetDecimal(dv.Table.Rows[ix]["weight"]) / 10;
                    fish.MaxWeight = DataConvert.GetDecimal(dv.Table.Rows[ix]["maxweight"]) / 10;
                    fishes.Add(fish);
                }
                return fishes;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetMyWarehouseFish", strxml, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #region GetMyWarehouseDetailFish
        public static Collection<FishMaturedInfo> GetMyWarehouseDetailFish(string strxml)
        {
            try
            {
                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(strxml);
                if (objXmlDoc == null)
                    return null;

                DataView dv = GetData(objXmlDoc, "data/fish");

                Collection<FishMaturedInfo> fishes = new Collection<FishMaturedInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    FishMaturedInfo fish = new FishMaturedInfo();
                    fish.UId = DataConvert.GetInt32(dv.Table.Rows[ix]["uid"]);
                    fish.FId = DataConvert.GetInt32(dv.Table.Rows[ix]["fid"]);
                    fish.Weight = DataConvert.GetDecimal(dv.Table.Rows[ix]["weight"]) / 10;
                    fishes.Add(fish);
                }
                return fishes;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetMyWarehouseDetailFish", strxml, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #region GetFishMaturedInMarket
        public static Collection<FishMaturedInfo> GetFishMaturedInMarket()
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_FISHMATUREDMASTERDATA);
                if (objXmlDoc == null)
                    return null;

                DataView dv = GetData(objXmlDoc, "data/fishmatured");

                Collection<FishMaturedInfo> fishmatureds = new Collection<FishMaturedInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    FishMaturedInfo fishmatured = new FishMaturedInfo();
                    fishmatured.FId = DataConvert.GetInt32(dv.Table.Rows[ix]["fid"]);
                    fishmatured.Name = DataConvert.GetString(dv.Table.Rows[ix]["name"]);
                    fishmatured.Price = DataConvert.GetDecimal(dv.Table.Rows[ix]["price"]);
                    fishmatured.Weight = DataConvert.GetDecimal(dv.Table.Rows[ix]["weight"]);
                    fishmatured.MaxWeight = DataConvert.GetDecimal(dv.Table.Rows[ix]["maxweight"]);
                    fishmatureds.Add(fishmatured);
                }

                return fishmatureds;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetFishFrysInShop", ex);
                return null;
            }
        }
        #endregion

        #endregion

        #region Rich

        #region GetAssetsInShop
        public static Collection<AssetInfo> GetAssetsInShop()
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_ASSETSMASTERDATA);
                if (objXmlDoc == null)
                    return null;

                DataView dv = GetData(objXmlDoc, "data/assets");

                Collection<AssetInfo> assets = new Collection<AssetInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    AssetInfo asset = new AssetInfo();
                    asset.IId = DataConvert.GetInt32(dv.Table.Rows[ix]["iid"]);
                    asset.Name = DataConvert.GetString(dv.Table.Rows[ix]["name"]);
                    asset.StandardPrice = DataConvert.GetInt64(dv.Table.Rows[ix]["standardprice"]);
                    asset.BuyRatio = DataConvert.GetDecimal(dv.Table.Rows[ix]["buyratio"]);
                    asset.BuyPrice = DataConvert.GetInt64(dv.Table.Rows[ix]["buyprice"]);
                    asset.SellRatio = DataConvert.GetDecimal(dv.Table.Rows[ix]["sellratio"]);
                    asset.SellPrice = DataConvert.GetInt64(dv.Table.Rows[ix]["sellprice"]);
                    asset.Description = DataConvert.GetString(dv.Table.Rows[ix]["description"]);
                    assets.Add(asset);
                }
                return assets;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetAssetsInShop", ex);
                return null;
            }
        }
        #endregion

        #region SetAssetsInShop
        public static bool SetAssetsInShop(Collection<AssetInfo> assets)
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_ASSETSMASTERDATA);
                if (objXmlDoc == null)
                    return false;

                XmlNode objNode = objXmlDoc.SelectSingleNode("data/assets");
                objNode.RemoveAll();
                foreach (AssetInfo asset in assets)
                {
                    XmlElement objChildNode = objXmlDoc.CreateElement("item");
                    objNode.AppendChild(objChildNode);
                    XmlElement emtIId = objXmlDoc.CreateElement("iid");
                    emtIId.InnerText = asset.IId.ToString();
                    XmlElement emtName = objXmlDoc.CreateElement("name");
                    emtName.InnerText = asset.Name.ToString();
                    XmlElement emtStandardPrice = objXmlDoc.CreateElement("standardprice");
                    emtStandardPrice.InnerText = asset.StandardPrice.ToString();
                    XmlElement emtBuyRatio = objXmlDoc.CreateElement("buyratio");
                    emtBuyRatio.InnerText = asset.BuyRatio.ToString();
                    XmlElement emtBuyPrice = objXmlDoc.CreateElement("buyprice");
                    emtBuyPrice.InnerText = asset.BuyPrice.ToString();
                    XmlElement emtSellRatio = objXmlDoc.CreateElement("sellratio");
                    emtSellRatio.InnerText = asset.SellRatio.ToString();
                    XmlElement emtSellPrice = objXmlDoc.CreateElement("sellprice");
                    emtSellPrice.InnerText = asset.SellPrice.ToString();
                    XmlElement emtDescription = objXmlDoc.CreateElement("description");
                    emtDescription.InnerText = asset.Description.ToString();

                    objChildNode.AppendChild(emtIId);
                    objChildNode.AppendChild(emtName);
                    objChildNode.AppendChild(emtStandardPrice);
                    objChildNode.AppendChild(emtBuyRatio);
                    objChildNode.AppendChild(emtBuyPrice);
                    objChildNode.AppendChild(emtSellRatio);
                    objChildNode.AppendChild(emtSellPrice);
                    objChildNode.AppendChild(emtDescription);
                }

                return SetMasterDataFile(objXmlDoc, Constants.FILE_ASSETSMASTERDATA);

            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.SetAssetsInShop", ex);
                return false;
            }
        }
        #endregion

        #region GetAssetsToTable
        public static DataTable GetAssetsToTable()
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_ASSETSMASTERDATA);
                if (objXmlDoc == null)
                    return null;

                DataSet ds = new DataSet();
                DataTable dt;

                XmlNode node = objXmlDoc.SelectSingleNode("data/assets");
                if (node == null)
                    dt = new DataTable("table0");
                else
                {
                    StringReader read = new StringReader(node.OuterXml);

                    ds.ReadXml(read);
                    if (ds.Tables.Count < 1)
                        dt = new DataTable("table0");
                    else
                        dt = ds.Tables[0];
                }
                return dt;
            }
            catch (Exception ex)
            {
                LogHelper.Write("读取推荐买卖率表", ex);
                return null;
            }
        }
        #endregion

        #region SetAssetsFromTable
        public static bool SetAssetsFromTable(DataTable dt)
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_ASSETSMASTERDATA);
                if (objXmlDoc == null)
                    return false;

                XmlNode objNode = objXmlDoc.SelectSingleNode("data/assets");
                objNode.RemoveAll();

                for (int ix = 0; ix < dt.Rows.Count; ix++)
                {
                    XmlElement objChildNode = objXmlDoc.CreateElement("item");
                    objNode.AppendChild(objChildNode);
                    XmlElement emtIId = objXmlDoc.CreateElement("iid");
                    emtIId.InnerText = dt.Rows[ix][0].ToString();
                    XmlElement emtName = objXmlDoc.CreateElement("name");
                    emtName.InnerText = dt.Rows[ix][1].ToString();
                    XmlElement emtStandardPrice = objXmlDoc.CreateElement("standardprice");
                    emtStandardPrice.InnerText = dt.Rows[ix][2].ToString();
                    XmlElement emtBuyRatio = objXmlDoc.CreateElement("buyratio");
                    emtBuyRatio.InnerText = dt.Rows[ix][3].ToString();
                    XmlElement emtBuyPrice = objXmlDoc.CreateElement("buyprice");
                    emtBuyPrice.InnerText = dt.Rows[ix][4].ToString();
                    XmlElement emtSellRatio = objXmlDoc.CreateElement("sellratio");
                    emtSellRatio.InnerText = dt.Rows[ix][5].ToString();
                    XmlElement emtSellPrice = objXmlDoc.CreateElement("sellprice");
                    emtSellPrice.InnerText = dt.Rows[ix][6].ToString();
                    XmlElement emtDescription = objXmlDoc.CreateElement("description");
                    emtDescription.InnerText = dt.Rows[ix][7].ToString();

                    objChildNode.AppendChild(emtIId);
                    objChildNode.AppendChild(emtName);
                    objChildNode.AppendChild(emtStandardPrice);
                    objChildNode.AppendChild(emtBuyRatio);
                    objChildNode.AppendChild(emtBuyPrice);
                    objChildNode.AppendChild(emtSellRatio);
                    objChildNode.AppendChild(emtSellPrice);
                    objChildNode.AppendChild(emtDescription);
                }

                return SetMasterDataFile(objXmlDoc, Constants.FILE_ASSETSMASTERDATA);

            }
            catch (Exception ex)
            {
                LogHelper.Write("SetAssetsFromTable", ex);
                return false;
            }
        }
        #endregion

        #region GetAdvancedPurchaseMD
        public static Collection<AdvancedPurchaseInfo> GetAdvancedPurchaseMD()
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_ADVANCEDPURCHASEMASTERDATA);
                if (objXmlDoc == null)
                    return null;

                DataView dv = GetData(objXmlDoc, "data/advancedpurchase");

                Collection<AdvancedPurchaseInfo> aps = new Collection<AdvancedPurchaseInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    AdvancedPurchaseInfo ap = new AdvancedPurchaseInfo();                    
                    ap.Cash = DataConvert.GetInt64(dv.Table.Rows[ix]["cash"]);
                    ap.Price = DataConvert.GetInt64(dv.Table.Rows[ix]["price"]);
                    ap.Count = DataConvert.GetInt64(dv.Table.Rows[ix]["count"]);
                    aps.Add(ap);
                }
                return aps;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetAdvancedPurchaseInShop", ex);
                return null;
            }
        }
        #endregion

        #region SetAdvancedPurchaseMD
        public static bool SetAdvancedPurchaseMD(Collection<AdvancedPurchaseInfo> aps)
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_ADVANCEDPURCHASEMASTERDATA);
                if (objXmlDoc == null)
                    return false;

                XmlNode objNode = objXmlDoc.SelectSingleNode("data/advancedpurchase");
                objNode.RemoveAll();
                foreach (AdvancedPurchaseInfo ap in aps)
                {
                    XmlElement objChildNode = objXmlDoc.CreateElement("item");
                    objNode.AppendChild(objChildNode);
                    XmlElement emtCash = objXmlDoc.CreateElement("cash");
                    emtCash.InnerText = ap.Cash.ToString();
                    XmlElement emtPrice = objXmlDoc.CreateElement("price");
                    emtPrice.InnerText = ap.Price.ToString();
                    XmlElement emtCount = objXmlDoc.CreateElement("count");
                    emtCount.InnerText = ap.Count.ToString();
                    objChildNode.AppendChild(emtCash);
                    objChildNode.AppendChild(emtPrice);
                    objChildNode.AppendChild(emtCount);
                }

                return SetMasterDataFile(objXmlDoc, Constants.FILE_ADVANCEDPURCHASEMASTERDATA);

            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.SetAdvancedPurchaseMD", ex);
                return false;
            }
        }
        #endregion

        #region GetAdvancedPurchaseToTable
        public static DataTable GetAdvancedPurchaseToTable()
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_ADVANCEDPURCHASEMASTERDATA);
                if (objXmlDoc == null)
                    return null;

                DataSet ds = new DataSet();
                DataTable dt;

                XmlNode node = objXmlDoc.SelectSingleNode("data/advancedpurchase");
                if (node == null)
                    dt = new DataTable("table0");
                else
                {
                    StringReader read = new StringReader(node.OuterXml);

                    ds.ReadXml(read);
                    if (ds.Tables.Count < 1)
                        dt = new DataTable("table0");
                    else
                        dt = ds.Tables[0];
                }
                return dt;
            }
            catch (Exception ex)
            {
                LogHelper.Write("读取高级购买配置表", ex);
                return null;
            }
        }
        #endregion

        #region SetAdvancedPurchaseFromTable
        public static bool SetAdvancedPurchaseFromTable(DataTable dt)
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_ADVANCEDPURCHASEMASTERDATA);
                if (objXmlDoc == null)
                    return false;

                XmlNode objNode = objXmlDoc.SelectSingleNode("data/advancedpurchase");
                objNode.RemoveAll();

                for (int ix = 0; ix < dt.Rows.Count; ix++)
                {
                    XmlElement objChildNode = objXmlDoc.CreateElement("item");
                    objNode.AppendChild(objChildNode);
                    XmlElement emtCash = objXmlDoc.CreateElement("cash");
                    emtCash.InnerText = dt.Rows[ix][0].ToString();
                    XmlElement emtPrice = objXmlDoc.CreateElement("price");
                    emtPrice.InnerText = dt.Rows[ix][1].ToString();
                    XmlElement emtCount = objXmlDoc.CreateElement("count");
                    emtCount.InnerText = dt.Rows[ix][2].ToString();
                    objChildNode.AppendChild(emtCash);
                    objChildNode.AppendChild(emtPrice);
                    objChildNode.AppendChild(emtCount);
                }

                return SetMasterDataFile(objXmlDoc, Constants.FILE_ADVANCEDPURCHASEMASTERDATA);

            }
            catch (Exception ex)
            {
                LogHelper.Write("SetAdvancedPurchaseFromTable", ex);
                return false;
            }
        }
        #endregion

        #endregion

        #region Cafe

        #region GetCafeFriends
        public static Collection<FriendInfo> GetCafeFriends(string content)
        {
            try
            {
                if (String.IsNullOrEmpty(content))
                    return new Collection<FriendInfo>();

                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(content);

                DataView dv = GetData(objXmlDoc, "data");

                Collection<FriendInfo> friends = new Collection<FriendInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    FriendInfo friend = new FriendInfo();
                    friend.Id = DataConvert.GetInt32(dv.Table.Rows[ix]["uid"]);
                    friend.Name = DataConvert.GetString(dv.Table.Rows[ix]["real_name"]);
                    if (dv.Table.Columns.Contains("help"))
                        friend.Help = ConvertIntToBool(DataConvert.GetInt32(dv.Table.Rows[ix]["help"]));
                    if (dv.Table.Columns.Contains("food"))
                        friend.Food = ConvertIntToBool(DataConvert.GetInt32(dv.Table.Rows[ix]["food"]));
                    if (dv.Table.Columns.Contains("employ"))
                        friend.Employ = ConvertIntToBool(DataConvert.GetInt32(dv.Table.Rows[ix]["employ"]));
                    if (dv.Table.Columns.Contains("appinstall"))
                        friend.AppInstall = ConvertIntToBool(DataConvert.GetInt32(dv.Table.Rows[ix]["appinstall"]));
                    friends.Add(friend);
                }

                return friends;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetCafeFriends", content, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #region GetCafe
        public static CafeInfo GetCafe(string strxml)
        {
            try
            {
                if (String.IsNullOrEmpty(strxml))
                    return null;

                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(strxml);
                if (objXmlDoc == null)
                    return null;

                CafeInfo cafe = new CafeInfo();

                //cafe
                XmlNode objNode = objXmlDoc.SelectSingleNode("data/account");
                if (objNode == null)
                    return null;

                cafe.Grade = DataConvert.GetInt32(objNode.SelectSingleNode("grade/item/grade").InnerText);
                cafe.GradeLabel = objNode.SelectSingleNode("grade/item/label").InnerText;
                cafe.Name = objNode.SelectSingleNode("real_name").InnerText;
                cafe.Cash = DataConvert.GetInt64(objNode.SelectSingleNode("cash").InnerText);
                cafe.GoldNum = DataConvert.GetInt32(objNode.SelectSingleNode("goldnum").InnerText);
                cafe.Evalue = DataConvert.GetInt32(objNode.SelectSingleNode("evalue").InnerText);

                objNode = objXmlDoc.SelectSingleNode("data/cafe");
                if (objNode == null)
                    return null;
                cafe.CafeName = objNode.SelectSingleNode("cafename").InnerText;
                cafe.CafeId = DataConvert.GetInt32(objNode.SelectSingleNode("cafeid").InnerText);

                //cooking
                DataView dv = GetData(objXmlDoc, "data/cooking");

                Collection<CookingInfo> cookings = new Collection<CookingInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    try
                    {
                        CookingInfo cooking = new CookingInfo();
                        cooking.OrderId = DataConvert.GetInt32(dv.Table.Rows[ix]["orderid"]);
                        if (dv.Table.Columns.Contains("stage"))
                            cooking.Stage = DataConvert.GetInt32(dv.Table.Rows[ix]["stage"]);
                        else
                            cooking.Stage = -98;
                        if (dv.Table.Columns.Contains("stage") && cooking.Stage != -1)
                        {                            
                            cooking.DishId = DataConvert.GetInt32(dv.Table.Rows[ix]["dishid"]);
                            cooking.Name = DataConvert.GetString(dv.Table.Rows[ix]["name"]);
                            cooking.FoodNum = DataConvert.GetInt32(dv.Table.Rows[ix]["foodnum"]);
                            cooking.Step = DataConvert.GetInt32(dv.Table.Rows[ix]["step"]);
                            cooking.Resver = DataConvert.GetInt32(dv.Table.Rows[ix]["resver"]);
                        }
                        cookings.Add(cooking);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Write("ConfigCtrl.GetCafe(Get CookingInfo)", strxml, ex, LogSeverity.Warn);
                        continue;
                    }
                }
                cafe.Cookings = cookings;

                //Employees
                //dv = GetData(objXmlDoc, "data/employees");

                //Collection<EmployeeInfo> employees = new Collection<EmployeeInfo>();

                //for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                //{
                //    try
                //    {
                //        EmployeeInfo employee = new EmployeeInfo();
                //        employee.UId = DataConvert.GetInt32(dv.Table.Rows[ix]["uid"]);
                //        employee.Name = DataConvert.GetString(dv.Table.Rows[ix]["real_name"]);
                //        employees.Add(employee);
                //    }
                //    catch (Exception ex)
                //    {
                //        LogHelper.Write("ConfigCtrl.GetCafe(Get EmployeeInfo)", strxml, ex, LogSeverity.Error);
                //        continue;
                //    }
                //}
                //cafe.Employees = employees;
                XmlNodeList nodes = objXmlDoc.SelectNodes("data/employees/item");
                Collection<FriendInfo> employees = new Collection<FriendInfo>();

                foreach (XmlNode node in nodes)
                {
                    try
                    {
                        FriendInfo employee = new FriendInfo();
                        employee.Id = DataConvert.GetInt32(node.SelectSingleNode("uid").InnerText);
                        employee.Name = node.SelectSingleNode("real_name").InnerText;
                        employees.Add(employee);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Write("ConfigCtrl.GetCafe(Get EmployeeInfo)", strxml, ex, LogSeverity.Error);
                        continue;
                    }
                }
                cafe.Employees = employees;
                

                //Foods
                dv = GetData(objXmlDoc, "data/dish");

                Collection<DinnerTableInfo> dinnertables = new Collection<DinnerTableInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    try
                    {
                        DinnerTableInfo dinnertable = new DinnerTableInfo();
                        dinnertable.OrderId = DataConvert.GetInt64(dv.Table.Rows[ix]["orderid"]);
                        if (dv.Table.Columns.Contains("name"))
                            dinnertable.Name = DataConvert.GetString(dv.Table.Rows[ix]["name"]);
                        if (dv.Table.Columns.Contains("foodnum"))
                            dinnertable.FoodNum = DataConvert.GetInt32(dv.Table.Rows[ix]["foodnum"]);
                        if (dv.Table.Columns.Contains("dishid"))
                            dinnertable.DishId = DataConvert.GetInt32(dv.Table.Rows[ix]["dishid"]);
                        if (dv.Table.Columns.Contains("num"))
                            dinnertable.Num = DataConvert.GetInt32(dv.Table.Rows[ix]["num"]);
                        if (dv.Table.Columns.Contains("resver"))
                            dinnertable.Resver = DataConvert.GetInt32(dv.Table.Rows[ix]["resver"]);
                        dinnertables.Add(dinnertable);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Write("ConfigCtrl.GetCafe(Get DishInfo)", strxml, ex, LogSeverity.Warn);
                        continue;
                    }
                }
                cafe.DinnerTables = dinnertables;

                //equipments
                dv = GetData(objXmlDoc, "data/equipments");
                if (dv.Table.Rows.Count > 0)
                {
                    string tkey = "";
                    for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                    {
                        try
                        {
                            tkey = DataConvert.GetString(dv.Table.Rows[ix]["tkey"]);
                            if (tkey == "autochef")
                                cafe.Chef = true;
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Write("ConfigCtrl.GetCafe(Get DishInfo)", strxml, ex, LogSeverity.Warn);
                            continue;
                        }
                    }
                }
                return cafe;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetCafe", strxml, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #region GetEmptyEmployees
        public static Collection<FriendInfo> GetEmptyEmployees(string content)
        {
            try
            {
                if (String.IsNullOrEmpty(content))
                    return new Collection<FriendInfo>();

                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(content);

                DataView dv = GetData(objXmlDoc, "data/notemployees");

                Collection<FriendInfo> employees = new Collection<FriendInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    FriendInfo employee = new FriendInfo();
                    employee.Id = DataConvert.GetInt32(dv.Table.Rows[ix]["fuid"]);
                    employee.Name = DataConvert.GetString(dv.Table.Rows[ix]["real_name"]);
                    employee.Power = DataConvert.GetInt32(dv.Table.Rows[ix]["power"]);
                    employees.Add(employee);
                }

                return employees;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetEmptyEmployees", content, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #region GetOriginalDishes
        public static Collection<DishInfo> GetOriginalDishes(string content)
        {
            try
            {
                if (String.IsNullOrEmpty(content))
                    return new Collection<DishInfo>();

                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(content);

                DataView dv = GetData(objXmlDoc, "data/dish");

                Collection<DishInfo> dishes = new Collection<DishInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    DishInfo dish = new DishInfo();
                    dish.DishId = DataConvert.GetInt32(dv.Table.Rows[ix]["dishid"]);
                    dish.Title = DataConvert.GetString(dv.Table.Rows[ix]["title"]);
                    dish.Price = DataConvert.GetInt32(DataConvert.GetString(dv.Table.Rows[ix]["price"]).Replace("￥", ""));
                    dish.Rank = DataConvert.GetInt32(dv.Table.Rows[ix]["rank"]);
                    dishes.Add(dish);
                }

                return dishes;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetOriginalDishes", content, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #region GetDishesInMenu
        public static Collection<DishInfo> GetDishesInMenu()
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_CAFEDISHESMASTERDATA);
                if (objXmlDoc == null)
                    return null;

                DataView dv = GetData(objXmlDoc, "data/dishes");

                Collection<DishInfo> dishes = new Collection<DishInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    DishInfo dish = new DishInfo();
                    dish.DishId = DataConvert.GetInt32(dv.Table.Rows[ix]["dishid"]);
                    dish.Title = DataConvert.GetString(dv.Table.Rows[ix]["title"]);
                    dish.Price = DataConvert.GetInt32(dv.Table.Rows[ix]["price"]);
                    dish.Rank = DataConvert.GetInt32(dv.Table.Rows[ix]["rank"]);
                    dishes.Add(dish);
                }
                return dishes;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetDishesInMenu", ex);
                return null;
            }
        }
        #endregion

        #region SetDishesInMenu
        public static bool SetDishesInMenu(Collection<DishInfo> dishes)
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_CAFEDISHESMASTERDATA);
                if (objXmlDoc == null)
                    return false;

                XmlNode objNode = objXmlDoc.SelectSingleNode("data/dishes");
                objNode.RemoveAll();
                foreach (DishInfo dish in dishes)
                {
                    XmlElement objChildNode = objXmlDoc.CreateElement("item");
                    objNode.AppendChild(objChildNode);
                    XmlElement emtDishId = objXmlDoc.CreateElement("dishid");
                    emtDishId.InnerText = dish.DishId.ToString();
                    XmlElement emtTitle = objXmlDoc.CreateElement("title");
                    emtTitle.InnerText = dish.Title.ToString();
                    XmlElement emtPrice = objXmlDoc.CreateElement("price");
                    emtPrice.InnerText = dish.Price.ToString();
                    XmlElement emtRank = objXmlDoc.CreateElement("rank");
                    emtRank.InnerText = dish.Rank.ToString();
                    objChildNode.AppendChild(emtDishId);
                    objChildNode.AppendChild(emtTitle);
                    objChildNode.AppendChild(emtPrice);
                    objChildNode.AppendChild(emtRank);
                }

                return SetMasterDataFile(objXmlDoc, Constants.FILE_CAFEDISHESMASTERDATA);

            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.SetDishesInMenu", ex);
                return false;
            }
        }
        #endregion

        #region GetTransactionDishes
        public static Collection<DishInfo> GetTransactionDishes()
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_CAFEDISHESTRANSACTIONMASTERDATA);
                if (objXmlDoc == null)
                    return null;

                DataView dv = GetData(objXmlDoc, "data/transactiondishes");

                Collection<DishInfo> dishes = new Collection<DishInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    DishInfo dish = new DishInfo();
                    dish.DishId = DataConvert.GetInt32(dv.Table.Rows[ix]["dishid"]);
                    dish.Title = DataConvert.GetString(dv.Table.Rows[ix]["title"]);
                    dish.MaxPrice = DataConvert.GetDecimal(dv.Table.Rows[ix]["maxprice"]);
                    dish.MinPrice = DataConvert.GetDecimal(dv.Table.Rows[ix]["minprice"]);
                    dish.SellPrice = DataConvert.GetDecimal(dv.Table.Rows[ix]["sellprice"]);
                    dish.PurchasePrice = DataConvert.GetDecimal(dv.Table.Rows[ix]["purchaseprice"]);
                    dishes.Add(dish);
                }
                return dishes;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetTransactionDishes", ex);
                return null;
            }
        }
        #endregion

        #region SetTransactionDishes
        public static bool SetTransactionDishes(Collection<DishInfo> dishes, bool overwrite)
        {
            try
            {
                if (!overwrite)
                {
                    Collection<DishInfo> originalDishes = GetTransactionDishes();
                    DishInfo olddish;
                    foreach (DishInfo dish in dishes)
                    {
                        olddish = GetTransactionDishById(originalDishes, dish.DishId);
                        if (olddish != null)
                        {
                            if (dish.MaxPrice < olddish.MaxPrice)
                                dish.MaxPrice = olddish.MaxPrice;
                            if (dish.MinPrice > olddish.MinPrice)
                                dish.MinPrice = olddish.MinPrice;
                            dish.SellPrice = dish.MaxPrice;
                            dish.PurchasePrice = dish.MinPrice;
                        }
                    }
                }

                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_CAFEDISHESTRANSACTIONMASTERDATA);
                if (objXmlDoc == null)
                    return false;

                XmlNode objNode = objXmlDoc.SelectSingleNode("data/transactiondishes");
                objNode.RemoveAll();
                foreach (DishInfo dish in dishes)
                {
                    XmlElement objChildNode = objXmlDoc.CreateElement("item");
                    objNode.AppendChild(objChildNode);
                    XmlElement emtDishId = objXmlDoc.CreateElement("dishid");
                    emtDishId.InnerText = dish.DishId.ToString();
                    XmlElement emtTitle = objXmlDoc.CreateElement("title");
                    emtTitle.InnerText = dish.Title.ToString();
                    XmlElement emtMaxPrice = objXmlDoc.CreateElement("maxprice");
                    emtMaxPrice.InnerText = dish.MaxPrice.ToString();
                    XmlElement emtMinPrice = objXmlDoc.CreateElement("minprice");
                    emtMinPrice.InnerText = dish.MinPrice.ToString();
                    XmlElement emtSellPrice = objXmlDoc.CreateElement("sellprice");
                    //emtSellPrice.InnerText = dish.SellPrice.ToString();
                    emtSellPrice.InnerText = dish.MaxPrice.ToString();
                    XmlElement emtPurchasePrice = objXmlDoc.CreateElement("purchaseprice");
                    //emtPurchasePrice.InnerText = dish.PurchasePrice.ToString();
                    emtPurchasePrice.InnerText = dish.MinPrice.ToString();

                    objChildNode.AppendChild(emtDishId);
                    objChildNode.AppendChild(emtTitle);
                    objChildNode.AppendChild(emtMaxPrice);
                    objChildNode.AppendChild(emtMinPrice);
                    objChildNode.AppendChild(emtSellPrice);
                    objChildNode.AppendChild(emtPurchasePrice);
                }

                return SetMasterDataFile(objXmlDoc, Constants.FILE_CAFEDISHESTRANSACTIONMASTERDATA);

            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.SetTransactionDishes", ex);
                return false;
            }
        }
        #endregion

        #region GetDishNameById
        private static DishInfo GetTransactionDishById(Collection<DishInfo> dishes, int dishid)
        {
            foreach (DishInfo dish in dishes)
            {
                if (dish.DishId == dishid)
                {
                    return dish;
                }
            }
            return null;
        }
        #endregion

        #region GetTransactionDishesToTable
        public static DataTable GetTransactionDishesToTable()
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_CAFEDISHESTRANSACTIONMASTERDATA);
                if (objXmlDoc == null)
                    return null;

                DataSet ds = new DataSet();
                DataTable dt;

                XmlNode node = objXmlDoc.SelectSingleNode("data/transactiondishes");
                if (node == null)
                    dt = new DataTable("table0");
                else
                {
                    StringReader read = new StringReader(node.OuterXml);

                    ds.ReadXml(read);
                    if (ds.Tables.Count < 1)
                        dt = new DataTable("table0");
                    else
                        dt = ds.Tables[0];
                }
                return dt;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetTransactionDishesToTable", ex);
                return null;
            }
        }
        #endregion

        #region SetTransactionDishesToFile
        public static bool SetTransactionDishesToFile(DataTable dt)
        {
            try
            {
                XmlDocument objXmlDoc = GetMasterDataFile(Constants.FILE_CAFEDISHESTRANSACTIONMASTERDATA);
                if (objXmlDoc == null)
                    return false;

                XmlNode objSeedNode = objXmlDoc.SelectSingleNode("data/transactiondishes");
                objSeedNode.RemoveAll();

                for (int ix = 0; ix < dt.Rows.Count; ix++)
                {
                    XmlElement objChildNode = objXmlDoc.CreateElement("item");
                    objSeedNode.AppendChild(objChildNode);
                    XmlElement emtDishId = objXmlDoc.CreateElement("dishid");
                    emtDishId.InnerText = dt.Rows[ix][0].ToString();
                    XmlElement emtTitle = objXmlDoc.CreateElement("title");
                    emtTitle.InnerText = dt.Rows[ix][1].ToString();
                    XmlElement emtMaxPrice = objXmlDoc.CreateElement("maxprice");
                    emtMaxPrice.InnerText = dt.Rows[ix][2].ToString();
                    XmlElement emtMinPrice = objXmlDoc.CreateElement("minprice");
                    emtMinPrice.InnerText = dt.Rows[ix][3].ToString();
                    XmlElement emtSellPrice = objXmlDoc.CreateElement("sellprice");
                    emtSellPrice.InnerText = dt.Rows[ix][4].ToString();
                    XmlElement emtPurchasePrice = objXmlDoc.CreateElement("purchaseprice");
                    emtPurchasePrice.InnerText = dt.Rows[ix][5].ToString();
                    objChildNode.AppendChild(emtDishId);
                    objChildNode.AppendChild(emtTitle);
                    objChildNode.AppendChild(emtMaxPrice);
                    objChildNode.AppendChild(emtMinPrice);
                    objChildNode.AppendChild(emtSellPrice);
                    objChildNode.AppendChild(emtPurchasePrice);
                }

                return SetMasterDataFile(objXmlDoc, Constants.FILE_CAFEDISHESTRANSACTIONMASTERDATA);

            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.SetTransactionDishesToFile", ex);
                return false;
            }
        }
        #endregion

        #region GetPurchaseDishes
        public static Collection<DishInfo> GetPurchaseDishes(string content)
        {
            try
            {
                //http://www.kaixin001.com/cafe/api_frienddish.php?verify=2588258_1136_2588258_1267104985_295bd02d0d7f370d3be08fa81d916e0e&uid=6195212&cafeid=2729328&page=0
                if (String.IsNullOrEmpty(content))
                    return new Collection<DishInfo>();

                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(content);

                DataView dv = GetData(objXmlDoc, "data/dish");

                Collection<DishInfo> dishes = new Collection<DishInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    DishInfo dish = new DishInfo();
                    dish.DishId = DataConvert.GetInt32(dv.Table.Rows[ix]["dishid"]);
                    dish.Title = DataConvert.GetString(dv.Table.Rows[ix]["name"]);
                    dish.CurrentPrice = DataConvert.GetDecimal(dv.Table.Rows[ix]["price"]);
                    dishes.Add(dish);
                }
                return dishes;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetPurchaseDishes", ex);
                return null;
            }
        }
        #endregion

        #endregion

        #region SaveXmlStringToFile
        public static string SaveXmlStringToFile(string strxml, string strFileName)
        {
            try
            {
                //load config info
                string folder = Path.Combine(Application.StartupPath, Constants.FOLDER_MASTERDATA);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                string configFile = folder + Constants.CHAR_DOUBLEBACKSLASH + strFileName + ".xml";

                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(strxml);
                xmldoc.Save(configFile);
                return Constants.STATUS_SUCCESS;
            }
            catch (Exception ex)
            {
                LogHelper.Write("保存XmlString成文件" + strFileName, ex);
                return Constants.STATUS_FAIL;
            }
        }
        #endregion

        #region GetContactsFromFile
        public static Collection<FriendInfo> GetContactsFromFile(string file)
        {
            try
            {
                if (!File.Exists(file))
                    return null;

                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.Load(file);

                if (objXmlDoc == null)
                    return null;

                DataView dv = GetData(objXmlDoc, "ZrAssistant/Contact");

                Collection<FriendInfo> friends = new Collection<FriendInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    FriendInfo friend = new FriendInfo();
                    friend.Name = dv.Table.Rows[ix][0].ToString();
                    friend.Id = DataConvert.GetInt32(dv.Table.Rows[ix][1]);
                    friends.Add(friend);
                }

                return friends;
            }
            catch (Exception ex)
            {
                LogHelper.Write("读取联系人信息" + file, ex);
                return null;
            }
        }
        #endregion

        #region SaveContactToFile
        public static string SaveContactToFile(string path, AccountInfo account, Collection<FriendInfo> friends)
        {
            try
            {
                if (String.IsNullOrEmpty(path))
                    return "保存路径不能为空！";

                if (friends == null || friends.Count <= 0)
                    return "没有任何好友，无法保存！";

                XmlDocument objXmlDoc = new XmlDocument();

                //<ZrAssistant></ZrAssistant>
                XmlElement root = objXmlDoc.CreateElement("ZrAssistant");
                objXmlDoc.AppendChild(root);

                //<Contact></Contact>
                XmlElement objContactNode = objXmlDoc.CreateElement("Contact");
                root.AppendChild(objContactNode);

                //<Friend>
                //  <UserName><UserName>
                //  <UserId></UserId>
                //</Friend>
                /*------------------------------Contact-----------------------------------*/
                foreach (FriendInfo friend in friends)
                {
                    XmlElement objChildNode = objXmlDoc.CreateElement("Friend");
                    objContactNode.AppendChild(objChildNode);
                    XmlElement emtUserName = objXmlDoc.CreateElement("UserName");
                    emtUserName.InnerText = friend.Name;
                    objChildNode.AppendChild(emtUserName);
                    XmlElement emtUserId = objXmlDoc.CreateElement("UserId");
                    emtUserId.InnerText = friend.Id.ToString();
                    objChildNode.AppendChild(emtUserId);
                }
                /*------------------------------Contact-----------------------------------*/

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string configFile = path + Constants.CHAR_DOUBLEBACKSLASH + account.UserName + "(" + account.Email + ").xml";
                objXmlDoc.Save(configFile);
                return Constants.STATUS_SUCCESS;
            }
            catch (Exception ex)
            {
                LogHelper.Write("保存联系人信息到文件" + path, ex);
                return ex.Message;
            }
        }
        #endregion

        #region GetContactsFromFile
        public static Collection<EncryptFriendInfo> GetEncryptFriendFromFile(string strxml)
        {
            try
            {
                if (String.IsNullOrEmpty(strxml))
                    return null;

                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(strxml);

                if (objXmlDoc == null)
                    return null;

                DataView dv = GetData(objXmlDoc, "ZrAssistant/Contact");

                Collection<EncryptFriendInfo> friends = new Collection<EncryptFriendInfo>();

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    EncryptFriendInfo friend = new EncryptFriendInfo();
                    friend.Name = dv.Table.Rows[ix][0].ToString();
                    friend.Id = dv.Table.Rows[ix][1].ToString();
                    friends.Add(friend);
                }

                return friends;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.GetEncryptFriendFromFile", strxml, ex, LogSeverity.Error);
                return null;
            }
        }
        #endregion

        #region SaveEncryptFriendToFile
        public static string SaveEncryptFriendToFile(string file, Collection<EncryptFriendInfo> friends)
        {
            try
            {
                if (friends == null || friends.Count <= 0)
                    return "没有任何好友，无法保存！";

                XmlDocument objXmlDoc = new XmlDocument();

                //<?xml version="1.0" encoding="utf-16"?>
                //objXmlDoc.

                //<ZrAssistant></ZrAssistant>
                XmlElement root = objXmlDoc.CreateElement("ZrAssistant");
                objXmlDoc.AppendChild(root);

                //<Contact></Contact>
                XmlElement objContactNode = objXmlDoc.CreateElement("Contact");
                root.AppendChild(objContactNode);

                //<Friend>
                //  <UserName><UserName>
                //  <UserId></UserId>
                //</Friend>
                /*------------------------------Contact-----------------------------------*/
                foreach (EncryptFriendInfo friend in friends)
                {
                    XmlElement objChildNode = objXmlDoc.CreateElement("Friend");
                    objContactNode.AppendChild(objChildNode);
                    XmlElement emtUserName = objXmlDoc.CreateElement("UserName");
                    emtUserName.InnerText = friend.Name;
                    objChildNode.AppendChild(emtUserName);
                    XmlElement emtUserId = objXmlDoc.CreateElement("UserId");
                    emtUserId.InnerText = friend.Id;
                    objChildNode.AppendChild(emtUserId);
                }
                /*------------------------------Contact-----------------------------------*/

                objXmlDoc.Save(file);
                return Constants.STATUS_SUCCESS;
            }
            catch (Exception ex)
            {
                LogHelper.Write("ConfigCtrl.SaveEncryptFriendToFile", file, ex, LogSeverity.Error);
                return ex.Message;
            }
        }
        #endregion        
    }
}
