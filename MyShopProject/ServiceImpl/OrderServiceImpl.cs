using MyShopProject.Model;
using MyShopProject.Repository;
using MyShopProject.Service;
using System;
using System.Collections.Generic;
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


        public List<Order> sortByPrice(float minPrice, float maxPrice)
        {
            List<Order> orders = new List<Order>();
            orders = IOrderRepository.Instance.sortByPrice(minPrice, maxPrice);
            return orders;
        }

        public List<Order> sortByPriceASC()
        {
            List<Order> orders = new List<Order>();
            orders = IOrderRepository.Instance.sortByPriceASC();
            return orders;
        }

        public List<Order> sortByPriceDesc()
        {
            List<Order> orders = new List<Order>();
            orders = IOrderRepository.Instance.sortByPriceDesc();
            return orders;
        }

        public List<Order> sortByQuantityASC()
        {
            List<Order> orders = new List<Order>();
            orders = IOrderRepository.Instance.sortByQuantityASC();
            return orders;
        }

        public List<Order> sortByQuantityDesc()
        {
            List<Order> orders = new List<Order>();
            orders = IOrderRepository.Instance.sortByQuantityDesc();
            return orders;
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
    }
}
