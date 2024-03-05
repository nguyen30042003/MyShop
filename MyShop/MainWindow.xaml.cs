using MyShop.Pages;
using MyShop.UserControls;
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

namespace MyShop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigateToPage(new Dashboard());
        }
        private void NavigateToPage(UserControl page)
        {
            // Đặt trang hiển thị trong Frame
            contentControl.Content = page;
        }

        private void finance_Click(object sender, MouseButtonEventArgs e)
        {
            NavigateToPage(new ProductManager());
        }

        private void dashboard_Click(object sender, MouseButtonEventArgs e)
        {
            NavigateToPage(new Dashboard());
        }

        private void productManager_Click(object sender, MouseButtonEventArgs e)
        {
            NavigateToPage(new ProductManager());
        }

        private void orderManager_Click(object sender, MouseButtonEventArgs e)
        {
            NavigateToPage(new OrderManager());
        }

        private void setting_Click(object sender, MouseButtonEventArgs e)
        {
            NavigateToPage(new Setting());
        }

        private void analystBudget_Click(object sender, MouseButtonEventArgs e)
        {
            NavigateToPage(new AnalystBudget());
        }
    }
}