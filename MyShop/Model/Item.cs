using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    class Item
    {
        private int Id {  get; set; }
        private int IdOrder { get; set; }
        private int IdProduct { get; set; }
        private float TotalPrice { get; set; }
        private int TotalQuantity { get; set; }
    }
}
