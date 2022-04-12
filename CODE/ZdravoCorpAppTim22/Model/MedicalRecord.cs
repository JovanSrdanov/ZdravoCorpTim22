using System.Text.Json.Serialization;
namespace Model
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public BloodType BloodType { get; set; }

        [JsonIgnore]
        public Patient Patient { get; set; }

        [JsonConstructor]
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