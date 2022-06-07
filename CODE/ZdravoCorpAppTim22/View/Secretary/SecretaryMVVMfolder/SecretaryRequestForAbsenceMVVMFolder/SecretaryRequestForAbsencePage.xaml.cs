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
            if (RequestForAbsenceController.Instance.GetAll().Count == 0)
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
    }
}
