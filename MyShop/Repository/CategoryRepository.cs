using MyShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Repository
{
    internal interface CategoryRepository
    {
        bool Insert(String name);
        bool Update(Category category);

        bool Delete(Category category);
        Category FindById(int id);
        Category FindByName(string name);
        List<Category> FindAll();
    }
}
