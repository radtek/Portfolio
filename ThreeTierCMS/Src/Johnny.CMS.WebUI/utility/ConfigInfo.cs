using System;
using System.Collections.Generic;
using System.Text;

using System.Configuration;

namespace Johnny.CMS.WebUI.utility
{
    public class ConfigInfo
    {
        public ConfigInfo()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        //Cultrue
        public static string GlobalizationCulture = ConfigurationManager.AppSettings["GlobalizationCulture"];
        //系统信息
        public static string AdminPageTitle = ConfigurationManager.AppSettings["AdminPageTitle"];
        public static string ForeTitle = ConfigurationManager.AppSettings["ForeTitle"];
        public static string ErosTitle = ConfigurationManager.AppSettings["ErosTitle"];
        //数据库连接字符串
        public static string DbConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        //DataGrid每页显示数据条数
        public static int PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
        //空白图片
        public static string DefaultImg = ConfigurationManager.AppSettings["DefaultImg"];
        //内容分隔串
        public static string PagerSeparator = ConfigurationManager.AppSettings["PagerSeparator"];
        //页面出错后的跳转页面
        public static string ErrorRedirectPage = ConfigurationManager.AppSettings["ErrorRedirectPage"];
        //文件上传允许的大小 单位 (K) byte 
        public static int UploadFileSize = Convert.ToInt32(ConfigurationManager.AppSettings["UploadFileSize"]) * 1024;
        //允许默认用户
        public static bool AllowDefaultCreator = Convert.ToBoolean(ConfigurationManager.AppSettings["AllowDefaultCreator"]);
        //重置密码时的默认值
        public static string DefaultPassword = ConfigurationManager.AppSettings["DefaultPassword"];

    }
}
