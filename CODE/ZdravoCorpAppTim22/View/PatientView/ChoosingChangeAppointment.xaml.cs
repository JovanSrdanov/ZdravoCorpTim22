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
            enteredPatient = ZdravoCorpTabs.LoggedPatient;
            SelectedAppointmentDoctor.Content = "Lekar: " + medicalAppointmentToChange.doctor.Name + " " + medicalAppointmentToChange.doctor.Surname;
            SelectedAppointmentRoom.Content = "Šifra sobe: " + medicalAppointmentToChange.room.Id;
            SelectedAppointmentDate.Content = "Novi datum: " + ChangeAppointment.selectedDateTime.ToString("dd.MM.yyyy");
            NewAppotimentsDataGrid.ItemsSource = MedicalAppointmentController.Instance.GetNewMedicalAppointments(medicalAppointmentToChange.doctor, medicalAppointmentToChange.room, enteredPatient, ChangeAppointment.selectedDateTime, medicalAppointmentToChange.Type);
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
            PatientController.Instance.AntiTroll(ZdravoCorpTabs.LoggedPatient);
            if (ZdravoCorpTabs.LoggedPatient == null)
            {
                
                Close();
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
