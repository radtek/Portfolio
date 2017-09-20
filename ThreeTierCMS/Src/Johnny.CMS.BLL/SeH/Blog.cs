using System.Collections.Generic;

using Johnny.CMS.OM;
using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL.SeH
{

    /// <summary>
    /// A business component to manage Blog
    /// </summary>
    public class Blog
    {
        // Get an instance of the Blog DAL
        // Making this static will cache the DAL instance after the initial load
        private static readonly Johnny.CMS.DAL.SeH.Blog dal = new Johnny.CMS.DAL.SeH.Blog();

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SeH.Blog> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SeH.Blog GetModel(int blogid)
        {
            return dal.GetModel(blogid);
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SeH.Blog model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void Update(Johnny.CMS.OM.SeH.Blog model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int blogid)
        {
            dal.Delete(blogid);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int blogid)
        {
            return dal.IsExist(blogid);
        }
    }
}
