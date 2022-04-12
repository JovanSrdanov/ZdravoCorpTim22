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
    public class DoctorFileHandler
    {
        private Serializer<List<Doctor>> serializer;

        public DoctorFileHandler(string Filename)
        {
            serializer = new Serializer<List<Doctor>>(Filename);
        }

        public void SaveData(List<Doctor> doctors)
        {
            foreach(Doctor doctor in doctors)
            {
                if(doctor.Address != null)
                {
                    doctor.addressID = doctor.address.ID;
                }
                else
                {
                    doctor.addressID = -1;
                }
            }
            serializer.Serialize(doctors);
        }

        public List<Doctor> LoadData()
        {
            List<Doctor> doctors = serializer.Deserialize();
            if (doctors == null)
            {
                return new List<Doctor>();
            }
            foreach (Doctor doctor in doctors)
            {
                if (doctor.addressID != -1)
                {
                    Address ad = AddressController.Instance.GetByID(doctor.addressID);
                    if (ad != null)
                    {
                        doctor.Address = ad;
                    }
                    else
                    {
                        doctor.addressID = -1;
                    }
                }
            }
            return doctors == null? new List<Doctor>() : doctors;
        }
    }
}
