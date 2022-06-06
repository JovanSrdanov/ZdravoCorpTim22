using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
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
            if (selectedRequest == null || selectedRequest.RequestState != RequestState.rejected)
            {
                DetailsBtn.IsEnabled = false;
            }
            else DetailsBtn.IsEnabled = true;
        }

        private void BackBtnClick(object sender, RoutedEventArgs e)     //ne pomeraj
        {
            this.Owner.Show();
            this.Close();
        }

        private void LogOutBtn(object sender, RoutedEventArgs e)        //ne pomeraj
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
        private void HomeButtonClick(object sender, RoutedEventArgs e)      //ne pomeraj
        {
            DoctorHomeScreen.doctorHomeScreen.Show();
            this.Close();
        }
    }
}
