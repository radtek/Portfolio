using System.Collections.Generic;

using Johnny.CMS.OM;
using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL.Access
{

    /// <summary>
    /// A business component to manage Permissioncategory
    /// </summary>
    public class PermissionCategory
    {
        // Get an instance of the User DAL
        // Making this static will cache the DAL instance after the initial load
        private static readonly Johnny.CMS.DAL.Access.PermissionCategory dal = new Johnny.CMS.DAL.Access.PermissionCategory();

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.Access.PermissionCategory> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.Access.PermissionCategory GetModel(int PermissionCategoryId)
        {
            return dal.GetModel(PermissionCategoryId);
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.Access.PermissionCategory model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void Update(Johnny.CMS.OM.Access.PermissionCategory model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int PermissionCategoryId)
        {
            dal.Delete(PermissionCategoryId);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int PermissionCategoryId)
        {
            return dal.IsExist(PermissionCategoryId);
        }
    }
}
