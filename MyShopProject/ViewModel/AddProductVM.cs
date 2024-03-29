using MyShopProject.Model;
using MyShopProject.ServiceImpl;
using System;
using System.Collections.Generic;
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

        public delegate void Hanlder();
        public event Hanlder Click_Hanlder;
        public String[] Categories { get; set; }
        public int[] IDCategories { get; set; }
        public int cbCategories { get; set; }
        public AddProductVM()
        {
            product = new Product();
            DateTime createDate = DateTime.Now;
            loadCategory();
            Action = "Add product";
            Action_Click = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                product.ID = 0;
                product.IDCategory = IDCategories[cbCategories];
                product.CreateDate = createDate; 
                if (ProductServiceImpl.Instance.save(product))
                {
                    System.Windows.MessageBox.Show("Add Product Success");
                }
                else
                {
                    System.Windows.MessageBox.Show("Add Product unsuccess");
                }
                Click_Hanlder.Invoke();
            });

            BrowseImage_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    product.Image = openFileDialog.FileName;
                    OnPropertyChanged("product");
                }
            });
        }
        private void loadCategory()
        {
            var list = new List<Category>();
            list = CategoryServiceImpl.Instance.findAll();
            Categories = list.Select(c => c.Name).ToArray();
            IDCategories = list.Select(c => c.ID).ToArray();
        }
    }
}
