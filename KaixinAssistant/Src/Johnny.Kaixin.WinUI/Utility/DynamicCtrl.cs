using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Forms;

using Johnny.Kaixin.Helper;
using Johnny.Library.Helper;

namespace Johnny.Kaixin.WinUI.Utility
{
    public sealed class DynamicCtrl
    {
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

        private static string GetFolderName(string path)
        {
            if (path == null || path == string.Empty)
                return "";
            return path.Substring(path.LastIndexOf("\\") + 1);
        }

        private static XmlNode GetAppendNode(XmlNode parentNode, XmlDocument objXmlDoc, string nodename, string defaultvalue)
        {
            XmlNode target = parentNode.SelectSingleNode(nodename);
            if (target == null)
            {
                XmlElement objChildNode = objXmlDoc.CreateElement(nodename);
                if (!String.IsNullOrEmpty(defaultvalue))
                    objChildNode.InnerText = defaultvalue;
                parentNode.AppendChild(objChildNode);
                target = parentNode.SelectSingleNode(nodename);
            }
            return target;
        }

        private static string GetResourceFile(string file)
        {
            string filename = "Johnny.Kaixin.WinUI.Config." + file;
            using (StreamReader streamReader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(filename)))
            {
                return streamReader.ReadToEnd();
            }
        }
        #endregion

        #region GetToolboxItemsConfigFile
        public static XmlDocument GetToolboxItemsConfigFile()
        {
            try
            {
                //load config info
                string folder = Path.Combine(Application.StartupPath, MainConstants.FOLDER_CONFIG);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                string configFile = folder + "\\" + MainConstants.FILE_TOOLBOXITEMS;
                if (!File.Exists(configFile))
                {
                    string configContent = GetResourceFile(MainConstants.FILE_TOOLBOXITEMS);
                    StreamWriter sw = new StreamWriter(configFile);
                    sw.Write(configContent);
                    sw.Close();
                    sw = null;
                }

                XmlDocument objXmlDoc = new XmlDocument();

                objXmlDoc.Load(configFile);

                return objXmlDoc;
            }            
            catch (Exception ex)
            {
                LogHelper.Write("GetToolboxItemsConfigFile", ex);
                return null;
            }
        }
        #endregion  

        #region GetOpenFileMenuConfigFile
        public static DataView GetOpenFileMenuConfigFile()
        {
            try
            {
                //load config info
                string folder = Path.Combine(Application.StartupPath, MainConstants.FOLDER_CONFIG);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                string configFile = folder + "\\" + MainConstants.FILE_OPENFILEMENUITEMS;
                if (!File.Exists(configFile))
                {
                    string configContent = GetResourceFile(MainConstants.FILE_OPENFILEMENUITEMS);
                    StreamWriter sw = new StreamWriter(configFile);
                    sw.Write(configContent);
                    sw.Close();
                    sw = null;
                }

                XmlDocument objXmlDoc = new XmlDocument();

                objXmlDoc.Load(configFile);

                DataView dv = GetData(objXmlDoc, "OpenFileMenu");

                return dv;
            }
            catch (Exception ex)
            {
                LogHelper.Write("GetOpenFileMenuConfigFile", ex);
                return null;
            }
        }
        #endregion  
    }
}
