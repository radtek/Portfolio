using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;

namespace Johnny.Component.Utility
{
    public class XmlUtility
    {
        protected string strXmlFile;
        protected XmlDocument objXmlDoc = new XmlDocument();

        public XmlUtility(string XmlFile)
        {
            //
            // TODO: 在這裡加入建構函式的程式碼
            //
            try
            {
                objXmlDoc.Load(XmlFile);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            strXmlFile = XmlFile;
        }

        public XmlDocument GetXmlFile()
        {
            return objXmlDoc;
        }
        public DataView GetData(string XmlPathNode)
        {
            //查找數據。返回一個DataView
            DataSet ds = new DataSet();
            StringReader read = new StringReader(objXmlDoc.SelectSingleNode(XmlPathNode).OuterXml);
            ds.ReadXml(read);
            return ds.Tables[0].DefaultView;
        }

        public XmlNode GetXmlNode(string XmlPathNode)
        {
            //查找數據。返回一個DataView
            return objXmlDoc.SelectSingleNode("//msg[@id='" + XmlPathNode + "']");
        }

        public void Replace(string XmlPathNode, string Content)
        {
            //更新節點內容。
            objXmlDoc.SelectSingleNode(XmlPathNode).InnerText = Content;
        }

        public void Delete(string Node)
        {
            //刪除一個節點。
            string mainNode = Node.Substring(0, Node.LastIndexOf("/"));
            objXmlDoc.SelectSingleNode(mainNode).RemoveChild(objXmlDoc.SelectSingleNode(Node));
        }

        public void InsertNode(string MainNode, string ChildNode, string Element, string Content)
        {
            //插入一節點和此節點的一子節點。
            XmlNode objRootNode = objXmlDoc.SelectSingleNode(MainNode);
            XmlElement objChildNode = objXmlDoc.CreateElement(ChildNode);
            objRootNode.AppendChild(objChildNode);
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objElement.InnerText = Content;
            objChildNode.AppendChild(objElement);
        }

        public void InsertElement(string MainNode, string Element, string Attrib, string AttribContent, string Content)
        {
            //插入一個節點，帶一屬性。
            XmlNode objNode = objXmlDoc.SelectSingleNode(MainNode);
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objElement.SetAttribute(Attrib, AttribContent);
            objElement.InnerText = Content;
            objNode.AppendChild(objElement);
        }

        public void InsertElement(string MainNode, string Element, string Content)
        {
            //插入一個節點，不帶屬性。
            XmlNode objNode = objXmlDoc.SelectSingleNode(MainNode);
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objElement.InnerText = Content;
            objNode.AppendChild(objElement);
        }

        public void Save()
        {
            //保存文檔。
            try
            {
                objXmlDoc.Save(strXmlFile);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            objXmlDoc = null;
        }
    }


}
