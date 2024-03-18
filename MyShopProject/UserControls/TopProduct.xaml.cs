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

namespace MyShopProject.UserControls
{
    /// <summary>
    /// Interaction logic for TopProduct.xaml
    /// </summary>
    public partial class TopProduct : UserControl
    {
        public TopProduct()
        {
            InitializeComponent();
        }

        public String ProductName
        {
            get { return (String)GetValue(ProductNameProperty); }
            set { SetValue(ProductNameProperty, value); }
        }
        public static readonly DependencyProperty ProductNameProperty =
            DependencyProperty.Register("ProductName", typeof(String), typeof(TopProduct));

        public String Price
        {
            get { return (String)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }
        public static readonly DependencyProperty PriceProperty =
            DependencyProperty.Register("Price", typeof(String), typeof(TopProduct));

        public String IsLevelUp
        {
            get { return (String)GetValue(IsLevelUpProperty); }
            set { SetValue(IsLevelUpProperty, value); }
        }
        public static readonly DependencyProperty IsLevelUpProperty =
            DependencyProperty.Register("IsLevelUp", typeof(String), typeof(TopProduct));

        public ImageSource Flag
        {
            get { return (ImageSource)GetValue(FlagProperty); }
            set { SetValue(FlagProperty, value); }
        }
        public static readonly DependencyProperty FlagProperty =
            DependencyProperty.Register("Flag", typeof(ImageSource), typeof(TopProduct));
    }
}

