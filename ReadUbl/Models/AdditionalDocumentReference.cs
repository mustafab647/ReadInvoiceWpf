using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ReadUbl.Models
{
    public class AdditionalDocumentReference
    {
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public ID ID { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public DateTime IssueDate { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string DocumentType { get; set; }
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public Attachment Attachment { get; set; }
    }
}
