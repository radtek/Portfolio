using System.Collections.Generic;

using Johnny.CMS.OM;
using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL.SystemInfo
{

    /// <summary>
    /// A business component to manage BreviarySettings
    /// </summary>
    public class BreviarySettings
    {
        // Get an instance of the BreviarySettings DAL
        // Making this static will cache the DAL instance after the initial load
        private static readonly Johnny.CMS.DAL.SystemInfo.BreviarySettings dal = new Johnny.CMS.DAL.SystemInfo.BreviarySettings();

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SystemInfo.BreviarySettings> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SystemInfo.BreviarySettings GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// Add or update one record
        /// </summary>
        public void AddOrUpdate(Johnny.CMS.OM.SystemInfo.BreviarySettings model)
        {
            if (!dal.IsExist(1))
                dal.Add(model);
            else
                dal.Update(model);
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SystemInfo.BreviarySettings model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void Update(Johnny.CMS.OM.SystemInfo.BreviarySettings model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int id)
        {
            dal.Delete(id);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int id)
        {
            return dal.IsExist(id);
        }
    }
}