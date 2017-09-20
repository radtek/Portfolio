using System;
using System.Collections.Generic;
using System.Windows.Forms;

//using Johnny.Library.Helper;

namespace Johnny.Kaixin.AutoUpdate
{
    static class Program
    {
        public const string MESSAGE_CAPTION = "开心助手自动升级";
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                FrmAutoUpdate frmUpdate = new FrmAutoUpdate();
                if (args != null && args.Length == 1)
                {
                    frmUpdate.UpdateXml = System.Web.HttpUtility.UrlDecode(args[0].ToString());
                    //LogHelper.Write("AutoUpdate.Main", args[0], LogSeverity.Info);
                }
                else
                {
                    MessageBox.Show("启动失败！请通过开心助手中的检查更新功能进行升级！", MESSAGE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }
                Application.Run(frmUpdate);
            }
            catch (Exception ex)
            {
                //if (args == null)
                //    LogHelper.Write("AutoUpdate.Main", "args is null", ex, LogSeverity.Error);
                //else
                //    LogHelper.Write("AutoUpdate.Main", args.Length.ToString(), ex, LogSeverity.Error);
                    
            }
        }
    }
}