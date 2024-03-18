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
    /// Interaction logic for CustomButton.xaml
    /// </summary>
    public partial class CustomButton : UserControl
    {
        public event RoutedEventHandler Click;
        public CustomButton()
        {
            InitializeComponent();

        }
        private void CustomButton_Loaded(object sender, RoutedEventArgs e)
        {
            myButton.Click += OnButtonClick;
        }
        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            Click?.Invoke(this, e);
        }
        public String nameButton
        {
            get { return (String)GetValue(nameButtonProperty); }
            set { SetValue(nameButtonProperty, value); }
        }
        public static readonly DependencyProperty nameButtonProperty =
            DependencyProperty.Register("nameButton", typeof(String), typeof(CustomButton));
    }
}
