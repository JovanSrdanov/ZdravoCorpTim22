using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace ZdravoCorpAppTim22.Model
{
    public class Medicine : IHasID
    {
        public int Id { get; set; }
        public int Amount { get; set; }

        #region properties


        #endregion

        [JsonConstructor]
        public Medicine() { }
        public Medicine(Medicine m)
        {
            if (m != null)
            {
                Id = m.Id;
                Amount = m.Amount;
                MedicineData = new MedicineData(m.MedicineData);

            }
        }

        [JsonConverter(typeof(MedicineDataToIDConverter))]
        private MedicineData medicineData;
        [JsonConverter(typeof(MedicineDataToIDConverter))]
        public MedicineData MedicineData
        {
            get
            {
                return medicineData;
            }
            set
            {
                if (this.medicineData == null || !this.medicineData.Equals(value))
                {
                    if (this.medicineData != null)
                    {
                        MedicineData oldMedicineData = this.medicineData;
                        this.medicineData = null;
                        oldMedicineData.RemoveMedicine(this);
                    }
                    if (value != null)
                    {
                        this.medicineData = value;
                        this.medicineData.AddMedicine(this);
                    }
                }
            }
        }

        [JsonConverter(typeof(MedicalReceiptToIDConverter))]
        private MedicalReceipt medicalReceipt;
        [JsonConverter(typeof(MedicalReceiptToIDConverter))]
        public MedicalReceipt MedicalReceipt
        {
            get
            {
                return medicalReceipt;
            }
            set
            {
                if (this.medicalReceipt == null || !this.medicalReceipt.Equals(value))
                {
                    if (this.medicalReceipt != null)
                    {
                        MedicalReceipt oldMedicalReceipt = this.medicalReceipt;
                        this.medicalReceipt = null;
                        oldMedicalReceipt.RemoveMedicine(this);
                    }
                    if (value != null)
                    {
                        this.medicalReceipt = value;
                        this.medicalReceipt.AddMedicine(this);
                    }
                }
            }
        }

        public override string ToString()
        {
            return MedicineData.Name;
        }


    }
}
