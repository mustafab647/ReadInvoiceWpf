using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReadInvoiceWpf.Forms
{
    /// <summary>
    /// Interaction logic for VatInf_Page.xaml
    /// </summary>
    public partial class VatInf_Page : Page
    {
        public VatInf_Page()
        {
            InitializeComponent();
        }

        public VatInf_Page(Model.Invoice.TaxTotal taxTotal)
            : this()
        {
            VatInf_DataGrid.ItemsSource = taxTotal.TaxSubtotal;
        }
    }
}
