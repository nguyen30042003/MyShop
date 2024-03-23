using MahApps.Metro.IconPacks;
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
    /// Interaction logic for InfoCard.xaml
    /// </summary>
    public partial class InfoCard : UserControl
    {
        public InfoCard()
        {
            InitializeComponent();
        }
        public String Title
        {
            get { return (String)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(String), typeof(InfoCard));


        public String NameTitle
        {
            get { return (String)GetValue(NameTitleProperty); }
            set { SetValue(NameTitleProperty, value); }
        }
        public static readonly DependencyProperty NameTitleProperty =
            DependencyProperty.Register("NameTitle", typeof(String), typeof(InfoCard));

        public String Number
        {
            get { return (String)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }
        public static readonly DependencyProperty NumberProperty =
            DependencyProperty.Register("Number", typeof(String), typeof(InfoCard));

        public String IsActive
        {
            get { return (String)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(String), typeof(InfoCard));

        public String IsTechnical
        {
            get { return (String)GetValue(IsTechnicalProperty); }
            set { SetValue(IsTechnicalProperty, value); }
        }
        public static readonly DependencyProperty IsTechnicalProperty =
            DependencyProperty.Register("IsTechnical", typeof(String), typeof(InfoCard));


        public PackIconMaterialKind Icon
        {
            get { return (PackIconMaterialKind)GetValue(InconProperty); }
            set { SetValue(InconProperty, value); }
        }
        public static readonly DependencyProperty InconProperty =
            DependencyProperty.Register("Icon", typeof(PackIconMaterialKind), typeof(InfoCard));
    }
}
