using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyShopProject.ViewModel
{
    public class AnalystBudgetVM : BaseViewModel
    {
        public ICommand revenew_Click {  get; set; }
        public ICommand profit_Click { get; set; }
        public ICommand topProduct_Click { get; set; }
        public ICommand quantityProduct_Click { get; set; } 


    }
}
