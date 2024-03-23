using DocumentFormat.OpenXml.Spreadsheet;
using MyShopProject.ViewModel;
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

namespace MyShopProject.Pages
{
    /// <summary>
    /// Interaction logic for OrderManager.xaml
    /// </summary>
    public partial class OrderManager : UserControl
    {
        public OrderManagerVM orderManagerVM { get; set; }
        public OrderManager()
        {
            InitializeComponent();
            this.DataContext = orderManagerVM = new OrderManagerVM();
        }

    }
}
