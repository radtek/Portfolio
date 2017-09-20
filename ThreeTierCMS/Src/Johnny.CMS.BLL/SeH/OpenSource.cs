using System.Collections.Generic;

using Johnny.CMS.OM;
using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL.SeH
{

    /// <summary>
    /// A business component to manage OpenSource
    /// </summary>
    public class OpenSource
    {
        // Get an instance of the OpenSource DAL
        // Making this static will cache the DAL instance after the initial load
        private static readonly Johnny.CMS.DAL.SeH.OpenSource dal = new Johnny.CMS.DAL.SeH.OpenSource();

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SeH.OpenSource> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SeH.OpenSource GetModel(int OpenSourceid)
        {
            return dal.GetModel(OpenSourceid);
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SeH.OpenSource model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void Update(Johnny.CMS.OM.SeH.OpenSource model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int OpenSourceid)
        {
            dal.Delete(OpenSourceid);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int OpenSourceid)
        {
            return dal.IsExist(OpenSourceid);
        }
    }
}