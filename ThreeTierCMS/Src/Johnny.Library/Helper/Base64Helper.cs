using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Library.Helper
{
    public class Base64Helper
    {
        //--------------------------------------------------------------------------------
        /// <summary>
        /// ���ַ���ʹ��base64�㷨����
        /// </summary>
        /// <param name="sourceString">�����ܵ��ַ���</param>
        /// <param name="ens">System.Text.Encoding �����紴�����ı��뼯����System.Text.Encoding.GetEncoding(54936)</param>
        /// <returns>�������ı��ַ���</returns>
        public static string EncodingForString(string sourceString, System.Text.Encoding ens)
        {
            return Convert.ToBase64String(ens.GetBytes(sourceString));
        }


        /// <summary>
        /// ���ַ���ʹ��base64�㷨����
        /// </summary>
        /// <param name="sourceString">�����ܵ��ַ���</param>
        /// <returns>�������ı��ַ���</returns>
        public static string EncodingForString(string sourceString)
        {
            return EncodingForString(sourceString, System.Text.Encoding.GetEncoding(54936));
        }


        /// <summary>
        /// ��base64������ַ����л�ԭ�ַ�����֧������
        /// </summary>
        /// <param name="base64String">base64���ܺ���ַ���</param>
        /// <param name="ens">System.Text.Encoding �����紴�����ı��뼯����System.Text.Encoding.GetEncoding(54936)</param>
        /// <returns>��ԭ����ı��ַ���</returns>
        public static string DecodingForString(string base64String, System.Text.Encoding ens)
        {
            /**
            * ***********************************************************
            * 
            * ��base64String��ȡ�õ��ֽ�ֵΪ�ַ��Ļ����루ansi�ַ����룩
            * һ��ģ��û�����ת��Ϊ�����ǹ�ʽ��
            * (char)(��һ�ֽڵĶ�����ֵ*256+�ڶ��ֽ�ֵ)
            * ����c#�е�char��string���ڲ�����unicode���룬�Ͳ��ܰ�������Ĺ�ʽ������
            * ansi���ֽڱ��unicode���벻����
            * ������.net����ṩ�ı�����ʵ�ִ�ansi���뵽unicode�����ת��
            * 
            * GetEncoding ���������ڻ���ƽ̨֧�ִ󲿷ִ���ҳ�����ǣ�������������ṩϵͳ֧�֣�Ĭ�ϱ��룬����ִ�д˷����ļ����������������ָ���ı��룻Little-Endian Unicode (UTF-16LE)��Big-Endian Unicode (UTF-16BE)��Windows ����ϵͳ (windows-1252)��UTF-7��UTF-8��ASCII �Լ� GB18030���������ģ���
            *
            *ָ���±����г�������һ�������Ի�ȡ���ж�Ӧ����ҳ��ϵͳ֧�ֵı��롣
            *
            * ����ҳ ���� 
            * 1200 ��UTF-16LE������utf-16������ucs-2������unicode����ISO-10646-UCS-2�� 
            * 1201 ��UTF-16BE����unicodeFFFE�� 
            * 1252 ��windows-1252�� 
            * 65000 ��utf-7������csUnicode11UTF7������unicode-1-1-utf-7������unicode-2-0-utf-7������x-unicode-1-1-utf-7����x-unicode-2-0-utf-7�� 
            * 65001 ��utf-8������unicode-1-1-utf-8������unicode-2-0-utf-8������x-unicode-1-1-utf-8����x-unicode-2-0-utf-8�� 
            * 20127 ��us-ascii������us������ascii������ANSI_X3.4-1968������ANSI_X3.4-1986������cp367������csASCII������IBM367������iso-ir-6������ISO646-US����ISO_646.irv:1991�� 
            * 54936 ��GB18030�� 
            *
            * ĳЩƽ̨���ܲ�֧���ض��Ĵ���ҳ�����磬Windows 98 �������汾���ܲ�֧������ Shift-jis ����ҳ������ҳ 932������������£�GetEncoding ��������ִ������� C# ����ʱ���� NotSupportedException��
            *
            * Encoding enc = Encoding.GetEncoding("shift-jis"); 
            *
            * **************************************************************/
            //��base64String�еõ�ԭʼ�ַ�
            return ens.GetString(Convert.FromBase64String(base64String));
        }


        /// <summary>
        /// ��base64������ַ����л�ԭ�ַ�����֧������
        /// </summary>
        /// <param name="base64String">base64���ܺ���ַ���</param>
        /// <returns>��ԭ����ı��ַ���</returns>
        public static string DecodingForString(string base64String)
        {
            return DecodingForString(base64String, System.Text.Encoding.GetEncoding(54936));
        }


        //--------------------------------------------------------------------------------------

        /// <summary>
        /// ���������͵��ļ�����base64����
        /// </summary>
        /// <param name="fileName">�ļ���·�����ļ���</param>
        /// <returns>���ļ�����base64�������ַ���</returns>
        public static string EncodingForFile(string fileName)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(fileName);
            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);

            /*System.Byte[] b=new System.Byte[fs.Length];
            fs.Read(b,0,Convert.ToInt32(fs.Length));*/


            string base64String = Convert.ToBase64String(br.ReadBytes((int)fs.Length));


            br.Close();
            fs.Close();
            return base64String;
        }

        /// <summary>
        /// �Ѿ���base64������ַ�������Ϊ�ļ�
        /// </summary>
        /// <param name="base64String">��base64�������ַ���</param>
        /// <param name="fileName">�����ļ���·�����ļ���</param>
        /// <returns>�����ļ��Ƿ�ɹ�</returns>
        public static bool SaveDecodingToFile(string base64String, string fileName)
        {
            System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
            System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
            bw.Write(Convert.FromBase64String(base64String));
            bw.Close();
            fs.Close();
            return true;
        }


        //-------------------------------------------------------------------------------

        /// <summary>
        /// �������ַһȡ���ļ���ת��Ϊbase64����
        /// </summary>
        /// <param name="url">�ļ���url��ַ,һ�����Ե�url��ַ</param>
        /// <param name="objWebClient">System.Net.WebClient ����</param>
        /// <returns></returns>
        public static string EncodingFileFromUrl(string url, System.Net.WebClient objWebClient)
        {
            return Convert.ToBase64String(objWebClient.DownloadData(url));
        }


        /// <summary>
        /// �������ַһȡ���ļ���ת��Ϊbase64����
        /// </summary>
        /// <param name="url">�ļ���url��ַ,һ�����Ե�url��ַ</param>
        /// <returns>���ļ�ת�����base64�ַ���</returns>
        public static string EncodingFileFromUrl(string url)
        {
            //System.Net.WebClient myWebClient = new System.Net.WebClient();
            return EncodingFileFromUrl(url, new System.Net.WebClient());
        }
    }
}
