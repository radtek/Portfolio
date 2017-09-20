using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

using Johnny.Component.Utility;
using Johnny.CMS.BLL;
using Johnny.Library.Helper;

namespace Johnny.CMS.admin
{
    public abstract class AdminAuth : AdminBase
    {
        //protected const string CONST_BUTTONTEXT_ADD = " 添 加 ";
        //protected const string CONST_BUTTONTEXT_SAVE = " 保 存 ";
        private string strAdminPageTitle = Johnny.CMS.WebUI.utility.ConfigInfo.AdminPageTitle;
        //protected SouEi.CommonBase.RegularMatch rm = new SouEi.CommonBase.RegularMatch();

        protected virtual void Page_Load(object sender, EventArgs e)
        {            /*
            //Check role
            if (Session["UserName"] == null || String.IsNullOrEmpty(Session["UserName"].ToString()))
            {
                Application["error"] = "<font color=#ff0000 style='font-size: 12px'>访问被拒绝！请先登录！</font><br>";
                Server.Transfer("~/admin/errorpage.aspx");
                return;
            }

            if (Request.UrlReferrer == null)
            {
                Application["error"] = "<font color=#ff0000 style='font-size: 12px'>请从页面正常点击访问！</font><br>";
                Server.Transfer("~/admin/errorpage.aspx");
                return;
            }

            string currentPage = GetCurrentPageName();
            if (currentPage != "index.aspx")
            {
                if (!CheckPermission(currentPage))
                {
                    Application["error"] = "<font color=#ff0000 style='font-size: 12px'>您没有足够的权限访问当前页面！请联系管理员。</font><br>";
                    Server.Transfer("~/admin/errorpage.aspx");
                    return;
                }
            }*/
            if (!this.IsPostBack)
            {
                Johnny.CMS.Common.SysParameter sp = new Johnny.CMS.Common.SysParameter();
                strAdminPageTitle = sp.WebSettings.WebsiteName;
            }
        }

        private bool CheckPermission(string currentPage)
        {
            //用户权限
            Johnny.CMS.BLL.Access.Accounts accounts = new Johnny.CMS.BLL.Access.Accounts();
            ArrayList arrPermission = new ArrayList();
            arrPermission = accounts.GetUserPermission(Session["UserName"].ToString());

            //页面访问权限
            Johnny.CMS.BLL.SystemInfo.Menu menu = new Johnny.CMS.BLL.SystemInfo.Menu();
            int permission = menu.GetPermissionByPageLink(currentPage);

            return arrPermission.Contains(permission);

        }
        private string GetCurrentPageName()
        {
            //int length = HttpContext.Current.Request.Url.Segments.Length;
            //return HttpContext.Current.Request.Url.Segments[length - 1];
            return HttpContext.Current.Request.Path.Replace("/admin/", "");
        }

        protected void SetMessage(string message)
        {
            Label lblStatus = (Label)this.Master.FindControl("lblStatus");
            lblStatus.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "&nbsp;&nbsp;&nbsp;" + message; 
        }

        protected bool CheckInputEmptyAndLength(TextBox txtName, string EmptyErrorCode, string ExceedErrorCode)
        {
            return CheckInputEmptyAndLength(txtName, EmptyErrorCode, ExceedErrorCode, true);
        }

        protected bool CheckInputEmptyAndLength(TextBox txtName, string EmptyErrorCode, string ExceedErrorCode, bool DoubleChar)
        {
            if (String.IsNullOrEmpty(txtName.Text))
            {
                SetMessage(GetMessage(EmptyErrorCode, txtName.MaxLength.ToString()));
                txtName.Focus();
                return false;
            }

            //VarChar
            if (DoubleChar == true && StringHelper.GetLengthByByte(txtName.Text) > txtName.MaxLength)
            {
                SetMessage(GetMessage(ExceedErrorCode, txtName.MaxLength.ToString()));
                txtName.Focus();
                return false;
            }

            //NVarChar
            if (DoubleChar == false && StringHelper.GetLength(txtName.Text) > txtName.MaxLength)
            {
                SetMessage(GetMessage(ExceedErrorCode, txtName.MaxLength.ToString()));
                txtName.Focus();
                return false;
            }

            return true;
        }

        protected bool CheckInputLength(TextBox txtName, string ExceedErrorCode)
        {
            return CheckInputLength(txtName, ExceedErrorCode, true);
        }

        protected bool CheckInputLength(TextBox txtName, string ExceedErrorCode, bool DoubleChar)
        {
            //VarChar
            if (DoubleChar == true && StringHelper.GetLengthByByte(txtName.Text) > txtName.MaxLength)
            {
                SetMessage(GetMessage(ExceedErrorCode, txtName.MaxLength.ToString()));
                txtName.Focus();
                return false;
            }

            //NVarChar
            if (DoubleChar == false && StringHelper.GetLength(txtName.Text) > txtName.MaxLength)
            {
                SetMessage(GetMessage(ExceedErrorCode, txtName.MaxLength.ToString()));
                txtName.Focus();
                return false;
            }

            return true;
        }

        #region Properties
        protected string AdminPageTitle
        {
            get {return strAdminPageTitle;}
            set {strAdminPageTitle = value;}
        }
        #endregion
        
    }
}
