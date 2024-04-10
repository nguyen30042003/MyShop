using DocumentFormat.OpenXml.Wordprocessing;
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
        public Visibility ListVisible { get; set; }
        public Visibility TextVisible { get; set; }
        public ICommand CreateOrder_Click { get; set; }
        public ICommand Search_Click { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Parse($"01/01/{DateTime.Now.Year}");
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);
        public String Search { get; set; } = "";
        public List<string> SortOptions { get; set; }
        public int selectedSortOptions { get; set; } = 0;
        public String MinPrice { get; set; } = "";
        public String MaxPrice { get; set; } = "";
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

        public int currentPage { get; set; } = 1;

        private int _totalItems;
        private int _totalPage;
        private int _perPage = 10;

        public OrderManagerVM()
        {
            SortOptions = new List<string>() { "Ascending", "Descending" };
            loadOrder();
            Search_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                loadOrder();
            });
            CreateOrder_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                SwitchToCreateOrderPage();
            });
            NavigateToPageCommand = new RelayCommand<int>((page) => true, NavigateToPage);
            previousPage = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                if (currentPage > 1)
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
        }

        public void loadOrder()
        {
            currentPage = 1;
          
            NavigateToPage(currentPage);
            
        }

        private Tuple<int, int> GetMinMax()
        {
            int min;
            int.TryParse(MinPrice, out min);

            int max;
            if (!int.TryParse(MaxPrice, out max))
            {
                max = int.MaxValue;
            }

            return new Tuple<int, int>(min, max);
        }
        private void NavigateToPage(int page)
        {
            ListVisible = Visibility.Hidden;
            TextVisible = Visibility.Visible;
            Task.Run(() => {
                currentPage = page;
                int skipCount = (currentPage - 1) * _perPage;
                int takeCount = _perPage;

                var MinMaxValues = GetMinMax();
                var pageResult = OrderServiceImpl.Instance.findAllPage(StartDate, EndDate, skipCount, takeCount, MinMaxValues.Item1, MinMaxValues.Item2, Search, selectedSortOptions);

                if (_totalItems != pageResult.Item2)
                {
                    _totalItems = pageResult.Item2;
                    _totalPage = _totalItems / _perPage;
                    if (_totalItems % _perPage != 0)
                    {
                        _totalPage += 1;
                    }
                }

                orderList = new ObservableCollection<Model.Order>(pageResult.Item1);
                PageNumbers = new ObservableCollection<int>(Enumerable.Range(1, _totalPage));
                TextVisible = Visibility.Hidden;
                ListVisible = Visibility.Visible;
            });
        }

        private void SwitchToCreateOrderPage()
        {

            CreateOrder createOrderPage = new CreateOrder();
            CreateOrderVM createOrderVM = new CreateOrderVM();

            void closeDialog()
            {
                createOrderPage.DialogResult = true;
            }

            createOrderVM.Click_Handler += closeDialog;
            createOrderPage.DataContext = createOrderVM;
            if (createOrderPage.ShowDialog() == true)
            {
                Order newOrder = createOrderVM.newOrder;
                if (OrderServiceImpl.Instance.save(newOrder))
                {
                    MessageBox.Show("Add success");
                    loadOrder();
                }
                
            }
        }


    }
}
