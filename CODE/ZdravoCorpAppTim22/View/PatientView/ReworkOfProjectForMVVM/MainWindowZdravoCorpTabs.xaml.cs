using Model;
using System.ComponentModel;
using System.Windows;
using ZdravoCorpAppTim22.Controller;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM
{
    /// <summary>
    /// Interaction logic for MainWindowZdravoCorpTabs.xaml
    /// </summary>
    public partial class MainWindowZdravoCorpTabs : Window
    {
        public MainWindowZdravoCorpTabs(Patient patient)
        {
            InitializeComponent();
        }

        private void MainWindowZdravoCorpTabs_OnClosing(object sender, CancelEventArgs e)
        {

            AuthenticationController.Instance.Logout();
            Application.Current.MainWindow.Show();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
