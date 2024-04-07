using DocumentFormat.OpenXml.Wordprocessing;
using MyShopProject.Model;
using MyShopProject.Repository;
using MyShopProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyShopProject.ServiceImpl
{
    internal class ProductServiceImpl : ProductService
    {
        private static ProductServiceImpl instance;
        public static ProductServiceImpl Instance
        {
            get
            {
                if (instance == null)
                    instance = new ProductServiceImpl();
                return instance;
            }
            set { instance = value; }
        }
        public bool delete(Product product)
        {
            if (IProductRepository.Instance.delete(product))
            {
                return true;
            }
            return false;
        }

        public bool deleteByIdCategory(int id)
        {
            List<Product> products = new List<Product>();
            products = IProductRepository.Instance.findAll();
            foreach(var product in products)
            {
                if(product.IDCategory == id)
                {
                    delete(product);
                }
            }
            return true;
        }

        public Tuple<List<Product>, int> findAllPage(DateTime start, DateTime end, int skip, int take, int min = 0, int max = int.MaxValue, string name = "", int sortOption = 1)
        {
            var products = IProductRepository.Instance.findAll()
                .Where(p =>
                    ((p.PriceSale ?? 0) <= max && (p.PriceSale ?? 0) >= min)
                    && (p.CreateDate <= end && p.CreateDate >= start)
                    && p.Name.ToLower().Contains(name.ToLower()));
            int count = products.Count();
            if (sortOption == 0)
                return Tuple.Create(products.OrderBy(p => p.PriceSale).Skip(skip).Take(take).ToList(), count);
            else
                return Tuple.Create(products.OrderByDescending(p => p.PriceSale).Skip(skip).Take(take).ToList(), count);
        }

        public Product findById(int id)
        {
            Product product = new Product();
            product = IProductRepository.Instance.findById(id);
            return product;
        }

        public List<Product> findByName(string name)
        {
            List<Product> products = new List<Product>();
            products = IProductRepository.Instance.findByName(name);
            return products;
        }

        public bool save(Product product)
        {
            if (IProductRepository.Instance.create(product))
            {
                return true;
            }
            return false;
        }

        public bool update(Product product)
        {
            if (IProductRepository.Instance.update(product))
            {
                return true;
            }
            return false;
        }

        public int totalQuantityProductInStock()
        {
            int total = 0;
            List<Product> products = findAll();
            foreach (var p in products)
            {
                total += p.Quantity.Value;
            }
            return total;
        }

        public List<Product> findAll() {
            return IProductRepository.Instance.findAll();
        }
    }
}
