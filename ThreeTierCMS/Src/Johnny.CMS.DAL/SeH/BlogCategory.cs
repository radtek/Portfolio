using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Johnny.Library.Database;

namespace Johnny.CMS.DAL.SeH
{

    /// <summary>
    /// BlogCategory is a DAL calss that represents seh_blogcategory
    /// </summary>
    public class BlogCategory
    {
        /// <summary>
        /// Method to get recoders with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SeH.BlogCategory> GetList()
        {
            IList<Johnny.CMS.OM.SeH.BlogCategory> list = new List<Johnny.CMS.OM.SeH.BlogCategory>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [BlogCategoryId], [BlogCategoryName], [Sequence] ");
            strSql.Append(" FROM [seh_blogcategory] ");
            strSql.Append(" ORDER BY [Sequence]");

            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString()))
            {
                while (sdr.Read())
                {
                    Johnny.CMS.OM.SeH.BlogCategory item = new Johnny.CMS.OM.SeH.BlogCategory(sdr.GetInt32(0), sdr.GetString(1), sdr.GetInt32(2));
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Method to get one recoder by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SeH.BlogCategory GetModel(int blogcategoryid)
        {
            //Set up a return value
            Johnny.CMS.OM.SeH.BlogCategory model = null;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [BlogCategoryId], [BlogCategoryName], [Sequence] ");
            strSql.Append(" FROM [seh_blogcategory] ");
            strSql.Append(" WHERE [BlogCategoryId]=@blogcategoryid");
            SqlParameter[] parameters = {
					new SqlParameter("@blogcategoryid", SqlDbType.Int,4)};
            parameters[0].Value = blogcategoryid;
            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString(), parameters))
            {
                if (sdr.Read())
                    model = new Johnny.CMS.OM.SeH.BlogCategory(sdr.GetInt32(0), sdr.GetString(1), sdr.GetInt32(2));
                else
                    model = new Johnny.CMS.OM.SeH.BlogCategory();
            }
            return model;
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SeH.BlogCategory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DECLARE @Sequence int");
            strSql.Append(" SELECT @Sequence=(max(Sequence)+1) FROM [seh_blogcategory]");
            strSql.Append(" if @Sequence is NULL");
            strSql.Append(" Set @Sequence=1");
            strSql.Append("INSERT INTO [seh_blogcategory](");
            strSql.Append("[BlogCategoryName],[Sequence]");
            strSql.Append(")");
            strSql.Append(" VALUES (");
            strSql.Append("@blogcategoryname,@sequence");
            strSql.Append(")");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
            		new SqlParameter("@blogcategoryname", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.BlogCategoryName;

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
        public void Update(Johnny.CMS.OM.SeH.BlogCategory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [seh_blogcategory] SET ");
            strSql.Append("[BlogCategoryName]=@blogcategoryname");
            strSql.Append(" WHERE [BlogCategoryId]=@blogcategoryid ");
            SqlParameter[] parameters = {
            		new SqlParameter("@blogcategoryid", SqlDbType.Int,4),
					new SqlParameter("@blogcategoryname", SqlDbType.NVarChar,50),
			};
            parameters[0].Value = model.BlogCategoryId;
            parameters[1].Value = model.BlogCategoryName;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int blogcategoryid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM [seh_blogcategory] WHERE [BlogCategoryId]=@blogcategoryid");
            SqlParameter[] parameters = {
					new SqlParameter("@blogcategoryid", SqlDbType.Int,4)};
            parameters[0].Value = blogcategoryid;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int blogcategoryid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM [seh_blogcategory] WHERE [BlogCategoryId]=@blogcategoryid");
            SqlParameter[] parameters = {
					new SqlParameter("@blogcategoryid", SqlDbType.Int,4)};
            parameters[0].Value = blogcategoryid;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
    }
}
