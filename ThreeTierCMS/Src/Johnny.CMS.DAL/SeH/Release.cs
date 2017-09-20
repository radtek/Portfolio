using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Johnny.Library.Database;

namespace Johnny.CMS.DAL.SeH
{

    /// <summary>
    /// Release is a DAL calss that represents seh_release
    /// </summary>
    public class Release
    {
        /// <summary>
        /// Method to get recoders with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SeH.Release> GetList()
        {
            IList<Johnny.CMS.OM.SeH.Release> list = new List<Johnny.CMS.OM.SeH.Release>();

            StringBuilder strSql = new StringBuilder();
            //strSql.Append("SELECT [ReleaseId], [SoftwareId], [ReleaseName], [ReleaseDate], [Description], [Hits], [Downloads], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence] ");
            //strSql.Append(" FROM [seh_release] ");
            //strSql.Append(" ORDER BY [Sequence]");

            strSql.Append("SELECT [ReleaseId], [seh_release].[SoftwareId], [seh_software].[SoftwareName], [ReleaseName], [ReleaseDate], [seh_release].[Description], [seh_release].[Hits], [seh_release].[Downloads], [seh_release].[IsDisplay], [seh_release].[CreatedTime], [seh_release].[CreatedById], [seh_release].[CreatedByName], [seh_release].[UpdatedTime], [seh_release].[UpdatedById], [seh_release].[UpdatedByName], [seh_release].[Sequence] ");
            strSql.Append(" FROM [seh_release] ");
            strSql.Append(" LEFT OUTER JOIN [seh_software] ");
            strSql.Append(" ON [seh_release].[SoftwareId] = [seh_software].[SoftwareId] ");
            strSql.Append(" ORDER BY [seh_release].[Sequence] DESC");
            
            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString()))
            {
                while (sdr.Read())
                {
                    Johnny.CMS.OM.SeH.Release item = new Johnny.CMS.OM.SeH.Release(sdr.GetInt32(0), sdr.GetInt32(1), sdr.GetString(2), sdr.GetString(3), sdr.GetDateTime(4), sdr.GetString(5), sdr.GetInt32(6), sdr.GetInt32(7), sdr.GetBoolean(8), sdr.GetDateTime(9), sdr.GetInt32(10), sdr.GetString(11), sdr.GetDateTime(12), sdr.GetInt32(13), sdr.GetString(14), sdr.GetInt32(15));
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Method to get recoders with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SeH.Release> GetList(int softwareid)
        {
            IList<Johnny.CMS.OM.SeH.Release> list = new List<Johnny.CMS.OM.SeH.Release>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [ReleaseId], [seh_release].[SoftwareId], [seh_software].[SoftwareName], [ReleaseName], [ReleaseDate], [seh_release].[Description], [seh_release].[Hits], [seh_release].[Downloads], [seh_release].[IsDisplay], [seh_release].[CreatedTime], [seh_release].[CreatedById], [seh_release].[CreatedByName], [seh_release].[UpdatedTime], [seh_release].[UpdatedById], [seh_release].[UpdatedByName], [seh_release].[Sequence] ");
            strSql.Append(" FROM [seh_release] ");
            strSql.Append(" LEFT OUTER JOIN [seh_software] ");
            strSql.Append(" ON [seh_release].[SoftwareId] = [seh_software].[SoftwareId] ");
            strSql.Append(" WHERE [seh_release].[SoftwareId]=@softwareid");           
            strSql.Append(" ORDER BY [seh_release].[Sequence] DESC");
            SqlParameter[] parameters = {
					new SqlParameter("@softwareid", SqlDbType.Int,4)};
            parameters[0].Value = softwareid;

            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString(), parameters))
            {
                while (sdr.Read())
                {
                    Johnny.CMS.OM.SeH.Release item = new Johnny.CMS.OM.SeH.Release(sdr.GetInt32(0), sdr.GetInt32(1), sdr.GetString(2), sdr.GetString(3), sdr.GetDateTime(4), sdr.GetString(5), sdr.GetInt32(6), sdr.GetInt32(7), sdr.GetBoolean(8), sdr.GetDateTime(9), sdr.GetInt32(10), sdr.GetString(11), sdr.GetDateTime(12), sdr.GetInt32(13), sdr.GetString(14), sdr.GetInt32(15));
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Method to get one recoder by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SeH.Release GetModel(int releaseid)
        {
            //Set up a return value
            Johnny.CMS.OM.SeH.Release model = null;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [ReleaseId], [SoftwareId], [ReleaseName], [ReleaseDate], [Description], [Hits], [Downloads], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence] ");
            strSql.Append(" FROM [seh_release] ");
            strSql.Append(" WHERE [ReleaseId]=@releaseid");
            SqlParameter[] parameters = {
					new SqlParameter("@releaseid", SqlDbType.Int,4)};
            parameters[0].Value = releaseid;
            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString(), parameters))
            {
                if (sdr.Read())
                    model = new Johnny.CMS.OM.SeH.Release(sdr.GetInt32(0), sdr.GetInt32(1), sdr.GetString(2), sdr.GetDateTime(3), sdr.GetString(4), sdr.GetInt32(5), sdr.GetInt32(6), sdr.GetBoolean(7), sdr.GetDateTime(8), sdr.GetInt32(9), sdr.GetString(10), sdr.GetDateTime(11), sdr.GetInt32(12), sdr.GetString(13), sdr.GetInt32(14));
                else
                    model = new Johnny.CMS.OM.SeH.Release();
            }
            return model;
        }

        /// <summary>
        /// Method to get one latest recoder
        /// </summary>    	 
        public Johnny.CMS.OM.SeH.Release GetLatestModel(int softwareid)
        {
            //Set up a return value
            Johnny.CMS.OM.SeH.Release model = null;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Top 1 [ReleaseId], [seh_release].[SoftwareId], [seh_software].[SoftwareName], [ReleaseName], [ReleaseDate], [seh_release].[Description], [seh_release].[Hits], [seh_release].[Downloads], [seh_release].[IsDisplay], [seh_release].[CreatedTime], [seh_release].[CreatedById], [seh_release].[CreatedByName], [seh_release].[UpdatedTime], [seh_release].[UpdatedById], [seh_release].[UpdatedByName], [seh_release].[Sequence] ");
            strSql.Append(" FROM [seh_release] ");
            strSql.Append(" LEFT OUTER JOIN [seh_software] ");
            strSql.Append(" ON [seh_release].[SoftwareId] = [seh_software].[SoftwareId] ");
            strSql.Append(" WHERE [seh_release].[SoftwareId]=@softwareid");
            strSql.Append(" ORDER BY [seh_release].[Sequence] DESC");
            SqlParameter[] parameters = {
					new SqlParameter("@softwareid", SqlDbType.Int,4)};
            parameters[0].Value = softwareid;
            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString(), parameters))
            {
                if (sdr.Read())
                    model = new Johnny.CMS.OM.SeH.Release(sdr.GetInt32(0), sdr.GetInt32(1), sdr.GetString(2), sdr.GetString(3), sdr.GetDateTime(4), sdr.GetString(5), sdr.GetInt32(6), sdr.GetInt32(7), sdr.GetBoolean(8), sdr.GetDateTime(9), sdr.GetInt32(10), sdr.GetString(11), sdr.GetDateTime(12), sdr.GetInt32(13), sdr.GetString(14), sdr.GetInt32(15));
                else
                    model = new Johnny.CMS.OM.SeH.Release();
            }
            return model;
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SeH.Release model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DECLARE @Sequence int");
            strSql.Append(" SELECT @Sequence=(max(Sequence)+1) FROM [seh_release]");
            strSql.Append(" if @Sequence is NULL");
            strSql.Append(" Set @Sequence=1");
            strSql.Append("INSERT INTO [seh_release](");
            strSql.Append("[SoftwareId],[ReleaseName],[ReleaseDate],[Description],[Hits],[Downloads],[IsDisplay],[CreatedTime],[CreatedById],[CreatedByName],[UpdatedTime],[UpdatedById],[UpdatedByName],[Sequence]");
            strSql.Append(")");
            strSql.Append(" VALUES (");
            strSql.Append("@softwareid,@releasename,@releasedate,@description,@hits,@downloads,@isdisplay,@createdtime,@createdbyid,@createdbyname,@updatedtime,@updatedbyid,@updatedbyname,@sequence");
            strSql.Append(")");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
            		new SqlParameter("@softwareid", SqlDbType.Int,4),
					new SqlParameter("@releasename", SqlDbType.VarChar,100),
					new SqlParameter("@releasedate", SqlDbType.DateTime),
					new SqlParameter("@description", SqlDbType.Text),
					new SqlParameter("@hits", SqlDbType.Int,4),
					new SqlParameter("@downloads", SqlDbType.Int,4),
					new SqlParameter("@isdisplay", SqlDbType.Bit),
					new SqlParameter("@createdtime", SqlDbType.DateTime),
					new SqlParameter("@createdbyid", SqlDbType.Int,4),
					new SqlParameter("@createdbyname", SqlDbType.VarChar,50),
					new SqlParameter("@updatedtime", SqlDbType.DateTime),
					new SqlParameter("@updatedbyid", SqlDbType.Int,4),
					new SqlParameter("@updatedbyname", SqlDbType.VarChar,50)};
            parameters[0].Value = model.SoftwareId;
            parameters[1].Value = model.ReleaseName;
            parameters[2].Value = model.ReleaseDate;
            parameters[3].Value = model.Description;
            parameters[4].Value = model.Hits;
            parameters[5].Value = model.Downloads;
            parameters[6].Value = model.IsDisplay;
            parameters[7].Value = model.CreatedTime;
            parameters[8].Value = model.CreatedById;
            parameters[9].Value = model.CreatedByName;
            parameters[10].Value = model.UpdatedTime;
            parameters[11].Value = model.UpdatedById;
            parameters[12].Value = model.UpdatedByName;

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
        public void Update(Johnny.CMS.OM.SeH.Release model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [seh_release] SET ");
            strSql.Append("[SoftwareId]=@softwareid,");
            strSql.Append("[ReleaseName]=@releasename,");
            strSql.Append("[ReleaseDate]=@releasedate,");
            strSql.Append("[Description]=@description,");
            strSql.Append("[Hits]=@hits,");
            strSql.Append("[Downloads]=@downloads,");
            strSql.Append("[IsDisplay]=@isdisplay,");
            //strSql.Append("[CreatedTime]=@createdtime,");
            //strSql.Append("[CreatedById]=@createdbyid,");
            //strSql.Append("[CreatedByName]=@createdbyname,");
            strSql.Append("[UpdatedTime]=@updatedtime,");
            strSql.Append("[UpdatedById]=@updatedbyid,");
            strSql.Append("[UpdatedByName]=@updatedbyname,");
            strSql.Append(" WHERE [ReleaseId]=@releaseid ");
            SqlParameter[] parameters = {
            		new SqlParameter("@releaseid", SqlDbType.Int,4),
					new SqlParameter("@softwareid", SqlDbType.Int,4),
					new SqlParameter("@releasename", SqlDbType.VarChar,100),
					new SqlParameter("@releasedate", SqlDbType.DateTime),
					new SqlParameter("@description", SqlDbType.Text),
					new SqlParameter("@hits", SqlDbType.Int,4),
					new SqlParameter("@downloads", SqlDbType.Int,4),
					new SqlParameter("@isdisplay", SqlDbType.Bit),
					//new SqlParameter("@createdtime", SqlDbType.DateTime),
					//new SqlParameter("@createdbyid", SqlDbType.Int,4),
					//new SqlParameter("@createdbyname", SqlDbType.VarChar,50),
					new SqlParameter("@updatedtime", SqlDbType.DateTime),
					new SqlParameter("@updatedbyid", SqlDbType.Int,4),
					new SqlParameter("@updatedbyname", SqlDbType.VarChar,50),
			};
            parameters[0].Value = model.ReleaseId;
            parameters[1].Value = model.SoftwareId;
            parameters[2].Value = model.ReleaseName;
            parameters[3].Value = model.ReleaseDate;
            parameters[4].Value = model.Description;
            parameters[5].Value = model.Hits;
            parameters[6].Value = model.Downloads;
            parameters[7].Value = model.IsDisplay;
            //parameters[8].Value = model.CreatedTime;
            //parameters[9].Value = model.CreatedById;
            //parameters[10].Value = model.CreatedByName;
            parameters[8].Value = model.UpdatedTime;
            parameters[9].Value = model.UpdatedById;
            parameters[10].Value = model.UpdatedByName;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int releaseid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM [seh_release] WHERE [ReleaseId]=@releaseid");
            SqlParameter[] parameters = {
					new SqlParameter("@releaseid", SqlDbType.Int,4)};
            parameters[0].Value = releaseid;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int releaseid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM [seh_release] WHERE [ReleaseId]=@releaseid");
            SqlParameter[] parameters = {
					new SqlParameter("@releaseid", SqlDbType.Int,4)};
            parameters[0].Value = releaseid;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
    }
}