using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoCorpAppTim22;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service.Generic;
using ZdravoCorpAppTim22.View.PatientView;

namespace Service
{
    public class PatientService : GenericService<PatientRepository, Patient>
    {
        private PatientService() : base(PatientRepository.Instance) { }
        public static PatientService Instance
        {
            get
            {
                return new PatientService();
            }
        }

        public Patient GetPatient(Patient patient)
        {
            return PatientRepository.Instance.GetPatient(patient);
        }

        public string TherapyNotification()
        {
            string returnMessage = "";
            if (IsPatientLoggedIn() )
            {
                if (((Patient)AuthenticationController.Instance.GetLoggedUser()).MedicalRecord == null) return "";
                returnMessage = CheckingUnfinishedTherapies(((Patient)AuthenticationController.Instance.GetLoggedUser()).MedicalRecord.MedicalReceipt);
            }

            return returnMessage;
        }

        private static bool IsPatientLoggedIn()
        {
            return App.Current != null && (AuthenticationController.Instance.GetLoggedUser()!=null) && ( AuthenticationController.Instance.GetLoggedUser().GetType() == typeof(Patient));
        }


        private string CheckingUnfinishedTherapies(List<MedicalReceipt> MedicalReceipts)
        {
            string message = "";
            foreach (var medicalReceipt in MedicalReceipts.Where(medicalReceipt =>
                         !CheckIfTherapyIsOver(medicalReceipt)))
            {
                if (CheckIfTimeForMessage(medicalReceipt))
                {
                    message = CreatingMessageForTherapy(medicalReceipt);
                   
                    UpdateNotifyNextDateTime(medicalReceipt);
                   
                }

                UpdateMissedNotification(medicalReceipt);
            }
            return message;
        }

        private static void UpdateMissedNotification(MedicalReceipt medicalReceipt)
        {
            if (DateTime.Now > medicalReceipt.NotifyNextDateTime)
            {
                UpdateNotifyNextDateTime(medicalReceipt);
            }
        }

        private static bool CheckIfTherapyIsOver(MedicalReceipt medicalReceipt)
        {
            return DateTime.Now.Date > medicalReceipt.EndDate.Date;
        }

        private static bool CheckIfTimeForMessage(MedicalReceipt medicalReceipt)
        {
            return DateTime.Now > medicalReceipt.NotifyNextDateTime.AddMinutes(-Constants.Constants.NOTIFICATION_TIME_START) && DateTime.Now < medicalReceipt.NotifyNextDateTime.AddMinutes(-Constants.Constants.NOTIFICATION_TIME_END);
        }

        private static void UpdateNotifyNextDateTime(MedicalReceipt medicalReceipt)
        {
            int hour = medicalReceipt.NotifyNextDateTime.Hour;
            int minute = medicalReceipt.NotifyNextDateTime.Minute;

            medicalReceipt.NotifyNextDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day,
                hour, minute, 0);
            MedicalReceiptController.Instance.Update(medicalReceipt);
        }

        private static string CreatingMessageForTherapy(MedicalReceipt medicalReceipt)
        {
            string message = "Podsetnik za terapiju:\n\nSvrha terapije: ";
            message += medicalReceipt.TherapyPurpose;
            message += "\n\n";

            message += "Način upotrebe: ";
            message += medicalReceipt.AdditionalInstructions;
            message += "\n\n";

            message += "Lekovi: ";
            foreach (Medicine m in medicalReceipt.Medicine)
            {
                message += m.MedicineData.Name;
                message += "\n\n";
            }

            message += "Uzeti u: ";
            message += medicalReceipt.Time;
            return message;
        }

        public bool AntiTroll(Patient patient)
        {

            UpdatingSuspiciousActivity(patient);
            bool Troll = false;

            if (CheckIfTroll(patient))
            {
                Blocking(patient);
                Troll = true;
            }

            return Troll;

        }

        private static void UpdatingSuspiciousActivity(Patient patient)
        {
            RemovingOutdatedSuspiciousActivity(patient);
            patient.SuspiciousActivity.Add(DateTime.Now);
            Instance.Update(patient);
        }

        private static bool CheckIfTroll(Patient patient)
        {
            return patient.SuspiciousActivity.Count >= Constants.Constants.MAX_SUSPICIOUS_ACTIVITY_COUNT;
        }

        private static void Blocking(Patient patient)
        {
            patient.Blocked = true;
            Instance.Update(patient);
            AuthenticationController.Instance.Logout();

        }

        private static void RemovingOutdatedSuspiciousActivity(Patient patient)
        {
            foreach (var dateTime in patient.SuspiciousActivity.ToList()
                         .Where(dateTime => dateTime < DateTime.Now.AddDays(-Constants.Constants.SUSPICIOUS_ACTIVITY_DAYS_RANGE)))
            {
                patient.SuspiciousActivity.Remove(dateTime);
            }
        }

        public void checkIfPatientHasMedicalRecord(Patient patient)
        {
            MedicalRecord patientMedicalRecord = patient.MedicalRecord;
            if (patientMedicalRecord == null)
            {
                patientMedicalRecord = new MedicalRecord(-1, BloodType.A_PLUS, patient,
                    new List<string>(), new List<string>());
                MedicalRecordRepository.Instance.Create(patientMedicalRecord);
                patient.MedicalRecord = patientMedicalRecord;
            }
        }
    }
}