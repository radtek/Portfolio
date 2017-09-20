using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace Johnny.Kaixin.Helper
{
    public class DataValidation
    {        
        public DataValidation()
        {
        }

        #region IsNull(object)
        /// <summary>
        /// �ж��Ƿ�Ϊnull
        /// </summary>
        /// <param name="oValue">ָ����objectֵ</param>
        /// <returns>����bool���жϽ��</returns>
        public static bool IsNull(object oValue)
        {
            if (oValue == null || oValue == System.DBNull.Value)
                return true;
            else
                return false;
        }
        #endregion

        #region IsNullOrEmpty(object)
        /// <summary>
        /// �ж��Ƿ�ΪEmtpy
        /// </summary>
        /// <param name="oValue">ָ����objectֵ</param>
        /// <returns>����bool���жϽ��</returns>
        public static bool IsNullOrEmpty(object oValue)
        {
            if (oValue == null || oValue == System.DBNull.Value || oValue.ToString() == string.Empty)
                return true;
            else
                return false;
        }

        /// <summary>
        /// �ж��Ƿ�ΪEmtpy
        /// </summary>
        /// <param name="oValue">ָ����stringֵ</param>
        /// <returns>����bool���жϽ��</returns>
        public static bool IsEmpty(string strValue)
        {
            if (strValue == string.Empty)
                return true;
            else
                return false;
        }
        #endregion

        #region IsEqual(object1, object2)
        /// <summary>
        /// �ж��Ƿ����
        /// </summary>
        /// <param name="oValue">ָ����object1, object2ֵ</param>
        /// <returns>����bool���жϽ��</returns>
        public static bool IsEqual(object oValue1, object oValue2)
        {
            if (object.Equals(oValue1,oValue2))
                return true;
            if (!IsNullOrEmpty(oValue1) && !IsNullOrEmpty(oValue2))
            {
                if (oValue1.ToString().Equals(oValue2.ToString()))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// �ж��Ƿ����
        /// </summary>
        /// <param name="oValue">ָ����object1, object2ֵ</param>
        /// <returns>����bool���жϽ��</returns>
        public static bool IsEqual(string strValue1, object strValue2)
        {
            if (strValue1.Equals(strValue2))
                return true;
            return false;
        }
        #endregion

        #region IsInt32(object)
        /// <summary>
        /// �ж��Ƿ�Ϊnull
        /// </summary>
        /// <param name="oValue">ָ����objectֵ</param>
        /// <returns>����bool���жϽ��</returns>
        public static bool IsInt32(object oValue)
        {
            int result;
            if (Int32.TryParse(DataConvert.GetString(oValue), out result))
                return true;
            else
                return false;
        }
        #endregion

        #region IsInt64(object)
        /// <summary>
        /// �ж��Ƿ�Ϊnull
        /// </summary>
        /// <param name="oValue">ָ����objectֵ</param>
        /// <returns>����bool���жϽ��</returns>
        public static bool IsInt64(object oValue)
        {
            long result;
            if (Int64.TryParse(DataConvert.GetString(oValue), out result))
                return true;
            else
                return false;
        }
        #endregion

        #region IsDecimal(object)
        /// <summary>
        /// �ж��Ƿ�Ϊnull
        /// </summary>
        /// <param name="oValue">ָ����objectֵ</param>
        /// <returns>����bool���жϽ��</returns>
        public static bool IsDecimal(object oValue)
        {
            decimal result;
            if (Decimal.TryParse(DataConvert.GetString(oValue), out result))
                return true;
            else
                return false;
        }
        #endregion

        #region IsNaturalNumber(object)
        /// <summary>
        /// �ж��Ƿ�Ϊnull
        /// </summary>
        /// <param name="oValue">ָ����objectֵ</param>
        /// <returns>����bool���жϽ��</returns>
        public static bool IsNaturalNumber(object oValue)
        {
            if (IsInt32(oValue) && DataConvert.GetInt32(oValue) >= 0)
                return true;
            else
                return false;
        }
        #endregion

        #region IsPercentNumber(object)
        /// <summary>
        /// �ж��Ƿ�Ϊnull
        /// </summary>
        /// <param name="oValue">ָ����objectֵ</param>
        /// <returns>����bool���жϽ��</returns>
        public static bool IsPercentNumber(object oValue)
        {
            if (IsDecimal(oValue))
            {
                decimal temp = DataConvert.GetDecimal(oValue);
                if (temp >= -100 && temp <= 100)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        #endregion

        #region �Ƿ�EMail���ͣ�XXX@XXX.XXX \w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*��
        /// <summary>
        /// �Ƿ�EMail���ͣ�XXX@XXX.XXX \w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*��
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsEmail(string input)
        {
            ArrayList aryResult = new ArrayList();
            return CommRegularMatch(input, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.None, ref aryResult, false);
        }
        #endregion

        #region �Ƿ�Windows FileName��^[^ \\/:*?""<>|]+([ ]+[^ \\/:*?""<>|]+)*$��
        /// <summary>
        /// Matches         bla, file.txt, !@#$ 
        /// Non-Matches     bla , file:txt, /\*? 
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsWindowsFileName(string input)
        {
            ArrayList aryResult = new ArrayList();
            return CommRegularMatch(input, @"^[^ \\/:*?""<>|]+([ ]+[^ \\/:*?""<>|]+)*$", RegexOptions.None, ref aryResult, false);
        }
        #endregion

        #region ͨ��������ʽ�жϺ���
        /// <summary>
        /// ͨ��������ʽ�жϺ���
        /// </summary>
        /// <param name="strVerifyString">String������ƥ����ַ���</param>
        /// <param name="strRegular">String��������ʽ</param>
        /// <param name="regOption">RegexOptions������������ʽ��ѡ��</param>
        /// <param name="aryResult">ArrayList���ֽ���ַ�������</param>
        /// <param name="IsEntirety">Boolean���Ƿ���Ҫ��ȫƥ��</param>
        /// <returns></returns>
        public static bool CommRegularMatch(string strVerifyString, string strRegular, System.Text.RegularExpressions.RegexOptions regOption, ref System.Collections.ArrayList aryResult, bool IsEntirety)
        {
            System.Text.RegularExpressions.Regex r;
            System.Text.RegularExpressions.Match m;

            #region �����Ҫ��ȫƥ��Ĵ���
            if (IsEntirety)
            {
                strRegular = strRegular.Insert(0, @"\A");
                strRegular = strRegular.Insert(strRegular.Length, @"\z");
            }
            #endregion

            try
            {
                r = new System.Text.RegularExpressions.Regex(strRegular, regOption);
            }
            catch (System.Exception e)
            {
                throw (e);
            }

            for (m = r.Match(strVerifyString); m.Success; m = m.NextMatch())
            {
                aryResult.Add(m);
            }

            if (aryResult.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }
}
