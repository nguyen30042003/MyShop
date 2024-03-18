﻿using MyShopProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopProject.Repository
{
    public class IProductRepository : ProductRepository
    {
        public void create(Product product)
        {
            if (product != null)
            {
                DataProvider.Instance.DB.Products.Add(product);
                DataProvider.Instance.DB.SaveChanges();
               
            }
        }

        public void delete(Product product)
        {
            Product p = findById(product.ID);
            if (p != null)
            {
                DataProvider.Instance.DB.Products.Remove(p);
                DataProvider.Instance.DB.SaveChanges();
            }
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

        public List<Product> findByName(string name)
        {
            return DataProvider.Instance.DB.Products.Where(c => c.Name.Contains(name)).ToList();
        }

        public void update(Product product)
        {
            Product p = findById(product.ID);
            if (p != null)
            {
                p.Name = product.Name;
                p.Description = product.Description;
                p.IDCategory = product.IDCategory;
                p.PriceImport = product.PriceImport;
                p.PriceSale = product.PriceSale;
                p.Discount = product.Discount;
                p.Quantity = product.Quantity;
                DataProvider.Instance.DB.SaveChanges();
            }
        }
    }
}
