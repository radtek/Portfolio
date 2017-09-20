using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Johnny.Library.Database;

namespace Johnny.CMS.DAL.Access
{

    /// <summary>
    /// AdminLoginLog is a DAL calss that represents cms_adminloginlog
    /// </summary>
    public class AdminLoginLog
    {
        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.Access.AdminLoginLog> GetList()
        {
            IList<Johnny.CMS.OM.Access.AdminLoginLog> list = new List<Johnny.CMS.OM.Access.AdminLoginLog>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [Id], [Name], [Password], [LoginTime], [LogoutTime], [LoginIP], [HosterName], [LoginStatus] ");
            strSql.Append(" FROM [cms_adminloginlog] ");
            strSql.Append(" ORDER BY [Sequence]");

            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString()))
            {
                while (sdr.Read())
                {
                    Johnny.CMS.OM.Access.AdminLoginLog item = new Johnny.CMS.OM.Access.AdminLoginLog(sdr.GetInt32(0), sdr.GetString(1), sdr.GetString(2), sdr.GetDateTime(3), sdr.GetDateTime(4), sdr.GetString(5), sdr.GetString(6), sdr.GetString(7));
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.Access.AdminLoginLog GetModel(int id)
        {
            //Set up a return value
            Johnny.CMS.OM.Access.AdminLoginLog model = null;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [Id], [Name], [Password], [LoginTime], [LogoutTime], [LoginIP], [HosterName], [LoginStatus] ");
            strSql.Append(" FROM [cms_adminloginlog] ");
            strSql.Append(" WHERE [Id]=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString(), parameters))
            {
                if (sdr.Read())
                    model = new Johnny.CMS.OM.Access.AdminLoginLog(sdr.GetInt32(0), sdr.GetString(1), sdr.GetString(2), sdr.GetDateTime(3), sdr.GetDateTime(4), sdr.GetString(5), sdr.GetString(6), sdr.GetString(7));
                else
                    model = new Johnny.CMS.OM.Access.AdminLoginLog();
            }
            return model;
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.Access.AdminLoginLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DECLARE @Sequence int");
            strSql.Append(" SELECT @Sequence=(max(Sequence)+1) FROM [cms_adminloginlog]");
            strSql.Append(" if @Sequence is NULL");
            strSql.Append(" Set @Sequence=1");
            strSql.Append("INSERT INTO [cms_adminloginlog](");
            strSql.Append("[Name],[Password],[LoginTime],[LogoutTime],[LoginIP],[HosterName],[LoginStatus]");
            strSql.Append(")");
            strSql.Append(" VALUES (");
            strSql.Append("@name,@password,@logintime,@logouttime,@loginip,@hostername,@loginstatus");
            strSql.Append(")");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
            		new SqlParameter("@name", SqlDbType.VarChar,50),
					new SqlParameter("@password", SqlDbType.VarChar,32),
					new SqlParameter("@logintime", SqlDbType.DateTime),
					new SqlParameter("@logouttime", SqlDbType.DateTime),
					new SqlParameter("@loginip", SqlDbType.VarChar,50),
					new SqlParameter("@hostername", SqlDbType.VarChar,100),
					new SqlParameter("@loginstatus", SqlDbType.NVarChar,4000)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Password;
            parameters[2].Value = model.LoginTime;
            parameters[3].Value = model.LogoutTime;
            parameters[4].Value = model.LoginIP;
            parameters[5].Value = model.HosterName;
            parameters[6].Value = model.LoginStatus;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void Update(Johnny.CMS.OM.Access.AdminLoginLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [cms_adminloginlog] SET ");
            strSql.Append("[Name]=@name,");
            strSql.Append("[Password]=@password,");
            strSql.Append("[LoginTime]=@logintime,");
            strSql.Append("[LogoutTime]=@logouttime,");
            strSql.Append("[LoginIP]=@loginip,");
            strSql.Append("[HosterName]=@hostername,");
            strSql.Append("[LoginStatus]=@loginstatus");
            strSql.Append(" WHERE [Id]=@id ");
            SqlParameter[] parameters = {
            		new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@name", SqlDbType.VarChar,50),
					new SqlParameter("@password", SqlDbType.VarChar,32),
					new SqlParameter("@logintime", SqlDbType.DateTime),
					new SqlParameter("@logouttime", SqlDbType.DateTime),
					new SqlParameter("@loginip", SqlDbType.VarChar,50),
					new SqlParameter("@hostername", SqlDbType.VarChar,100),
					new SqlParameter("@loginstatus", SqlDbType.NVarChar,4000)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Password;
            parameters[3].Value = model.LoginTime;
            parameters[4].Value = model.LogoutTime;
            parameters[5].Value = model.LoginIP;
            parameters[6].Value = model.HosterName;
            parameters[7].Value = model.LoginStatus;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM [cms_adminloginlog] WHERE [Id]=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM [cms_adminloginlog] WHERE [Id]=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
    }
}
