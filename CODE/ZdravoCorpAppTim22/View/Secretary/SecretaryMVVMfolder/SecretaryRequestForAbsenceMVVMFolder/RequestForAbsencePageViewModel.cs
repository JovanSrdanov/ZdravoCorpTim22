using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.Secretary.SecretaryMVVMfolder.SecretaryRequestForAbsenceMVVMFolder
{
    class RequestForAbsencePageViewModel : ViewModel
    {
        public ObservableCollection<RequestForAbsenceModelView> RequestForAbsenceModelViews { get; set; }

        private RequestForAbsenceModelView selectedRequestForAbsenceModelView;
        public RequestForAbsenceModelView SelectedMedicalReportViewModel
        {
            get { return selectedRequestForAbsenceModelView; }
            set
            {
                selectedRequestForAbsenceModelView = value;
            }
        }

        public RequestForAbsencePageViewModel(List<RequestForAbsence> requestForAbsenceModelViews)
        {
            for (int i = 0; i < requestForAbsenceModelViews.Count; i++)
            {
                RequestForAbsenceModelView requestForAbsenceModelViewTemp = new RequestForAbsenceModelView(requestForAbsenceModelViews[i]);
                if (RequestForAbsenceModelViews == null)
                {
                    RequestForAbsenceModelViews = new ObservableCollection<RequestForAbsenceModelView>();
                }
                RequestForAbsenceModelViews.Add(requestForAbsenceModelViewTemp);
            }

        }
    }
}
