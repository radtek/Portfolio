using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Johnny.Library.Database;

namespace Johnny.CMS.DAL.Access
{

    /// <summary>
    /// Administrator is a DAL calss that represents cms_administrator
    /// </summary>
    public class Administrator
    {
        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.Access.Administrator> GetList()
        {
            IList<Johnny.CMS.OM.Access.Administrator> list = new List<Johnny.CMS.OM.Access.Administrator>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [AdminId], [AdminName], [Password], [FullName], [Gender], [Tel], [Email], [ValidFrom], [ValidTo], [IsActivated], [LoginTimes], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence] ");
            strSql.Append(" FROM [cms_administrator] ");
            strSql.Append(" ORDER BY [Sequence]");

            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString()))
            {
                while (sdr.Read())
                {
                    Johnny.CMS.OM.Access.Administrator item = new Johnny.CMS.OM.Access.Administrator(sdr.GetInt32(0), sdr.GetString(1), sdr.GetString(2), sdr.GetString(3), sdr.GetBoolean(4), sdr.GetString(5), sdr.GetString(6), sdr.GetDateTime(7), sdr.GetDateTime(8), sdr.GetBoolean(9), sdr.GetInt32(10), sdr.GetDateTime(11), sdr.GetInt32(12), sdr.GetString(13), sdr.GetDateTime(14), sdr.GetInt32(15), sdr.GetString(16), sdr.GetInt32(17));
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.Access.Administrator GetModel(int adminid)
        {
            //Set up a return value
            Johnny.CMS.OM.Access.Administrator model = null;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [AdminId], [AdminName], [Password], [FullName], [Gender], [Tel], [Email], [ValidFrom], [ValidTo], [IsActivated], [LoginTimes], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence] ");
            strSql.Append(" FROM [cms_administrator] ");
            strSql.Append(" WHERE [AdminId]=@adminid");
            SqlParameter[] parameters = {
					new SqlParameter("@adminid", SqlDbType.Int,4)};
            parameters[0].Value = adminid;
            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString(), parameters))
            {
                if (sdr.Read())
                    model = new Johnny.CMS.OM.Access.Administrator(sdr.GetInt32(0), sdr.GetString(1), sdr.GetString(2), sdr.GetString(3), sdr.GetBoolean(4), sdr.GetString(5), sdr.GetString(6), sdr.GetDateTime(7), sdr.GetDateTime(8), sdr.GetBoolean(9), sdr.GetInt32(10), sdr.GetDateTime(11), sdr.GetInt32(12), sdr.GetString(13), sdr.GetDateTime(14), sdr.GetInt32(15), sdr.GetString(16), sdr.GetInt32(17));
                else
                    model = new Johnny.CMS.OM.Access.Administrator();
            }
            return model;
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.Access.Administrator model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DECLARE @Sequence int");
            strSql.Append(" SELECT @Sequence=(max(Sequence)+1) FROM [cms_administrator]");
            strSql.Append(" if @Sequence is NULL");
            strSql.Append(" Set @Sequence=1");
            strSql.Append("INSERT INTO [cms_administrator](");
            strSql.Append("[AdminName],[Password],[FullName],[Gender],[Tel],[Email],[ValidFrom],[ValidTo],[IsActivated],[LoginTimes],[CreatedTime],[CreatedById],[CreatedByName],[UpdatedTime],[UpdatedById],[UpdatedByName],[Sequence]");
            strSql.Append(")");
            strSql.Append(" VALUES (");
            strSql.Append("@adminname,@password,@fullname,@gender,@tel,@email,@validfrom,@validto,@isactivated,@logintimes,@createdtime,@createdbyid,@createdbyname,@updatedtime,@updatedbyid,@updatedbyname,@sequence");
            strSql.Append(")");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
            		new SqlParameter("@adminname", SqlDbType.VarChar,50),
					new SqlParameter("@password", SqlDbType.VarChar,32),
					new SqlParameter("@fullname", SqlDbType.NVarChar,50),
					new SqlParameter("@gender", SqlDbType.Bit),
					new SqlParameter("@tel", SqlDbType.VarChar,50),
					new SqlParameter("@email", SqlDbType.VarChar,50),
					new SqlParameter("@validfrom", SqlDbType.DateTime),
					new SqlParameter("@validto", SqlDbType.DateTime),
					new SqlParameter("@isactivated", SqlDbType.Bit),
					new SqlParameter("@logintimes", SqlDbType.Int,4),
					new SqlParameter("@createdtime", SqlDbType.DateTime),
					new SqlParameter("@createdbyid", SqlDbType.Int,4),
					new SqlParameter("@createdbyname", SqlDbType.VarChar,50),
					new SqlParameter("@updatedtime", SqlDbType.DateTime),
					new SqlParameter("@updatedbyid", SqlDbType.Int,4),
					new SqlParameter("@updatedbyname", SqlDbType.VarChar,50)};
            parameters[0].Value = model.AdminName;
            parameters[1].Value = model.Password;
            parameters[2].Value = model.FullName;
            parameters[3].Value = model.Gender;
            parameters[4].Value = model.Tel;
            parameters[5].Value = model.Email;
            parameters[6].Value = model.ValidFrom;
            parameters[7].Value = model.ValidTo;
            parameters[8].Value = model.IsActivated;
            parameters[9].Value = model.LoginTimes;
            parameters[10].Value = model.CreatedTime;
            parameters[11].Value = model.CreatedById;
            parameters[12].Value = model.CreatedByName;
            parameters[13].Value = model.UpdatedTime;
            parameters[14].Value = model.UpdatedById;
            parameters[15].Value = model.UpdatedByName;

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
        public void Update(Johnny.CMS.OM.Access.Administrator model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [cms_administrator] SET ");
            strSql.Append("[AdminName]=@adminname,");
            //strSql.Append("[Password]=@password,");
            strSql.Append("[FullName]=@fullname,");
            strSql.Append("[Gender]=@gender,");
            strSql.Append("[Tel]=@tel,");
            strSql.Append("[Email]=@email,");
            strSql.Append("[ValidFrom]=@validfrom,");
            strSql.Append("[ValidTo]=@validto,");
            strSql.Append("[IsActivated]=@isactivated,");
            //strSql.Append("[LoginTimes]=@logintimes,");
            //strSql.Append("[CreatedTime]=@createdtime,");
            //strSql.Append("[CreatedById]=@createdbyid,");
            //strSql.Append("[CreatedByName]=@createdbyname,");
            strSql.Append("[UpdatedTime]=@updatedtime,");
            strSql.Append("[UpdatedById]=@updatedbyid,");
            strSql.Append("[UpdatedByName]=@updatedbyname");
            strSql.Append(" WHERE [AdminId]=@adminid ");
            SqlParameter[] parameters = {
            		new SqlParameter("@adminid", SqlDbType.Int,4),
					new SqlParameter("@adminname", SqlDbType.VarChar,50),
					//new SqlParameter("@password", SqlDbType.VarChar,32),
					new SqlParameter("@fullname", SqlDbType.NVarChar,50),
					new SqlParameter("@gender", SqlDbType.Bit),
					new SqlParameter("@tel", SqlDbType.VarChar,50),
					new SqlParameter("@email", SqlDbType.VarChar,50),
					new SqlParameter("@validfrom", SqlDbType.DateTime),
					new SqlParameter("@validto", SqlDbType.DateTime),
					new SqlParameter("@isactivated", SqlDbType.Bit),
					//new SqlParameter("@logintimes", SqlDbType.Int,4),
					//new SqlParameter("@createdtime", SqlDbType.DateTime),
					//new SqlParameter("@createdbyid", SqlDbType.Int,4),
					//new SqlParameter("@createdbyname", SqlDbType.VarChar,50),
					new SqlParameter("@updatedtime", SqlDbType.DateTime),
					new SqlParameter("@updatedbyid", SqlDbType.Int,4),
					new SqlParameter("@updatedbyname", SqlDbType.VarChar,50),};
            parameters[0].Value = model.AdminId;
            parameters[1].Value = model.AdminName;
            //parameters[2].Value = model.Password;
            parameters[2].Value = model.FullName;
            parameters[3].Value = model.Gender;
            parameters[4].Value = model.Tel;
            parameters[5].Value = model.Email;
            parameters[6].Value = model.ValidFrom;
            parameters[7].Value = model.ValidTo;
            parameters[8].Value = model.IsActivated;
            //parameters[10].Value = model.LoginTimes;
            //parameters[11].Value = model.CreatedTime;
            //parameters[12].Value = model.CreatedById;
            //parameters[13].Value = model.CreatedByName;
            parameters[9].Value = model.UpdatedTime;
            parameters[10].Value = model.UpdatedById;
            parameters[11].Value = model.UpdatedByName;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void UpdatePersonal(Johnny.CMS.OM.Access.Administrator model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [cms_administrator] SET ");
            //strSql.Append("[AdminName]=@adminname,");
            //strSql.Append("[Password]=@password,");
            strSql.Append("[FullName]=@fullname,");
            strSql.Append("[Gender]=@gender,");
            strSql.Append("[Tel]=@tel,");
            strSql.Append("[Email]=@email,");
            //strSql.Append("[ValidFrom]=@validfrom,");
            //strSql.Append("[ValidTo]=@validto,");
            //strSql.Append("[IsActivated]=@isactivated,");
            //strSql.Append("[LoginTimes]=@logintimes,");
            //strSql.Append("[CreatedTime]=@createdtime,");
            //strSql.Append("[CreatedById]=@createdbyid,");
            //strSql.Append("[CreatedByName]=@createdbyname,");
            strSql.Append("[UpdatedTime]=@updatedtime,");
            strSql.Append("[UpdatedById]=@updatedbyid,");
            strSql.Append("[UpdatedByName]=@updatedbyname");
            strSql.Append(" WHERE [AdminId]=@adminid ");
            SqlParameter[] parameters = {
            		new SqlParameter("@adminid", SqlDbType.Int,4),
                    //new SqlParameter("@adminname", SqlDbType.VarChar,50),
					//new SqlParameter("@password", SqlDbType.VarChar,32),
					new SqlParameter("@fullname", SqlDbType.NVarChar,50),
					new SqlParameter("@gender", SqlDbType.Bit),
					new SqlParameter("@tel", SqlDbType.VarChar,50),
					new SqlParameter("@email", SqlDbType.VarChar,50),
                    //new SqlParameter("@validfrom", SqlDbType.DateTime),
					//new SqlParameter("@validto", SqlDbType.DateTime),
					//new SqlParameter("@isactivated", SqlDbType.Bit),
					//new SqlParameter("@logintimes", SqlDbType.Int,4),
					//new SqlParameter("@createdtime", SqlDbType.DateTime),
					//new SqlParameter("@createdbyid", SqlDbType.Int,4),
					//new SqlParameter("@createdbyname", SqlDbType.VarChar,50),
					new SqlParameter("@updatedtime", SqlDbType.DateTime),
					new SqlParameter("@updatedbyid", SqlDbType.Int,4),
					new SqlParameter("@updatedbyname", SqlDbType.VarChar,50)};
            parameters[0].Value = model.AdminId;
            //parameters[1].Value = model.AdminName;
            //parameters[2].Value = model.Password;
            parameters[1].Value = model.FullName;
            parameters[2].Value = model.Gender;
            parameters[3].Value = model.Tel;
            parameters[4].Value = model.Email;
            //parameters[7].Value = model.ValidFrom;
            //parameters[8].Value = model.ValidTo;
            //parameters[9].Value = model.IsActivated;
            //parameters[10].Value = model.LoginTimes;
            //parameters[11].Value = model.CreatedTime;
            //parameters[12].Value = model.CreatedById;
            //parameters[13].Value = model.CreatedByName;
            parameters[5].Value = model.UpdatedTime;
            parameters[6].Value = model.UpdatedById;
            parameters[7].Value = model.UpdatedByName;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int adminid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM [cms_administrator] WHERE [AdminId]=@adminid");
            SqlParameter[] parameters = {
					new SqlParameter("@adminid", SqlDbType.Int,4)};
            parameters[0].Value = adminid;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int adminid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM [cms_administrator] WHERE [AdminId]=@adminid");
            SqlParameter[] parameters = {
					new SqlParameter("@adminid", SqlDbType.Int,4)};
            parameters[0].Value = adminid;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Check exist by name
        /// </summary>
        public bool IsExist(string adminname)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM [cms_administrator] WHERE [Name]=@adminname");
            SqlParameter[] parameters = {
					new SqlParameter("@adminname", SqlDbType.VarChar,50)};
            parameters[0].Value = adminname;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adminname"></param>
        /// <returns></returns>
        public int GetUserIdByName(string adminname)
        {
            int iUser = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [AdminId] FROM [cms_administrator] WHERE [AdminName]=@adminname");
            SqlParameter[] parameters = {
					new SqlParameter("@adminname", SqlDbType.VarChar,50)};
            parameters[0].Value = adminname;
            using (SqlDataReader rdr = DbHelperSQL.ExecuteReader(strSql.ToString(), parameters))
            {
                if (rdr.Read())
                    iUser = rdr.GetInt32(0);
            }
            return iUser;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckLogin(string adminname, string password)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM [cms_administrator]");
            strSql.Append(" WHERE [AdminName]=@adminname");
            strSql.Append(" AND [Password]=@password");
            SqlParameter[] parameters = {
					new SqlParameter("@adminname", SqlDbType.VarChar,50),
					new SqlParameter("@password", SqlDbType.VarChar,32)};
            parameters[0].Value = adminname;
            parameters[1].Value = password;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public void UpdateLoginTimes(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [cms_administrator] SET [LoginTimes] = [LoginTimes] + 1");
            strSql.Append(" WHERE [adminname]=@adminname");
            SqlParameter[] parameters = {
					new SqlParameter("@adminname", SqlDbType.VarChar,50)};
            parameters[0].Value = name;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void ResetPassword(Johnny.CMS.OM.Access.Administrator model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [cms_administrator] SET ");
            strSql.Append("[Password]=@password,");
            strSql.Append("[UpdatedTime]=@updatedtime,");
            strSql.Append("[UpdatedById]=@updatedbyid,");
            strSql.Append("[UpdatedByName]=@updatedbyname");
            strSql.Append(" WHERE [AdminId]=@adminid ");
            SqlParameter[] parameters = {
            		new SqlParameter("@adminid", SqlDbType.Int,4),
					new SqlParameter("@adminname", SqlDbType.VarChar,50),
					new SqlParameter("@password", SqlDbType.VarChar,32),
					new SqlParameter("@updatedtime", SqlDbType.DateTime),
					new SqlParameter("@updatedbyid", SqlDbType.Int,4),
					new SqlParameter("@updatedbyname", SqlDbType.VarChar,50)};
            parameters[0].Value = model.AdminId;
            parameters[1].Value = model.AdminName;
            parameters[2].Value = model.Password;
            parameters[3].Value = model.UpdatedTime;
            parameters[4].Value = model.UpdatedById;
            parameters[5].Value = model.UpdatedByName;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
    }
}
