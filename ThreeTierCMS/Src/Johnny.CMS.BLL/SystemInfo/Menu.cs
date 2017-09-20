using System.Collections.Generic;

using Johnny.CMS.OM;
using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL.SystemInfo
{

    /// <summary>
    /// A business component to manage Menu
    /// </summary>
    public class Menu
    {
        // Get an instance of the User DAL
        // Making this static will cache the DAL instance after the initial load
        private static readonly Johnny.CMS.DAL.SystemInfo.Menu dal = new Johnny.CMS.DAL.SystemInfo.Menu();

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SystemInfo.Menu> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SystemInfo.Menu> GetListByCategory(int menucategoryid)
        {
            return dal.GetListByCategory(menucategoryid);
        }

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SystemInfo.Menu> GetListForLink()
        {
            IList<Johnny.CMS.OM.SystemInfo.Menu> list = dal.GetList();
            foreach (Johnny.CMS.OM.SystemInfo.Menu item in list)
            {
                item.PageLink = "../" + item.PageLink;
            }
            return list;
        }

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public IList<Johnny.CMS.OM.SystemInfo.Menu> GetList(bool isdisplay)
        {
            return dal.GetList(isdisplay);
        }

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public int GetPermissionByPageLink(string pagelink)
        {
            return dal.GetPermissionByPageLink(pagelink);
        }

        /// <summary>
        /// Method to get one record by primary key
        /// </summary>    	 
        public Johnny.CMS.OM.SystemInfo.Menu GetModel(int MenuId)
        {
            return dal.GetModel(MenuId);
        }

        /// <summary>
        /// Add one record
        /// </summary>
        public int Add(Johnny.CMS.OM.SystemInfo.Menu model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// Update one record
        /// </summary>
        public void Update(Johnny.CMS.OM.SystemInfo.Menu model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// Delete record by primary key
        /// </summary>
        public void Delete(int MenuId)
        {
            dal.Delete(MenuId);
        }

        /// <summary>
        /// Check exist by primary key
        /// </summary>
        public bool IsExist(int MenuId)
        {
            return dal.IsExist(MenuId);
        }
    }
}
