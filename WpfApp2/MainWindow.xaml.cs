using Microsoft.Win32;
using ReadUbl.Concrete;
using ReadUbl.Helper;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly DependencyProperty htmlProperty = DependencyProperty.RegisterAttached("Html", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        public MainWindow()
        {
            InitializeComponent();
            WebInvoiceInitialize();
        }

        private bool isCompleteWebCore { get; set; } = false;
        private bool isInitializeWebCore { get; set; } = false;
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog(this) ?? false)
            {
                FilePath.Text = openFileDialog.FileName;
                string ublStr = System.IO.File.ReadAllText(openFileDialog.FileName);
                ReadUbl.Concrete.InvoiceBussines invoiceBussines = new ReadUbl.Concrete.InvoiceBussines();

                ReadUbl.Models.Invoice.Invoice invoice = invoiceBussines.ReadUbl(ublStr);

                UblText.Text = ublStr;
                XsltText.Text = invoice.EmbededXslt;
                string invoiceStr = invoiceBussines.InvoiceToXmlString(invoice);
                string html = string.Empty;
                var invoiceModel = ReadInvoiceWpf.Helper.ConvertInvoice.ToInvoiceModel(invoice);
                setDetail(invoiceModel);
                //editControl.Text = ublStr;
                try
                {
                    html = invoiceBussines.InvoiceToHtml(invoice);
                }
                catch { }
                setWebViewHtml(html);
            }
        }

        internal void RefreshView(object sender, RoutedEventArgs e)
        {
            ReadUbl.Concrete.InvoiceBussines invoiceBussines = new ReadUbl.Concrete.InvoiceBussines();
            ReadUbl.Models.Invoice.Invoice invoice = invoiceBussines.ReadUbl(UblText.Text);
            invoice.EmbededXslt = XsltText.Text;
            string html = string.Empty;
            try
            {
                html = invoiceBussines.InvoiceToHtml(invoice);
            }
            catch { }
            setWebViewHtml(html);
        }

        private void setDetail(ReadInvoiceWpf.Model.Invoice.Invoice invoice)
        {
            setSupplierInf(invoice);
            setCustomerInf(invoice);
            setDataGrid(invoice.Lines);
            setSubTotal(invoice);
        }
        private void setSubTotal(ReadInvoiceWpf.Model.Invoice.Invoice invoice)
        {
            GrandTotalExclVatTxt.Text = invoice.TaxExclAmount.ToString();
            DiscExclVatTxt.Text = invoice.ChargeAmount.ToString();
            VatTxt.Text = invoice.VatInclAmount.ToString();
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
        private void setDataGrid(List<ReadInvoiceWpf.Model.Invoice.InvoiceLine> invoiceLines)
        {
            invoiceLine.ItemsSource = invoiceLines;
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
    }
}