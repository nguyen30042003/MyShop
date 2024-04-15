using MyShopProject.Model;
using MyShopProject.ServiceImpl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyShopProject.ViewModel
{
    public class DetailOrderVM : BaseViewModel
    {
        public ICommand Return_Click { get; set; }
        public Order order { get; set; }
        public ObservableCollection<Item> items { get; set; }
        public string CustomerName { get; set; }

        public DetailOrderVM(Order o)
        {
            Return_Click = new RelayCommand<object>((p) => { return true; }, (p) => ReturnToProductManager());
            order = o;
            items = new ObservableCollection<Item>(order.Item);

            // Find the customer name
            List<Customer> customers = CustomerServiceImpl.Instance.findAll();
            foreach (Customer customer in customers)
            {
                if (customer.ID == o.IDCustomer)
                {
                    CustomerName = customer.Full_Name;
                    break;
                }
            }
        }
        private void ReturnToProductManager()
        {
            Window mainWindow = Application.Current.Windows[0];
            MainWidownVM mainWidownVM = (MainWidownVM)mainWindow.DataContext;
            mainWidownVM.orderManager_Click.Execute(null);
        }

    }
}
