using System.Collections.Generic;

using Johnny.CMS.OM;
using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL.SeH
{

    /// <summary>
    /// A business component to manage Article
    /// </summary>
    public class Article
    {
        // Get an instance of the Article DAL
        // Making this static will cache the DAL instance after the initial load
        private static readonly Johnny.CMS.DAL.SeH.Article dal = new Johnny.CMS.DAL.SeH.Article();

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SeH.Article> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SeH.Article> GetList(int channelid, int? top)
        {
            return dal.GetList(channelid, top);
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SeH.Article GetModel(int ArticleId)
        {
            return dal.GetModel(ArticleId);
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SeH.Article model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void Update(Johnny.CMS.OM.SeH.Article model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int ArticleId)
        {
            dal.Delete(ArticleId);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int ArticleId)
        {
            return dal.IsExist(ArticleId);
        }
    }
}
