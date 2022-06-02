using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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

        private void setItemSources()       //ne pomeraj
        {
            DoctorComboBox.ItemsSource = getAvailableDoctorsForRefferal();         //vraca listu svih doktora izuzev ulogovanog
            DoctorComboBox.SelectedIndex = 0;
            setAppointmentTypeItemSource();
            TimeComboBox.SelectedIndex = 0;
        }

        private void setAppointmentTypeItemSource()     //ne pomeraj
        {
            AppointmentTypeComboBox.ItemsSource = new ObservableCollection<AppointmentType>(getAppropriateSpicializationAppointmentTypes());
            AppointmentTypeComboBox.SelectedIndex = 0;
        }

        private List<AppointmentType> getAppropriateSpicializationAppointmentTypes()       //pomerio
        {
            List<AppointmentType> appointmentTypes = Enum.GetValues(typeof(AppointmentType)).Cast<AppointmentType>().ToList();
            if (DoctorController.Instance.isDoctorRegular(DoctorComboBox.SelectedItem as Doctor))
            {
                appointmentTypes.Remove(global::Model.AppointmentType.Operation);
            }
            return appointmentTypes;
        }

        private ObservableCollection<Doctor> getAvailableDoctorsForRefferal()       //mozda?
        {
            Doctor selectedDoctor = DoctorController.Instance.GetByID(DoctorHomeScreen.LoggedInDoctor.Id);
            ObservableCollection<Doctor> allDoctors = new ObservableCollection<Doctor>(DoctorController.Instance.GetAll());
            allDoctors.Remove(selectedDoctor);
            return allDoctors;
        }

        private void setChecBoxVisibility()     //ne pomeraj
        {
            if (AppointmentTypeComboBox.SelectedItem == null || (AppointmentType)(AppointmentTypeComboBox.SelectedItem) != AppointmentType.Operation)
            {
                UrgentCheckBox.Visibility = Visibility.Hidden;
                UrgentCheckBox.IsChecked = false;
            }
            else
                UrgentCheckBox.Visibility = Visibility.Visible;
        }

        private bool isInputDataValid()             //ne pomeraj   
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

        private void updateAnamnesis()      //ne pomeraj
        {

            Doctor refferedDoctor = DoctorComboBox.SelectedItem as Doctor;
            string referralComment = "\n\nReffered to: " + refferedDoctor.Name + " " + refferedDoctor.Surname +
                "\nAppointment date: " + getForwardedAppointmentDate() + "\nComment for the specialist:\n" + CommentTextBox.Text;
            parentWindow.AnamnesisBox.Text += referralComment;
        }

        private Interval getInterval(DateTime forwardedDate)        //ne pomeraj
        {
            Interval forvardedAppointmentTimeLength = new Interval();
            forvardedAppointmentTimeLength.Start = forwardedDate;
            forvardedAppointmentTimeLength.End = forwardedDate;
            return forvardedAppointmentTimeLength;
        }

        private DateTime getForwardedAppointmentDate()      //ne pomeraj
        {
            return DateTime.Parse(AppointmentDatePicker.Text + " " + TimeComboBox.Text);
        }

        private void createForwardedAppointment()       //ne pomeraj
        {
            MedicalAppointment forwardedAppointment =
                new MedicalAppointment(-1, (AppointmentType)AppointmentTypeComboBox.SelectedItem, 
                getInterval(getForwardedAppointmentDate()), null,
                forwardedPatient, (DoctorComboBox.SelectedItem as Doctor));
            forwardedAppointment.isUrgent = true;
            MedicalAppointmentController.Instance.Create(forwardedAppointment);
            MedicalRecordView.newlyCreatedAppointments.Add(forwardedAppointment);
        }

        //Selection changed event handlers

        private void selectedDoctorChanged(object sender, SelectionChangedEventArgs e)      //ne pomeraj
        {
            setAppointmentTypeItemSource();
        }

        private void selectedAppointmentTypeChanged(object sender, SelectionChangedEventArgs e)     //ne pomeraj
        {
            setChecBoxVisibility();
        }

        //Button click event handlers

        private void ConfirmBtnClick(object sender, RoutedEventArgs e)      //ne pomeraj
        {
            if (!isInputDataValid())
                return;
            updateAnamnesis();
            createForwardedAppointment();
            parentWindow.ReferToSpecialistBtn.Visibility = Visibility.Hidden;

            this.Owner.Show();
            this.Close();
        }

        private void LogOutBtn(object sender, RoutedEventArgs e)        //ne pomeraj
        {
            //DoctorHome.doctorHome.Show();
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

        private void CancelBtnClick(object sender, RoutedEventArgs e)       //ne pomeraj
        {
            MessageBoxResult result = MessageBox.Show("Close window without saving?", "Refer to another doctor", MessageBoxButton.YesNo, MessageBoxImage.Warning);
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
