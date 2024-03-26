using MyShopProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopProject.Repository
{
    internal interface CategoryRepository 
    {
        bool create(Category category);
        bool update(Category category);
        bool delete(Category category);
        List<Category> findAll();
        List<Category> findByName(string name);
        Category findById(int id);
    }
}
