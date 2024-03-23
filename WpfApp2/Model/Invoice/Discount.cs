using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadInvoiceWpf.Model.Invoice
{
    public class Discount
    {
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public Price Amount { get; set; }
        public Price BaseAmount { get; set; }

        public override string ToString()
        {
            if (Amount is null)
                return "";
            return $"{Amount.Amount} {Amount.CurrencyCode}".PadLeft(40,' ');
        }
    }
}
