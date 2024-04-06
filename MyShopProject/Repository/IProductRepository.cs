using MyShopProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopProject.Repository
{
    public class IProductRepository : ProductRepository
    {
        private static IProductRepository instance;
        public static IProductRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new IProductRepository();
                return instance;
            }
            set { instance = value; }
        }
        public bool create(Product product)
        {
            if (product != null)
            {
                try
                {
                    DataProvider.Instance.DB.Products.Add(product);
                    DataProvider.Instance.DB.SaveChanges();
                    return true; // Thành công
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while creating product: {ex.Message}");
                    return false; // Thất bại
                }
            }
            return false; // T
        }

        public bool delete(Product product)
        {
            Product p = findById(product.ID);
            if (p != null)
            {
                try
                {
                    DataProvider.Instance.DB.Products.Remove(p);
                    DataProvider.Instance.DB.SaveChanges();
                    return true; // Thành công
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while deleting product: {ex.Message}");
                    return false; // Thất bại
                }
            }
            return false;
        }

        public List<Product> findAll()
        {
            return DataProvider.Instance.DB.Products.ToList();
        }

        public Product findById(int id)
        {
            Product product = DataProvider.Instance.DB.Products.FirstOrDefault(c => c.ID == id);
            if (product != null)
            {
                Console.WriteLine(product.Name);
            }
            else
            {

                Console.WriteLine($"Customer with ID {id} not found.");
            }
            return product;
        }
        public List<Product> findPage(int skipCount, int takeCount,string name, DateTime previousDate, DateTime lastDate, float minPrice, float maxPrice)
        {
            List<Product> orders = DataProvider.Instance.DB.Products.OrderBy(o => o.Name.Contains(name) && o.CreateDate >= previousDate && o.CreateDate <= lastDate && o.PriceSale >= minPrice && o.PriceSale <= maxPrice).Skip(skipCount).Take(takeCount).ToList();
            return orders;
        }
        public List<Product> findByName(string name)
        {
            return DataProvider.Instance.DB.Products.Where(c => c.Name.Contains(name)).ToList();
        }

        public List<Product> sortByCreateDate(DateTime previousDate, DateTime lastDate)
        {
            return DataProvider.Instance.DB.Products.Where(p => p.CreateDate >= previousDate && p.CreateDate <= lastDate)
                                                     .OrderBy(p => p.CreateDate)
                                                     .ToList();
        }

        public List<Product> sortByPrice(float minPrice, float maxPrice)
        {
            return DataProvider.Instance.DB.Products.Where(p => p.PriceSale >= minPrice && p.PriceSale <= maxPrice)
                                                     .OrderBy(p => p.PriceSale)
                                                     .ToList();
        }

        public List<Product> sortByPriceASC()
        {
            return DataProvider.Instance.DB.Products.OrderBy(p => p.PriceSale).ToList();
        }

        public List<Product> sortByPriceDesc()
        {
            return DataProvider.Instance.DB.Products.OrderByDescending(p => p.PriceSale).ToList();
        }

        public List<Product> sortByQuantityASC()
        {
            return DataProvider.Instance.DB.Products.OrderBy(p => p.Quantity).ToList();
        }

        public List<Product> sortByQuantityDesc()
        {
            return DataProvider.Instance.DB.Products.OrderByDescending(p => p.Quantity).ToList();
        }

        public bool update(Product product)
        {
            Product p = findById(product.ID);
            if (p != null)
            {
                try
                {
                    p.Name = product.Name;
                    p.Description = product.Description;
                    p.IDCategory = product.IDCategory;
                    p.PriceImport = product.PriceImport;
                    p.PriceSale = product.PriceSale;
                    p.Discount = product.Discount;
                    p.Quantity = product.Quantity;
                    DataProvider.Instance.DB.SaveChanges();
                    return true; // Thành công
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while updating product: {ex.Message}");
                    return false; // Thất bại
                }
            }
            return false; // Thất bại
        }
        public Tuple<List<Product>, int> findPage(int skipCount, int takeCount, string name, double minPrice, double maxPrice)
        {
            var products = DataProvider.Instance.DB.Products
                .Where(o => o.Name.Contains(name) && o.PriceSale >= minPrice && o.PriceSale <= maxPrice)
                .OrderBy(o => o.ID);
            int count = products.Count();
            List<Product> list = products.Skip(skipCount)
                .Take(takeCount)
                .ToList();
            return new Tuple<List<Product>, int>(list, count);
        }
    }
}
