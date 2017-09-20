using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Johnny.Library.Database;

namespace Johnny.CMS.DAL.SystemInfo
{

    /// <summary>
    /// Topmenu2 is a DAL calss that represents cms_topmenu
    /// </summary>
    public class TopMenu
    {
        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SystemInfo.TopMenu> GetList()
        {
            IList<Johnny.CMS.OM.SystemInfo.TopMenu> list = new List<Johnny.CMS.OM.SystemInfo.TopMenu>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [TopMenuId], [TopMenuName], [ToolTip], [PageLink], [Sequence] ");
            strSql.Append(" FROM [cms_topmenu] ");
            strSql.Append(" ORDER BY [Sequence]");

            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString()))
            {
                while (sdr.Read())
                {
                    Johnny.CMS.OM.SystemInfo.TopMenu item = new Johnny.CMS.OM.SystemInfo.TopMenu(sdr.GetInt32(0), sdr.GetString(1), sdr.GetString(2), sdr.GetString(3), sdr.GetInt32(4));
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SystemInfo.TopMenu GetModel(int topmenuid)
        {
            //Set up a return value
            Johnny.CMS.OM.SystemInfo.TopMenu model = null;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [TopMenuId], [TopMenuName], [ToolTip], [PageLink], [Sequence] ");
            strSql.Append(" FROM [cms_topmenu] ");
            strSql.Append(" WHERE [TopMenuId]=@topmenuid");
            SqlParameter[] parameters = {
					new SqlParameter("@topmenuid", SqlDbType.Int,4)};
            parameters[0].Value = topmenuid;
            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString(), parameters))
            {
                if (sdr.Read())
                    model = new Johnny.CMS.OM.SystemInfo.TopMenu(sdr.GetInt32(0), sdr.GetString(1), sdr.GetString(2), sdr.GetString(3), sdr.GetInt32(4));
                else
                    model = new Johnny.CMS.OM.SystemInfo.TopMenu();
            }
            return model;
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SystemInfo.TopMenu model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DECLARE @Sequence int");
            strSql.Append(" SELECT @Sequence=(max(Sequence)+1) FROM [cms_topmenu]");
            strSql.Append(" if @Sequence is NULL");
            strSql.Append(" Set @Sequence=1");
            strSql.Append("INSERT INTO [cms_topmenu](");
            strSql.Append("[TopMenuName],[toolTip],[PageLink],[Sequence]");
            strSql.Append(")");
            strSql.Append(" VALUES (");
            strSql.Append("@topmenuname,@toolTip,@pagelink,@Sequence");
            strSql.Append(")");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
            		new SqlParameter("@topmenuname", SqlDbType.NVarChar,50),
					new SqlParameter("@toolTip", SqlDbType.NVarChar,50),
					new SqlParameter("@pagelink", SqlDbType.VarChar,100)};
            parameters[0].Value = model.TopMenuName;
            parameters[1].Value = model.ToolTip;
            parameters[2].Value = model.PageLink;

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
        public void Update(Johnny.CMS.OM.SystemInfo.TopMenu model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [cms_topmenu] SET ");
            strSql.Append("[TopMenuName]=@topmenuname,");
            strSql.Append("[ToolTip]=@toolTip,");
            strSql.Append("[PageLink]=@pagelink");
            strSql.Append(" WHERE [TopMenuId]=@topmenuid ");
            SqlParameter[] parameters = {
            		new SqlParameter("@topmenuid", SqlDbType.Int,4),
					new SqlParameter("@topmenuname", SqlDbType.NVarChar,50),
					new SqlParameter("@toolTip", SqlDbType.NVarChar,50),
					new SqlParameter("@pagelink", SqlDbType.VarChar,100)};
            parameters[0].Value = model.TopMenuId;
            parameters[1].Value = model.TopMenuName;
            parameters[2].Value = model.ToolTip;
            parameters[3].Value = model.PageLink;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int topmenuid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM [cms_topmenu] WHERE [TopMenuId]=@topmenuid");
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
            strSql.Append("SELECT COUNT(1) FROM [cms_topmenu] WHERE [TopMenuId]=@topmenuid");
            SqlParameter[] parameters = {
					new SqlParameter("@topmenuid", SqlDbType.Int,4)};
            parameters[0].Value = topmenuid;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
    }
}
