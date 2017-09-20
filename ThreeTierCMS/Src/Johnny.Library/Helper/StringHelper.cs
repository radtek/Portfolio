using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Library.Helper
{
    public class StringHelper
    {
        #region FiltrateHtmlTags
        public static string FiltrateHtmlTags(string result)
        {
            if (result == null)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            bool flag = false;
            foreach (char ch in result)
            {
                switch (ch)
                {
                    case '<':
                        flag = true;
                        break;

                    case '>':
                        flag = false;
                        break;

                    default:
                        if (!flag)
                        {
                            if (ch == '\n')
                            {
                                builder.Append(" ");
                            }
                            else if (((ch != ' ') && (ch != '\t')) && (ch != '\r'))
                            {
                                builder.Append(ch);
                            }
                        }
                        break;
                }
            }
            return builder.ToString().Trim().Replace("&yen;", "￥").Replace("&nbsp;", "");
        }
        #endregion

        #region CreateHtml
        public static string CreateHtml(string result)
        {
            if (result == null)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            bool flag = false;
            foreach (char ch in result)
            {
                if (ch == '\r')
                {
                    flag = true;
                    continue;
                }
                if (ch == '\n')
                {
                    if (flag)
                    {
                        builder.Append("<br>");
                        flag = false;
                        continue;
                    }
                }
                builder.Append(ch);
            }
            return builder.ToString().Trim();
        }
        #endregion

        #region ConvertToHtmlTags
        public static string ConvertToHtmlTags(string result)
        {
            if (result == null)
            {
                return string.Empty;
            }
            return result.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&nbsp;", " ");
        }
        #endregion

        #region GetLengthByByte(string) 取得字符串实际长度
        public static int GetLengthByByte(string strText)
        {
            return System.Text.Encoding.Default.GetByteCount(strText);
        }
        #endregion

        #region GetLength(string) 取得字符串长度
        public static int GetLength(string strText)
        {
            return strText.Length;
        }
        #endregion

        #region htmlInputText(string) HTML过滤输入字符串
        public static string htmlInputText(string inputString)//HTML过滤输入字符串
        {
            if ((inputString != null) && (inputString != String.Empty))
            {
                inputString = inputString.Trim();
                inputString = inputString.Replace("'", "&quot;");
                inputString = inputString.Replace("<", "&lt;");
                inputString = inputString.Replace(">", "&gt;");
                inputString = inputString.Replace(" ", "&nbsp;");
                inputString = inputString.Replace("\n", "<br>");
                return inputString.ToString();
            }
            return "";
        }
        #endregion

        #region htmlOutputText(string) HTML还原字符串
        public static string htmlOutputText(string inputString)//HTML还原字符串
        {
            if ((inputString != null) && (inputString != String.Empty))
            {
                inputString = inputString.Trim();
                inputString = inputString.Replace("&quot;", "'");
                inputString = inputString.Replace("&lt;", "<");
                inputString = inputString.Replace("&gt;", ">");
                inputString = inputString.Replace("&nbsp;", " ");
                inputString = inputString.Replace("<br>", "\n");
                return inputString.ToString();
            }
            return "";
        }
        #endregion
    }
}
