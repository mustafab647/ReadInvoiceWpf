using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ReadUbl.Models
{
    [XmlRoot(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
    public class Quantity
    {
        [XmlAttribute("unitCode")]
        public string UnitCode { get; set; }
        [XmlText]
        public int Value { get; set; }
    }
}
