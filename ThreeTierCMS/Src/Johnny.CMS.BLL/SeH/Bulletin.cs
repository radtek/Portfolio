using System.Collections.Generic;

using Johnny.CMS.OM;
using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL.SeH
{

    /// <summary>
    /// A business component to manage Bulletin
    /// </summary>
    public class Bulletin
    {
        // Get an instance of the Bulletin DAL
        // Making this static will cache the DAL instance after the initial load
        private static readonly Johnny.CMS.DAL.SeH.Bulletin dal = new Johnny.CMS.DAL.SeH.Bulletin();

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SeH.Bulletin> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SeH.Bulletin GetModel(int bulletinid)
        {
            return dal.GetModel(bulletinid);
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SeH.Bulletin model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void Update(Johnny.CMS.OM.SeH.Bulletin model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int bulletinid)
        {
            dal.Delete(bulletinid);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int bulletinid)
        {
            return dal.IsExist(bulletinid);
        }
    }
}