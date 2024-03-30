using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ReadUbl.Models.Envelope
{
    public class TaxPayer
    {
        [XmlElement(Namespace = "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader")]
        public string Identifier { get; set; }
        [XmlElement(Namespace = "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader")]
        public List<ContactInformation> ContactInformation { get; set; }
    }
}
