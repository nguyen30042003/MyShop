//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyShopProject.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item
    {
        public int ID { get; set; }
        public Nullable<int> IDProduct { get; set; }
        public Nullable<int> IDOrder { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> Quantity { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
