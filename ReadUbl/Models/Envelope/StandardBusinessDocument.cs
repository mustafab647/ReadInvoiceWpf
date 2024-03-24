using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ReadUbl.Models.Envelope
{
    [XmlRoot(Namespace = "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader")]
    public class StandardBusinessDocument
    {

        XmlSerializerNamespaces xmlnsns;
        public StandardBusinessDocument()
        {
            xmlnsns = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            xmlnsns.Add("sh", "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader");
            xmlnsns.Add("ef", "http://www.efatura.gov.tr/package-namespace");
        }

        [XmlAttribute(Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string schemaLocation { get; set; }
        [XmlElement("StandardBusinessDocumentHeader",Namespace = "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader")]
        public StandardBusinessDocumentHeader Header { get; set; }
        [XmlElement(Namespace = "http://www.efatura.gov.tr/package-namespace")]
        public Package Package { get; set; }        
    }
}
