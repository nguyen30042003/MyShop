using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.ChartDrawing;
using MyShopProject.Model;
using MyShopProject.Repository;
using MyShopProject.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopProject.ServiceImpl
{
    public class OrderServiceImpl : OrderService
    {
        private static OrderServiceImpl instance;
        public static OrderServiceImpl Instance
        {
            get
            {
                if (instance == null)
                    instance = new OrderServiceImpl();
                return instance;
            }
            set { instance = value; }
        }
        public bool delete(Order order)
        {
            if(IOrderRepository.Instance.delete(order))
            {
                return true;
            }    
            return false;
        }

        public List<Order> findAll()
        {
            List<Order> orders = new List<Order>();
            orders = IOrderRepository.Instance.findAll();
            return orders;
        }

        public Order findById(int id)
        {
            Order order = new Order();
            order = IOrderRepository.Instance.findById(id);
            return order;
        }

        public List<Order> findByCustomerName(string name)
        {
            List<Order> orders = new List<Order>();
            orders = IOrderRepository.Instance.findAll();
            return orders;
        }

        public bool save(Order order)
        {
            if (IOrderRepository.Instance.create(order))
            {
                return true;
            }
            return false;
        }

        public bool update(Order order)
        {
            if (IOrderRepository.Instance.update(order))
            {
                return true;
            }
            return false;
        }


        public Tuple<List<Order>, int> sortByCreateDate(int skipCount, int takeCount, DateTime previousDate, DateTime lastDate)
        {
            return IOrderRepository.Instance.findPage(skipCount, takeCount, previousDate, lastDate, 0, double.MaxValue);
           
        }
        public Tuple<List<Order>, int> findAllPage(int skipCount, int takeCount)
        {
            return IOrderRepository.Instance.findPage(skipCount, takeCount, DateTime.MinValue, DateTime.Now, 0, double.MaxValue);
           
        }

        public Tuple<List<Order>, int> sortByPrice(int skipCount, int takeCount, double minPrice, double maxPrice)
        {
            return IOrderRepository.Instance.findPage(skipCount, takeCount, DateTime.MinValue, DateTime.Now, minPrice, maxPrice);
            
        }
     
  
        public double totalProfitByMonth(int year, int month)
        {
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            return IOrderRepository.Instance.totalProfit(startDate, endDate);
        }
        
        
        public Tuple<ObservableCollection<double>, ObservableCollection<string>> profitByWeek()
        {
            ObservableCollection<double> dataProfit = new ObservableCollection<double>();
            
            ObservableCollection<string> seriesLabel = new ObservableCollection<string>();
            for (int i = 0; i < 7; i++)
            {
                DateTime date = DateTime.Now.AddDays(-i);
                dataProfit.Add(IOrderRepository.Instance.totalProfit(date, date));
                seriesLabel.Add(date.Date.ToString("MM-dd"));

            }
   
            return Tuple.Create(dataProfit,seriesLabel);
        }
        public Tuple<ObservableCollection<double>, ObservableCollection<string>> profitByMonth()
        {
            ObservableCollection<double> dataProfit = new ObservableCollection<double>();
            ObservableCollection<string> seriesLabel = new ObservableCollection<string>() { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            for (int month = 1; month <= 12; month++)
            {
                dataProfit.Add(totalProfitByMonth(DateTime.Now.Year, month));

            }
            return Tuple.Create(dataProfit, seriesLabel);

        }

        public double totalTurnover(DateTime previousDate, DateTime lastDate)
        {
            double total = 0;
            List<Order> orders = findAll();
            foreach (var o in orders)
            {
                if (o.CreateDate >= previousDate.Date && o.CreateDate <= lastDate.Date)
                {
                    foreach (var i in o.Item)
                        total += i.Price.Value;
                }
            }
            return total;
        }
        public int totalQuantity(DateTime previousDate, DateTime lastDate)
        {
            int total = 0;
            List<Order> orders = findAll();
            foreach (var o in orders)
            {
                if (o.CreateDate >= previousDate.Date && o.CreateDate <= lastDate.Date)
                {
                    foreach (var i in o.Item)
                        total += i.Quantity.Value;
                }
            }
            return total;
        }
    
        public int totalQuantitySoldToday()
        {
            int total = 0;
            List<Order> orders = findAll();
            foreach(var o in orders)
            {
                if(o.CreateDate == DateTime.Now.Date)
                {
                    total += o.TotalQuantity.Value;
                }
            }
            return total;
        }
        public int totalOrderToday()
        {
            int total = 0;
            List<Order> orders = findAll();
            foreach (var o in orders)
            {
                if (o.CreateDate == DateTime.Now.Date)
                {
                    total += 1;
                }
            }
            return total;
        }
        public int totalOrderWeek()
        {
            int total = 0;
            List<Order> orders = findAll();
            foreach (var o in orders)
            {
                if (o.CreateDate >= DateTime.Now.AddDays(-7) && o.CreateDate <= DateTime.Now)
                {
                    total += 1;
                }
            }
            return total;
        }
    }
}
