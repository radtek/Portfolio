using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;

using System.Configuration;

namespace Johnny.Controls.Web
{
    public class WebControlLocalization
    {
        private static ResourceManager rm;
        public static string GetText(string key)
        {
            if (rm == null) 
            {
                string cultureString = ConfigurationManager.AppSettings["GlobalizationCulture"];
                if (cultureString == "zh-cn")
                    rm = new ResourceManager("Johnny.Controls.Web.Assembly_Resx.ControlTexts_zh", typeof(Assembly_Resx.ControlTexts_zh).Assembly);
                else if (cultureString == "en-us")
                    rm = new ResourceManager("Johnny.Controls.Web.Assembly_Resx.ControlTexts", typeof(Assembly_Resx.ControlTexts).Assembly);
                else
                    rm = new ResourceManager("Johnny.Controls.Web.Assembly_Resx.ControlTexts", typeof(Assembly_Resx.ControlTexts).Assembly);
            }

            return rm.GetString(key);
        }
    }
}
