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
using Johnny.Controls.Web.LeftMenu;
using Johnny.Library.Helper;

namespace Johnny.CMS.admin
{
    public partial class menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //�˵���Ŀ
            Johnny.CMS.BLL.SystemInfo.MenuCategory category = new Johnny.CMS.BLL.SystemInfo.MenuCategory();
            IList<Johnny.CMS.OM.SystemInfo.MenuCategory> categoryModel = category.GetList();

            //�˵�
            Johnny.CMS.BLL.SystemInfo.Menu menu = new Johnny.CMS.BLL.SystemInfo.Menu();
            IList<Johnny.CMS.OM.SystemInfo.Menu> menuModel = menu.GetList(true);
                        
            //�û�Ȩ��
            Johnny.CMS.BLL.Access.Accounts accounts = new Johnny.CMS.BLL.Access.Accounts();
            ArrayList arrPermission = new ArrayList();
            arrPermission = accounts.GetUserPermission(DataConvert.GetString(Session["UserName"]));

            //�����˵���˵����
            int iTopMenuId = DataConvert.GetInt32(Request.QueryString["TopMenuId"]);
            if (iTopMenuId <= 0)
                iTopMenuId = 1;
            Johnny.CMS.BLL.SystemInfo.TopMenuBinding topMainMenu = new Johnny.CMS.BLL.SystemInfo.TopMenuBinding();
            IList<Johnny.CMS.OM.SystemInfo.TopMenuBinding> topMainMenuModel = topMainMenu.GetList(iTopMenuId);

            //�������˵�������
            foreach (Johnny.CMS.OM.SystemInfo.MenuCategory item in categoryModel)
            {
                if (CategoryContains(item.MenuCategoryId, topMainMenuModel))
                {
                    MainMenuItem mainmenu = new MainMenuItem();
                    mainmenu.Text = item.MenuCategoryName;

                    foreach (Johnny.CMS.OM.SystemInfo.Menu subitem in menuModel)
                    {
                        //�ж��Ƿ����ڸ����˵������Ҿ��з���Ȩ��
                        if (subitem.MenuCategoryId == item.MenuCategoryId && arrPermission.Contains(subitem.PermissionId))
                        {
                            SubMenuItem submenu = new SubMenuItem();
                            submenu.Text = subitem.MenuName;
                            submenu.Url = subitem.PageLink;
                            submenu.ToolTip = subitem.ToolTip;
                            submenu.Image = subitem.Image;
                            mainmenu.SubItems.Add(submenu);
                        }
                    }

                    if (mainmenu.SubItems.Count > 0)
                        LeftMenu1.items.Add(mainmenu);
                }
            }
        }

        private bool CategoryContains(int menuCategoryId, IList<Johnny.CMS.OM.SystemInfo.TopMenuBinding> list)
        {
            foreach (Johnny.CMS.OM.SystemInfo.TopMenuBinding item in list)
            {
                if (item.MenuCategoryId == menuCategoryId)
                    return true;
            }
            return false;
        }
    }
}
