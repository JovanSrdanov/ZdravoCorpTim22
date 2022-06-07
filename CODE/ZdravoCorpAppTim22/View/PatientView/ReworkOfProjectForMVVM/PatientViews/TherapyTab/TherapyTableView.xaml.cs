using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Model;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.TherapyTab
{

    public partial class TherapyTableView : Page
    {
        public TherapyTableView()
        {
            InitializeComponent();
            Patient patient = (Patient)AuthenticationController.Instance.GetLoggedUser();
            ObservableCollection<TherapyViewModel> therapyViewModel = new ObservableCollection<TherapyViewModel>();
            if (patient.medicalRecord != null)
            {
                therapyViewModel = ConvertToMedicalReceiptViewModel(patient.medicalRecord.MedicalReceipt);
            }
            this.DataGridReciepts.ItemsSource = therapyViewModel;
        }

        public ObservableCollection<TherapyViewModel> ConvertToMedicalReceiptViewModel(List<MedicalReceipt> medicalReceipts)
        {
            ObservableCollection<TherapyViewModel> medicalReceiptsViewModel = new ObservableCollection<TherapyViewModel>();
            foreach (MedicalReceipt medicalReceipt in medicalReceipts)
            {
                TherapyViewModel therapyViewModel = new TherapyViewModel(medicalReceipt.Id, medicalReceipt.EndDate,
                    medicalReceipt.NotifyNextDateTime, medicalReceipt.Time, medicalReceipt.AdditionalInstructions,
                    medicalReceipt.TherapyPurpose);
                if (medicalReceipt.medicine != null)
                {
                    foreach (Medicine medicine in medicalReceipt.medicine)
                    {
                        therapyViewModel.MedicineName.Add(medicine.MedicineData.Name);
                    }

                    medicalReceiptsViewModel.Add(therapyViewModel);
                }
            }

            return medicalReceiptsViewModel;
        }
    }
}
