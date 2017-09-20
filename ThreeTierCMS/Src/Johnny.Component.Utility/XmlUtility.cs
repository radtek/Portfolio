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
            // TODO: ���@�e���뽨����ʽ�ĳ�ʽ�a
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
            //���Ҕ���������һ��DataView
            DataSet ds = new DataSet();
            StringReader read = new StringReader(objXmlDoc.SelectSingleNode(XmlPathNode).OuterXml);
            ds.ReadXml(read);
            return ds.Tables[0].DefaultView;
        }

        public XmlNode GetXmlNode(string XmlPathNode)
        {
            //���Ҕ���������һ��DataView
            return objXmlDoc.SelectSingleNode("//msg[@id='" + XmlPathNode + "']");
        }

        public void Replace(string XmlPathNode, string Content)
        {
            //���¹��c���ݡ�
            objXmlDoc.SelectSingleNode(XmlPathNode).InnerText = Content;
        }

        public void Delete(string Node)
        {
            //�h��һ�����c��
            string mainNode = Node.Substring(0, Node.LastIndexOf("/"));
            objXmlDoc.SelectSingleNode(mainNode).RemoveChild(objXmlDoc.SelectSingleNode(Node));
        }

        public void InsertNode(string MainNode, string ChildNode, string Element, string Content)
        {
            //����һ���c�ʹ˹��c��һ�ӹ��c��
            XmlNode objRootNode = objXmlDoc.SelectSingleNode(MainNode);
            XmlElement objChildNode = objXmlDoc.CreateElement(ChildNode);
            objRootNode.AppendChild(objChildNode);
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objElement.InnerText = Content;
            objChildNode.AppendChild(objElement);
        }

        public void InsertElement(string MainNode, string Element, string Attrib, string AttribContent, string Content)
        {
            //����һ�����c����һ���ԡ�
            XmlNode objNode = objXmlDoc.SelectSingleNode(MainNode);
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objElement.SetAttribute(Attrib, AttribContent);
            objElement.InnerText = Content;
            objNode.AppendChild(objElement);
        }

        public void InsertElement(string MainNode, string Element, string Content)
        {
            //����һ�����c���������ԡ�
            XmlNode objNode = objXmlDoc.SelectSingleNode(MainNode);
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objElement.InnerText = Content;
            objNode.AppendChild(objElement);
        }

        public void Save()
        {
            //�����ęn��
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
