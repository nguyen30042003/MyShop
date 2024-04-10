using LiveCharts;
using LiveCharts.Wpf;
using MyShopProject.Model;
using MyShopProject.ServiceImpl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MyShopProject.ViewModel {
    public class DashBoardVM : BaseViewModel {
        public ObservableCollection<double> dataProfit { get; set; }
        public ObservableCollection<string> seriesLabel { get; set; }

        public ObservableCollection<double> dataProfitMonth { get; set; }
        public ObservableCollection<string> seriesLabelMonth { get; set; }
        public ObservableCollection<Product> topFiveProducts { get; set; }
        public SeriesCollection seriesCollection { get; set; }
        public SeriesCollection seriesCollectionByMonth { get; set; }

        public double maxValue { get; set; } = 1;
        public double maxValueMonth { get; set; } = 1;
        public int totalQuantityProductSoldToday { get; set; }
        public int totalOrderToday { get; set; }
        public int totalCustomerPurchasedToday { get; set; }
        public int totalProductInStock { get; set; }
        public int totalOrderWeek { get; set; }
        public int totalProductSelling { get; set; }
        public DashBoardVM() {
            Task.Run(() => {
                Application.Current.Dispatcher.Invoke(load);
            });
        }
        private void load() {
            totalQuantityProductSoldToday = OrderServiceImpl.Instance.totalQuantitySoldToday();
            totalOrderToday = OrderServiceImpl.Instance.totalOrderToday();
            totalCustomerPurchasedToday = CustomerServiceImpl.Instance.findAll().Count;
            totalProductInStock = ProductServiceImpl.Instance.totalQuantityProductInStock();
            totalOrderWeek = OrderServiceImpl.Instance.totalOrderWeek();
            totalProductSelling = ProductServiceImpl.Instance.findAll().Count;
            topFiveProducts = new ObservableCollection<Product>(ProductServiceImpl.Instance.topFiveProductRunOut());
            visualizeProfitByWeek();
            visualizeProfitByMonth();
        }
        private void visualizeProfitByWeek() {
            dataProfit = OrderServiceImpl.Instance.profitByWeek().Item1;
            seriesLabel = OrderServiceImpl.Instance.profitByWeek().Item2;
            seriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Weekly Profit",
                    Values = new ChartValues<double>(dataProfit)
                }
            };

            if (dataProfit.Max() == 0)
            {
                maxValue = 1;
            } else {
                maxValue = dataProfit.Max() * 1.5;
            }
        }
        private void visualizeProfitByMonth() {
            dataProfitMonth = OrderServiceImpl.Instance.profitByMonth().Item1;
            seriesLabelMonth = OrderServiceImpl.Instance.profitByMonth().Item2;
            seriesCollectionByMonth = new SeriesCollection
                {
                   new LineSeries
                   {
                        Title = "Monthly Profit",
                        Values = new ChartValues<double>(dataProfitMonth)
                   }
                };

            if (dataProfitMonth.Max() == 0) {
                maxValueMonth = 1;
            } else {
                maxValueMonth = dataProfitMonth.Max() * 1.5;
            }
        }

    }
}