using Model;
using Repository;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service.Generic;

namespace Service
{
    public class DoctorService : GenericService<DoctorRepository, Doctor>
    {
        private DoctorService() : base(DoctorRepository.Instance) { }
        public static DoctorService Instance
        {
            get
            {
                return new DoctorService();
            }
        }

        public bool isDoctorRegular(Doctor doctor)
        {
            DoctorSpecialization doctorSpecializationTemp = new DoctorSpecialization("Regular");
            return doctor.DoctorSpecialization.Name.Equals(doctorSpecializationTemp.Name);
        }

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
    }
}