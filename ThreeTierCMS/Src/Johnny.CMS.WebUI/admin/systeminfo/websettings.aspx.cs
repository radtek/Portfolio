using System;
using System.Web.UI.WebControls;

using Johnny.Component.Utility;
using Johnny.Library.Helper;

namespace Johnny.CMS.admin.systeminfo
{
    public partial class websettings : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                Johnny.CMS.BLL.SystemInfo.WebSettings bll = new Johnny.CMS.BLL.SystemInfo.WebSettings();
                Johnny.CMS.OM.SystemInfo.WebSettings model = new Johnny.CMS.OM.SystemInfo.WebSettings();
                model = bll.GetModel();

                txtWebsiteName.Text = model.WebsiteName;
                txtWebsiteTitle.Text = model.WebsiteTitle;
                txtShortDescription.Text = model.ShortDescription;
                txtTel.Text = model.Tel;
                txtFax.Text = model.Fax;
                txtEmail.Text = model.Email;
                txtWebsiteAddress.Text = model.WebsiteAddress;
                txtWebsitePath.Text = model.WebsitePath;
                txtFileSize.Text = DataConvert.GetString(model.FileSize);
                txtLogoPath.Text = model.LogoPath;
                txtBannerPath.Text = model.BannerPath;
                txtCopyright.Text = model.Copyright;
                txtMetaKeywords.Text = model.MetaKeywords;
                txtMetaDescription.Text = model.MetaDescription;
                if (model.IsClosed)
                    rdbIsClosed1.Checked = true;
                else
                    rdbIsClosed0.Checked = true;
                txtClosedInfo.Text = model.ClosedInfo;
                txtUserAgreement.Text = model.UserAgreement;
                if (model.LoginType)
                    rdbLoginType0.Checked = true;
                else
                    rdbLoginType1.Checked = true;
                btnAdd.ButtonType = Johnny.Controls.Web.Button.Button.EnumButtonType.Save;
            }
        }       

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            SetMessage("");

            //validation
            if (!CheckInputEmptyAndLength(txtWebsiteName, "E00101", "E00102", false))
                return;
            if (!CheckInputEmptyAndLength(txtWebsiteTitle, "E00101", "E00102", false))
                return;
            if (!CheckInputLength(txtTel, "E00104"))
                return;
            if (!CheckInputLength(txtFax, "E00106"))
                return;
            if (!CheckInputLength(txtEmail, "E00106"))
                return;
            if (!CheckInputEmptyAndLength(txtWebsiteAddress, "E00101", "E00102"))
                return;
            if (!CheckInputEmptyAndLength(txtWebsitePath, "E00101", "E00102"))
                return;
            if (!CheckInputEmptyAndLength(txtFileSize, "E00101", "E00102"))
                return;
            if (!DataValidation.IsNaturalNumberRange(txtFileSize.Text, 0, 5120))
                return;
            if (!CheckInputEmptyAndLength(txtLogoPath, "E00101", "E00102"))
                return;
            if (!CheckInputEmptyAndLength(txtBannerPath, "E00101", "E00102"))
                return;
            if (!CheckInputLength(txtCopyright, "E00106"))
                return;
            if (!CheckInputLength(txtMetaKeywords, "E00106"))
                return;
            if (!CheckInputLength(txtMetaDescription, "E00106"))
                return;
            if (!CheckInputLength(txtClosedInfo, "E00106"))
                return;

            Johnny.CMS.BLL.SystemInfo.WebSettings bll = new Johnny.CMS.BLL.SystemInfo.WebSettings();
            Johnny.CMS.OM.SystemInfo.WebSettings model = new Johnny.CMS.OM.SystemInfo.WebSettings();

            model.WebsiteName = txtWebsiteName.Text;
            model.WebsiteTitle = txtWebsiteTitle.Text;
            model.ShortDescription = txtShortDescription.Text;
            model.Tel = txtTel.Text;
            model.Fax = txtFax.Text;
            model.Email = txtEmail.Text;
            model.WebsiteAddress = txtWebsiteAddress.Text;
            model.WebsitePath = txtWebsitePath.Text;
            model.FileSize = DataConvert.GetInt32(txtFileSize.Text);
            model.LogoPath = txtLogoPath.Text;
            model.BannerPath = txtBannerPath.Text;
            model.Copyright = txtCopyright.Text;
            model.MetaKeywords = txtMetaKeywords.Text;
            model.MetaDescription = txtMetaDescription.Text;
            model.IsClosed = rdbIsClosed1.Checked;
            model.ClosedInfo = txtClosedInfo.Text;
            model.UserAgreement = txtUserAgreement.Text;
            model.LoginType = rdbLoginType0.Checked;

            bll.AddOrUpdate(model);
            SetMessage(GetMessage("C00003"));
        }
    }
}