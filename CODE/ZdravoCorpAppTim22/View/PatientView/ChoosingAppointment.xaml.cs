using Controller;
using Model;
using System;
using System.Collections.Generic;
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
        public int counterForList;

        public ObservableCollection<MedicalAppointmentStruct> MedicalAppointmentsList { get; set; }
        public List<MedicalAppointmentStruct> medicalAppointments;
        public ChoosingAppointment()
        {
            InitializeComponent();

            enteredAppointmentType = MakeAppointment.selectedAppointmentType;
            enteredDoctor = MakeAppointment.selectedDoctor;
            enteredDateTime = MakeAppointment.selectedDateTime;
            enteredPriority = MakeAppointment.selectedPriority;
            enteredPatient = PatientSelectionForTemporaryPurpose.LoggedPatient;

            SelectedDateLabel.Content = "Datum: " + enteredDateTime.ToLongDateString();
            SelectedDoctorLabel.Content = "Pozeljan lekar: " + enteredDoctor.Name + " " + enteredDoctor.Surname;
            SelectedPriority.Content = "Prioritet je: " + enteredPriority;
            SelectedAppointmentType.Content = "Vrsta termina: " + enteredAppointmentType;
            CurrentPatient.Content = "Vi ste:  " + enteredPatient.Name;



            medicalAppointments = GetSuggestedMedicalAppointments(enteredPatient, enteredDateTime, enteredAppointmentType, enteredPriority);

            MedicalAppointmentsList = new ObservableCollection<MedicalAppointmentStruct>(medicalAppointments);
            dataGridSuggestedMedicalAppointments.ItemsSource = MedicalAppointmentsList;

        }

        private List<MedicalAppointmentStruct> GetSuggestedMedicalAppointments(Patient enteredPatient, DateTime enteredDateTime, AppointmentType enteredAppointmentType, string enteredPriority)
        {
            List<MedicalAppointmentStruct> availableMedicalAppointments = new List<MedicalAppointmentStruct>();

            DateTime appointmentTimeStart = new DateTime(enteredDateTime.Year, enteredDateTime.Month, enteredDateTime.Day, 7, 0, 0);
            DateTime workDayEndTime = new DateTime(enteredDateTime.Year, enteredDateTime.Month, enteredDateTime.Day, 9, 0, 0);
            DateTime appointmentTimeEnd = new DateTime();

            int jumpToNextAppointmetnTime = 15;
            int durationOfAppointment = 15;

            if (enteredAppointmentType == AppointmentType.examination)
            {
                durationOfAppointment = 30;
            }

            if (enteredAppointmentType == AppointmentType.operation)
            {
                durationOfAppointment = 60;
            }

            List<Doctor> suggestedDoctors = DoctorController.Instance.GetAll();
            List<Room> suggestetRooms = RoomController.Instance.GetAllRooms();

            if (enteredAppointmentType == AppointmentType.operation)
            {
                List<Doctor> temporaryDoctors = new List<Doctor>();

                foreach (Doctor doctor in suggestedDoctors)
                {
                    if (doctor.DoctorType == DoctorSpecialisationType.specialist)
                        temporaryDoctors.Add(doctor);

                }
                suggestedDoctors = temporaryDoctors;

                List<Room> temporaryRooms = new List<Room>();

                foreach (Room room in temporaryRooms)
                {
                    if (room.type == RoomType.operation)
                        temporaryRooms.Add(room);

                }
                suggestetRooms = temporaryRooms;

            }


            List<DateTime> forListStart = new List<DateTime>();
            List<DateTime> forListEnd = new List<DateTime>();
            List<Room> forListRoom = new List<Room>();
            List<Doctor> forListDoctor = new List<Doctor>();
            counterForList = 0;


            for (; appointmentTimeStart.AddMinutes(durationOfAppointment) <= workDayEndTime;)
            {
                appointmentTimeEnd = appointmentTimeStart.AddMinutes(durationOfAppointment);

                if (enteredPatient.IsAvailable(appointmentTimeStart, appointmentTimeEnd))
                {

                    foreach (Doctor doctor in suggestedDoctors)
                    {
                        if (doctor.IsAvailable(appointmentTimeStart, appointmentTimeEnd))
                        {


                            foreach (Room room in suggestetRooms)
                            {


                                if (room.IsAvailable(appointmentTimeStart, appointmentTimeEnd))
                                {
                                    forListDoctor.Add(doctor);
                                    forListStart.Add(appointmentTimeStart);
                                    forListEnd.Add(appointmentTimeEnd);
                                    forListRoom.Add(room);
                                    counterForList++;

                                }

                            }
                        }

                    }
                }
                appointmentTimeStart = appointmentTimeStart.AddMinutes(jumpToNextAppointmetnTime);


            }

            for (int i = 0; i < counterForList; i++)
            {
                MedicalAppointmentStruct medicalAppointmentToAdd = new MedicalAppointmentStruct(i, enteredAppointmentType, forListStart[i], forListEnd[i], enteredPatient, forListDoctor[i], forListRoom[i]);
                availableMedicalAppointments.Add(medicalAppointmentToAdd);

            }

            if (enteredPriority.Equals("Lekar"))
            {
                List<MedicalAppointmentStruct> availableMedicalAppointmentsSortDoctor = new List<MedicalAppointmentStruct>();

                foreach (MedicalAppointmentStruct item in availableMedicalAppointments)
                {
                    if (item.Doctor.ID == enteredDoctor.ID)
                    {
                        availableMedicalAppointmentsSortDoctor.Add(item);

                    }

                }
                foreach (MedicalAppointmentStruct item in availableMedicalAppointments)
                {
                    if (!(item.Doctor.ID == enteredDoctor.ID))
                    {
                        availableMedicalAppointmentsSortDoctor.Add(item);

                    }

                }

                availableMedicalAppointments = availableMedicalAppointmentsSortDoctor;


            }


            return availableMedicalAppointments;
        }

        private void ConfirmAppointment_Click(object sender, RoutedEventArgs e)
        {
            MedicalAppointmentStruct medicalAppointmentStruct = (MedicalAppointmentStruct)dataGridSuggestedMedicalAppointments.SelectedItem;
            if (medicalAppointmentStruct == null)
            {
                return;
            }
            MedicalAppointment medicalAppointmentTemp = new MedicalAppointment(medicalAppointmentStruct.Id, medicalAppointmentStruct.Type, medicalAppointmentStruct.MedicalAppointmentStartDateTime, medicalAppointmentStruct.MedicalAppointmentEndDateTime, medicalAppointmentStruct.Room, medicalAppointmentStruct.Patient, medicalAppointmentStruct.Doctor);
            MedicalAppointmentController.Instance.Create(medicalAppointmentTemp);
            PatientHome.MedicalAppointmentList.Add(medicalAppointmentTemp);


            Close();
        }
    }

}
