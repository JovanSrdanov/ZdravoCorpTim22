using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ZdravoCorpAppTim22.View.Manager
{
    public partial class ManagerHome : Window
    {
        internal static MainWindow MainWindow;
        public static NavigationService NavigationService { get; private set; }
        public ManagerHome(MainWindow mainWindow)
        {
            InitializeComponent();
            MainWindow = mainWindow;
            NavigationService = ContentFrame.NavigationService;
        }

        private void Content_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void Window_Closing(object sender, CancelEventArgs e) => MainWindow.Show();

        private void ButtonLogout_Click(object sender, RoutedEventArgs e) => Close();
    }
}
