using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

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
