using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadInvoiceWpf.Model
{
    public class Address
    {
        public string StreetName { get; set; }
        public string BuildingNumber { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
        public string PostalZone { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
    }
}
