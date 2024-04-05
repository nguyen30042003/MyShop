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
        public ObservableCollection<Model.Product> Products { get ; set ; }
        public ProductManagerVM() {
            loadProduct();

            Category_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                CategoryManager categoryManager = new CategoryManager();
                categoryManager.ShowDialog();
            });

            Action_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                OpenAddProductDialog();
            });

            GetProductInfo = new RelayCommand<object>((p) => { return true; }, (p) => {
                if (p != null)
                {
                    getProductInfo((Model.Product) p);
                }
            });
        }
        private void loadProduct()
        {
            Task.Run(() => {
                Products = new ObservableCollection<Model.Product>(ProductServiceImpl.Instance.findAll());
            });
        }
        private void OpenAddProductDialog() {
            AddProduct addProduct = new AddProduct();
            var addProductVM = new AddProductVM();
            addProduct.DataContext = addProductVM;
            addProductVM.Click_Handler += () => {
                addProduct.DialogResult = true;
            };

            if (addProduct.ShowDialog() == true) {
                if (ProductServiceImpl.Instance.save(addProductVM.product)) {
                    MessageBox.Show("Add success");
                    Products.Add(addProductVM.product);
                }
            }
        }

        private void getProductInfo(Model.Product p) {
            Window mainWindow = Application.Current.MainWindow;
            MainWidownVM mainWidownVM = (MainWidownVM)mainWindow.DataContext;
            mainWidownVM.productInfo_Click.Execute(p);
        }
    }
}
