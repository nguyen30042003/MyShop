using MyShopProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopProject.Repository
{
    public class IItemRepository : ItemRepository
    {
        public void create(Item item)
        {
            if (item != null)
            {
                DataProvider.Instance.DB.Item.Add(item);
                DataProvider.Instance.DB.SaveChanges();
            }
        }

        public void delete(Item item)
        {
            Item i = findById(item.ID);
            if (i != null)
            {
                DataProvider.Instance.DB.Item.Remove(i);
                DataProvider.Instance.DB.SaveChanges();
            }
        }

        public List<Item> findAll()
        {
            return DataProvider.Instance.DB.Item.ToList();
        }

        public Item findById(int id)
        {
            Item item = DataProvider.Instance.DB.Item.FirstOrDefault(c => c.ID == id);
            if (item != null)
            {
                Console.WriteLine(item.IDOrder);
            }
            else
            {

                Console.WriteLine($"Customer with ID {id} not found.");
            }
            return item;
        }

        public List<Item> findByIdOrder(int id)
        {
            List<Item> items = DataProvider.Instance.DB.Item.Where(c => c.IDOrder == id).ToList();


            return items;
        }

        public void update(Item item)
        {
            Item i = findById(item.ID);
            if (i != null)
            {
                i.IDProduct = item.IDProduct;
                i.IDOrder = item.IDOrder;
                i.Price = item.Price;
                i.Quantity = item.Quantity;
                DataProvider.Instance.DB.SaveChanges();
            }
        }
    }
}
