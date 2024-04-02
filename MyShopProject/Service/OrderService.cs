using MyShopProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopProject.Service
{
    internal interface OrderService
    {
        bool save(Order order);
        bool update(Order order);
        List<Order> findAll();
        List<Order> findByCustomerName(string name);
        Order findById(int id);
        bool delete(Order order);

        List<Order> sortByCreateDate(int skipCount, int takeCount, DateTime previousDate, DateTime lastDate);
        List<Order> sortByPrice(int skipCount, int takeCount, double minPrice, double maxPrice);


    }
}
