using System;
using System.Windows;
using System.Windows.Controls;

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
