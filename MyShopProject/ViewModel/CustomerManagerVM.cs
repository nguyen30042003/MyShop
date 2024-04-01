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
using System.Windows.Input;

namespace MyShopProject.ViewModel
{
    public class CustomerManagerVM : BaseViewModel
    {
        public ICommand CreateCustomer_Click {  get; set; }
        
        public ICommand Sort_Click { get; set; }
        private ObservableCollection<Customer> _customerList { get; set; }
        public ObservableCollection<Customer> customerList {  get => _customerList; set { _customerList = value; OnPropertyChanged(nameof(customerList)); } }

        public CustomerManagerVM() {
            loadCustomer();
            CreateCustomer_Click = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                AddCustomer addCustomer = new AddCustomer();
                addCustomer.ShowDialog();
                loadCustomer();
            });
            Sort_Click = new RelayCommand<Object>((p) => { return true; }, (p) => {
                //loadCustomer();
            });
        }
        public void loadCustomer()
        {
            customerList = new ObservableCollection<Customer>( CustomerServiceImpl.Instance.findAll());
        }
    }
}
