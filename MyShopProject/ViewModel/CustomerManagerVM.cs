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
using System.Windows.Forms;
using System.Windows.Input;

namespace MyShopProject.ViewModel
{
    public class CustomerManagerVM : BaseViewModel
    {
        public Visibility ListVisible { get; set; }
        public Visibility TextVisible { get; set; }
        public ICommand CreateCustomer_Click {  get; set; }
        public ICommand DeleteCustomer_Click { get; set; }
        public ICommand UpdateCustomer_Click { get; set; }
        public ICommand NavigateToPageCommand { get; set; }
        public ICommand Sort_Click { get; set; }
        public ICommand previousPage { get; set; }
        public ICommand nextPage { get; set; }
        public ICommand Search_Click { get; set; }
        public ICommand Import_Click { get; set; }
        
        public ICommand perPage_Click { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Parse($"01/01/1000");
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);
        public String Search { get; set; } = "";
        public List<string> SortOptions { get; set; }
        public int selectedSortOptions { get; set; } = 0;
        private string _PerPage {  get; set; }
        public string perPage { get => _PerPage;
            set {
                _PerPage = value;
                OnPropertyChanged(nameof(_PerPage));
            }
        }
        private ObservableCollection<Customer> _customerList { get; set; }
        public ObservableCollection<Customer> customerList {  get => _customerList; set { _customerList = value; OnPropertyChanged(nameof(customerList)); } }

        private ObservableCollection<int> _pageNumbers;
        public ObservableCollection<int> PageNumbers
        {
            get { return _pageNumbers; }
            set { _pageNumbers = value; OnPropertyChanged(nameof(PageNumbers)); }
        }
        public int currentPage { get; set; } = 1;

        private int _totalItems;
        private int _totalPage;
        private int _perPage = 10;

        public Customer SelectedCustomer { get; set; }
        public CustomerManagerVM() {
            
            SortOptions = new List<string>() { "Ascending", "Descending" };
            loadCustomer();
            perPage_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                _perPage = int.Parse(perPage);
                loadCustomer();


            });
            Search_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                loadCustomer();
            });
            CreateCustomer_Click = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                SwitchToCreateOrderPage();
            });
            UpdateCustomer_Click = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                OpenUpdateCustomerDialog();
            });
            Import_Click = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel Files (*.xls; *.xlsx)|*.xls; *.xlsx|All files (*.*)|*.*";
                openFileDialog.ShowDialog();
                string filename = openFileDialog.FileName;
                if (filename != null)
                {
                    ExcelDataProcessor processor = new ExcelDataProcessor();
                    processor.ImportDataFromExcel(filename);
                    loadCustomer();
                }    
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
                MessageBoxResult result = System.Windows.MessageBox.Show("Do you want to delete customer information?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (CustomerServiceImpl.Instance.delete(SelectedCustomer))
                    {
                        System.Windows.MessageBox.Show("Delete success");
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Delete unSuccess");
                    }
                    NavigateToPage(1);
                }
                else if (result == MessageBoxResult.No)
                {
                    NavigateToPage(currentPage);
                }
                
            });
            Sort_Click = new RelayCommand<Object>((p) => { return true; }, (p) => {
                loadCustomer();
            });
        }
        private void OpenUpdateCustomerDialog()
        {
            AddCustomer addCustomer = new AddCustomer();

            var updateCustomerVM = new UpdateCustomerVM(SelectedCustomer);
            updateCustomerVM.Click_Handler += () => {
                addCustomer.DialogResult = true;
            };
            addCustomer.DataContext = updateCustomerVM;

            if (addCustomer.ShowDialog() == true)
            {
                Customer customer = updateCustomerVM.newCustomer;
                if(CustomerServiceImpl.Instance.update(customer))
                {
                    System.Windows.MessageBox.Show("Update success");
                    loadCustomer();
                }
            }
        }
        private void NavigateToPage(int p)
        {
            ListVisible = Visibility.Hidden;
            TextVisible = Visibility.Visible;
            Task.Run(() => {
                currentPage = p;
                int skipCount = (currentPage - 1) * _perPage;
                int takeCount = _perPage;

                var pageResult = CustomerServiceImpl.Instance.findAllPage(StartDate, EndDate, skipCount, takeCount, Search, selectedSortOptions);

                _totalItems = pageResult.Item2;
                _totalPage = _totalItems / _perPage;
                if (_totalItems % _perPage != 0)
                {
                    _totalPage += 1;
                }

                customerList = new ObservableCollection<Model.Customer>(pageResult.Item1);
                PageNumbers = new ObservableCollection<int>(Enumerable.Range(1, _totalPage));
                TextVisible = Visibility.Hidden;
                ListVisible = Visibility.Visible;
            });
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
                    System.Windows.MessageBox.Show("Add success");
                    loadCustomer();
                }

            }
        }
    }
}
