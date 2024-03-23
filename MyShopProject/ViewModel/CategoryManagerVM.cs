﻿using MyShopProject.Model;
using MyShopProject.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace MyShopProject.ViewModel
{
    public class CategoryManagerVM : BaseViewModel
    {
        public ICommand AddCategory_Click { get; set; }
        public ICommand DeleteCategory_Click { get; set; }
        public string TbCategoryName { get; set; }
        public ObservableCollection<Category> ListCategory { get; set; }
        public Category SelectedCategory { get; set; } // Added property for selected category

        public CategoryManagerVM()
        {
            ListCategory = new ObservableCollection<Category>(categoryList());

            AddCategory_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                Category category = new Category() { ID = 0, Name = TbCategoryName };
                ICategoryRepository categoryRepository = new ICategoryRepository();
                categoryRepository.create(category);
                ListCategory.Add(category);
                MessageBox.Show("Add success");
            });

            DeleteCategory_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                MessageBox.Show("Are you delete category");
                if (SelectedCategory != null)
                {
                    ICategoryRepository categoryRepository = new ICategoryRepository();
                    categoryRepository.delete(SelectedCategory);
                    ListCategory.Remove(SelectedCategory);
                    MessageBox.Show("Delete success");
                }
                else
                {
                    MessageBox.Show("Please select a category to delete.");
                }
            });
        }

        private List<Category> categoryList()
        {
            ICategoryRepository categoryRepository = new ICategoryRepository();
            return categoryRepository.findAll();
        }
    }
}