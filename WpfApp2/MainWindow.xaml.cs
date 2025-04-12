using Microsoft.Win32;
using ReadInvoiceWpf.Forms;
using ReadUbl.Concrete;
using ReadUbl.Helper;
using ReadUbl.Models;
using ReadUbl.Models.Dispatch;
using ReadUbl.RIWInterface;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using WPFLocalizeExtension.Engine;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly DependencyProperty htmlProperty = DependencyProperty.RegisterAttached("Html", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        private ReadUbl.RIWInterface.RIW_UblItem ublObj { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            WebInvoiceInitialize();
            LanguageMenu();
        }

        private void LanguageMenu()
        {
            // Create a menu for language selection

            MenuItem englishMenuItem = new MenuItem { Header = "English" };
            MenuItem spanishMenuItem = new MenuItem { Header = "Türkçe" };
            // Add event handlers for language selection

            englishMenuItem.Click += (s, e) => ChangeLanguage("en-Us");
            spanishMenuItem.Click += (s, e) => ChangeLanguage("tr-TR");
            // Add the menu items to the language menu
            SelectLanguage.Items.Clear();
            //LanguageMenuTest.Items.Add(new MenuItem { Header = "Select Language" });
            SelectLanguage.Items.Add(englishMenuItem);
            SelectLanguage.Items.Add(spanishMenuItem);
            // Add the language menu to the window
            //this.Content = LanguageMenuTest;
        }

        private void ChangeLanguage(string v)
        {
            LocalizeDictionary.Instance.Culture = new System.Globalization.CultureInfo(v);
        }
        private bool isCompleteWebCore { get; set; } = false;
        private bool isInitializeWebCore { get; set; } = false;
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog(this) ?? false)
            {
                allReset();
                FilePath.Text = openFileDialog.FileName;
                string ublStr = System.IO.File.ReadAllText(openFileDialog.FileName);
                ReadUbl.Concrete.InvoiceBussines invoiceBussines = new ReadUbl.Concrete.InvoiceBussines();
                Type xmlType = invoiceBussines.GetXmlModelType(ublStr);
                if (xmlType == typeof(ReadUbl.Models.Invoice.Invoice))
                {
                    ublObj = invoiceBussines.ReadUbl<ReadUbl.Models.Invoice.Invoice>(ublStr);
                    DocList.ItemsSource = new List<ReadUbl.Models.Invoice.Invoice>() { (ReadUbl.Models.Invoice.Invoice)ublObj };
                    setHeader(ublObj);
                    showDoc(ublObj);
                }
                else if (xmlType == typeof(ReadUbl.Models.Dispatch.DespatchAdvice))
                {
                    ublObj = invoiceBussines.ReadUbl<DespatchAdvice>(ublStr);
                    DocList.ItemsSource = new List<ReadUbl.Models.Dispatch.DespatchAdvice>() { (ReadUbl.Models.Dispatch.DespatchAdvice)ublObj };
                    setHeader(ublObj);
                    showDoc(ublObj);
                }
                else if (xmlType == typeof(ReadUbl.Models.Envelope.StandardBusinessDocument))
                {
                    var envelope = invoiceBussines.GetEnvelope(ublStr);
                    setHeader(envelope);
                    showDoc(envelope.Package?.Elements?.ElementList?.Invoice?.FirstOrDefault());
                }

            }
        }

        internal void RefreshView(object sender, RoutedEventArgs e)
        {
            ReadUbl.Concrete.InvoiceBussines invoiceBussines = new ReadUbl.Concrete.InvoiceBussines();
            //ReadUbl.Models.Invoice.Invoice invoice = invoiceBussines.ReadUblForInv(UblText.Text);
            //invoice.EmbededXslt = XsltText.Text;
            string html = string.Empty;
            try
            {
                html = invoiceBussines.ToHtml(UblText.Text, XsltText.Text);
            }
            catch { }
            setWebViewHtml(html);
        }
        private void showDoc(ReadUbl.RIWInterface.RIW_UblItem ublItem)
        {
            ReadUbl.Concrete.InvoiceBussines invoiceBussines = new InvoiceBussines();
            string ublStr = "";
            if (ublItem is ReadUbl.Models.Invoice.Invoice)
                ublStr = invoiceBussines.InvoiceToXmlString((ReadUbl.Models.Invoice.Invoice)ublItem);
            else if (ublItem is ReadUbl.Models.Dispatch.DespatchAdvice)
                ublStr = invoiceBussines.InvoiceToXmlString((ReadUbl.Models.Dispatch.DespatchAdvice)ublItem);
            UblText.Text = ublStr;
            XsltText.Text = ublItem.EmbededXslt;
            string html = string.Empty;
            if (ublItem is ReadUbl.Models.Invoice.Invoice)
            {
                var invoiceModel = ReadInvoiceWpf.Helper.ConvertInvoice.ToInvoiceModel((ReadUbl.Models.Invoice.Invoice)ublItem);
                setDetail(invoiceModel);
            }
            else if (ublItem is ReadUbl.Models.Dispatch.DespatchAdvice)
            {
                var dispatchModel = ReadInvoiceWpf.Helper.ConvertDespatch.ToDespatchModel((ReadUbl.Models.Dispatch.DespatchAdvice)ublItem);
                setDetail(dispatchModel);
            }
            //editControl.Text = ublStr;
            try
            {
                html = invoiceBussines.ToHtml(ublStr, XsltText.Text);
            }
            catch { }
            setWebViewHtml(html);
        }
        private void setHeader(object doc)
        {
            if (doc is ReadUbl.Models.Envelope.StandardBusinessDocument)
            {
                ReadUbl.Models.Envelope.StandardBusinessDocument envelope = (ReadUbl.Models.Envelope.StandardBusinessDocument)doc;
                SenderEmail.Text = envelope.Header.Sender.Identifier;
                SenderTitle.Text = envelope.Header.Sender.ContactInformation.FirstOrDefault(x => x.ContactTypeIdentifier == "UNVAN")?.Contact;
                SenderIdentifier.Text = envelope.Header.Sender.ContactInformation.FirstOrDefault(x => x.ContactTypeIdentifier == "VKN_TCKN")?.Contact;

                ReceiverEmail.Text = envelope.Header.Receiver.Identifier;
                ReceiverTitle.Text = envelope.Header.Receiver.ContactInformation.FirstOrDefault(x => x.ContactTypeIdentifier == "UNVAN")?.Contact;
                ReceiverIdentifier.Text = envelope.Header.Receiver.ContactInformation.FirstOrDefault(x => x.ContactTypeIdentifier == "VKN_TCKN")?.Contact;
                if (envelope.Package?.Elements?.ElementList?.Invoice?.Count > 0)
                    DocList.ItemsSource = envelope.Package?.Elements?.ElementList?.Invoice;
            }
            else if (doc is ReadUbl.Models.Invoice.Invoice)
            {
                ReadUbl.Models.Invoice.Invoice invoice = (ReadUbl.Models.Invoice.Invoice)doc;
                SenderEmail.Text = invoice.AccountingSupplierParty?.Party?.Contact?.ElectronicMail ?? "";
                SenderTitle.Text = invoice.AccountingSupplierParty?.Party?.PartyName?.Name ?? "";
                SenderIdentifier.Text = invoice.AccountingSupplierParty?.Party?.PartyIdentification?.FirstOrDefault(x => x.ID.schemeID == "VKN")?.ID?.Value ?? "";

                ReceiverEmail.Text = invoice.AccountingCustomerParty?.Party?.Contact?.ElectronicMail ?? "";
                ReceiverTitle.Text = invoice.AccountingCustomerParty?.Party?.PartyName?.Name ?? "";
                ReceiverIdentifier.Text = invoice.AccountingCustomerParty?.Party?.PartyIdentification?.FirstOrDefault(x => x.ID.schemeID == "VKN")?.ID?.Value ?? "";
            }
            else if (doc is DespatchAdvice)
            {
                ReadUbl.Models.Dispatch.DespatchAdvice despatch = (ReadUbl.Models.Dispatch.DespatchAdvice)doc;
                SenderEmail.Text = despatch.AccountingSupplierParty?.Party?.Contact?.ElectronicMail ?? "";
                SenderTitle.Text = despatch.AccountingSupplierParty?.Party?.PartyName?.Name ?? "";
                SenderIdentifier.Text = despatch.AccountingSupplierParty?.Party?.PartyIdentification?.FirstOrDefault(x => x.ID.schemeID == "VKN")?.ID?.Value ?? "";

                ReceiverEmail.Text = despatch.AccountingCustomerParty?.Party?.Contact?.ElectronicMail ?? "";
                ReceiverTitle.Text = despatch.AccountingCustomerParty?.Party?.PartyName?.Name ?? "";
                ReceiverIdentifier.Text = despatch.AccountingCustomerParty?.Party?.PartyIdentification?.FirstOrDefault(x => x.ID.schemeID == "VKN")?.ID?.Value ?? "";
            }

        }
        private void setDetail(ReadInvoiceWpf.Model.Invoice.Invoice invoice)
        {
            dataGridMenu.Visibility = Visibility.Visible;
            VatInf.Visibility = Visibility.Visible;
            setSupplierInf(invoice);
            setCustomerInf(invoice);
            setDataGrid(invoice.Lines);
            setSubTotal(invoice);
            setNotes(invoice.Note);
        }
        private void setDetail(ReadInvoiceWpf.Model.Despatch.DespatchModel despatch)
        {
            VatInf.Visibility = Visibility.Hidden;
            dataGridMenu.Visibility = Visibility.Hidden;
            setSupplierInf(despatch);
            setCustomerInf(despatch);
            setDataGrid(despatch.Lines);
            setNotes(despatch.Note);
        }
        private void setSubTotal(ReadInvoiceWpf.Model.Invoice.Invoice invoice)
        {
            if (invoice.TaxExclAmount?.Amount > 0)
                GrandTotalExclVatTxt.Text = invoice.TaxExclAmount.ToString();
            if (invoice.ChargeAmount?.Amount > 0)
                DiscExclVatTxt.Text = invoice.ChargeAmount.ToString();
            if (invoice.VatInclAmount?.Amount > 0)
                VatTxt.Text = (invoice.VatInclAmount - invoice.TaxExclAmount).ToString();
            if (invoice.TaxInclAmount?.Amount > 0)
                TotalText.Text = invoice.TaxInclAmount.ToString();
        }
        private void setSupplierInf(ReadInvoiceWpf.Model.Invoice.Invoice invoice)
        {
            SupplierTxt.Text = invoice.Supplier.Name;
            SupplierAddrTxt.Text = $"{invoice.Supplier.Address.StreetName} {invoice.Supplier.Address.BuildingNumber}";
            SupplierTownTxt.Text = invoice.Supplier.Address.Town;
            SupplierCityTxt.Text = invoice.Supplier.Address.City;
            SupplierTaxIdTxt.Text = invoice.Supplier.TaxId.ToString();
            SupplierTaxOfficeTxt.Text = invoice.Supplier.TaxOffice;
            SupplierPhoneTxt.Text = invoice.Supplier.Phone;
        }

        private void setSupplierInf(ReadInvoiceWpf.Model.Despatch.DespatchModel despatch)
        {
            SupplierTxt.Text = despatch.Supplier.Name;
            SupplierAddrTxt.Text = $"{despatch.Supplier.Address.StreetName} {despatch.Supplier.Address.BuildingNumber}";
            SupplierTownTxt.Text = despatch.Supplier.Address.Town;
            SupplierCityTxt.Text = despatch.Supplier.Address.City;
            SupplierTaxIdTxt.Text = despatch.Supplier.TaxId.ToString();
            SupplierTaxOfficeTxt.Text = despatch.Supplier.TaxOffice;
            SupplierPhoneTxt.Text = despatch.Supplier.Phone;
        }
        private void setCustomerInf(ReadInvoiceWpf.Model.Invoice.Invoice invoice)
        {
            CustomerTxt.Text = invoice.Customer.Name;
            CustomerAddrTxt.Text = $"{invoice.Customer.Address.StreetName} {invoice.Customer.Address.BuildingNumber}";
            CustomerTownTxt.Text = invoice.Customer.Address.Town;
            CustomerCityTxt.Text = invoice.Customer.Address.City;
            CustomerTaxIdTxt.Text = invoice.Customer.TaxId.ToString();
            CustomerTaxOfficeTxt.Text = invoice.Customer.TaxOffice;
            CustomerPhoneTxt.Text = invoice.Customer.Phone;
        }
        private void setCustomerInf(ReadInvoiceWpf.Model.Despatch.DespatchModel despatch)
        {
            CustomerTxt.Text = despatch.Customer.Name;
            CustomerAddrTxt.Text = $"{despatch.Customer.Address.StreetName} {despatch.Customer.Address.BuildingNumber}";
            CustomerTownTxt.Text = despatch.Customer.Address.Town;
            CustomerCityTxt.Text = despatch.Customer.Address.City;
            CustomerTaxIdTxt.Text = despatch.Customer.TaxId.ToString();
            CustomerTaxOfficeTxt.Text = despatch.Customer.TaxOffice;
            CustomerPhoneTxt.Text = despatch.Customer.Phone;
        }
        private void setDataGrid(List<ReadInvoiceWpf.Model.Invoice.InvoiceLine> invoiceLines)
        {
            invoiceLine.ItemsSource = invoiceLines;
        }
        private void setDataGrid(List<ReadInvoiceWpf.Model.Despatch.DespatchLine> despatchLines)
        {
            invoiceLine.ItemsSource = despatchLines;
        }
        private void setNotes(List<string> notes)
        {
            if (notes is null)
                return;
            NotesTxt.Text = string.Join("\r\n", notes);
        }
        private void generateData(object sender, EventArgs e)
        {
            System.Windows.Controls.DataGrid dataGrid = (System.Windows.Controls.DataGrid)sender;

            foreach (var dataGridColumn in dataGrid.Columns)
            {
                var textColumn = dataGridColumn as DataGridTextColumn;
                if (textColumn == null) continue;

                textColumn.IsReadOnly = true;
                textColumn.CanUserReorder = true;
            }
        }
        private async void setWebViewHtml(string html)
        {
            if (WebInvoice?.CoreWebView2 is not null)
            {
                WebInvoice.CoreWebView2.NavigateToString(html);
            }
        }
        private void WebInvoiceInitialize()
        {
            Microsoft.Web.WebView2.Core.CoreWebView2Environment environment = Microsoft.Web.WebView2.Core.CoreWebView2Environment.CreateAsync().Result;
            WebInvoice.CoreWebView2InitializationCompleted += WebInvoice_CoreWebView2InitializationCompleted;
            WebInvoice?.EnsureCoreWebView2Async(environment);
            isInitializeWebCore = true;
        }
        private void WebInvoice_CoreWebView2InitializationCompleted(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            isCompleteWebCore = e.IsSuccess;
        }

        private void OnHtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //WebBrowser web = d as WebBrowser;
            //if (WebBrowser != null)
            //    WebBrowser.NavigateToString(e.NewValue as string);
        }

        private void ReView_Click(object sender, RoutedEventArgs e)
        {

        }

        private void VatInf_Click(object sender, RoutedEventArgs eventArgs)
        {
            var selectedItem = (ReadInvoiceWpf.Model.Invoice.InvoiceLine)invoiceLine.SelectedItem;
            SecondForm secondForm = new SecondForm();
            secondForm.Title = "Vat Information";
            VatInf_Page vatInf_Page = new VatInf_Page(selectedItem.TaxTotal);
            secondForm.Content = vatInf_Page;
            secondForm.ShowDialog();
        }

        private void resetHeader()
        {
            SenderEmail.Text = "";
            SenderTitle.Text = "";
            SenderIdentifier.Text = "";

            ReceiverEmail.Text = "";
            ReceiverTitle.Text = "";
            ReceiverIdentifier.Text = "";
            DocList.ItemsSource = null;
        }
        private void resetDataGrid()
        {
            invoiceLine.ItemsSource = null;
        }
        private void resetSupplierInf()
        {
            SupplierTxt.Text = "";
            SupplierAddrTxt.Text = "";
            SupplierTownTxt.Text = "";
            SupplierCityTxt.Text = "";
            SupplierTaxIdTxt.Text = "";
            SupplierTaxOfficeTxt.Text = "";
            SupplierPhoneTxt.Text = "";
        }
        private void resetCustomerInf()
        {
            CustomerTxt.Text = "";
            CustomerAddrTxt.Text = "";
            CustomerTownTxt.Text = "";
            CustomerCityTxt.Text = "";
            CustomerTaxIdTxt.Text = "";
            CustomerTaxOfficeTxt.Text = "";
            CustomerPhoneTxt.Text = "";
        }
        private void resetSubTotal()
        {
            GrandTotalExclVatTxt.Text = "";
            DiscExclVatTxt.Text = "";
            VatTxt.Text = "";
            TotalText.Text = "";
        }
        private void resetNotes()
        {
            NotesTxt.Text = string.Empty;
        }
        private void allReset()
        {
            resetHeader();
            resetSupplierInf();
            resetCustomerInf();
            resetDataGrid();
            resetSubTotal();
        }

        private void DocList_Selected(object sender, RoutedEventArgs e)
        {
            var selected = (System.Windows.Controls.SelectionChangedEventArgs)e;
            if (selected.AddedItems.Count == 0)
                return;
            RIW_UblItem ublItem = (RIW_UblItem)selected.AddedItems[0];
            showDoc(ublItem);
        }
    }
}