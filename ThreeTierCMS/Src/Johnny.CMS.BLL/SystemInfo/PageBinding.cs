using System.Collections.Generic;

using Johnny.CMS.OM;
using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL.SystemInfo
{

    /// <summary>
    /// A business component to manage Pagebinding
    /// </summary>
    public class PageBinding
    {
        // Get an instance of the Pagebinding DAL
        // Making this static will cache the DAL instance after the initial load
        private static readonly Johnny.CMS.DAL.SystemInfo.PageBinding dal = new Johnny.CMS.DAL.SystemInfo.PageBinding();

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SystemInfo.PageBinding> GetList()
        {
            IList<Johnny.CMS.OM.SystemInfo.PageBinding> list = dal.GetList();
            foreach (Johnny.CMS.OM.SystemInfo.PageBinding item in list)
            {
                item.ListPageLink = "../" + item.ListPageLink;
                item.AddPageLink = "../" + item.AddPageLink;
            }
            return list;
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SystemInfo.PageBinding GetModel(int pagebindingid)
        {
            return dal.GetModel(pagebindingid);
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SystemInfo.PageBinding model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void Update(Johnny.CMS.OM.SystemInfo.PageBinding model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int pagebindingid)
        {
            dal.Delete(pagebindingid);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int pagebindingid)
        {
            return dal.IsExist(pagebindingid);
        }
    }
}
