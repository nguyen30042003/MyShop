using MyShopProject.Model;
using MyShopProject.Pages;
using MyShopProject.ServiceImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyShopProject.ViewModel
{
    public class CustomerManagerVM : BaseViewModel
    {
        public ICommand CreateCustomer_Click {  get; set; }
        public List<Customer> customerList { get; set; }

        public CustomerManagerVM() {
            loadCustomer();
            CreateCustomer_Click = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                AddCustomer addCustomer = new AddCustomer();
                addCustomer.ShowDialog();
            });
        }
        private void loadCustomer()
        {
            customerList = CustomerServiceImpl.Instance.findAll();
        }
    }
}
