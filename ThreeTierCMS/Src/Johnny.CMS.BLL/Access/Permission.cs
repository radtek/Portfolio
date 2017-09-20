using System.Collections.Generic;

using Johnny.CMS.OM;
using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL.Access
{

    /// <summary>
    /// A business component to manage Permission
    /// </summary>
    public class Permission
    {
        // Get an instance of the User DAL
        // Making this static will cache the DAL instance after the initial load
        private static readonly Johnny.CMS.DAL.Access.Permission dal = new Johnny.CMS.DAL.Access.Permission();

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.Access.Permission> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.Access.Permission> GetList(int permissioncategoryid)
        {
            return dal.GetList(permissioncategoryid);
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.Access.Permission GetModel(int permissionid)
        {
            return dal.GetModel(permissionid);
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.Access.Permission model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void Update(Johnny.CMS.OM.Access.Permission model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int PermissionId)
        {
            dal.Delete(PermissionId);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int PermissionId)
        {
            return dal.IsExist(PermissionId);
        }
    }
}
