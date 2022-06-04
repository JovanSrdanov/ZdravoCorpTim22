using MVVM1;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.HospitalInformationTab
{
    public class GeneralInformationViewModel : BindableBase
    {

        public MyICommand ReviewHospitalCommand { get; set; }

        public GeneralInformationViewModel()
        {
            ReviewHospitalCommand = new MyICommand(ReviewHospital);

        }

        public void ReviewHospital()
        {
           
        }

    }
}