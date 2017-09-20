using System;
using System.Web.UI.WebControls;

using Johnny.Library.Helper;
using Johnny.Component.Utility;

namespace Johnny.CMS.admin.access
{
    public partial class permissionadd : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                litPageTitle.Text = GetLabelText("Permission_Title");
                litPermissionCategory.Text = GetLabelText("Permission_PermissionCategoryId");
                ddlCategory.ToolTip = GetLabelText("Permission_PermissionCategoryId");
                litPermissionName.Text = GetLabelText("Permission_PermissionName");
                txtPermissionName.ToolTip = GetLabelText("Permission_PermissionName");

                if (Request.QueryString["action"] == "modify")
                {
                    //get PermissionId
                    int PermissionId = Convert.ToInt32(Request.QueryString["id"]);

                    //Permission entity
                    Johnny.CMS.BLL.Access.Permission bll = new Johnny.CMS.BLL.Access.Permission();

                    //bind data
                    Johnny.CMS.OM.Access.Permission model = new Johnny.CMS.OM.Access.Permission();
                    model = bll.GetModel(PermissionId);

                    CreateddlCategory();
                    foreach (ListItem item in ddlCategory.Items)
                    {
                        if (DataConvert.GetInt32(item.Value) == model.PermissionCategoryId)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                    txtPermissionName.Text = model.PermissionName;

                    btnAdd.ButtonType = Johnny.Controls.Web.Button.Button.EnumButtonType.Save;
                    //btnAdd.Text = CONST_BUTTONTEXT_SAVE;
                }
                else
                {
                    CreateddlCategory();
                }

                //RFVldtPermissionName.ErrorMessage = GetMessage("E01301", txtPermissionName.MaxLength.ToString());
            }
        }

        private void CreateddlCategory()
        {
            Johnny.CMS.BLL.Access.PermissionCategory category = new Johnny.CMS.BLL.Access.PermissionCategory();
            ddlCategory.DataSource = category.GetList();
            ddlCategory.DataTextField = "PermissionCategoryName";
            ddlCategory.DataValueField = "PermissionCategoryId";
            ddlCategory.DataBind();
        }

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            //check category name
            if (!CheckInputEmptyAndLength(txtPermissionName, "E01301", "E01302", false))
                return;

            Johnny.CMS.BLL.Access.Permission bll = new Johnny.CMS.BLL.Access.Permission();
            Johnny.CMS.OM.Access.Permission model = new Johnny.CMS.OM.Access.Permission();

            if (Request.QueryString["action"] == "modify")
            {
                //update
                model.PermissionId = Convert.ToInt32(Request.QueryString["id"]);
                model.PermissionName = txtPermissionName.Text;
                model.PermissionCategoryId = DataConvert.GetInt32(ddlCategory.SelectedValue);

                bll.Update(model);
                SetMessage(GetMessage("C00003"));
            }
            else
            {
                //insert
                model.PermissionName = txtPermissionName.Text;
                model.PermissionCategoryId = DataConvert.GetInt32(ddlCategory.SelectedValue);
                
                if (bll.Add(model) > 0)
                {
                    SetMessage(GetMessage("C00001"));
                    ddlCategory.SelectedIndex = 0;
                    txtPermissionName.Text = "";
                }
                else
                    SetMessage(GetMessage("C00002"));
            }
        }
    }
}