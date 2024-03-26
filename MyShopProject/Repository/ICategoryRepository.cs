using MyShopProject.Model;
using MyShopProject.ServiceImpl;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyShopProject.Repository
{
    public class ICategoryRepository : CategoryRepository
    {
        private static ICategoryRepository instance;
        public static ICategoryRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new ICategoryRepository();
                return instance;
            }
            set { instance = value; }
        }
        public bool create(Category category)
        {
            try
            {
                DataProvider.Instance.DB.Categories.Add(category);
                DataProvider.Instance.DB.SaveChanges();
                return true;
            }catch(SqlException ex)
            {
                return false;
            }
            return false;
        }


        public bool delete(Category category)
        {
            Category c = findById(category.ID);
            if (c != null)
            {
                try
                {
                    DataProvider.Instance.DB.Categories.Remove(c);
                    DataProvider.Instance.DB.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while deleting category: {ex.Message}");
                    return false; 
                }
            }
            return false;
        }


        public List<Category> findAll()
        {
            return DataProvider.Instance.DB.Categories.ToList();
        }

        public Category findById(int id)
        {
            Category category = DataProvider.Instance.DB.Categories.FirstOrDefault(c => c.ID == id);
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
            return DataProvider.Instance.DB.Categories.Where(c => c.Name.Contains(name)).ToList();
        }

        public bool update(Category category)
        {
            Category c = findById(category.ID);
            if (c != null)
            {
                c.Name = category.Name;
                DataProvider.Instance.DB.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
