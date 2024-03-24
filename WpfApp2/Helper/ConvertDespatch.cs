using ReadInvoiceWpf.Model;
using ReadInvoiceWpf.Model.Despatch;
using ReadUbl.Models.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadInvoiceWpf.Helper
{
    public class ConvertDespatch
    {
        public static DespatchModel ToDespatchModel(ReadUbl.Models.Dispatch.DespatchAdvice despatch)
        {
            DespatchModel model = new DespatchModel();
            model.ID = despatch.ID?.Value;
            model.UUID = despatch.UUID;
            model.DespatchDateTime = DateTime.ParseExact($"{despatch.IssueDate.ToString("yyyy-MM-dd")} {despatch.IssueTime.ToString("HH:mm:ss")}", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            model.DespatchAdviceTypeCode = despatch.DespatchAdviceTypeCode;
            model.Note = despatch.Note;
            model.LineCountNumeric = despatch.LineCountNumeric;
            model.DocumentReferences = getAdditionalDocumentReferences(despatch);
            model.Supplier = getTaxPayer(despatch.AccountingSupplierParty);
            model.Customer = getTaxPayer(despatch.AccountingCustomerParty);
            model.DispatchAddress = getAddress(despatch.Shipment?.Delivery?.DeliveryAddress);
            model.Lines = getLines(despatch.DespatchLine);
            model.Note = despatch.Note;
            return model;
        }
        private static List<AdditionalDocumentReference> getAdditionalDocumentReferences(ReadUbl.Models.Dispatch.DespatchAdvice despatchAdvice)
        {
            List<AdditionalDocumentReference> result = new List<AdditionalDocumentReference>();
            
            foreach(var docRef in despatchAdvice.AdditionalDocumentReference)
            {
                AdditionalDocumentReference _docRef = new AdditionalDocumentReference();
                _docRef.IssueDate = docRef.IssueDate;
                _docRef.DocumentType = docRef.DocumentType;
                _docRef.ID = docRef.ID?.Value ?? "";
                result.Add(_docRef);
            }
            return result;
        }

        private static TaxPayer getTaxPayer(ReadUbl.Models.Invoice.AccountingParty _party)
        {
            Party party = _party?.Party;
            TaxPayer taxPayer = new TaxPayer();
            if (party is null)
                return taxPayer;
            taxPayer.Name = party.PartyName?.Name;
            taxPayer.TaxId = Convert.ToInt64(party?.PartyIdentification?.FirstOrDefault(x => x.ID.schemeID == "VKN")?.ID?.Value ?? "0");
            taxPayer.Eposta = party.Contact?.ElectronicMail;
            taxPayer.TaxOffice = party.PartyTaxScheme?.TaxScheme?.Name;
            taxPayer.Address = getAddress(party?.PostalAddress);
            taxPayer.MerssisNo = party.PartyIdentification.FirstOrDefault(x => x.ID.schemeID == "MERSISNO")?.ID?.Value ?? "";
            taxPayer.Phone = party.Contact.Telephone;
            taxPayer.CommerId = party.PartyIdentification.FirstOrDefault(x => x.ID.schemeID == "TICARETSICILNO")?.ID?.Value ?? "";
            taxPayer.WebAddress = party.WebsiteURI;
            return taxPayer;
        }

        private static Address getAddress(ReadUbl.Models.PostalAddress address)
        {
            Address result = new Address();
            if (address is null)
                return result;
            result.BuildingNumber = address.BuildingNumber;
            result.StreetName = address.StreetName;
            result.CountryCode = address.Country?.IdentificationCode ?? "";
            result.Town = address.CitySubdivisionName;
            result.City = address.CityName;
            result.PostalZone = address.PostalZone;
            return result;
        }
        private static List<DespatchLine> getLines(List<ReadUbl.Models.Dispatch.Line> lines)
        {
            List<DespatchLine> result = new List<DespatchLine>();
            foreach (var line in lines)
            {
                DespatchLine dispatchLine = new DespatchLine();
                dispatchLine.Note = line.Note;
                dispatchLine.DeliveredQuantity = getQuantity(line.DeliveredQuantity);
                dispatchLine.ManufacturerIdentification = line.Item?.ManufacturersItemIdentification?.ID?.Value ?? "";
                dispatchLine.SellerItemIdentification = line.Item?.SellersItemIdentification?.ID?.Value ?? "";
                dispatchLine.Name = line?.Item?.Name ?? "";
                dispatchLine.Description = line.Item?.Description ?? "";
                dispatchLine.ID = Convert.ToInt32(line.ID?.Value);
                dispatchLine.ProductTraceId = line.Item?.ItemInstance?.ProductTraceID ?? "";
                result.Add(dispatchLine);
            }
            return result;
        }
        private static Quantity getQuantity(ReadUbl.Models.Quantity quantity)
        {
            Quantity result = new Quantity();
            result.Qty = quantity.Value;
            result.UnitCode = quantity.UnitCode;
            return result;
        }
    }
}
