using System.Collections.Generic;

using Johnny.CMS.OM;
using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL.Access
{

    /// <summary>
    /// A business component to manage Rolepermission
    /// </summary>
    public class RolePermission
    {
        // Get an instance of the User DAL
        // Making this static will cache the DAL instance after the initial load
        private static readonly Johnny.CMS.DAL.Access.RolePermission dal = new Johnny.CMS.DAL.Access.RolePermission();

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.Access.RolePermission> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.Access.RolePermission> GetList(int roleid, int permissioncategoryid)
        {
            return dal.GetList(roleid, permissioncategoryid);
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.Access.RolePermission GetModel(int RoleId)
        {
            return dal.GetModel(RoleId);
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.Access.RolePermission model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void Update(Johnny.CMS.OM.Access.RolePermission model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int RoleId)
        {
            dal.Delete(RoleId);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int RoleId)
        {
            return dal.IsExist(RoleId);
        }
    }
}
