using System.Collections.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository.Generic;

namespace ZdravoCorpAppTim22.Repository
{
    public class ReplacementRepository : GenericRepository<Replacement>
    {
        public static string FileName = "ReplacementData.json";
        private static ReplacementRepository instance;
        private ReplacementRepository() : base(FileName) { }
        public static ReplacementRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ReplacementRepository();

                }

                return instance;
            }
        }
        public override void Load()
        {
            base.Load();
            List<Replacement> replacementsToRemove = new List<Replacement>();
            foreach (Replacement replacement in GetAll())
            {
                MedicineData replacingMedicine = MedicineDataRepository.Instance.GetByID(replacement.ReplacingMedicine.Id);
                MedicineData replacementToMedicine = MedicineDataRepository.Instance.GetByID(replacement.ReplacementToMedicine.Id);
                if (replacingMedicine != null && replacementToMedicine != null)
                {
                    replacementToMedicine.AddReplacement(replacingMedicine);
                    //replacingMedicine.ReplacementTo = replacementToMedicine;
                }
                else
                {
                    replacementsToRemove.Add(replacement);
                }
            }
            foreach (Replacement replacement in replacementsToRemove)
            {
                List.Remove(replacement);
            }
        }
    }
}
