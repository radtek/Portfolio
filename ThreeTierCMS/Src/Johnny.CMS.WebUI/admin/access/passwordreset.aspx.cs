using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Johnny.Library.Helper;
using Johnny.Component.Utility;

namespace Johnny.CMS.admin.access
{
    public partial class passwordreset : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!IsPostBack)
            {
                litPageTitle.Text = GetLabelText("ResetPassword_Title");
                litOriginalPassword.Text = GetLabelText("ResetPassword_Original");
                txtOriginalPassword.ToolTip = GetLabelText("ResetPassword_Original");
                litNewPassword.Text = GetLabelText("ResetPassword_New");
                txtNewPassword.ToolTip = GetLabelText("ResetPassword_New");
                litConfirmedPassword.Text = GetLabelText("ResetPassword_Confirm");
                txtConfirmedPassword.ToolTip = GetLabelText("ResetPassword_Confirm");
                //RFVldtOldPassword.ErrorMessage = GetMessage("E00113");
                //RFVldtNewPassword.ErrorMessage = GetMessage("E00114");
                //RFVldtConfirmPassword.ErrorMessage = GetMessage("E00115");
                //CVldtPwd.ErrorMessage = GetMessage("E00116");
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //check input
            if (DataValidation.IsEmpty(txtOriginalPassword.Text))
            {
                SetMessage(GetMessage("E00113"));
                txtOriginalPassword.Focus();
                return;
            }
            if (DataValidation.IsEmpty(txtNewPassword.Text))
            {
                SetMessage(GetMessage("E00114"));
                txtNewPassword.Focus();
                return;
            }
            if (DataValidation.IsEmpty(txtConfirmedPassword.Text))
            {
                SetMessage(GetMessage("E00115"));
                txtConfirmedPassword.Focus();
                return;
            }

            if (!DataValidation.IsEqual(txtNewPassword.Text, txtConfirmedPassword.Text))
            {
                SetMessage(GetMessage("E00116"));
                txtNewPassword.Focus();
                return;
            }

            Johnny.CMS.BLL.Access.Administrator admin = new Johnny.CMS.BLL.Access.Administrator();
            string strPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(txtOriginalPassword.Text, "MD5");

            //check old password
            if (!admin.CheckLogin(DataConvert.GetString(Session["UserName"]), strPwd))
            {
                SetMessage(GetMessage("E00117"));
                txtOriginalPassword.Focus();
                return;
            }

            Johnny.CMS.OM.Access.Administrator model = new Johnny.CMS.OM.Access.Administrator();
            model.AdminId = DataConvert.GetInt32(Session["UserId"]);
            model.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtNewPassword.Text, "MD5");
            model.UpdatedTime = System.DateTime.Now;
            model.UpdatedById = DataConvert.GetInt32(Session["UserId"]);
            model.UpdatedByName = DataConvert.GetString(Session["UserName"]);
            admin.ResetPassword(model);
            SetMessage(GetMessage("E00118"));
        }
    }
}
