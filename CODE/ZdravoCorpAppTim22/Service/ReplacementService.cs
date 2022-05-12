﻿using System.Collections.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class ReplacementService : GenericService<ReplacementRepository, Replacement>
    {
        private static ReplacementService instance;
        private ReplacementService() : base(ReplacementRepository.Instance) { }
        public static ReplacementService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ReplacementService();
                }
                return instance;
            }
        }
        
        public void DeleteMany(List<Replacement> list)
        {
            foreach (Replacement rel in list)
            {
                DeleteByID(rel.Id);
            }
        }
        public void DeleteByReplacementTarget(MedicineData medicineData)
        {
            List<Replacement> replacementsToRemove = new List<Replacement>();
            foreach (Replacement replacement in GetAll())
            {
                if (replacement.ReplacementToMedicine.Id == medicineData.Id)
                {
                    replacementsToRemove.Add(replacement);
                }
            }
            DeleteMany(replacementsToRemove);
        }

        public void DeleteByMedicineData(MedicineData medicineData)
        {
            List<Replacement> replacementsToRemove = new List<Replacement>();
            foreach (Replacement replacement in GetAll())
            {
                if (replacement.ReplacementToMedicine.Id == medicineData.Id || replacement.ReplacingMedicine.Id == medicineData.Id)
                {
                    replacementsToRemove.Add(replacement);
                }
            }
            DeleteMany(replacementsToRemove);
        }
    }
}
