using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace ZdravoCorpAppTim22.Repository.FileHandlers
{
    public class PatientFileHandler
    {
        private Serializer<List<Patient>> serializer;
        public PatientFileHandler(string Filename)
        {
            serializer = new Serializer<List<Patient>>(Filename);
        }
        public void SaveData(List<Patient> patients)
        {
            serializer.Serialize(patients);
        }

        public List<Patient> LoadData()
        {
            List<Patient> patients = serializer.Deserialize();
            return patients == null ? new List<Patient>() : patients;
        }
    }

    
}
