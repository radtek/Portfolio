using System;
using System.Collections.Generic;
using System.Text;

using System.Web;

namespace Johnny.Kaixin.Helper
{
    public class DataConvert
    {
        #region GetString(object)
        public static string GetString(object oValue)
        {
            if (oValue == null || oValue == System.DBNull.Value)
                return "";            
            return oValue.ToString();
        }
        #endregion

        #region GetInt32(object)
        public static int GetInt32(object oValue)
        {            
            if (oValue is Enum)
            {
                return Convert.ToInt32(oValue);
            }
                
            int result;
            if (Int32.TryParse(GetString(oValue), out result))
                return result;
            else
                return 0;
        }
        #endregion

        #region GetInt64(object)
        public static long GetInt64(object oValue)
        {
            if (oValue is Enum)
            {
                return Convert.ToInt64(oValue);
            }

            long result;
            if (Int64.TryParse(GetString(oValue), out result))
                return result;
            else
                return 0;
        }
        #endregion

        #region GetDouble(object)
        public static double GetDouble(object oValue)
        {
            double result;
            if (Double.TryParse(GetString(oValue), out result))
                return result;
            else
                return 0;
        }
        #endregion

        #region GetDecimal(object)
        public static decimal GetDecimal(object oValue)
        {
            decimal result;
            if (Decimal.TryParse(GetString(oValue), out result))
                return result;
            else
                return 0;
        }
        #endregion

        #region GetBool(object)
        public static bool GetBool(object oValue)
        {
            bool result;
            Boolean.TryParse(GetString(oValue), out result);
            return result;            
        }
        #endregion

        #region GetEncodeData(string)
        public static string GetEncodeData(string str)
        {
            return HttpUtility.UrlEncode(str);           
        }
        #endregion

        public static class RemoveItems<T>
        {
            public delegate bool Decide(T item);
            public static void Execute(ICollection<T> collection, Decide decide)
            {
                List<T> removed = new List<T>();
                foreach (T item in collection)
                {
                    if (decide(item))
                        removed.Add(item);
                }

                foreach (T item in removed)
                {
                    collection.Remove(item);
                }
                removed.Clear();
            }
        } 
    }
}
