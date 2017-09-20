using System.Collections.Generic;

using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL.Access
{

    /// <summary>
    /// A business component to manage cms_administrator
    /// </summary>
    public class Administrator
    {
        // Get an instance of the User DAL
        // Making this static will cache the DAL instance after the initial load
        private static readonly Johnny.CMS.DAL.Access.Administrator dal = new Johnny.CMS.DAL.Access.Administrator();

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.Access.Administrator> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.Access.Administrator GetModel(int AdminId)
        {
            return dal.GetModel(AdminId);
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.Access.Administrator model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void Update(Johnny.CMS.OM.Access.Administrator model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void UpdatePersonal(Johnny.CMS.OM.Access.Administrator model)
        {
            dal.UpdatePersonal(model);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int AdminId)
        {
            dal.Delete(AdminId);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int AdminId)
        {
            return dal.IsExist(AdminId);
        }

        /// <summary>
        /// Check exist by name
        /// </summary>
        public bool IsExist(string name)
        {
            return dal.IsExist(name);
        }

        /// <summary>
        /// Get userid by username
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetUserIdByName(string name)
        {
            return dal.GetUserIdByName(name);
        }
        /// <summary>
        /// Search for a user given it's unique identifier
        /// </summary>
        /// <param name="categoryId">Unique identifier for a User</param>
        /// <returns>A Category business entity</returns>
        public bool CheckLogin(string name, string password)
        {

            // Validate input
            if (string.IsNullOrEmpty(name))
                return false;
            if (string.IsNullOrEmpty(password))
                return false;

            // Use the dal to search by user Id
            return dal.CheckLogin(name, password);
        }

        /// <summary>
        /// Search for a user given it's unique identifier
        /// </summary>
        /// <param name="categoryId">Unique identifier for a User</param>
        /// <returns>A Category business entity</returns>
        public void UpdateLoginTimes(string name)
        {
            dal.UpdateLoginTimes(name);
        }

        /// <summary>
        /// Reset password
        /// </summary>
        /// <param name="model"></param>
        public void ResetPassword(Johnny.CMS.OM.Access.Administrator model)
        {
            dal.ResetPassword(model);
        }
    }
}
