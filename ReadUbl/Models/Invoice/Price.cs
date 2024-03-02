using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ReadUbl.Models.Invoice
{
    [XmlRoot(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class Price
    {
        [XmlAttribute("currencyID")]
        public string CurrencyID { get; set; }
        [XmlText]
        public decimal Value { get; set; }
    }
}
