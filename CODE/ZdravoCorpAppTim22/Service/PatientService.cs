using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
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

            if (PatientSelectionForTemporaryPurpose.LoggedPatient == null)
            {
                return;
            }
            else
            {
                if (App.Current != null)
                {
                    App.Current.Dispatcher.Invoke(delegate
                    {
                        MedicalRecord medRec = MedicalRecordService.Instance.GetAll().Where(r => r.Patient.Id == PatientSelectionForTemporaryPurpose.LoggedPatient.Id).FirstOrDefault();

                        if (medRec == null)
                        {
                            return;
                        }
                        List<MedicalReceipt> MedicalReceipts = medRec.MedicalReceipt;

                        List<int> IdsForRemoving = new List<int>();
                        foreach (MedicalReceipt medicalReceipt in MedicalReceipts)
                        {
                            if (DateTime.Now.Date > medicalReceipt.EndDate.Date)
                            {
                                MessageBox.Show("Terapija je zavrsena!");
                                return;
                            }
                            else
                            {
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
                                    message += "\n\n";

                                    MessageBox.Show(message);

                                    int hour = medicalReceipt.NotifyNextDateTime.Hour;
                                    int minute = medicalReceipt.NotifyNextDateTime.Minute;

                                    medicalReceipt.NotifyNextDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Day, DateTime.Now.Month, hour, minute, 0);
                                    medicalReceipt.NotifyNextDateTime = medicalReceipt.NotifyNextDateTime.AddDays(1);
                                    MedicalReceiptController.Instance.Update(medicalReceipt);
                                }
                                if (DateTime.Now > medicalReceipt.NotifyNextDateTime)
                                {
                                    int hour = medicalReceipt.NotifyNextDateTime.Hour;
                                    int minute = medicalReceipt.NotifyNextDateTime.Minute;

                                    medicalReceipt.NotifyNextDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Day, DateTime.Now.Month, hour, minute, 0);
                                    medicalReceipt.NotifyNextDateTime = medicalReceipt.NotifyNextDateTime.AddDays(1);
                                    MedicalReceiptController.Instance.Update(medicalReceipt);
                                }
                            }
                        }
                    });
                }
            }
        }
    }
}