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

            dataGridSuggestedMedicalAppointments.ItemsSource = GetSuggestedMedicalAppointments(enteredPatient, enteredDateTime, enteredAppointmentType, enteredPriority);

        }

        public ObservableCollection<MedicalAppointmentStruct> GetSuggestedMedicalAppointments(Patient enteredPatient, DateTime enteredDateTime, AppointmentType enteredAppointmentType, string enteredPriority)
        {
            ObservableCollection<MedicalAppointmentStruct> availableMedicalAppointments = new ObservableCollection<MedicalAppointmentStruct>();

            DateTime appointmentTimeStart = new DateTime(enteredDateTime.Year, enteredDateTime.Month, enteredDateTime.Day, Constants.Constants.WORK_DAY_START_TIME, 0, 0);
            DateTime workDayEndTime = new DateTime(enteredDateTime.Year, enteredDateTime.Month, enteredDateTime.Day, Constants.Constants.WORK_DAY_END_TIME, 0, 0);


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

            List<Doctor> suggestedDoctors = new List<Doctor>(DoctorController.Instance.GetAll());
            List<Room> suggestetRooms = new List<Room>(RoomController.Instance.GetAll());

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

                foreach (Room room in suggestetRooms)
                {

                    if (room.Type == RoomType.operation)
                        temporaryRooms.Add(room);

                }
                suggestetRooms = temporaryRooms;

            }





            Interval interval = new Interval();
            for (; appointmentTimeStart.AddMinutes(durationOfAppointment) <= workDayEndTime;)
            {
                DateTime appointmentTimeEnd = appointmentTimeStart.AddMinutes(durationOfAppointment);

                interval.Start = appointmentTimeStart;
                interval.End = appointmentTimeEnd;

                if (enteredPatient.IsAvailable(interval))
                {

                    foreach (Doctor doctor in suggestedDoctors)
                    {
                        if (doctor.IsAvailable(interval))
                        {


                            foreach (Room room in suggestetRooms)
                            {


                                if (room.IsAvailable(interval))
                                {
                                    MedicalAppointmentStruct medicalAppointmentToAdd = new MedicalAppointmentStruct(-1, enteredAppointmentType, interval, enteredPatient, doctor, room);
                                    availableMedicalAppointments.Add(medicalAppointmentToAdd);

                                }

                            }
                        }

                    }
                }
                appointmentTimeStart = appointmentTimeStart.AddMinutes(jumpToNextAppointmetnTime);


            }




            ObservableCollection<MedicalAppointmentStruct> availableMedicalAppointmentsSortDefault = new ObservableCollection<MedicalAppointmentStruct>();

            foreach (MedicalAppointmentStruct item in availableMedicalAppointments)
            {
                if (item.Doctor.Id == enteredDoctor.Id)
                {
                    availableMedicalAppointmentsSortDefault.Add(item);

                }

            }
            foreach (MedicalAppointmentStruct item in availableMedicalAppointments)
            {
                if (!(item.Doctor.Id == enteredDoctor.Id))
                {
                    availableMedicalAppointmentsSortDefault.Add(item);

                }

            }
            availableMedicalAppointments = availableMedicalAppointmentsSortDefault;


            if (enteredPriority.Equals("Lekar"))
            {
                ObservableCollection<MedicalAppointmentStruct> availableMedicalAppointmentsSortDoctor = new ObservableCollection<MedicalAppointmentStruct>();

                foreach (MedicalAppointmentStruct item in availableMedicalAppointments)
                {
                    if (item.Doctor.Id == enteredDoctor.Id)
                    {
                        availableMedicalAppointmentsSortDoctor.Add(item);

                    }

                }

                if(availableMedicalAppointmentsSortDoctor.Count == 0)
                {
                    availableMedicalAppointmentsSortDoctor = GetSuggestedMedicalAppointments(enteredPatient, enteredDateTime.AddDays(1), enteredAppointmentType, enteredPriority);

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

            MedicalAppointment medicalAppointmentTemp = new MedicalAppointment(medicalAppointmentStruct.Id, medicalAppointmentStruct.Type, medicalAppointmentStruct.Interval, medicalAppointmentStruct.Room, medicalAppointmentStruct.Patient, medicalAppointmentStruct.Doctor);
            MedicalAppointmentController.Instance.Create(medicalAppointmentTemp);
            ZdravoCorpTabs.MedicalAppointmentList.Add(medicalAppointmentTemp);


            Close();
        }
    }

}
