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
using System.Collections.ObjectModel;
using System.CodeDom;
namespace MyShopProject.ViewModel {
    internal class ProductManagerVM : BaseViewModel {
        public String Action {  get; set; }
        public ICommand Action_Click {  get; set; }
        public ICommand GetProductInfo {  get; set; }
        public ICommand Category_Click { get; set; }
        private ObservableCollection<Model.Product> _Products { get; set; }
        public ObservableCollection<Model.Product> Products { get => _Products; set { _Products = value; OnPropertyChanged(nameof(Products)); } }
        public ProductManagerVM() {
            loadProduct();
            Category_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                CategoryManager categoryManager = new CategoryManager();
                categoryManager.ShowDialog();
            });
            Action_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                OpenAddProductDialog();
                loadProduct();
            });
                GetProductInfo = new RelayCommand<object>((p) => { return true; }, (p) => {
                    if (p != null && p is int productId)
                    {
                        getProductInfo(productId);
                    }

                });
        }
        private void loadProduct()
        {

            Products = new ObservableCollection<Model.Product>(ProductServiceImpl.Instance.findAll());
           
        }
        private AddProduct addProduct { get; set; }
        private void OpenAddProductDialog() {
            var addProductVM = new AddProductVM();
            addProductVM.Click_Handler += AddProduct;

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
            ProductInfoVM productInfoVM = new ProductInfoVM(p);
            ProductInfo productInfo = new ProductInfo();

            Window mainWindow = Application.Current.MainWindow;
            MainWidownVM mainWidownVM = (MainWidownVM)mainWindow.DataContext;
            mainWidownVM.productInfo_Click.Execute(p);
        }
    }
}
