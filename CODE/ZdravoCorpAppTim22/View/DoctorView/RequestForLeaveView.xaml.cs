using Controller;
using Model;
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
using System.Windows.Shapes;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public partial class RequestForLeaveView : Window
    {
        private Doctor requestingDoctor;
        public RequestForLeaveView()
        {
            InitializeComponent();
            requestingDoctor = DoctorController.Instance.GetByID(DoctorHome.selectedDoctorId);
        }

        private bool validateDate(Interval absenceInterval)
        {
            bool returnValue = true;
            if (!RequestForAbsenceController.Instance.validateDate(absenceInterval))
            {
                MessageBox.Show("Invalid date input", "Request for absence", MessageBoxButton.OK, MessageBoxImage.Warning);
                returnValue =  false;
            }
            return returnValue;
        }

        private bool hasAlreadyRequestedAbsenceInSelectedPeriod(Interval absenceInterval, Doctor requestingDoctor)
        {
            bool returnValue = false;
            if (RequestForAbsenceController.Instance.hasAlreadyRequestedAbsenceInSelectedPeriod(absenceInterval, requestingDoctor))
            {
                MessageBox.Show("You have already requested for absence in the selected time period",
                    "Request for absence", MessageBoxButton.OK, MessageBoxImage.Warning);
                returnValue = true;
            }
            return returnValue;
        }

        private bool alreadyHasAnAppointment(Interval absenceInterval, Doctor requestingDoctor) 
        {
            bool returnValue = false;
            if (RequestForAbsenceController.Instance.alreadyHasAnAppointment(absenceInterval, requestingDoctor))
            {
                MessageBox.Show("You have an appointment at that time", "Request for absence",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                returnValue = true;
            }
            return returnValue;
        }

        private bool areMultipleDoctorsOfSameTypeOnLeave(bool isUrgent, Interval absenceInterval)
        {
            bool returnValue = false;
            if (isUrgent == false && RequestForAbsenceController.Instance.
                areMultipleDoctorsOfSameTypeOnLeave(requestingDoctor.DoctorSpecialization.Name, absenceInterval))
            {
                MessageBox.Show("There are already multiple doctors of the same specialization on leave for that time period",
                    "Request for absence", MessageBoxButton.OK, MessageBoxImage.Warning);
                returnValue = true;
            }
            return returnValue;
        }

        private void SendBtnClick(object sender, RoutedEventArgs e)
        {
            string reasonForAbsence = ReasonForAbsenceTextBox.Text;
            Interval absenceInterval = new Interval();
            absenceInterval.Start = (DateTime)AbsenceStartDatePicker.SelectedDate;
            absenceInterval.End = (DateTime)AbsenceEndDatePicker.SelectedDate;
            bool isUrgent = (bool)UrgentCheckBox.IsChecked;

            if (!validateDate(absenceInterval)) return;
            if (hasAlreadyRequestedAbsenceInSelectedPeriod(absenceInterval, requestingDoctor)) return;
            if (alreadyHasAnAppointment(absenceInterval, requestingDoctor)) return;
            if (areMultipleDoctorsOfSameTypeOnLeave(isUrgent, absenceInterval)) return;

            RequestForAbsence newRequest = new RequestForAbsence(reasonForAbsence, isUrgent, absenceInterval, requestingDoctor);
            RequestForAbsenceController.Instance.Create(newRequest);

            this.Owner.Show();
            this.Close();
        }

        private void CancelBtnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult cancleAnswer = MessageBox.Show("Close window without saving?", "Request for absence", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (cancleAnswer)
            {
                case MessageBoxResult.Yes:
                    this.Owner.Show();
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void LogOutBtn(object sender, RoutedEventArgs e)
        {
            DoctorHome.doctorHome.Show();
            this.Close();
        }

        private void HomeButtonClick(object sender, RoutedEventArgs e)
        {
            DoctorHomeScreen.doctorHomeScreen.Show();
            this.Close();
        }
    }
}
