using System.Collections.ObjectModel;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.PresonalNoteTab
{
    public class PersonalNotesPageViewModel : ViewModel
    {

        public ObservableCollection<PersonalNotesViewModel> PersonalNotesViewModels { get; set; }


        public PersonalNotesPageViewModel()
        {
            Patient patient = (Patient)AuthenticationController.Instance.GetLoggedUser();
            PersonalNotesViewModels = new ObservableCollection<PersonalNotesViewModel>();
            if (patient.PersonalNotes != null)
                foreach (PersonalNote personalNote in patient.PersonalNotes)
                {
                    PersonalNotesViewModels.Add(new PersonalNotesViewModel(personalNote));
                }


        }
    }
}