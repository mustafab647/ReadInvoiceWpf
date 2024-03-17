using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadInvoiceWpf.Model
{
    public class Quantity
    {
        public string UnitCode { get; set; }
        public int Qty { get; set; }

        public override string ToString()
        {
            return $"{Qty} {UnitCode}";
        }
    }
}
