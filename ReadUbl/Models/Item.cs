using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ReadUbl.Models
{
    [XmlRoot(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
    public class Item
    {
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string Description { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string Name { get; set; }
        public ItemIdentification SellersItemIdentification { get; set; }
        public ItemIdentification ManufacturersItemIdentification { get; set; }
        public ItemInstance ItemInstance { get; set; }
    }
}
