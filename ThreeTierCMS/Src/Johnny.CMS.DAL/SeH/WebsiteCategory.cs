using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Johnny.Library.Database;

namespace Johnny.CMS.DAL.SeH
{

    /// <summary>
    /// WebsiteCategory is a DAL calss that represents seh_WebsiteCategory
    /// </summary>
    public class WebsiteCategory
    {
        /// <summary>
        /// Method to get recoders with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SeH.WebsiteCategory> GetList()
        {
            IList<Johnny.CMS.OM.SeH.WebsiteCategory> list = new List<Johnny.CMS.OM.SeH.WebsiteCategory>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [WebsiteCategoryId], [WebsiteCategoryName], [Sequence] ");
            strSql.Append(" FROM [seh_websitecategory] ");
            strSql.Append(" ORDER BY [Sequence]");

            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString()))
            {
                while (sdr.Read())
                {
                    Johnny.CMS.OM.SeH.WebsiteCategory item = new Johnny.CMS.OM.SeH.WebsiteCategory(sdr.GetInt32(0), sdr.GetString(1), sdr.GetInt32(2));
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Method to get one recoder by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SeH.WebsiteCategory GetModel(int websitecategoryid)
        {
            //Set up a return value
            Johnny.CMS.OM.SeH.WebsiteCategory model = null;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [WebsiteCategoryId], [WebsiteCategoryName], [Sequence] ");
            strSql.Append(" FROM [seh_websitecategory] ");
            strSql.Append(" WHERE [WebsiteCategoryId]=@websitecategoryid");
            SqlParameter[] parameters = {
					new SqlParameter("@websitecategoryid", SqlDbType.Int,4)};
            parameters[0].Value = websitecategoryid;
            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString(), parameters))
            {
                if (sdr.Read())
                    model = new Johnny.CMS.OM.SeH.WebsiteCategory(sdr.GetInt32(0), sdr.GetString(1), sdr.GetInt32(2));
                else
                    model = new Johnny.CMS.OM.SeH.WebsiteCategory();
            }
            return model;
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SeH.WebsiteCategory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DECLARE @Sequence int");
            strSql.Append(" SELECT @Sequence=(max(Sequence)+1) FROM [seh_websitecategory]");
            strSql.Append(" if @Sequence is NULL");
            strSql.Append(" Set @Sequence=1");
            strSql.Append("INSERT INTO [seh_websitecategory](");
            strSql.Append("[WebsiteCategoryName],[Sequence]");
            strSql.Append(")");
            strSql.Append(" VALUES (");
            strSql.Append("@websitecategoryname,@sequence");
            strSql.Append(")");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
            		new SqlParameter("@websitecategoryname", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.WebsiteCategoryName;

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
        public void Update(Johnny.CMS.OM.SeH.WebsiteCategory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [seh_websitecategory] SET ");
            strSql.Append("[WebsiteCategoryName]=@websitecategoryname");
            strSql.Append(" WHERE [WebsiteCategoryId]=@websitecategoryid ");
            SqlParameter[] parameters = {
            		new SqlParameter("@websitecategoryid", SqlDbType.Int,4),
					new SqlParameter("@websitecategoryname", SqlDbType.NVarChar,50),
			};
            parameters[0].Value = model.WebsiteCategoryId;
            parameters[1].Value = model.WebsiteCategoryName;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int websitecategoryid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM [seh_websitecategory] WHERE [WebsiteCategoryId]=@websitecategoryid");
            SqlParameter[] parameters = {
					new SqlParameter("@websitecategoryid", SqlDbType.Int,4)};
            parameters[0].Value = websitecategoryid;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int websitecategoryid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM [seh_websitecategory] WHERE [WebsiteCategoryId]=@websitecategoryid");
            SqlParameter[] parameters = {
					new SqlParameter("@websitecategoryid", SqlDbType.Int,4)};
            parameters[0].Value = websitecategoryid;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
    }
}
