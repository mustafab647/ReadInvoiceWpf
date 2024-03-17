using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ReadInvoiceWpf.Model.Invoice
{
    public class InvoiceLine
    {
        public int LineNo { get; set; }
        public string SellerItemIdentification { get; set; }
        public string ManufacturerIdentification { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public Quantity Quantity { get; set; }
        public Price ExtAmount { get; set; }
        public Discount AllowanceCharge { get; set; }
        public TaxTotal TaxTotal { get; set; }
        public Price Price { get; set; }
        public Price TotalPrice { get; set; }
        public string VatException { get; set; }
    }
}
