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
        private static IItemRepository instance;
        public static IItemRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new IItemRepository();
                return instance;
            }
            set { instance = value; }
        }
        public bool create(Item item)
        {
            if (item != null)
            {
                try
                {
                    DataProvider.Instance.DB.Items.Add(item);
                    DataProvider.Instance.DB.SaveChanges();
                    return true; // Thành công
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while creating item: {ex.Message}");
                    return false; // Thất bại
                }
            }
            return false; // Thất bại
        }

        public bool delete(Item item)
        {
            Item i = findById(item.ID);
            if (i != null)
            {
                try
                {
                    DataProvider.Instance.DB.Items.Remove(i);
                    DataProvider.Instance.DB.SaveChanges();
                    return true; // Thành công
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while deleting item: {ex.Message}");
                    return false; // Thất bại
                }
            }
            return false;
        }

        public List<Item> findAll()
        {
            return DataProvider.Instance.DB.Items.ToList();
        }

        public Item findById(int id)
        {
            Item item = DataProvider.Instance.DB.Items.FirstOrDefault(c => c.ID == id);
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
            List<Item> items = DataProvider.Instance.DB.Items.Where(c => c.IDOrder == id).ToList();


            return items;
        }
        public List<Item> findByIdProduct(int id)
        {
            List<Item> items = DataProvider.Instance.DB.Items.Where(c => c.IDProduct == id).ToList();


            return items;
        }
        public bool update(Item item)
        {
            Item i = findById(item.ID);
            if (i != null)
            {
                try
                {
                    i.IDProduct = item.IDProduct;
                    i.IDOrder = item.IDOrder;
                    i.Price = item.Price;
                    i.Quantity = item.Quantity;
                    DataProvider.Instance.DB.SaveChanges();
                    return true; // Thành công
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while updating item: {ex.Message}");
                    return false; // Thất bại
                }
            }
            return false;
        }
    }
}
