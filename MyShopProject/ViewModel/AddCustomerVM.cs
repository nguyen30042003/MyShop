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
        public string tbFullName {  get; set; }
        public string tbEmail { get; set; }
        public DateTime tbDOB { get; set; }
        public string tbGender { get; set; }
        public string tbPhone { get; set; }

        public string _pathImage;
        public string PathImage
        {
            get { return _pathImage; }
            set
            {
                if (_pathImage != value)
                {
                    _pathImage = value;
                    OnPropertyChanged("PathImage");
                }
            }
        }
        public ICommand AddCustomer_Click {  get; set; }
        public ICommand PathImage_Click { get; set; }

        public AddCustomerVM() {
            PathImage_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png|All files (*.*)|*.*";
                openFileDialog.ShowDialog();
                PathImage = openFileDialog.FileName;
            });
            AddCustomer_Click = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Customer customer = new Customer() { ID = 0, Full_Name = tbFullName, DOB = tbDOB, Email = tbEmail, Gender = "Male", Avatar = PathImage, Phone = tbPhone };
                ICustomerRepository customerRepository = new ICustomerRepository();
                MessageBox.Show(tbFullName);
                customerRepository.create(customer);
                MessageBox.Show("Add Success");
            });
        }
    }
}
