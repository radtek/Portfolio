using System;
using System.Web.UI.WebControls;

using Johnny.Library.Helper;
using Johnny.Component.Utility;

namespace Johnny.CMS.admin.seh
{
    public partial class websitecategoryadd : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                litPageTitle.Text = GetLabelText("WebsiteCategory_Title");
                litWebsiteCategoryName.Text = GetLabelText("Websitecategory_WebsiteCategoryName");
                txtWebsiteCategoryName.ToolTip = GetLabelText("Websitecategory_WebsiteCategoryName");

                if (Request.QueryString["action"] == "modify")
                {
                    //primary key
                    int WebsiteCategoryId = DataConvert.GetInt32(Request.QueryString["id"]);

                    Johnny.CMS.BLL.SeH.WebsiteCategory bll = new Johnny.CMS.BLL.SeH.WebsiteCategory();
                    Johnny.CMS.OM.SeH.WebsiteCategory model = new Johnny.CMS.OM.SeH.WebsiteCategory();
                    model = bll.GetModel(WebsiteCategoryId);

                    txtWebsiteCategoryName.Text = model.WebsiteCategoryName;

                    btnAdd.ButtonType = Johnny.Controls.Web.Button.Button.EnumButtonType.Save;
                    //btnAdd.Text = CONST_BUTTONTEXT_SAVE;
                }

                //RFVldtMenuCategoryName.ErrorMessage = GetMessage("E00801", txtMenuCategoryName.MaxLength.ToString());
            }
        }

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            //validation
            if (!CheckInputEmptyAndLength(txtWebsiteCategoryName, "E00801", "E00802", false))
                return;

            Johnny.CMS.BLL.SeH.WebsiteCategory bll = new Johnny.CMS.BLL.SeH.WebsiteCategory();
            Johnny.CMS.OM.SeH.WebsiteCategory model = new Johnny.CMS.OM.SeH.WebsiteCategory();

            if (Request.QueryString["action"] == "modify")
            {
                //update
                model.WebsiteCategoryId = DataConvert.GetInt32(Request.QueryString["id"]);
                model.WebsiteCategoryName = txtWebsiteCategoryName.Text;

                bll.Update(model);
                SetMessage(GetMessage("C00003"));
            }
            else
            {
                //insert
                model.WebsiteCategoryName = txtWebsiteCategoryName.Text;

                if (bll.Add(model) > 0)
                {
                    SetMessage(GetMessage("C00001"));
                    txtWebsiteCategoryName.Text = "";
                }
                else
                    SetMessage(GetMessage("C00002"));
            }
        }        
    }
}