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
using DocumentFormat.OpenXml.Spreadsheet;
using MyShopProject.Repository;
namespace MyShopProject.ViewModel {
    internal class ProductManagerVM : BaseViewModel {
        public Visibility ListVisible { get; set; }
        public Visibility TextVisible { get; set; }
        public String Action {  get; set; }
        public DateTime StartDate { get; set; } = DateTime.Parse($"01/01/{DateTime.Now.Year}");
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);

        public String Search { get; set; } = "";
        public List<string> SortOptions { get; set; }
        public int selectedSortOptions { get; set; } = 0;
        public String MinPrice { get; set; } = "";
        public String MaxPrice { get; set; } = "";
        public ICommand Action_Click {  get; set; }
        public ICommand GetProductInfo {  get; set; }
        public ICommand Category_Click { get; set; }
        public ICommand Search_Click { get; set; }
        public ObservableCollection<Model.Product> Products { get ; set ; }
        public ProductManagerVM() {
            SortOptions = new List<string>() { "Ascending" , "Descending" };
            loadProduct();

            Category_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                CategoryManager categoryManager = new CategoryManager();
                categoryManager.ShowDialog();
            });

            Action_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                OpenAddProductDialog();
            });

            Search_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                loadProduct();
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
            ListVisible = Visibility.Hidden;
            TextVisible = Visibility.Visible;
            Task.Run(() => {
                var MinMaxValues = GetMinMax();

                Products = new ObservableCollection<Model.Product>(ProductServiceImpl.Instance.findAll(StartDate, EndDate, MinMaxValues.Item1, MinMaxValues.Item2, Search, selectedSortOptions));
                TextVisible = Visibility.Hidden;
                ListVisible = Visibility.Visible;
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
                    loadProduct();
                }
            }
        }

        private void getProductInfo(Model.Product p) {
            Window mainWindow = Application.Current.MainWindow;
            MainWidownVM mainWidownVM = (MainWidownVM)mainWindow.DataContext;
            mainWidownVM.productInfo_Click.Execute(p);
        }

        private Tuple<int, int> GetMinMax() {
            int min;
            int.TryParse(MinPrice, out min);

            int max;
            if (!int.TryParse(MaxPrice, out max)) {
                max = int.MaxValue;
            }

            return new Tuple<int, int>(min, max);
        }
    }
}
