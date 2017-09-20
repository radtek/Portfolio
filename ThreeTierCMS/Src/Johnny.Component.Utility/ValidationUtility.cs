using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace Johnny.Component.Utility
{
    public sealed class ValidationUtility
    {
        #region �Ƿ�Byte���ͣ�8 λ���޷����������� 0 �� 255 ֮����޷�������
        /// <summary>
        /// �Ƿ�Byte���ͣ�8 λ���޷����������� 0 �� 255 ֮����޷�������
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsByte(string input)
        {
            Byte result;
            return Byte.TryParse(input, out result);
        }
        #endregion

        #region �Ƿ�SByte���ͣ�8 λ���з����������� -128 �� +127 ֮�������
        /// <summary>
        /// �Ƿ�SByte���ͣ�8 λ���з����������� -128 �� +127 ֮�������
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsSByte(string input)
        {
            SByte result;
            return SByte.TryParse(input, out result);
        }
        #endregion

        #region �Ƿ�Int16���ͣ�16 λ���з����������� -32768 �� +32767 ֮����з�������
        /// <summary>
        /// �Ƿ�Int16���ͣ�16 λ���з����������� -32768 �� +32767 ֮����з�������
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsInt16(string input)
        {
            Int16 result;
            return Int16.TryParse(input, out result);
        }
        #endregion

        #region �Ƿ�Int32���ͣ�32 λ���з�����������-2,147,483,648 �� +2,147,483,647 ֮����з�������
        /// <summary>
        /// �Ƿ�Int32���ͣ�32 λ���з�����������-2,147,483,648 �� +2,147,483,647 ֮����з�������
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsInt32(string input)
        {
            int result;
            return Int32.TryParse(input, out result);
        }
        #endregion

        #region �Ƿ�Int64���ͣ�64 λ���з����������� -9,223,372,036,854,775,808 �� +9,223,372,036,854,775,807 ֮�������
        /// <summary>
        /// �Ƿ�Int64���ͣ�64 λ���з����������� -9,223,372,036,854,775,808 �� +9,223,372,036,854,775,807 ֮�������
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsInt64(string input)
        {
            Int64 result;
            return Int64.TryParse(input, out result);
        }
        #endregion

        #region �Ƿ�Single���ͣ������ȣ�32 λ���������֣��� -3.402823e38 �� +3.402823e38 ֮��ĵ����� 32 λ����
        /// <summary>
        /// �Ƿ�Single���ͣ������ȣ�32 λ���������֣��� -3.402823e38 �� +3.402823e38 ֮��ĵ����� 32 λ����
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsSingle(string input)
        {
            Single result;
            return Single.TryParse(input, out result);
        }
        #endregion

        #region �Ƿ�Double���ͣ������ȣ�64 λ���������֣��� -1.79769313486232e308 �� +1.79769313486232e308 ֮���˫���� 64 λ����
        /// <summary>
        /// �Ƿ�Double���ͣ������ȣ�64 λ���������֣��� -1.79769313486232e308 �� +1.79769313486232e308 ֮���˫���� 64 λ����
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsDouble(string input)
        {
            Double result;
            return Double.TryParse(input, out result);
        }
        #endregion

        #region �Ƿ�Boolean���ͣ�����ֵ����true �� false
        /// <summary>
        /// �Ƿ�Double���ͣ������ȣ�64 λ���������֣��� -1.79769313486232e308 �� +1.79769313486232e308 ֮���˫���� 64 λ����
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsBoolean(string input)
        {
            Boolean result;
            return Boolean.TryParse(input, out result);
        }
        #endregion

        #region �Ƿ�Char���ͣ�Unicode��16 λ���ַ������� 16 λ���ֵ�ֵ��ΧΪ��ʮ������ֵ 0x0000 �� 0xFFFF
        /// <summary>
        /// �Ƿ�Char���ͣ�Unicode��16 λ���ַ������� 16 λ���ֵ�ֵ��ΧΪ��ʮ������ֵ 0x0000 �� 0xFFFF
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsChar(string input)
        {
            Char result;
            return Char.TryParse(input, out result);
        }
        #endregion

        #region �Ƿ�Decimal���ͣ�96 λʮ����ֵ�������� 79,228,162,514,264,337,593,543,950,335 ���� 79,228,162,514,264,337,593,543,950,335 ֮���ʮ������
        /// <summary>
        /// �Ƿ�Decimal���ͣ�96 λʮ����ֵ�������� 79,228,162,514,264,337,593,543,950,335 ���� 79,228,162,514,264,337,593,543,950,335 ֮���ʮ������
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsDecimal(string input)
        {
            Decimal result;
            return Decimal.TryParse(input, out result);
        }
        #endregion

        #region �Ƿ�DateTime���ͣ���ʾʱ���ϵ�һ�̣��� ��Χ�ڹ�Ԫ��������Ԫ��0001 �� 1 �� 1 ����ҹ 12:00:00 ����Ԫ (C.E.) 9999 �� 12 �� 31 ������ 11:59:59 ֮������ں�ʱ��
        /// <summary>
        /// �Ƿ�DateTime���ͣ���ʾʱ���ϵ�һ�̣��� ��Χ�ڹ�Ԫ��������Ԫ��0001 �� 1 �� 1 ����ҹ 12:00:00 ����Ԫ (C.E.) 9999 �� 12 �� 31 ������ 11:59:59 ֮������ں�ʱ��
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsDateTime(string input)
        {
            DateTime result;
            return DateTime.TryParse(input, out result);
        }
        #endregion

        #region �Ƿ�Date���ͣ���ʾʱ������ڲ��֣��� ��Χ�ڹ�Ԫ��������Ԫ��0001 �� 1 �� 1 �� ����Ԫ (C.E.) 9999 �� 12 �� 31 ��֮�������
        /// <summary>
        /// �Ƿ�Date���ͣ���ʾʱ������ڲ��֣��� ��Χ�ڹ�Ԫ��������Ԫ��0001 �� 1 �� 1 �� ����Ԫ (C.E.) 9999 �� 12 �� 31 ��֮�������
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

        #region �Ƿ�Time���ͣ���ʾʱ�䲿��HHMMSS���� ��Χ��ҹ 12:00:00 ������ 11:59:59 ֮���ʱ��
        /// <summary>
        /// �Ƿ�Time���ͣ���ʾʱ�䲿��HHMMSS���� ��Χ��ҹ 12:00:00 ������ 11:59:59 ֮���ʱ��
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

        #region �Ƿ�IPAddress���ͣ�IPv4 �������ʹ���Ե�ָ����Ĳ��ֱ�ʾ����ʽ��ʾ��IPv6 �������ʹ��ð����ʮ�����Ƹ�ʽ��ʾ��
        /// <summary>
        /// �Ƿ�IPAddress���ͣ�IPv4 �������ʹ���Ե�ָ����Ĳ��ֱ�ʾ����ʽ��ʾ��IPv6 �������ʹ��ð����ʮ�����Ƹ�ʽ��ʾ��
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsIPAddress(string input)
        {
            System.Net.IPAddress result;
            return System.Net.IPAddress.TryParse(input, out result);
        }
        #endregion

        #region �Ƿ��й��绰�������ͣ�XXX/XXXX-XXXXXXX/XXXXXXXX (\d{3,4})-?\d{7,8}�����ж��Ƿ��ǣ����ţ�3��4λ��-���绰���룺7��8λ��
        /// <summary>
        /// �Ƿ��й��绰�������ͣ�XXX/XXXX-XXXXXXX/XXXXXXXX (\d{3,4})-?\d{7,8}�����ж��Ƿ��ǣ����ţ�3��4λ��-���绰���룺7��8λ��
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsChinaPhone(string input)
        {
            return CommRegularMatch(input, @"(\d{3,4})-?\d{7,8}");
        }
        #endregion

        #region �Ƿ��й��������루6λ���� \d{6}��
        /// <summary>
        /// �Ƿ��й��������루6λ���� \d{6}��
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsChinesePostalCode(string input)
        {
            return CommRegularMatch(input, @"\d{6}");
        }
        #endregion

        #region �Ƿ��й��ƶ��绰���루13��ͷ����11λ���� 13\d{9}��
        /// <summary>
        /// �Ƿ��й��ƶ��绰���루13��ͷ����11λ���� 13\d{9}��
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsChineseMobile(string input)
        {
            return CommRegularMatch(input, @"13\d{9}");
        }
        #endregion

        #region �Ƿ�EMail���ͣ�XXX@XXX.XXX \w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*��
        /// <summary>
        /// �Ƿ�EMail���ͣ�XXX@XXX.XXX \w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*��
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsEmail(string input)
        {
            return CommRegularMatch(input, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        }
        #endregion

        #region �Ƿ�Internet URL��ַ���ͣ�http://��
        /// <summary>
        /// �Ƿ�Internet URL��ַ���ͣ�http://��
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsURL(string input)
        {
            return CommRegularMatch(input, @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
        }
        #endregion

        #region �Ƿ������ַ���[\u4e00-\u9fa5]��
        /// <summary>
        /// �Ƿ������ַ���[\u4e00-\u9fa5]��
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsChineseWord(string input)
        {
            ArrayList aryResult = new ArrayList();
            return CommRegularMatch(input, @"[\u4e00-\u9fa5]");
        }
        #endregion

        #region �Ƿ������֣�0��9������[\d]+��������������"."��"-"
        /// <summary>
        /// �Ƿ������֣�0��9������[\d]+��������������"."��"-"
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsNumber(string input)
        {
            return CommRegularMatch(input, @"[\d]+");
        }
        #endregion

        #region �Ƿ�ֻ�������֣�Ӣ�ĺ��»��ߣ�[\w]+��
        /// <summary>
        /// �Ƿ�ֻ�������֣�Ӣ�ĺ��»��ߣ�[\w]+��
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsStringModel_01(string input)
        {
            return CommRegularMatch(input, @"[\w]+");
        }
        #endregion

        #region �Ƿ��д����ĸ��Ӣ����ĸ��[A-Z][a-z]+��
        /// <summary>
        /// �Ƿ��д����ĸ��Ӣ����ĸ��[A-Z][a-z]+��
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsStringModel_02(string input)
        {
            return CommRegularMatch(input, @"[A-Z][a-z]+");
        }
        #endregion

        #region �Ƿ�ȫ���ַ���[^\x00-\xff]����������������
        /// <summary>
        /// �Ƿ�ȫ���ַ���[^\x00-\xff]����������������
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsWideWord(string input)
        {
            return CommRegularMatch(input, @"[^\x00-\xff]");
        }
        #endregion

        #region �Ƿ����ַ���[\x00-\xff]��
        /// <summary>
        /// �Ƿ����ַ���[^\x00-\xff]����������������
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsNarrowWord(string input)
        {
            ArrayList aryResult = new ArrayList();
            return CommRegularMatch(input, @"[\x00-\xff]");
        }
        #endregion

        #region �Ƿ�Ϸ����й����֤����
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
                if (strResult == "�Ƿ�����" || strResult == "�Ƿ�����" || strResult == "�Ƿ�֤��")
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

        #region �Ƿ�Ϸ�QQ����
        /// <summary>
        /// �Ƿ�Ϸ�QQ���루5-10λ��
        /// </summary>
        /// <returns>Boolean</returns>
        public bool IsQQ(string input)
        {
            ArrayList aryResult = new ArrayList();
            return CommRegularMatch(input, @"^\d{5,10}$");
        }
        #endregion

        #region �Ƿ�Ƿ��ַ�
        /// <summary>
        /// �Ƿ�Ƿ��ַ�
        /// </summary>
        /// <returns>Boolean</returns>
        public bool IsStringModel_03(string input)
        {
            return CommRegularMatch(input, @"^(?:[\u4e00-\u9fa5]*\w*\s*)+$");
        }
        #endregion

        #region ͨ��������ʽ�жϺ���
        /// <summary>
        /// ͨ��������ʽ�жϺ���
        /// </summary>
        /// <param name="strVerifyString">String������ƥ����ַ���</param>
        /// <param name="strRegular">String��������ʽ</param>
        /// <returns>Boolean</returns>
        public static bool CommRegularMatch(string input, string strRegular)
        {
            return Regex.IsMatch(input,strRegular,RegexOptions.Compiled);
        }
        #endregion

        #region �й����֤������֤
        private string CheckCidInfo(string cid)
        {
            string[] aCity = new string[] { null, null, null, null, null, null, null, null, null, null, null, "����", "���", "�ӱ�", "ɽ��", "���ɹ�", null, null, null, null, null, "����", "����", "������", null, null, null, null, null, null, null, "�Ϻ�", "����", "�㽭", "��΢", "����", "����", "ɽ��", null, null, null, "����", "����", "����", "�㶫", "����", "����", null, null, null, "����", "�Ĵ�", "����", "����", "����", null, null, null, null, null, null, "����", "����", "�ຣ", "����", "�½�", null, null, null, null, null, "̨��", null, null, null, null, null, null, null, null, null, "���", "����", null, null, null, null, null, null, null, null, "����" };
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
                return "�Ƿ�����";
            }
            try
            {
                DateTime.Parse(cid.Substring(6, 4) + " - " + cid.Substring(10, 2) + " - " + cid.Substring(12, 2));
            }
            catch
            {
                return "�Ƿ�����";
            }
            for (int i = 17; i >= 0; i--)
            {
                iSum += (System.Math.Pow(2, i) % 11) * int.Parse(cid[17 - i].ToString(), System.Globalization.NumberStyles.HexNumber);
            }
            if (iSum % 11 != 1)
            {
                return ("�Ƿ�֤��");
            }
            else
            {
                return (aCity[int.Parse(cid.Substring(0, 2))] + "," + cid.Substring(6, 4) + "-" + cid.Substring(10, 2) + "-" + cid.Substring(12, 2) + "," + (int.Parse(cid.Substring(16, 1)) % 2 == 1 ? "��" : "Ů"));
            }
        }
        #endregion

        #region ���֤����15����Ϊ18λ
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

            //��Ȩ���ӳ��� 
            int[] iW = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            //У���볣�� 
            string LastCode = "10X98765432";
            //�����֤�� 
            string perIDNew;

            perIDNew = ShortCid.Substring(0, 6);
            //���ڵ�6λ����7λ�����ϡ�1������9���������� 
            perIDNew += "19";

            perIDNew += ShortCid.Substring(6, 9);

            //���м�Ȩ��� 
            for (int i = 0; i < 17; i++)
            {
                iS += int.Parse(perIDNew.Substring(i, 1)) * iW[i];
            }

            //ȡģ���㣬�õ�ģֵ 
            int iY = iS % 11;
            //��LastCode��ȡ����ģΪ�����ŵ�ֵ���ӵ����֤�����һλ����Ϊ�����֤�š� 
            perIDNew += LastCode.Substring(iY, 1);
            return perIDNew;
        }
        #endregion
    }
}
