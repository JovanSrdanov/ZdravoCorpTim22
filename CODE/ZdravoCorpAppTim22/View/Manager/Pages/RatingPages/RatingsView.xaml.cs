using System.Windows.Controls;
using ZdravoCorpAppTim22.View.Manager.ViewModels.RatingsViewModels;
using ZdravoCorpAppTim22.View.Manager.Views;

namespace ZdravoCorpAppTim22.View.Manager.Pages.RatingPages
{
    public partial class RatingsView : Page
    {
        RatingsViewModel ViewModel { get; set; }
        public RatingsView()
        {
            InitializeComponent();
            ViewModel = new RatingsViewModel();
            //int[] grades = { 1, 2, 1, 2, 3, 2, 5, 5, 4, 3, 4, 2, 5, 1, 2, 3, 2, 5, 5, 5, 5 };
            StaffHospitality.Content = new GradesBlock((string)FindResource("Manager_Hospitality"), ViewModel.StaffGrades);
            Accessibility.Content = new GradesBlock((string)FindResource("Manager_Accessibility"), ViewModel.AccessibilityGrades);
            Hygiene.Content = new GradesBlock((string)FindResource("Manager_Hygiene"), ViewModel.HygieneGrades);
            Appearance.Content = new GradesBlock((string)FindResource("Manager_Appearance"), ViewModel.AppearanceGrades);
            Application.Content = new GradesBlock((string)FindResource("Manager_Appearance"), ViewModel.ApplicationGrades);
        }
    }
}
