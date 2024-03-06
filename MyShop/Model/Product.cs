using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    class Product
    {
        private int Id { get; set; }
        private string Name { get; set; }
        private string Description { get; set; }
        private float PriceSale { get; set; }
        private float PriceImport { get; set; }
        private DateTime CreateDate { get; set; }
        private string Image { get; set; }
        private int IdCategory { get; set; }
    }
}
