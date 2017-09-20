using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Johnny.Library.Database;

namespace Johnny.CMS.DAL.SeH
{

    /// <summary>
    /// Article is a DAL calss that represents seh_article
    /// </summary>
    public class Article
    {
        /// <summary>
        /// Method to get recoders with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SeH.Article> GetList()
        {
            IList<Johnny.CMS.OM.SeH.Article> list = new List<Johnny.CMS.OM.SeH.Article>();
            
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("SELECT [ArticleId], [ChannelId], [Title], [SubTitle], [KeyWord], [SubContent], [Content], [FirstImage], [CopyFrom], [Hits], [IsTop], [IsElite], [IsPic], [IsPageType], [IsVerified], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence] ");
            //strSql.Append(" FROM [seh_article] ");
            //strSql.Append(" ORDER BY [Sequence]");

            strSql.Append("SELECT [ArticleId], [seh_article].[ChannelId], [seh_channel].[ChannelName], [Title], [SubTitle], [KeyWord], [SubContent], [Content], [FirstImage], [CopyFrom], [Hits], [IsTop], [IsElite], [IsPic], [IsPageType], [IsVerified], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [seh_article].[Sequence] ");
            strSql.Append(" FROM [seh_article] ");
            strSql.Append(" LEFT OUTER JOIN [seh_channel] ");
            strSql.Append(" ON [seh_article].[ChannelId] = [seh_channel].[ChannelId] ");
            strSql.Append(" ORDER BY [seh_article].[Sequence] DESC");

            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString()))
            {
                while (sdr.Read())
                {
                    Johnny.CMS.OM.SeH.Article item = new Johnny.CMS.OM.SeH.Article(sdr.GetInt64(0), sdr.GetInt32(1), sdr.GetString(2), sdr.GetString(3), sdr.GetString(4), sdr.GetString(5), sdr.GetString(6), sdr.GetString(7), sdr.GetString(8), sdr.GetString(9), sdr.GetInt32(10), sdr.GetBoolean(11), sdr.GetBoolean(12), sdr.GetBoolean(13), sdr.GetBoolean(14), sdr.GetBoolean(15), sdr.GetDateTime(16), sdr.GetInt32(17), sdr.GetString(18), sdr.GetDateTime(19), sdr.GetInt32(20), sdr.GetString(21), sdr.GetInt32(22));
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Method to get recoders with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SeH.Article> GetList(int channelid, int? top)
        {
            IList<Johnny.CMS.OM.SeH.Article> list = new List<Johnny.CMS.OM.SeH.Article>();

            StringBuilder strSql = new StringBuilder();
            if (top != null)
                strSql.Append(string.Format("SELECT TOP {0} [ArticleId], [seh_article].[ChannelId], [seh_channel].[ChannelName], [Title], [SubTitle], [KeyWord], [SubContent], [Content], [FirstImage], [CopyFrom], [Hits], [IsTop], [IsElite], [IsPic], [IsPageType], [IsVerified], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [seh_article].[Sequence] ", top));
            else
                strSql.Append("SELECT [ArticleId], [seh_article].[ChannelId], [seh_channel].[ChannelName], [Title], [SubTitle], [KeyWord], [SubContent], [Content], [FirstImage], [CopyFrom], [Hits], [IsTop], [IsElite], [IsPic], [IsPageType], [IsVerified], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [seh_article].[Sequence] ");
            strSql.Append(" FROM [seh_article] ");
            strSql.Append(" LEFT OUTER JOIN [seh_channel] ");
            strSql.Append(" ON [seh_article].[ChannelId] = [seh_channel].[ChannelId] ");
            strSql.Append(" WHERE [seh_article].[ChannelId]=@channelid");
            strSql.Append(" ORDER BY [seh_article].[Sequence]");
            SqlParameter[] parameters = {
					new SqlParameter("@channelid", SqlDbType.Int,4)};
            parameters[0].Value = channelid;
            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString(), parameters))
            {
                while (sdr.Read())
                {
                    Johnny.CMS.OM.SeH.Article item = new Johnny.CMS.OM.SeH.Article(sdr.GetInt64(0), sdr.GetInt32(1), sdr.GetString(2), sdr.GetString(3), sdr.GetString(4), sdr.GetString(5), sdr.GetString(6), sdr.GetString(7), sdr.GetString(8), sdr.GetString(9), sdr.GetInt32(10), sdr.GetBoolean(11), sdr.GetBoolean(12), sdr.GetBoolean(13), sdr.GetBoolean(14), sdr.GetBoolean(15), sdr.GetDateTime(16), sdr.GetInt32(17), sdr.GetString(18), sdr.GetDateTime(19), sdr.GetInt32(20), sdr.GetString(21), sdr.GetInt32(22));
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Method to get one recoder by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SeH.Article GetModel(int articleid)
        {
            //Set up a return value
            Johnny.CMS.OM.SeH.Article model = null;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [ArticleId], [ChannelId], [Title], [SubTitle], [KeyWord], [SubContent], [Content], [FirstImage], [CopyFrom], [Hits], [IsTop], [IsElite], [IsPic], [IsPageType], [IsVerified], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence] ");
            strSql.Append(" FROM [seh_article] ");
            strSql.Append(" WHERE [ArticleId]=@articleid");
            SqlParameter[] parameters = {
					new SqlParameter("@articleid", SqlDbType.BigInt,8)};
            parameters[0].Value = articleid;
            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString(), parameters))
            {
                if (sdr.Read())
                    model = new Johnny.CMS.OM.SeH.Article(sdr.GetInt64(0), sdr.GetInt32(1), sdr.GetString(2), sdr.GetString(3), sdr.GetString(4), sdr.GetString(5), sdr.GetString(6), sdr.GetString(7), sdr.GetString(8), sdr.GetInt32(9), sdr.GetBoolean(10), sdr.GetBoolean(11), sdr.GetBoolean(12), sdr.GetBoolean(13), sdr.GetBoolean(14), sdr.GetDateTime(15), sdr.GetInt32(16), sdr.GetString(17), sdr.GetDateTime(18), sdr.GetInt32(19), sdr.GetString(20), sdr.GetInt32(21));
                else
                    model = new Johnny.CMS.OM.SeH.Article();
            }
            return model;
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SeH.Article model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DECLARE @Sequence int");
            strSql.Append(" SELECT @Sequence=(max(Sequence)+1) FROM [seh_article]");
            strSql.Append(" if @Sequence is NULL");
            strSql.Append(" Set @Sequence=1");
            strSql.Append("INSERT INTO [seh_article](");
            strSql.Append("[ChannelId],[Title],[SubTitle],[KeyWord],[SubContent],[Content],[FirstImage],[CopyFrom],[Hits],[IsTop],[IsElite],[IsPic],[IsPageType],[IsVerified],[CreatedTime],[CreatedById],[CreatedByName],[UpdatedTime],[UpdatedById],[UpdatedByName],[Sequence]");
            strSql.Append(")");
            strSql.Append(" VALUES (");
            strSql.Append("@channelid,@title,@subtitle,@keyword,@subcontent,@content,@firstimage,@copyfrom,@hits,@istop,@iselite,@ispic,@ispagetype,@isverified,@createdtime,@createdbyid,@createdbyname,@updatedtime,@updatedbyid,@updatedbyname,@Sequence");
            strSql.Append(")");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
            		new SqlParameter("@channelid", SqlDbType.Int,4),
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@subtitle", SqlDbType.NVarChar,200),
					new SqlParameter("@keyword", SqlDbType.NVarChar,100),
					new SqlParameter("@subcontent", SqlDbType.VarChar,500),
					new SqlParameter("@content", SqlDbType.Text),
					new SqlParameter("@firstimage", SqlDbType.VarChar,500),
					new SqlParameter("@copyfrom", SqlDbType.VarChar,500),
					new SqlParameter("@hits", SqlDbType.Int,4),
					new SqlParameter("@istop", SqlDbType.Bit),
					new SqlParameter("@iselite", SqlDbType.Bit),
					new SqlParameter("@ispic", SqlDbType.Bit),
					new SqlParameter("@ispagetype", SqlDbType.Bit),
					new SqlParameter("@isverified", SqlDbType.Bit),
					new SqlParameter("@createdtime", SqlDbType.DateTime),
					new SqlParameter("@createdbyid", SqlDbType.Int,4),
					new SqlParameter("@createdbyname", SqlDbType.VarChar,50),
					new SqlParameter("@updatedtime", SqlDbType.DateTime),
					new SqlParameter("@updatedbyid", SqlDbType.Int,4),
					new SqlParameter("@updatedbyname", SqlDbType.VarChar,50)};
            parameters[0].Value = model.ChannelId;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.SubTitle;
            parameters[3].Value = model.KeyWord;
            parameters[4].Value = model.SubContent;
            parameters[5].Value = model.Content;
            parameters[6].Value = model.FirstImage;
            parameters[7].Value = model.CopyFrom;
            parameters[8].Value = model.Hits;
            parameters[9].Value = model.IsTop;
            parameters[10].Value = model.IsElite;
            parameters[11].Value = model.IsPic;
            parameters[12].Value = model.IsPageType;
            parameters[13].Value = model.IsVerified;
            parameters[14].Value = model.CreatedTime;
            parameters[15].Value = model.CreatedById;
            parameters[16].Value = model.CreatedByName;
            parameters[17].Value = model.UpdatedTime;
            parameters[18].Value = model.UpdatedById;
            parameters[19].Value = model.UpdatedByName;

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
        public void Update(Johnny.CMS.OM.SeH.Article model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [seh_article] SET ");
            strSql.Append("[ChannelId]=@channelid,");
            strSql.Append("[Title]=@title,");
            strSql.Append("[SubTitle]=@subtitle,");
            strSql.Append("[KeyWord]=@keyword,");
            strSql.Append("[SubContent]=@subcontent,");
            strSql.Append("[Content]=@content,");
            strSql.Append("[FirstImage]=@firstimage,");
            strSql.Append("[CopyFrom]=@copyfrom,");
            strSql.Append("[Hits]=@hits,");
            strSql.Append("[IsTop]=@istop,");
            strSql.Append("[IsElite]=@iselite,");
            strSql.Append("[IsPic]=@ispic,");
            strSql.Append("[IsPageType]=@ispagetype,");
            strSql.Append("[IsVerified]=@isverified,");
            //strSql.Append("[CreateTime]=@createtime,");
            //strSql.Append("[CreatorId]=@creatorid,");
            //strSql.Append("[CreatorName]=@creatorname,");
            strSql.Append("[UpdatedTime]=@updatedtime,");
            strSql.Append("[UpdatedById]=@updatedbyid,");
            strSql.Append("[UpdatedByName]=@updatedbyname,");
            strSql.Append(" WHERE [ArticleId]=@articleid ");
            SqlParameter[] parameters = {
            		new SqlParameter("@articleid", SqlDbType.BigInt,8),
					new SqlParameter("@channelid", SqlDbType.Int,4),
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@subtitle", SqlDbType.NVarChar,200),
					new SqlParameter("@keyword", SqlDbType.NVarChar,100),
					new SqlParameter("@subcontent", SqlDbType.VarChar,500),
					new SqlParameter("@content", SqlDbType.Text),
					new SqlParameter("@firstimage", SqlDbType.VarChar,500),
					new SqlParameter("@copyfrom", SqlDbType.VarChar,500),
					new SqlParameter("@hits", SqlDbType.Int,4),
					new SqlParameter("@istop", SqlDbType.Bit),
					new SqlParameter("@iselite", SqlDbType.Bit),
					new SqlParameter("@ispic", SqlDbType.Bit),
					new SqlParameter("@ispagetype", SqlDbType.Bit),
					new SqlParameter("@isverified", SqlDbType.Bit),
                    //new SqlParameter("@createtime", SqlDbType.DateTime),
                    //new SqlParameter("@creatorid", SqlDbType.Int,4),
                    //new SqlParameter("@creatorname", SqlDbType.VarChar,50),
					new SqlParameter("@updatedtime", SqlDbType.DateTime),
					new SqlParameter("@updatedbyid", SqlDbType.Int,4),
					new SqlParameter("@updatedbyname", SqlDbType.VarChar,50),
			};
            parameters[0].Value = model.ArticleId;
            parameters[1].Value = model.ChannelId;
            parameters[2].Value = model.Title;
            parameters[3].Value = model.SubTitle;
            parameters[4].Value = model.KeyWord;
            parameters[5].Value = model.SubContent;
            parameters[6].Value = model.Content;
            parameters[7].Value = model.FirstImage;
            parameters[8].Value = model.CopyFrom;
            parameters[9].Value = model.Hits;
            parameters[10].Value = model.IsTop;
            parameters[11].Value = model.IsElite;
            parameters[12].Value = model.IsPic;
            parameters[13].Value = model.IsPageType;
            parameters[14].Value = model.IsVerified;
            //parameters[15].Value = model.CreateTime;
            //parameters[16].Value = model.CreatorId;
            //parameters[17].Value = model.CreatorName;
            parameters[15].Value = model.UpdatedTime;
            parameters[16].Value = model.UpdatedById;
            parameters[17].Value = model.UpdatedByName;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int articleid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM [seh_article] WHERE [ArticleId]=@articleid");
            SqlParameter[] parameters = {
					new SqlParameter("@articleid", SqlDbType.BigInt,8)};
            parameters[0].Value = articleid;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int articleid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM [seh_article] WHERE [ArticleId]=@articleid");
            SqlParameter[] parameters = {
					new SqlParameter("@articleid", SqlDbType.BigInt,8)};
            parameters[0].Value = articleid;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
    }
}
