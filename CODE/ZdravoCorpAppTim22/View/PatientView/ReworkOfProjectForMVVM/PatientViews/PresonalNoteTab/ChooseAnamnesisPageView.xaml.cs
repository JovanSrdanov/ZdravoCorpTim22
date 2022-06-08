using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.RecordTab;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.PresonalNoteTab
{
    /// <summary>
    /// Interaction logic for ChooseAnamnesisPageView.xaml
    /// </summary>
    public partial class ChooseAnamnesisPageView : Page
    {
        public ChooseAnamnesisPageView()
        {
            InitializeComponent();
            ProfileViewModel profileViewModel = new ProfileViewModel((Patient)AuthenticationController.Instance.GetLoggedUser());


            this.DataContext = new MedicalReportsPageViewModel(profileViewModel.MedicalReportsViewModels);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MedicalReportsViewModel medicalReportsViewModel = (MedicalReportsViewModel)AnamnesisAndComments.SelectedItem;

            CreatePersonalNotePageView createPersonalNotePageView =
                new CreatePersonalNotePageView(medicalReportsViewModel);
            NavigationService.Navigate(createPersonalNotePageView);
        }
    }
}
