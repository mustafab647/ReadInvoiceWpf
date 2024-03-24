using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ReadUbl.Models.Envelope
{
    public class DocumentIdentification
    {
        [XmlElement(Namespace = "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader")]
        public string Standard { get; set; }
        [XmlElement(Namespace = "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader")]
        public string TypeVersion { get; set; }
        [XmlElement(Namespace = "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader")]
        public Guid InstanceIdentifier { get; set; }
        [XmlElement(Namespace = "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader")]
        public string Type { get; set; }
        [XmlElement(Namespace = "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader")]
        public DateTime CreationDateAndTime { get; set; }
    }
}
