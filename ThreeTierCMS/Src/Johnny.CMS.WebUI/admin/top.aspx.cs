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
using System.Collections.Generic;

using Johnny.CMS.BLL;
using Johnny.CMS.OM;
using Johnny.Library.Helper;
using Johnny.Controls.Web.WebTab;

namespace Johnny.CMS.admin
{
    public partial class top : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = GetLabelText("Top_Welcome");
            linkLogout.Text = GetLabelText("Top_Logout_Text");
            linkLogout.ToolTip = GetLabelText("Top_Logout_ToolTip");
            linkHomepage.Text = GetLabelText("Top_Homepage_Text");
            linkHomepage.ToolTip = GetLabelText("Top_Homepage_ToolTip");
            linkProfile.Text = GetLabelText("Top_Profile_Text");
            linkProfile.ToolTip = GetLabelText("Top_Profile_ToolTip");
            linkPassword.Text = GetLabelText("Top_Password_Text");
            linkPassword.ToolTip = GetLabelText("Top_Password_ToolTip");
            linkAbout.Text = GetLabelText("Top_About_Text");
            linkAbout.ToolTip = GetLabelText("Top_About_ToolTip");
            linkHelp.Text = GetLabelText("Top_Help_Text");
            linkHelp.ToolTip = GetLabelText("Top_Help_ToolTip");


            lblLogonUser.Text = DataConvert.GetString(Session["UserName"]);
            Johnny.CMS.BLL.SystemInfo.TopMenu topMenu = new Johnny.CMS.BLL.SystemInfo.TopMenu();
            IList<Johnny.CMS.OM.SystemInfo.TopMenu> topMenuModel = topMenu.GetList();
            foreach (Johnny.CMS.OM.SystemInfo.TopMenu item in topMenuModel)
            {
                WebTabPage tabPage = new WebTabPage();
                tabPage.TabPageID = item.TopMenuId;
                tabPage.Text = item.TopMenuName;
                tabPage.Url = item.PageLink;
                tabPage.ToolTip = item.ToolTip;
                WebTab1.items.Add(tabPage);
            }           
        }
    }
}
