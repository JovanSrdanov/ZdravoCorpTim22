﻿using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ZdravoCorpAppTim22.DTO;
using ZdravoCorpAppTim22.Model.Utility;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public partial class DoctorAppointmentCreate : Window
    {
        private DoctorAppointments doctorAppointments;
        private Doctor loggedInDoctor;

        private DateTime startDateTime;
        public ObservableCollection<Patient> PatientList { get; set; }
        public List<Patient> patients;

        public DoctorAppointmentCreate(DoctorAppointments doctorAppointments)
        {
            InitializeComponent();
            this.DataContext = this;
            this.doctorAppointments = doctorAppointments;

            loggedInDoctor = DoctorController.Instance.GetByID(DoctorHome.selectedDoctorId);
            setItemSources();

        }

        private void setItemSources()       //ne pomeraj
        {
            AppointmentTypeCBOX.ItemsSource = new ObservableCollection<AppointmentType>(getAppropriateSpecializationAppointmentTypes());
            AppointmentTypeCBOX.SelectedIndex = 0;

            RoomComboBox.ItemsSource = new ObservableCollection<Room>(new List<Room>(RoomController.Instance.GetAll()));
            RoomComboBox.SelectedIndex = 0;

            PatientComboBox.ItemsSource = new ObservableCollection<Patient>(new List<Patient>(PatientController.Instance.GetAll()));
            PatientComboBox.SelectedIndex = 0;
        }

        private List<AppointmentType> getAppropriateSpecializationAppointmentTypes()        //pomerio
        {
            List<AppointmentType> appointmentTypes = Enum.GetValues(typeof(AppointmentType)).Cast<AppointmentType>().ToList();
            if (DoctorController.Instance.isDoctorRegular(loggedInDoctor))
            {
                appointmentTypes.Remove(global::Model.AppointmentType.Operation);
            }
            return appointmentTypes;
        }

        private bool isDateValid(string startDate, string startTime)        //ne pomeraj
        {
#pragma warning disable CS0168 // Variable is declared but never used
            bool returnValue = true;
            try
            {
                startDateTime = DateTime.Parse(startDate + " " + startTime);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter the valid date and time", "Create appointment", MessageBoxButton.OK, MessageBoxImage.Warning);
                returnValue = false;
                
            }
            return returnValue;
#pragma warning restore CS0168 // Variable is declared but never used
        }

        private bool isFormFilled(MedicalAppointmentDTO medicalAppointmentDTO)      //ne pomeraj
        {
            bool returnValue = true;
            if (medicalAppointmentDTO.AppointmentType == null || medicalAppointmentDTO.StartDate == null || 
                medicalAppointmentDTO.StartTime == null)
            {
                MessageBox.Show("Please fill out all fields", "Create appointment", MessageBoxButton.OK, MessageBoxImage.Warning);
                returnValue = false;
            }
            return returnValue;
        }

        private Interval getInterval(DateTime startDateTime)        //ne pomeraj
        {
            Interval interval = new Interval();
            interval.Start = startDateTime;
            interval.End = startDateTime;
            return interval;
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)      //ne pomeraj
        {
            Patient patient = PatientComboBox.SelectedItem as Patient;
            Room room = RoomComboBox.SelectedItem as Room;
            string startDate = datePicker.Text;
            string startTime = TimeComboBox.Text;
            
            if(!(isDateValid(startDate, startTime))) return;
            MedicalAppointmentDTO medicalAppointmentDTO = 
                new MedicalAppointmentDTO(datePicker.SelectedDate, startTime, AppointmentTypeCBOX.SelectedItem);
            if(!(isFormFilled(medicalAppointmentDTO))) return;

            string appointmentTypeText = AppointmentTypeCBOX.SelectedItem.ToString();
            Interval interval = getInterval(startDateTime);
            AppointmentType appointmentType = (AppointmentType)Enum.Parse(typeof(AppointmentType), appointmentTypeText);

            MedicalAppointment newMedicalAppointment = new MedicalAppointment(-1, appointmentType, interval, room, patient, loggedInDoctor);        //-1 je privremeni id
            MedicalAppointmentController.Instance.Create(newMedicalAppointment);

            DoctorAppointments.CurDocAppointemntsObservable.Add(newMedicalAppointment);

            doctorAppointments.Show();
            this.Close();

        }

        private void CancelBtnClick(object sender, RoutedEventArgs e)       //ne pomeraj
        {
            CancelDialogBox();
        }

        private void CancelDialogBox()      //ne pomeraj
        {
            MessageBoxResult result = MessageBox.Show("Close window without saving?", "Create appointment", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    doctorAppointments.Show();
                    this.Close();
                    break;
                case MessageBoxResult.No:

                    break;
            }
        }

        private void LogOutBtn(object sender, RoutedEventArgs e)        //ne pomeraj
        {
            DoctorHome.doctorHome.Show();
            this.Close();
        }

        private void HomeButtonClick(object sender, RoutedEventArgs e)      //ne pomeraj
        {
            DoctorHomeScreen.doctorHomeScreen.Show();
            this.Close();
        }
    }
}
