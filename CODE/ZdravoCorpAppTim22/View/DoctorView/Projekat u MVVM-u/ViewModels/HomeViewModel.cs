using MVVM1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZdravoCorpAppTim22.View.DoctorView.Commands;
using ZdravoCorpAppTim22.View.DoctorView.Navigation;
using ZdravoCorpAppTim22.View.DoctorView.Services;

namespace ZdravoCorpAppTim22.View.DoctorView.Projekat_u_MVVM_u.ViewModels
{
    public class HomeViewModel : ViewModel
    {
        public ICommand ScheduleCommand { get; }
        public ICommand DrugRequestsCommand { get; }
        public ICommand ViewMedRecCommand { get; }
        public ICommand RequestAbsenceCommand { get; }
        public ICommand LogOutCommand { get; }
        public ICommand HomeCommand { get; }

        public HomeViewModel(NavService homeViewNavigationService)
        {
            ScheduleCommand = new NavigateCommand(homeViewNavigationService);
            DrugRequestsCommand = new NavigateCommand(homeViewNavigationService);
            ViewMedRecCommand = new NavigateCommand(homeViewNavigationService);
            RequestAbsenceCommand = new NavigateCommand(homeViewNavigationService);
            LogOutCommand = new NavigateCommand(homeViewNavigationService);
            HomeCommand = new NavigateCommand(homeViewNavigationService);
        }
    }
}
