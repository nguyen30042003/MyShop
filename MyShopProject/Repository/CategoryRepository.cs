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
        void create(Category category);
        void update(Category category);
        void delete(Category category);
        List<Category> findAll();
        List<Category> findByName(string name);
        Category findById(int id);
    }
}
