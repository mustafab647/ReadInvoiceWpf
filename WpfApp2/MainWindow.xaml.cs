using Microsoft.Win32;
using System.Text;
using System.Windows;
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
                string xsltStr = invoice.AdditionalDocumentReference.FirstOrDefault(x => x.DocumentType == "XSLT")?.Attachment?.EmbeddedDocumentBinaryObject.Value ?? "";
                XsltText.Text = invoice.EmbededXslt;
                string invoiceStr = invoiceBussines.InvoiceToXmlString(invoice);
                string html = string.Empty;
                //editControl.Text = ublStr;
                try
                {
                    html = invoiceBussines.InvoiceToHtml(invoice);
                }
                catch { }
                setWebViewHtml(html);
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
    }
}