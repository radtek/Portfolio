using System;
using System.Web.UI.WebControls;

using Johnny.Component.Utility;
using Johnny.Library.Helper;

namespace Johnny.CMS.admin.access
{
    public partial class profile : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                litPageTitle.Text = GetLabelText("Profile_Title");
                litAdminName.Text = GetLabelText("Administrator_AdminName");
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
                litValidTo.Text = GetLabelText("Administrator_ValidTo");
                litActivated.Text = GetLabelText("Administrator_IsActivated");
                rdbActivated0.Text = GetLabelText("Common_Yes");
                rdbActivated1.Text = GetLabelText("Common_No");
                litRdbActivatedTip.Text = GetLabelText("Administrator_IsActivated");
                litLoginTimes.Text = GetLabelText("Administrator_LoginTimes");
                litCreatedTime.Text = GetLabelText("Common_CreatedTime");
                litCreatedByName.Text = GetLabelText("Common_CreatedByName");
                litUpdatedTime.Text = GetLabelText("Common_UpdatedTime");
                litUpdatedByName.Text = GetLabelText("Common_UpdatedByName");

                //get UserId
                int UserId = DataConvert.GetInt32(Session["UserId"]);

                Johnny.CMS.BLL.Access.Administrator bll = new Johnny.CMS.BLL.Access.Administrator();
                Johnny.CMS.OM.Access.Administrator model = new Johnny.CMS.OM.Access.Administrator();
                model = bll.GetModel(UserId);

                lblAdminName.Text = model.AdminName;
                txtFullName.Text = model.FullName;
                if (model.Gender)
                    rdbGender0.Checked = true;
                else
                    rdbGender1.Checked = true;
                txtTel.Text = model.Tel;
                txtEmail.Text = model.Email;
                lblValidFrom.Text = DataConvert.GetShortDateString(model.ValidFrom);
                lblValidTo.Text = DataConvert.GetShortDateString(model.ValidTo);
                if (model.IsActivated)
                    rdbActivated0.Checked = true;
                else
                    rdbActivated1.Checked = true;
                rdbActivated0.Disabled = true;
                rdbActivated1.Disabled = true;
                lblLoginTimes.Text = DataConvert.GetString(model.LoginTimes);
                lblCreatedTime.Text = DataConvert.GetLongDateString(model.CreatedTime);
                lblCreatedByName.Text = model.CreatedByName;
                lblUpdatedTime.Text = DataConvert.GetLongDateString(model.UpdatedTime);
                lblUpdatedByName.Text = model.UpdatedByName;

                btnAdd.ButtonType = Johnny.Controls.Web.Button.Button.EnumButtonType.Save;
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

            //check full name
            if (!CheckInputLength(txtFullName, "E00104", false))
                return;
            //check tel
            if (!CheckInputLength(txtTel, "E00106"))
                return;
            //check email
            if (!CheckInputLength(txtEmail, "E00106"))
                return;

            Johnny.CMS.BLL.Access.Administrator bll = new Johnny.CMS.BLL.Access.Administrator();
            Johnny.CMS.OM.Access.Administrator model = new Johnny.CMS.OM.Access.Administrator();

            //update
            model.AdminId = DataConvert.GetInt32(Session["UserId"]);
            model.FullName = txtFullName.Text;
            model.Gender = rdbGender0.Checked;
            model.Tel = txtTel.Text;
            model.Email = txtEmail.Text;
            //model.BeginTime = DataConvert.GetDateTime(lblBeginTime.Text);
            //model.EndTime = DataConvert.GetDateTime(lblEndTime.Text);
            //model.IsActivated = rdbActivated0.Checked;
            model.UpdatedTime = System.DateTime.Now;
            model.UpdatedById = DataConvert.GetInt32(Session["UserId"]);
            model.UpdatedByName = DataConvert.GetString(Session["UserName"]);

            bll.UpdatePersonal(model);
            SetMessage(GetMessage("C00003"));          
        }
    }
}