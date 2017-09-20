using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.JScript.Vsa;
using Microsoft.JScript;

using System.Net.Json;

namespace Johnny.Kaixin.Helper
{
    public class JsonHelper
    {
        // Methods
        public JsonHelper()
        {
        }

        #region GetIntegerValue
        public static int GetIntegerValue(JsonObject field)
        {
            string stringValue = GetStringValue(field);
            if (stringValue == null)
            {
                return -1;
            }
            try
            {
                return int.Parse(stringValue);
            }
            catch (OverflowException)
            {
                return -1;
            }
            catch (FormatException)
            {
                return -1;
            }
        }
        #endregion

        #region GetStringValue
        public static string GetStringValue(JsonObject field)
        {
            if ((field == null) || (field.GetValue() == null))
            {
                return null;
            }
            string str = null;
            string name = field.GetValue().GetType().Name;
            if (name == null)
            {
                return str;
            }
            if (!(name == "String"))
            {
                if (name != "Double")
                {
                    if (name != "Boolean")
                    {
                        return str;
                    }
                    return field.GetValue().ToString();
                }
            }
            else
            {
                return (string)field.GetValue();
            }
            return field.GetValue().ToString();
        }
        #endregion

        #region GetMid
        public static string GetMid(string str, string start, string end)
        {
            int num;
            return GetMid(str, start, end, out num);
        }

        public static string GetMid(string str, string start, string end, out int pos)
        {
            pos = -1;
            if (str == null)
            {
                return null;
            }
            int index = str.IndexOf(start);
            if (-1 == index)
            {
                return null;
            }
            index += start.Length;
            int num2 = str.IndexOf(end, index);
            if (-1 == num2)
            {
                return null;
            }
            pos = num2;
            return str.Substring(index, num2 - index);
        }
        #endregion

        #region GetMidLast
        public static string GetMidLast(string str, string start, string end)
        {
            int num;
            return GetMidLast(str, start, end, out num);
        }

        public static string GetMidLast(string str, string start, string end, out int pos)
        {
            pos = -1;
            if (str == null)
            {
                return null;
            }
            int index = str.LastIndexOf(start);
            if (-1 == index)
            {
                return null;
            }
            index += start.Length;
            int num2 = str.LastIndexOf(end);
            if (-1 == num2)
            {
                return null;
            }
            pos = num2;
            return str.Substring(index, num2 - index);
        }
        #endregion

        #region GetFirstLast
        public static string GetFirstLast(string str, string start, string end)
        {
            int num;
            return GetFirstLast(str, start, end, out num);
        }

        public static string GetFirstLast(string str, string start, string end, out int pos)
        {
            pos = -1;
            if (str == null)
            {
                return null;
            }
            int index = str.IndexOf(start);
            if (-1 == index)
            {
                return null;
            }
            index += start.Length;
            int num2 = str.LastIndexOf(end);
            if (-1 == num2)
            {
                return null;
            }
            pos = num2;
            return str.Substring(index, num2 - index);
        }
        #endregion
        
        #region GetMidInteger
        public static int GetMidInteger(string str, string start, string end)
        {
            int num;
            return GetMidInteger(str, start, end, out num);
        }

        public static int GetMidInteger(string str, string start, string end, out int pos)
        {
            return GetInteger(GetMid(str, start, end, out pos));
        }        
        #endregion

        #region GetInteger
        public static int GetInteger(string str)
        {
            int num;
            if (!string.IsNullOrEmpty(str) && int.TryParse(str, out num))
            {
                return num;
            }
            return -1;
        }
        #endregion
        
        #region FiltrateHtmlTags
        public static string FiltrateHtmlTags(string result)
        {
            if (result == null)
            {
                return string.Empty;
            }

            //<div  style=\"padding:20px 0 20px 120px;\">\n\t\t<!--<div class=\"dealimg\"><a href=\"/interface/c.php?name=slave_comfort-img_26&url=http%3A%2F%2Fwww.iask.com%2F\" target=_blank><img src=\"/i2/kaixinlogo.gif\" /></a></div>-->\n\t\t\n\n<object codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,0,0\" width=\"210\" height=\"210\" align=\"middle\">\n<param name=\"allowScriptAccess\" value=\"always\" />\n<param name=\"allowFullScreen\" value=\"false\" />\n<param name=\"movie\" value=\"/i/slave/a_moto.swf\" />\n<param name=\"quality\" value=\"high\" />\n<param name=\"bgcolor\" value=\"#ffffff\" />\n<param name=\"wmode\" value=\"opaque\" />\n<embed name=\"cpm_swf\" src=\"/i/slave/a_moto.swf\" quality=\"high\" bgcolor=\"#ffffff\" width=\"210\" height=\"210\" align=\"middle\" allowScriptAccess=\"sameDomain\" allowFullScreen=\"false\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" />\n</object>\n\t\t\n\t\t<div class=\"c\"></div>\n\t</div>\n\t<div class=\"f14\" style=\"width:24em;margin:0 auto;\"><strong>你给奴隶<span class=\"sl\">蔡港</span>配备一款MOTO A3000 GPS智能手机，外出的奴隶不再迷路，高呼\"主人万岁\"。 </strong><a href=\"/interface/c.php?name=slave_comfort-text_26&url=http%3A%2F%2Fad.cn.doubleclick.net%2Fclk%3B211795683%3B33270003%3Bz%3Fhttp%3A%2F%2Fa3000.motorola.com.cn%2F%3FWT.mc_id%3DA3000LAKX004\" target=\"_blank\" class=\"sl f12\">详细&gt;&gt;</a></div>\n\t\n\t<div class=\"f14\" style=\"width:24em;margin:20px auto;\"><strong>奴隶<span class=\"sl\">蔡港</span>感恩图报，为你挣回<strong class=\"dgreen\">&yen;90</strong></strong></div>\n\t\n\t<div style=\"padding:20px 166px;\">\n\t<div class=\"rbs1\">\n\t\t
            Regex regular = new Regex(@"<!--[\s\S]+-->");
            result = regular.Replace(result, "");

            result = result.Replace("&gt;", ">").Replace("&lt;", "<").Replace("<br>", " ");

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

        #region RunJavascript
        public static string RunJavascript(string scriptText)
        {
            VsaEngine engine = VsaEngine.CreateEngine();
            return Eval.JScriptEvaluate(scriptText, engine).ToString();
        }
        #endregion

    }
}
