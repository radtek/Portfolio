using System;
using System.Web.UI.WebControls;

using Johnny.CMS.OM;
using Johnny.Component.Utility;
using Johnny.CMS.BLL;

namespace Johnny.CMS.admin
{
    public partial class menucategoryadd : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                litPageTitle.Text = GetLabelText("MenuCategory_Title");
                litMenuCategoryName.Text = GetLabelText("Menucategory_MenuCategoryName");
                txtMenuCategoryName.ToolTip = GetLabelText("Menucategory_MenuCategoryName");

                if (Request.QueryString["action"] == "modify")
                {
                    //get PermissionCategoryId
                    int MenuCategoryId = Convert.ToInt32(Request.QueryString["id"]);

                    Johnny.CMS.BLL.SystemInfo.MenuCategory bll = new Johnny.CMS.BLL.SystemInfo.MenuCategory();
                    Johnny.CMS.OM.SystemInfo.MenuCategory model = new Johnny.CMS.OM.SystemInfo.MenuCategory();
                    model = bll.GetModel(MenuCategoryId);

                    txtMenuCategoryName.Text = model.MenuCategoryName;

                    btnAdd.ButtonType = Johnny.Controls.Web.Button.Button.EnumButtonType.Save;
                    //btnAdd.Text = CONST_BUTTONTEXT_SAVE;
                }

                //RFVldtMenuCategoryName.ErrorMessage = GetMessage("E00801", txtMenuCategoryName.MaxLength.ToString());
            }
        }

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            //check category name
            if (!CheckInputEmptyAndLength(txtMenuCategoryName, "E00801", "E00802", false))
                return;

            Johnny.CMS.BLL.SystemInfo.MenuCategory bll = new Johnny.CMS.BLL.SystemInfo.MenuCategory();
            Johnny.CMS.OM.SystemInfo.MenuCategory model = new Johnny.CMS.OM.SystemInfo.MenuCategory();

            if (Request.QueryString["action"] == "modify")
            {
                //update
                model.MenuCategoryId = Convert.ToInt32(Request.QueryString["id"]);
                model.MenuCategoryName = txtMenuCategoryName.Text;

                bll.Update(model);
                SetMessage(GetMessage("C00003"));
            }
            else
            {
                //insert
                model.MenuCategoryName = txtMenuCategoryName.Text;
                
                if (bll.Add(model) > 0)
                {
                    SetMessage(GetMessage("C00001"));
                    txtMenuCategoryName.Text = "";
                }
                else
                    SetMessage(GetMessage("C00002"));
            }
        }        
    }
}