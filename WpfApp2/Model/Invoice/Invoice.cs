using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadInvoiceWpf.Model.Invoice
{
    public class Invoice
    {
        public Guid UUID { get; set; }
        public string No { get; set; }
        public DateTime DateTime { get; set; }
        public string ProfileId { get; set; }
        public List<string> Note { get; set; }
        public string Type { get; set; }
        public TaxPayer Supplier { get; set; }
        public TaxPayer Customer { get; set; }
        public Price TaxExclAmount { get; set; }
        public Price TaxInclAmount { get; set; }
        public Price TaxAmount { get; set; }
        public Price ChargeAmount { get; set; }
        public Price PayableAmount { get; set; }
        public Price VatInclAmount { get; set; }
        public Price Discount { get; set; }
        public List<InvoiceLine> Lines { get; set; }
    }
}
