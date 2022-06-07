using System.Windows;
using System.Windows.Controls;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.Secretary.SecretaryMVVMfolder.SecretaryRequestForAbsenceMVVMFolder
{
    /// <summary>
    /// Interaction logic for SecretaryAbsenceRequestDialog.xaml
    /// </summary>
    public partial class SecretaryAbsenceRequestDialog : Page
    {
        private RequestForAbsence requestForAbsence;
        private bool shouldApprove;
        public SecretaryAbsenceRequestDialog(RequestForAbsence requestForAbsence, bool shouldApprove)
        {
            InitializeComponent();
            this.requestForAbsence = requestForAbsence;
            this.shouldApprove = shouldApprove;
            SetLabels();
        }

        public void SetLabels()
        {
            lblDoctor.Content = requestForAbsence.Doctor;
            isUrgentLBl.Content = requestForAbsence.Urgent;
            dateStartlbl.Content = requestForAbsence.AbsenceInterval.Start;
            dateEndlbl.Content = requestForAbsence.AbsenceInterval.End;
            textBoxReason_Copy.Text = requestForAbsence.Reason;
            if (shouldApprove)
            {
                textBoxReason.Visibility = Visibility.Hidden;
                lblDenying.Visibility = Visibility.Hidden;
            }
            else
            {
                textBoxReason.Visibility = Visibility.Visible;
                lblDenying.Visibility = Visibility.Visible;
            }
        }
        private void btnYes_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (shouldApprove)
            {
                requestForAbsence.RequestState = RequestState.approved;
                NavigationService.GoBack();
            }
            else if (textBoxReason.Text != "")
            {
                requestForAbsence.RequestState = RequestState.rejected;
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("You must give reasong for denying request");
            }

        }

        private void btnNo_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
