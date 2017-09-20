using System;
using System.Web.UI.WebControls;

using Johnny.CMS.OM;
using Johnny.CMS.BLL;
using Johnny.Library.Helper;
using Johnny.Component.Utility;

namespace Johnny.CMS.admin.seh
{
    public partial class blogadd : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                litPageTitle.Text = GetLabelText("Blog_Title");
                litCategory.Text = GetLabelText("Blog_BlogCategoryId");
                ddlCategory.ToolTip = GetLabelText("Blog_BlogCategoryId");
                litTitle.Text = GetLabelText("Blog_Title");
                txtTitle.ToolTip = GetLabelText("Blog_Title");
                litTag.Text = GetLabelText("Blog_Tag");
                txtTag.ToolTip = GetLabelText("Blog_Tag");
                litContent.Text = GetLabelText("Blog_Content");
                litHits.Text = GetLabelText("Blog_Hits");
                txtHits.ToolTip = GetLabelText("Blog_Hits");
                litIsDisplay.Text = GetLabelText("Blog_IsDisplay");
                rdbDisplay0.Text = GetLabelText("Common_Yes");
                rdbDisplay1.Text = GetLabelText("Common_No");
                litRdbDisplayTip.Text = GetLabelText("Blog_IsDisplay");
                litCreatedTime.Text = GetLabelText("Common_CreatedTime");
                litCreatedByName.Text = GetLabelText("Common_CreatedByName");
                litUpdatedTime.Text = GetLabelText("Common_UpdatedTime");
                litUpdatedByName.Text = GetLabelText("Common_UpdatedByName");

                if (Request.QueryString["action"] == "modify")
                {
                    //get id
                    int BlogId = Convert.ToInt32(Request.QueryString["id"]);

                    Johnny.CMS.BLL.SeH.Blog bll = new Johnny.CMS.BLL.SeH.Blog();
                    Johnny.CMS.OM.SeH.Blog model = new Johnny.CMS.OM.SeH.Blog();
                    model = bll.GetModel(BlogId);

                    CreateddlCategory();
                    foreach (ListItem item in ddlCategory.Items)
                    {
                        if (DataConvert.GetInt32(item.Value) == model.BlogCategoryId)
                        {
                            item.Selected = true;
                            break;
                        }
                    }

                    txtTitle.Text = model.Title;
                    txtTag.Text = model.Tag;
                    fckContent.Value = model.Content;
                    txtHits.Text = DataConvert.GetString(model.Hits);
                    if (model.IsDisplay)
                        rdbDisplay0.Checked = true;
                    else
                        rdbDisplay1.Checked = true;
                    
                    lblCreatedTime.Text = DataConvert.GetLongDateString(model.CreatedTime);
                    lblCreatedByName.Text = model.CreatedByName;
                    lblUpdatedTime.Text = DataConvert.GetLongDateString(model.UpdatedTime);
                    lblUpdatedByName.Text = model.UpdatedByName;

                    btnAdd.ButtonType = Johnny.Controls.Web.Button.Button.EnumButtonType.Save;
                    //btnAdd.Text = CONST_BUTTONTEXT_SAVE;
                }
                else
                {
                    CreateddlCategory();
                    rdbDisplay0.Checked = true;
                    txtHits.Text = "0";
                }
            }
        }

        private void CreateddlCategory()
        {
            Johnny.CMS.BLL.SeH.BlogCategory bll = new Johnny.CMS.BLL.SeH.BlogCategory();
            ddlCategory.DataSource = bll.GetList();
            ddlCategory.DataTextField = "BlogCategoryName";
            ddlCategory.DataValueField = "BlogCategoryId";
            ddlCategory.DataBind();
        }

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            //validation
            if (!CheckInputEmptyAndLength(txtTitle, "E00901", "E00902", false))
                return;
            if (!CheckInputEmptyAndLength(txtHits, "E00901", "E00902", false))
                return;

            Johnny.CMS.BLL.SeH.Blog bll = new Johnny.CMS.BLL.SeH.Blog();
            Johnny.CMS.OM.SeH.Blog model = new Johnny.CMS.OM.SeH.Blog();
            if (Request.QueryString["action"] == "modify")
            {
                //update
                model.BlogId = Convert.ToInt32(Request.QueryString["id"]);
                model.BlogCategoryId = DataConvert.GetInt32(ddlCategory.SelectedValue);
                model.Title = txtTitle.Text;
                model.Tag = txtTag.Text;
                model.Content = fckContent.Value;
                model.Hits = DataConvert.GetInt32(txtHits.Text);               
                model.IsDisplay = rdbDisplay0.Checked;
                model.UpdatedTime = System.DateTime.Now;
                model.UpdatedById = DataConvert.GetInt32(Session["UserId"]);
                model.UpdatedByName = DataConvert.GetString(Session["UserName"]);

                bll.Update(model);
                SetMessage(GetMessage("C00003"));
            }
            else
            {
                //insert                
                model.BlogCategoryId = DataConvert.GetInt32(ddlCategory.SelectedValue);
                model.Title = txtTitle.Text;
                model.Tag = txtTag.Text;
                model.Content = StringHelper.htmlInputText(fckContent.Value);               
                model.Hits = DataConvert.GetInt32(txtHits.Text);
                model.IsDisplay = rdbDisplay0.Checked;
                model.CreatedTime = System.DateTime.Now;
                model.CreatedById = DataConvert.GetInt32(Session["UserId"]);
                model.CreatedByName = DataConvert.GetString(Session["UserName"]);
                model.UpdatedTime = System.DateTime.Now;
                model.UpdatedById = DataConvert.GetInt32(Session["UserId"]);
                model.UpdatedByName = DataConvert.GetString(Session["UserName"]);

                if (bll.Add(model) > 0)
                {
                    SetMessage(GetMessage("C00001"));
                    ddlCategory.SelectedIndex = 0;
                    txtTitle.Text = "";
                    txtTag.Text = "";
                    fckContent.Value = "";
                    txtHits.Text = "0";
                    rdbDisplay0.Checked = true;
                    lblCreatedTime.Text = "";
                    lblCreatedByName.Text = "";
                    lblUpdatedTime.Text = "";
                    lblUpdatedByName.Text = "";
                }
                else
                    SetMessage(GetMessage("C00002"));
            }
        }        
    }
}