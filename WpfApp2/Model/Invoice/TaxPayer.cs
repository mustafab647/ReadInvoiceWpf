using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadInvoiceWpf.Model.Invoice
{
    public class TaxPayer
    {
        public string Name { get; set; }
        public string WebAddress { get; set; }
        public string Eposta { get; set; }
        public string Phone { get; set; }
        public string TaxOffice { get; set; }
        public long TaxId { get; set; }
        public string CommerId { get; set; }
        public string MerssisNo { get; set; }
        public Address Address { get; set; }
    }
}
