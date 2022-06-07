using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public partial class RequestListView : Window
    {
        private ObservableCollection<RequestForAbsence> allRequests;
        public RequestListView()
        {
            InitializeComponent();
            List<RequestForAbsence> requests = RequestForAbsenceController.Instance.GetAll();
            allRequests = new ObservableCollection<RequestForAbsence>(requests);
            RequestListDataGrid.ItemsSource = allRequests;
        }
        private void DetailsBtnView(object sender, RoutedEventArgs e)
        {
            RequestDeniedView requestDeniedView = new RequestDeniedView();
            requestDeniedView.Owner = this;
            requestDeniedView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            requestDeniedView.Show();
            this.Hide();
        }

        private void RequestDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RequestForAbsence selectedRequest = RequestListDataGrid.SelectedItem as RequestForAbsence;
            if (selectedRequest == null || !(RequestForAbsenceController.Instance.isRequestDenied(selectedRequest)))
            {
                DetailsBtn.IsEnabled = false;
            }
            else DetailsBtn.IsEnabled = true;
        }

        private void BackBtnClick(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void LogOutBtn(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Show();
            foreach (Window item in App.Current.Windows)
            {
                if (item != Application.Current.MainWindow)
                {
                    item.Close();
                }
            }
        }
        private void HomeButtonClick(object sender, RoutedEventArgs e)
        {
            DoctorHomeScreen.doctorHomeScreen.Show();
            this.Close();
        }
    }
}
