using System.Collections.Generic;

using Johnny.CMS.OM;
using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL.Access
{

    /// <summary>
    /// A business component to manage Adminrole
    /// </summary>
    public class AdminRole
    {
        // Get an instance of the User DAL
        // Making this static will cache the DAL instance after the initial load
        private static readonly Johnny.CMS.DAL.Access.AdminRole dal = new Johnny.CMS.DAL.Access.AdminRole();

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.Access.AdminRole> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.Access.AdminRole GetModel(int AdminRoleId)
        {
            return dal.GetModel(AdminRoleId);
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.Access.AdminRole model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void Update(Johnny.CMS.OM.Access.AdminRole model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int AdminRoleId)
        {
            dal.Delete(AdminRoleId);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int AdminRoleId)
        {
            return dal.IsExist(AdminRoleId);
        }
    }
}
