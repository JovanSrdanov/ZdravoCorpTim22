using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public partial class ReferToDoctorView : Window
    {
        private Patient forwardedPatient;
        private CreateReport parentWindow;
        public ReferToDoctorView(CreateReport parentWindow, Patient forwardedPatient)
        {
            InitializeComponent();

            this.forwardedPatient = forwardedPatient;
            this.parentWindow = parentWindow;

            setItemSources();
        }

        private void setItemSources()
        {
            DoctorComboBox.ItemsSource = getAvailableDoctorsForRefferal();         //vraca listu svih doktora izuzev ulogovanog
            DoctorComboBox.SelectedIndex = 0;
            setAppointmentTypeItemSource();
            TimeComboBox.SelectedIndex = 0;
        }

        private void setAppointmentTypeItemSource()
        {
            AppointmentTypeComboBox.ItemsSource = new ObservableCollection<AppointmentType>(getAppropriateSpicializationAppointmentTypes());
            AppointmentTypeComboBox.SelectedIndex = 0;
        }

        private List<AppointmentType> getAppropriateSpicializationAppointmentTypes()
        {
            List<AppointmentType> appointmentTypes = Enum.GetValues(typeof(AppointmentType)).Cast<AppointmentType>().ToList();
            if (DoctorController.Instance.isDoctorRegular(DoctorComboBox.SelectedItem as Doctor))
            {
                appointmentTypes.Remove(global::Model.AppointmentType.Operation);
            }
            return appointmentTypes;
        }

        private ObservableCollection<Doctor> getAvailableDoctorsForRefferal()
        {
            Doctor selectedDoctor = DoctorController.Instance.GetByID(DoctorHomeScreen.LoggedInDoctor.Id);
            ObservableCollection<Doctor> allDoctors = new ObservableCollection<Doctor>(DoctorController.Instance.GetAll());
            allDoctors.Remove(selectedDoctor);
            return allDoctors;
        }

        private void setChecBoxVisibility()
        {
            if (AppointmentTypeComboBox.SelectedItem == null || (AppointmentType)(AppointmentTypeComboBox.SelectedItem) != 
                AppointmentType.Operation)
            {
                UrgentCheckBox.Visibility = Visibility.Hidden;
                UrgentCheckBox.IsChecked = false;
            }
            else
                UrgentCheckBox.Visibility = Visibility.Visible;
        }

        private bool isInputDataValid()   
        {
#pragma warning disable CS0168
            try
            {
                DateTime.Parse(AppointmentDatePicker.Text + " " + TimeComboBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter the valid date and time", "Refer to a doctor", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;

#pragma warning restore CS0168 
        }

        private void updateAnamnesis()
        {

            Doctor refferedDoctor = DoctorComboBox.SelectedItem as Doctor;
            string referralComment = "\n\nReffered to: " + refferedDoctor.Name + " " + refferedDoctor.Surname +
                "\nAppointment date: " + getForwardedAppointmentDate() + "\nComment for the specialist:\n" + CommentTextBox.Text;
            parentWindow.AnamnesisBox.Text += referralComment;
        }

        private Interval getInterval(DateTime forwardedDate)
        {
            Interval forvardedAppointmentTimeLength = new Interval();
            forvardedAppointmentTimeLength.Start = forwardedDate;
            forvardedAppointmentTimeLength.End = forwardedDate;
            return forvardedAppointmentTimeLength;
        }

        private DateTime getForwardedAppointmentDate()
        {
            return DateTime.Parse(AppointmentDatePicker.Text + " " + TimeComboBox.Text);
        }

        private void createForwardedAppointment()
        {
            MedicalAppointment forwardedAppointment =
                new MedicalAppointment(-1, (AppointmentType)AppointmentTypeComboBox.SelectedItem, 
                getInterval(getForwardedAppointmentDate()), null,
                forwardedPatient, (DoctorComboBox.SelectedItem as Doctor));
            forwardedAppointment.isUrgent = true;
            MedicalAppointmentController.Instance.Create(forwardedAppointment);
            MedicalRecordView.newlyCreatedAppointments.Add(forwardedAppointment);
        }

        private void selectedDoctorChanged(object sender, SelectionChangedEventArgs e)
        {
            setAppointmentTypeItemSource();
        }

        private void selectedAppointmentTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            setChecBoxVisibility();
        }

        private void ConfirmBtnClick(object sender, RoutedEventArgs e)
        {
            if (!isInputDataValid())
                return;
            updateAnamnesis();
            createForwardedAppointment();
            parentWindow.ReferToSpecialistBtn.Visibility = Visibility.Hidden;

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

        private void CancelBtnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Close window without saving?", "Refer to another doctor", 
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    this.Owner.Show();
                    this.Close();
                    break;
                case MessageBoxResult.No:

                    break;
            }
        }
    }
}
