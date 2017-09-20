using System;
using System.Web.UI.WebControls;

using Johnny.CMS.OM;
using Johnny.CMS.BLL;
using Johnny.Library.Helper;
using Johnny.Component.Utility;

namespace Johnny.CMS.admin.seh
{
    public partial class releaseadd : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                litPageTitle.Text = GetLabelText("Release_Title");
                litSoftware.Text = GetLabelText("Release_SoftwareId");
                ddlSoftware.ToolTip = GetLabelText("Release_SoftwareId");
                litReleaseName.Text = GetLabelText("Release_ReleaseName");
                txtReleaseName.ToolTip = GetLabelText("Release_ReleaseName");
                litReleaseDate.Text = GetLabelText("Release_ReleaseDate");
                txtReleaseDate.ToolTip = GetLabelText("Release_ReleaseDate");
                litContent.Text = GetLabelText("Release_Description");
                litHits.Text = GetLabelText("Release_Hits");
                txtHits.ToolTip = GetLabelText("Release_Hits");
                litDownloads.Text = GetLabelText("Release_Downloads");
                txtDownloads.ToolTip = GetLabelText("Release_Downloads");
                litIsDisplay.Text = GetLabelText("Release_IsDisplay");
                rdbDisplay0.Text = GetLabelText("Common_Yes");
                rdbDisplay1.Text = GetLabelText("Common_No");
                litRdbDisplayTip.Text = GetLabelText("Release_IsDisplay");
                litCreatedTime.Text = GetLabelText("Common_CreatedTime");
                litCreatedByName.Text = GetLabelText("Common_CreatedByName");
                litUpdatedTime.Text = GetLabelText("Common_UpdatedTime");
                litUpdatedByName.Text = GetLabelText("Common_UpdatedByName");

                if (Request.QueryString["action"] == "modify")
                {
                    //get id
                    int ReleaseId = Convert.ToInt32(Request.QueryString["id"]);

                    Johnny.CMS.BLL.SeH.Release bll = new Johnny.CMS.BLL.SeH.Release();
                    Johnny.CMS.OM.SeH.Release model = new Johnny.CMS.OM.SeH.Release();
                    model = bll.GetModel(ReleaseId);

                    CreateddlSoftware();
                    foreach (ListItem item in ddlSoftware.Items)
                    {
                        if (DataConvert.GetInt32(item.Value) == model.SoftwareId)
                        {
                            item.Selected = true;
                            break;
                        }
                    }

                    txtReleaseName.Text = model.ReleaseName;
                    txtReleaseDate.Text = DataConvert.GetShortDateString(model.ReleaseDate);
                    fckContent.Value = StringHelper.htmlOutputText(model.Description);
                    txtHits.Text = DataConvert.GetString(model.Hits);
                    txtDownloads.Text = DataConvert.GetString(model.Downloads);
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
                    CreateddlSoftware();
                    rdbDisplay0.Checked = true;
                    txtHits.Text = "0";
                    txtDownloads.Text = "0";
                }
            }
        }

        private void CreateddlSoftware()
        {
            Johnny.CMS.BLL.SeH.Software bll = new Johnny.CMS.BLL.SeH.Software();
            ddlSoftware.DataSource = bll.GetList(null);
            ddlSoftware.DataTextField = "SoftwareName";
            ddlSoftware.DataValueField = "SoftwareId";
            ddlSoftware.DataBind();
        }

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            //validation
            if (!CheckInputEmptyAndLength(txtReleaseName, "E00901", "E00902", false))
                return;
            if (!CheckInputEmptyAndLength(txtHits, "E00901", "E00902", false))
                return;

            Johnny.CMS.BLL.SeH.Release bll = new Johnny.CMS.BLL.SeH.Release();
            Johnny.CMS.OM.SeH.Release model = new Johnny.CMS.OM.SeH.Release();
            if (Request.QueryString["action"] == "modify")
            {
                //update
                model.ReleaseId = Convert.ToInt32(Request.QueryString["id"]);
                model.SoftwareId = DataConvert.GetInt32(ddlSoftware.SelectedValue);
                model.ReleaseName = txtReleaseName.Text;
                model.ReleaseDate = DataConvert.GetDateTime(txtReleaseDate.Text);
                model.Description = StringHelper.htmlInputText(fckContent.Value);
                model.Hits = DataConvert.GetInt32(txtHits.Text);
                model.Downloads = DataConvert.GetInt32(txtDownloads.Text);
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
                model.SoftwareId = DataConvert.GetInt32(ddlSoftware.SelectedValue);
                model.ReleaseName = txtReleaseName.Text;
                model.ReleaseDate = DataConvert.GetDateTime(txtReleaseDate.Text);
                model.Description = StringHelper.htmlInputText(fckContent.Value);
                model.Hits = DataConvert.GetInt32(txtHits.Text);
                model.Downloads = DataConvert.GetInt32(txtDownloads.Text);
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
                    ddlSoftware.SelectedIndex = 0;
                    txtReleaseName.Text = "";
                    txtReleaseDate.Text = "";
                    fckContent.Value = "";
                    txtHits.Text = "0";
                    txtDownloads.Text = "0";
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