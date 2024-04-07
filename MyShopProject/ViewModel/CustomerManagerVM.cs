using MyShopProject.Model;
using MyShopProject.Pages;
using MyShopProject.Repository;
using MyShopProject.ServiceImpl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public ICommand DeleteCustomer_Click { get; set; }
        public ICommand NavigateToPageCommand { get; set; }
        public ICommand Sort_Click { get; set; }
        public ICommand previousPage { get; set; }
        public ICommand nextPage { get; set; }
        private ObservableCollection<Customer> _customerList { get; set; }
        public ObservableCollection<Customer> customerList {  get => _customerList; set { _customerList = value; OnPropertyChanged(nameof(customerList)); } }

        private ObservableCollection<int> _pageNumbers;
        public ObservableCollection<int> PageNumbers
        {
            get { return _pageNumbers; }
            set { _pageNumbers = value; OnPropertyChanged(nameof(PageNumbers)); }
        }
        public int currentPage { get; set; }

        private int _totalPage;
        public Customer SelectedCustomer { get; set; }
        public CustomerManagerVM() {
            
            loadCustomer();
            CreateCustomer_Click = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                SwitchToCreateOrderPage();
            });
            NavigateToPageCommand = new RelayCommand<int>((page) => true, (page) => NavigateToPage(page));
            previousPage = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                if(currentPage > 1)
                {
                    NavigateToPage(currentPage - 1);
                }    
                
            });
            nextPage = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                if (currentPage < _totalPage)
                {
                    NavigateToPage(currentPage + 1);
                }
            });
            DeleteCustomer_Click = new RelayCommand<Object>((p) => { return true; }, (p) => {
                MessageBoxResult result = MessageBox.Show("Do you want to delete customer information?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (CustomerServiceImpl.Instance.delete(SelectedCustomer))
                    {
                        MessageBox.Show("Delete success");
                    }
                    else
                    {
                        MessageBox.Show("Delete unSuccess");
                    }
                    NavigateToPage(1);
                }
                else if (result == MessageBoxResult.No)
                {
                    NavigateToPage(currentPage);
                }
                
            });
            Sort_Click = new RelayCommand<Object>((p) => { return true; }, (p) => {
                //loadCustomer();
            });
        }
        private void NavigateToPage(int p)
        {
            Page page = new Page();
            var pageResult = page.LoadPage<Customer>(new Customer(), p);
            currentPage = p;
            _totalPage = page.TotalPage;
            customerList = new ObservableCollection<Customer>(pageResult.Item1.Cast<Customer>());
            PageNumbers = pageResult.Item2;
        }
        public void loadCustomer()
        {
            currentPage = 1;
            NavigateToPage(currentPage);
        }
        private void SwitchToCreateOrderPage()
        {

            AddCustomer addCustomerPage = new AddCustomer();
            AddCustomerVM addCustomerVM = new AddCustomerVM();

            void closeDialog()
            {
                addCustomerPage.DialogResult = true;
            }

            addCustomerVM.Click_Handler += closeDialog;
            addCustomerPage.DataContext = addCustomerVM;
            if (addCustomerPage.ShowDialog() == true)
            {
                Customer newCustomer = addCustomerVM.newCustomer;
                if (CustomerServiceImpl.Instance.save(newCustomer))
                {
                    MessageBox.Show("Add success");
                    loadCustomer();
                }

            }
        }
    }
}
