using Controller;
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

            if (ZdravoCorpTabs.LoggedPatient == null)
            {
                return;
            }
            else
            {

                if (App.Current != null)
                {
                    App.Current.Dispatcher.Invoke(delegate
                    {
                        MedicalRecord medRec = ZdravoCorpTabs.LoggedPatient.medicalRecord;


                        if (medRec == null)
                        {
                            return;
                        }
                        List<MedicalReceipt> MedicalReceipts = medRec.MedicalReceipt;


                        foreach (MedicalReceipt medicalReceipt in MedicalReceipts)
                        {


                            if (DateTime.Now.Date > medicalReceipt.EndDate.Date)
                            {
                               
                                return;
                            }


                            if (DateTime.Now > medicalReceipt.NotifyNextDateTime.AddMinutes(-30) && DateTime.Now < medicalReceipt.NotifyNextDateTime.AddMinutes(-5))
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



                                MessageBox.Show(message);

                                int hour = medicalReceipt.NotifyNextDateTime.Hour;
                                int minute = medicalReceipt.NotifyNextDateTime.Minute;

                                medicalReceipt.NotifyNextDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, hour, minute, 0);
                                MedicalReceiptController.Instance.Update(medicalReceipt);
                            }
                            if (DateTime.Now > medicalReceipt.NotifyNextDateTime)
                            {
                                int hour = medicalReceipt.NotifyNextDateTime.Hour;
                                int minute = medicalReceipt.NotifyNextDateTime.Minute;

                                medicalReceipt.NotifyNextDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, hour, minute, 0);

                                MedicalReceiptController.Instance.Update(medicalReceipt);
                            }
                        }
                    });
                }
            }
        }

        public void AntiTroll(Patient patient)
        {

            RemovingOutdatedSuspiciousActivity(patient);
            patient.SuspiciousActivity.Add(DateTime.Now);
            Instance.Update(patient);
            if (CheckIfTroll(patient))
            {
                SanctioningTroll(patient);
            }
        }

        private static bool CheckIfTroll(Patient patient)
        {
            return patient.SuspiciousActivity.Count >= Constants.Constants.MAX_SUSPICIOUS_ACTIVITY_COUNT;
        }

        private static void SanctioningTroll(Patient patient)
        {
            Blocking(patient);
            TrollLogOut();
        }

        private static void Blocking(Patient patient)
        {
            patient.Blocked = true;
            Instance.Update(patient);
            ZdravoCorpTabs.LoggedPatient = null;
            MessageBox.Show("Pacijent je blokiran!");
        }

        private static void TrollLogOut()
        {
            AuthenticationController.Instance.Logout();
            List<Window> windows = Application.Current.Windows.Cast<Window>().Where(window => window.Visibility != Visibility.Hidden).ToList();
            foreach (Window window in windows)
            {
                window.Close();
            }


        }

        private static void RemovingOutdatedSuspiciousActivity(Patient patient)
        {
            foreach (var dateTime in patient.SuspiciousActivity.ToList()
                         .Where(dateTime => dateTime < DateTime.Now.AddDays(-Constants.Constants.SUSPICIOUS_ACTIVITY_DAYS_RANGE)))
            {
                patient.SuspiciousActivity.Remove(dateTime);
            }
        }

        ///STEFAN DODAO
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
        ///STEFAN DODAO
    }
}