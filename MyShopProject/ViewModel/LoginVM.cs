using MyShopProject.Model;
using MyShopProject.Pages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyShopProject.ViewModel {
    internal class LoginVM : BaseViewModel {
        public ICommand LoginCommand { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string Database { get; set; }
        public string Server { get; set; }
        public LoginVM() {
            LoginCommand = new RelayCommand<Object>((p) => { return true; }, (p) => VerifyLogin());
            LoadLoginInfo();
        }

        private void LoadLoginInfo() {
            username = ConfigurationManager.AppSettings["username"];
            var encryptedPassword = Convert.FromBase64String(ConfigurationManager.AppSettings["password"]);
            var entropy = Convert.FromBase64String(ConfigurationManager.AppSettings["entropy"]);
            var cypherText = ProtectedData.Unprotect(
                encryptedPassword,
                entropy,
                DataProtectionScope.CurrentUser
            );
            password = Encoding.UTF8.GetString(cypherText);
            password = Encoding.UTF8.GetString(cypherText);
            Database = ConfigurationManager.AppSettings["database"];
            Server = ConfigurationManager.AppSettings["server"];
        }

        private void VerifyLogin() {
            if (username == null || password == null ||
                Database == null || Server == null) {
                MessageBox.Show("All field must not be empty");
                return;
            }
            String connectionString = $"metadata=res://*/Model.Model1.csdl|res://*/Model.Model1.ssdl|res://*/Model.Model1.msl;provider=System.Data.SqlClient;provider connection string=\"data source={Server};initial catalog={Database};integrated security=True;trustservercertificate=True;MultipleActiveResultSets=True;App=EntityFramework\"";
            try {
                SaveLoginInfo(connectionString);
                var exist = DataProvider.Instance.DB.Database.Exists();
            } catch (Exception ex) {
                MessageBox.Show("Wrong Database Infomation", ex.Message);
                return;
            }

            MainWindow mainWindow = new MainWindow();
            Application.Current.Windows[0].Close();
            mainWindow.Show();
        }

        private void SaveLoginInfo(String connectionString) {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var entropy = new byte[20];
            using (var rng = new RNGCryptoServiceProvider()) {
                rng.GetBytes(entropy);
            }
            var entropyText = Convert.ToBase64String(entropy);
            var cypherText = Convert.ToBase64String(ProtectedData.Protect(passwordBytes, entropy, DataProtectionScope.CurrentUser));
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings["ManagerMyShopEntities"].ConnectionString = connectionString;
            config.AppSettings.Settings["username"].Value = username;
            config.AppSettings.Settings["database"].Value = Database;
            config.AppSettings.Settings["server"].Value = Server;
            config.AppSettings.Settings["password"].Value = cypherText;
            config.AppSettings.Settings["entropy"].Value = entropyText;
            config.Save(ConfigurationSaveMode.Minimal);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
