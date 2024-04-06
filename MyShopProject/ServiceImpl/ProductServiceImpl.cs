using DocumentFormat.OpenXml.Wordprocessing;
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

        public List<Product> findAll()
        {
            List<Product> products = new List<Product>();   
            products = IProductRepository.Instance.findAll();
            return products;
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

        public Tuple<List<Product>, int> findAllPage(int skipCount, int takeCount)
        {
            return IProductRepository.Instance.findPage(skipCount, takeCount, "", double.MinValue, double.MaxValue);

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
    }
}
