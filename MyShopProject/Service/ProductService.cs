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
        List<Product> findAll(DateTime start, DateTime end, int min, int max, string name, int sortOption);
        List<Product> findByName(string name);
        Product findById(int id);
        bool delete(Product product);

        bool deleteByIdCategory (int id);
        List<Product> sortByCreateDate(DateTime previousDate, DateTime lastDate);
        List<Product> sortByPrice(float minPrice, float maxPrice);
        List<Product> sortByPriceASC();
        List<Product> sortByPriceDesc();
        List<Product> sortByQuantityASC();
        List <Product> sortByQuantityDesc();
    }
}
