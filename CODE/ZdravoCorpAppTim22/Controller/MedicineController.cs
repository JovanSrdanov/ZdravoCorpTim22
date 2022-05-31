using System.Collections.Generic;
using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service;

namespace ZdravoCorpAppTim22.Controller
{
    public class MedicineController : GenericController<MedicineService, Medicine>
    {
        private static MedicineController instance;
        private MedicineController() : base(MedicineService.Instance) { }
        public static MedicineController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicineController();
                }
                return instance;
            }
        }

        public void Create(string name, int amount, List<Ingredient> ingredients, List<MedicineData> replacements)
        {
            MedicineData medicineData = MedicineDataController.Instance.GetByName(name);
            if (medicineData == null)
            {
                medicineData = new MedicineData(-1, name);
                Approval approval = new Approval
                {
                    IsApproved = false
                };
                medicineData.Approval = approval;
                MedicineDataController.Instance.Create(medicineData);
                ApprovalController.Instance.Create(approval);

                MedicineService.Instance.Create(medicineData, amount, ingredients, replacements);
            }
        }

        public void Update(Medicine oldMedicine, string newName, int newAmount, List<Ingredient> ingredients, List<MedicineData> replacements)
        {
            oldMedicine.MedicineData.Name = newName;
            oldMedicine.Amount = newAmount;
            oldMedicine.MedicineData.Approval = new Approval()
            {
                Id = oldMedicine.MedicineData.Approval.Id,
                IsApproved = false
            };
            MedicineService.Instance.Update(oldMedicine, ingredients, replacements);
        }

        public List<Medicine> GetAllFree()
        {
            return MedicineService.Instance.GetAllFree();
        }
        
        public List<Medicine> GetAllApproved()
        {
            return MedicineService.Instance.GetAllApproved();
        }
    }
}
