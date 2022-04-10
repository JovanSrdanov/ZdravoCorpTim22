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

namespace ZdravoCorpAppTim22.View.Manager.Views
{
    public partial class NavMenu : UserControl
    {
        public static readonly DependencyProperty NavigationServiceProperty =
            DependencyProperty.Register("NavigationServiceProp", typeof(NavigationService), typeof(NavMenu), new FrameworkPropertyMetadata(null));

        public NavigationService NavigationServiceProp
        {
            get => (NavigationService)GetValue(NavigationServiceProperty);
            set => SetValue(NavigationServiceProperty, value);
        }

        public NavMenu()
        {
            InitializeComponent();
        }
        private void NavigationMenuListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                Uri uri = ((sender as ListBox).SelectedItem as CustomListItem).NavigateUri;
                NavigationServiceProp.Navigate(uri);
            }
        }
    }
}
