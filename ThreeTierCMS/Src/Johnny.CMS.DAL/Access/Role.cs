using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Johnny.Library.Database;

namespace Johnny.CMS.DAL.Access
{

    /// <summary>
    /// Role is a DAL calss that represents cms_role
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.Access.Role> GetList()
        {
            IList<Johnny.CMS.OM.Access.Role> list = new List<Johnny.CMS.OM.Access.Role>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [RoleId], [RoleName], [Description], [Sequence] ");
            strSql.Append(" FROM [cms_role] ");
            strSql.Append(" ORDER BY [Sequence]");

            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString()))
            {
                while (sdr.Read())
                {
                    Johnny.CMS.OM.Access.Role item = new Johnny.CMS.OM.Access.Role(sdr.GetInt32(0), sdr.GetString(1), sdr.GetString(2), sdr.GetInt32(3));
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.Access.Role GetModel(int roleid)
        {
            //Set up a return value
            Johnny.CMS.OM.Access.Role model = null;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [RoleId], [RoleName], [Description], [Sequence] ");
            strSql.Append(" FROM [cms_role] ");
            strSql.Append(" WHERE [RoleId]=@roleid");
            SqlParameter[] parameters = {
					new SqlParameter("@roleid", SqlDbType.Int,4)};
            parameters[0].Value = roleid;
            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString(), parameters))
            {
                if (sdr.Read())
                    model = new Johnny.CMS.OM.Access.Role(sdr.GetInt32(0), sdr.GetString(1), sdr.GetString(2), sdr.GetInt32(3));
                else
                    model = new Johnny.CMS.OM.Access.Role();
            }
            return model;
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.Access.Role model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DECLARE @Sequence int");
            strSql.Append(" SELECT @Sequence=(max(Sequence)+1) FROM [cms_role]");
            strSql.Append(" if @Sequence is NULL");
            strSql.Append(" Set @Sequence=1");
            strSql.Append("INSERT INTO [cms_role](");
            strSql.Append("[RoleName],[Description],[Sequence]");
            strSql.Append(")");
            strSql.Append(" VALUES (");
            strSql.Append("@rolename,@description,@Sequence");
            strSql.Append(")");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
            		new SqlParameter("@rolename", SqlDbType.NVarChar,50),
					new SqlParameter("@description", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.RoleName;
            parameters[1].Value = model.Description;

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
        public void Update(Johnny.CMS.OM.Access.Role model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [cms_role] SET ");
            strSql.Append("[RoleName]=@rolename,");
            strSql.Append("[Description]=@description");
            strSql.Append(" WHERE [RoleId]=@roleid ");
            SqlParameter[] parameters = {
            		new SqlParameter("@roleid", SqlDbType.Int,4),
					new SqlParameter("@rolename", SqlDbType.NVarChar,50),
					new SqlParameter("@description", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.RoleId;
            parameters[1].Value = model.RoleName;
            parameters[2].Value = model.Description;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int roleid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM [cms_role] WHERE [RoleId]=@roleid");
            SqlParameter[] parameters = {
					new SqlParameter("@roleid", SqlDbType.Int,4)};
            parameters[0].Value = roleid;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int roleid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM [cms_role] WHERE [RoleId]=@roleid");
            SqlParameter[] parameters = {
					new SqlParameter("@roleid", SqlDbType.Int,4)};
            parameters[0].Value = roleid;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
    }
}
