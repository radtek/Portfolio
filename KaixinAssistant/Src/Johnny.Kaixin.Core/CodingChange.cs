using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class WordConvert
    {
        public static string ToUnicode(string character)
        {
            string coding = "";
            for (int i = 0; i < character.Length; i++)
            {
                byte[] bytes = System.Text.Encoding.Unicode.GetBytes(character.Substring(i, 1)); //取出二进制编码内容
                string lowCode = System.Convert.ToString(bytes[0], 16); //取出低字节编码内容（两位16进制）
                if (lowCode.Length == 1)
                    lowCode = "0" + lowCode;
                string hightCode = System.Convert.ToString(bytes[1], 16);//取出高字节编码内容（两位16进制）
                if (hightCode.Length == 1)
                    hightCode = "0" + hightCode;
                coding += "\\u" + hightCode + lowCode;//加入到字符串中,
            }
            return coding;
        }
        public static string ToChinese(string coding)
        {
            int pos = 0;
            string characters = "";
            string word;
            for (int ix = 0; ix < coding.Length; ix++)
            {
                pos = coding.IndexOf("\\u", ix);
                if ((pos - ix) == 0)
                {
                    word = CodeToWord(coding.Substring(pos + 2, 4)); //每四位为一个汉字
                    characters += word;
                    ix += 5; //移动到该汉字后一个位置。
                }
                else if ((pos-ix) > 0)
                {
                    characters += coding.Substring(ix, pos - ix);
                    word = CodeToWord(coding.Substring(pos + 2, 4)); //每四位为一个汉字
                    characters += word;
                    ix += 5 + pos - ix; //移动到该汉字后一个位置。 
                }
                else
                {
                    characters += coding.Substring(ix, coding.Length - ix);
                    ix += coding.Length - ix;
                }
            }
            return characters;
        }

        private static string CodeToWord(string code)
        {
            if (code == null || code == string.Empty)
                return "";

            byte[] bytes = new byte[2];
            string highCode = code.Substring(2, 2); //取出高字节,并以16进制进行转换
            bytes[0] = System.Convert.ToByte(highCode, 16);
            string lowCode = code.Substring(0, 2); //取出低字节,并以16进制进制转换
            bytes[1] = System.Convert.ToByte(lowCode, 16);
            return System.Text.Encoding.Unicode.GetString(bytes);
        }

        //public static string ToUnicode(string character)
        //{
        //    string coding = "";
        //    for (int i = 0; i < character.Length; i++)
        //    {
        //        byte[] bytes = System.Text.Encoding.Unicode.GetBytes(character.Substring(i, 1)); //取出二进制编码内容
        //        string lowCode = System.Convert.ToString(bytes[0], 16); //取出低字节编码内容（两位16进制）
        //        if (lowCode.Length == 1)
        //            lowCode = "0" + lowCode;
        //        string hightCode = System.Convert.ToString(bytes[1], 16);//取出高字节编码内容（两位16进制）
        //        if (hightCode.Length == 1)
        //            hightCode = "0" + hightCode;
        //        coding += "\\u" + hightCode + lowCode;//加入到字符串中,
        //    }
        //    return coding;
        //}

        //public static string ToChinese(string coding)
        //{
        //    coding = coding.Replace("\\u", "");
        //    string characters = "";
        //    if (coding.Length % 4 != 0)//编码为16进制,必须为4的倍数。
        //    {
        //        return "";
        //    }
        //    for (int i = 0; i < coding.Length; i += 4) //每四位为一个汉字
        //    {
        //        byte[] bytes = new byte[2];
        //        string highCode = coding.Substring(i + 2, 2); //取出高字节,并以16进制进行转换
        //        bytes[0] = System.Convert.ToByte(highCode, 16);
        //        string lowCode = coding.Substring(i, 2); //取出低字节,并以16进制进制转换
        //        bytes[1] = System.Convert.ToByte(lowCode, 16);
        //        string character = System.Text.Encoding.Unicode.GetString(bytes);
        //        characters += character;
        //    }
        //    return characters;
        //}

    }
}
