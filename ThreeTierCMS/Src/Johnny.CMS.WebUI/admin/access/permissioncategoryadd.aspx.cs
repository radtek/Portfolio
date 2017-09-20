using System;
using System.Web.UI.WebControls;

using Johnny.Component.Utility;

namespace Johnny.CMS.admin.access
{
    public partial class permissioncategoryadd : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                litPageTitle.Text = GetLabelText("PermissionCategory_Title");
                litPermissionCategory.Text = GetLabelText("Permissioncategory_PermissionCategoryName");
                txtPermissionCategoryName.ToolTip = GetLabelText("Permissioncategory_PermissionCategoryName");

                if (Request.QueryString["action"] == "modify")
                {
                    //get PermissionCategoryId
                    int PermissionCategoryId = Convert.ToInt32(Request.QueryString["id"]);

                    //PermissionCategory entity
                    Johnny.CMS.BLL.Access.PermissionCategory bll = new Johnny.CMS.BLL.Access.PermissionCategory();

                    //bind data
                    Johnny.CMS.OM.Access.PermissionCategory model = new Johnny.CMS.OM.Access.PermissionCategory();
                    model = bll.GetModel(PermissionCategoryId);

                    txtPermissionCategoryName.Text = model.PermissionCategoryName;

                    btnAdd.ButtonType = Johnny.Controls.Web.Button.Button.EnumButtonType.Save;
                    //btnAdd.Text = CONST_BUTTONTEXT_SAVE;
                }

                //RFVldtPermissionName.ErrorMessage = GetMessage("E01201", txtDescription.MaxLength.ToString());
            }
        }

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            //check category name
            if (!CheckInputEmptyAndLength(txtPermissionCategoryName, "E01201", "E01202", false))
                return;
            
            Johnny.CMS.BLL.Access.PermissionCategory bll = new Johnny.CMS.BLL.Access.PermissionCategory();
            Johnny.CMS.OM.Access.PermissionCategory model = new Johnny.CMS.OM.Access.PermissionCategory();  
            
            if (Request.QueryString["action"] == "modify")
            {
                //update
                model.PermissionCategoryId = Convert.ToInt32(Request.QueryString["id"]);
                model.PermissionCategoryName = txtPermissionCategoryName.Text;

                bll.Update(model);
                SetMessage(GetMessage("C00003"));
                
            }
            else
            {
                //insert
                model.PermissionCategoryName = txtPermissionCategoryName.Text;

                if (bll.Add(model) > 0)
                {
                    SetMessage(GetMessage("C00001"));
                    txtPermissionCategoryName.Text = "";
                }
                else
                    SetMessage(GetMessage("C00002"));
            }
        }
    }
}