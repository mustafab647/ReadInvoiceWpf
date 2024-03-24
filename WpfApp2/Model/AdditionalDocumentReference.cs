using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadInvoiceWpf.Model
{
    public class AdditionalDocumentReference
    {
        public string ID { get; set; }
        public DateTime IssueDate { get; set; }
        public string DocumentType { get; set; }
    }
}
