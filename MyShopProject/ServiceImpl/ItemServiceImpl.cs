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
    public class ItemServiceImpl : ItemService
    {
        public bool delete(Item item)
        {
            if (IItemRepository.Instance.delete(item))
            {
                return true;
            }
            return false;
        }

        public List<Item> findAll()
        {
            List<Item> items = new List<Item>();
            items = IItemRepository.Instance.findAll();
            return items;
        }

        public Item findById(int id)
        {
            Item item = new Item();
            item = IItemRepository.Instance.findById(id);
            return item;
        }

        public List<Item> findByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Item> findByOrderID(int orderID)
        {
            List<Item> items = new List<Item>();
            items = IItemRepository.Instance.findByIdOrder(orderID);
            return items;
        }

        public List<Item> findByProductID(int productID)
        {
            List<Item> items = new List<Item>();
            items = IItemRepository.Instance.findByIdProduct(productID);
            return items;
        }

        public bool save(Item item)
        {
            if (IItemRepository.Instance.create(item))
            {
                return true;
            }
            return false;
        }

        public bool update(Item item)
        {
            if (IItemRepository.Instance.update(item))
            {
                return true;
            }
            return false;
        }
    }
}
