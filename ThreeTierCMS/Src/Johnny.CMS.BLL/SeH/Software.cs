using System.Collections.Generic;

using Johnny.CMS.OM;
using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL.SeH
{

    /// <summary>
    /// A business component to manage Software
    /// </summary>
    public class Software
    {
        // Get an instance of the Software DAL
        // Making this static will cache the DAL instance after the initial load
        private static readonly Johnny.CMS.DAL.SeH.Software dal = new Johnny.CMS.DAL.SeH.Software();

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SeH.Software> GetList(int? top)
        {
            return dal.GetList(top);
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SeH.Software GetModel(int softwareid)
        {
            return dal.GetModel(softwareid);
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SeH.Software model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void Update(Johnny.CMS.OM.SeH.Software model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int softwareid)
        {
            dal.Delete(softwareid);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int softwareid)
        {
            return dal.IsExist(softwareid);
        }
    }
}