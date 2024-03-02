using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ReadUbl.Models
{
    [XmlRoot(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class ID
    {
        [XmlAttribute]
        public string schemeID { get; set; }
        [XmlText]
        public string Value { get; set; }
    }
}
