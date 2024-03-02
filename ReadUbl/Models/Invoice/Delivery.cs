using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ReadUbl.Models.Invoice
{
    public class Delivery
    {
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public DeliveryAddress DeliveryAddress { get; set; }
    }
}
