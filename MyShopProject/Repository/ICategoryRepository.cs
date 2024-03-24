using MyShopProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopProject.Repository
{
    public class ICategoryRepository : CategoryRepository
    {
        public void create(Category category)
        {
            if (category != null)
            {
                DataProvider.Instance.DB.Category.Add(category);
                DataProvider.Instance.DB.SaveChanges();
            }
        }

        public void delete(Category category)
        {
            Category c = findById(category.ID);
            if (c != null)
            {
                DataProvider.Instance.DB.Category.Remove(c);
                DataProvider.Instance.DB.SaveChanges();
            }
        }

        public List<Category> findAll()
        {
            return DataProvider.Instance.DB.Category.ToList();
        }

        public Category findById(int id)
        {
            Category category = DataProvider.Instance.DB.Category.FirstOrDefault(c => c.ID == id);
            if (category != null)
            {
                Console.WriteLine(category.Name);
            }
            else
            {

                Console.WriteLine($"Customer with ID {id} not found.");
            }
            return category;
        }

        public List<Category> findByName(string name)
        {
            return DataProvider.Instance.DB.Category.Where(c => c.Name.Contains(name)).ToList();
        }

        public void update(Category category)
        {
            Category c = findById(category.ID);
            if (c != null)
            {
                c.Name = category.Name;
                
                DataProvider.Instance.DB.SaveChanges();
            }
        }
    }
}
