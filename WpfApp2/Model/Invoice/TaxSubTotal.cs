using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadInvoiceWpf.Model.Invoice
{
    public class TaxSubTotal
    {
        public Price TaxableAmount { get; set; }
        public Price TaxAmount { get; set; }
        public decimal Percent { get; set; }
        public TaxCategory Category { get; set; }
    }
}
