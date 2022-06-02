using System.Collections.Generic;
using System.Linq;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class MedicineService : GenericService<MedicineRepository, Medicine>
    {
        private static MedicineService instance;
        private MedicineService() : base(MedicineRepository.Instance) { }
        public static MedicineService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicineService();
                }
                return instance;
            }
        }

        public void DeleteByList(List<Medicine> list)
        {
            foreach (Medicine m in list)
            {
                DeleteByID(m.Id);
            }
        }

        public void Create(MedicineData medicineData, int amount, List<Ingredient> ingredients, List<MedicineData> replacements)
        {
            foreach (Ingredient ingredient in ingredients)
            {
                ingredient.MedicineData = medicineData;
                IngredientService.Instance.Create(ingredient);
            }

            MedicineDataService.Instance.AddReplacements(medicineData, replacements);

            Medicine medicine = new Medicine
            {
                MedicineData = medicineData,
                Amount = amount
            };

            MedicineDataService.Instance.Update(medicineData);
            Create(medicine);
        }

        public void Update(Medicine medicine, List<Ingredient> ingredients, List<MedicineData> replacements)
        {
            IngredientService.Instance.AddIngredientsToMedicineData(medicine.MedicineData, ingredients);
            ReplacementService.Instance.AddReplacementsToMedicineData(medicine.MedicineData, replacements);

            ApprovalService.Instance.Update(medicine.MedicineData.Approval);
            MedicineDataService.Instance.Update(medicine.MedicineData);
            Update(medicine);
        }

        public List<Medicine> GetAllFree()
        {
            List<Medicine> list = new List<Medicine>();
            foreach (Medicine m in GetAll())
            {
                if (m.MedicalReceipt == null)
                {
                    list.Add(m);
                }
            }
            return list;
        }

        public List<Medicine> GetAllApproved()
        {
            List<Medicine> list = new List<Medicine>();
            foreach (Medicine m in GetAllFree())
            {
                if (m.MedicineData.Approval.IsApproved)
                {
                    list.Add(m);
                }
            }
            return list;
        }

        ///STEFAN DODAO
        public bool hasStorageEnoughMedicine(Medicine medicineInStorage, Medicine requestedMedicine)
        {
            bool returnValue = true;
            if (medicineInStorage.Amount - requestedMedicine.Amount < 0)
            {
                returnValue = false;
            }
            return returnValue;
        }
        ///STEFAN DODAO
    }
}
