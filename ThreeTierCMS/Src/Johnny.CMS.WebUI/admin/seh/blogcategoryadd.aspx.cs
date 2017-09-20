using System;
using System.Web.UI.WebControls;

using Johnny.Library.Helper;
using Johnny.Component.Utility;

namespace Johnny.CMS.admin.seh
{
    public partial class blogcategoryadd : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                litPageTitle.Text = GetLabelText("BlogCategory_Title");
                litBlogCategoryName.Text = GetLabelText("Blogcategory_BlogCategoryName");
                txtBlogCategoryName.ToolTip = GetLabelText("Blogcategory_BlogCategoryName");

                if (Request.QueryString["action"] == "modify")
                {
                    //primary key
                    int BlogCategoryId = DataConvert.GetInt32(Request.QueryString["id"]);

                    Johnny.CMS.BLL.SeH.BlogCategory bll = new Johnny.CMS.BLL.SeH.BlogCategory();
                    Johnny.CMS.OM.SeH.BlogCategory model = new Johnny.CMS.OM.SeH.BlogCategory();
                    model = bll.GetModel(BlogCategoryId);

                    txtBlogCategoryName.Text = model.BlogCategoryName;

                    btnAdd.ButtonType = Johnny.Controls.Web.Button.Button.EnumButtonType.Save;
                    //btnAdd.Text = CONST_BUTTONTEXT_SAVE;
                }

                //RFVldtMenuCategoryName.ErrorMessage = GetMessage("E00801", txtMenuCategoryName.MaxLength.ToString());
            }
        }

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            //validation
            if (!CheckInputEmptyAndLength(txtBlogCategoryName, "E00801", "E00802", false))
                return;

            Johnny.CMS.BLL.SeH.BlogCategory bll = new Johnny.CMS.BLL.SeH.BlogCategory();
            Johnny.CMS.OM.SeH.BlogCategory model = new Johnny.CMS.OM.SeH.BlogCategory();

            if (Request.QueryString["action"] == "modify")
            {
                //update
                model.BlogCategoryId = DataConvert.GetInt32(Request.QueryString["id"]);
                model.BlogCategoryName = txtBlogCategoryName.Text;

                bll.Update(model);
                SetMessage(GetMessage("C00003"));
            }
            else
            {
                //insert
                model.BlogCategoryName = txtBlogCategoryName.Text;

                if (bll.Add(model) > 0)
                {
                    SetMessage(GetMessage("C00001"));
                    txtBlogCategoryName.Text = "";
                }
                else
                    SetMessage(GetMessage("C00002"));
            }
        }        
    }
}