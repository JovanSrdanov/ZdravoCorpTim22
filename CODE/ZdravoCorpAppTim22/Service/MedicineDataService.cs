using System.Collections.Generic;
using System.Linq;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class MedicineDataService : GenericService<MedicineDataRepository, MedicineData>
    {
        private MedicineDataService() : base(MedicineDataRepository.Instance) { }
        public static MedicineDataService Instance
        {
            get
            {
                return new MedicineDataService();
            }
        }

        public MedicineData GetByName(string name)
        {
            foreach(MedicineData item in GetAll())
            {
                if (item.Name.Equals(name))
                {
                    return item;
                }
            }
            return null;
        }

        public override void DeleteByID(int id)
        {
            ClearAllReferences(id);
            base.DeleteByID(id);
        }

        public List<MedicineData> GetAllApproved()
        {
            List<MedicineData> list = new List<MedicineData>();
            foreach (MedicineData m in GetAll())
            {
                if (m.Approval != null && m.Approval.IsApproved == true)
                {
                    list.Add(m);
                }
            }
            return list;
        }
        public List<MedicineData> GetAllUnapproved()
        {
            List<MedicineData> list = new List<MedicineData>();
            foreach (MedicineData m in GetAll())
            {
                if (m.Approval == null || m.Approval.IsApproved == false)
                {
                    list.Add(m);
                }
            }
            return list;
        }

        public void AddReplacements(MedicineData medicineData, List<MedicineData> replacements)
        {
            foreach (MedicineData m in replacements)
            {
                medicineData.AddReplacement(m);
                Replacement replacement = new Replacement
                {
                    ReplacementToMedicine = medicineData,
                    ReplacingMedicine = m
                };
                ReplacementService.Instance.Create(replacement);
            }
        }

        #region private

        private void ClearAllReferences(int id)
        {
            MedicineData medicineData = GetByID(id);

            ClearIngredientReferences(medicineData);
            ClearMedicineReferences(medicineData);
            ClearReplacementReferences(medicineData);

            if (medicineData.Approval != null)
            {
                ApprovalService.Instance.DeleteByID(medicineData.Approval.Id);
            }
        }

        private void ClearIngredientReferences(MedicineData medicineData)
        {
            IngredientService.Instance.DeleteByMedicineData(medicineData);
        }

        private void ClearMedicineReferences(MedicineData medicineData)
        {
            List<Medicine> medicineToRemove = new List<Medicine>();
            foreach (Medicine m in MedicineService.Instance.GetAll())
            {
                if (m.MedicineData.Id == medicineData.Id)
                {
                    medicineToRemove.Add(m);
                }
            }
            MedicineService.Instance.DeleteByList(medicineToRemove);
        }

        private void ClearReplacementReferences(MedicineData medicineData)
        {
            foreach (MedicineData m in GetAll())
            {
                MedicineData temp = m.Replacements.Where(x => x.Id == medicineData.Id).FirstOrDefault();
                if (temp != null)
                {
                    m.RemoveReplacement(temp);
                }
            }
            ReplacementService.Instance.DeleteByMedicineData(medicineData);
        }
        #endregion
    }
}
