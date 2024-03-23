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
        public ICommand OpenAddProduct_Click {  get; set; }
        public ProductManagerVM() {
            OpenAddProduct_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                OpenAddProductDialog();
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
            var updateProductVM = new UpdateProductVM(new Model.Product() { Name = "Haha"});
            updateProductVM.Click_Hanlder += UpdateProduct;

            addProduct = new AddProduct();
            addProduct.DataContext = updateProductVM;
            if (addProduct.ShowDialog() == true) {

            }
        }

        private void UpdateProduct() {
            addProduct.DialogResult = true;
            MessageBox.Show("Update product");
        }
    }
}
