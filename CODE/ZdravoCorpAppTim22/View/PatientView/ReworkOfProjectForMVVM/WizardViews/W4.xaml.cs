using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.WizardViews
{
    /// <summary>
    /// Interaction logic for W4.xaml
    /// </summary>
    public partial class W4 : Page
    {
        public W4()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new W5());
        }
    }
}
