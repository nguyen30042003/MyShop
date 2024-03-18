using MyShopProject.Model;
using MyShopProject.Pages;
using MyShopProject.Repository;
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

namespace MyShopProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWidownVM mainVM { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = mainVM = new MainWidownVM();
            //NavigateToPage(new OrderManager());
            /*ICustomerRepository customerRepository = new ICustomerRepository();
            Customer newCustomer = new Customer
            {
                ID = 5,
                Full_Name = "John Doe",
                Phone = "123-456-7890",
                Email = "john.doe@example.com",
                DOB = new DateTime(1990, 1, 1),
                Gender = "Male",
                Avatar = "avatar111.jpg"
            };
            ExcelDataProcessor e = new ExcelDataProcessor();
            e.ImportDataFromExcel();
            customerRepository.create(newCustomer);*/
            //MessageBox.Show(customerRepository.findById(2).Avatar);
            //var a = DataProvider.Instance.DB.Customers.First().Full_Name;
            //customerRepository.delete(newCustomer);
            //customerRepository.update(newCustomer);
            //MessageBox.Show(customerRepository.findByName("n")[0].Full_Name);
            //MessageBox.Show(customerRepository.findAll().Count.ToString());

        }
        /*private void NavigateToPage(UserControl page)
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
        }*/
    }
}
