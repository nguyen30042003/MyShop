using MyShopProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyShopProject.Pages {
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window {
        LoginVM LoginViewModel { get; set; }
        public Login() {
            InitializeComponent();
            LoginViewModel = new LoginVM();
            passwordbox.Password = LoginViewModel.password;
            DataContext = LoginViewModel;
        }

        private void ChangePassword(object sender, RoutedEventArgs e) {
            LoginViewModel.password = passwordbox.Password;
        }
    }
}
