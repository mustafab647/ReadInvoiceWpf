using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ReadUbl.Models.Invoice
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2", IsNullable = false)]

    public class Invoice
    {
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();
        public Invoice()
        {
            xmlns.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            xmlns.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            xmlns.Add("", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2");
        }

        [XmlIgnore]
        public string EmbededXslt { get; set; }

        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public float UBLVersionID { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string CustomizationID { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string ProfileID { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public ID ID { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public bool CopyIndicator { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Guid UUID { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public DateTime IssueDate { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public DateTime IssueTime { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string InvoiceTypeCode { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string Note { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public DocumentCurrencyCode DocumentCurrencyCode { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public int LineCountNumeric { get;set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public List<AdditionalDocumentReference> AdditionalDocumentReference { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public Signature Signature { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public AccountingParty AccountingSupplierParty { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public AccountingParty AccountingCustomerParty { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public Delivery Delivery { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public PaymentMeans PaymentMeans { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public PaymentTerms PaymentTerms { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public TaxTotal TaxTotal { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public LegalMonetaryTotal LegalMonetaryTotal { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public List<InvoiceLine> InvoiceLine { get; set; }
    }
}
