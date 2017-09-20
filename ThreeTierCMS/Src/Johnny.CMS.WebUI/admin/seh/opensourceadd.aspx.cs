using System;
using System.Web.UI.WebControls;

using Johnny.CMS.OM;
using Johnny.CMS.BLL;
using Johnny.Library.Helper;
using Johnny.Component.Utility;

namespace Johnny.CMS.admin.seh
{
    public partial class opensourceadd : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                litPageTitle.Text = GetLabelText("OpenSource_Title");
                litOpenSourceName.Text = GetLabelText("Opensource_OpenSourceName");
                txtOpenSourceName.ToolTip = GetLabelText("Opensource_OpenSourceName");
                litShortDescription.Text = GetLabelText("Opensource_ShortDescription");
                txtShortDescription.ToolTip = GetLabelText("Opensource_ShortDescription");
                litContent.Text = GetLabelText("Opensource_Description");
                litURL.Text = GetLabelText("Opensource_URL");
                txtURL.ToolTip = GetLabelText("Opensource_URL");
                litHits.Text = GetLabelText("Opensource_Hits");
                txtHits.ToolTip = GetLabelText("Opensource_Hits");
                litIsDisplay.Text = GetLabelText("Opensource_IsDisplay");
                rdbDisplay0.Text = GetLabelText("Common_Yes");
                rdbDisplay1.Text = GetLabelText("Common_No");
                litRdbDisplayTip.Text = GetLabelText("Opensource_IsDisplay");
                litCreatedTime.Text = GetLabelText("Common_CreatedTime");
                litCreatedByName.Text = GetLabelText("Common_CreatedByName");
                litUpdatedTime.Text = GetLabelText("Common_UpdatedTime");
                litUpdatedByName.Text = GetLabelText("Common_UpdatedByName");

                if (Request.QueryString["action"] == "modify")
                {
                    //get id
                    int OpenSourceId = Convert.ToInt32(Request.QueryString["id"]);

                    Johnny.CMS.BLL.SeH.OpenSource bll = new Johnny.CMS.BLL.SeH.OpenSource();
                    Johnny.CMS.OM.SeH.OpenSource model = new Johnny.CMS.OM.SeH.OpenSource();
                    model = bll.GetModel(OpenSourceId);


                    txtOpenSourceName.Text = model.OpenSourceName;
                    txtShortDescription.Text = model.ShortDescription;
                    fckContent.Value = model.Description;
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
                }
                else
                {
                    rdbDisplay0.Checked = true;
                    txtHits.Text = "0";
                }
            }
        }

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            //validation
            if (!CheckInputEmptyAndLength(txtOpenSourceName, "E00901", "E00902", false))
                return;
            if (!CheckInputEmptyAndLength(txtHits, "E00901", "E00902", false))
                return;

            Johnny.CMS.BLL.SeH.OpenSource bll = new Johnny.CMS.BLL.SeH.OpenSource();
            Johnny.CMS.OM.SeH.OpenSource model = new Johnny.CMS.OM.SeH.OpenSource();
            if (Request.QueryString["action"] == "modify")
            {
                //update
                model.OpenSourceId = Convert.ToInt32(Request.QueryString["id"]);
                model.OpenSourceName = txtOpenSourceName.Text;
                model.ShortDescription = txtShortDescription.Text;
                model.Description = fckContent.Value;
                model.URL = txtURL.Text;
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
                model.OpenSourceName = txtOpenSourceName.Text;
                model.ShortDescription = txtShortDescription.Text;
                model.Description = StringHelper.htmlInputText(fckContent.Value);
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
                    txtOpenSourceName.Text = "";
                    txtShortDescription.Text = "";
                    fckContent.Value = "";
                    txtURL.Text = "";
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