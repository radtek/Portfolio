using System.Collections.Generic;

using Johnny.CMS.OM;
using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL.SeH
{

    /// <summary>
    /// A business component to manage Bestpractice
    /// </summary>
    public class BestPractice
    {
        // Get an instance of the BestPractice DAL
        // Making this static will cache the DAL instance after the initial load
        private static readonly Johnny.CMS.DAL.SeH.BestPractice dal = new Johnny.CMS.DAL.SeH.BestPractice();

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SeH.BestPractice> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SeH.BestPractice GetModel(int bestpracticeid)
        {
            return dal.GetModel(bestpracticeid);
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SeH.BestPractice model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void Update(Johnny.CMS.OM.SeH.BestPractice model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int bestpracticeid)
        {
            dal.Delete(bestpracticeid);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int bestpracticeid)
        {
            return dal.IsExist(bestpracticeid);
        }
    }
}