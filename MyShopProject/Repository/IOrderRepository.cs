using MyShopProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopProject.Repository
{
    public class IOrderRepository : OrderRepository
    {

        private static IOrderRepository instance;
        public static IOrderRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new IOrderRepository();
                return instance;
            }
            set { instance = value; }
        }



        public bool create(Order order)
        {
            if (order != null)
            {
                try
                {
                    DataProvider.Instance.DB.Orders.Add(order);
                    DataProvider.Instance.DB.SaveChanges();
                    return true; // Thành công
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while creating order: {ex.Message}");
                    return false; // Thất bại
                }
            }
            return false;
        }

        public bool delete(Order order)
        {
            Order o = findById(order.ID);
            if (o != null)
            {
                try
                {
                    DataProvider.Instance.DB.Orders.Remove(o);
                    DataProvider.Instance.DB.SaveChanges();
                    return true; // Thành công
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while deleting order: {ex.Message}");
                    return false; // Thất bại
                }
            }
            return false;
        }

        public List<Order> findAll()
        {
            return DataProvider.Instance.DB.Orders.ToList();
        }

        public Order findById(int id)
        {
            Order order = DataProvider.Instance.DB.Orders.FirstOrDefault(c => c.ID == id);
            if (order != null)
            {
                Console.WriteLine(order.ID);
            }
            else
            {

                Console.WriteLine($"Customer with ID {id} not found.");
            }
            return order;
        }
        public List<Order> findPage(int skipCount, int takeCount, DateTime previousDate, DateTime lastDate, double minPrice, double maxPrice)
        {
            List<Order> orders = DataProvider.Instance.DB.Orders.OrderBy(o => o.CreateDate >= previousDate && o.CreateDate <= lastDate && o.TotalPrice >= minPrice && o.TotalPrice <= maxPrice).Skip(skipCount).Take(takeCount).ToList();
            return orders;
        }




        public List<Order> findByName(string name)
        {
            List<Customer> customers = ICustomerRepository.Instance.findByName(name);
            List<Order> orders = new List<Order>();

            foreach (var customer in customers)
            {
                orders.AddRange(DataProvider.Instance.DB.Orders.Where(c => c.IDCustomer == customer.ID).ToList());
            }

            return orders;
        }

        public List<Order> sortByCreateDate(DateTime previousDate, DateTime lastDate)
        {
            return DataProvider.Instance.DB.Orders.Where(o => o.CreateDate >= previousDate && o.CreateDate <= lastDate)
                                                     .OrderBy(o => o.CreateDate)
                                                     .ToList();
        }

        public List<Order> sortByPrice(float minPrice, float maxPrice)
        {
            return DataProvider.Instance.DB.Orders.Where(o => o.TotalPrice >= minPrice && o.TotalPrice <= maxPrice)
                                                     .OrderBy(o => o.TotalPrice)
                                                     .ToList();
        }

        public List<Order> sortByPriceASC()
        {
            return DataProvider.Instance.DB.Orders.OrderBy(o => o.TotalPrice).ToList();
        }

        public List<Order> sortByPriceDesc()
        {
            return DataProvider.Instance.DB.Orders.OrderByDescending(o => o.TotalPrice).ToList();
        }

        public List<Order> sortByQuantityASC()
        {
            return DataProvider.Instance.DB.Orders.OrderBy(o => o.TotalQuantity).ToList();
        }

        public List<Order> sortByQuantityDesc()
        {
            return DataProvider.Instance.DB.Orders.OrderByDescending(o => o.TotalQuantity).ToList();
        }

        public bool update(Order order)
        {
            Order o = findById(order.ID);
            if (o != null)
            {
                try
                {
                    o.IDCustomer = order.IDCustomer;
                    o.TotalPrice = order.TotalPrice;
                    o.TotalQuantity = order.TotalQuantity;
                    o.CreateDate = order.CreateDate;
                    DataProvider.Instance.DB.SaveChanges();
                    return true; // Thành công
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while updating order: {ex.Message}");
                    return false; // Thất bại
                }
            }
            return false;
        }
    }
}
