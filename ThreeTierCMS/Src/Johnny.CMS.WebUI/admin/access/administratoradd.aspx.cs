using System;
using System.Web.UI.WebControls;
using System.Web.Security;

using Johnny.Component.Utility;
using Johnny.Library.Helper;

namespace Johnny.CMS.admin.access
{
    public partial class administratoradd : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);            

            if (!this.IsPostBack)
            {
                litPageTitle.Text = GetLabelText("Administrator_Title");
                litAdminName.Text = GetLabelText("Administrator_AdminName");
                txtAdminName.ToolTip = GetLabelText("Administrator_AdminName");
                litFullName.Text = GetLabelText("Administrator_FullName");
                txtFullName.ToolTip = GetLabelText("Administrator_FullName");
                litGender.Text = GetLabelText("Administrator_Gender");
                rdbGender0.Text = GetLabelText("Common_Male");
                rdbGender1.Text = GetLabelText("Common_Female");
                litRdbTip.Text = GetLabelText("Administrator_Gender");
                litTel.Text = GetLabelText("Administrator_Tel");
                txtTel.ToolTip = GetLabelText("Administrator_Tel");
                litEmail.Text = GetLabelText("Administrator_Email");
                txtEmail.ToolTip = GetLabelText("Administrator_Email");
                litValidFrom.Text = GetLabelText("Administrator_ValidFrom");
                txtValidFrom.ToolTip = GetLabelText("Administrator_ValidFrom");
                litValidTo.Text = GetLabelText("Administrator_ValidTo");
                txtValidTo.ToolTip = GetLabelText("Administrator_ValidTo");
                litActivated.Text = GetLabelText("Administrator_IsActivated");
                rdbActivated0.Text = GetLabelText("Common_Yes");
                rdbActivated1.Text = GetLabelText("Common_No");
                litRdbActivatedTip.Text = GetLabelText("Administrator_IsActivated");
                litLoginTimes.Text = GetLabelText("Administrator_LoginTimes");
                litCreatedTime.Text = GetLabelText("Common_CreatedTime");
                litCreatedByName.Text = GetLabelText("Common_CreatedByName");
                litUpdatedTime.Text = GetLabelText("Common_UpdatedTime");
                litUpdatedByName.Text = GetLabelText("Common_UpdatedByName");
                
                if (Request.QueryString["action"] == "modify")
                {
                    //get UserId
                    int UserId = Convert.ToInt32(Request.QueryString["id"]);

                    Johnny.CMS.BLL.Access.Administrator bll = new Johnny.CMS.BLL.Access.Administrator();
                    Johnny.CMS.OM.Access.Administrator model = new Johnny.CMS.OM.Access.Administrator();
                    model = bll.GetModel(UserId);

                    txtAdminName.Text = model.AdminName;
                    txtFullName.Text = model.FullName;
                    if (model.Gender)
                        rdbGender0.Checked = true;
                    else
                        rdbGender1.Checked = true;
                    txtTel.Text = model.Tel;
                    txtEmail.Text = model.Email;
                    txtValidFrom.Text = DataConvert.GetShortDateString(model.ValidFrom);
                    txtValidTo.Text = DataConvert.GetShortDateString(model.ValidTo);
                    if (model.IsActivated)
                        rdbActivated0.Checked = true;
                    else
                        rdbActivated1.Checked = true;
                    lblLoginTimes.Text = DataConvert.GetString(model.LoginTimes);
                    lblCreatedTime.Text = DataConvert.GetLongDateString(model.CreatedTime);
                    lblCreatedByName.Text = model.CreatedByName;
                    lblUpdatedTime.Text = DataConvert.GetLongDateString(model.UpdatedTime);
                    lblUpdatedByName.Text = model.UpdatedByName;

                    btnAdd.ButtonType = Johnny.Controls.Web.Button.Button.EnumButtonType.Save;
                    //btnAdd.Text = CONST_BUTTONTEXT_SAVE;
                }
                else
                {
                    rdbGender0.Checked = true;
                    rdbActivated0.Checked = true;
                }
            }
        }

        private void CreateddlCity()
        {
            //BLLCity city = new BLLCity();
            //ddlCity.DataSource = city.GetList();
            //ddlCity.DataTextField = "CityName";
            //ddlCity.DataValueField = "CityId";
            //ddlCity.DataBind();
        }       

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            SetMessage("");

            //check admin name
            if (!CheckInputEmptyAndLength(txtAdminName, "E00101", "E00102"))
                return;
            //check full name
            if (!CheckInputLength(txtFullName, "E00104", false))
                return;
            //check tel
            if (!CheckInputLength(txtTel, "E00106"))
                return;
            //check email
            if (!CheckInputLength(txtEmail, "E00106"))
                return;
            if (!DataValidation.IsEmail(txtEmail.Text))
            {
                SetMessage(GetMessage("C00010"));
                txtEmail.Focus();
                return;
            }
            ////check date
            if (!DataValidation.IsDate(txtValidFrom.Text))
            {
                SetMessage(GetMessage("C00009"));
                txtValidFrom.Focus();
                return;
            }

            if (!DataValidation.IsDate(txtValidTo.Text))
            {
                SetMessage(GetMessage("C00009"));
                txtValidTo.Focus();
                return;
            }

            Johnny.CMS.BLL.Access.Administrator bll = new Johnny.CMS.BLL.Access.Administrator();
            Johnny.CMS.OM.Access.Administrator model = new Johnny.CMS.OM.Access.Administrator();

            if (Request.QueryString["action"] == "modify")
            {
                //update
                model.AdminId = Convert.ToInt32(Request.QueryString["id"]);
                model.AdminName = txtAdminName.Text;
                model.FullName = txtFullName.Text;
                model.Gender = rdbGender0.Checked;
                model.Tel = txtTel.Text;
                model.Email = txtEmail.Text;
                model.ValidFrom = DataConvert.GetDateTime(txtValidFrom.Text);
                model.ValidTo = DataConvert.GetDateTime(txtValidTo.Text);
                model.IsActivated = rdbActivated0.Checked;
                model.UpdatedTime = System.DateTime.Now;
                model.UpdatedById = DataConvert.GetInt32(Session["UserId"]);
                model.UpdatedByName = DataConvert.GetString(Session["UserName"]);

                bll.Update(model);
                SetMessage(GetMessage("C00003"));
            }
            else
            {
                //insert
                model.AdminName = txtAdminName.Text;
                model.Password = "123456";
                model.FullName = txtFullName.Text;
                model.Gender = rdbGender0.Checked;
                model.Tel = txtTel.Text;
                model.Email = txtEmail.Text;
                model.ValidFrom = DataConvert.GetDateTime(txtValidFrom.Text);
                model.ValidTo = DataConvert.GetDateTime(txtValidTo.Text);
                model.IsActivated = rdbActivated0.Checked;
                model.LoginTimes = 0;
                model.CreatedTime = System.DateTime.Now;
                model.CreatedById = DataConvert.GetInt32(Session["UserId"]);
                model.CreatedByName = DataConvert.GetString(Session["UserName"]);
                model.UpdatedTime = System.DateTime.Now;
                model.UpdatedById = DataConvert.GetInt32(Session["UserId"]);
                model.UpdatedByName = DataConvert.GetString(Session["UserName"]);

                if (bll.Add(model) > 0)
                {
                    SetMessage(GetMessage("C00001"));
                    txtAdminName.Text = "";
                    txtFullName.Text = "";
                    txtTel.Text = "";
                    txtEmail.Text = "";
                    txtValidFrom.Text = "";
                    txtValidTo.Text = "";
                    lblLoginTimes.Text = "";
                    lblCreatedTime.Text = "";
                    lblCreatedByName.Text = "";
                    lblUpdatedTime.Text = "";
                    lblUpdatedByName.Text = "";
                }
                else
                    SetMessage(GetMessage("C00002"));
            }
        }

        protected void btnResetPassword_Click(object sender, System.EventArgs e)
        {            
            Johnny.CMS.BLL.Access.Administrator bll = new Johnny.CMS.BLL.Access.Administrator();
            Johnny.CMS.OM.Access.Administrator model = new Johnny.CMS.OM.Access.Administrator();
            model.AdminId = Convert.ToInt32(Request.QueryString["id"]);
            model.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(Johnny.CMS.WebUI.utility.ConfigInfo.DefaultPassword, "MD5");
            model.UpdatedTime = System.DateTime.Now;
            model.UpdatedById = DataConvert.GetInt32(Session["UserId"]);
            model.UpdatedByName = DataConvert.GetString(Session["UserName"]);
            bll.ResetPassword(model);
            SetMessage(GetMessage("E00119", Johnny.CMS.WebUI.utility.ConfigInfo.DefaultPassword));
        }
    }
}