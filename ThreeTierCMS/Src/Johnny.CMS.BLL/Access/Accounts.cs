using System.Collections;

using Johnny.CMS.OM;
using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL.Access
{

    /// <summary>
    /// A business component to manage Menu
    /// </summary>
    public class Accounts
    {
        // Get an instance of the User DAL
        // Making this static will cache the DAL instance after the initial load
        private static readonly Johnny.CMS.DAL.Access.Accounts dal = new Johnny.CMS.DAL.Access.Accounts();

        /// <summary>
        /// Method to get records with condition
        /// </summary>    	 
        public ArrayList GetUserPermission(string strUserName)
        {
            return dal.GetUserPermission(strUserName);
        }
        
    }
}
