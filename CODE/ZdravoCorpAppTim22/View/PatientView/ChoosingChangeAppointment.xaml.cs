using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;

namespace ZdravoCorpAppTim22.View.PatientView
{

    public partial class ChoosingChangeAppointment : Window
    {

        public ObservableCollection<MedicalAppointmentStruct> MedicalAppointmentsList { get; set; }
        public List<MedicalAppointmentStruct> medicalAppointments;
        public Patient enteredPatient;


        public ChoosingChangeAppointment()
        {
            MedicalAppointment medicalAppointmentToChange = ZdravoCorpTabs.MedicalAppointmentSelected;
            InitializeComponent();
            enteredPatient = PatientSelectionForTemporaryPurpose.LoggedPatient;
            SelectedAppointmentDoctor.Content = "Lekar: " + medicalAppointmentToChange.doctor.Name + " " + medicalAppointmentToChange.doctor.Surname;
            SelectedAppointmentRoom.Content = "Šifra sobe: " + medicalAppointmentToChange.room.Id;
            SelectedAppointmentDate.Content = "Novi datum: " + ChangeAppointment.selectedDateTime.ToString("dd.MM.yyyy");

            enteredPatient = PatientSelectionForTemporaryPurpose.LoggedPatient;
            medicalAppointments = GetNewMedicalAppointments(medicalAppointmentToChange.doctor, medicalAppointmentToChange.room, enteredPatient, ChangeAppointment.selectedDateTime, medicalAppointmentToChange.Type);

            MedicalAppointmentsList = new ObservableCollection<MedicalAppointmentStruct>(medicalAppointments);
            NewAppotimentsDataGrid.ItemsSource = MedicalAppointmentsList;


        }

        private List<MedicalAppointmentStruct> GetNewMedicalAppointments(Doctor doctor, Room room, Patient enteredPatient, DateTime selectedDateTime, AppointmentType type)
        {
            List<MedicalAppointmentStruct> availableMedicalAppointments = new List<MedicalAppointmentStruct>();

            DateTime appointmentTimeStart = new DateTime(selectedDateTime.Year, selectedDateTime.Month, selectedDateTime.Day, Constants.Constants.WORK_DAY_START_TIME, 0, 0);
            DateTime workDayEndTime = new DateTime(selectedDateTime.Year, selectedDateTime.Month, selectedDateTime.Day, Constants.Constants.WORK_DAY_END_TIME, 0, 0);


            int jumpToNextAppointmetnTime = 15;
            int durationOfAppointment = 15;

            if (type == AppointmentType.examination)
            {
                durationOfAppointment = 30;
            }

            if (type == AppointmentType.operation)
            {
                durationOfAppointment = 60;

            }

            Interval interval = new Interval();
            for (; appointmentTimeStart.AddMinutes(durationOfAppointment) <= workDayEndTime;)
            {
                DateTime appointmentTimeEnd = appointmentTimeStart.AddMinutes(durationOfAppointment);
                interval.Start = appointmentTimeStart;
                interval.End = appointmentTimeEnd;

                if (enteredPatient.IsAvailable(interval))
                {
                    if (doctor.IsAvailable(interval))
                    {
                        if (room.IsAvailable(interval))
                        {


                            MedicalAppointmentStruct medicalAppointmentStructToAdd = new MedicalAppointmentStruct(1, type, interval, enteredPatient, doctor, room);
                            availableMedicalAppointments.Add(medicalAppointmentStructToAdd);

                        }
                    }
                }
                appointmentTimeStart = appointmentTimeStart.AddMinutes(jumpToNextAppointmetnTime);
            }


            return availableMedicalAppointments;
        }

        private void Cancle_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            MedicalAppointmentStruct medicalAppointmentStruct = (MedicalAppointmentStruct)NewAppotimentsDataGrid.SelectedItem;
            if (medicalAppointmentStruct == null)
            {
                return;
            }
            MedicalAppointment medicalAppointmentTemp = new MedicalAppointment(ZdravoCorpTabs.MedicalAppointmentSelected.Id, medicalAppointmentStruct.Type, medicalAppointmentStruct.Interval, medicalAppointmentStruct.Room, medicalAppointmentStruct.Patient, medicalAppointmentStruct.Doctor);

            MedicalAppointmentController.Instance.Update(medicalAppointmentTemp);
            ZdravoCorpTabs.MedicalAppointmentList.Remove(ZdravoCorpTabs.MedicalAppointmentSelected);
            ZdravoCorpTabs.MedicalAppointmentList.Add(medicalAppointmentTemp); 

              Close();
        }
    }
}
