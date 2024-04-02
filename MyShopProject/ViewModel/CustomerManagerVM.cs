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
        private int currentPage { get; set; }
        private int _currentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                OnPropertyChanged(nameof(_currentPage));
            }
        }
        private int _totalItems;
        private int _totalPage;
        private int _perPage = 5;
        public CustomerManagerVM() {
            
            loadCustomer();
            CreateCustomer_Click = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                AddCustomer addCustomer = new AddCustomer();
                addCustomer.ShowDialog();
                loadCustomer();
            });
            NavigateToPageCommand = new RelayCommand<int>((page) => true, NavigateToPage);
            previousPage = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                if(_currentPage > 1)
                {
                    NavigateToPage(_currentPage - 1);
                }    
                
            });
            nextPage = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                if (_currentPage < _totalPage)
                {
                    NavigateToPage(_currentPage + 1);
                }
            });
            Sort_Click = new RelayCommand<Object>((p) => { return true; }, (p) => {
                //loadCustomer();
            });
        }
        private void NavigateToPage(int page)
        {  
            _currentPage = page;
            int skipCount = (_currentPage - 1) * _perPage;
            int takeCount = _perPage;
            if (page == _totalPage)
            {
                takeCount = (_totalItems - skipCount) - 1;
            }
            
            customerList = new ObservableCollection<Customer>(ICustomerRepository.Instance.findPage(skipCount, takeCount));
        }
        public void loadCustomer()
        {
            customerList = new ObservableCollection<Customer>(CustomerServiceImpl.Instance.findAll());
            _totalItems = customerList.Count;
            _totalPage = _totalItems / _perPage;
            if (_totalItems % _perPage != 0)
            {
                _totalPage += 1;
            }

            PageNumbers = new ObservableCollection<int>(Enumerable.Range(1, _totalPage));
            currentPage = 1;
            NavigateToPage(1);
        }
    }
}
