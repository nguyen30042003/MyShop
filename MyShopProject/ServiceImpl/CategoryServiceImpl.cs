using MyShopProject.Model;
using MyShopProject.Repository;
using MyShopProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyShopProject.ServiceImpl
{
    public class CategoryServiceImpl : CategoryService
    {
        public bool delete(Category category)
        {
            if (ICategoryRepository.Instance.delete(category))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Category> findAll()
        {
            List<Category> categories = new List<Category>();
            categories = ICategoryRepository.Instance.findAll();
            return categories;
        }

        public Category findById(int id)
        {
            Category category = new Category();
            category = ICategoryRepository.Instance.findById(id);
            return category;
        }

        public List<Category> findByName(string name)
        {
            List<Category> categories = new List<Category>();
            categories = ICategoryRepository.Instance.findByName(name);
            return categories;

        }

        public bool save(Category category)
        {
            if(ICategoryRepository.Instance.create(category) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool update(Category category)
        {
            if (ICategoryRepository.Instance.update(category) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static CategoryServiceImpl instance;
        public static CategoryServiceImpl Instance
        {
            get
            {
                if (instance == null)
                    instance = new CategoryServiceImpl();
                return instance;
            }
            set { instance = value; }
        }
    }
}
