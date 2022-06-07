using MVVM1;
using System.Collections.ObjectModel;
using ZdravoCorpAppTim22.Controller;
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
        public MyICommand EditStuffMeeting { get; set; }
        public MyICommand DeleteStuffMeeting { get; set; }
        private bool isStuffMeetingSelected()
        {
            return selectedStuffMeeting != null;
        }

        public SecretaryStuffMeetingOverviewPageViewModel()
        {
            StuffMeetings = new ObservableCollection<StuffMeeting>();
            if (StuffMeetingController.Instance.GetAll().Count > 0)
            {
                for (int i = 0; i < StuffMeetingController.Instance.GetAll().Count; i++)
                {

                    StuffMeetings.Add(StuffMeetingController.Instance.GetAll()[i]);
                }
            }
            EditStuffMeeting = new MyICommand(null, isStuffMeetingSelected);

            DeleteStuffMeeting = new MyICommand(null, isStuffMeetingSelected);


        }
    }
}
