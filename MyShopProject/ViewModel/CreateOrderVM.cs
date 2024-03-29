using MyShopProject.Model;
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
    internal class CreateOrderVM : BaseViewModel
    {
        public string cbName { get; set; }
        public string cbProduct { get; set;}
        public ICommand addProduct_Click { get; set; }
        public ICommand payment_Click { get; set; } 
        public string quantityProduct {  get; set; }
        public string priceProduct { get; set; }

        public string address { get; set; }
        public string phone { get; set; }

        public string[] productList { get; set; }
        public string[] customerList { get; set; }

        
        public CreateOrderVM() {
            loadCustomerAndProduct();
            addProduct_Click = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {

            });
        }
        public void loadCustomerAndProduct()
        {
            var list = CustomerServiceImpl.Instance.findAll();
            customerList = list.Select(c => c.Full_Name).ToArray();
            List<Product> l = ProductServiceImpl.Instance.findAll();
            productList = l.Select(c=> c.Name).ToArray();
            MessageBox.Show(productList[1]);
        }
    }
}
