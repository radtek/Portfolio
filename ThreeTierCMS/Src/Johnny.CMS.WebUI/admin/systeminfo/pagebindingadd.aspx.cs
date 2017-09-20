using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;

using Johnny.CMS.OM;
using Johnny.CMS.BLL;
using Johnny.Library.Helper;
using Johnny.Component.Utility;

namespace Johnny.CMS.admin
{
    public partial class pagebindingadd : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                litPageTitle.Text = GetLabelText("PageBinding_Title");
                litMenuCategory.Text = GetLabelText("Pagebinding_MenuCategoryId");
                ddlCategory.ToolTip = GetLabelText("Pagebinding_MenuCategoryId");
                litTitle.Text = GetLabelText("Pagebinding_Title");
                txtTitle.ToolTip = GetLabelText("Pagebinding_Title");
                litListMenu.Text = GetLabelText("Pagebinding_ListMenuId");
                ddlListMenu.ToolTip = GetLabelText("Pagebinding_ListMenuId");
                litAddMenu.Text = GetLabelText("Pagebinding_AddMenuId");
                ddlAddMenu.ToolTip = GetLabelText("Pagebinding_AddMenuId");

                if (Request.QueryString["action"] == "modify")
                {
                    //get PageBindingId
                    int PageBindingId = Convert.ToInt32(Request.QueryString["id"]);

                    //Permission entity
                    Johnny.CMS.BLL.SystemInfo.PageBinding bll = new Johnny.CMS.BLL.SystemInfo.PageBinding();

                    //bind data
                    Johnny.CMS.OM.SystemInfo.PageBinding model = new Johnny.CMS.OM.SystemInfo.PageBinding();
                    model = bll.GetModel(PageBindingId);

                    CreateddlCategory();
                    foreach (ListItem item in ddlCategory.Items)
                    {
                        if (DataConvert.GetInt32(item.Value) == model.MenuCategoryId)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                    txtTitle.Text = model.Title;
                    CreateddlMenu();
                    foreach (ListItem item in ddlListMenu.Items)
                    {
                        if (DataConvert.GetInt32(item.Value) == model.ListMenuId)
                        {
                            item.Selected = true;
                            break;
                        }
                    }

                    foreach (ListItem item in ddlAddMenu.Items)
                    {
                        if (DataConvert.GetInt32(item.Value) == model.AddMenuId)
                        {
                            item.Selected = true;
                            break;
                        }
                    }                    

                    btnAdd.ButtonType = Johnny.Controls.Web.Button.Button.EnumButtonType.Save;
                    //btnAdd.Text = CONST_BUTTONTEXT_SAVE;
                }
                else
                {
                    CreateddlCategory();
                    CreateddlMenu();
                }

                //RFVldtMenuName.Text = GetMessage("E00901", txtMenuName.MaxLength.ToString());
                //RFVldtPageLink.Text = GetMessage("E00903", txtPageLink.MaxLength.ToString());
            }

//                            <zr:ValidationRadioButton ID="rdbDisplay0" runat="server" GroupName="IsDisplay" Text="是" IsRequired="True" />
//                <zr:ValidationRadioButton ID="rdbDisplay1" runat="server" GroupName="IsDisplay" Text="否"  IsRequired="True" />
//<asp:Panel ID="IsDisplayTip" runat="server" class="msgNormal" tip="显示在左侧菜单栏里">显示在左侧菜单栏里</asp:Panel>
        }

        private void CreateddlCategory()
        {
            Johnny.CMS.BLL.SystemInfo.MenuCategory category = new Johnny.CMS.BLL.SystemInfo.MenuCategory();
            ddlCategory.DataSource = category.GetList();
            ddlCategory.DataTextField = "MenuCategoryName";
            ddlCategory.DataValueField = "MenuCategoryId";
            ddlCategory.DataBind();
        }

        private void CreateddlMenu()
        {
            Johnny.CMS.BLL.SystemInfo.Menu menu = new Johnny.CMS.BLL.SystemInfo.Menu();
            IList<Johnny.CMS.OM.SystemInfo.Menu> menuList;
            if (ddlCategory.SelectedIndex == -1)
                menuList = menu.GetList();
            else
                menuList = menu.GetListByCategory(DataConvert.GetInt32(ddlCategory.SelectedValue));
            ddlListMenu.DataSource = menuList;
            ddlListMenu.DataTextField = "MenuName";
            ddlListMenu.DataValueField = "MenuId";
            ddlListMenu.DataBind();

            ddlAddMenu.DataSource = menuList;
            ddlAddMenu.DataTextField = "MenuName";
            ddlAddMenu.DataValueField = "MenuId";
            ddlAddMenu.DataBind();
        }

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            Johnny.CMS.BLL.SystemInfo.PageBinding bll = new Johnny.CMS.BLL.SystemInfo.PageBinding();
            Johnny.CMS.OM.SystemInfo.PageBinding model = new Johnny.CMS.OM.SystemInfo.PageBinding();
            if (Request.QueryString["action"] == "modify")
            {
                //update
                model.PageBindingId = Convert.ToInt32(Request.QueryString["id"]);
                model.MenuCategoryId = DataConvert.GetInt32(ddlCategory.SelectedValue);
                model.Title = txtTitle.Text;
                model.ListMenuId = DataConvert.GetInt32(ddlListMenu.SelectedValue);
                model.AddMenuId = DataConvert.GetInt32(ddlAddMenu.SelectedValue);

                bll.Update(model);
                SetMessage(GetMessage("C00003"));
            }
            else
            {
                //insert                
                model.MenuCategoryId = DataConvert.GetInt32(ddlCategory.SelectedValue);
                model.Title = txtTitle.Text;
                model.ListMenuId = DataConvert.GetInt32(ddlListMenu.SelectedValue);
                model.AddMenuId = DataConvert.GetInt32(ddlAddMenu.SelectedValue);

                if (bll.Add(model) > 0)
                {
                    SetMessage(GetMessage("C00001"));
                    ddlCategory.SelectedIndex = 0;
                    txtTitle.Text = "";
                }
                else
                    SetMessage(GetMessage("C00002"));
            }
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateddlMenu();
        }
    }
}