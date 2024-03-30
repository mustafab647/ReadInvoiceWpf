using ReadUbl.Models.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ReadUbl.Models.Envelope
{
    [XmlRoot(Namespace ="")]
    public class ElementList
    {
        [XmlArray(Namespace ="")]
        public List<Invoice.Invoice> Invoice { get; set; }
        //public Invoice.Invoice Invoice { get; set; }
        [XmlArrayItem("DespatchAdvice", Namespace = "")]
        public List<Dispatch.DespatchAdvice> DespatchAdvices { get; set; }
    }
}
