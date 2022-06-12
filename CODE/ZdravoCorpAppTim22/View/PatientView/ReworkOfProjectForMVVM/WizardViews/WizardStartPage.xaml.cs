using System.Windows.Controls;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.WizardViews
{
    /// <summary>
    /// Interaction logic for WizardStartPage.xaml
    /// </summary>
    public partial class WizardStartPage : Page
    {
        public WizardStartPage()
        {
            InitializeComponent();
        }

        private void Next_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new W1());
        }
    }
}
