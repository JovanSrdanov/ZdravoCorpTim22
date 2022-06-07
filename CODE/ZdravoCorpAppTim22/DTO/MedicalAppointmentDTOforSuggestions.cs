using Model;
using ZdravoCorpAppTim22.Model.Utility;

namespace ZdravoCorpAppTim22.DTO
{
    public class MedicalAppointmentDTOforSuggestions
    {
        public int Id { get; set; }
        public AppointmentType Type { get; set; }
        public Interval Interval { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public Room Room { get; set; }

        public MedicalAppointmentDTOforSuggestions(int id, AppointmentType type, Interval interval, Patient patient, Doctor doctor, Room room)
        {
            Id = id;
            Type = type;
            Interval = interval;
            Patient = patient;
            Doctor = doctor;
            Room = room;
        }
    }
}
