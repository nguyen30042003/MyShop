using MyShopProject.Model;
using MyShopProject.ServiceImpl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;


namespace MyShopProject.ViewModel {
    internal class AddProductVM : BaseViewModel
    {
        public String Action { get; set; }
        public Product product { get; set; }
        public ICommand Action_Click { get; set; }
        public ICommand BrowseImage_Click { get; set; }

        public delegate void Handler();
        public event Handler Click_Handler;

        public List<Category> Categories { get; set; }
        public AddProductVM()
        {
            product = new Product() { CreateDate = DateTime.Now };
            loadCategory();
            Action = "Add product";
            Action_Click = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Click_Handler.Invoke();
            });

            BrowseImage_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    product.Image = openFileDialog.FileName;
                }
            });
        }
        private void loadCategory()
        {
            Categories = CategoryServiceImpl.Instance.findAll();
        }
    }
}
