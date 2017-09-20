using System.Collections.Generic;

using Johnny.CMS.OM;
using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL.SeH
{

    /// <summary>
    /// A business component to manage Website
    /// </summary>
    public class Website
    {
        // Get an instance of the Website DAL
        // Making this static will cache the DAL instance after the initial load
        private static readonly Johnny.CMS.DAL.SeH.Website dal = new Johnny.CMS.DAL.SeH.Website();

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SeH.Website> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SeH.Website> GetList(int top)
        {
            return dal.GetList(top);
        }

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SeH.Website> GetListByCategoryId(int categoryid)
        {
            return dal.GetListByCategoryId(categoryid);
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SeH.Website GetModel(int websiteid)
        {
            return dal.GetModel(websiteid);
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SeH.Website model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void Update(Johnny.CMS.OM.SeH.Website model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int websiteid)
        {
            dal.Delete(websiteid);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int websiteid)
        {
            return dal.IsExist(websiteid);
        }
    }
}