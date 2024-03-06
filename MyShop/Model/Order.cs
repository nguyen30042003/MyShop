using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    class Order
    {
        private int Id {  get; set; }
        private DateTime CreateDate { get; set; }
        private float TotalPrice { get; set; }
        private int TotalQuantity { get; set; }
        private int IdCustomer {  get; set; }
        private List<Item> Items { get; set; }

    }
}
