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
        public Visibility ListVisible { get; set; } = Visibility.Hidden;
        public Visibility TextVisible { get; set; } = Visibility.Visible;
        public String Action {  get; set; }
        public ICommand Action_Click {  get; set; }
        public ICommand GetProductInfo {  get; set; }
        public ICommand Category_Click { get; set; }
        public ObservableCollection<Model.Product> Products { get ; set ; }

        public ICommand NavigateToPageCommand { get; set; }
        public ICommand Sort_Click { get; set; }
        public ICommand previousPage { get; set; }
        public ICommand nextPage { get; set; }
        private ObservableCollection<int> _pageNumbers;
        public ObservableCollection<int> PageNumbers
        {
            get { return _pageNumbers; }
            set { _pageNumbers = value; OnPropertyChanged(nameof(PageNumbers)); }
        }
        public int currentPage { get; set; }

        private int _totalItems;
        private int _totalPage;
        private int _perPage = 10;
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
            NavigateToPageCommand = new RelayCommand<int>((page) => true, NavigateToPage);
            previousPage = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                if (currentPage > 1)
                {
                    NavigateToPage(currentPage - 1);
                }

            });
            nextPage = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                if (currentPage < _totalPage)
                {
                    NavigateToPage(currentPage + 1);
                }
            });
        }
        private void loadProduct()
        {
            Task.Run(() => {
                NavigateToPage(1);
                //Products = new ObservableCollection<Model.Product>(ProductServiceImpl.Instance.findAll());
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
                    Products.Add(addProductVM.product);
                }
            }
        }

        private void getProductInfo(Model.Product p) {
            Window mainWindow = Application.Current.MainWindow;
            MainWidownVM mainWidownVM = (MainWidownVM)mainWindow.DataContext;
            mainWidownVM.productInfo_Click.Execute(p);
        }

        private void NavigateToPage(int p)
        {
            Page page = new Page();
            var pageResult = page.LoadPage<Model.Product>(new Model.Product(), p);
            currentPage = p;
            _totalPage = page.TotalPage;
            Products = new ObservableCollection<Model.Product>(pageResult.Item1.Cast<Model.Product>());
            PageNumbers = pageResult.Item2;
        }
    }
}
