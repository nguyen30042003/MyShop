using MyShopProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopProject.Repository
{
    internal interface OrderRepository
    {
        void create(Order order);
        void update(Order order);
        void delete(Order order);
        List<Order> findAll();
        List<Order> findByName(string name);
        Order findById(int id);
    }
}
