using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Johnny.Library.Database;

namespace Johnny.CMS.DAL.Access
{

    /// <summary>
    /// Rolepermission is a DAL calss that represents cms_rolepermission
    /// </summary>
    public class RolePermission
    {
        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.Access.RolePermission> GetList()
        {
            IList<Johnny.CMS.OM.Access.RolePermission> list = new List<Johnny.CMS.OM.Access.RolePermission>();

            StringBuilder strSql = new StringBuilder();
            //strSql.Append("SELECT [RoleId], [PermissionId] ");
            //strSql.Append(" FROM [cms_rolepermission] ");
            //strSql.Append(" ORDER BY [Sequence]");
            strSql.Append("SELECT mtb.[RoleId], roles.[RoleName], mtb.[PermissionId], permissions.[PermissionName]");
            strSql.Append(" FROM [cms_rolepermission] mtb");
            strSql.Append(" LEFT OUTER JOIN [cms_role] roles");
            strSql.Append(" ON mtb.[RoleId]=roles.[RoleId]");
            strSql.Append(" LEFT OUTER JOIN [cms_permission] permissions");
            strSql.Append(" ON mtb.[PermissionId] = permissions.[PermissionId]");

            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString()))
            {
                while (sdr.Read())
                {
                    Johnny.CMS.OM.Access.RolePermission item = new Johnny.CMS.OM.Access.RolePermission(sdr.GetInt32(0), sdr.GetString(1), sdr.GetInt32(2), sdr.GetString(3));
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.Access.RolePermission> GetList(int roleid, int permissioncategoryid)
        {
            IList<Johnny.CMS.OM.Access.RolePermission> list = new List<Johnny.CMS.OM.Access.RolePermission>();

            StringBuilder strSql = new StringBuilder();
            //strSql.Append("SELECT [RoleId], [PermissionId] ");
            //strSql.Append(" FROM [cms_rolepermission] ");
            //strSql.Append(" ORDER BY [Sequence]");
            strSql.Append("SELECT mtb.[RoleId], roles.[RoleName], mtb.[PermissionId], permissions.[PermissionName]");
            strSql.Append(" FROM [cms_rolepermission] mtb");
            strSql.Append(" LEFT OUTER JOIN [cms_role] roles");
            strSql.Append(" ON mtb.[RoleId]=roles.[RoleId]");
            strSql.Append(" LEFT OUTER JOIN [cms_permission] permissions");
            strSql.Append(" ON mtb.[PermissionId] = permissions.[PermissionId]");
            strSql.Append(" LEFT OUTER JOIN [cms_permissioncategory] category");
            strSql.Append(" ON permissions.[PermissionCategoryId] = category.[PermissionCategoryId]");
            strSql.Append(" WHERE mtb.[RoleId]=@roleid");
            strSql.Append(" AND category.[PermissionCategoryId]=@permissioncategoryid");
            SqlParameter[] parameters = {
					new SqlParameter("@roleid", SqlDbType.Int,4),
                    new SqlParameter("@permissioncategoryid", SqlDbType.Int,4)};
            parameters[0].Value = roleid;
            parameters[1].Value = permissioncategoryid;

            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString(), parameters))
            {
                while (sdr.Read())
                {
                    Johnny.CMS.OM.Access.RolePermission item = new Johnny.CMS.OM.Access.RolePermission(sdr.GetInt32(0), sdr.GetString(1), sdr.GetInt32(2), sdr.GetString(3));
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.Access.RolePermission GetModel(int roleid)
        {
            //Set up a return value
            Johnny.CMS.OM.Access.RolePermission model = null;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [RoleId], [PermissionId] ");
            strSql.Append(" FROM [cms_rolepermission] ");
            strSql.Append(" WHERE [RoleId]=@roleid");
            SqlParameter[] parameters = {
					new SqlParameter("@roleid", SqlDbType.Int,4)};
            parameters[0].Value = roleid;
            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString(), parameters))
            {
                if (sdr.Read())
                    model = new Johnny.CMS.OM.Access.RolePermission(sdr.GetInt32(0), sdr.GetInt32(1));
                else
                    model = new Johnny.CMS.OM.Access.RolePermission();
            }
            return model;
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.Access.RolePermission model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO [cms_rolepermission](");
            strSql.Append("[RoleId],[PermissionId]");
            strSql.Append(")");
            strSql.Append(" VALUES (");
            strSql.Append("@roleid,");
            strSql.Append("@permissionid");
            strSql.Append(")");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@roleid", SqlDbType.Int,4),
            		new SqlParameter("@permissionid", SqlDbType.Int,4)};
            parameters[0].Value = model.RoleId;
            parameters[1].Value = model.PermissionId;

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
        public void Update(Johnny.CMS.OM.Access.RolePermission model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [cms_rolepermission] SET ");
            strSql.Append("[PermissionId]=@permissionid");
            strSql.Append(" WHERE [RoleId]=@roleid ");
            SqlParameter[] parameters = {
            		new SqlParameter("@roleid", SqlDbType.Int,4),
					new SqlParameter("@permissionid", SqlDbType.Int,4)};
            parameters[0].Value = model.RoleId;
            parameters[1].Value = model.PermissionId;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int roleid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM [cms_rolepermission] WHERE [RoleId]=@roleid");
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
            strSql.Append("SELECT COUNT(1) FROM [cms_rolepermission] WHERE [RoleId]=@roleid");
            SqlParameter[] parameters = {
					new SqlParameter("@roleid", SqlDbType.Int,4)};
            parameters[0].Value = roleid;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
    }
}
