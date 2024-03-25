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



        public void create(Order order)
        {
            if (order != null)
            {
                DataProvider.Instance.DB.Orders.Add(order);
                DataProvider.Instance.DB.SaveChanges();
            }
        }

        public void delete(Order order)
        {
            Order o = findById(order.ID);
            if (o != null)
            {
                DataProvider.Instance.DB.Orders.Remove(o);
                DataProvider.Instance.DB.SaveChanges();
            }
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

        public void update(Order order)
        {
            Order o = findById(order.ID);
            if (o != null)
            {
                o.IDCustomer = order.IDCustomer;
                o.TotalPrice = order.TotalPrice;
                o.TotalQuantity = order.TotalQuantity;
                o.CreateDate = order.CreateDate;
                DataProvider.Instance.DB.SaveChanges();
            }
        }
    }
}
