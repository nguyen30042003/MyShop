using MyShopProject.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyShopProject.ViewModel {
    internal class ProductManagerVM : BaseViewModel {
        public String Action {  get; set; }
        public ICommand Action_Click {  get; set; }
        public ProductManagerVM() {
            Action_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                OpenUpdateProductDialog();
            });
        }
        private AddProduct addProduct { get; set; }
        private void OpenAddProductDialog() {
            var addProductVM = new AddProductVM();
            addProductVM.Click_Hanlder += AddProduct;

            addProduct = new AddProduct();
            addProduct.DataContext = addProductVM;
            if (addProduct.ShowDialog() == true) {

            }
        }

        private void AddProduct() {
            addProduct.DialogResult = true;
            MessageBox.Show("Add product");
        }

        private void OpenUpdateProductDialog() {
            var product = new Model.Product() { Name = "Haha", Image = "Haha" };
            var updateProductVM = new UpdateProductVM(product);
            void UpdateProduct() {
                addProduct.DialogResult = true;
                MessageBox.Show(product.Name);
            }
            updateProductVM.Click_Hanlder += UpdateProduct;

            addProduct = new AddProduct();
            addProduct.DataContext = updateProductVM;
            if (addProduct.ShowDialog() == true) {

            }
        }

    }
}
