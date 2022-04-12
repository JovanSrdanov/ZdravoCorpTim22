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
            foreach (Patient patient in patients)
            {
                if (patient.Address != null)
                {
                    patient.addressID = patient.address.ID;
                }
                else
                {
                    patient.addressID = -1;
                }
            }
            serializer.Serialize(patients);
        }

        public List<Patient> LoadData()
        {
            List<Patient> patients = serializer.Deserialize();
            if (patients == null)
            {
                return new List<Patient>();
            }
            foreach (Patient patient in patients)
            {
                if (patient.addressID != -1)
                {
                    Address ad = AddressController.Instance.GetByID(patient.addressID);
                    if (ad != null)
                    {
                        patient.Address = ad;
                    }
                    else
                    {
                        patient.addressID = -1;
                    }
                }
            }
            return patients == null ? new List<Patient>() : patients;
        }
    }

    
}
