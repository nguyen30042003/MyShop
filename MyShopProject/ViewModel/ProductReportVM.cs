using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts;
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
    public class ProductReportVM : BaseViewModel
    {
        public ObservableCollection<ProductBestSeller> productBestSellers { get; set; }
        public String cbFilter { get; set; }

        public ICommand filter_Click { get; set; }

        public String[] FilterOptions { get; set; } = { "Today", "Week", "Month", "Year" };
        public SeriesCollection seriesProductBestSeller { get; set; }
        public ProductReportVM()
        {
            
            filter_Click = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                Task.Run(() => {
                    Application.Current.Dispatcher.Invoke(load);
                });


            });
        }

        private void load()
        {
            productBestSellers = new ObservableCollection<ProductBestSeller>();
            if (cbFilter == "Today")
            {
                today();
            }
            else if (cbFilter == "Week")
            {
                week();
            }
            else if (cbFilter == "Month")
            {
                month();
            }
            else
            {
                year();
            }
        }
        private void today()
        {
            

            var report = OrderServiceImpl.Instance.reportProductBestSellerByToday();
            ObservableCollection<Product> products = report.Item1;
            ObservableCollection<int> quantities = report.Item2;

            for (int i = 0; i < products.Count; i++)
            {
                ProductBestSeller p = new ProductBestSeller()
                {
                    ID = products[i].ID,
                    Name = products[i].Name,
                    Price = products[i].PriceImport.Value,
                    Quantity = quantities[i]
                };

                productBestSellers.Add(p);
            }

            var top5Products = productBestSellers.OrderByDescending(p => p.Quantity).Take(5);

            // Tạo seriesProductBestSeller từ top 5 sản phẩm
            seriesProductBestSeller = new SeriesCollection();
            foreach (var product in top5Products)
            {
                seriesProductBestSeller.Add(new PieSeries
                {
                    Title = product.Name,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(product.Quantity) },
                    DataLabels = true
                });
            }
        }
        private void month()
        {

            var report = OrderServiceImpl.Instance.reportProductBestSellerByMonth();
            ObservableCollection<Product> products = report.Item1;
            ObservableCollection<int> quantities = report.Item2;

            for (int i = 0; i < products.Count; i++)
            {
                ProductBestSeller p = new ProductBestSeller()
                {
                    ID = products[i].ID,
                    Name = products[i].Name,
                    Price = products[i].PriceSale.Value * quantities[i],
                    Quantity = quantities[i]
                };

                productBestSellers.Add(p);
            }

            var top5Products = productBestSellers.OrderByDescending(p => p.Quantity).Take(5);

            // Tạo seriesProductBestSeller từ top 5 sản phẩm
            seriesProductBestSeller = new SeriesCollection();
            foreach (var product in top5Products)
            {
                seriesProductBestSeller.Add(new PieSeries
                {
                    Title = product.Name,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(product.Quantity) },
                    DataLabels = true
                });
            }
        }
        private void week()
        {
            var report = OrderServiceImpl.Instance.reportProductBestSellerByWeek();
            ObservableCollection<Product> products = report.Item1;
            ObservableCollection<int> quantities = report.Item2;

            for (int i = 0; i < products.Count; i++)
            {
                ProductBestSeller p = new ProductBestSeller()
                {
                    ID = products[i].ID,
                    Name = products[i].Name,
                    Price = products[i].PriceImport.Value,
                    Quantity = quantities[i]
                };

                productBestSellers.Add(p);
            }

            var top5Products = productBestSellers.OrderByDescending(p => p.Quantity).Take(5);

            // Tạo seriesProductBestSeller từ top 5 sản phẩm
            seriesProductBestSeller = new SeriesCollection();
            foreach (var product in top5Products)
            {
                seriesProductBestSeller.Add(new PieSeries
                {
                    Title = product.Name,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(product.Quantity) },
                    DataLabels = true
                });
            }
        }
        private void year()
        {
            var report = OrderServiceImpl.Instance.reportProductBestSellerByYear();
            ObservableCollection<Product> products = report.Item1;
            ObservableCollection<int> quantities = report.Item2;

            for (int i = 0; i < products.Count; i++)
            {
                ProductBestSeller p = new ProductBestSeller()
                {
                    ID = products[i].ID,
                    Name = products[i].Name,
                    Price = products[i].PriceImport.Value,
                    Quantity = quantities[i]
                };

                productBestSellers.Add(p);
            }

            var top5Products = productBestSellers.OrderByDescending(p => p.Quantity).Take(5);

            // Tạo seriesProductBestSeller từ top 5 sản phẩm
            seriesProductBestSeller = new SeriesCollection();
            foreach (var product in top5Products)
            {
                seriesProductBestSeller.Add(new PieSeries
                {
                    Title = product.Name,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(product.Quantity) },
                    DataLabels = true
                });
            }
        }
    }
    public class ProductBestSeller
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
