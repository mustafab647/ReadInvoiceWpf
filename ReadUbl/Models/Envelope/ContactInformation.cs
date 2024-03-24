using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ReadUbl.Models.Envelope
{
    public class ContactInformation
    {
        [XmlElement(Namespace = "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader")]
        public string Contact { get; set; }
        [XmlElement(Namespace = "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader")]
        public string ContactTypeIdentifier { get; set; }
    }
}
