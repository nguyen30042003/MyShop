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
        public ICommand GetProductInfo {  get; set; }
        public List<int> products { get; set; }
        public ProductManagerVM() {
            products = new List<int>();
            products.Add(1);
            products.Add(2);
            products.Add(3);
            products.Add(4);
            products.Add(5);
            products.Add(6);
            products.Add(7);
            Action_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                OpenUpdateProductDialog();
            });
            GetProductInfo = new RelayCommand<object>((p) => { return true; }, (p) => {
                getProductInfo((int) p);
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
                MessageBox.Show(product.Image);
            }
            updateProductVM.Click_Hanlder += UpdateProduct;

            addProduct = new AddProduct();
            addProduct.DataContext = updateProductVM;
            if (addProduct.ShowDialog() == true) {

            }
        }

        private void getProductInfo(int p) {
            Window mainWindow = Application.Current.MainWindow;
            MainWidownVM mainWidownVM = (MainWidownVM)mainWindow.DataContext;
            mainWidownVM.analystBudget_Click.Execute(null);
        }
    }
}
