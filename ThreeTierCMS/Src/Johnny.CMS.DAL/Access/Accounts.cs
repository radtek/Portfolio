using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using Johnny.CMS.OM;
using Johnny.Library.Database;

namespace Johnny.CMS.DAL.Access
{

    /// <summary>
    /// Permission is a DAL calss that represents account business
    /// </summary>
    public class Accounts
    {
        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public ArrayList GetUserPermission(string name)
        {
            ArrayList permission = new ArrayList();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT [cms_rolepermission].[PermissionId]");
            strSql.Append(" FROM [cms_rolepermission]");
            strSql.Append(" LEFT OUTER JOIN [cms_adminrole]");
            strSql.Append(" ON [cms_rolepermission].[RoleId] = [cms_adminrole].[RoleId]");
            strSql.Append(" LEFT OUTER JOIN [cms_administrator]");
            strSql.Append(" ON [cms_adminrole].[AdminId] = [cms_administrator].[AdminId]");
            strSql.Append(" WHERE [AdminName] = @adminname");
            SqlParameter[] parameters = {
					new SqlParameter("@adminname", SqlDbType.VarChar,50)};
            parameters[0].Value = name;
            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString(), parameters))
            {
                while (sdr.Read())
                {
                    permission.Add(sdr.GetInt32(0));
                }
            }
            return permission;
        }        
    }
}
