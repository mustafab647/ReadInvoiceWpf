using ReadUbl.Models.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace ReadUbl.Concrete
{
    public class InvoiceBussines
    {
        public Invoice ReadUbl(string xmlStr)
        {
            Invoice invoice = Helper.XmlHelper<Invoice>.DeSerialize(xmlStr);
            string base64Xslt = invoice.AdditionalDocumentReference?.FirstOrDefault(x => x.DocumentType != null && x.DocumentType.Equals("XSLT"))?.Attachment?.EmbeddedDocumentBinaryObject.Value ?? "";
            if (string.IsNullOrEmpty(base64Xslt))
                base64Xslt = invoice.AdditionalDocumentReference?.FirstOrDefault(x => x.Attachment.EmbeddedDocumentBinaryObject.FileName.EndsWith("xslt"))?.Attachment?.EmbeddedDocumentBinaryObject.Value ?? "";
            byte[] bufferXslt = System.Convert.FromBase64String(base64Xslt);
            string xslt = System.Text.Encoding.UTF8.GetString(bufferXslt);
            invoice.EmbededXslt = xslt;
            return invoice;
        }

        public string XmlToString(Invoice invoice)
        {
            string result = Helper.XmlHelper<Invoice>.Serialize(invoice);
            return result;
        }
        public string InvoiceToHtml(Invoice invoice)
        {
            string result = string.Empty; ;
            try
            {
                XsltArgumentList xsltArgumentList = new XsltArgumentList();
                XslCompiledTransform xsltTransform = new XslCompiledTransform();

                byte[] xsltBuffer = System.Text.Encoding.UTF8.GetBytes(invoice.EmbededXslt);
                using (MemoryStream xsltMs = new MemoryStream(xsltBuffer))
                {
                    XmlReader xsltReader = XmlReader.Create(new StreamReader(xsltMs));

                    xsltTransform.Load(xsltReader);
                    string ubl = XmlToString(invoice);
                    XmlReader ublReader = XmlReader.Create(new StringReader(ubl));
                    MemoryStream htmlMs = new MemoryStream();
                    StringWriter sw = new StringWriter();
                    xsltTransform.Transform(ublReader, xsltArgumentList, sw);
                    StreamReader htmlReader = new StreamReader(htmlMs);
                    result = sw.ToString();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
    }
}
