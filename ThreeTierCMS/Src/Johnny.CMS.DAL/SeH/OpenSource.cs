using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Johnny.Library.Database;

namespace Johnny.CMS.DAL.SeH
{

    /// <summary>
    /// OpenSource is a DAL calss that represents seh_OpenSource
    /// </summary>
    public class OpenSource
    {
        /// <summary>
        /// Method to get recoders with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SeH.OpenSource> GetList()
        {
            IList<Johnny.CMS.OM.SeH.OpenSource> list = new List<Johnny.CMS.OM.SeH.OpenSource>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [OpenSourceId], [OpenSourceName], [ShortDescription], [Description], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence] ");
            strSql.Append(" FROM [seh_opensource] ");
            strSql.Append(" ORDER BY [Sequence]");

            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString()))
            {
                while (sdr.Read())
                {
                    Johnny.CMS.OM.SeH.OpenSource item = new Johnny.CMS.OM.SeH.OpenSource(sdr.GetInt32(0), sdr.GetString(1), sdr.GetString(2), sdr.GetString(3), sdr.GetString(4), sdr.GetInt32(5), sdr.GetBoolean(6), sdr.GetDateTime(7), sdr.GetInt32(8), sdr.GetString(9), sdr.GetDateTime(10), sdr.GetInt32(11), sdr.GetString(12), sdr.GetInt32(13));
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Method to get one recoder by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SeH.OpenSource GetModel(int OpenSourceid)
        {
            //Set up a return value
            Johnny.CMS.OM.SeH.OpenSource model = null;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [OpenSourceId], [OpenSourceName], [ShortDescription], [Description], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence] ");
            strSql.Append(" FROM [seh_opensource] ");
            strSql.Append(" WHERE [OpenSourceId]=@opensourceid");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenSourceid", SqlDbType.Int,4)};
            parameters[0].Value = OpenSourceid;
            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString(), parameters))
            {
                if (sdr.Read())
                    model = new Johnny.CMS.OM.SeH.OpenSource(sdr.GetInt32(0), sdr.GetString(1), sdr.GetString(2), sdr.GetString(3), sdr.GetString(4), sdr.GetInt32(5), sdr.GetBoolean(6), sdr.GetDateTime(7), sdr.GetInt32(8), sdr.GetString(9), sdr.GetDateTime(10), sdr.GetInt32(11), sdr.GetString(12), sdr.GetInt32(13));
                else
                    model = new Johnny.CMS.OM.SeH.OpenSource();
            }
            return model;
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SeH.OpenSource model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DECLARE @Sequence int");
            strSql.Append(" SELECT @Sequence=(max(Sequence)+1) FROM [seh_OpenSource]");
            strSql.Append(" if @Sequence is NULL");
            strSql.Append(" Set @Sequence=1");
            strSql.Append("INSERT INTO [seh_OpenSource](");
            strSql.Append("[OpenSourceName],[ShortDescription],[Description],[URL],[Hits],[IsDisplay],[CreatedTime],[CreatedById],[CreatedByName],[UpdatedTime],[UpdatedById],[UpdatedByName],[Sequence]");
            strSql.Append(")");
            strSql.Append(" VALUES (");
            strSql.Append("@OpenSourcename,@shortdescription,@description,@url,@hits,@isdisplay,@createdtime,@createdbyid,@createdbyname,@updatedtime,@updatedbyid,@updatedbyname,@sequence");
            strSql.Append(")");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
            		new SqlParameter("@OpenSourcename", SqlDbType.NVarChar,50),
					new SqlParameter("@shortdescription", SqlDbType.NVarChar,200),
					new SqlParameter("@description", SqlDbType.Text),
					new SqlParameter("@url", SqlDbType.VarChar,200),
					new SqlParameter("@hits", SqlDbType.Int,4),
					new SqlParameter("@isdisplay", SqlDbType.Bit),
		            new SqlParameter("@createdtime", SqlDbType.DateTime),
					new SqlParameter("@createdbyid", SqlDbType.Int,4),
					new SqlParameter("@createdbyname", SqlDbType.VarChar,50),
					new SqlParameter("@updatedtime", SqlDbType.DateTime),
					new SqlParameter("@updatedbyid", SqlDbType.Int,4),
					new SqlParameter("@updatedbyname", SqlDbType.VarChar,50)};
            parameters[0].Value = model.OpenSourceName;
            parameters[1].Value = model.ShortDescription;
            parameters[2].Value = model.Description;
            parameters[3].Value = model.URL;
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
        public void Update(Johnny.CMS.OM.SeH.OpenSource model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [seh_OpenSource] SET ");
            strSql.Append("[OpenSourceName]=@OpenSourcename,");
            strSql.Append("[ShortDescription]=@shortdescription,");
            strSql.Append("[Description]=@description,");
            strSql.Append("[URL]=@url,");
            strSql.Append("[Hits]=@hits,");
            strSql.Append("[IsDisplay]=@isdisplay,");
            //strSql.Append("[CreatedTime]=@createdtime,");
            //strSql.Append("[CreatedById]=@createdbyid,");
            //strSql.Append("[CreatedByName]=@createdbyname,");
            strSql.Append("[UpdatedTime]=@updatedtime,");
            strSql.Append("[UpdatedById]=@updatedbyid,");
            strSql.Append("[UpdatedByName]=@updatedbyname,");
            strSql.Append(" WHERE [OpenSourceId]=@OpenSourceid ");
            SqlParameter[] parameters = {
            		new SqlParameter("@OpenSourceid", SqlDbType.Int,4),
					new SqlParameter("@OpenSourcename", SqlDbType.NVarChar,50),
					new SqlParameter("@shortdescription", SqlDbType.NVarChar,200),
					new SqlParameter("@description", SqlDbType.Text),
					new SqlParameter("@url", SqlDbType.VarChar,200),
					new SqlParameter("@hits", SqlDbType.Int,4),
					new SqlParameter("@isdisplay", SqlDbType.Bit),
		            //new SqlParameter("@createdtime", SqlDbType.DateTime),
					//new SqlParameter("@createdbyid", SqlDbType.Int,4),
					//new SqlParameter("@createdbyname", SqlDbType.VarChar,50),
					new SqlParameter("@updatedtime", SqlDbType.DateTime),
					new SqlParameter("@updatedbyid", SqlDbType.Int,4),
					new SqlParameter("@updatedbyname", SqlDbType.VarChar,50),
			};
            parameters[0].Value = model.OpenSourceId;
            parameters[1].Value = model.OpenSourceName;
            parameters[2].Value = model.ShortDescription;
            parameters[3].Value = model.Description;
            parameters[4].Value = model.URL;
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
        public void Delete(int OpenSourceid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM [seh_OpenSource] WHERE [OpenSourceId]=@OpenSourceid");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenSourceid", SqlDbType.Int,4)};
            parameters[0].Value = OpenSourceid;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int OpenSourceid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM [seh_OpenSource] WHERE [OpenSourceId]=@OpenSourceid");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenSourceid", SqlDbType.Int,4)};
            parameters[0].Value = OpenSourceid;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
    }
}
