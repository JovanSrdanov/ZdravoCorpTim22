using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.RecordTab
{
    /// <summary>
    /// Interaction logic for SuccesReportReviewPage.xaml
    /// </summary>
    public partial class SuccesReportReviewPage : Page
    {
        public SuccesReportReviewPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProfileView profileView = new ProfileView();
            this.NavigationService.Navigate(profileView);
        }
    }
}
