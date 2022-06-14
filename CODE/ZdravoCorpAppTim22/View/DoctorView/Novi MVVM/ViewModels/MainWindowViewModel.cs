using MVVM1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        MainDoctorWindow MainDoctorWindow { get; set; }
        public MainWindow MainWindow { get; set; }
        public NavigationService NavService { get; set; }
        public MyICommand NavigateToMainPageCommand { get; set; }

        public MyICommand NavigateToLogInScreenCommand { get; set; }

        //Konstruktor
        public MainWindowViewModel(NavigationService navService, MainDoctorWindow mainDoctorWindow, MainWindow mainWindow)
        {
            MainDoctorWindow = mainDoctorWindow;
            MainWindow = mainWindow;
            this.NavService = navService;
            this.NavigateToMainPageCommand = new MyICommand(ExecuteNavigateHome);
            this.NavigateToLogInScreenCommand = new MyICommand(ExecuteNavigateLogInScreen);
        }

        //Komande
        private void ExecuteNavigateHome()
        {
            this.NavService.Navigate(
                new Uri("/View/DoctorView/Novi MVVM/Views/HomeScreen.xaml", UriKind.Relative));
        }

        private void ExecuteNavigateLogInScreen()
        {
            MainWindow.Show();
            MainDoctorWindow.Close();
        }
    }
}
