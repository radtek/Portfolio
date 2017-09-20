using System.Collections.Generic;

using Johnny.CMS.OM;
using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL.SeH
{

    /// <summary>
    /// A business component to manage Channel
    /// </summary>
    public class Channel
    {
        // Get an instance of the Channel DAL
        // Making this static will cache the DAL instance after the initial load
        private static readonly Johnny.CMS.DAL.SeH.Channel dal = new Johnny.CMS.DAL.SeH.Channel();

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SeH.Channel> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SeH.Channel GetModel(int ChannelId)
        {
            return dal.GetModel(ChannelId);
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SeH.Channel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void Update(Johnny.CMS.OM.SeH.Channel model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int ChannelId)
        {
            dal.Delete(ChannelId);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int ChannelId)
        {
            return dal.IsExist(ChannelId);
        }
    }
}
