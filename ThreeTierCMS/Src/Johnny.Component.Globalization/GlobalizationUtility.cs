using Johnny.Component.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Johnny.Component.Globalization
{
    public class GlobalizationUtility
    {
        public static string GlobalizationCulture;
        public static string GetLabelText(string key)
        {
            if (String.IsNullOrEmpty(key))
                return "";

            string xmlFile = "";


            if (GlobalizationCulture == "zh-cn")
                xmlFile = "Label_zh-CHS.xml";
            else if (GlobalizationCulture == "en-us")
                xmlFile = "Label_en-US.xml";
            else
                xmlFile = "Label_en-US.xml";

            XmlDocument xmlDoc = CacheUtility.GetCache(xmlFile) as XmlDocument;

            if (xmlDoc == null)
            {
                xmlDoc = new XmlDocument();
                try
                {
                    Assembly asm = Assembly.GetExecutingAssembly();
                    XmlTextReader reader = new XmlTextReader(asm.GetManifestResourceStream(asm.GetName().Name + ".Label." + xmlFile));
                    xmlDoc.Load(reader);
                }
                catch (Exception ex)
                {
                }
                if (xmlDoc != null)
                {
                    //add file to cache
                    CacheUtility.InsertCache(xmlFile, xmlDoc);
                }
                else return "";
            }

            XmlNode node1 = xmlDoc.SelectSingleNode("//label[@key='" + key + "']");
            if (node1 == null)
                return "";

            XmlAttributeCollection attr;
            attr = node1.Attributes;
            if (attr == null) return "";

            return attr["value"].Value;
        }

        public static string GetMessage(string msgId)
        {
            return GetMessage(msgId, "");
        }
        public static string GetMessage(string msgId, string param)
        {

            if (msgId == null || msgId.Length == 0)
                return "";

            string xmlFile = "";

            if (GlobalizationCulture == "zh-cn")
                xmlFile = "Message_zh-CHS.xml";
            else if (GlobalizationCulture == "en-us")
                xmlFile = "Message_en-US.xml";
            else
                xmlFile = "Message_en-US.xml";

            XmlDocument xmlDoc = CacheUtility.GetCache(xmlFile) as XmlDocument;

            if (xmlDoc == null)
            {
                xmlDoc = new XmlDocument();
                try
                {
                    Assembly asm = Assembly.GetExecutingAssembly();
                    XmlTextReader reader = new XmlTextReader(asm.GetManifestResourceStream(asm.GetName().Name + ".Message." + xmlFile));
                    xmlDoc.Load(reader);
                }
                catch (Exception ex)
                {
                }
                if (xmlDoc != null)
                {
                    //add file to cache
                    CacheUtility.InsertCache(xmlFile, xmlDoc);
                }
                else return "";
            }

            XmlNode node1 = xmlDoc.SelectSingleNode("//msg[@id='" + msgId + "']");
            if (node1 == null)
                return "";

            XmlAttributeCollection attr;
            attr = node1.Attributes;
            if (attr == null) return "";

            if (param == string.Empty)
                return attr["text"].Value;
            else
                return String.Format(attr["text"].Value, param);
        }

    }
}
