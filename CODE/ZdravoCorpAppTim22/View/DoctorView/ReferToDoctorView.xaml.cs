using Controller;
using Model;
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
using ZdravoCorpAppTim22.Model.Utility;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public partial class ReferToDoctorView : Window
    {
        private List<AppointmentType> allAppointmentTypes;
        private List<AppointmentType> allNonSpecialistAppointmentTypes;
        private Patient forwardedPatient;
        private CreateReport parentWindow;
        public ReferToDoctorView(CreateReport parentWindow, Patient forwardedPatient)
        {
            InitializeComponent();

            allAppointmentTypes = Enum.GetValues(typeof(AppointmentType)).Cast<AppointmentType>().ToList();
            allNonSpecialistAppointmentTypes = new List<AppointmentType>(allAppointmentTypes);
            allNonSpecialistAppointmentTypes.Remove(AppointmentType.Operation);
            this.forwardedPatient = forwardedPatient;
            this.parentWindow = parentWindow;

            DoctorComboBox.ItemsSource = getAvailableDoctorsForRefferal();         //vraca listu svih doktora izuzev ulogovanog
            DoctorComboBox.SelectedIndex = 0;
            setAppropriateAppointmentTypes();
            AppointmentTypeComboBox.SelectedIndex = 0;
            TimeComboBox.SelectedIndex = 0;     
    }

        ObservableCollection<Doctor> getAvailableDoctorsForRefferal()
        {
            Doctor selectedDoctor = DoctorController.Instance.GetByID(DoctorHome.selectedDoctorId);
            ObservableCollection<Doctor> allDoctors = new ObservableCollection<Doctor>(DoctorController.Instance.GetAll());
            allDoctors.Remove(selectedDoctor);
            return allDoctors;
        }

        void setAppropriateAppointmentTypes()
        {     
            if ((DoctorComboBox.SelectedItem as Doctor).DoctorType == DoctorSpecialisationType.specialist)
                AppointmentTypeComboBox.ItemsSource = new ObservableCollection<AppointmentType>(allAppointmentTypes);
            else
                AppointmentTypeComboBox.ItemsSource = new ObservableCollection<AppointmentType>(allNonSpecialistAppointmentTypes);
        }
        
        void setChecBoxVisibility()
        {
            if (AppointmentTypeComboBox.SelectedItem == null || (AppointmentType)(AppointmentTypeComboBox.SelectedItem) != AppointmentType.Operation)
            {
                UrgentCheckBox.Visibility = Visibility.Hidden;
                UrgentCheckBox.IsChecked = false;
            }
            else
                UrgentCheckBox.Visibility = Visibility.Visible;
        }

        bool validateInputData()
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

        void updateAnamnesis()
        {

            Doctor refferedDoctor =  DoctorComboBox.SelectedItem as Doctor;
            string referralComment = "\n\nReffered to: " + refferedDoctor.Name + " " + refferedDoctor.Surname + 
                "\nAppointment date: " + getForwardedAppointmentDate() + "\nComment for the specialist:\n" + CommentTextBox.Text;
            parentWindow.AnamnesisBox.Text += referralComment;
        }

        Interval getInterval(DateTime forwardedDate)
        {
            Interval forvardedAppointmentTimeLength = new Interval();
            forvardedAppointmentTimeLength.Start = forwardedDate;
            forvardedAppointmentTimeLength.End = forwardedDate;
            return forvardedAppointmentTimeLength;
        }

        DateTime getForwardedAppointmentDate()
        {
            return DateTime.Parse(AppointmentDatePicker.Text + " " + TimeComboBox.Text);
        }

        void createForwardedAppointment()
        {
            MedicalAppointment forwardedAppointment =
                new MedicalAppointment(-1, (AppointmentType)AppointmentTypeComboBox.SelectedItem, getInterval(getForwardedAppointmentDate()), null,
                forwardedPatient, (DoctorComboBox.SelectedItem as Doctor));
            forwardedAppointment.isUrgent = true;
            MedicalAppointmentController.Instance.Create(forwardedAppointment);
            MedicalRecordView.newlyCreatedAppointments.Add(forwardedAppointment);
        }

        //Selection changed event handlers

        private void selectedDoctorChanged(object sender, SelectionChangedEventArgs e)
        {
            setAppropriateAppointmentTypes();
            AppointmentTypeComboBox.SelectedIndex = 0;
        }

        private void selectedAppointmentTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            setChecBoxVisibility();
        }

        //Button click event handlers


        private void ConfirmBtnClick(object sender, RoutedEventArgs e)
        {
            if (!validateInputData())
                return;
            updateAnamnesis();
            createForwardedAppointment();
            parentWindow.ReferToSpecialistBtn.Visibility = Visibility.Hidden;

            this.Owner.Show();
            this.Close();
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

        private void CancelBtnClick(object sender, RoutedEventArgs e)
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
