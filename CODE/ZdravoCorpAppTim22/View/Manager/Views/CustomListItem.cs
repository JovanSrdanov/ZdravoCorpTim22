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
    public class CustomListItem : ListBoxItem
    {
        public static readonly DependencyProperty NavigateUriProperty =
            DependencyProperty.Register("NavigateUri", typeof(Uri), typeof(CustomListItem), new PropertyMetadata(null));

        static CustomListItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomListItem), new FrameworkPropertyMetadata(typeof(CustomListItem)));
        }

        public Uri NavigateUri
        {
            get { return (Uri)GetValue(NavigateUriProperty); }
            set { SetValue(NavigateUriProperty, value); }
        }


    }
}
