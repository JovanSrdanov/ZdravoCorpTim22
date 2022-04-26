using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Repository.FileHandlers;
using ZdravoCorpAppTim22.Repository.Generic;

namespace Repository
{
    public class MedicalReportRepository : IRepository<MedicalReport>
    {
        public string Filename = "MedicalReportData.json";
        GenericFileHandler<MedicalReport> medicalReportFileHandler;

        List<MedicalReport> medicalReports = new List<MedicalReport>();

        private static MedicalReportRepository instance;

        private MedicalReportRepository()
        {
            medicalReportFileHandler = new GenericFileHandler<MedicalReport>(Filename);
        }

        public static MedicalReportRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicalReportRepository();
                }

                return instance;
            }
        }

        public void Create(Model.MedicalReport medicalReport)
        {
            if (medicalReports.Count > 0)
            {
                medicalReport.Id = medicalReports[medicalReports.Count - 1].Id + 1;
            }
            else
            {
                medicalReport.Id = 0;
            }
            this.medicalReports.Add(medicalReport);
            medicalReportFileHandler.SaveData(medicalReports);
        }

        public void DeleteByID(int id)
        {
            int index = medicalReports.FindIndex(r => r.Id == id);
            medicalReports.RemoveAt(index);
            medicalReportFileHandler.SaveData(medicalReports);
        }

        public List<MedicalReport> GetAll()
        {
            return this.medicalReports;
        }

        public MedicalReport GetByID(int id)
        {
            int index = medicalReports.FindIndex(r => r.Id == id);
            return medicalReports[index];
        }

        public void Load()
        {
            medicalReports = medicalReportFileHandler.LoadData();
        }

        public void Update(Model.MedicalReport medicalReport)
        {
            int index = medicalReports.FindIndex(r => r.Id == medicalReport.Id);
            medicalReports[index] = medicalReport;
            medicalReportFileHandler.SaveData(medicalReports);
        }
    }
}
