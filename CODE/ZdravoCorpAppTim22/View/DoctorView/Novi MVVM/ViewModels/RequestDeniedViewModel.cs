using MVVM1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.ViewModels
{
    public class RequestDeniedViewModel : ViewModel
    {
        private NavigationService navigationService;
        public string RequestDeniedReason { get; set; }

        //KOMANDE
        public RequestDeniedViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
            RequestDeniedReason = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                "Suspendisse accumsan nunc non enim aliquet, in aliquam lacus dictum. " +
                "Aliquam felis ipsum, sodales quis pretium in, porttitor tempus mi. " +
                "Donec tristique fringilla arcu semper tristique. Mauris a sapien eleifend, " +
                "maximus lectus eu, eleifend dolor. Quisque a enim sit amet eros imperdiet " +
                "euismod eu euismod dolor. In nec quam congue, efficitur est eget, molestie mauris. " +
                "Maecenas sed ipsum sit amet augue posuere aliquam. Nulla sit amet bibendum neque, ut " +
                "vehicula lorem. Donec consectetur nisl ut mollis ullamcorper. Praesent odio erat, " +
                "pulvinar sit amet placerat et, ullamcorper quis mauris. Phasellus egestas, " +
                "augue efficitur malesuada porta, erat lacus pharetra ante, sed volutpat ante ex sed felis. " +
                "Vestibulum sit amet nulla eu nunc convallis dapibus.";

            BackCommand = new MyICommand(ExecuteBack);
        }

        public MyICommand BackCommand { get; set; }

        private void ExecuteBack()
        {
            this.navigationService.GoBack();
        }
    }
}
