using System;
using System.Web.UI.WebControls;

using Johnny.Library.Helper;
using Johnny.Component.Utility;

namespace Johnny.CMS.admin.seh
{
    public partial class websiteadd : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                litPageTitle.Text = GetLabelText("Website_Title");
                litWebsiteCategory.Text = GetLabelText("Website_WebsiteCategoryId");
                ddlCategory.ToolTip = GetLabelText("Website_WebsiteCategoryId");
                litWebsiteName.Text = GetLabelText("Website_WebsiteName");
                txtWebsiteName.ToolTip = GetLabelText("Website_WebsiteName");
                litDescription.Text = GetLabelText("Website_Description");
                txtDescription.ToolTip = GetLabelText("Website_Description");
                litURL.Text = GetLabelText("Website_URL");
                txtURL.ToolTip = GetLabelText("Website_URL");
                litHits.Text = GetLabelText("Website_Hits");
                txtHits.ToolTip = GetLabelText("Website_Hits");
                litIsDisplay.Text = GetLabelText("Website_IsDisplay");
                rdbDisplay0.Text = GetLabelText("Common_Yes");
                rdbDisplay1.Text = GetLabelText("Common_No");
                litRdbDisplayTip.Text = GetLabelText("Website_IsDisplay");
                litCreatedTime.Text = GetLabelText("Common_CreatedTime");
                litCreatedByName.Text = GetLabelText("Common_CreatedByName");
                litUpdatedTime.Text = GetLabelText("Common_UpdatedTime");
                litUpdatedByName.Text = GetLabelText("Common_UpdatedByName");

                if (Request.QueryString["action"] == "modify")
                {
                    //get WebsiteId
                    int WebsiteId = Convert.ToInt32(Request.QueryString["id"]);

                    Johnny.CMS.BLL.SeH.Website bll = new Johnny.CMS.BLL.SeH.Website();
                    Johnny.CMS.OM.SeH.Website model = new Johnny.CMS.OM.SeH.Website();
                    model = bll.GetModel(WebsiteId);

                    CreateddlCategory();
                    foreach (ListItem item in ddlCategory.Items)
                    {
                        if (DataConvert.GetInt32(item.Value) == model.WebsiteCategoryId)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                    txtWebsiteName.Text = model.WebsiteName;
                    txtDescription.Text = model.Description;
                    txtURL.Text = model.URL;
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

                //RFVldtPermissionName.ErrorMessage = GetMessage("E01301", txtPermissionName.MaxLength.ToString());
            }
        }

        private void CreateddlCategory()
        {
            Johnny.CMS.BLL.SeH.WebsiteCategory category = new Johnny.CMS.BLL.SeH.WebsiteCategory();
            ddlCategory.DataSource = category.GetList();
            ddlCategory.DataTextField = "WebsiteCategoryName";
            ddlCategory.DataValueField = "WebsiteCategoryId";
            ddlCategory.DataBind();
        }

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            //check category name
            if (!CheckInputEmptyAndLength(txtWebsiteName, "E01301", "E01302", false))
                return;

            Johnny.CMS.BLL.SeH.Website bll = new Johnny.CMS.BLL.SeH.Website();
            Johnny.CMS.OM.SeH.Website model = new Johnny.CMS.OM.SeH.Website();

            if (Request.QueryString["action"] == "modify")
            {
                //update
                model.WebsiteId = Convert.ToInt32(Request.QueryString["id"]);
                model.WebsiteName = txtWebsiteName.Text;
                model.WebsiteCategoryId = DataConvert.GetInt32(ddlCategory.SelectedValue);
                model.Description = txtDescription.Text;
                model.URL = txtURL.Text;
                model.Hits = DataConvert.GetInt32(txtHits.Text);
                model.UpdatedTime = System.DateTime.Now;
                model.UpdatedById = DataConvert.GetInt32(Session["UserId"]);
                model.UpdatedByName = DataConvert.GetString(Session["UserName"]);

                bll.Update(model);
                SetMessage(GetMessage("C00003"));
            }
            else
            {
                //insert
                model.WebsiteName = txtWebsiteName.Text;
                model.WebsiteCategoryId = DataConvert.GetInt32(ddlCategory.SelectedValue);                
                model.Description = txtDescription.Text;
                model.URL = txtURL.Text;
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
                    txtWebsiteName.Text = "";
                    txtDescription.Text = "";
                    txtURL.Text = "";
                    txtHits.Text = "0";
                    rdbDisplay1.Checked = false;
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