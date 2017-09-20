using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;

using Johnny.Library.Helper;

namespace Johnny.CMS.admin
{
    public partial class display : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {            
            base.Page_Load(sender, e);

            lbServerName.Text = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath;
            lbIp.Text = Request.ServerVariables["LOCAl_ADDR"];
            lbDomain.Text = Request.ServerVariables["SERVER_NAME"].ToString();
            lbPort.Text = Request.ServerVariables["Server_Port"].ToString();
            lbIISVer.Text = Request.ServerVariables["Server_SoftWare"].ToString();
            lbPhPath.Text = Request.PhysicalApplicationPath;
            lbOperat.Text = Environment.OSVersion.ToString();
            lbSystemPath.Text = Environment.SystemDirectory.ToString();
            lbTimeOut.Text = (Server.ScriptTimeout / 1000).ToString() + "√Î";
            lbLan.Text = CultureInfo.InstalledUICulture.EnglishName;
            lbAspnetVer.Text = string.Concat(new object[] { Environment.Version.Major, ".", Environment.Version.Minor, Environment.Version.Build, ".", Environment.Version.Revision });
            lbCurrentTime.Text = DateTime.Now.ToString();

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Version Vector");
            lbIEVer.Text = key.GetValue("IE", "Œ¥ºÏ≤‚µΩ").ToString();
            lbServerLastStartToNow.Text = ((Environment.TickCount / 0x3e8) / 60).ToString() + "∑÷÷”";

            string[] achDrives = Directory.GetLogicalDrives();
            for (int i = 0; i < Directory.GetLogicalDrives().Length - 1; i++)
            {
                lbLogicDriver.Text = lbLogicDriver.Text + achDrives[i].ToString();
            }

            //ManagementClass diskClass = new ManagementClass("NUMBER_OF_PROCESSORS");
            lbCpuNum.Text = Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS").ToString();
            lbCpuType.Text = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER").ToString();
            lbMemory.Text = (Environment.WorkingSet / 1024).ToString() + "M";
            lbMemoryPro.Text = ((Double)GC.GetTotalMemory(false) / 1048576).ToString("N2") + "M";
            lbMemoryNet.Text = ((Double)Process.GetCurrentProcess().WorkingSet64 / 1048576).ToString("N2") + "M";
            lbCpuNet.Text = ((TimeSpan)Process.GetCurrentProcess().TotalProcessorTime).TotalSeconds.ToString("N0");
            lbSessionNum.Text = Session.Contents.Count.ToString();
            lbSession.Text = Session.Contents.SessionID;
            lbUser.Text = Environment.UserName;

            if (DataConvert.GetString(Session["IsFirstAccess"]) == "true")
            {
                popupMsn.Message = "<img src='images/bob.gif' border='0' align='right'><p><b>Welcome<span style='color:#00a000;'>[" + Session["UserName"] + "]</span></b><br><br>System Time:" + System.DateTime.Now.ToString() + "</p>";
                popupMsn.Title = "Console";
                popupMsn.Visible = true;
                Session["IsFirstAccess"] = "false";
            }
        }
    }
}
