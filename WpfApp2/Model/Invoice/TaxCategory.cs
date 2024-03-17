using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadInvoiceWpf.Model.Invoice
{
    public class TaxCategory
    {
        public string TaxExemptionReasonCode { get; set; }
        public string TaxExemptionReason { get; set; }
        public string Name { get; set; }
        public string TaxTypeCode { get; set; }
    }
}
