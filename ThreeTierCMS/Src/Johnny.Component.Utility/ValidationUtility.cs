using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace Johnny.Component.Utility
{
    public sealed class ValidationUtility
    {
        #region 是否Byte类型（8 位的无符号整数）： 0 和 255 之间的无符号整数
        /// <summary>
        /// 是否Byte类型（8 位的无符号整数）： 0 和 255 之间的无符号整数
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsByte(string input)
        {
            Byte result;
            return Byte.TryParse(input, out result);
        }
        #endregion

        #region 是否SByte类型（8 位的有符号整数）： -128 到 +127 之间的整数
        /// <summary>
        /// 是否SByte类型（8 位的有符号整数）： -128 到 +127 之间的整数
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsSByte(string input)
        {
            SByte result;
            return SByte.TryParse(input, out result);
        }
        #endregion

        #region 是否Int16类型（16 位的有符号整数）： -32768 到 +32767 之间的有符号整数
        /// <summary>
        /// 是否Int16类型（16 位的有符号整数）： -32768 到 +32767 之间的有符号整数
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsInt16(string input)
        {
            Int16 result;
            return Int16.TryParse(input, out result);
        }
        #endregion

        #region 是否Int32类型（32 位的有符号整数）：-2,147,483,648 到 +2,147,483,647 之间的有符号整数
        /// <summary>
        /// 是否Int32类型（32 位的有符号整数）：-2,147,483,648 到 +2,147,483,647 之间的有符号整数
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsInt32(string input)
        {
            int result;
            return Int32.TryParse(input, out result);
        }
        #endregion

        #region 是否Int64类型（64 位的有符号整数）： -9,223,372,036,854,775,808 到 +9,223,372,036,854,775,807 之间的整数
        /// <summary>
        /// 是否Int64类型（64 位的有符号整数）： -9,223,372,036,854,775,808 到 +9,223,372,036,854,775,807 之间的整数
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsInt64(string input)
        {
            Int64 result;
            return Int64.TryParse(input, out result);
        }
        #endregion

        #region 是否Single类型（单精度（32 位）浮点数字）： -3.402823e38 和 +3.402823e38 之间的单精度 32 位数字
        /// <summary>
        /// 是否Single类型（单精度（32 位）浮点数字）： -3.402823e38 和 +3.402823e38 之间的单精度 32 位数字
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsSingle(string input)
        {
            Single result;
            return Single.TryParse(input, out result);
        }
        #endregion

        #region 是否Double类型（单精度（64 位）浮点数字）： -1.79769313486232e308 和 +1.79769313486232e308 之间的双精度 64 位数字
        /// <summary>
        /// 是否Double类型（单精度（64 位）浮点数字）： -1.79769313486232e308 和 +1.79769313486232e308 之间的双精度 64 位数字
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsDouble(string input)
        {
            Double result;
            return Double.TryParse(input, out result);
        }
        #endregion

        #region 是否Boolean类型（布尔值）：true 或 false
        /// <summary>
        /// 是否Double类型（单精度（64 位）浮点数字）： -1.79769313486232e308 和 +1.79769313486232e308 之间的双精度 64 位数字
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsBoolean(string input)
        {
            Boolean result;
            return Boolean.TryParse(input, out result);
        }
        #endregion

        #region 是否Char类型（Unicode（16 位）字符）：该 16 位数字的值范围为从十六进制值 0x0000 到 0xFFFF
        /// <summary>
        /// 是否Char类型（Unicode（16 位）字符）：该 16 位数字的值范围为从十六进制值 0x0000 到 0xFFFF
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsChar(string input)
        {
            Char result;
            return Char.TryParse(input, out result);
        }
        #endregion

        #region 是否Decimal类型（96 位十进制值）：从正 79,228,162,514,264,337,593,543,950,335 到负 79,228,162,514,264,337,593,543,950,335 之间的十进制数
        /// <summary>
        /// 是否Decimal类型（96 位十进制值）：从正 79,228,162,514,264,337,593,543,950,335 到负 79,228,162,514,264,337,593,543,950,335 之间的十进制数
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsDecimal(string input)
        {
            Decimal result;
            return Decimal.TryParse(input, out result);
        }
        #endregion

        #region 是否DateTime类型（表示时间上的一刻）： 范围在公元（基督纪元）0001 年 1 月 1 日午夜 12:00:00 到公元 (C.E.) 9999 年 12 月 31 日晚上 11:59:59 之间的日期和时间
        /// <summary>
        /// 是否DateTime类型（表示时间上的一刻）： 范围在公元（基督纪元）0001 年 1 月 1 日午夜 12:00:00 到公元 (C.E.) 9999 年 12 月 31 日晚上 11:59:59 之间的日期和时间
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsDateTime(string input)
        {
            DateTime result;
            return DateTime.TryParse(input, out result);
        }
        #endregion

        #region 是否Date类型（表示时间的日期部分）： 范围在公元（基督纪元）0001 年 1 月 1 日 到公元 (C.E.) 9999 年 12 月 31 日之间的日期
        /// <summary>
        /// 是否Date类型（表示时间的日期部分）： 范围在公元（基督纪元）0001 年 1 月 1 日 到公元 (C.E.) 9999 年 12 月 31 日之间的日期
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsDate(string input)
        {
            DateTime result;
            if (DateTime.TryParse(input, out result) == false)
                return false;
            if (result.Hour == 0 && result.Minute == 0 && result.Second == 0 && result.Millisecond == 0)
                return true;
            else
                return false;
        }
        #endregion

        #region 是否Time类型（表示时间部分HHMMSS）： 范围在夜 12:00:00 到晚上 11:59:59 之间的时间
        /// <summary>
        /// 是否Time类型（表示时间部分HHMMSS）： 范围在夜 12:00:00 到晚上 11:59:59 之间的时间
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsTime(string input)
        {
            DateTime result;
            if (DateTime.TryParse(input, out result) == false)
                return false;
            if (result.Year == 1 && result.Month == 1 && result.Day == 1)
                return true;
            else
                return false;
        }
        #endregion

        #region 是否IPAddress类型（IPv4 的情况下使用以点分隔的四部分表示法格式表示，IPv6 的情况下使用冒号与十六进制格式表示）
        /// <summary>
        /// 是否IPAddress类型（IPv4 的情况下使用以点分隔的四部分表示法格式表示，IPv6 的情况下使用冒号与十六进制格式表示）
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsIPAddress(string input)
        {
            System.Net.IPAddress result;
            return System.Net.IPAddress.TryParse(input, out result);
        }
        #endregion

        #region 是否中国电话号码类型（XXX/XXXX-XXXXXXX/XXXXXXXX (\d{3,4})-?\d{7,8}）：判断是否是（区号：3或4位）-（电话号码：7或8位）
        /// <summary>
        /// 是否中国电话号码类型（XXX/XXXX-XXXXXXX/XXXXXXXX (\d{3,4})-?\d{7,8}）：判断是否是（区号：3或4位）-（电话号码：7或8位）
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsChinaPhone(string input)
        {
            return CommRegularMatch(input, @"(\d{3,4})-?\d{7,8}");
        }
        #endregion

        #region 是否中国邮政编码（6位数字 \d{6}）
        /// <summary>
        /// 是否中国邮政编码（6位数字 \d{6}）
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsChinesePostalCode(string input)
        {
            return CommRegularMatch(input, @"\d{6}");
        }
        #endregion

        #region 是否中国移动电话号码（13开头的总11位数字 13\d{9}）
        /// <summary>
        /// 是否中国移动电话号码（13开头的总11位数字 13\d{9}）
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsChineseMobile(string input)
        {
            return CommRegularMatch(input, @"13\d{9}");
        }
        #endregion

        #region 是否EMail类型（XXX@XXX.XXX \w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*）
        /// <summary>
        /// 是否EMail类型（XXX@XXX.XXX \w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*）
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsEmail(string input)
        {
            return CommRegularMatch(input, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        }
        #endregion

        #region 是否Internet URL地址类型（http://）
        /// <summary>
        /// 是否Internet URL地址类型（http://）
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsURL(string input)
        {
            return CommRegularMatch(input, @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
        }
        #endregion

        #region 是否中文字符（[\u4e00-\u9fa5]）
        /// <summary>
        /// 是否中文字符（[\u4e00-\u9fa5]）
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsChineseWord(string input)
        {
            ArrayList aryResult = new ArrayList();
            return CommRegularMatch(input, @"[\u4e00-\u9fa5]");
        }
        #endregion

        #region 是否是数字（0到9的数字[\d]+）：不包括符号"."和"-"
        /// <summary>
        /// 是否是数字（0到9的数字[\d]+）：不包括符号"."和"-"
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsNumber(string input)
        {
            return CommRegularMatch(input, @"[\d]+");
        }
        #endregion

        #region 是否只包含数字，英文和下划线（[\w]+）
        /// <summary>
        /// 是否只包含数字，英文和下划线（[\w]+）
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsStringModel_01(string input)
        {
            return CommRegularMatch(input, @"[\w]+");
        }
        #endregion

        #region 是否大写首字母的英文字母（[A-Z][a-z]+）
        /// <summary>
        /// 是否大写首字母的英文字母（[A-Z][a-z]+）
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsStringModel_02(string input)
        {
            return CommRegularMatch(input, @"[A-Z][a-z]+");
        }
        #endregion

        #region 是否全角字符（[^\x00-\xff]）：包括汉字在内
        /// <summary>
        /// 是否全角字符（[^\x00-\xff]）：包括汉字在内
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsWideWord(string input)
        {
            return CommRegularMatch(input, @"[^\x00-\xff]");
        }
        #endregion

        #region 是否半角字符（[\x00-\xff]）
        /// <summary>
        /// 是否半角字符（[^\x00-\xff]）：包括汉字在内
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsNarrowWord(string input)
        {
            ArrayList aryResult = new ArrayList();
            return CommRegularMatch(input, @"[\x00-\xff]");
        }
        #endregion

        #region 是否合法的中国身份证号码
        public bool IsChineseID(string input)
        {
            if (input.Length == 15)
            {
                String MailRegex = @"^\d{15}$";
                if (!Regex.IsMatch(input, MailRegex))
                {
                    return false;
                }
                input = CidUpdate(input);
            }
            if (input.Length == 18)
            {
                string strResult = CheckCidInfo(input);
                if (strResult == "非法地区" || strResult == "非法生日" || strResult == "非法证号")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 是否合法QQ号码
        /// <summary>
        /// 是否合法QQ号码（5-10位）
        /// </summary>
        /// <returns>Boolean</returns>
        public bool IsQQ(string input)
        {
            ArrayList aryResult = new ArrayList();
            return CommRegularMatch(input, @"^\d{5,10}$");
        }
        #endregion

        #region 是否非法字符
        /// <summary>
        /// 是否非法字符
        /// </summary>
        /// <returns>Boolean</returns>
        public bool IsStringModel_03(string input)
        {
            return CommRegularMatch(input, @"^(?:[\u4e00-\u9fa5]*\w*\s*)+$");
        }
        #endregion

        #region 通用正则表达式判断函数
        /// <summary>
        /// 通用正则表达式判断函数
        /// </summary>
        /// <param name="strVerifyString">String，用于匹配的字符串</param>
        /// <param name="strRegular">String，正则表达式</param>
        /// <returns>Boolean</returns>
        public static bool CommRegularMatch(string input, string strRegular)
        {
            return Regex.IsMatch(input,strRegular,RegexOptions.Compiled);
        }
        #endregion

        #region 中国身份证号码验证
        private string CheckCidInfo(string cid)
        {
            string[] aCity = new string[] { null, null, null, null, null, null, null, null, null, null, null, "北京", "天津", "河北", "山西", "内蒙古", null, null, null, null, null, "辽宁", "吉林", "黑龙江", null, null, null, null, null, null, null, "上海", "江苏", "浙江", "安微", "福建", "江西", "山东", null, null, null, "河南", "湖北", "湖南", "广东", "广西", "海南", null, null, null, "重庆", "四川", "贵州", "云南", "西藏", null, null, null, null, null, null, "陕西", "甘肃", "青海", "宁夏", "新疆", null, null, null, null, null, "台湾", null, null, null, null, null, null, null, null, null, "香港", "澳门", null, null, null, null, null, null, null, null, "国外" };
            double iSum = 0;
            string info = string.Empty;
            System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex(@"^\d{17}(\d|x)$");
            System.Text.RegularExpressions.Match mc = rg.Match(cid);
            if (!mc.Success)
            {
                return string.Empty;
            }
            cid = cid.ToLower();
            cid = cid.Replace("x", "a");
            if (aCity[int.Parse(cid.Substring(0, 2))] == null)
            {
                return "非法地区";
            }
            try
            {
                DateTime.Parse(cid.Substring(6, 4) + " - " + cid.Substring(10, 2) + " - " + cid.Substring(12, 2));
            }
            catch
            {
                return "非法生日";
            }
            for (int i = 17; i >= 0; i--)
            {
                iSum += (System.Math.Pow(2, i) % 11) * int.Parse(cid[17 - i].ToString(), System.Globalization.NumberStyles.HexNumber);
            }
            if (iSum % 11 != 1)
            {
                return ("非法证号");
            }
            else
            {
                return (aCity[int.Parse(cid.Substring(0, 2))] + "," + cid.Substring(6, 4) + "-" + cid.Substring(10, 2) + "-" + cid.Substring(12, 2) + "," + (int.Parse(cid.Substring(16, 1)) % 2 == 1 ? "男" : "女"));
            }
        }
        #endregion

        #region 身份证号码15升级为18位
        private string CidUpdate(string ShortCid)
        {
            //char[] strJiaoYan = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
            //int[] intQuan = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1 };
            //string strTemp;
            //int intTemp = 0;

            //strTemp = ShortCid.Substring(0, 6) + "19" + ShortCid.Substring(6);
            //for (int i = 0; i <= strTemp.Length - 1; i++)
            //{
            //    intTemp += int.Parse(strTemp.Substring(i, 1)) * intQuan[i];
            //}
            //intTemp = intTemp % 11;
            //return strTemp + strJiaoYan[intTemp];

            int iS = 0;

            //加权因子常数 
            int[] iW = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            //校验码常数 
            string LastCode = "10X98765432";
            //新身份证号 
            string perIDNew;

            perIDNew = ShortCid.Substring(0, 6);
            //填在第6位及第7位上填上‘1’，‘9’两个数字 
            perIDNew += "19";

            perIDNew += ShortCid.Substring(6, 9);

            //进行加权求和 
            for (int i = 0; i < 17; i++)
            {
                iS += int.Parse(perIDNew.Substring(i, 1)) * iW[i];
            }

            //取模运算，得到模值 
            int iY = iS % 11;
            //从LastCode中取得以模为索引号的值，加到身份证的最后一位，即为新身份证号。 
            perIDNew += LastCode.Substring(iY, 1);
            return perIDNew;
        }
        #endregion
    }
}
