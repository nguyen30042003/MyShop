using MyShopProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopProject.Repository
{
    internal interface ItemRepository
    {
        void create(Item item);
        void update(Item item);
        void delete(Item item);
        List<Item> findAll();
        List<Item> findByIdOrder(int id);
        Item findById(int id);
    }
}
