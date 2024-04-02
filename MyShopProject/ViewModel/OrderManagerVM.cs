using MyShopProject.Model;
using MyShopProject.Pages;
using MyShopProject.Repository;
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

        public ICommand NavigateToPageCommand { get; set; }
        public ICommand Sort_Click { get; set; }
        public ICommand previousPage { get; set; }
        public ICommand nextPage { get; set; }

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

        public OrderManagerVM()
        {
            loadOrder();
            CreateOrder_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                SwitchToCreateOrderPage();
                loadOrder();
            });
            NavigateToPageCommand = new RelayCommand<int>((page) => true, NavigateToPage);
            previousPage = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                if (_currentPage > 1)
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
        }
        public void loadOrder()
        {
            orderList = new ObservableCollection<Order>(OrderServiceImpl.Instance.findAll());
            _totalItems = orderList.Count;
            _totalPage = _totalItems / _perPage;
            if (_totalItems % _perPage != 0)
            {
                _totalPage += 1;
            }

            PageNumbers = new ObservableCollection<int>(Enumerable.Range(1, _totalPage));
            currentPage = 1;
            NavigateToPage(1);
            
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

            orderList = new ObservableCollection<Order>(OrderServiceImpl.Instance.findAllPage(skipCount, takeCount));
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
