using System;
using System.Web.UI.WebControls;

using Johnny.CMS.OM;
using Johnny.CMS.BLL;
using Johnny.Library.Helper;
using Johnny.Component.Utility;

namespace Johnny.CMS.admin.seh
{
    public partial class bestpracticeadd : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                litPageTitle.Text = GetLabelText("BestPractice_Title");
                litBestPracticeName.Text = GetLabelText("Bestpractice_BestPracticeName");
                txtBestPracticeName.ToolTip = GetLabelText("Bestpractice_BestPracticeName");
                litShortDescription.Text = GetLabelText("Bestpractice_ShortDescription");
                txtShortDescription.ToolTip = GetLabelText("Bestpractice_ShortDescription");
                litContent.Text = GetLabelText("Bestpractice_Description");
                litHits.Text = GetLabelText("Bestpractice_Hits");
                txtHits.ToolTip = GetLabelText("Bestpractice_Hits");
                litIsDisplay.Text = GetLabelText("Bestpractice_IsDisplay");
                rdbDisplay0.Text = GetLabelText("Common_Yes");
                rdbDisplay1.Text = GetLabelText("Common_No");
                litRdbDisplayTip.Text = GetLabelText("Bestpractice_IsDisplay");
                litCreatedTime.Text = GetLabelText("Common_CreatedTime");
                litCreatedByName.Text = GetLabelText("Common_CreatedByName");
                litUpdatedTime.Text = GetLabelText("Common_UpdatedTime");
                litUpdatedByName.Text = GetLabelText("Common_UpdatedByName");

                if (Request.QueryString["action"] == "modify")
                {
                    //get id
                    int BestPracticeId = Convert.ToInt32(Request.QueryString["id"]);

                    Johnny.CMS.BLL.SeH.BestPractice bll = new Johnny.CMS.BLL.SeH.BestPractice();
                    Johnny.CMS.OM.SeH.BestPractice model = new Johnny.CMS.OM.SeH.BestPractice();
                    model = bll.GetModel(BestPracticeId);


                    txtBestPracticeName.Text = model.BestPracticeName;
                    txtShortDescription.Text = model.ShortDescription;
                    fckContent.Value = model.Description;
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
            if (!CheckInputEmptyAndLength(txtBestPracticeName, "E00901", "E00902", false))
                return;
            if (!CheckInputEmptyAndLength(txtHits, "E00901", "E00902", false))
                return;

            Johnny.CMS.BLL.SeH.BestPractice bll = new Johnny.CMS.BLL.SeH.BestPractice();
            Johnny.CMS.OM.SeH.BestPractice model = new Johnny.CMS.OM.SeH.BestPractice();
            if (Request.QueryString["action"] == "modify")
            {
                //update
                model.BestPracticeId = Convert.ToInt32(Request.QueryString["id"]);
                model.BestPracticeName = txtBestPracticeName.Text;
                model.ShortDescription = txtShortDescription.Text;
                model.Description = fckContent.Value;
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
                model.BestPracticeName = txtBestPracticeName.Text;
                model.ShortDescription = txtShortDescription.Text;
                model.Description = StringHelper.htmlInputText(fckContent.Value);
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
                    txtBestPracticeName.Text = "";
                    txtShortDescription.Text = "";
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