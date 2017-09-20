using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Johnny.Library.Database;

namespace Johnny.CMS.DAL.SystemInfo
{

    /// <summary>
    /// BreviarySettings is a DAL calss that represents cms_breviarysettings
    /// </summary>
    public class BreviarySettings
    {
        /// <summary>
        /// Method to get recoders with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SystemInfo.BreviarySettings> GetList()
        {
            IList<Johnny.CMS.OM.SystemInfo.BreviarySettings> list = new List<Johnny.CMS.OM.SystemInfo.BreviarySettings>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [Id], [Width], [Height], [PlusWatermark], [WatermarkType], [WatermarkImage], [ImageTransparent], [WatermarkText], [TextTransparent], [WatermarkPosition] ");
            strSql.Append(" FROM [cms_breviarysettings] ");
            strSql.Append(" ORDER BY [Sequence]");

            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString()))
            {
                while (sdr.Read())
                {
                    Johnny.CMS.OM.SystemInfo.BreviarySettings item = new Johnny.CMS.OM.SystemInfo.BreviarySettings(sdr.GetInt32(0), sdr.GetInt32(1), sdr.GetInt32(2), sdr.GetBoolean(3), sdr.GetBoolean(4), sdr.GetString(5), sdr.GetInt32(6), sdr.GetString(7), sdr.GetInt32(8), sdr.GetInt32(9));
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Method to get one recoder by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SystemInfo.BreviarySettings GetModel(int id)
        {
            //Set up a return value
            Johnny.CMS.OM.SystemInfo.BreviarySettings model = null;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [Id], [Width], [Height], [PlusWatermark], [WatermarkType], [WatermarkImage], [ImageTransparent], [WatermarkText], [TextTransparent], [WatermarkPosition] ");
            strSql.Append(" FROM [cms_breviarysettings] ");
            strSql.Append(" WHERE [Id]=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString(), parameters))
            {
                if (sdr.Read())
                    model = new Johnny.CMS.OM.SystemInfo.BreviarySettings(sdr.GetInt32(0), sdr.GetInt32(1), sdr.GetInt32(2), sdr.GetBoolean(3), sdr.GetBoolean(4), sdr.GetString(5), sdr.GetInt32(6), sdr.GetString(7), sdr.GetInt32(8), sdr.GetInt32(9));
                else
                    model = new Johnny.CMS.OM.SystemInfo.BreviarySettings();
            }
            return model;
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SystemInfo.BreviarySettings model)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("DECLARE @Sequence int");
            //strSql.Append(" SELECT @Sequence=(max(Sequence)+1) FROM [cms_breviarysettings]");
            //strSql.Append(" if @Sequence is NULL");
            //strSql.Append(" Set @Sequence=1");
            strSql.Append("INSERT INTO [cms_breviarysettings](");
            strSql.Append("[Width],[Height],[PlusWatermark],[WatermarkType],[WatermarkImage],[ImageTransparent],[WatermarkText],[TextTransparent],[WatermarkPosition]");
            strSql.Append(")");
            strSql.Append(" VALUES (");
            strSql.Append("@width,@height,@pluswatermark,@watermarktype,@watermarkimage,@imagetransparent,@watermarktext,@texttransparent,@watermarkposition");
            strSql.Append(")");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
            		new SqlParameter("@width", SqlDbType.Int,4),
					new SqlParameter("@height", SqlDbType.Int,4),
					new SqlParameter("@pluswatermark", SqlDbType.Bit),
					new SqlParameter("@watermarktype", SqlDbType.Bit),
					new SqlParameter("@watermarkimage", SqlDbType.VarChar,800),
					new SqlParameter("@imagetransparent", SqlDbType.Int,4),
					new SqlParameter("@watermarktext", SqlDbType.NVarChar,50),
					new SqlParameter("@texttransparent", SqlDbType.Int,4),
					new SqlParameter("@watermarkposition", SqlDbType.Int,4)};
            parameters[0].Value = model.Width;
            parameters[1].Value = model.Height;
            parameters[2].Value = model.PlusWatermark;
            parameters[3].Value = model.WatermarkType;
            parameters[4].Value = model.WatermarkImage;
            parameters[5].Value = model.ImageTransparent;
            parameters[6].Value = model.WatermarkText;
            parameters[7].Value = model.TextTransparent;
            parameters[8].Value = model.WatermarkPosition;

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
        public void Update(Johnny.CMS.OM.SystemInfo.BreviarySettings model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [cms_breviarysettings] SET ");
            strSql.Append("[Width]=@width,");
            strSql.Append("[Height]=@height,");
            strSql.Append("[PlusWatermark]=@pluswatermark,");
            strSql.Append("[WatermarkType]=@watermarktype,");
            strSql.Append("[WatermarkImage]=@watermarkimage,");
            strSql.Append("[ImageTransparent]=@imagetransparent,");
            strSql.Append("[WatermarkText]=@watermarktext,");
            strSql.Append("[TextTransparent]=@texttransparent,");
            strSql.Append("[WatermarkPosition]=@watermarkposition");
            //strSql.Append(" WHERE [Id]=@id ");
            SqlParameter[] parameters = {
            		new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@width", SqlDbType.Int,4),
					new SqlParameter("@height", SqlDbType.Int,4),
					new SqlParameter("@pluswatermark", SqlDbType.Bit),
					new SqlParameter("@watermarktype", SqlDbType.Bit),
					new SqlParameter("@watermarkimage", SqlDbType.VarChar,800),
					new SqlParameter("@imagetransparent", SqlDbType.Int,4),
					new SqlParameter("@watermarktext", SqlDbType.NVarChar,50),
					new SqlParameter("@texttransparent", SqlDbType.Int,4),
					new SqlParameter("@watermarkposition", SqlDbType.Int,4)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Width;
            parameters[2].Value = model.Height;
            parameters[3].Value = model.PlusWatermark;
            parameters[4].Value = model.WatermarkType;
            parameters[5].Value = model.WatermarkImage;
            parameters[6].Value = model.ImageTransparent;
            parameters[7].Value = model.WatermarkText;
            parameters[8].Value = model.TextTransparent;
            parameters[9].Value = model.WatermarkPosition;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM [cms_breviarysettings] WHERE [Id]=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM [cms_breviarysettings] WHERE [Id]=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
    }
}
