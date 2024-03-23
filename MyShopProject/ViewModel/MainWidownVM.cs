using MyShopProject.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyShopProject.ViewModel
{
    public class MainWidownVM : BaseViewModel
    {
        public bool IsLoaded = false;
        public ICommand dashboad_Click {  get; set; }
        public ICommand productManager_Click { get; set; }
        public ICommand orderManager_Click { get; set; }
        public ICommand setting_Click { get; set; }
        public ICommand analystBudget_Click { get; set; }
        public ICommand quit_Click { get; set; }

        private UserControl _currentPage;

        public UserControl content
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(content));
            }
        }
        public MainWidownVM()
        {
            if (!IsLoaded)
            {
                IsLoaded = true;
                quit_Click = new RelayCommand<Object>((p) => { return true; }, (p) => Application.Current.Shutdown());
                dashboad_Click = new RelayCommand<Object>((p) => { return true; }, (p) => NavigateToPage(new Dashboard()));
                productManager_Click = new RelayCommand<Object>((p) => { return true; }, (p) => NavigateToPage(new ProductManager()));
                orderManager_Click = new RelayCommand<Object>((p) => { return true; }, (p) => NavigateToPage(new OrderManager()));
                setting_Click = new RelayCommand<Object>((p) => { return true; }, (p) => NavigateToPage(new Setting()));
                analystBudget_Click = new RelayCommand<Object>((p) => { return true; }, (p) => NavigateToPage(new AnalystBudget()));
            }
        }
        private void NavigateToPage(UserControl page)
        {
            content = page;
        }
    }
}
