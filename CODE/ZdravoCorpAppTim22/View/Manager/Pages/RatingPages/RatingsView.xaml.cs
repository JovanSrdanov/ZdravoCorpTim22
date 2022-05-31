using System.Windows.Controls;
using ZdravoCorpAppTim22.View.Manager.Views;

namespace ZdravoCorpAppTim22.View.Manager.Pages.RatingPages
{
    public partial class RatingsView : Page
    {
        public RatingsView()
        {
            InitializeComponent();
            int[] grades = { 1, 2, 1, 2, 3, 2, 5, 5, 4, 3, 4, 2, 5, 1, 2, 3, 2, 5, 5, 5, 5 };
            StaffHospitality.Content = new GradesBlock("Staff", grades);
            Accessibility.Content = new GradesBlock("Accessibility", grades);
            Hygiene.Content = new GradesBlock("Hygiene", grades);
            Appearance.Content = new GradesBlock("Appearance", grades);
            Application.Content = new GradesBlock("Application", grades);
        }
    }
}
