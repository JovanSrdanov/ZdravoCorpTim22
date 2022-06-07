using Controller;
using System.Windows.Controls;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;

namespace ZdravoCorpAppTim22.View.Secretary.SecretaryMVVMfolder.SecretaryRequestForAbsenceMVVMFolder
{
    /// <summary>
    /// Interaction logic for SecretaryRequestForAbsencePage.xaml
    /// </summary>
    public partial class SecretaryRequestForAbsencePage : Page
    {
        public SecretaryRequestForAbsencePage()
        {
            InitializeComponent();
            bool shouldMakeRequest = false;
            if (RequestForAbsenceController.Instance.GetAll().Count == 0)
            {
                shouldMakeRequest = true;
            }
            else
            {
                shouldMakeRequest = true;
                for (int i = 0; i < RequestForAbsenceController.Instance.GetAll().Count; i++)
                {
                    if (RequestForAbsenceController.Instance.GetAll()[i].RequestState == RequestState.pending)
                    {
                        shouldMakeRequest = false;
                        break;
                    }
                }

            }

            if (shouldMakeRequest)
            {
                Interval interval = new Interval();
                interval.Start = System.DateTime.Now.AddDays(2);
                interval.End = System.DateTime.Now.AddDays(4);
                RequestForAbsence requestForAbsence = new RequestForAbsence("Moze mi se", true, interval, DoctorController.Instance.GetAll()[0]);
                RequestForAbsenceController.Instance.Create(requestForAbsence);
            }

            RequestForAbsencePageViewModel requestForAbsencePageView = new RequestForAbsencePageViewModel(RequestForAbsenceController.Instance.GetAll());
            this.DataContext = requestForAbsencePageView;
        }

        private void btnApprove_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SecretaryAbsenceRequestDialog secretaryAbsenceRequestDialog = new SecretaryAbsenceRequestDialog((RequestForAbsence)DataGridRequestForAbsence.SelectedItem, true);

            NavigationService.Navigate(secretaryAbsenceRequestDialog);
        }

        private void btnDeny_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SecretaryAbsenceRequestDialog secretaryAbsenceRequestDialog = new SecretaryAbsenceRequestDialog((RequestForAbsence)DataGridRequestForAbsence.SelectedItem, false);

            NavigationService.Navigate(secretaryAbsenceRequestDialog);
        }
    }
}
