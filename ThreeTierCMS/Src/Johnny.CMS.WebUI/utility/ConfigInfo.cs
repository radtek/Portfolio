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
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        //Cultrue
        public static string GlobalizationCulture = ConfigurationManager.AppSettings["GlobalizationCulture"];
        //ϵͳ��Ϣ
        public static string AdminPageTitle = ConfigurationManager.AppSettings["AdminPageTitle"];
        public static string ForeTitle = ConfigurationManager.AppSettings["ForeTitle"];
        public static string ErosTitle = ConfigurationManager.AppSettings["ErosTitle"];
        //���ݿ������ַ���
        public static string DbConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        //DataGridÿҳ��ʾ��������
        public static int PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
        //�հ�ͼƬ
        public static string DefaultImg = ConfigurationManager.AppSettings["DefaultImg"];
        //���ݷָ���
        public static string PagerSeparator = ConfigurationManager.AppSettings["PagerSeparator"];
        //ҳ���������תҳ��
        public static string ErrorRedirectPage = ConfigurationManager.AppSettings["ErrorRedirectPage"];
        //�ļ��ϴ�����Ĵ�С ��λ (K) byte 
        public static int UploadFileSize = Convert.ToInt32(ConfigurationManager.AppSettings["UploadFileSize"]) * 1024;
        //����Ĭ���û�
        public static bool AllowDefaultCreator = Convert.ToBoolean(ConfigurationManager.AppSettings["AllowDefaultCreator"]);
        //��������ʱ��Ĭ��ֵ
        public static string DefaultPassword = ConfigurationManager.AppSettings["DefaultPassword"];

    }
}
