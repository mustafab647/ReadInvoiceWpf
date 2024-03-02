using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ReadUbl.Models.Invoice
{
    [XmlRoot(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
    public class TaxSubtotal
    {
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Price TaxableAmount { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Price TaxAmount { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public decimal Percent { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public TaxCategory TaxCategory { get; set; }
    }
}
