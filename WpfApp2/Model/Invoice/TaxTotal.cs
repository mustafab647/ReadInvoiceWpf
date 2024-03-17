using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadInvoiceWpf.Model.Invoice
{
    public class TaxTotal
    {
        public Price TaxAmount { get; set; }
        public List<TaxSubTotal> TaxSubtotal { get; set; }

        public override string ToString()
        {
            return $"{TaxAmount.Amount} {TaxAmount.CurrencyCode}";
        }
    }
}
