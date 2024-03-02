using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ReadUbl.Models
{
    public class EmbeddedDocumentBinaryObject
    {
        [XmlAttribute("characterSetCode")]
        public string CharacterSetCode { get; set; }
        [XmlAttribute("encodingCode")]
        public string EncodingCode { get; set; }
        [XmlAttribute("filename")]
        public string FileName { get; set; }
        [XmlAttribute("mimeCode")]
        public string MimeCode { get; set; }
        [XmlText]
        public string Value { get; set; }
    }
}
