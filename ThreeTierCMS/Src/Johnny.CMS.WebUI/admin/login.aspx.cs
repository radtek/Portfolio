using System;
using System.Web.Security;
using System.Web.UI.WebControls;

using Johnny.Component.Utility;
using Johnny.Library.Helper;
using Johnny.CMS.BLL;

namespace Johnny.CMS.admin
{
    public partial class login : AdminBase
    {
        protected string strPageTitle;

        protected void Page_Load(object sender, EventArgs e)
        {
            //clear cache
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";

            //clear session
            Session["UserName"] = null;

            strPageTitle = GetLabelText("Login_Title");
            lblAdminName.Text = GetLabelText("Login_AdminName");
            VAdminName.ErrorMessage = GetLabelText("Login_VUserName_ErrorMessage");
            lblPassword.Text = GetLabelText("Login_Password");
            VPassword.ErrorMessage = GetLabelText("Login_VPassword_ErrorMessage");
            lblVerifyCode.Text = GetLabelText("Login_VerifyCode");
            lblCopyRight.Text = GetLabelText("Login_CopyRight");
        }       

        protected void imgbtnLogin_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            this.lblStatus.Text = "";

            string strAdminname = StringHelper.htmlInputText(this.txtAdminName.Text);
            string strAdminPW = StringHelper.htmlInputText(this.txtPassword.Text);

            if (strAdminname == String.Empty || strAdminPW == String.Empty)
            {
                this.lblStatus.Text = GetMessage("F00101");                 
                return;
            }

            if (Session["CheckCode"] == null)
            {
                Response.Redirect("login.aspx");
                return;
            }

            //check validation code
            if (Session["CheckCode"].ToString() != txtVerifyCode.Text.Trim())
            {
                this.lblStatus.Text = GetMessage("F00102");
                this.txtVerifyCode.Text = "";
                this.txtVerifyCode.Focus();
                return;
            }

            Johnny.CMS.BLL.Access.Administrator admin = new Johnny.CMS.BLL.Access.Administrator();
            strAdminPW = FormsAuthentication.HashPasswordForStoringInConfigFile(strAdminPW, "MD5");
            if (admin.CheckLogin(strAdminname, strAdminPW))
            {
                //update login times and login time
                admin.UpdateLoginTimes(strAdminname);
                Session["UserId"] = admin.GetUserIdByName(strAdminname);
                Session["UserName"] = strAdminname;
                Session["IsFirstAccess"] = "true";
                Response.Redirect("index.aspx");
            }
            else
            {
                this.txtVerifyCode.Text = "";
                this.lblStatus.Text = GetMessage("F00103");
            }
        }

        #region Properties
        protected string LoginPageTitle
        {
            get { return strPageTitle; }
        }
        #endregion
    }
}
