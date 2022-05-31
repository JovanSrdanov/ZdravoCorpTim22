using Model;
using Repository;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service.Generic;

namespace Service
{
    public class DoctorService : GenericService<DoctorRepository, Doctor>
    {
        private static DoctorService instance;
        private DoctorService() : base(DoctorRepository.Instance) { }
        public static DoctorService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorService();
                }

                return instance;
            }
        }

        public bool isDoctorRegular(Doctor doctor)
        {
            DoctorSpecialization doctorSpecializationTemp = new DoctorSpecialization("Regular");
            return doctor.DoctorSpecialization.Name.Equals(doctorSpecializationTemp.Name);
        }

        //DODAO
        public bool doctorHasMedicalRecord(Doctor doctor, Patient patient)
        {
            bool returnValue = false;
            foreach (MedicalRecord temp in doctor.MedicalRecord)
            {
                if (temp.Id == patient.MedicalRecord.Id)
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }
        //DODAO
    }
}