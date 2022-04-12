using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace ZdravoCorpAppTim22.Repository.FileHandlers
{
    internal class DoctorFileHandler
    {
        private Serializer<List<Doctor>> serializer;

        public DoctorFileHandler(String Filename)
        {
            serializer = new Serializer<List<Doctor>>(Filename);
        }

        public void SaveData(List<Doctor> doctors)
        {
            serializer.Serialize(doctors);
        }

        public List<Doctor> LoadData()
        {
            List<Doctor> doctors = serializer.Deserialize();
            return doctors == null? new List<Doctor>() : doctors;
        }
    }
}
