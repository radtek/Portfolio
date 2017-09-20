using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Johnny.Library.Database;

namespace Johnny.CMS.DAL.SystemInfo
{

    /// <summary>
    /// Topmainmenu is a DAL calss that represents cms_topmenubinding
    /// </summary>
    public class TopMenuBinding
    {
        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SystemInfo.TopMenuBinding> GetList()
        {
            IList<Johnny.CMS.OM.SystemInfo.TopMenuBinding> list = new List<Johnny.CMS.OM.SystemInfo.TopMenuBinding>();

            StringBuilder strSql = new StringBuilder();
            //strSql.Append("SELECT [TopMenuId], [MenuCategoryId] ");
            //strSql.Append(" FROM [cms_topmenubinding] ");
            //strSql.Append(" ORDER BY [Sequence]");
            strSql.Append(" SELECT [cms_topmenubinding].[TopMenuId],[cms_topmenu].[TopMenuName],[cms_topmenubinding].[MenuCategoryId],[cms_menucategory].[MenuCategoryName]");
            strSql.Append(" FROM [cms_topmenubinding]");
            strSql.Append(" LEFT OUTER JOIN [cms_topmenu]");
            strSql.Append(" ON [cms_topmenubinding].[TopMenuId] = [cms_topmenu].[TopMenuId]");
            strSql.Append(" LEFT OUTER JOIN [cms_menucategory]");
            strSql.Append(" ON [cms_topmenubinding].[MenuCategoryId] = [cms_menucategory].[MenuCategoryId]");

            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString()))
            {
                while (sdr.Read())
                {
                    //Johnny.CMS.OM.SystemInfo.TopMainMenu item = new Johnny.CMS.OM.SystemInfo.TopMainMenu(sdr.GetInt32(0), sdr.GetInt32(1));
                    Johnny.CMS.OM.SystemInfo.TopMenuBinding item = new Johnny.CMS.OM.SystemInfo.TopMenuBinding(sdr.GetInt32(0), sdr.GetString(1), sdr.GetInt32(2), sdr.GetString(3));
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SystemInfo.TopMenuBinding> GetList(int topmenuid)
        {
            IList<Johnny.CMS.OM.SystemInfo.TopMenuBinding> list = new List<Johnny.CMS.OM.SystemInfo.TopMenuBinding>();

            StringBuilder strSql = new StringBuilder();
            //strSql.Append("SELECT [TopMenuId], [MenuCategoryId] ");
            //strSql.Append(" FROM [cms_topmenubinding] ");
            //strSql.Append(" ORDER BY [Sequence]");
            strSql.Append(" SELECT [cms_topmenubinding].[TopMenuId],[cms_topmenu].[TopMenuName],[cms_topmenubinding].[MenuCategoryId],[cms_menucategory].[MenuCategoryName]");
            strSql.Append(" FROM [cms_topmenubinding]");
            strSql.Append(" LEFT OUTER JOIN [cms_topmenu]");
            strSql.Append(" ON [cms_topmenubinding].[TopMenuId] = [cms_topmenu].[TopMenuId]");
            strSql.Append(" LEFT OUTER JOIN [cms_menucategory]");
            strSql.Append(" ON [cms_topmenubinding].[MenuCategoryId] = [cms_menucategory].[MenuCategoryId]");
            strSql.Append(" WHERE [cms_topmenubinding].[TopMenuId]=@topmenuid");
            SqlParameter[] parameters = {
					new SqlParameter("@topmenuid", SqlDbType.Int,4)};
            parameters[0].Value = topmenuid;
            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString(), parameters))
            {
                while (sdr.Read())
                {
                    //Johnny.CMS.OM.SystemInfo.TopMainMenu item = new Johnny.CMS.OM.SystemInfo.TopMainMenu(sdr.GetInt32(0), sdr.GetInt32(1));
                    Johnny.CMS.OM.SystemInfo.TopMenuBinding item = new Johnny.CMS.OM.SystemInfo.TopMenuBinding(sdr.GetInt32(0), sdr.GetString(1), sdr.GetInt32(2), sdr.GetString(3));
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SystemInfo.TopMenuBinding GetModel(int topmenuid)
        {
            //Set up a return value
            Johnny.CMS.OM.SystemInfo.TopMenuBinding model = null;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [TopMenuId], [MenuCategoryId] ");
            strSql.Append(" FROM [cms_topmenubinding] ");
            strSql.Append(" WHERE [TopMenuId]=@topmenuid");
            SqlParameter[] parameters = {
					new SqlParameter("@topmenuid", SqlDbType.Int,4)};
            parameters[0].Value = topmenuid;
            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString(), parameters))
            {
                if (sdr.Read())
                    model = new Johnny.CMS.OM.SystemInfo.TopMenuBinding(sdr.GetInt32(0), sdr.GetInt32(1));
                else
                    model = new Johnny.CMS.OM.SystemInfo.TopMenuBinding();
            }
            return model;
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SystemInfo.TopMenuBinding model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO [cms_topmenubinding](");
            strSql.Append("[TopMenuId],[MenuCategoryId]");
            strSql.Append(")");
            strSql.Append(" VALUES (");
            strSql.Append("@topmenuid,");
            strSql.Append("@menucategoryid");
            strSql.Append(")");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@topmenuid", SqlDbType.Int,4),
            		new SqlParameter("@menucategoryid", SqlDbType.Int,4)};
            parameters[0].Value = model.TopMenuId;
            parameters[1].Value = model.MenuCategoryId;

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
        public void Update(Johnny.CMS.OM.SystemInfo.TopMenuBinding model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [cms_topmenubinding] SET ");
            strSql.Append("[MenuCategoryId]=@menucategoryid");
            strSql.Append(" WHERE [TopMenuId]=@topmenuid ");
            SqlParameter[] parameters = {
                    new SqlParameter("@topmenuid", SqlDbType.Int,4),
                    new SqlParameter("@menucategoryid", SqlDbType.Int,4)};
            parameters[0].Value = model.TopMenuId;
            parameters[1].Value = model.MenuCategoryId;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int topmenuid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM [cms_topmenubinding] WHERE [TopMenuId]=@topmenuid");
            SqlParameter[] parameters = {
					new SqlParameter("@topmenuid", SqlDbType.Int,4)};
            parameters[0].Value = topmenuid;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int topmenuid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM [cms_topmenubinding] WHERE [TopMenuId]=@topmenuid");
            SqlParameter[] parameters = {
					new SqlParameter("@topmenuid", SqlDbType.Int,4)};
            parameters[0].Value = topmenuid;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
    }
}
