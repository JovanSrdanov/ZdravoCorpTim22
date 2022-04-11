namespace Model
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public BloodType BloodType { get; set; }
        public Patient Patient { get; set; }

        public MedicalRecord()
        {
        }

        public MedicalRecord(int id, BloodType bloodType, Patient patient)
        {
            Id = id;
            BloodType = bloodType;
            Patient = patient;
        }
    }
}