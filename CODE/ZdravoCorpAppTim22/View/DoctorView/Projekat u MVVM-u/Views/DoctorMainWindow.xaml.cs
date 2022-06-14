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
using System.Windows.Shapes;
using ZdravoCorpAppTim22.View.DoctorView.Navigation;
using ZdravoCorpAppTim22.View.DoctorView.Projekat_u_MVVM_u.ViewModels;
using ZdravoCorpAppTim22.View.DoctorView.Services;

namespace ZdravoCorpAppTim22.View.DoctorView.Projekat_u_MVVM_u.Views
{
    /// <summary>
    /// Interaction logic for DoctorMainWindow.xaml
    /// </summary>
    public partial class DoctorMainWindow : Window
    {
        //private readonly NavigationBase _navigationBase;
        public static NavigationBase _navigationBase;
        public DoctorMainWindow()
        {
            InitializeComponent();
            _navigationBase = new NavigationBase();
            _navigationBase.CurrentViewModel = CreateHomeViewModel();
            DataContext = new DoctorMainWindowViewModel(_navigationBase);
        }

        private DoctorAppointmentsViewModel CreateDoctorAppoinementsViewModel()
        {
            return new DoctorAppointmentsViewModel(new NavService(_navigationBase, CreateHomeViewModel));
        }

        private HomeViewModel CreateHomeViewModel()
        {
            return new HomeViewModel(new NavService(_navigationBase, CreateDoctorAppoinementsViewModel));
        }
    }
}
