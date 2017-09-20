using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Johnny.Library.Database;

namespace Johnny.CMS.DAL.SystemInfo
{

    /// <summary>
    /// Pagebinding is a DAL calss that represents cms_pagebinding
    /// </summary>
    public class PageBinding
    {
        /// <summary>
        /// Method to get recoders with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SystemInfo.PageBinding> GetList()
        {
            IList<Johnny.CMS.OM.SystemInfo.PageBinding> list = new List<Johnny.CMS.OM.SystemInfo.PageBinding>();

            StringBuilder strSql = new StringBuilder();
            //strSql.Append("SELECT [PageBindingId], [Title], [MenuCategoryId], [ListMenuId], [AddMenuId] ");
            //strSql.Append(" FROM [cms_pagebinding] ");
            //strSql.Append(" ORDER BY [Sequence]");
            strSql.Append(" SELECT [PageBindingId], [Title], [cms_pagebinding].[MenuCategoryId], [cms_menucategory].[MenuCategoryName], [ListMenuId], [menu1].[MenuName], [menu1].[PageLink], [AddMenuId], [menu2].[MenuName], [menu2].[PageLink] ");
            strSql.Append(" FROM [cms_pagebinding] ");
            strSql.Append(" LEFT OUTER JOIN [cms_menucategory] ");
            strSql.Append(" ON [cms_pagebinding].[MenuCategoryId] = [cms_menucategory].[MenuCategoryId] ");
            strSql.Append(" LEFT OUTER JOIN [cms_menu] AS menu1");
            strSql.Append(" ON [cms_pagebinding].[ListMenuId] = [menu1].[MenuId] ");
            strSql.Append(" LEFT OUTER JOIN [cms_menu] AS menu2 ");
            strSql.Append(" ON [cms_pagebinding].[AddMenuId] = [menu2].[MenuId] ");
            strSql.Append(" ORDER BY [cms_menucategory].[Sequence]");

            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString()))
            {
                while (sdr.Read())
                {
                    Johnny.CMS.OM.SystemInfo.PageBinding item = new Johnny.CMS.OM.SystemInfo.PageBinding(sdr.GetInt32(0), sdr.GetString(1), sdr.GetInt32(2), sdr.GetString(3), sdr.GetInt32(4), sdr.GetString(5), sdr.GetString(6), sdr.GetInt32(7), sdr.GetString(8), sdr.GetString(9));
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Method to get one recoder by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SystemInfo.PageBinding GetModel(int pagebindingid)
        {
            //Set up a return value
            Johnny.CMS.OM.SystemInfo.PageBinding model = null;

            StringBuilder strSql = new StringBuilder();
            //strSql.Append("SELECT [PageBindingId], [Title], [MenuCategoryId], [ListMenuId], [AddMenuId] ");
            //strSql.Append(" FROM [cms_pagebinding] ");
            //strSql.Append(" WHERE [PageBindingId]=@pagebindingid");
            strSql.Append(" SELECT [PageBindingId], [Title], [cms_pagebinding].[MenuCategoryId], [cms_menucategory].[MenuCategoryName], [ListMenuId], [menu1].[MenuName], [menu1].[PageLink], [AddMenuId], [menu2].[MenuName], [menu2].[PageLink] ");
            strSql.Append(" FROM [cms_pagebinding] ");
            strSql.Append(" LEFT OUTER JOIN [cms_menucategory] ");
            strSql.Append(" ON [cms_pagebinding].[MenuCategoryId] = [cms_menucategory].[MenuCategoryId] ");
            strSql.Append(" LEFT OUTER JOIN [cms_menu] AS menu1");
            strSql.Append(" ON [cms_pagebinding].[ListMenuId] = [menu1].[MenuId] ");
            strSql.Append(" LEFT OUTER JOIN [cms_menu] AS menu2 ");
            strSql.Append(" ON [cms_pagebinding].[AddMenuId] = [menu2].[MenuId] ");
            strSql.Append(" WHERE [cms_pagebinding].[PageBindingId]=@pagebindingid");
            SqlParameter[] parameters = {
					new SqlParameter("@pagebindingid", SqlDbType.Int,4)};
            parameters[0].Value = pagebindingid;
            using (SqlDataReader sdr = DbHelperSQL.ExecuteReader(strSql.ToString(), parameters))
            {
                if (sdr.Read())
                    model = new Johnny.CMS.OM.SystemInfo.PageBinding(sdr.GetInt32(0), sdr.GetString(1), sdr.GetInt32(2), sdr.GetString(3), sdr.GetInt32(4), sdr.GetString(5), sdr.GetString(6), sdr.GetInt32(7), sdr.GetString(8), sdr.GetString(9));
                else
                    model = new Johnny.CMS.OM.SystemInfo.PageBinding();
            }
            return model;
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SystemInfo.PageBinding model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DECLARE @Sequence int");
            strSql.Append(" SELECT @Sequence=(max(Sequence)+1) FROM [cms_pagebinding]");
            strSql.Append(" if @Sequence is NULL");
            strSql.Append(" Set @Sequence=1");
            strSql.Append("INSERT INTO [cms_pagebinding](");
            strSql.Append("[Title],[MenuCategoryId],[ListMenuId],[AddMenuId]");
            strSql.Append(")");
            strSql.Append(" VALUES (");
            strSql.Append("@title,@menucategoryid,@listmenuid,@addmenuid");
            strSql.Append(")");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
            		new SqlParameter("@title", SqlDbType.NVarChar,50),
					new SqlParameter("@menucategoryid", SqlDbType.Int,4),
					new SqlParameter("@listmenuid", SqlDbType.Int,4),
					new SqlParameter("@addmenuid", SqlDbType.Int,4)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.MenuCategoryId;
            parameters[2].Value = model.ListMenuId;
            parameters[3].Value = model.AddMenuId;

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
        public void Update(Johnny.CMS.OM.SystemInfo.PageBinding model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [cms_pagebinding] SET ");
            strSql.Append("[Title]=@title,");
            strSql.Append("[MenuCategoryId]=@menucategoryid,");
            strSql.Append("[ListMenuId]=@listmenuid,");
            strSql.Append("[AddMenuId]=@addmenuid");
            strSql.Append(" WHERE [PageBindingId]=@pagebindingid ");
            SqlParameter[] parameters = {
            		new SqlParameter("@pagebindingid", SqlDbType.Int,4),
					new SqlParameter("@title", SqlDbType.NVarChar,50),
					new SqlParameter("@menucategoryid", SqlDbType.Int,4),
					new SqlParameter("@listmenuid", SqlDbType.Int,4),
					new SqlParameter("@addmenuid", SqlDbType.Int,4)};
            parameters[0].Value = model.PageBindingId;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.MenuCategoryId;
            parameters[3].Value = model.ListMenuId;
            parameters[4].Value = model.AddMenuId;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int pagebindingid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM [cms_pagebinding] WHERE [PageBindingId]=@pagebindingid");
            SqlParameter[] parameters = {
					new SqlParameter("@pagebindingid", SqlDbType.Int,4)};
            parameters[0].Value = pagebindingid;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int pagebindingid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM [cms_pagebinding] WHERE [PageBindingId]=@pagebindingid");
            SqlParameter[] parameters = {
					new SqlParameter("@pagebindingid", SqlDbType.Int,4)};
            parameters[0].Value = pagebindingid;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
    }
}
