using Model;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace ZdravoCorpAppTim22.Model
{
    public class Approval : IHasID
    {
        public int Id { get; set; }

        private bool isApproved;
        public string Message { get; set; }

        private Doctor doctor;

        [JsonConverter(typeof(MedicineDataToIDConverter))]
        private MedicineData medicineData;

        #region properties

        public bool IsApproved
        {
            get { return isApproved; }
            set
            {
                if (isApproved != value)
                {
                    isApproved = value;
                }
            }
        }

        [JsonConverter(typeof(DoctorToIDConverter))]
        public Doctor Doctor
        {
            get { return doctor; }
            set
            {
                if (doctor != value)
                {
                    doctor = value;
                }
            }
        }

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
        #endregion

        [JsonConstructor]
        public Approval() { }
    }
}
