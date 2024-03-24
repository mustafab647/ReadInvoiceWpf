using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ReadInvoiceWpf.Model.Despatch
{
    public class DespatchModel
    {
        public string ProfileID { get; set; }
        public string ID { get; set; }
        public Guid UUID { get; set; }
        public DateTime DespatchDateTime { get; set; }
        public string DespatchAdviceTypeCode { get; set; }
        public List<string> Note { get; set; }
        public int LineCountNumeric { get; set; }
        public List<AdditionalDocumentReference> DocumentReferences { get; set; }
        public TaxPayer Supplier { get; set; }
        public TaxPayer Customer { get; set; }
        public Address DispatchAddress { get; set; }
        public List<DespatchLine> Lines { get; set; }
    }
}
