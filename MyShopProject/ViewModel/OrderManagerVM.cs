using MyShopProject.Pages;
using System;
using System.Collections.Generic;
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

        public OrderManagerVM()
        {
            CreateOrder_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                SwitchToCreateOrderPage();
            });
        }

        private void SwitchToCreateOrderPage()
        {

            AddCustomer addCustomer = new AddCustomer();
            addCustomer.ShowDialog();

        }
    }
}
