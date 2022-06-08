using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.AppointmentsTab
{
    /// <summary>
    /// Interaction logic for ChoosingIntervals.xaml
    /// </summary>
    public partial class ChoosingIntervalsView : Page
    {
        public ChoosingIntervalsView(ChoosingIntervalsViewModel choosingIntervalsViewModel)
        {
            InitializeComponent();
          
            this.DataContext = choosingIntervalsViewModel;

        }

        private void Cancle_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            
            if (PatientController.Instance.AntiTroll((Patient)AuthenticationController.Instance.GetLoggedUser()))
            {
                MessageBox.Show("Pacijent je blokiran!");
                List<Window> windows = Application.Current.Windows.Cast<Window>().Where(window => window.Visibility != Visibility.Hidden).ToList();
                foreach (Window window in windows)
                {
                    window.Close();
                }
                return;
            }

            MessageBox.Show("Datum promenjen!");
           
        }

        private void StartPage_Click(object sender, RoutedEventArgs e)
        {
            AppointmentsPage appointmentsPage = new AppointmentsPage();
            this.NavigationService.Navigate(appointmentsPage);
        }
    }
}
