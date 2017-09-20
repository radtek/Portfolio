using System.Collections.Generic;

using Johnny.CMS.OM;
using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL.SystemInfo
{

    /// <summary>
    /// A business component to manage TopMenu
    /// </summary>
    public class TopMenu
    {
        // Get an instance of the User DAL
        // Making this static will cache the DAL instance after the initial load
        private static readonly Johnny.CMS.DAL.SystemInfo.TopMenu dal = new Johnny.CMS.DAL.SystemInfo.TopMenu();

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SystemInfo.TopMenu> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SystemInfo.TopMenu GetModel(int TopMenuId)
        {
            return dal.GetModel(TopMenuId);
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SystemInfo.TopMenu model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void Update(Johnny.CMS.OM.SystemInfo.TopMenu model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int TopMenuId)
        {
            dal.Delete(TopMenuId);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int TopMenuId)
        {
            return dal.IsExist(TopMenuId);
        }
    }
}
