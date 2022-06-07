using System.Collections.ObjectModel;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.Secretary.SecretaryMVVMfolder.SecretaryRequestForAbsenceMVVMFolder
{
    class SecretaryStuffMeetingOverviewPageViewModel : ViewModel
    {
        public ObservableCollection<StuffMeeting> StuffMeetings { get; set; }

        private StuffMeeting selectedStuffMeeting;
        public StuffMeeting SelectedStuffMeeting
        {
            get { return selectedStuffMeeting; }
            set
            {
                selectedStuffMeeting = value;
                //ApproveRequestForAbsenceCommand.RaiseCanExecuteChanged();
                //DenyRequestForAbsenceCommand.RaiseCanExecuteChanged();
            }
        }

        private bool isStuffMeetingSelected()
        {
            return selectedStuffMeeting != null;
        }

        public SecretaryStuffMeetingOverviewPageViewModel()
        {
            StuffMeetings = new ObservableCollection<StuffMeeting>();


        }
    }
}
