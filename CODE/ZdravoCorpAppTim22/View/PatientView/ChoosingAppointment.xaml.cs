using Controller;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;
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


        public ObservableCollection<MedicalAppointmentStruct> MedicalAppointmentsList { get; set; }

        public ChoosingAppointment()
        {
            InitializeComponent();

            enteredAppointmentType = MakeAppointment.selectedAppointmentType;
            enteredDoctor = MakeAppointment.selectedDoctor;
            enteredDateTime = MakeAppointment.selectedDateTime;
            enteredPriority = MakeAppointment.selectedPriority;
            enteredPatient = PatientSelectionForTemporaryPurpose.LoggedPatient;

            dataGridSuggestedMedicalAppointments.ItemsSource = MedicalAppointmentController.Instance.GetSuggestedMedicalAppointments(enteredPatient, enteredDateTime, enteredAppointmentType, enteredPriority, enteredDoctor);

        }

        private void ConfirmAppointment_Click(object sender, RoutedEventArgs e)
        {
            MedicalAppointmentStruct medicalAppointmentStruct = (MedicalAppointmentStruct)dataGridSuggestedMedicalAppointments.SelectedItem;
            if (medicalAppointmentStruct == null)
            {
                return;
            }

            MedicalAppointment medicalAppointmentTemp = new MedicalAppointment(medicalAppointmentStruct.Id, medicalAppointmentStruct.Type, medicalAppointmentStruct.Interval, medicalAppointmentStruct.Room, medicalAppointmentStruct.Patient, medicalAppointmentStruct.Doctor);
            MedicalAppointmentController.Instance.Create(medicalAppointmentTemp);
            ZdravoCorpTabs.MedicalAppointmentList.Add(medicalAppointmentTemp);


            Close();
        }
    }

}
