using Model;
using ZdravoCorpAppTim22.Repository.Generic;

namespace Repository
{
    public class MedicalAppointmentRepository : GenericRepository<MedicalAppointment>
    {
        public static string FileName = "MedicalAppointmentData.json";
        private static MedicalAppointmentRepository instance;
        private MedicalAppointmentRepository() : base(FileName) { }
        public static MedicalAppointmentRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicalAppointmentRepository();
                }

                return instance;
            }
        }

        public override void DeleteByID(int id)
        {

            MedicalAppointment medicalAppointment = Instance.GetByID(id);

            Patient patient = PatientRepository.Instance.GetByID(medicalAppointment.Patient.Id);
            patient.MedicalAppointment.Remove(medicalAppointment);
            
            Doctor doctor = DoctorRepository.Instance.GetByID(medicalAppointment.Doctor.Id);
            doctor.MedicalAppointment.Remove(medicalAppointment);

            Room room = RoomRepository.Instance.GetByID(medicalAppointment.Room.Id);
            room.MedicalAppointment.Remove(medicalAppointment);

            base.DeleteByID(id);
        }
    }
}