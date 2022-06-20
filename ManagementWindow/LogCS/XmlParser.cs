using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace BaseClass.Tool
{
    public class CXmlParser
    {
        private static CXmlParser m_instance;
        //private static object m_syncXmlParser;

        private XmlDocument m_domDocument;
        private string m_filename;
        private object m_syncObject;
        private string m_rightFilename;
        private object m_syncRightObject;
        private static object m_syncXmlParser = new object();

        static CXmlParser()
        {
            m_instance = new CXmlParser();
        }

        //public static CXmlParser Instance
        //{
        //    get
        //    {
        //        if (m_instance == null)
        //        {
        //            lock (m_syncXmlParser)
        //            {
        //                if (m_instance == null)
        //                {
        //                    m_instance = new CXmlParser();
        //                }
        //            }
        //        }
        //        return m_instance;
        //    }
        //}

        public static CXmlParser Instance
        {
            get
            {
                return m_instance;
            }
        }

        private CXmlParser()
        {
            try
            {
                m_syncObject = new object();
                m_filename = string.Format(@"{0}\SysConfig.xml", CGlobalParams.CurrentDirectory);
                m_domDocument = new XmlDocument();
                m_domDocument.Load(m_filename);
                m_syncRightObject = new object();
                m_rightFilename = string.Format(@"{0}\RightTree.xml", CGlobalParams.CurrentDirectory);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public XmlDocument DomDocument
        {
            get
            {
                return m_domDocument;
            }
        }

        public string GetAttributeValue(string elementName, string attributeName)
        {
            lock (m_syncObject)
            {
                XmlNodeList nodeList = m_domDocument.GetElementsByTagName(elementName);
                for (int i = 0; i < nodeList.Count; ++i)
                {
                    XmlNode node = nodeList[i].Attributes.GetNamedItem(attributeName);
                    if (node != null)
                    {
                        return node.Value;
                    }
                }
            }
            return null;
        }

        public void SetAttributeValue(string elementName, string attributeName, string attributeValue)
        {
            lock (m_syncObject)
            {
                XmlNodeList nodeList = m_domDocument.GetElementsByTagName(elementName);
                for (int i = 0; i < nodeList.Count; ++i)
                {
                    XmlNode node = nodeList[i].Attributes.GetNamedItem(attributeName);
                    if (node != null)
                    {
                        node.Value = attributeValue;
                        m_domDocument.Save(m_filename);
                    }
                }
            }
        }

        public bool GetIoLogic()
        {
            if (GetAttributeValue("Io", "Logic") == "0")
                return false;
            return true;
        }

        public bool InitMesContext()
        {
            string strValue = GetAttributeValue("InitMesContext", "Enabled");
            if (strValue == null)
                return false;
            if (strValue == "0")
                return false;
            return true;
        }

        public int GetCarrierCount()
        {
            try
            {
                return int.Parse((CXmlParser.Instance.GetAttributeValue("Agv", "Count") == null) ? "0" : CXmlParser.Instance.GetAttributeValue("Agv", "Count"));
            }
            catch (Exception ex)
            {
                CMyLog.Instance.WriteLog(LogLevel.ERROR_LOG_LEVEL, string.Format("GetCarrierCount() --- {0}", ex.ToString()));
                return 0;
            }
        }

        public string GetRightTree()
        {
            lock (m_syncRightObject)
            {
                try
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(m_rightFilename);
                    return document.InnerXml;
                }
                catch (Exception ex)
                {
                    CMyLog.Instance.WriteLog(LogLevel.ERROR_LOG_LEVEL, string.Format("GetRightTree() --- {0}", ex.ToString()));
                    return null;
                }
            }
        }
    }
}
