using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model.Generic;

namespace Model
{
    public class MedicalRecord : IHasID
    {
        public int Id { get; set; }
        public BloodType BloodType { get; set; }

        //dodao
        public ObservableCollection<string> AllergiesList { get; set; }
        public ObservableCollection<string> ConditionList { get; set; }
        public ObservableCollection<MedicalReport> medicalReport { get; set; }
        //dodao

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

            AllergiesList = new ObservableCollection<string>
            {
                "Peanuts", "Sun", "Milk", "Polen"
            };

            ConditionList = new ObservableCollection<string>
            {
                "Arthitis", "Asthma", "Glaucoma"
            };

            medicalReport = new ObservableCollection<MedicalReport>();
        }
    }
}