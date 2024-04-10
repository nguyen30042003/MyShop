using MyShopProject.Model;
using MyShopProject.Pages;
using MyShopProject.ServiceImpl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;
using System.Xml;

namespace MyShopProject.ViewModel {
    public class MainWidownVM : BaseViewModel
    {
        public ICommand dashboard_Click {  get; set; }
        public ICommand productManager_Click { get; set; }
        public ICommand orderManager_Click { get; set; }
        public ICommand setting_Click { get; set; }
        public ICommand analystBudget_Click { get; set; }
        public ICommand quit_Click { get; set; }
        public ICommand customerManager_Click { get; set; }
        public ICommand productInfo_Click { get; set; }
        public UserControl content { get; set; }
        public BaseViewModel viewModel { get; set; }

        private string lastOpenedControlKey = "LastOpenedControl";
        private string lastProductKey = "LastProductID";

        public MainWidownVM()
        {
            quit_Click = new RelayCommand<Object>((p) => { return true; }, (p) => Quit());
            dashboard_Click = new RelayCommand<Object>((p) => { return true; }, (p) => NavigateToPage(new Dashboard()));
            productManager_Click = new RelayCommand<Object>((p) => { return true; }, (p) => NavigateToPage(new ProductManager()));
            orderManager_Click = new RelayCommand<Object>((p) => { return true; }, (p) => NavigateToPage(new OrderManager()));
            setting_Click = new RelayCommand<Object>((p) => { return true; }, (p) => NavigateToPage(new Setting()));
            analystBudget_Click = new RelayCommand<Object>((p) => { return true; }, (p) => NavigateToPage(new AnalystBudget()));
            customerManager_Click = new RelayCommand<Object>((p) => { return true; }, (p) => NavigateToPage(new CustomerManager()));
            productInfo_Click = new RelayCommand<Object>((p) => { return true; }, (p) => NavigateToPage(new ProductInfo((Model.Product)p)));
            
            LoadLastOpenedControl();
        }
        private void NavigateToPage(UserControl page)
        {
            content = page;
        }
        private void Quit() {
            SaveLastOpenedControl(content.GetType());
            Application.Current.Shutdown();
        }

        private void SaveLastOpenedControl(Type controlType)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[lastOpenedControlKey].Value = controlType.FullName;
            if (controlType == typeof(ProductInfo)) {
                ProductInfoVM productInfoVM = content.DataContext as ProductInfoVM;
                config.AppSettings.Settings[lastProductKey].Value = productInfoVM.product.ID.ToString();
            }
            config.Save(ConfigurationSaveMode.Minimal);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void LoadLastOpenedControl()
        {
            string lastOpenedControl = ConfigurationManager.AppSettings[lastOpenedControlKey];
            if (!string.IsNullOrEmpty(lastOpenedControl))
            {
                Type controlType = Type.GetType(lastOpenedControl);
                if (controlType != null)
                {
                    if (controlType == typeof(ProductInfo)) {
                        string lastProductID = ConfigurationManager.AppSettings[lastProductKey];
                        content = new ProductInfo(ProductServiceImpl.Instance.findById(int.Parse(lastProductID)));
                    }
                    else
                        content = (UserControl)Activator.CreateInstance(controlType);
                    return;
                }
            }
            
            // If no last opened control exists or failed to load, default to Dashboard
            content = new Dashboard();
        }
    }
}
