using Model;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.View.Manager.Views;
using System;

namespace ZdravoCorpAppTim22.View.Manager
{
    public partial class ManagerHome : Window
    {
        public static ManagerHome Instance { get; private set; }
        public static NavigationService NavigationService { get; private set; }
        public ManagerHome(ManagerClass manager)
        {
            
            InitializeComponent();
            NavigationService = ContentFrame.NavigationService;
            Instance = this;
            
            
        }

        public static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void Content_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            AuthenticationController.Instance.Logout();
            Application.Current.MainWindow.Show();
        }
        private void ButtonLogout_Click(object sender, RoutedEventArgs e) => Close();

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            var window = (Window)((FrameworkElement)sender).TemplatedParent;
            window.WindowState = WindowState.Minimized;
        }

        private void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            var window = (Window)((FrameworkElement)sender).TemplatedParent;
            if (window.WindowState == WindowState.Normal)
            {
                window.WindowState = WindowState.Maximized;
            }
            else
            {
                window.WindowState = WindowState.Normal;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            var window = (Window)((FrameworkElement)sender).TemplatedParent;
            window.Close();
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            InfoModal.Show("Work in progress!");
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LanguageComboBox.SelectedIndex == 1)
            {
                App.Current.Resources.MergedDictionaries[0] = new ResourceDictionary() { Source = new Uri("View/Manager/Language/srb.xaml", UriKind.Relative) };
            }
            else
            {
                App.Current.Resources.MergedDictionaries[0] = new ResourceDictionary() { Source = new Uri("View/Manager/Language/eng.xaml", UriKind.Relative) };
            }
        }

        private void ThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ThemeComboBox.SelectedIndex == 1)
            {
                App.Current.Resources.MergedDictionaries[1] = new ResourceDictionary() { Source = new Uri("View/Manager/Styles/ManagerLight.xaml", UriKind.Relative) };
            }
            else
            {
                App.Current.Resources.MergedDictionaries[1] = new ResourceDictionary() { Source = new Uri("View/Manager/Styles/ManagerDark.xaml", UriKind.Relative) };
            }
        }
    }
}
