using System;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Collections;

using Johnny.CMS.OM;
using Johnny.CMS.BLL;
using Johnny.Library.Helper;
using Johnny.Component.Utility;
using Johnny.CMS.admin.usercontrol;

namespace Johnny.CMS.admin
{
    public partial class topmenubinding : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!IsPostBack)
            {
                lblTopMenuName.Text = GetLabelText("Topmenubinding_TopMenuId");
                CreateddlTopMenu();
                CreateMenuCategoryList();                
            }
        }

        //角色
        private void CreateddlTopMenu()
        {
            Johnny.CMS.BLL.SystemInfo.TopMenu topMenu = new Johnny.CMS.BLL.SystemInfo.TopMenu();
            ddlTopMenus.DataSource = topMenu.GetList();
            ddlTopMenus.DataTextField = "TopMenuName";
            ddlTopMenus.DataValueField = "TopMenuId";
            ddlTopMenus.DataBind();
        }

        //动态绑定菜单栏目列表
        private void CreateMenuCategoryList()
        {
            string hdnSeletedClientID = "";
            string hdnRightClientID = "";
            menucontrol menuCtrl = (menucontrol)LoadControl("..\\usercontrol\\menucontrol.ascx");
            menuCtrl.TopMenuId = DataConvert.GetInt32(ddlTopMenus.SelectedValue);
            PlaceHolder1.Controls.Add(menuCtrl);
            HtmlInputHidden hdnValue = (HtmlInputHidden)menuCtrl.FindControl("hdnSelected");
            HtmlSelect hdnRightList = (HtmlSelect)menuCtrl.FindControl("lstRight");
            hdnSeletedClientID = hdnSeletedClientID + hdnValue.ClientID + ",";
            hdnRightClientID = hdnRightClientID + hdnRightList.ClientID + ",";

            btnSave.Attributes.Add("onclick", "return Save('" + ddlTopMenus.ClientID + "','" + hdnSeletedClientID + "','" + hdnAllSelected.ClientID + "');");
            btnReset.Attributes.Add("onclick", "listClear('" + hdnRightClientID + "','" + hdnSeletedClientID + "','" + hdnAllSelected.ClientID + "');return false;");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string[] accouts = hdnAllSelected.Value.Split(',');
            int TopMenuId = DataConvert.GetInt32(ddlTopMenus.SelectedValue);

            Johnny.CMS.BLL.SystemInfo.TopMenuBinding bll = new Johnny.CMS.BLL.SystemInfo.TopMenuBinding();
            bll.Delete(TopMenuId);

            for (int ix = 0; ix < accouts.Length; ix++)
            {
                if (accouts[ix] != string.Empty)
                {
                    Johnny.CMS.OM.SystemInfo.TopMenuBinding model = new Johnny.CMS.OM.SystemInfo.TopMenuBinding();
                    model.TopMenuId = TopMenuId;
                    model.MenuCategoryId = DataConvert.GetInt32(accouts[ix]);
                    if (bll.Add(model) > 0)
                    {
                        SetMessage(GetMessage("C00001"));
                    }
                    else
                        SetMessage(GetMessage("C00002"));
                    
                }
            }
            CreateMenuCategoryList();
        }

        protected void ddlTopMenus_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateMenuCategoryList();
        }
    }
}