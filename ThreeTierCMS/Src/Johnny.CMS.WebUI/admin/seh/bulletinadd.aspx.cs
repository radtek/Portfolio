using System;
using System.Web.UI.WebControls;

using Johnny.CMS.OM;
using Johnny.CMS.BLL;
using Johnny.Library.Helper;
using Johnny.Component.Utility;

namespace Johnny.CMS.admin.seh
{
    public partial class bulletinadd : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                litPageTitle.Text = GetLabelText("Bulletin_Title");
                litTitle.Text = GetLabelText("Bulletin_Title");
                txtTitle.ToolTip = GetLabelText("Bulletin_Title");
                litContent.Text = GetLabelText("Bulletin_Content");
                litURL.Text = GetLabelText("Bulletin_URL");
                txtURL.ToolTip = GetLabelText("Bulletin_URL");
                litHits.Text = GetLabelText("Bulletin_Hits");
                txtHits.ToolTip = GetLabelText("Bulletin_Hits");
                litIsDisplay.Text = GetLabelText("Bulletin_IsDisplay");
                rdbDisplay0.Text = GetLabelText("Common_Yes");
                rdbDisplay1.Text = GetLabelText("Common_No");
                litRdbDisplayTip.Text = GetLabelText("Bulletin_IsDisplay");
                litCreatedTime.Text = GetLabelText("Common_CreatedTime");
                litCreatedByName.Text = GetLabelText("Common_CreatedByName");
                litUpdatedTime.Text = GetLabelText("Common_UpdatedTime");
                litUpdatedByName.Text = GetLabelText("Common_UpdatedByName");

                if (Request.QueryString["action"] == "modify")
                {
                    //get id
                    int BulletinId = Convert.ToInt32(Request.QueryString["id"]);

                    Johnny.CMS.BLL.SeH.Bulletin bll = new Johnny.CMS.BLL.SeH.Bulletin();
                    Johnny.CMS.OM.SeH.Bulletin model = new Johnny.CMS.OM.SeH.Bulletin();
                    model = bll.GetModel(BulletinId);

                    txtTitle.Text = model.Title;
                    fckContent.Value = StringHelper.htmlOutputText(model.Content);
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
                    rdbDisplay0.Checked = true;
                    txtHits.Text = "0";
                }
            }
        }

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            //validation
            if (!CheckInputEmptyAndLength(txtTitle, "E00901", "E00902", false))
                return;
            if (!CheckInputEmptyAndLength(txtHits, "E00901", "E00902", false))
                return;

            Johnny.CMS.BLL.SeH.Bulletin bll = new Johnny.CMS.BLL.SeH.Bulletin();
            Johnny.CMS.OM.SeH.Bulletin model = new Johnny.CMS.OM.SeH.Bulletin();
            if (Request.QueryString["action"] == "modify")
            {
                //update
                model.BulletinId = Convert.ToInt32(Request.QueryString["id"]);
                model.Title = txtTitle.Text;
                model.Content = StringHelper.htmlInputText(fckContent.Value);
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
                model.Title = txtTitle.Text;
                model.Content = StringHelper.htmlInputText(fckContent.Value);
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
                    txtTitle.Text = "";
                    fckContent.Value = "";
                    txtURL.Text = "";
                    txtHits.Text = "";
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