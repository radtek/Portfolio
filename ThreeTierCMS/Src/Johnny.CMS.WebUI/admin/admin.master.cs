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
using System.Xml;
using System.Collections.Generic;
using System.Linq;

using Johnny.Library.Helper;
using Johnny.Component.Globalization;

namespace Johnny.CMS.admin
{
    public partial class adminmaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /*
                string strXmlFile = System.Web.HttpRuntime.AppDomainAppPath + "admin/xmlconfig/pagepart.xml";
                //set properties
                XmlDocument doc = new XmlDocument();
                doc.Load(strXmlFile);
                XmlNodeList nodes = doc.SelectSingleNode("JohnnyCMS").SelectNodes("Page");

                string currentPage = GetCurrentPageName();
                for (int ix = 0; ix < nodes.Count; ix++)
                {
                    if (currentPage == nodes[ix].Attributes["AddName"].Value || currentPage == nodes[ix].Attributes["ListName"].Value)
                    {
                        lblStatus.Text = "&nbsp;";
                        //lblCategory.Text = nodes[ix].ChildNodes[0].InnerText;
                        lblTitle.Text = nodes[ix].ChildNodes[1].InnerText;
                        hyperlinkAllList.Text = nodes[ix].ChildNodes[2].InnerText;
                        hyperlinkAllList.NavigateUrl = nodes[ix].ChildNodes[3].InnerText;
                        hyperlinkAdd.Text = nodes[ix].ChildNodes[4].InnerText;
                        hyperlinkAdd.NavigateUrl = nodes[ix].ChildNodes[5].InnerText;
                        if (nodes[ix].Attributes["Single"].Value == "True")
                        {
                            lblSeparator.Visible = false;
                            hyperlinkAdd.Visible = false;
                        }
                        break;
                    }
                }*/

                //Get current page
                string currentPage = GetCurrentPageName();
                //Get Page Binding
                Johnny.CMS.BLL.SystemInfo.PageBinding pagebinding = new Johnny.CMS.BLL.SystemInfo.PageBinding();
                IList<Johnny.CMS.OM.SystemInfo.PageBinding> bindinglist = pagebinding.GetList();
                Johnny.CMS.BLL.SystemInfo.Menu menu = new Johnny.CMS.BLL.SystemInfo.Menu();
                IList<Johnny.CMS.OM.SystemInfo.Menu> menulist = menu.GetList();
                foreach (var menuitem in menulist)
                {
                    if (menuitem.PageLink.Equals(currentPage) || menuitem.PageLink.Contains("/"+currentPage))
                    {
                        foreach (var bindingitem in bindinglist)
                        {
                            if (menuitem.MenuId == bindingitem.ListMenuId || menuitem.MenuId == bindingitem.AddMenuId)
                            {
                                lblStatus.Text = "&nbsp;";
                                lblTitle.Text = bindingitem.Title;
                                hyperlinkAllList.Text = GlobalizationUtility.GetLabelText("AdminMaster_List");
                                //hyperlinkAllList.Text = DataConvert.GetString(GetGlobalResourceObject("globaladmin", "AdminMasterLinkButtonList"));
                                Johnny.CMS.OM.SystemInfo.Menu listmenu = menulist.FirstOrDefault(p => p.MenuId == bindingitem.ListMenuId);
                                hyperlinkAllList.NavigateUrl = listmenu.PageLink;
                                //hyperlinkAdd.Text = "Add";
                                hyperlinkAdd.Text = GlobalizationUtility.GetLabelText("AdminMaster_Add");
                                Johnny.CMS.OM.SystemInfo.Menu addmenu = menulist.FirstOrDefault(p => p.MenuId == bindingitem.AddMenuId);
                                hyperlinkAdd.NavigateUrl = addmenu.PageLink;
                                if (bindingitem.ListMenuId == bindingitem.AddMenuId)
                                {
                                    hyperlinkAllList.Text = bindingitem.Title;
                                    lblSeparator.Visible = false;
                                    hyperlinkAdd.Visible = false;
                                }
                                break;
                            }
                        }
                        break;
                    }
                }
            }
        }

        private string GetCurrentPageName()
        {
            int length = HttpContext.Current.Request.Url.Segments.Length;
            return HttpContext.Current.Request.Url.Segments[length - 1];
        }
    }
}
