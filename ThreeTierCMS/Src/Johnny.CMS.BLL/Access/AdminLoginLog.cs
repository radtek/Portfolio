using System.Collections.Generic;

using Johnny.CMS.OM;
using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL.Access
{

	/// <summary>
	/// A business component to manage AdminLoginLog
	/// </summary>
	public class AdminLoginLog
	{
		// Get an instance of the User DAL
	    // Making this static will cache the DAL instance after the initial load
	    private static readonly Johnny.CMS.DAL.Access.AdminLoginLog dal = new Johnny.CMS.DAL.Access.AdminLoginLog();
	    
		/// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.Access.AdminLoginLog> GetList()
        {
            return dal.GetList();
        }
        
		/// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.Access.AdminLoginLog GetModel(int Id)
        {
			return dal.GetModel(Id);
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.Access.AdminLoginLog model)
        {
            return dal.Add(model);
        }
        
        /// <summary>
        /// Update one record
        /// </summary>
        public void Update(Johnny.CMS.OM.Access.AdminLoginLog model)
        {
        	dal.Update(model);
        }
        
        /// <summary>
		/// Delete record by primary key
		/// </summary>
        public void Delete(int Id)
        {
            dal.Delete(Id);
        }
        
        /// <summary>
		/// Check exist by primary key
		/// </summary>
        public bool IsExist(int Id)
        {
            return dal.IsExist(Id);
        }
	}
}
	