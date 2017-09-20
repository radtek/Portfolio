using System;
using System.Web.UI.WebControls;

using Johnny.Library.Helper;
using Johnny.Component.Utility;

namespace Johnny.CMS.admin.access
{
    public partial class adminroleadd : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                litPageTitle.Text = GetLabelText("AdminRole_Title");
                litAdministrator.Text = GetLabelText("Adminrole_AdminId");
                ddlAdministrators.ToolTip = GetLabelText("Adminrole_AdminId");
                litRole.Text = GetLabelText("Adminrole_RoleId");
                ddlRoles.ToolTip = GetLabelText("Adminrole_RoleId");

                if (Request.QueryString["action"] == "modify")
                {
                    //get AdminRoleId
                    int AdminRoleId = Convert.ToInt32(Request.QueryString["id"]);

                    //UserRole entity
                    Johnny.CMS.BLL.Access.AdminRole bll = new Johnny.CMS.BLL.Access.AdminRole();
                    Johnny.CMS.OM.Access.AdminRole model = new Johnny.CMS.OM.Access.AdminRole();
                    model = bll.GetModel(AdminRoleId);

                    CreateddlAdministrators();
                    foreach (ListItem item in ddlAdministrators.Items)
                    {
                        if (DataConvert.GetInt32(item.Value) == model.AdminId)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                    CreateddlRole();
                    foreach (ListItem item in ddlRoles.Items)
                    {
                        if (DataConvert.GetInt32(item.Value) == model.RoleId)
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
                    CreateddlAdministrators();
                    CreateddlRole();
                }
            }
        }

        private void CreateddlAdministrators()
        {
            Johnny.CMS.BLL.Access.Administrator administrator = new Johnny.CMS.BLL.Access.Administrator();
            ddlAdministrators.DataSource = administrator.GetList();
            ddlAdministrators.DataTextField = "AdminName";
            ddlAdministrators.DataValueField = "AdminId";
            ddlAdministrators.DataBind();
        }
        private void CreateddlRole()
        {
            Johnny.CMS.BLL.Access.Role role = new Johnny.CMS.BLL.Access.Role();
            ddlRoles.DataSource = role.GetList();
            ddlRoles.DataTextField = "RoleName";
            ddlRoles.DataValueField = "RoleId";
            ddlRoles.DataBind();
        }

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            if (ddlAdministrators.Items.Count < 0 || ddlAdministrators.SelectedIndex < 0)
            {
                SetMessage(GetMessage("E01401"));
                ddlAdministrators.Focus();
                return;
            }
            if (ddlRoles.Items.Count < 0 || ddlRoles.SelectedIndex < 0)
            {
                SetMessage(GetMessage("E01402"));
                ddlRoles.Focus();
                return;
            }


            Johnny.CMS.BLL.Access.AdminRole bll = new Johnny.CMS.BLL.Access.AdminRole();
            Johnny.CMS.OM.Access.AdminRole model = new Johnny.CMS.OM.Access.AdminRole();

            if (Request.QueryString["action"] == "modify")
            {
                //update
                model.AdminRoleId = Convert.ToInt32(Request.QueryString["id"]);
                model.AdminId = DataConvert.GetInt32(ddlAdministrators.SelectedValue);
                model.RoleId = DataConvert.GetInt32(ddlRoles.SelectedValue);

                bll.Update(model);
                SetMessage(GetMessage("C00003"));
            }
            else
            {
                //insert
                model.AdminId = DataConvert.GetInt32(ddlAdministrators.SelectedValue);
                model.RoleId = DataConvert.GetInt32(ddlRoles.SelectedValue);

                if (bll.Add(model) > 0)
                {
                    SetMessage(GetMessage("C00001"));
                    ddlAdministrators.SelectedIndex = 0;
                    ddlRoles.SelectedIndex = 0;

                }
                else
                    SetMessage(GetMessage("C00002"));
            }
        }
    }
}