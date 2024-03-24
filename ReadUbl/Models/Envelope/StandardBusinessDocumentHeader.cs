using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ReadUbl.Models.Envelope
{
    public class StandardBusinessDocumentHeader
    {
        [XmlElement(Namespace = "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader")]
        public string HeaderVersion { get; set; }
        [XmlElement(Namespace = "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader")]
        public TaxPayer Sender { get; set; }
        [XmlElement(Namespace = "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader")]
        public TaxPayer Receiver { get; set; }
        [XmlElement(Namespace = "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader")]
        public DocumentIdentification DocumentIdentification { get; set; }
    }
}
