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
        bool create(Product product);
        bool update(Product product);
        bool delete(Product product);
        List<Product> findAll();
        List<Product> findByName(string name);
        Product findById(int id);

    }
}
