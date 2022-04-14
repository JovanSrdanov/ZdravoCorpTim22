using Controller;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace ZdravoCorpAppTim22.Repository.FileHandlers
{
    internal class MedicalAppointmentFileHandler
    {
        private Serializer<List<MedicalAppointment>> serializer;

        public MedicalAppointmentFileHandler(string filename)
        {
            serializer = new Serializer<List<MedicalAppointment>>(filename);
        }

        public void SaveData(List<MedicalAppointment> medicalAppointments)
        {
            serializer.Serialize(medicalAppointments);
        }

        public List<MedicalAppointment> LoadData()
        {
            List <MedicalAppointment> medicalAppointments = serializer.Deserialize();
            return medicalAppointments == null ? new List<MedicalAppointment>() : medicalAppointments;
        }
    }
}
