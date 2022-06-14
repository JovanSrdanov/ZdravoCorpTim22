using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.View.DoctorView.Navigation;
using ZdravoCorpAppTim22.View.DoctorView.Projekat_u_MVVM_u.ViewModels;
using ZdravoCorpAppTim22.View.DoctorView.Services;

namespace ZdravoCorpAppTim22.View.DoctorView.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly NavService _navigationService ;

        public NavigateCommand(NavService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}
