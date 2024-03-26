using MyShopProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopProject.Service
{
    internal interface ProductService
    {
        bool save(Product product);
        bool update(Product product);
        List<Product> findAll();
        List<Product> findByName(string name);
        Product findById(int id);
        bool delete(Product product);

        List<Product> sortByCreateDate(DateTime previousDate, DateTime lastDate);
        List<Product> sortByPrice(float minPrice, float maxPrice);
        List<Product> sortByPriceASC();
        List<Product> sortByPriceDesc();
        List<Product> sortByQuantityASC();
        List <Product> sortByQuantityDesc();
    }
}
