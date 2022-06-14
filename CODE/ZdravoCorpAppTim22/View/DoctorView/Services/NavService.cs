using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.View.DoctorView.Navigation;

namespace ZdravoCorpAppTim22.View.DoctorView.Services
{
    public class NavService
    {
        private readonly NavigationBase _navigationBase;
        private readonly Func<ViewModel> _createViewModel;

        public NavService(NavigationBase navigationBase, Func<ViewModel> createViewModel)
        {
            _navigationBase = navigationBase;
            this._createViewModel = createViewModel;
        }
        public void Navigate()
        {
            _navigationBase.CurrentViewModel = _createViewModel();
        }
    }
}
