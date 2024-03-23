using ReadUbl.Models.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ReadUbl.Helper
{
    public static class XmlHelper<T>
    {
        public static string Serialize(T invoice)
        {
            string result = "";
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringWriter sw = new StringWriter())
            {
                serializer.Serialize(sw, invoice);
                result = sw.ToString().Trim();
            }
            return result;
        }

        public static Type AsXmlType(string xmlStr)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlStr);
                return Helper.GetModelType(xmlDoc);
            }
            catch { }
            return null;
        }

        public static T DeSerialize(string xmlStr)
        {
            T result;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlStr);
                XmlReader xmlReader = new XmlNodeReader(xmlDoc);
                xmlStr = xmlStr.Trim();
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(xmlStr);
                using (MemoryStream ms = new MemoryStream(buffer))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    result = (T)serializer.Deserialize(xmlReader);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
