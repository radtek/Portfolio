using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Johnny.Library.Database;

namespace Johnny.CMS.DAL.SystemInfo
{

    /// <summary>
    /// Menucategory is a DAL calss that represents cms_menucategory
    /// </summary>
    public class MenuCategory
    {
        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SystemInfo.MenuCategory> GetList()
        {
            IList<Johnny.CMS.OM.SystemInfo.MenuCategory> list = new List<Johnny.CMS.OM.SystemInfo.MenuCategory>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [MenuCategoryId], [MenuCategoryName], [Sequence] ");
            strSql.Append(" FROM [cms_menucategory] ");
            strSql.Append(" ORDER BY [Sequence]");

            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString()))
            {
                while (sdr.Read())
                {
                    Johnny.CMS.OM.SystemInfo.MenuCategory item = new Johnny.CMS.OM.SystemInfo.MenuCategory(sdr.GetInt32(0), sdr.GetString(1), sdr.GetInt32(2));
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SystemInfo.MenuCategory GetModel(int menucategoryid)
        {
            //Set up a return value
            Johnny.CMS.OM.SystemInfo.MenuCategory model = null;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [MenuCategoryId], [MenuCategoryName], [Sequence] ");
            strSql.Append(" FROM [cms_menucategory] ");
            strSql.Append(" WHERE [MenuCategoryId]=@menucategoryid");
            SqlParameter[] parameters = {
					new SqlParameter("@menucategoryid", SqlDbType.Int,4)};
            parameters[0].Value = menucategoryid;
            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString(), parameters))
            {
                if (sdr.Read())
                    model = new Johnny.CMS.OM.SystemInfo.MenuCategory(sdr.GetInt32(0), sdr.GetString(1), sdr.GetInt32(2));
                else
                    model = new Johnny.CMS.OM.SystemInfo.MenuCategory();
            }
            return model;
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SystemInfo.MenuCategory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DECLARE @Sequence int");
            strSql.Append(" SELECT @Sequence=(max(Sequence)+1) FROM [cms_menucategory]");
            strSql.Append(" if @Sequence is NULL");
            strSql.Append(" Set @Sequence=1");
            strSql.Append("INSERT INTO [cms_menucategory](");
            strSql.Append("[MenuCategoryName],[Sequence]");
            strSql.Append(")");
            strSql.Append(" VALUES (");
            strSql.Append("@menucategoryname,@Sequence");
            strSql.Append(")");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
            		new SqlParameter("@menucategoryname", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.MenuCategoryName;

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
        public void Update(Johnny.CMS.OM.SystemInfo.MenuCategory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [cms_menucategory] SET ");
            strSql.Append("[MenuCategoryName]=@menucategoryname");
            strSql.Append(" WHERE [MenuCategoryId]=@menucategoryid ");
            SqlParameter[] parameters = {
            		new SqlParameter("@menucategoryid", SqlDbType.Int,4),
					new SqlParameter("@menucategoryname", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.MenuCategoryId;
            parameters[1].Value = model.MenuCategoryName;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int menucategoryid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM [cms_menucategory] WHERE [MenuCategoryId]=@menucategoryid");
            SqlParameter[] parameters = {
					new SqlParameter("@menucategoryid", SqlDbType.Int,4)};
            parameters[0].Value = menucategoryid;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int menucategoryid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM [cms_menucategory] WHERE [MenuCategoryId]=@menucategoryid");
            SqlParameter[] parameters = {
					new SqlParameter("@menucategoryid", SqlDbType.Int,4)};
            parameters[0].Value = menucategoryid;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
    }
}
