using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
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

        public int cbName { get; set; }
        private int _cbProduct {  get; set;
        }
        public int cbProduct {
            get => _cbProduct;
            set
            {
                _cbProduct = value;
                OnPropertyChanged(nameof(cbProduct));
                UpdatePriceProduct();
            }
        }
        public ICommand addProduct_Click { get; set; }
        public ICommand payment_Click { get; set; }
        public string quantityProduct {  get; set; }

        private string _priceProduct { get; set; }
        public string priceProduct {
            get => _priceProduct;
            set
            {
                _priceProduct = value;
                OnPropertyChanged(nameof(priceProduct));
            }
        }

        public string address { get; set; }
        public string phone { get; set; }

        public string[] productList { get; set; }
        public int[] productIdList { get; set; }
        public string[] customerList { get; set; }
        public int[] customerIdList { get; set; }
        
        public float totalPrice { get; set; }
        public float totalQuantity { get; set; }

        private string _IdOrder {  get; set; }
        public string IdOrder {  
            get => _IdOrder;
            set
            {
                _IdOrder = value;
                OnPropertyChanged(nameof(IdOrder));
            }
        }
        private string _nameCustomer;
        public string nameCustomer
        {
            get => _nameCustomer;
            set
            {
                _nameCustomer = value;
                OnPropertyChanged(nameof(nameCustomer));
            }
        }

        private string _Address;
        public string Address
        {
            get => _Address;
            set
            {
                _Address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        private string _Phone;
        public string Phone
        {
            get => _Phone;
            set
            {
                _Phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        private string _TotalQuantity;
        public string TotalQuantity
        {
            get => _TotalQuantity;
            set
            {
                _TotalQuantity = value;
                OnPropertyChanged(nameof(TotalQuantity));
            }
        }

        private string _TotalPrice;
        public string TotalPrice
        {
            get => _TotalPrice;
            set
            {
                _TotalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }


        public ObservableCollection<ItemList> itemList { get; set; }


        public CreateOrderVM() {
            itemList = new ObservableCollection<ItemList>();
            loadCustomerAndProduct();
            UpdatePriceProduct();
            addProduct_Click = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                loadItemList(productIdList[cbProduct], int.Parse(quantityProduct));
                loadTotalBill();
            });

            payment_Click = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                if(addItem())
                {
                    MessageBox.Show("Add Order Success");
                }    
            });
        }
        private int _quantityCurrent { get; set; }
        public int quantityCurrent { 
            get => _quantityCurrent;
            set
            {
                _quantityCurrent = value;
                OnPropertyChanged(nameof(quantityCurrent));
            }
        }
        private double _priceCurrent { get; set; }
        public double priceCurrent
        {
            get => _priceCurrent;
            set
            {
                _priceCurrent = value;
                OnPropertyChanged(nameof(priceCurrent));
            }
        }
        private void loadTotalBill()
        {
            List<Order> orderIdList = OrderServiceImpl.Instance.findAll();
            if(orderIdList.Count > 0)
            {
                IdOrder = (orderIdList[orderIdList.Count - 1].ID + 1).ToString();
            }
            else
            {
                IdOrder = "#1";
            }
            nameCustomer = customerList[cbName];
            Phone = phone;
            Address = address;
            int quantity = int.Parse(quantityProduct);
            quantityCurrent += quantity;
            double i = double.Parse(priceProduct);
            priceCurrent += i * quantity;
            TotalQuantity = quantityCurrent.ToString();
            TotalPrice = priceCurrent.ToString();

        }
        private void UpdatePriceProduct()
        {
            if (cbProduct >= 0 && cbProduct < productIdList.Length)
            {
                Product selectedProduct = ProductServiceImpl.Instance.findById(productIdList[cbProduct]);
                priceProduct = selectedProduct.PriceSale.Value.ToString();
            }
        }
        private void loadItemList(int idProduct, int quantity)
        {
            Product product = ProductServiceImpl.Instance.findById(idProduct);
            ItemList item = new ItemList() { ID = idProduct, productName = product.Name, quantity = quantity, price = product.PriceSale.Value * quantity };
            itemList.Add(item);         
        }
        private bool addItem()
        {
            List<Item> items = new List<Item>();
            List<Order> orderIdList = OrderServiceImpl.Instance.findAll();
            Order order = new Order() { CreateDate = DateTime.Now, IDCustomer = customerIdList[cbName], TotalPrice = priceCurrent, TotalQuantity=quantityCurrent  };
            if(!OrderServiceImpl.Instance.save(order))
            {
                return false;
            }    
            int o = 1;
            if (orderIdList.Count > 1)
            {
                o = orderIdList[orderIdList.Count - 1].ID + 1;
            }
            foreach (var i in itemList)
            {
                Item item = new Item() { ID = 0, IDOrder = o, IDProduct = i.ID, Price = i.price, Quantity = i.quantity };
                if (!ItemServiceImpl.Instance.save(item))
                {
                    return false;
                }
            }
            return true;
        }
        public void loadCustomerAndProduct()
        {
            var list = CustomerServiceImpl.Instance.findAll();
            customerList = list.Select(c => c.Full_Name).ToArray();
            customerIdList = list.Select(c=>c.ID).ToArray();
            List<Product> l = ProductServiceImpl.Instance.findAll();
            productList = l.Select(c=> c.Name).ToArray();
            productIdList = l.Select(c => c.ID).ToArray();
        }
    }
    public class ItemList
    {
        public int ID { get; set; }
        public string productName { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
    }
}
