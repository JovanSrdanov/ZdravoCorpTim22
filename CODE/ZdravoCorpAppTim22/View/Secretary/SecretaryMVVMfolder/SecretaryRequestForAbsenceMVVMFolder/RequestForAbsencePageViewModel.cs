using MVVM1;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZdravoCorpAppTim22.Model;
namespace ZdravoCorpAppTim22.View.Secretary.SecretaryMVVMfolder.SecretaryRequestForAbsenceMVVMFolder
{
    class RequestForAbsencePageViewModel : ViewModel
    {
        public ObservableCollection<RequestForAbsence> RequestForAbsenceModelViews { get; set; }

        private RequestForAbsence selectedRequestForAbsenceModelView;
        public RequestForAbsence SelectedMedicalReportViewModel
        {
            get { return selectedRequestForAbsenceModelView; }
            set
            {
                selectedRequestForAbsenceModelView = value;
                ApproveRequestForAbsenceCommand.RaiseCanExecuteChanged();
                DenyRequestForAbsenceCommand.RaiseCanExecuteChanged();
            }
        }

        public MyICommand ApproveRequestForAbsenceCommand { get; set; }
        public MyICommand DenyRequestForAbsenceCommand { get; set; }

        private bool isRequestForAbsenceSelected()
        {
            return SelectedMedicalReportViewModel != null;
        }
        public RequestForAbsencePageViewModel(List<RequestForAbsence> requestForAbsenceModelViews)
        {
            ApproveRequestForAbsenceCommand = new MyICommand(ApproveSelectedRequestForAbsence, isRequestForAbsenceSelected);

            DenyRequestForAbsenceCommand = new MyICommand(DenySelectedRequestForAbsence, isRequestForAbsenceSelected);

            for (int i = 0; i < requestForAbsenceModelViews.Count; i++)
            {
                RequestForAbsence requestForAbsenceModelViewTemp = requestForAbsenceModelViews[i];
                if (requestForAbsenceModelViewTemp.RequestState == RequestState.pending)
                {
                    if (RequestForAbsenceModelViews == null)
                    {
                        RequestForAbsenceModelViews = new ObservableCollection<RequestForAbsence>();
                    }

                    RequestForAbsenceModelViews.Add(requestForAbsenceModelViewTemp);
                }
            }

        }
        public void ApproveSelectedRequestForAbsence()
        {

        }

        public void DenySelectedRequestForAbsence()
        {
        }
    }
}
