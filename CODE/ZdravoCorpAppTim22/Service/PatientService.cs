using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using System.Windows.Navigation;
using ZdravoCorpAppTim22;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service;
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
        
        public void DaemonMethod()
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
                                MessageBox.Show("Terapija je zavrsena!");
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
                                foreach(Medicine m in medicalReceipt.Medicine)
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

        public void AntiTroll(Patient patient,DateTime activity)
        {

            foreach (var dateTime in patient.SuspiciousActivity.ToList().Where(dateTime => dateTime < DateTime.Now.AddDays(-30)))
            {
                patient.SuspiciousActivity.Remove(dateTime);
            }
            
            patient.SuspiciousActivity.Add(activity);
            Instance.Update(patient);

            if (patient.SuspiciousActivity.Count >= 3)
            {
                
                patient.Blocked = true;
                Instance.Update(patient);

                MessageBox.Show("Pacijent je blokiran!");
                System.Windows.Forms.Application.Restart();
                System.Windows.Application.Current.Shutdown();
                
            }
        }
    }
}