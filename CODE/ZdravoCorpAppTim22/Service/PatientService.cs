using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ZdravoCorpAppTim22;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service.Generic;
using ZdravoCorpAppTim22.View.PatientView;

namespace Service
{
    public class PatientService : GenericService<PatientRepository, Patient>
    {
        private static PatientService instance;
        private PatientService() : base(PatientRepository.Instance) { }
        public static PatientService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PatientService();
                }

                return instance;
            }
        }

        public Patient GetPatient(Patient patient)
        {
            return PatientRepository.Instance.GetPatient(patient);
        }

        public void TherapyNotification()
        {
            if (App.Current != null && ZdravoCorpTabs.LoggedPatient != null)
                App.Current.Dispatcher.Invoke(delegate
                {
                    MedicalRecord medicalRecord = ZdravoCorpTabs.LoggedPatient.medicalRecord;

                    if (medicalRecord == null) return;
                    List<MedicalReceipt> MedicalReceipts = medicalRecord.MedicalReceipt;
                    CheckingUnfinishedTherapies(MedicalReceipts);
                });
        }

        private static void CheckingUnfinishedTherapies(List<MedicalReceipt> MedicalReceipts)
        {
            foreach (var medicalReceipt in MedicalReceipts.Where(medicalReceipt =>
                         !CheckIfTherapyIsOver(medicalReceipt)))
            {
                if (CheckIfTimeForMessage(medicalReceipt))
                {
                    string message = CreatingMessageForTherapy(medicalReceipt);
                    // ovdde dodaj neki event sta ja znam
                    MessageBox.Show(message);
                    UpdateNotifyNextDateTime(medicalReceipt);
                }

                UpdateMissedNotification(medicalReceipt);
            }
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
            ZdravoCorpTabs.LoggedPatient = null;
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
    }
}