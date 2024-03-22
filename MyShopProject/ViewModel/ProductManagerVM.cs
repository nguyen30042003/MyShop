using MyShopProject.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyShopProject.ViewModel {
    internal class ProductManagerVM : BaseViewModel {
        public ICommand addProduct_Click {  get; set; }
        public ProductManagerVM() {
            addProduct_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                openAddProductDialog();
            });
        }

        private void openAddProductDialog() {
            AddProduct addProduct = new AddProduct();
            addProduct.ShowDialog();
        }
    }
}
