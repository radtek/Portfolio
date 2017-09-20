using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Johnny.Component.Globalization;

namespace Johnny.CMS.admin.usercontrol
{
    public partial class menucontrol : System.Web.UI.UserControl
    {
        public int TopMenuId;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMenuCategory.Text = GlobalizationUtility.GetLabelText("UserControl_MenuCategory");
            CreateAllItems(lstLeft);
            CreateRightAccount(lstRight, TopMenuId);
            btnSelect.Attributes.Add("onclick", "SelectOne('" + lstLeft.ClientID + "','" + lstRight.ClientID + "','" + hdnSelected.ClientID + "')");
            btnSelectAll.Attributes.Add("onclick", "SelectAll('" + lstLeft.ClientID + "','" + lstRight.ClientID + "','" + hdnSelected.ClientID + "')");
            btnUnselect.Attributes.Add("onclick", "UnSelectOne('" + lstRight.ClientID + "','" + hdnSelected.ClientID + "')");
            btnUnselectAll.Attributes.Add("onclick", "UnSelectAll('" + lstRight.ClientID + "','" + hdnSelected.ClientID + "')");
            hdnSelected.Value = "";
            foreach (ListItem item in lstRight.Items)
            {
                hdnSelected.Value = hdnSelected.Value + item.Value + ",";
            }
        }

        //所有菜单栏目
        private void CreateAllItems(ListBox listcontrolleft)
        {
            Johnny.CMS.BLL.SystemInfo.MenuCategory bll = new Johnny.CMS.BLL.SystemInfo.MenuCategory();
            listcontrolleft.DataSource = bll.GetList();
            listcontrolleft.DataTextField = "MenuCategoryName";
            listcontrolleft.DataValueField = "MenuCategoryId";
            listcontrolleft.DataBind();
        }

        //允许显示的菜单栏目
        private void CreateRightAccount(HtmlSelect listcontrolright, int TopMenuId)
        {
            Johnny.CMS.BLL.SystemInfo.TopMenuBinding bll = new Johnny.CMS.BLL.SystemInfo.TopMenuBinding();
            listcontrolright.DataSource = bll.GetList(TopMenuId);
            listcontrolright.DataTextField = "MenuCategoryName";
            listcontrolright.DataValueField = "MenuCategoryId";
            listcontrolright.DataBind();
        }
    }
}