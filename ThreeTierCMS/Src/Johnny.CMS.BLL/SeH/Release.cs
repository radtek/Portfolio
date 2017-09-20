using System.Collections.Generic;

using Johnny.CMS.OM;
using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL.SeH
{

    /// <summary>
    /// A business component to manage Release
    /// </summary>
    public class Release
    {
        // Get an instance of the Release DAL
        // Making this static will cache the DAL instance after the initial load
        private static readonly Johnny.CMS.DAL.SeH.Release dal = new Johnny.CMS.DAL.SeH.Release();

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SeH.Release> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SeH.Release> GetList(int softwareid)
        {
            return dal.GetList(softwareid);
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SeH.Release GetModel(int releaseid)
        {
            return dal.GetModel(releaseid);
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SeH.Release GetLatestModel(int softwareid)
        {
            return dal.GetLatestModel(softwareid);
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SeH.Release model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void Update(Johnny.CMS.OM.SeH.Release model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int releaseid)
        {
            dal.Delete(releaseid);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int releaseid)
        {
            return dal.IsExist(releaseid);
        }
    }
}