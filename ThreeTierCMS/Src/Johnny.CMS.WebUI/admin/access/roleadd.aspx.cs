using System;
using System.Web.UI.WebControls;

using Johnny.Component.Utility;

namespace Johnny.CMS.admin.access
{
    public partial class roleadd : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                litPageTitle.Text = GetLabelText("Role_Title");
                litRoleName.Text = GetLabelText("Role_RoleName");
                txtRoleName.ToolTip = GetLabelText("Role_RoleName");
                litDescription.Text = GetLabelText("Role_Description");
                txtDescription.ToolTip = GetLabelText("Role_Description");

                if (Request.QueryString["action"] == "modify")
                {
                    //get RoleId
                    int RoleId = Convert.ToInt32(Request.QueryString["id"]);

                    //Role entity
                    Johnny.CMS.BLL.Access.Role bll = new Johnny.CMS.BLL.Access.Role();

                    //bind data
                    Johnny.CMS.OM.Access.Role model = new Johnny.CMS.OM.Access.Role();
                    model = bll.GetModel(RoleId);

                    txtRoleName.Text = model.RoleName;
                    txtDescription.Text = model.Description;

                    btnAdd.ButtonType = Johnny.Controls.Web.Button.Button.EnumButtonType.Save;
//                    btnAdd.Text = CONST_BUTTONTEXT_SAVE;
                }

                //RFVldtRoleName.ErrorMessage = GetMessage("E01501", txtRoleName.MaxLength.ToString());
            }
        }

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            //validation
            if (!CheckInputEmptyAndLength(txtRoleName, "E01501", "E01502", false))
                return;

            if (!CheckInputLength(txtDescription, "E01502", false))
                return;

            Johnny.CMS.BLL.Access.Role bll = new Johnny.CMS.BLL.Access.Role();
            Johnny.CMS.OM.Access.Role model = new Johnny.CMS.OM.Access.Role();
            if (Request.QueryString["action"] == "modify")
            {
                //update
                model.RoleId = Convert.ToInt32(Request.QueryString["id"]);
                model.RoleName = txtRoleName.Text;
                model.Description = txtDescription.Text;

                bll.Update(model);
                SetMessage(GetMessage("C00003"));
            }
            else
            {
                //insert 
                model.RoleName = txtRoleName.Text;
                model.Description = txtDescription.Text;
                
                if (bll.Add(model) > 0)
                {
                    SetMessage(GetMessage("C00001"));
                    txtRoleName.Text = "";
                    txtDescription.Text = "";
                }
                else
                    SetMessage(GetMessage("C00002"));
            }

        }        
    }
}