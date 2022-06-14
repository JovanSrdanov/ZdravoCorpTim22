using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.View.DoctorView.Navigation;

namespace ZdravoCorpAppTim22.View.DoctorView.Projekat_u_MVVM_u.ViewModels
{
    public class DoctorMainWindowViewModel : ViewModel
    {
        private readonly NavigationBase _navigationBase;
        public ViewModel CurrentViewModel => _navigationBase.CurrentViewModel;

        public DoctorMainWindowViewModel(NavigationBase navigationBase)
        {
            _navigationBase = navigationBase;
            _navigationBase.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
