using MyShopProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopProject.Repository
{
    internal interface ProductRepository
    {
        void create(Product product);
        void update(Product product);
        void delete(Product product);
        List<Product> findAll();
        List<Product> findByName(string name);
        Product findById(int id);
    }
}
