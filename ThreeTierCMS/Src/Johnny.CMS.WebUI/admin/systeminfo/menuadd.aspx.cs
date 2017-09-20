using System;
using System.Web.UI.WebControls;

using Johnny.CMS.OM;
using Johnny.CMS.BLL;
using Johnny.Library.Helper;
using Johnny.Component.Utility;

namespace Johnny.CMS.admin
{
    public partial class menuadd : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                litPageTitle.Text = GetLabelText("Menu_Title");
                litMenuCategory.Text = GetLabelText("Menu_MenuCategoryId");
                ddlCategory.ToolTip = GetLabelText("Menu_MenuCategoryId");
                litMenuName.Text = GetLabelText("Menu_MenuName");
                txtMenuName.ToolTip = GetLabelText("Menu_MenuName");
                litPageLink.Text = GetLabelText("Menu_PageLink");
                txtPageLink.ToolTip = GetLabelText("Menu_PageLink");
                litToolTip.Text = GetLabelText("Menu_ToolTip");
                txtToolTip.ToolTip = GetLabelText("Menu_ToolTip");
                litImage.Text = GetLabelText("Menu_Image");
                txtImage.ToolTip = GetLabelText("Menu_Image");
                litPermission.Text = GetLabelText("Menu_PermissionId");
                ddlPermission.ToolTip = GetLabelText("Menu_PermissionId");
                litIsDisplay.Text = GetLabelText("Menu_IsDisplay");
                rdbDisplay0.Text = GetLabelText("Common_Yes");
                rdbDisplay1.Text = GetLabelText("Common_No");
                litRdbDisplayTip.Text = GetLabelText("Menu_IsDisplay");

                if (Request.QueryString["action"] == "modify")
                {
                    //get MenuId
                    int MenuId = Convert.ToInt32(Request.QueryString["id"]);

                    //Permission entity
                    Johnny.CMS.BLL.SystemInfo.Menu bll = new Johnny.CMS.BLL.SystemInfo.Menu();

                    //bind data
                    Johnny.CMS.OM.SystemInfo.Menu model = new Johnny.CMS.OM.SystemInfo.Menu();
                    model = bll.GetModel(MenuId);

                    CreateddlCategory();
                    foreach (ListItem item in ddlCategory.Items)
                    {
                        if (DataConvert.GetInt32(item.Value) == model.MenuCategoryId)
                        {
                            item.Selected = true;
                            break;
                        }
                    }

                    txtMenuName.Text = model.MenuName;
                    txtToolTip.Text = model.ToolTip;
                    txtPageLink.Text = model.PageLink;
                    txtImage.Text = model.Image;

                    CreateddlPermission();
                    foreach (ListItem item in ddlPermission.Items)
                    {
                        if (DataConvert.GetInt32(item.Value) == model.PermissionId)
                        {
                            item.Selected = true;
                            break;
                        }
                    }

                    if (model.IsDisplay)
                        rdbDisplay0.Checked = true;
                    else
                        rdbDisplay1.Checked = true;

                    btnAdd.ButtonType = Johnny.Controls.Web.Button.Button.EnumButtonType.Save;
                    //btnAdd.Text = CONST_BUTTONTEXT_SAVE;
                }
                else
                {
                    CreateddlCategory();
                    CreateddlPermission();
                    rdbDisplay0.Checked = true;
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

        private void CreateddlPermission()
        {
            Johnny.CMS.BLL.Access.Permission permission = new Johnny.CMS.BLL.Access.Permission();
            ddlPermission.DataSource = permission.GetList();
            ddlPermission.DataTextField = "PermissionName";
            ddlPermission.DataValueField = "PermissionId";
            ddlPermission.DataBind();
        }

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            //check category name
            if (!CheckInputEmptyAndLength(txtMenuName, "E00901", "E00902", false))
                return;
            //check page url
            if (!CheckInputEmptyAndLength(txtPageLink, "E00903", "E00904"))
                return;

            //tips
            if (!CheckInputLength(txtToolTip, "E00903", false))
                return;

            //image
            if (!CheckInputLength(txtImage, "E00903"))
                return;

            Johnny.CMS.BLL.SystemInfo.Menu bll = new Johnny.CMS.BLL.SystemInfo.Menu();
            Johnny.CMS.OM.SystemInfo.Menu model = new Johnny.CMS.OM.SystemInfo.Menu();
            if (Request.QueryString["action"] == "modify")
            {
                //update
                model.MenuId = Convert.ToInt32(Request.QueryString["id"]);
                model.MenuCategoryId = DataConvert.GetInt32(ddlCategory.SelectedValue);
                model.MenuName = txtMenuName.Text;
                model.ToolTip = txtToolTip.Text;
                model.PageLink = txtPageLink.Text;
                model.Image = txtImage.Text;
                model.PermissionId = DataConvert.GetInt32(ddlPermission.SelectedValue);
                model.IsDisplay = rdbDisplay0.Checked;

                bll.Update(model);
                SetMessage(GetMessage("C00003"));
            }
            else
            {
                //insert                
                model.MenuCategoryId = DataConvert.GetInt32(ddlCategory.SelectedValue);
                model.MenuName = txtMenuName.Text;
                model.ToolTip = txtToolTip.Text;
                model.PageLink = txtPageLink.Text;
                model.Image = txtImage.Text;
                model.PermissionId = DataConvert.GetInt32(ddlPermission.SelectedValue);
                model.IsDisplay = rdbDisplay0.Checked;

                if (bll.Add(model) > 0)
                {
                    SetMessage(GetMessage("C00001"));
                    ddlCategory.SelectedIndex = 0;
                    txtMenuName.Text = "";
                }
                else
                    SetMessage(GetMessage("C00002"));
            }
        }        
    }
}