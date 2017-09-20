using System.Collections.Generic;

using Johnny.CMS.OM;
using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL.SeH
{

    /// <summary>
    /// A business component to manage BlogCategory
    /// </summary>
    public class BlogCategory
    {
        // Get an instance of the BlogCategory DAL
        // Making this static will cache the DAL instance after the initial load
        private static readonly Johnny.CMS.DAL.SeH.BlogCategory dal = new Johnny.CMS.DAL.SeH.BlogCategory();

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SeH.BlogCategory> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SeH.BlogCategory GetModel(int blogcategoryid)
        {
            return dal.GetModel(blogcategoryid);
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SeH.BlogCategory model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void Update(Johnny.CMS.OM.SeH.BlogCategory model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int blogcategoryid)
        {
            dal.Delete(blogcategoryid);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int blogcategoryid)
        {
            return dal.IsExist(blogcategoryid);
        }
    }
}
