﻿using MyShopProject.Model;
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

        public Tuple<List<Order>, int> findPage(int skipCount, int takeCount, DateTime previousDate, DateTime lastDate, double minPrice, double maxPrice)
        {
            var orders = DataProvider.Instance.DB.Orders
                .Where(o => o.CreateDate >= previousDate && o.CreateDate <= lastDate && o.TotalPrice >= minPrice && o.TotalPrice <= maxPrice)
                .OrderBy(o => o.ID);
            int count = orders.Count();
            List<Order> list = orders.Skip(skipCount)
                .Take(takeCount)
                .ToList();
            return new Tuple<List<Order>, int>(list, count);
        }

        public double totalProfit(DateTime previousDate, DateTime lastDate)
        {
            double total = 0;
            List<Order> orders = findAll();
            foreach (var o in orders)
            {
                if (o.CreateDate >= previousDate && o.CreateDate <= lastDate)
                {
                    foreach (var i in o.Item)
                        total += i.Profit.Value;
                }
            }
            return total;
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


        public Tuple<List<Product>, List<int>> ProductBestSell(DateTime previousDate, DateTime lastDate)
        {
            List<Order> orders = findAll();
            List<Product> products = new List<Product>();
            List<int> quantities = new List<int>();

            foreach (var order in orders)
            {
                if (order.CreateDate >= previousDate.Date && order.CreateDate <= lastDate.Date)
                {
                    foreach (var item in order.Item)
                    {
                        int index = products.FindIndex(p => p.ID == item.IDProduct);
                        if (index != -1)
                        {
                            // Product already exists, update its quantity
                            quantities[index] += item.Quantity.Value;
                        }
                        else
                        {
                            // Product doesn't exist, add it to the list
                            products.Add(item.Product);
                            quantities.Add(item.Quantity.Value);
                        }
                    }
                }
            }

            // Sort products by quantity sold
            var sortedProducts = products.OrderByDescending(p => quantities[products.IndexOf(p)]).ToList();
            var sortedQuantities = quantities.OrderByDescending(q => q).ToList();

            return Tuple.Create(sortedProducts, sortedQuantities);
        }
    }
}
