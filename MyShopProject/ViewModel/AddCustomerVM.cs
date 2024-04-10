using MyShopProject.Model;
using MyShopProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace MyShopProject.ViewModel
{
    public class AddCustomerVM : BaseViewModel
    {
        public delegate void Handler();
        public event Handler Click_Handler;
        public Customer newCustomer {  get; set; }
        public ICommand AddCustomer_Click {  get; set; }
        public ICommand PathImage_Click { get; set; }
        public bool isGender { get; set; } = true;
        public AddCustomerVM() {
            newCustomer = new Customer();
            PathImage_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png|All files (*.*)|*.*";
                openFileDialog.ShowDialog();
                newCustomer.Avatar = openFileDialog.FileName;
            });
            AddCustomer_Click = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if(isGender == true)
                {
                    newCustomer.Gender = "Male";
                }
                else
                {
                    newCustomer.Gender = "Female";
                }
                Click_Handler.Invoke();
            });
        }
    }
}
