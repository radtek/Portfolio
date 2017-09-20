using System;
using System.Web.UI.WebControls;

using Johnny.Component.Utility;
using Johnny.Library.Helper;

namespace Johnny.CMS.admin.systeminfo
{
    public partial class mailsettings : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                Johnny.CMS.BLL.SystemInfo.MailSettings bll = new Johnny.CMS.BLL.SystemInfo.MailSettings();
                Johnny.CMS.OM.SystemInfo.MailSettings model = new Johnny.CMS.OM.SystemInfo.MailSettings();
                model = bll.GetModel(1);

                txtSmtpServerIP.Text = model.SmtpServerIP;
                txtSmtpServerPort.Text = DataConvert.GetString(model.SmtpServerPort);
                txtMailId.Text = model.MailId;
                txtMailPassword.Text = model.MailPassword;
                btnAdd.ButtonType = Johnny.Controls.Web.Button.Button.EnumButtonType.Save;
            }
        }       

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            SetMessage("");

            //validation
            if (!CheckInputEmptyAndLength(txtSmtpServerIP, "E00101", "E00102"))
                return;
            if (!CheckInputEmptyAndLength(txtSmtpServerPort, "E00101", "E00102"))
                return;
            if (!CheckInputEmptyAndLength(txtMailId, "E00101", "E00102"))
                return;
            if (!CheckInputEmptyAndLength(txtMailPassword, "E00101", "E00102"))
                return;

            Johnny.CMS.BLL.SystemInfo.MailSettings bll = new Johnny.CMS.BLL.SystemInfo.MailSettings();
            Johnny.CMS.OM.SystemInfo.MailSettings model = new Johnny.CMS.OM.SystemInfo.MailSettings();

            model.SmtpServerIP = txtSmtpServerIP.Text;
            model.SmtpServerPort = DataConvert.GetInt32(txtSmtpServerPort.Text);
            model.MailId = txtMailId.Text;
            model.MailPassword = txtMailPassword.Text;

            bll.AddOrUpdate(model);
            SetMessage(GetMessage("C00003"));
        }
    }
}