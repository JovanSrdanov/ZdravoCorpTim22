using System.Windows;
using System.Windows.Controls;

namespace ZdravoCorpAppTim22.View.Manager
{
    public partial class ManagerHome : Window
    {
        internal static MainWindow MainWindow;
        public ManagerHome(MainWindow mainWindow)
        {
            InitializeComponent();
            MainWindow = mainWindow;
        }

        private void Content_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow.Show();
        }
    }
}
