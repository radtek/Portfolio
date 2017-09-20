using System;
using System.Collections.Generic;
using System.Web;

using Johnny.Library.Helper;
using Johnny.Component.Globalization;

namespace Johnny.CMS.admin
{
    public class AdminBase : System.Web.UI.Page
    {
        protected override void InitializeCulture()
        {
            if (Session["GlobalizationCulture"] == null)
            {
                Session["GlobalizationCulture"] = Johnny.CMS.WebUI.utility.ConfigInfo.GlobalizationCulture;
            }

            string cultureString = DataConvert.GetString(Session["GlobalizationCulture"]);
            GlobalizationUtility.GlobalizationCulture = cultureString;
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(cultureString);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cultureString);
        }

        protected string GetLabelText(string key)
        {
            return GlobalizationUtility.GetLabelText(key);
        }

        protected string GetMessage(string msgId)
        {
            return GlobalizationUtility.GetMessage(msgId);
        }
        protected string GetMessage(string msgId, string param)
        {
            return GlobalizationUtility.GetMessage(msgId, param);
        }
    }
}