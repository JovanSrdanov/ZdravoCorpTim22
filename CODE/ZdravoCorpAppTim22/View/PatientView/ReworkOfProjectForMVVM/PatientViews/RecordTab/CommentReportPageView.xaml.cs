using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.RecordTab
{
   
    public partial class CommentReportPageView : Page
    {
        public CommentReportPageView(int id, string reportComment)
        {
            InitializeComponent();

            CommentReportPageViewModel commentReportPageViewModel = new CommentReportPageViewModel(id,reportComment);
            this.DataContext = commentReportPageViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProfileView profileView = new ProfileView();
            this.NavigationService.Navigate(profileView);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            SuccessReportCommented successReportCommented = new SuccessReportCommented();
            this.NavigationService.Navigate(successReportCommented);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("osk.exe");
        }

        private void CommentTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("osk.exe");
        }
    }
}
