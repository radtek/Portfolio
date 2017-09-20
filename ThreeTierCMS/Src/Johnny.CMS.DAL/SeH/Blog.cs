using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Johnny.Library.Database;

namespace Johnny.CMS.DAL.SeH
{

    /// <summary>
    /// Blog is a DAL calss that represents seh_blog
    /// </summary>
    public class Blog
    {
        /// <summary>
        /// Method to get recoders with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SeH.Blog> GetList()
        {
            IList<Johnny.CMS.OM.SeH.Blog> list = new List<Johnny.CMS.OM.SeH.Blog>();

            StringBuilder strSql = new StringBuilder();
            //strSql.Append("SELECT [BlogId], [Title], [BlogCategoryId], [Tag], [Content], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence] ");
            //strSql.Append(" FROM [seh_blog] ");
            //strSql.Append(" ORDER BY [Sequence]");

            strSql.Append("SELECT [BlogId], [Title], [seh_blog].[BlogCategoryId], [seh_blogcategory].[BlogCategoryName], [Tag], [Content], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [seh_blog].[Sequence] ");
            strSql.Append(" FROM [seh_blog] ");
            strSql.Append(" LEFT OUTER JOIN [seh_blogcategory] ");
            strSql.Append(" ON [seh_blog].[BlogCategoryId] = [seh_blogcategory].[BlogCategoryId] ");
            strSql.Append(" ORDER BY [seh_blog].[Sequence] DESC");

            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString()))
            {
                while (sdr.Read())
                {
                    Johnny.CMS.OM.SeH.Blog item = new Johnny.CMS.OM.SeH.Blog(sdr.GetInt32(0), sdr.GetString(1), sdr.GetInt32(2), sdr.GetString(3), sdr.GetString(4), sdr.GetString(5), sdr.GetInt32(6), sdr.GetBoolean(7), sdr.GetDateTime(8), sdr.GetInt32(9), sdr.GetString(10), sdr.GetDateTime(11), sdr.GetInt32(12), sdr.GetString(13), sdr.GetInt32(14));
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Method to get one recoder by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SeH.Blog GetModel(int blogid)
        {
            //Set up a return value
            Johnny.CMS.OM.SeH.Blog model = null;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [BlogId], [Title], [BlogCategoryId], [Tag], [Content], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence] ");
            strSql.Append(" FROM [seh_blog] ");
            strSql.Append(" WHERE [BlogId]=@blogid");
            SqlParameter[] parameters = {
					new SqlParameter("@blogid", SqlDbType.Int,4)};
            parameters[0].Value = blogid;
            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString(), parameters))
            {
                if (sdr.Read())
                    model = new Johnny.CMS.OM.SeH.Blog(sdr.GetInt32(0), sdr.GetString(1), sdr.GetInt32(2), sdr.GetString(3), sdr.GetString(4), sdr.GetInt32(5), sdr.GetBoolean(6), sdr.GetDateTime(7), sdr.GetInt32(8), sdr.GetString(9), sdr.GetDateTime(10), sdr.GetInt32(11), sdr.GetString(12), sdr.GetInt32(13));
                else
                    model = new Johnny.CMS.OM.SeH.Blog();
            }
            return model;
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SeH.Blog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DECLARE @Sequence int");
            strSql.Append(" SELECT @Sequence=(max(Sequence)+1) FROM [seh_blog]");
            strSql.Append(" if @Sequence is NULL");
            strSql.Append(" Set @Sequence=1");
            strSql.Append("INSERT INTO [seh_blog](");
            strSql.Append("[Title],[BlogCategoryId],[Tag],[Content],[Hits],[IsDisplay],[CreatedTime],[CreatedById],[CreatedByName],[UpdatedTime],[UpdatedById],[UpdatedByName],[Sequence]");
            strSql.Append(")");
            strSql.Append(" VALUES (");
            strSql.Append("@title,@blogcategoryid,@tag,@content,@hits,@isdisplay,@createdtime,@createdbyid,@createdbyname,@updatedtime,@updatedbyid,@updatedbyname,@sequence");
            strSql.Append(")");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
            		new SqlParameter("@title", SqlDbType.NVarChar,255),
					new SqlParameter("@blogcategoryid", SqlDbType.Int,4),
					new SqlParameter("@tag", SqlDbType.VarChar,100),
					new SqlParameter("@content", SqlDbType.Text),
					new SqlParameter("@hits", SqlDbType.Int,4),
					new SqlParameter("@isdisplay", SqlDbType.Bit),
					new SqlParameter("@createdtime", SqlDbType.DateTime),
					new SqlParameter("@createdbyid", SqlDbType.Int,4),
					new SqlParameter("@createdbyname", SqlDbType.VarChar,50),
					new SqlParameter("@updatedtime", SqlDbType.DateTime),
					new SqlParameter("@updatedbyid", SqlDbType.Int,4),
					new SqlParameter("@updatedbyname", SqlDbType.VarChar,50)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.BlogCategoryId;
            parameters[2].Value = model.Tag;
            parameters[3].Value = model.Content;
            parameters[4].Value = model.Hits;
            parameters[5].Value = model.IsDisplay;
            parameters[6].Value = model.CreatedTime;
            parameters[7].Value = model.CreatedById;
            parameters[8].Value = model.CreatedByName;
            parameters[9].Value = model.UpdatedTime;
            parameters[10].Value = model.UpdatedById;
            parameters[11].Value = model.UpdatedByName;

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
        public void Update(Johnny.CMS.OM.SeH.Blog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [seh_blog] SET ");
            strSql.Append("[Title]=@title,");
            strSql.Append("[BlogCategoryId]=@blogcategoryid,");
            strSql.Append("[Tag]=@tag,");
            strSql.Append("[Content]=@content,");
            strSql.Append("[Hits]=@hits,");
            strSql.Append("[IsDisplay]=@isdisplay,");
            //strSql.Append("[CreatedTime]=@createdtime,");
            //strSql.Append("[CreatedById]=@createdbyid,");
            //strSql.Append("[CreatedByName]=@createdbyname,");
            strSql.Append("[UpdatedTime]=@updatedtime,");
            strSql.Append("[UpdatedById]=@updatedbyid,");
            strSql.Append("[UpdatedByName]=@updatedbyname,");
            strSql.Append(" WHERE [BlogId]=@blogid ");
            SqlParameter[] parameters = {
            		new SqlParameter("@blogid", SqlDbType.Int,4),
					new SqlParameter("@title", SqlDbType.NVarChar,255),
					new SqlParameter("@blogcategoryid", SqlDbType.Int,4),
					new SqlParameter("@tag", SqlDbType.VarChar,100),
					new SqlParameter("@content", SqlDbType.Text),
					new SqlParameter("@hits", SqlDbType.Int,4),
					new SqlParameter("@isdisplay", SqlDbType.Bit),
					//new SqlParameter("@createdtime", SqlDbType.DateTime),
					//new SqlParameter("@createdbyid", SqlDbType.Int,4),
					//new SqlParameter("@createdbyname", SqlDbType.VarChar,50),
					new SqlParameter("@updatedtime", SqlDbType.DateTime),
					new SqlParameter("@updatedbyid", SqlDbType.Int,4),
					new SqlParameter("@updatedbyname", SqlDbType.VarChar,50),
			};
            parameters[0].Value = model.BlogId;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.BlogCategoryId;
            parameters[3].Value = model.Tag;
            parameters[4].Value = model.Content;
            parameters[5].Value = model.Hits;
            parameters[6].Value = model.IsDisplay;
            //parameters[7].Value = model.CreatedTime;
            //parameters[8].Value = model.CreatedById;
            //parameters[9].Value = model.CreatedByName;
            parameters[7].Value = model.UpdatedTime;
            parameters[8].Value = model.UpdatedById;
            parameters[9].Value = model.UpdatedByName;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int blogid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM [seh_blog] WHERE [BlogId]=@blogid");
            SqlParameter[] parameters = {
					new SqlParameter("@blogid", SqlDbType.Int,4)};
            parameters[0].Value = blogid;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int blogid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM [seh_blog] WHERE [BlogId]=@blogid");
            SqlParameter[] parameters = {
					new SqlParameter("@blogid", SqlDbType.Int,4)};
            parameters[0].Value = blogid;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
    }
}
