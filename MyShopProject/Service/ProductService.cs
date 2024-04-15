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
        Tuple<List<Product>, int> findAllPage(DateTime start, DateTime end, int skip, int take, int min, int max, string name, int sortOption,Category category);
        List<Product> findByName(string name);
        Product findById(int id);
        bool delete(Product product);
        bool deleteByIdCategory (int id);
        int totalQuantityProductInStock();
    }
}
