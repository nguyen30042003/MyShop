using MyShopProject.Model;
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
        public Product product { get; set; }
        public ICommand Action_Click { get; set; }
        public ICommand BrowseImage_Click { get; set; }

        public delegate void Hanlder();
        public event Hanlder Click_Hanlder;
        public UpdateProductVM(Product product) {
            Categories = new List<Category>() {
                new Category() {Name = "Haha"},
                new Category() {Name = "Hihi"}
            };
            BrowseImage_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png|All files (*.*)|*.*";
                
                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    product.Image = openFileDialog.FileName;
                    OnPropertyChanged("product");
                }
            });
            this.product = new Product() { Name=product.Name, Image=product.Image };
            Action = "Update";
            Action_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                Click_Hanlder.Invoke();
            });
        }
    }
}
