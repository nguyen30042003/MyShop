using MyShopProject.Model;
using MyShopProject.Pages;
using MyShopProject.ServiceImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyShopProject.ViewModel
{
    public class ProductInfoVM : BaseViewModel
    {
        public ICommand Update_Click { get; set; }
        public ICommand Return_Click { get; set; }
        public ICommand Delete_Click { get; set; }
        public string Category {  get; set; }
        public Model.Product product { get; set; }

        public ProductInfoVM(Model.Product product)
        {
            this.product = product;
            Update_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                OpenUpdateProductDialog();
            });

            Return_Click = new RelayCommand<object>((p) => { return true; }, (p) => ReturnToProductManager());

            Delete_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                DeleteProduct();
                Return_Click.Execute(null);
                
            });
        }

        private void DeleteProduct() {
            if (ProductServiceImpl.Instance.delete(product))
                MessageBox.Show("Delete success");
        }

        private void ReturnToProductManager() {
            Window mainWindow = Application.Current.Windows[0];
            MainWidownVM mainWidownVM = (MainWidownVM) mainWindow.DataContext;
            mainWidownVM.productManager_Click.Execute(null);
        }

        private void OpenUpdateProductDialog() {
            AddProduct addProduct = new AddProduct();

            var updateProductVM = new UpdateProductVM(product);
            updateProductVM.Click_Hanlder += () => {
                addProduct.DialogResult = true;
            };
            addProduct.DataContext = updateProductVM;

            if (addProduct.ShowDialog() == true) {
                product.Name = updateProductVM.product.Name;
                product.CreateDate = updateProductVM.product.CreateDate;
                product.Quantity = updateProductVM.product.Quantity;
                product.PriceSale = updateProductVM.product.PriceSale;
                product.PriceImport = updateProductVM.product.PriceImport;
                product.IDCategory = updateProductVM.product.IDCategory;
                product.Category = updateProductVM.product.Category;
                product.Discount = updateProductVM.product.Discount;
                product.Image = updateProductVM.product.Image;
                product.Description = updateProductVM.product.Description;
                if (ProductServiceImpl.Instance.update(product)) {
                    MessageBox.Show("Update success");
                }
            }
        }
    }

}
