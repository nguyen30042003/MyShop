﻿using MyShopProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyShopProject.Pages {
    public partial class ProductInfo : UserControl {
        public ProductInfo(Model.Product product) {
            InitializeComponent();
            DataContext = new ProductInfoVM(product);
        }
    }
}
