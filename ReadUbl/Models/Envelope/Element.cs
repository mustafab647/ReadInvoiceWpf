using ReadUbl.Models.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadUbl.Models.Envelope
{
    public class Element
    {
        public string ElementType { get; set; }
        public int ElementCount { get; set; }
        public Models.Envelope.ElementList ElementList { get; set; }
    }
}
