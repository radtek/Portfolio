using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Johnny.Library.Database;

namespace Johnny.CMS.DAL.SystemInfo
{

    /// <summary>
    /// WebSettings is a DAL calss that represents cms_websettings
    /// </summary>
    public class WebSettings
    {
        /// <summary>
        /// Method to get recoders with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SystemInfo.WebSettings> GetList()
        {
            IList<Johnny.CMS.OM.SystemInfo.WebSettings> list = new List<Johnny.CMS.OM.SystemInfo.WebSettings>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [Id], [WebsiteName], [WebsiteTitle], [ShortDescription], [Tel], [Fax], [Email], [WebsiteAddress], [WebsitePath], [FileSize], [LogoPath], [BannerPath], [Copyright], [MetaKeywords], [MetaDescription], [IsClosed], [ClosedInfo], [UserAgreement], [LoginType] ");
            strSql.Append(" FROM [cms_websettings] ");
            strSql.Append(" ORDER BY [Sequence]");

            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString()))
            {
                while (sdr.Read())
                {
                    Johnny.CMS.OM.SystemInfo.WebSettings item = new Johnny.CMS.OM.SystemInfo.WebSettings(sdr.GetInt32(0), sdr.GetString(1), sdr.GetString(2), sdr.GetString(3), sdr.GetString(4), sdr.GetString(5), sdr.GetString(6), sdr.GetString(7), sdr.GetString(8), sdr.GetInt32(9), sdr.GetString(10), sdr.GetString(11), sdr.GetString(12), sdr.GetString(13), sdr.GetString(14), sdr.GetBoolean(15), sdr.GetString(16), sdr.GetString(17), sdr.GetBoolean(18));
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Method to get one recoder by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SystemInfo.WebSettings GetModel()
        {
            //Set up a return value
            Johnny.CMS.OM.SystemInfo.WebSettings model = null;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [Id], [WebsiteName], [WebsiteTitle], [ShortDescription], [Tel], [Fax], [Email], [WebsiteAddress], [WebsitePath], [FileSize], [LogoPath], [BannerPath], [Copyright], [MetaKeywords], [MetaDescription], [IsClosed], [ClosedInfo], [UserAgreement], [LoginType] ");
            strSql.Append(" FROM [cms_websettings] ");           
            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString()))
            {
                if (sdr.Read())
                    model = new Johnny.CMS.OM.SystemInfo.WebSettings(sdr.GetInt32(0), sdr.GetString(1), sdr.GetString(2), sdr.GetString(3), sdr.GetString(4), sdr.GetString(5), sdr.GetString(6), sdr.GetString(7), sdr.GetString(8), sdr.GetInt32(9), sdr.GetString(10), sdr.GetString(11), sdr.GetString(12), sdr.GetString(13), sdr.GetString(14), sdr.GetBoolean(15), sdr.GetString(16), sdr.GetString(17), sdr.GetBoolean(18));
                else
                    model = new Johnny.CMS.OM.SystemInfo.WebSettings();
            }
            return model;
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SystemInfo.WebSettings model)
        {
            StringBuilder strSql = new StringBuilder();           
            strSql.Append("INSERT INTO [cms_websettings](");
            strSql.Append("[WebsiteName],[WebsiteTitle],[ShortDescription],[Tel],[Fax],[Email],[WebsiteAddress],[WebsitePath],[FileSize],[LogoPath],[BannerPath],[Copyright],[MetaKeywords],[MetaDescription],[IsClosed],[ClosedInfo],[UserAgreement],[LoginType]");
            strSql.Append(")");
            strSql.Append(" VALUES (");
            strSql.Append("@websitename,@websitetitle,@shortdescription,@tel,@fax,@email,@websiteaddress,@websitepath,@filesize,@logopath,@bannerpath,@copyright,@metakeywords,@metadescription,@isclosed,@closedinfo,@useragreement,@logintype");
            strSql.Append(")");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
            		new SqlParameter("@websitename", SqlDbType.NVarChar,40),
					new SqlParameter("@websitetitle", SqlDbType.NVarChar,100),
                    new SqlParameter("@shortdescription", SqlDbType.VarChar,500),
					new SqlParameter("@tel", SqlDbType.VarChar,50),
					new SqlParameter("@fax", SqlDbType.VarChar,50),
					new SqlParameter("@email", SqlDbType.VarChar,50),
					new SqlParameter("@websiteaddress", SqlDbType.VarChar,200),
					new SqlParameter("@websitepath", SqlDbType.VarChar,50),
					new SqlParameter("@filesize", SqlDbType.Int,4),
					new SqlParameter("@logopath", SqlDbType.VarChar,100),
					new SqlParameter("@bannerpath", SqlDbType.VarChar,100),
					new SqlParameter("@copyright", SqlDbType.NVarChar,500),
					new SqlParameter("@metakeywords", SqlDbType.NVarChar,100),
					new SqlParameter("@metadescription", SqlDbType.NVarChar,400),
					new SqlParameter("@isclosed", SqlDbType.Bit),
					new SqlParameter("@closedinfo", SqlDbType.NVarChar,1000),
					new SqlParameter("@useragreement", SqlDbType.Text),
					new SqlParameter("@logintype", SqlDbType.Int,4)};
            parameters[0].Value = model.WebsiteName;
            parameters[1].Value = model.WebsiteTitle;
            parameters[2].Value = model.ShortDescription;
            parameters[3].Value = model.Tel;
            parameters[4].Value = model.Fax;
            parameters[5].Value = model.Email;
            parameters[6].Value = model.WebsiteAddress;
            parameters[7].Value = model.WebsitePath;
            parameters[8].Value = model.FileSize;
            parameters[9].Value = model.LogoPath;
            parameters[10].Value = model.BannerPath;
            parameters[11].Value = model.Copyright;
            parameters[12].Value = model.MetaKeywords;
            parameters[13].Value = model.MetaDescription;
            parameters[14].Value = model.IsClosed;
            parameters[15].Value = model.ClosedInfo;
            parameters[16].Value = model.UserAgreement;
            parameters[17].Value = model.LoginType;

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
        public void Update(Johnny.CMS.OM.SystemInfo.WebSettings model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [cms_websettings] SET ");
            strSql.Append("[WebsiteName]=@websitename,");
            strSql.Append("[WebsiteTitle]=@websitetitle,");
            strSql.Append("[ShortDescription]=@shortdescription,");
            strSql.Append("[Tel]=@tel,");
            strSql.Append("[Fax]=@fax,");
            strSql.Append("[Email]=@email,");
            strSql.Append("[WebsiteAddress]=@websiteaddress,");
            strSql.Append("[WebsitePath]=@websitepath,");
            strSql.Append("[FileSize]=@filesize,");
            strSql.Append("[LogoPath]=@logopath,");
            strSql.Append("[BannerPath]=@bannerpath,");
            strSql.Append("[Copyright]=@copyright,");
            strSql.Append("[MetaKeywords]=@metakeywords,");
            strSql.Append("[MetaDescription]=@metadescription,");
            strSql.Append("[IsClosed]=@isclosed,");
            strSql.Append("[ClosedInfo]=@closedinfo,");
            strSql.Append("[UserAgreement]=@useragreement,");
            strSql.Append("[LoginType]=@logintype");
            //strSql.Append(" WHERE [Id]=@id ");
            SqlParameter[] parameters = {
            		new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@websitename", SqlDbType.NVarChar,40),
					new SqlParameter("@websitetitle", SqlDbType.NVarChar,100),
                    new SqlParameter("@shortdescription", SqlDbType.VarChar,500),
					new SqlParameter("@tel", SqlDbType.VarChar,50),
					new SqlParameter("@fax", SqlDbType.VarChar,50),
					new SqlParameter("@email", SqlDbType.VarChar,50),
					new SqlParameter("@websiteaddress", SqlDbType.VarChar,200),
					new SqlParameter("@websitepath", SqlDbType.VarChar,50),
					new SqlParameter("@filesize", SqlDbType.Int,4),
					new SqlParameter("@logopath", SqlDbType.VarChar,100),
					new SqlParameter("@bannerpath", SqlDbType.VarChar,100),
					new SqlParameter("@copyright", SqlDbType.NVarChar,500),
					new SqlParameter("@metakeywords", SqlDbType.NVarChar,100),
					new SqlParameter("@metadescription", SqlDbType.NVarChar,400),
					new SqlParameter("@isclosed", SqlDbType.Bit),
					new SqlParameter("@closedinfo", SqlDbType.NVarChar,1000),
					new SqlParameter("@useragreement", SqlDbType.Text),
					new SqlParameter("@logintype", SqlDbType.Int,4)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.WebsiteName;
            parameters[2].Value = model.WebsiteTitle;
            parameters[3].Value = model.ShortDescription;
            parameters[4].Value = model.Tel;
            parameters[5].Value = model.Fax;
            parameters[6].Value = model.Email;
            parameters[7].Value = model.WebsiteAddress;
            parameters[8].Value = model.WebsitePath;
            parameters[9].Value = model.FileSize;
            parameters[10].Value = model.LogoPath;
            parameters[11].Value = model.BannerPath;
            parameters[12].Value = model.Copyright;
            parameters[13].Value = model.MetaKeywords;
            parameters[14].Value = model.MetaDescription;
            parameters[15].Value = model.IsClosed;
            parameters[16].Value = model.ClosedInfo;
            parameters[17].Value = model.UserAgreement;
            parameters[18].Value = model.LoginType;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM [cms_websettings] WHERE [Id]=@id");
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
            strSql.Append("SELECT COUNT(1) FROM [cms_websettings] WHERE [Id]=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
    }
}
