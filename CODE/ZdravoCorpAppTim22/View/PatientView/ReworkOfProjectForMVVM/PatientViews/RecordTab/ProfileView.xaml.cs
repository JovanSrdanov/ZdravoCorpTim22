using Model;
using System.Windows.Controls;
using ZdravoCorpAppTim22.Controller;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.RecordTab
{

    public partial class ProfileView : Page
    {
        public ProfileViewModel profileViewModel { get; set; }

        public ProfileView()
        {
            InitializeComponent();
            profileViewModel = new ProfileViewModel((Patient)AuthenticationController.Instance.GetLoggedUser());
            this.DataContext = profileViewModel;
        }

        private void MedicalReportsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MedicalReportsView medicalReportsView = new MedicalReportsView(profileViewModel.MedicalReportsViewModels);
            this.NavigationService.Navigate(medicalReportsView);

        }
    }
}
