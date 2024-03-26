using MyShopProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopProject.Service
{
    internal interface CategoryService
    {
        bool save(Category category);
        List<Category> findAll();
        Category findById(int id);
        List<Category> findByName(string name);
        bool delete(Category category);

        bool update(Category category);
        //Pages<Category> 
    }
}
