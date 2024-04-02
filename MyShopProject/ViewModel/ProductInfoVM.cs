using MyShopProject.Model;
using MyShopProject.ServiceImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyShopProject.ViewModel
{
    public class ProductInfoVM : BaseViewModel
    {
        public ICommand Update_Click { get; set; }
        public ICommand Delete_Click { get; set; }
        public string Category {  get; set; }
        private Product _product;
        public Product product
        {
            get => _product;
            set
            {
                _product = value;
                OnPropertyChanged(nameof(product));
            }
        }

        public ProductInfoVM(int productId)
        {
            LoadProductInfo(productId);
        }

        private void LoadProductInfo(int productId)
        {

            product = ProductServiceImpl.Instance.findById(productId);

            }
    }

}
