using System;
using System.Collections.Generic;
using System.Web;

namespace Johnny.Glossary.Service
{
    public class DataConvert
    {
        public static string GetString(object oValue)
        {
            if (oValue == null || oValue == System.DBNull.Value)
                return "";
            return oValue.ToString();
        }
    }
}