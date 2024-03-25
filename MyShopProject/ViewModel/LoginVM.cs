using MyShopProject.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyShopProject.ViewModel {
    internal class LoginVM : BaseViewModel {
        public ICommand LoginCommand { get; set; }
        public String username { get; set; } = "";
        public String password { get; set; } = "";
        public LoginVM() {
            LoginCommand = new RelayCommand<Object>((p) => { return true; }, (p) => VerifyLogin());
        }

        private void VerifyLogin() {
             if (username == "admin" && password == "admin") {
                new MainWindow().Show();
                Application.Current.Windows[0].Close();
             }
        }
    }
}
