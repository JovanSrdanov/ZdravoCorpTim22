using Controller;
using Model;
using System;
using System.Windows;
using ZdravoCorpAppTim22.DTO;
using ZdravoCorpAppTim22.Model;



namespace ZdravoCorpAppTim22.View.PatientView
{


    public partial class ChoosingAppointment : Window
    {

        public Doctor enteredDoctor;
        public Patient enteredPatient;
        public DateTime enteredDateTime;
        public AppointmentType enteredAppointmentType;
        public string enteredPriority;


        public ChoosingAppointment()
        {
            InitializeComponent();

            enteredAppointmentType = MakeAppointment.selectedAppointmentType;
            enteredDoctor = MakeAppointment.selectedDoctor;
            enteredDateTime = MakeAppointment.selectedDateTime;
            enteredPriority = MakeAppointment.selectedPriority;
            enteredPatient = ZdravoCorpTabs.LoggedPatient;

          //  dataGridSuggestedMedicalAppointments.ItemsSource = MedicalAppointmentController.Instance.GetSuggestedMedicalAppointments(enteredPatient, enteredDateTime, enteredAppointmentType, enteredPriority, enteredDoctor);

        }

        private void ConfirmAppointment_Click(object sender, RoutedEventArgs e)
        {
            MedicalAppointmentDTOforSuggestions medicalAppointmentStruct = (MedicalAppointmentDTOforSuggestions)dataGridSuggestedMedicalAppointments.SelectedItem;
            if (medicalAppointmentStruct == null)
            {
                return;
            }

            MedicalAppointment medicalAppointmentTemp = new MedicalAppointment(medicalAppointmentStruct.Id, medicalAppointmentStruct.Type, medicalAppointmentStruct.Interval, medicalAppointmentStruct.Room, medicalAppointmentStruct.Patient, medicalAppointmentStruct.Doctor);
            MedicalAppointmentController.Instance.Create(medicalAppointmentTemp);
            ZdravoCorpTabs.MedicalAppointmentList.Add(medicalAppointmentTemp);


            Close();
        }

        private void Cancle_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

}
