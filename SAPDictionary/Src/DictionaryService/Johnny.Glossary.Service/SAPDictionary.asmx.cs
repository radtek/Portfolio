using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Text;

using System.Data.SQLite;

namespace Johnny.Glossary.Service
{
    /// <summary>
    ///    Summary description for CodeWebService.
    /// </summary>
    public class SAPDictionary : System.Web.Services.WebService
    {
        public SAPDictionary()
        {
            //CODEGEN: This call is required by the ASP.NET Web Services Designer
            InitializeComponent();
        }

        #region Component Designer generated code
        /// <summary>
        ///    Required method for Designer support - do not modify
        ///    the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }
        #endregion

        [WebMethod]
        public string[] FindTheWord(string name, string language)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select Name, Domain, Description from SAPDictionary");
                strSql.Append(" where Name=@name ");
                strSql.Append(" and Language=@language ");
                SQLiteParameter[] parameters = {
                    SQLiteHelper.MakeSQLiteParameter("@name", DbType.String,name),
                    SQLiteHelper.MakeSQLiteParameter("@language", DbType.String,language)};

                DataSet ds = SQLiteHelper.Query(strSql.ToString(), parameters);
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    return new string[0] { };
                else
                {
                    string[] ret = new string[3];
                    ret[0] = DataConvert.GetString(ds.Tables[0].Rows[0][0]);
                    ret[1] = DataConvert.GetString(ds.Tables[0].Rows[0][1]);
                    ret[2] = DataConvert.GetString(ds.Tables[0].Rows[0][2]);
                    return ret;
                }                    
            }
            catch (Exception ex)
            {
                return new string[0] { };
            }
        }

        [WebMethod]
        public string[] FindAllWords(string name, string language, int top)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select Name from SAPDictionary");
                strSql.Append(" where Name like :name");
                strSql.Append(" and Language=@language ");
                SQLiteParameter[] parameters = {
                    SQLiteHelper.MakeSQLiteParameter(":name", DbType.String, string.Format("{0}%", name)),
                    SQLiteHelper.MakeSQLiteParameter("@language", DbType.String,language)};

                DataSet ds = SQLiteHelper.Query(strSql.ToString(), parameters);
                if (ds != null && ds.Tables[0] != null)
                {
                    int count = Math.Min(ds.Tables[0].Rows.Count, top);
                    string[] words = new string[count];
                    for (int ix = 0; ix < count; ix++)
                    {
                        words[ix] = DataConvert.GetString(ds.Tables[0].Rows[ix][0]);
                    }
                    return words;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        [WebMethod]
        public string[] FindWordsByAlphabet(char letter, string language)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select Name from SAPDictionary");
                strSql.Append(" where Name like :name");
                strSql.Append(" and Language=@language ");
                SQLiteParameter[] parameters = {
                    SQLiteHelper.MakeSQLiteParameter(":name", DbType.String, string.Format("{0}%", letter)),
                    SQLiteHelper.MakeSQLiteParameter("@language", DbType.String,language)};

                DataSet ds = SQLiteHelper.Query(strSql.ToString(), parameters);
                if (ds != null && ds.Tables[0] != null)
                {
                    string[] words = new string[ds.Tables[0].Rows.Count];
                    for (int ix = 0; ix < ds.Tables[0].Rows.Count; ix++)
                    {
                        words[ix] = DataConvert.GetString(ds.Tables[0].Rows[ix][0]);
                    }
                    return words;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }        
    }
}
