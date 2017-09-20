using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.CMS.Common
{
    public class SysParameter
    {
        //系统设置参数
        private Johnny.CMS.OM.SystemInfo.WebSettings _websettings;

        public SysParameter()
        {
            int intCache = 30;//缓存时间
            string cc_WebSettings = "WebSettings";

            object objWebSetting = Johnny.Library.Helper.CacheHelper.GetCache(cc_WebSettings);
            Johnny.CMS.OM.SystemInfo.WebSettings wsmodel = new Johnny.CMS.OM.SystemInfo.WebSettings();
            if (objWebSetting != null)
            {
                wsmodel = (Johnny.CMS.OM.SystemInfo.WebSettings)objWebSetting;
            }
            else
            {
                Johnny.CMS.BLL.SystemInfo.WebSettings wsbll = new Johnny.CMS.BLL.SystemInfo.WebSettings();
                wsmodel = wsbll.GetModel();
                Johnny.Library.Helper.CacheHelper.SetCache(cc_WebSettings, wsmodel, DateTime.Now.AddMinutes(intCache), TimeSpan.Zero);
            }
            _websettings = wsmodel;
        }

        public Johnny.CMS.OM.SystemInfo.WebSettings WebSettings
        {
            get { return _websettings; }
            set { _websettings = value; }
        }
    }
}
