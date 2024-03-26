using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShopProject.Model;

namespace MyShopProject.Service
{
    internal interface ItemService
    {
        bool save(Item item);
        bool update(Item item);
        List<Item> findAll();
        List<Item> findByName(string name);
        List<Item> findByOrderID(int orderID);
        List<Item> findByProductID(int productID);
        Item findById(int id);
        bool delete(Item item);
    }
}
