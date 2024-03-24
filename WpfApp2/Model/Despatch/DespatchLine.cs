using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadInvoiceWpf.Model.Despatch
{
    public class DespatchLine : IItemLine
    {
        public int ID { get; set; }
        public string Note { get; set; }
        public Quantity DeliveredQuantity { get; set; }
        public string SellerItemIdentification { get; set; }
        public string ManufacturerIdentification { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductTraceId { get; set; }
    }
}
