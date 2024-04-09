using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Wordprocessing;
using MyShopProject.Model;
using MyShopProject.Repository;
using MyShopProject.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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

        public Tuple<List<Order>, int> findAllPage(DateTime start, DateTime end, int skip, int take, int min = 0, int max = int.MaxValue, string name = "", int sortOption = 1)
        {
            var orders = IOrderRepository.Instance.findAll()
                .Where(p =>
                    ((p.TotalPrice ?? 0) <= max && (p.TotalPrice ?? 0) >= min)
                    && (p.CreateDate <= end && p.CreateDate >= start)
                    && p.IDCustomer.ToString().ToLower().Contains(name.ToLower()));
            int count = orders.Count();
            if (sortOption == 0)
                return Tuple.Create(orders.OrderBy(p => p.TotalPrice).Skip(skip).Take(take).ToList(), count);
            else
                return Tuple.Create(orders.OrderByDescending(p => p.TotalPrice).Skip(skip).Take(take).ToList(), count);
            
           
        }
     
  
        
        
        public Tuple<ObservableCollection<double>, ObservableCollection<string>> profitByWeek()
        {
            ObservableCollection<double> dataProfit = new ObservableCollection<double>();
            
            ObservableCollection<string> seriesLabel = new ObservableCollection<string>();
            for (int i = 0; i < 7; i++)
            {
                DateTime date = DateTime.Now.AddDays(-i);
                dataProfit.Add(IOrderRepository.Instance.totalProfit(date.Date, date));
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
                DateTime startDate = new DateTime(DateTime.Now.Year, month, 1);
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);
                dataProfit.Add(IOrderRepository.Instance.totalProfit(startDate, endDate));
            }
            return Tuple.Create(dataProfit, seriesLabel);

        }
        public Tuple<ObservableCollection<double>, ObservableCollection<string>> profitForCurrentMonthWeeks()
        {
            ObservableCollection<double> dataProfit = new ObservableCollection<double>();
            ObservableCollection<string> seriesLabel = new ObservableCollection<string>() { "Week_1", "Week_2", "Week_3", "Week_4"};
            DateTime currentDate = DateTime.Now;
            int currentMonth = currentDate.Month;
            int currentYear = currentDate.Year;

            DateTime startDate = new DateTime(currentYear, currentMonth, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            int totalDaysInMonth = (endDate - startDate).Days;

            int daysPerWeek = totalDaysInMonth / 4;
            for (int i = 0; i < 4; i++)
            {
                DateTime weekStartDate = startDate.AddDays(i * daysPerWeek);
                DateTime weekEndDate = i == 3 ? endDate : startDate.AddDays((i + 1) * daysPerWeek - 1);
                dataProfit.Add(IOrderRepository.Instance.totalProfit(weekStartDate, weekEndDate));
            }
            return Tuple.Create(dataProfit, seriesLabel);

        }
        public Tuple<ObservableCollection<double>, ObservableCollection<string>> turnoverByWeek()
        {
            ObservableCollection<double> dataProfit = new ObservableCollection<double>();

            ObservableCollection<string> seriesLabel = new ObservableCollection<string>();
            for (int i = 0; i < 7; i++)
            {
                DateTime date = DateTime.Now.AddDays(-i);
                dataProfit.Add(IOrderRepository.Instance.totalTurnover(date.Date, date));
                seriesLabel.Add(date.Date.ToString("MM-dd"));
            }

            return Tuple.Create(dataProfit, seriesLabel);
        }
        public Tuple<ObservableCollection<double>, ObservableCollection<string>> turnoverByMonth()
        {
            ObservableCollection<double> dataProfit = new ObservableCollection<double>();
            ObservableCollection<string> seriesLabel = new ObservableCollection<string>() { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            for (int month = 1; month <= 12; month++)
            {
                DateTime startDate = new DateTime(DateTime.Now.Year, month, 1);
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);
                dataProfit.Add(IOrderRepository.Instance.totalTurnover(startDate, endDate));
            }
            return Tuple.Create(dataProfit, seriesLabel);

        }

        public Tuple<ObservableCollection<double>, ObservableCollection<string>> totalQuantityByWeek()
        {
            ObservableCollection<double> data = new ObservableCollection<double>();

            ObservableCollection<string> seriesLabel = new ObservableCollection<string>();
            for (int i = 0; i < 7; i++)
            {
                DateTime date = DateTime.Now.AddDays(-i);
                data.Add(totalQuantity(date, date));
                seriesLabel.Add(date.Date.ToString("MM-dd"));
            }

            return Tuple.Create(data, seriesLabel);
        }
        public Tuple<ObservableCollection<double>, ObservableCollection<string>> totalQuantityForCurrentMonthWeeks()
        {
            ObservableCollection<double> data = new ObservableCollection<double>();
            ObservableCollection<string> seriesLabel = new ObservableCollection<string>() { "Week_1", "Week_2", "Week_3", "Week_4" };
            DateTime currentDate = DateTime.Now;
            int currentMonth = currentDate.Month;
            int currentYear = currentDate.Year;

            DateTime startDate = new DateTime(currentYear, currentMonth, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            int totalDaysInMonth = (endDate - startDate).Days;

            int daysPerWeek = totalDaysInMonth / 4;
            for (int i = 0; i < 4; i++)
            {
                DateTime weekStartDate = startDate.AddDays(i * daysPerWeek);
                DateTime weekEndDate = i == 3 ? endDate : startDate.AddDays((i + 1) * daysPerWeek - 1);
                data.Add(totalQuantity(weekStartDate, weekEndDate));
            }
            return Tuple.Create(data, seriesLabel);
        }
        public Tuple<ObservableCollection<double>, ObservableCollection<string>> totalQuantityByMonth()
        {
            ObservableCollection<double> data = new ObservableCollection<double>();
            ObservableCollection<string> seriesLabel = new ObservableCollection<string>() { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            for (int month = 1; month <= 12; month++)
            {
                DateTime startDate = new DateTime(DateTime.Now.Year, month, 1);
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);
                data.Add(totalQuantity(startDate, endDate));
            }
            return Tuple.Create(data, seriesLabel);
        }
        public Tuple<ObservableCollection<double>, ObservableCollection<string>> turnoverForCurrentMonthWeeks()
        {
            ObservableCollection<double> dataProfit = new ObservableCollection<double>();
            ObservableCollection<string> seriesLabel = new ObservableCollection<string>() { "Week_1", "Week_2", "Week_3", "Week_4" };
            DateTime currentDate = DateTime.Now;
            int currentMonth = currentDate.Month;
            int currentYear = currentDate.Year;

            DateTime startDate = new DateTime(currentYear, currentMonth, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            int totalDaysInMonth = (endDate - startDate).Days;

            int daysPerWeek = totalDaysInMonth / 4;
            for (int i = 0; i < 4; i++)
            {
                DateTime weekStartDate = startDate.AddDays(i * daysPerWeek);
                DateTime weekEndDate = i == 3 ? endDate : startDate.AddDays((i + 1) * daysPerWeek - 1);
                dataProfit.Add(IOrderRepository.Instance.totalTurnover(weekStartDate, weekEndDate));
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
