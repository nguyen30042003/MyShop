using LiveCharts.Wpf;
using LiveCharts;
using MyShopProject.Pages;
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
using LiveCharts.Defaults;

namespace MyShopProject.ViewModel
{
    public class AnalystBudgetVM : BaseViewModel
    {

        public ObservableCollection<double> dataProfit { get; set; }
        public ObservableCollection<string> seriesProfitLabel { get; set; }

        public ObservableCollection<double> dataTurnover { get; set; }
        public ObservableCollection<string> seriesTurnoverLabel { get; set; }

        public ObservableCollection<double> dataQuantityProduct { get; set; }
        public ObservableCollection<string> seriesQuantityProductLabel { get; set; }

        public ObservableCollection<double> dataProfitAndTurnover { get; set; }

        public SeriesCollection seriesCollection { get; set; }
        public SeriesCollection seriesTurnoverCollection { get; set; }
        public SeriesCollection seriesTurnoverAndProfitCollection { get; set; }
        public SeriesCollection seriesQuantityProductCollection { get; set; }
        public double maxValueProfit { get; set; } = 1;
        public double maxValueTurnover { get; set; } = 1;

        public double maxValueQuantity { get; set; } = 1;
        public String cbFilter { get; set; }

        public ICommand filter_Click { get; set; }

        public String[] FilterOptions { get; set; } = { "Today", "Week", "Month", "Year" };
        public AnalystBudgetVM()
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
            if(cbFilter == "Today")
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
        }
        private void today()
        {
            dataProfit = OrderServiceImpl.Instance.profitByWeek().Item1;
            seriesProfitLabel = OrderServiceImpl.Instance.profitByWeek().Item2;
            seriesProfit();

            dataTurnover = OrderServiceImpl.Instance.turnoverByWeek().Item1;
            seriesTurnoverLabel = OrderServiceImpl.Instance.turnoverByWeek().Item2;

            dataQuantityProduct = OrderServiceImpl.Instance.totalQuantityByWeek().Item1;
            seriesQuantityProductLabel = OrderServiceImpl.Instance.totalQuantityByWeek().Item2;


            seriesTurnoverAndProfitCollection = new SeriesCollection {
                new PieSeries
                {
                     Title = "Profit",
                     Values = new ChartValues<ObservableValue> { new ObservableValue(dataProfit[0]) },
                     DataLabels = true,
                },
                 new PieSeries
                {
                     Title = "Expense",
                     Values = new ChartValues<ObservableValue> { new ObservableValue(dataTurnover[0] - dataProfit[0]) },
                     DataLabels = true
                }
            };

            seriesTurnover();
            seriesQuantityProduct();
        }    
        private void week()
        {
            dataProfit = OrderServiceImpl.Instance.profitForCurrentMonthWeeks().Item1;
            seriesProfitLabel = OrderServiceImpl.Instance.profitForCurrentMonthWeeks().Item2;
            seriesProfit();

            dataTurnover = OrderServiceImpl.Instance.turnoverForCurrentMonthWeeks().Item1;
            seriesTurnoverLabel = OrderServiceImpl.Instance.turnoverForCurrentMonthWeeks().Item2;
            dataQuantityProduct = OrderServiceImpl.Instance.totalQuantityForCurrentMonthWeeks().Item1;
            seriesQuantityProductLabel = OrderServiceImpl.Instance.totalQuantityForCurrentMonthWeeks().Item2;
            seriesTurnover();
            seriesProfitAndTurnover();
            seriesQuantityProduct();
        }
        private void month()
        {
            dataProfit = OrderServiceImpl.Instance.profitByMonth().Item1;
            seriesProfitLabel = OrderServiceImpl.Instance.profitByMonth().Item2;
            seriesProfit();

            dataTurnover = OrderServiceImpl.Instance.turnoverByMonth().Item1;
            seriesTurnoverLabel = OrderServiceImpl.Instance.turnoverByMonth().Item2;

            dataQuantityProduct = OrderServiceImpl.Instance.totalQuantityByMonth().Item1;
            seriesQuantityProductLabel = OrderServiceImpl.Instance.totalQuantityByMonth().Item2;
            seriesTurnover();
            seriesQuantityProduct();
            seriesProfitAndTurnover();
        }
        private void seriesProfit()
        {
            seriesCollection = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Profit",
                        Values = new ChartValues<double>(dataProfit)
                    }
                };

            if (dataProfit.Max() == 0)
            {
                maxValueProfit = 1;
            }
            else
            {
                maxValueProfit = dataProfit.Max() * 1.5;
            }
        }
        private void seriesTurnover()
        {
            seriesTurnoverCollection = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Turnover",
                        Values = new ChartValues<double>(dataTurnover)
                    }
                };

            if (dataProfit.Max() == 0)
            {
                maxValueTurnover = 1;
            }
            else
            {
                maxValueTurnover = dataTurnover.Max() * 1.5;
            }
        }
        private void seriesProfitAndTurnover()
        {
            seriesTurnoverAndProfitCollection = new SeriesCollection {
                new PieSeries
                {
                     Title = "Profit",
                     Values = new ChartValues<ObservableValue> { new ObservableValue(dataProfit.Sum())},
                     DataLabels = true,
                },
                 new PieSeries
                {
                     Title = "Expense",
                     Values = new ChartValues<ObservableValue> { new ObservableValue(dataTurnover.Sum() - dataProfit.Sum()) },
                     DataLabels = true
                }
            };

        }
        private void seriesQuantityProduct()
        {
            seriesQuantityProductCollection = new SeriesCollection
                {
                   new ColumnSeries
                   {
                        Title = "Quantity Of products sold",
                        Values = new ChartValues<double>(dataQuantityProduct)
                   }
                };

            if (dataQuantityProduct.Max() == 0)
            {
                maxValueQuantity = 1;
            }
            else
            {
                maxValueQuantity = dataQuantityProduct.Max() * 1.5;
            }
        }
    }
}
