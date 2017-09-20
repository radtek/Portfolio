using System;
using System.Web.UI.WebControls;

using Johnny.CMS.OM;
using Johnny.CMS.BLL;
using Johnny.Library.Helper;
using Johnny.Component.Utility;

namespace Johnny.CMS.admin.seh
{
    public partial class softwareadd : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                litPageTitle.Text = GetLabelText("Software_Title");
                litSoftwareName.Text = GetLabelText("Software_SoftwareName");
                txtSoftwareName.ToolTip = GetLabelText("Software_SoftwareName");
                litShortDescription.Text = GetLabelText("Software_ShortDescription");
                txtShortDescription.ToolTip = GetLabelText("Software_ShortDescription");
                litContent.Text = GetLabelText("Software_Description");
                litImage.Text = GetLabelText("Software_Image");
                txtImage.ToolTip = GetLabelText("Software_Image");
                litFeature1.Text = GetLabelText("Software_Feature1");
                txtFeature1.ToolTip = GetLabelText("Software_Feature1");
                litFeature2.Text = GetLabelText("Software_Feature2");
                txtFeature2.ToolTip = GetLabelText("Software_Feature2");
                litFeature3.Text = GetLabelText("Software_Feature3");
                txtFeature3.ToolTip = GetLabelText("Software_Feature3");
                litFeature4.Text = GetLabelText("Software_Feature4");
                txtFeature4.ToolTip = GetLabelText("Software_Feature4");
                litDownloadUrl.Text = GetLabelText("Software_DownloadUrl");
                txtDownloadUrl.ToolTip = GetLabelText("Software_DownloadUrl");
                litDocumentTitle.Text = GetLabelText("Software_DocumentTitle");
                txtDocumentTitle.ToolTip = GetLabelText("Software_DocumentTitle");
                litDocumentDescription.Text = GetLabelText("Software_DocumentDescription");
                txtDocumentDescription.ToolTip = GetLabelText("Software_DocumentDescription");
                litDocumentUrl.Text = GetLabelText("Software_DocumentUrl");
                txtDocumentUrl.ToolTip = GetLabelText("Software_DocumentUrl");
                litHits.Text = GetLabelText("Software_Hits");
                txtHits.ToolTip = GetLabelText("Software_Hits");
                litDownloads.Text = GetLabelText("Software_Downloads");
                txtDownloads.ToolTip = GetLabelText("Software_Downloads");
                litIsDisplay.Text = GetLabelText("Software_IsDisplay");
                rdbDisplay0.Text = GetLabelText("Common_Yes");
                rdbDisplay1.Text = GetLabelText("Common_No");
                litRdbDisplayTip.Text = GetLabelText("Software_IsDisplay");
                litCreatedTime.Text = GetLabelText("Common_CreatedTime");
                litCreatedByName.Text = GetLabelText("Common_CreatedByName");
                litUpdatedTime.Text = GetLabelText("Common_UpdatedTime");
                litUpdatedByName.Text = GetLabelText("Common_UpdatedByName");

                if (Request.QueryString["action"] == "modify")
                {
                    //get id
                    int SoftwareId = Convert.ToInt32(Request.QueryString["id"]);

                    Johnny.CMS.BLL.SeH.Software bll = new Johnny.CMS.BLL.SeH.Software();
                    Johnny.CMS.OM.SeH.Software model = new Johnny.CMS.OM.SeH.Software();
                    model = bll.GetModel(SoftwareId);

                    txtSoftwareName.Text = model.SoftwareName;
                    txtShortDescription.Text = model.ShortDescription;
                    fckContent.Value = StringHelper.htmlOutputText(model.Description);
                    txtImage.Text = model.Image;
                    txtFeature1.Text = model.Feature1;
                    txtFeature2.Text = model.Feature2;
                    txtFeature3.Text = model.Feature3;
                    txtFeature4.Text = model.Feature4;
                    txtDownloadUrl.Text = model.DownloadUrl;
                    txtDocumentTitle.Text = model.DocumentTitle;
                    txtDocumentDescription.Text = model.DocumentDescription;
                    txtDocumentUrl.Text = model.DocumentUrl;
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
                    rdbDisplay0.Checked = true;
                    txtHits.Text = "0";
                    txtDownloads.Text = "0";
                }
            }
        }

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            //validation
            if (!CheckInputEmptyAndLength(txtSoftwareName, "E00901", "E00902", false))
                return;
            if (!CheckInputEmptyAndLength(txtHits, "E00901", "E00902", false))
                return;

            Johnny.CMS.BLL.SeH.Software bll = new Johnny.CMS.BLL.SeH.Software();
            Johnny.CMS.OM.SeH.Software model = new Johnny.CMS.OM.SeH.Software();
            if (Request.QueryString["action"] == "modify")
            {
                //update
                model.SoftwareId = Convert.ToInt32(Request.QueryString["id"]);
                model.SoftwareName = txtSoftwareName.Text;
                model.ShortDescription = txtShortDescription.Text;
                model.Description = StringHelper.htmlInputText(fckContent.Value);
                model.Image = txtImage.Text;
                model.Feature1 = txtFeature1.Text;
                model.Feature2 = txtFeature2.Text;
                model.Feature3 = txtFeature3.Text;
                model.Feature4 = txtFeature4.Text;
                model.DownloadUrl = txtDownloadUrl.Text;
                model.DocumentTitle = txtDocumentTitle.Text;
                model.DocumentDescription = txtDocumentDescription.Text;
                model.DocumentUrl = txtDocumentUrl.Text;
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
                model.SoftwareName = txtSoftwareName.Text;
                model.ShortDescription = txtShortDescription.Text;
                model.Description = StringHelper.htmlInputText(fckContent.Value);
                model.Image = txtImage.Text;
                model.Feature1 = txtFeature1.Text;
                model.Feature2 = txtFeature2.Text;
                model.Feature3 = txtFeature3.Text;
                model.Feature4 = txtFeature4.Text;
                model.DownloadUrl = txtDownloadUrl.Text;
                model.DocumentTitle = txtDocumentTitle.Text;
                model.DocumentDescription = txtDocumentDescription.Text;
                model.DocumentUrl = txtDocumentUrl.Text;                
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
                    txtSoftwareName.Text = "";
                    txtShortDescription.Text = "";
                    fckContent.Value = "";
                    txtImage.Text = "";
                    txtFeature1.Text = "";
                    txtFeature2.Text = "";
                    txtFeature3.Text = "";
                    txtFeature4.Text = "";
                    txtDownloadUrl.Text = "";
                    txtDocumentTitle.Text = "";
                    txtDocumentDescription.Text = "";
                    txtDocumentUrl.Text = "";
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