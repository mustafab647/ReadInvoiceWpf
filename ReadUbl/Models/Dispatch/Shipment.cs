using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadUbl.Models.Dispatch
{
    public class Shipment
    {
        public ID ID { get; set; }
        public object ShipmentStage { get; set; }
        public Delivery Delivery { get; set; }
    }
}
