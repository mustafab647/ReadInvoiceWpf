using ReadUbl.Models.Dispatch;
using ReadUbl.Models.Envelope;
using ReadUbl.Models.Invoice;
using ReadUbl.RIWInterface;
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
        public Invoice ReadUblForInv(string xmlStr)
        {
            Type modelType = Helper.XmlHelper<object>.AsXmlType(xmlStr);
            if (modelType != typeof(Invoice))
                throw new Exception($"Lütfen {modelType.Name} modeli ile çağırınız.");
            Invoice invoice = Helper.XmlHelper<Invoice>.DeSerialize(xmlStr);
            return invoice;
        }

        public T ReadUbl<T>(string xmlStr) where T : RIW_UblItem,new()
        {
            Type modelType = Helper.XmlHelper<object>.AsXmlType(xmlStr);
            if (modelType != typeof(T))
                throw new Exception($"Lütfen {modelType.Name} modeli ile çağırınız.");
            T despatchAdvice = Helper.XmlHelper<T>.DeSerialize(xmlStr);

            return despatchAdvice;
        }
        public DespatchAdvice ReadUblForDespatch(string xmlStr)
        {
            Type modelType = Helper.XmlHelper<object>.AsXmlType(xmlStr);
            if (modelType != typeof(DespatchAdvice))
                throw new Exception($"Lütfen {modelType.Name} modeli ile çağırınız.");
            DespatchAdvice despatchAdvice = Helper.XmlHelper<DespatchAdvice>.DeSerialize(xmlStr);

            return despatchAdvice;
        }
        public Models.Envelope.StandardBusinessDocument GetEnvelope(string xmlStr)
        {
            Type modelType = Helper.XmlHelper<object>.AsXmlType(xmlStr);
            XmlDocument xmlDoc = Helper.XmlHelper<object>.GetXmlDoc(xmlStr);
            if (modelType != typeof(Models.Envelope.StandardBusinessDocument))
                throw new Exception($"Lütfen {modelType.Name} modeli ile çağırınız.");
            Models.Envelope.StandardBusinessDocument result = Helper.XmlHelper<Models.Envelope.StandardBusinessDocument>.DeSerialize(xmlStr);
            XmlNodeList xmlNodeList = xmlDoc.GetElementsByTagName("Invoice");
            ElementList elements = result.Package.Elements.ElementList;
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                Invoice invoice = Helper.XmlHelper<Invoice>.DeSerialize(xmlNode.OuterXml);
                elements.Invoice.Add(invoice);
            }
            return result;
        }

        public Type GetXmlModelType(string xmlStr)
        {
            return Helper.XmlHelper<object>.AsXmlType(xmlStr);
        }

        public string InvoiceToXmlString(Invoice invoice)
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
                    string ubl = InvoiceToXmlString(invoice);
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
        public string ToHtml(string ubl, string xslt)
        {
            string result = string.Empty; ;
            try
            {
                XsltArgumentList xsltArgumentList = new XsltArgumentList();
                XslCompiledTransform xsltTransform = new XslCompiledTransform();

                byte[] xsltBuffer = System.Text.Encoding.UTF8.GetBytes(xslt);
                using (MemoryStream xsltMs = new MemoryStream(xsltBuffer))
                {
                    XmlReader xsltReader = XmlReader.Create(new StreamReader(xsltMs));

                    xsltTransform.Load(xsltReader);
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
