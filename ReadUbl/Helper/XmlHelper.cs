using ReadUbl.Models.Invoice;
using ReadUbl.RIWInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ReadUbl.Helper
{
    public static class XmlHelper<RIW_UblItem>
    {
        public static string Serialize(RIW_UblItem invoice)
        {
            string result = "";
            XmlSerializer serializer = new XmlSerializer(typeof(RIW_UblItem));
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

        public static RIW_UblItem DeSerialize(string xmlStr)
        {
            RIW_UblItem result;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlStr);
                Type xmlType = Helper.GetModelType(xmlDoc);
                XmlReader xmlReader = new XmlNodeReader(xmlDoc);
                xmlStr = xmlStr.Trim();
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(xmlStr);
                using (MemoryStream ms = new MemoryStream(buffer))
                {
                    XmlSerializer serializer = new XmlSerializer(xmlType);
                    result = (RIW_UblItem)serializer.Deserialize(xmlReader);
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
