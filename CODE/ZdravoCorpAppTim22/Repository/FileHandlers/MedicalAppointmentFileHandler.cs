using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace ZdravoCorpAppTim22.Repository.FileHandlers
{
    internal class MedicalAppointmentFileHandler
    {
        private Serializer<List<MedicalAppointment>> serializer;

        public MedicalAppointmentFileHandler(string filename)
        {
            serializer = new Serializer<List<MedicalAppointment>>(filename);
        }

        public void SaveData(List<MedicalAppointment> medicalAppointments)
        {
            foreach (var item in medicalAppointments)
            {
                item.RoomID = item.Room.id;
                item.DoctorID = item.Doctor.ID;
                item.PatientID = item.PatientID;

                serializer.Serialize(medicalAppointments);
            }

        }

        public List<MedicalAppointment> LoadData()
        {
            Debug.WriteLine("rade");
            RoomController roomController = RoomController.Instance;
            DoctorController doctorController = DoctorController.Instance;
            PatientController patientController = PatientController.Instance;
            List < MedicalAppointment > medicalAppointments = serializer.Deserialize();

            if (medicalAppointments == null)
            {
                return new List<MedicalAppointment>();
            }

            foreach (var item in medicalAppointments)
            {
                Room room = roomController.GetRoomByID(item.RoomID);
                if(room != null)
                {
                    item.room = room;
                    room.AddMedicalAppointment(item);
                }
                Doctor doctor = doctorController.GetByID(item.DoctorID);
                if(doctor != null)
                {
                    item.doctor = doctor;
                    doctor.AddMedicalAppointment(item);
                }
                Patient patient = patientController.GetByID(item.PatientID);
                if(patient != null)
                {
                    item.patient = patient;
                    patient.AddMedicalAppointment(item);
                }
            }
            return medicalAppointments;
        }
    }
}
