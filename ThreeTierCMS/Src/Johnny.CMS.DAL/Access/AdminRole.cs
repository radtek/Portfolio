using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Johnny.Library.Database;

namespace Johnny.CMS.DAL.Access
{

    /// <summary>
    /// Adminrole is a DAL calss that represents cms_adminrole
    /// </summary>
    public class AdminRole
    {
        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.Access.AdminRole> GetList()
        {
            IList<Johnny.CMS.OM.Access.AdminRole> list = new List<Johnny.CMS.OM.Access.AdminRole>();

            StringBuilder strSql = new StringBuilder();
            //strSql.Append("SELECT [AdminRoleId], [AdminId], [RoleId], [Sequence] ");
            //strSql.Append(" FROM [cms_adminrole] ");
            //strSql.Append(" ORDER BY [Sequence]");
            strSql.Append("SELECT mtb.[AdminRoleId],mtb.[AdminId],admins.[AdminName],mtb.[RoleId],roles.[RoleName],mtb.[Sequence]");
            strSql.Append(" FROM [cms_adminrole] mtb");
            strSql.Append(" LEFT OUTER JOIN [cms_administrator] admins");
            strSql.Append(" ON mtb.[AdminId] = admins.[AdminId]");
            strSql.Append(" LEFT OUTER JOIN [cms_role] roles");
            strSql.Append(" ON mtb.[RoleId] = roles.[RoleId]");
            strSql.Append(" ORDER BY mtb.[Sequence]");

            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString()))
            {
                while (sdr.Read())
                {
                    Johnny.CMS.OM.Access.AdminRole item = new Johnny.CMS.OM.Access.AdminRole(sdr.GetInt32(0), sdr.GetInt32(1), sdr.GetString(2), sdr.GetInt32(3), sdr.GetString(4), sdr.GetInt32(5));
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.Access.AdminRole GetModel(int adminroleid)
        {
            //Set up a return value
            Johnny.CMS.OM.Access.AdminRole model = null;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [AdminRoleId], [AdminId], [RoleId], [Sequence] ");
            strSql.Append(" FROM [cms_adminrole] ");
            strSql.Append(" WHERE [AdminRoleId]=@adminroleid");
            SqlParameter[] parameters = {
					new SqlParameter("@adminroleid", SqlDbType.Int,4)};
            parameters[0].Value = adminroleid;
            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString(), parameters))
            {
                if (sdr.Read())
                    model = new Johnny.CMS.OM.Access.AdminRole(sdr.GetInt32(0), sdr.GetInt32(1), sdr.GetInt32(2), sdr.GetInt32(3));
                else
                    model = new Johnny.CMS.OM.Access.AdminRole();
            }
            return model;
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.Access.AdminRole model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DECLARE @Sequence int");
            strSql.Append(" SELECT @Sequence=(max(Sequence)+1) FROM [cms_adminrole]");
            strSql.Append(" if @Sequence is NULL");
            strSql.Append(" Set @Sequence=1");
            strSql.Append("INSERT INTO [cms_adminrole](");
            strSql.Append("[AdminId],[RoleId],[Sequence]");
            strSql.Append(")");
            strSql.Append(" VALUES (");
            strSql.Append("@adminid,@roleid,@Sequence");
            strSql.Append(")");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
            		new SqlParameter("@adminid", SqlDbType.Int,4),
					new SqlParameter("@roleid", SqlDbType.Int,4)};
            parameters[0].Value = model.AdminId;
            parameters[1].Value = model.RoleId;

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
        public void Update(Johnny.CMS.OM.Access.AdminRole model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [cms_adminrole] SET ");
            strSql.Append("[AdminId]=@adminid,");
            strSql.Append("[RoleId]=@roleid");
            strSql.Append(" WHERE [AdminRoleId]=@adminroleid ");
            SqlParameter[] parameters = {
            		new SqlParameter("@adminroleid", SqlDbType.Int,4),
					new SqlParameter("@adminid", SqlDbType.Int,4),
					new SqlParameter("@roleid", SqlDbType.Int,4)};
            parameters[0].Value = model.AdminRoleId;
            parameters[1].Value = model.AdminId;
            parameters[2].Value = model.RoleId;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int adminroleid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM [cms_adminrole] WHERE [AdminRoleId]=@adminroleid");
            SqlParameter[] parameters = {
					new SqlParameter("@adminroleid", SqlDbType.Int,4)};
            parameters[0].Value = adminroleid;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int adminroleid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM [cms_adminrole] WHERE [AdminRoleId]=@adminroleid");
            SqlParameter[] parameters = {
					new SqlParameter("@adminroleid", SqlDbType.Int,4)};
            parameters[0].Value = adminroleid;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
    }
}
