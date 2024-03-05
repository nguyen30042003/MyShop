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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyShop.UserControls
{
    /// <summary>
    /// Interaction logic for ProductCard.xaml
    /// </summary>
    public partial class ProductCard : UserControl
    {
        public ProductCard()
        {
            InitializeComponent();
        }

        public String nameProduct
        {
            get { return (String)GetValue(nameProductProperty); }
            set { SetValue(nameProductProperty, value); }
        }
        public static readonly DependencyProperty nameProductProperty =
            DependencyProperty.Register("nameProduct", typeof(String), typeof(ProductCard));

        public String priceProduct
        {
            get { return (String)GetValue(priceProductProperty); }
            set { SetValue(priceProductProperty, value); }
        }
        public static readonly DependencyProperty priceProductProperty =
            DependencyProperty.Register("priceProduct", typeof(String), typeof(ProductCard));

        public String imageProduct
        {
            get { return (String)GetValue(imageProductProperty); }
            set { SetValue(imageProductProperty, value); }
        }
        public static readonly DependencyProperty imageProductProperty =
            DependencyProperty.Register("imageProduct", typeof(String), typeof(ProductCard));
    }
}
