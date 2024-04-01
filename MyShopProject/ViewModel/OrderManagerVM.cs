using MyShopProject.Model;
using MyShopProject.Pages;
using MyShopProject.ServiceImpl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyShopProject.ViewModel
{
    public class OrderManagerVM : BaseViewModel
    {
        public ICommand CreateOrder_Click { get; set; }
        private ObservableCollection<Order> _orderList { get; set; }
        public ObservableCollection<Order> orderList { get => _orderList; set { _orderList = value; OnPropertyChanged(nameof(orderList)); } }
        public OrderManagerVM()
        {
            loadOrder();
            CreateOrder_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                SwitchToCreateOrderPage();
                loadOrder();
            });
        }
        public void loadOrder()
        {
            orderList = new ObservableCollection<Order>(OrderServiceImpl.Instance.findAll());
        }
        private void SwitchToCreateOrderPage()
        {
            CategoryManager categoryManager = new CategoryManager();

            CreateOrder createOrderPage = new CreateOrder();

            AddCustomer addCustomer = new AddCustomer();
            createOrderPage.ShowDialog();
        }
    }
}
