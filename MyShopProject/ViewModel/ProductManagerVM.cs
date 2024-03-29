using MyShopProject.Pages;
using MyShopProject.ServiceImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MyShopProject.Model;
namespace MyShopProject.ViewModel {
    internal class ProductManagerVM : BaseViewModel {
        public String Action {  get; set; }
        public ICommand Action_Click {  get; set; }
        public ICommand GetProductInfo {  get; set; }


        public List<Model.Product> Products { get; set; }
        public ProductManagerVM() {
            loadProduct();
            Action_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                OpenAddProductDialog();
            });
            GetProductInfo = new RelayCommand<object>((p) => { return true; }, (p) => {
                getProductInfo((int) p);
            });
        }
        private void loadProduct()
        {

            Products = ProductServiceImpl.Instance.findAll();
           
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
            OnPropertyChanged(nameof(Products));
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
