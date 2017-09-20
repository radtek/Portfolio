using System.Collections.Generic;

using Johnny.CMS.OM;
using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL.SystemInfo
{

    /// <summary>
    /// A business component to manage Mailsettings
    /// </summary>
    public class MailSettings
    {
        // Get an instance of the User DAL
        // Making this static will cache the DAL instance after the initial load
        private static readonly Johnny.CMS.DAL.SystemInfo.MailSettings dal = new Johnny.CMS.DAL.SystemInfo.MailSettings();

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SystemInfo.MailSettings> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SystemInfo.MailSettings GetModel(int Id)
        {
            return dal.GetModel(Id);
        }

        /// <summary>
        /// Add or update one record
        /// </summary>
        public void AddOrUpdate(Johnny.CMS.OM.SystemInfo.MailSettings model)
        {
            if (!dal.IsExist(1))
                dal.Add(model);
            else
                dal.Update(model);
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SystemInfo.MailSettings model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void Update(Johnny.CMS.OM.SystemInfo.MailSettings model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int Id)
        {
            dal.Delete(Id);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int Id)
        {
            return dal.IsExist(Id);
        }
    }
}
