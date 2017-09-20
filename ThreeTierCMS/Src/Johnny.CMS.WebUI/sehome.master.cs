using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

using Johnny.Controls.Web.ExtjsTab;

namespace Johnny.CMS.WebUI
{
    public partial class sehome : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                myExtjsTab.TabPages.Clear();
                string strXmlFile = System.Web.HttpRuntime.AppDomainAppPath + "config/TabPages.xml";
                //set properties
                XmlDocument doc = new XmlDocument();
                doc.Load(strXmlFile);

                XmlNodeList pageNodes = doc.SelectNodes("Tab/TabPage");
                for (int ix = 0; ix < pageNodes.Count; ix++)
                {
                    ExtjsTabPage tabPage = new ExtjsTabPage();
                    tabPage.TabPageID = pageNodes[ix].Attributes["TabPageID"].Value;
                    tabPage.Text = pageNodes[ix].Attributes["Name"].Value;
                    tabPage.Url = pageNodes[ix].Attributes["URL"].Value;
                    string strPage = pageNodes[ix].Attributes["Pages"].Value;
                    if (!String.IsNullOrEmpty(strPage))
                    {
                        string[] pages = strPage.Split('|');
                        for (int iy = 0; iy < pages.Length; iy++)
                        {
                            if (!String.IsNullOrEmpty(pages[iy]))
                            {
                                if (pages[iy] == GetCurrentPageName())
                                {
                                    tabPage.Selected = true;
                                    break;
                                }
                            }
                        }
                    }
                    myExtjsTab.TabPages.Add(tabPage);
                }

                //build navigation
                string strCurrentPage = GetCurrentPageName();
                myNavigator.CurrentPageName = strCurrentPage;
                //switch (strCurrentPage)
                //{
                //    case "index.aspx":
                //        lblNavigation.Text = "<td><span>欢迎光临Johnny的程序员之家</span></td>";
                //        break;
                //    case "sapindex.aspx":
                //    case "saparticledetail.aspx":
                //        lblNavigation.Text = "<td><a href=\"index.html\">首页</a></td><td class=\"spacer\"><img src=\"images/c-sep.gif\"></td><td><a href=\"sapindex.html\">SAP</a></td>";
                //        break;
                //    default:
                //        lblNavigation.Text = "";
                //        break;
                //}
            }
        }

        private string GetCurrentPageName()
        {
            int length = HttpContext.Current.Request.Url.Segments.Length;
            return HttpContext.Current.Request.Url.Segments[length - 1];
        }
    }
}
