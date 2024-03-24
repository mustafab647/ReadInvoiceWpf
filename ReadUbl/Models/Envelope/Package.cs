using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ReadUbl.Models.Envelope
{
    public class Package
    {
        [XmlElement("Elements", Namespace ="")]
        public Element Elements { get; set; }
    }
}
