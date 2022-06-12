using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.WizardViews
{
    /// <summary>
    /// Interaction logic for W5.xaml
    /// </summary>
    public partial class W5 : Page
    {
        public W5()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new W_LAST());
        }
    }
}
