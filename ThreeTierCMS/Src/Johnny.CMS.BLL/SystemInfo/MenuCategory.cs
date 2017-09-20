using System.Collections.Generic;

using Johnny.CMS.OM;
using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL.SystemInfo
{

    /// <summary>
    /// A business component to manage Menucategory2
    /// </summary>
    public class MenuCategory
    {
        // Get an instance of the User DAL
        // Making this static will cache the DAL instance after the initial load
        private static readonly Johnny.CMS.DAL.SystemInfo.MenuCategory dal = new Johnny.CMS.DAL.SystemInfo.MenuCategory();

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SystemInfo.MenuCategory> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SystemInfo.MenuCategory GetModel(int MenuCategoryId)
        {
            return dal.GetModel(MenuCategoryId);
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SystemInfo.MenuCategory model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void Update(Johnny.CMS.OM.SystemInfo.MenuCategory model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int MenuCategoryId)
        {
            dal.Delete(MenuCategoryId);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int MenuCategoryId)
        {
            return dal.IsExist(MenuCategoryId);
        }
    }
}
