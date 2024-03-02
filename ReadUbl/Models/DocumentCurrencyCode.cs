using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ReadUbl.Models
{
    public class DocumentCurrencyCode
    {
        [XmlAttribute]
        public string listAgencyName { get; set; }
        [XmlAttribute]
        public string listID { get; set; }
        [XmlAttribute]
        public string listName { get; set; }
        [XmlAttribute]
        public string listVersionID { get; set; }
        [XmlText]
        public string Value { get; set; }
    }
}
