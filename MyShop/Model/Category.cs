﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Category(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
    }
}
