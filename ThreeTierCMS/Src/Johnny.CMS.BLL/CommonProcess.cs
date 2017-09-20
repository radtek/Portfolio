using System.Collections.Generic;

using Johnny.CMS.OM;
using Johnny.CMS.DAL;

namespace Johnny.CMS.BLL
{
    /// <summary>
    /// A business component to process common command
    /// </summary>
    public class CommonProcess
    {

        // Get an instance of the User DAL 
        // Making this static will cache the DAL instance after the initial load
        private static readonly DAL.CommonProcess dal = new DAL.CommonProcess();

        public void DeleteById(string strTable, string strKey, int strId)
        {
            // Use the dal to update a news
            dal.DeleteById(strTable, strKey, strId);
        }

        public void SetIsDisplay(string lblText, string strTable, string strKey, int strId)
        {
            // Use the dal to update a news
            dal.SetIsDisplay(lblText, strTable, strKey, strId);
        }

        public void ExchangeSequence(string tableName, string key, int id, int sequence, bool UpDown)
        {
            dal.ExchangeSequence(tableName, key, id, sequence, UpDown);
        }
    }
}
