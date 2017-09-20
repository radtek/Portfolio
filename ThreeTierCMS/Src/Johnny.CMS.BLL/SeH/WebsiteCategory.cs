using System.Collections.Generic;

using Johnny.CMS.OM;
using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL.SeH
{

    /// <summary>
    /// A business component to manage WebsiteCategory
    /// </summary>
    public class WebsiteCategory
    {
        // Get an instance of the WebsiteCategory DAL
        // Making this static will cache the DAL instance after the initial load
        private static readonly Johnny.CMS.DAL.SeH.WebsiteCategory dal = new Johnny.CMS.DAL.SeH.WebsiteCategory();

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SeH.WebsiteCategory> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SeH.WebsiteCategory GetModel(int websitecategoryid)
        {
            return dal.GetModel(websitecategoryid);
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SeH.WebsiteCategory model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void Update(Johnny.CMS.OM.SeH.WebsiteCategory model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int websitecategoryid)
        {
            dal.Delete(websitecategoryid);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int websitecategoryid)
        {
            return dal.IsExist(websitecategoryid);
        }
    }
}