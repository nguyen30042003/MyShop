using MyShopProject.Model;
using MyShopProject.ServiceImpl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace MyShopProject.ViewModel {
    internal class UpdateProductVM : BaseViewModel {
        public String Action { get; set; }
        public List<Category> Categories { get; set; }
        public int selectedIndex { get; set; }
        public Product product { get; set; }
        public ICommand Action_Click { get; set; }
        public ICommand BrowseImage_Click { get; set; }

        public delegate void Hanlder();
        public event Hanlder Click_Hanlder;
        public UpdateProductVM(Product product_in) {
            product = new Product() {
                ID = product_in.ID,
                Name = product_in.Name,
                CreateDate = product_in.CreateDate,
                Quantity = product_in.Quantity,
                PriceSale = product_in.PriceSale,
                PriceImport = product_in.PriceImport,
                IDCategory = product_in.IDCategory,
                Discount = product_in.Discount,
                Image = product_in.Image,
                Description = product_in.Description
            };
            
            Action = "Update";
            loadCategory();
            BrowseImage_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png|All files (*.*)|*.*";
                
                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    product.Image = openFileDialog.FileName;
                }
            });
            Action_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                if (selectedIndex != -1) {
                    product.IDCategory = Categories.ElementAt(selectedIndex).ID;
                    product.Category = Categories.ElementAt(selectedIndex);
                } else {
                    product.IDCategory = null;
                    product.Category = null;
                }
                Click_Hanlder.Invoke();
            });
        }
        private void loadCategory() {
            Categories = CategoryServiceImpl.Instance.findAll();
            var selectedCategory = Categories.Where(c => c.ID == product.IDCategory).FirstOrDefault();
            selectedIndex = selectedCategory == null ? -1 : Categories.IndexOf(selectedCategory);
        }
    }
}
