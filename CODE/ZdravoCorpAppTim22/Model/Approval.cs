using Model;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace ZdravoCorpAppTim22.Model
{
    public class Approval : IHasID
    {
        public int Id { get; set; }
        public bool IsApproved { get; set; }
        [JsonConverter(typeof(DoctorToIDConverter))]
        public Doctor Doctor { get; set; }

        [JsonConverter(typeof(MedicineDataToIDConverter))]
        private MedicineData medicineData;
        [JsonConverter(typeof(MedicineDataToIDConverter))]
        public MedicineData MedicineData
        {
            get => medicineData;
            set
            {
                if (value != medicineData)
                {
                    medicineData = value;
                    medicineData.Approval = this;
                }
            }
        }

        public string Message { get; set; }

        [JsonConstructor]
        public Approval() { }
    }
}
