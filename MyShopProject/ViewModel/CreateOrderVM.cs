using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using MyShopProject.Model;
using MyShopProject.ServiceImpl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Item = MyShopProject.Model.Item;

namespace MyShopProject.ViewModel
{
    internal class CreateOrderVM : BaseViewModel
    {
        public delegate void Handler();
        public event Handler Click_Handler;
        public ICommand addProduct_Click { get; set; }
        public ICommand payment_Click { get; set; }
        public List<Model.Product> products { get; set; }
        public List<Customer> customers { get; set; }
        public Order newOrder { get; set; }
        public Product product { get; set; }
        public Customer customer { get; set; }
        public string quantity {  get; set; }
        public ObservableCollection<Item> items { get; set; }   
        public string address {  get; set; }
        
        public CreateOrderVM() {
            loadCustomerAndProduct();
            items = new ObservableCollection<Item>();
            product = new Model.Product();
            newOrder = new Order();
            newOrder.TotalQuantity = 0;
            newOrder.TotalPrice = 0;
            addProduct_Click = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                Item item = new Item()
                {
                    Product = product,
                    Quantity = int.Parse(quantity),
                    Price = product.PriceSale * int.Parse(quantity),
                    Profit = int.Parse(quantity) * (product.PriceSale * (product.Discount == 0 ? 1 : product.Discount) - product.PriceImport)
                };

                bool flag = false;
                for (var i = 0; i < items.Count; i++)
                {
                    if (items[i].Product.ID == item.Product.ID)
                    {
                        newOrder.TotalQuantity += item.Quantity;
                        newOrder.TotalPrice += item.Price;
                        item.Quantity += items[i].Quantity;
                        item.Price += items[i].Price;
                        item.Profit += items[i].Quantity * (items[i].Product.PriceSale * (product.Discount == 0 ? 1 : product.Discount) - items[i].Product.PriceImport);
                        flag = true;
                        items[i] = item;
                        break;
                    }
                }
                if (!flag)
                {
                    items.Add(item);
                    newOrder.TotalQuantity += item.Quantity;
                    newOrder.TotalPrice += item.Price;
                }
            });

            payment_Click = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                newOrder.Item = items;
                newOrder.IDCustomer = customer.ID;
                newOrder.CreateDate = DateTime.Now.Date;
                Click_Handler.Invoke();
            });
        }

        public void loadCustomerAndProduct()
        {
            customers = CustomerServiceImpl.Instance.findAll();
            products = ProductServiceImpl.Instance.findAll(DateTime.Parse($"01/01/{DateTime.Now.Year}"), DateTime.Now.AddDays(1));
        }
    }
}
