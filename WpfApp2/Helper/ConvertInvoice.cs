using ReadInvoiceWpf.Model;
using ReadInvoiceWpf.Model.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadInvoiceWpf.Helper
{
    public class ConvertInvoice
    {
        public static Invoice ToInvoiceModel(ReadUbl.Models.Invoice.Invoice model)
        {
            Invoice invoice = new Invoice();
            if (model is null)
                return invoice;
            invoice.No = model.ID?.Value;
            invoice.UUID = model.UUID;
            string dateTimeStr = $"{model.IssueTime.ToString("yyyy-MM-dd")} {model.IssueTime.ToString("HH:mm:ss")}";
            DateTime dateTime;
            if (DateTime.TryParse(dateTimeStr, out dateTime))
                invoice.DateTime = dateTime;
            invoice.Type = model.InvoiceTypeCode;
            invoice.ProfileId = model.ProfileID;
            invoice.Lines = getLines(model.InvoiceLine);
            invoice.Supplier = getTaxPayer(model.AccountingSupplierParty);
            invoice.Customer = getTaxPayer(model.AccountingCustomerParty);
            invoice.Discount = getPrice(model.LegalMonetaryTotal?.AllowanceTotalAmount);
            invoice.PayableAmount = getPrice(model.LegalMonetaryTotal?.PayableAmount);
            invoice.VatInclAmount = getPrice(model.LegalMonetaryTotal?.TaxInclusiveAmount);
            invoice.TaxAmount = getPrice(model.TaxTotal?.TaxAmount);
            invoice.TaxExclAmount = getPrice(model.LegalMonetaryTotal?.TaxExclusiveAmount);
            invoice.ChargeAmount = getPrice(model.LegalMonetaryTotal?.ChargeTotalAmount);
            invoice.TaxInclAmount = getPrice(model.LegalMonetaryTotal?.TaxInclusiveAmount);
            
            return invoice;
        }

        private static List<InvoiceLine> getLines(List<ReadUbl.Models.Invoice.InvoiceLine> invoiceLines)
        {
            List<InvoiceLine> result = new List<InvoiceLine>();
            if (invoiceLines?.Count == 0)
                return result;

            foreach (var invoiceLine in invoiceLines)
            {
                InvoiceLine _invoiceLine = new InvoiceLine();
                _invoiceLine.Note = invoiceLine.Note;
                _invoiceLine.LineNo = Convert.ToInt32(invoiceLine.ID?.Value);
                _invoiceLine.Description = invoiceLine.Item?.Description ?? "";
                _invoiceLine.Name = invoiceLine.Item?.Name ?? "";
                _invoiceLine.SellerItemIdentification = invoiceLine.Item?.SellersItemIdentification?.ID?.Value ?? "";
                _invoiceLine.ManufacturerIdentification = invoiceLine.Item?.ManufacturersItemIdentification?.ID?.Value ?? "";
                _invoiceLine.Price = getPrice(invoiceLine.Price);
                _invoiceLine.Quantity = getQuantity(invoiceLine.InvoicedQuantity);
                _invoiceLine.TaxTotal = getTax(invoiceLine.TaxTotal);
                _invoiceLine.TotalPrice = getPrice(invoiceLine.LineExtensionAmount);
                _invoiceLine.AllowanceCharge = getDiscount(invoiceLine.AllowanceCharge);
                var taxExemption = invoiceLine.TaxTotal.TaxSubtotal.FirstOrDefault(x => x.TaxCategory.TaxExemptionReasonCode != null && x.TaxCategory.TaxExemptionReasonCode != "");
                _invoiceLine.VatException = $"{(taxExemption?.TaxCategory?.TaxExemptionReasonCode ?? "")} - {taxExemption?.TaxCategory.TaxExemptionReason}";
                if (_invoiceLine.VatException.Trim().Equals("-"))
                    _invoiceLine.VatException = "";
                result.Add(_invoiceLine);
            }
            return result;
        }
        private static TaxPayer getTaxPayer(ReadUbl.Models.Invoice.AccountingParty accountingParty)
        {
            TaxPayer taxPayer = new();
            if(accountingParty is null)
                return taxPayer;
            taxPayer.MerssisNo = accountingParty.Party?.PartyIdentification?.FirstOrDefault(x => x.ID.schemeID == "MERSISNO")?.ID?.Value ?? "";
            taxPayer.Eposta = accountingParty.Party?.Contact?.ElectronicMail ?? "";
            taxPayer.Phone = accountingParty.Party?.Contact?.Telephone;
            taxPayer.Name = accountingParty.Party?.PartyName?.Name ?? "";
            taxPayer.Address = getAddress(accountingParty.Party?.PostalAddress);
            taxPayer.TaxOffice = accountingParty.Party?.PartyTaxScheme?.TaxScheme?.Name ?? "";
            taxPayer.TaxId = Convert.ToInt64(accountingParty.Party?.PartyIdentification?.FirstOrDefault(x => x.ID.schemeID == "VKN")?.ID?.Value ?? "0");
            taxPayer.CommerId = accountingParty.Party?.PartyIdentification?.FirstOrDefault(x => x.ID.schemeID == "TICARETSICILNO")?.ID?.Value ?? "";
            taxPayer.WebAddress = accountingParty.Party?.WebsiteURI;
            return taxPayer;
        }
        private static Discount getDiscount(ReadUbl.Models.Invoice.AllowanceCharge allowanceCharge)
        {
            Discount discount = new();
            if (allowanceCharge is null)
                return discount;
            discount.Description = allowanceCharge.AllowanceChargeReason;
            discount.Rate = allowanceCharge.MultiplierFactorNumeric;
            discount.Amount = getPrice(allowanceCharge.Amount);
            discount.BaseAmount = getPrice(allowanceCharge.BaseAmount);
            return discount;
        }
        private static List<Discount> getDiscounts(List<ReadUbl.Models.Invoice.AllowanceCharge> allowanceCharges)
        {
            List<Discount> discounts = new();
            if (allowanceCharges?.Count == 0)
                return discounts;
            foreach(var allowCharge in allowanceCharges)
            {
                var discount = getDiscount(allowCharge);
                discounts.Add(discount);
            }
            return discounts;
        }
        private static Address getAddress(ReadUbl.Models.PostalAddress postalAddress)
        {
            Address address = new();
            if (postalAddress is null)
                return address;
            address.StreetName = postalAddress.StreetName;
            address.BuildingNumber = postalAddress.BuildingNumber;
            address.Town = postalAddress.CitySubdivisionName;
            address.City = postalAddress.CityName ?? string.Empty;
            address.PostalZone = postalAddress.PostalZone;
            address.CountryCode = postalAddress.Country?.IdentificationCode ?? string.Empty;
            address.Country = postalAddress.Country?.Name ?? string.Empty;
            return address;
        }
        private static Price getPrice(ReadUbl.Models.Invoice.Price price)
        {
            Price result = new();
            if (price is null)
                return result;
            result.CurrencyCode = price.CurrencyID;
            result.Amount = price.Value;
            return result;
        }
        private static Price getPrice(ReadUbl.Models.Invoice.LinePrice price)
        {
            Price result = new();
            if (price is null)
                return result;
            result.CurrencyCode = price.PriceAmount.CurrencyID;
            result.Amount = price.PriceAmount.Value;
            return result;
        }
        private static Quantity getQuantity(ReadUbl.Models.Quantity quantity)
        {
            Quantity result = new();
            if(quantity is null) return result;
            result.Qty = quantity.Value;
            result.UnitCode = quantity.UnitCode;
            return result;
        }
        private static TaxTotal getTax(ReadUbl.Models.Invoice.TaxTotal taxTotal)
        {
            TaxTotal result = new();
            if (taxTotal is null)
                return result;
            result.TaxAmount = getPrice(taxTotal.TaxAmount);
            result.TaxSubtotal = new();
            foreach(var tax in taxTotal.TaxSubtotal)
            {
                TaxSubTotal taxSubTotal = new();
                taxSubTotal.TaxableAmount = getPrice(tax.TaxableAmount);
                taxSubTotal.TaxAmount = getPrice(tax.TaxAmount);
                taxSubTotal.Percent = tax.Percent;
                taxSubTotal.Category = getTaxCategory(tax.TaxCategory);
                result.TaxSubtotal.Add(taxSubTotal);
            }
            return result;
        }
        private static TaxCategory getTaxCategory(ReadUbl.Models.Invoice.TaxCategory taxCategory)
        {
            TaxCategory result = new();
            if(taxCategory is null) return result;

            result.TaxExemptionReason = taxCategory.TaxExemptionReason;
            result.TaxExemptionReasonCode = taxCategory.TaxExemptionReasonCode;
            result.TaxTypeCode = taxCategory.TaxScheme?.TaxTypeCode;
            result.Name = taxCategory.TaxScheme?.Name;
            return result;
        }
    }
}
