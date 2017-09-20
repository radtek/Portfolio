using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Data;

namespace Johnny.Kaixin.AutoUpdate
{
    class XmlUtility
    {
        public static UpdateInfo GetUpdateInfo(string strxml)
        {
            try
            {
                if (String.IsNullOrEmpty(strxml))
                {
                    //return null;
                    using (StreamReader streamReader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Johnny.Kaixin.AutoUpdate.update.xml")))
                    {
                        strxml = streamReader.ReadToEnd();
                    }
                }

                XmlDocument objXmlDoc = new XmlDocument();
                objXmlDoc.LoadXml(strxml);

                if (objXmlDoc == null)
                    return null;
                                
                //root node
                XmlNode objRootNode = objXmlDoc.SelectSingleNode("AutoUpdater");
                if (objRootNode == null)
                    return null;

                UpdateInfo updateOM = new UpdateInfo();

                XmlNode objNode;
                objNode = objRootNode.SelectSingleNode("URLAddress");
                updateOM.UrlAddress = objNode.Attributes["URL"].Value;

                objNode = objRootNode.SelectSingleNode("ReleaseInfo/UpdateTime");
                updateOM.UpdateTime = objNode.Attributes["Date"].Value;

                objNode = objRootNode.SelectSingleNode("ReleaseInfo/Version");
                updateOM.Version = objNode.Attributes["Num"].Value;

                DataView dv = GetData(objXmlDoc, "AutoUpdater/UpdateFileList");

                string[] files =new string[dv.Table.Rows.Count];

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    files[ix] = dv.Table.Rows[ix][0].ToString();
                }
                updateOM.UpdateFileList = files;

                //deletion directory
                dv = GetData(objXmlDoc, "AutoUpdater/DeletionDirectoryList");

                string[] delFolders = new string[dv.Table.Rows.Count];

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    delFolders[ix] = dv.Table.Rows[ix][0].ToString();
                }
                updateOM.DeletionDirectoryList = delFolders;

                //deletion file
                dv = GetData(objXmlDoc, "AutoUpdater/DeletionFileList");

                string[] delFiles = new string[dv.Table.Rows.Count];

                for (int ix = 0; ix < dv.Table.Rows.Count; ix++)
                {
                    delFiles[ix] = dv.Table.Rows[ix][0].ToString();
                }
                updateOM.DeletionFileList = delFiles;

                objNode = objRootNode.SelectSingleNode("RestartApp/ReStart");
                updateOM.ReStart = Convert.ToBoolean(objNode.Attributes["Allow"].Value);

                objNode = objRootNode.SelectSingleNode("RestartApp/AppName");
                updateOM.AppName = objNode.Attributes["Name"].Value;

                return updateOM;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #region Private Methods
        private static DataView GetData(XmlDocument xmldoc, string XmlPathNode)
        {
            //get data from xml file
            DataSet ds = new DataSet();
            DataView dv = new DataView();

            XmlNode node = xmldoc.SelectSingleNode(XmlPathNode);
            if (node == null)
                dv.Table = new DataTable("table0");
            else
            {
                StringReader read = new StringReader(node.OuterXml);

                ds.ReadXml(read);
                if (ds.Tables.Count < 1)
                    dv.Table = new DataTable("table0");
                else
                    dv = ds.Tables[0].DefaultView;
            }

            return dv;
        }
        #endregion
    }
}
