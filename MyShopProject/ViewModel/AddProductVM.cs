using MyShopProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyShopProject.ViewModel {
    internal class AddProductVM : BaseViewModel {
        public String Action {  get; set; }
        public Product product {  get; set; }
        public ICommand Action_Click { get; set; }

        public delegate void Hanlder();
        public event Hanlder Click_Hanlder;
        public AddProductVM() {
            Action = "Add product";
            Action_Click = new RelayCommand<object>((p) => { return true; }, (p) => {
                Click_Hanlder.Invoke();
            });
        }
    }
}
