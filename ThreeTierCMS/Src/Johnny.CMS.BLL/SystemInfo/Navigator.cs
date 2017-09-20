using System.Collections.Generic;

using Johnny.CMS.OM;
using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL.SystemInfo
{

    /// <summary>
    /// A business component to manage Navigator
    /// </summary>
    public class Navigator
    {
        // Get an instance of the Navigator DAL
        // Making this static will cache the DAL instance after the initial load
        private static readonly Johnny.CMS.DAL.SystemInfo.Navigator dal = new Johnny.CMS.DAL.SystemInfo.Navigator();

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SystemInfo.Navigator> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SystemInfo.Navigator GetModel(int navigatorid)
        {
            return dal.GetModel(navigatorid);
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SystemInfo.Navigator model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void Update(Johnny.CMS.OM.SystemInfo.Navigator model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int navigatorid)
        {
            dal.Delete(navigatorid);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int navigatorid)
        {
            return dal.IsExist(navigatorid);
        }
    }
}