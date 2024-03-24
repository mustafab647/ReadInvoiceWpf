using Microsoft.Win32;
using ReadInvoiceWpf.Forms;
using ReadUbl.Concrete;
using ReadUbl.Helper;
using ReadUbl.Models;
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
using System.Windows.Media.Media3D;
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
        private ReadUbl.RIWInterface.RIW_UblItem ublObj { get; set; }
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
                allReset();
                FilePath.Text = openFileDialog.FileName;
                string ublStr = System.IO.File.ReadAllText(openFileDialog.FileName);
                ReadUbl.Concrete.InvoiceBussines invoiceBussines = new ReadUbl.Concrete.InvoiceBussines();
                Type xmlType = invoiceBussines.GetXmlModelType(ublStr);
                if (xmlType == typeof(ReadUbl.Models.Invoice.Invoice))
                {
                    ublObj = invoiceBussines.ReadUblForInv(ublStr);
                }
                else if (xmlType == typeof(ReadUbl.Models.Dispatch.DespatchAdvice))
                {
                    ublObj = invoiceBussines.ReadUblForDespatch(ublStr);
                }

                UblText.Text = ublStr;
                XsltText.Text = ublObj.EmbededXslt;
                string html = string.Empty;
                if (ublObj is ReadUbl.Models.Invoice.Invoice)
                {
                    var invoiceModel = ReadInvoiceWpf.Helper.ConvertInvoice.ToInvoiceModel((ReadUbl.Models.Invoice.Invoice)ublObj);
                    setDetail(invoiceModel);
                }
                else if(ublObj is ReadUbl.Models.Dispatch.DespatchAdvice)
                {
                    var dispatchModel = ReadInvoiceWpf.Helper.ConvertDespatch.ToDespatchModel((ReadUbl.Models.Dispatch.DespatchAdvice)ublObj);
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
        private void setDetail(ReadInvoiceWpf.Model.Invoice.Invoice invoice)
        {
            VatInf.Visibility = Visibility.Visible;
            setSupplierInf(invoice);
            setCustomerInf(invoice);
            setDataGrid(invoice.Lines);
            setSubTotal(invoice);
        }
        private void setDetail(ReadInvoiceWpf.Model.Despatch.DespatchModel despatch)
        {
            VatInf.Visibility = Visibility.Hidden;
            setSupplierInf(despatch);
            setCustomerInf(despatch);
            setDataGrid(despatch.Lines);
            //setSubTotal(invoice);
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
            VatInf_Page vatInf_Page = new VatInf_Page(selectedItem.TaxTotal);
            secondForm.Content = vatInf_Page;
            secondForm.ShowDialog();
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
        private void allReset()
        {
            resetSupplierInf();
            resetCustomerInf();
            resetSubTotal();
        }
    }
}