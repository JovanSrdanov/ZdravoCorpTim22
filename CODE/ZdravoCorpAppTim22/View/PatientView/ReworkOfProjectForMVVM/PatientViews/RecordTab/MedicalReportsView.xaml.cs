using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.RecordTab
{

    public partial class MedicalReportsView : Page
    {
        public MedicalReportsView(ObservableCollection<MedicalReportsViewModel> medicalReportsViewModels)
        {
            InitializeComponent();
            MedicalReportsPageViewModel medicalReportsPageViewModel =
                new MedicalReportsPageViewModel(medicalReportsViewModels);

            this.DataContext = medicalReportsPageViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MedicalReportsViewModel m = (MedicalReportsViewModel)ReportsAll.SelectedItem;
            ReportReviewPageView reportReviewPageView = new ReportReviewPageView(m.Id);
            this.NavigationService.Navigate(reportReviewPageView);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MedicalReportsViewModel m = (MedicalReportsViewModel)ReportsAll.SelectedItem;
            CommentReportPageView commentReportPageView = new CommentReportPageView(m.Id,m.ReportComment);
            this.NavigationService.Navigate(commentReportPageView);
        }
    }
}
