using ReadUbl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadInvoiceWpf.Model.Invoice
{
    public class Price
    {
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }

        public static Price operator -(Price price, Price price2)
        {
            return new Price
            {
                Amount = price.Amount - price2.Amount,
                CurrencyCode = price.CurrencyCode
            };
        }
        public static Price operator +(Price price, Price price2)
        {
            return new Price
            {
                Amount = price.Amount + price2.Amount,
                CurrencyCode = price.CurrencyCode
            };
        }
        public override string ToString()
        {
            return $"{Amount.ToString("N2")} {CurrencyCode.Trim()}".PadLeft(40,' ');
        }
    }
}
