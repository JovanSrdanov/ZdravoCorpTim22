using Controller;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Model;
using MVVM1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Navigation;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.ViewModels
{
    public class OpenReportViewModel : ViewModel
    {
        //POLJA
        private readonly NavigationService navigationService;

        //PROPERTY
        public MedicalReportViewModel SelectedMedicalReportViewModel { get; set; }
        public DrugsViewModel SelectedMedicineViewModel { get; set; }

        public string ReportDate { get; set; }
        public string ReportPatientName { get; set; }
        public string ReportPatientSurname { get; set; }
        public string ReportDiagnosis { get; set; }
        public string ReportAnamnesis { get; set; }
        public string MedicineAmount { get; set; }
        public string ReportEndDate { get; set; }
        public string ReportEndTime { get; set; }
        public string ReportAdditionalInstructions { get; set; }
        public string ReportPurpose { get; set; }
        public string OldDiagnosis { get; set; }
        public DateTime EndDate { get; set; }
        public string EndTime { get; set; }

        public bool CanChangeData { get; set; }

        public ObservableCollection<DrugsViewModel> ApprovedMedicine { get; set; }

        public Medicine ChangableMedicine { get; set; }

        //KONSTRUKTOR
        public OpenReportViewModel(System.Windows.Navigation.NavigationService navigationService, MedicalReportViewModel selectedMedicalReportViewModel)
        {
            this.navigationService = navigationService;
            SelectedMedicalReportViewModel = selectedMedicalReportViewModel;

            ApprovedMedicine = new ObservableCollection<DrugsViewModel>(GetAllApprovedMedicine());
            SelectedMedicineViewModel = (DrugsViewModel)ApprovedMedicine.Where(r => r.Medicine.MedicineData.Id ==
            SelectedMedicalReportViewModel.MedicalReceipt.Medicine[0].MedicineData.Id).FirstOrDefault();

            ChangableMedicine = SelectedMedicalReportViewModel.MedicalReceipt.Medicine[0];      //ovo je pravi selected medicine

            ReportDate = SelectedMedicalReportViewModel.ReportDate.ToShortDateString();
            ReportPatientName = SelectedMedicalReportViewModel.MedicalReport.MedicalRecord.Patient.Name;
            ReportPatientSurname = SelectedMedicalReportViewModel.MedicalReport.MedicalRecord.Patient.Surname;
            ReportDiagnosis = SelectedMedicalReportViewModel.Diagnosis;
            ReportAnamnesis = SelectedMedicalReportViewModel.Anamnesis;
            MedicineAmount = ChangableMedicine.Amount.ToString();
            ReportPurpose = SelectedMedicalReportViewModel.MedicalReceipt.TherapyPurpose;
            ReportAdditionalInstructions = SelectedMedicalReportViewModel.MedicalReceipt.AdditionalInstructions;
            EndDate = SelectedMedicalReportViewModel.MedicalReceipt.EndDate;
            EndTime = SelectedMedicalReportViewModel.MedicalReceipt.Time;

            OldDiagnosis = SelectedMedicalReportViewModel.Diagnosis;

            SetAbilityToChangeReport();
            ConfirmCommand = new MyICommand(ExecuteConfirmCommand, CanExecuteConfirmCommand);
            CancelCommand = new MyICommand(ExecuteCancelCommand);
            PrintCommand = new MyICommand(ExecutePrintCommand);
        }

        //POMOCNE FUNKCIJE
        private void SetAbilityToChangeReport()
        {
            if (SelectedMedicalReportViewModel.MedicalReport.DoctorId !=
                (AuthenticationController.Instance.GetLoggedUser() as Doctor).Id)
            {
                CanChangeData = false;
            }
            else CanChangeData = true;
        }

        private List<DrugsViewModel> GetAllApprovedMedicine()
        {
            List<DrugsViewModel> tempList = new List<DrugsViewModel>();
            foreach (var item in MedicineController.Instance.GetAllApproved())
            {
                tempList.Add(new DrugsViewModel(item));
            }
            return tempList;
        }
        private bool isMedicineAmountValid()
        {
            bool returnValue = true;
#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                ChangableMedicine.Amount = Int32.Parse(MedicineAmount);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("The field 'Amount' can only be a number!", "Open report",
                MessageBoxButton.OK, MessageBoxImage.Warning);
                returnValue = false;
            }
#pragma warning restore CS0168 // Variable is declared but never used
            return returnValue;
        }
        private bool hasStorageEnoughMedicine(Medicine medicineInStorage)
        {
            bool returnValue = true;
            if (!(MedicineController.Instance.hasStorageEnoughMedicine(medicineInStorage, ChangableMedicine)))
            {
                System.Windows.MessageBox.Show("Selected amount excedes the amount located in the werehouse", "Open report",
                MessageBoxButton.OK, MessageBoxImage.Warning);
                returnValue = false;
            }
            return returnValue;
        }
        private void updateMedicalReport()
        {
            SelectedMedicalReportViewModel.MedicalReport.Anamnesis = ReportAnamnesis;
            SelectedMedicalReportViewModel.MedicalReport.Diagnosis = ReportDiagnosis;
            SelectedMedicalReportViewModel.Anamnesis = ReportAnamnesis;
            SelectedMedicalReportViewModel.Diagnosis = ReportDiagnosis;

            SelectedMedicalReportViewModel.MedicalReport.MedicalReceipt.AdditionalInstructions = ReportAdditionalInstructions;
            SelectedMedicalReportViewModel.MedicalReport.MedicalReceipt.TherapyPurpose = ReportPurpose;
            SelectedMedicalReportViewModel.MedicalReport.MedicalReceipt.Medicine[0] = SelectedMedicineViewModel.Medicine;
            SelectedMedicalReportViewModel.MedicalReport.MedicalReceipt.EndDate = EndDate;
            SelectedMedicalReportViewModel.MedicalReport.MedicalReceipt.Time = EndTime;
        }
        private void updateDiagnosis(MedicalRecord selectedPatientMedicalRecord)
        {
            foreach (string diagnosis in selectedPatientMedicalRecord.ConditionList)
            {
                if (diagnosis == OldDiagnosis)
                {
                    selectedPatientMedicalRecord.ConditionList
                        [selectedPatientMedicalRecord.ConditionList.IndexOf(diagnosis)] = ReportDiagnosis;
                    MedicalRecordController.Instance.Update(selectedPatientMedicalRecord);
                    RecordViewModel.SelectedPatientConditionHistory
                        [RecordViewModel.SelectedPatientConditionHistory.IndexOf(diagnosis)] = ReportDiagnosis;
                    break;
                }
            }
        }

        //KOMANDE 
        public MyICommand ConfirmCommand { get; set; }
        private void ExecuteConfirmCommand()
        {
            MedicineData selectedMedicineData = SelectedMedicineViewModel.MedicineData;
            SelectedMedicineViewModel.Medicine.MedicineData = selectedMedicineData;

            //NOVO
            ChangableMedicine.MedicineData = SelectedMedicineViewModel.MedicineData;


            if (!(isMedicineAmountValid())) return;

            Medicine medicineInStorage = MedicineController.Instance.GetAllFree().
                Where(r => r.MedicineData.Id == ChangableMedicine.MedicineData.Id).FirstOrDefault();
            if (!(hasStorageEnoughMedicine(medicineInStorage))) return;
            else
            {
                medicineInStorage.Amount -= ChangableMedicine.Amount;
                SelectedMedicineViewModel.Amount = medicineInStorage.Amount;
                SelectedMedicineViewModel.Medicine = ChangableMedicine;
                MedicineController.Instance.Update(medicineInStorage);
            }

            MedicalRecord selectedPatientMedicalRecord = RecordViewModel.SelectedPatient.MedicalRecord;
            updateMedicalReport();

            SelectedMedicalReportViewModel.MedicalReceipt.Medicine[0] = ChangableMedicine;

            ChangableMedicine.MedicalReceipt = SelectedMedicalReportViewModel.MedicalReceipt;
            MedicineController.Instance.Update(ChangableMedicine);
            SelectedMedicineViewModel.Medicine = ChangableMedicine;
            SelectedMedicineViewModel.MedicineData = ChangableMedicine.MedicineData;
            SelectedMedicineViewModel.Amount = ChangableMedicine.Amount;

            //NOVO
            MedicalReceipt tempReceipt = SelectedMedicalReportViewModel.MedicalReceipt;
            tempReceipt.Medicine[0] = ChangableMedicine; 
            MedicalReceiptController.Instance.Update(tempReceipt);
            MedicalReport tempReport = SelectedMedicalReportViewModel.MedicalReport;
            tempReport.MedicalReceipt = tempReceipt;
            MedicalReportController.Instance.Update(tempReport);

            updateDiagnosis(selectedPatientMedicalRecord);

            RecordViewModel.SelectedPatientMedicineHistory[RecordViewModel.SelectedPatientReportHistory.IndexOf
                (SelectedMedicalReportViewModel)] = SelectedMedicineViewModel;

            this.navigationService.GoBack();
        }

        private bool CanExecuteConfirmCommand()
        {
            return true;
        }

        public MyICommand CancelCommand { get; set; }
        private void ExecuteCancelCommand()
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Close window without saving?", "Create appointment",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    this.navigationService.GoBack();
                    break;
                case MessageBoxResult.No:

                    break;
            }
        }

        public MyICommand PrintCommand { get; set; }
        private void ExecutePrintCommand()
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "PDF file|*.pdf", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4.Rotate());
                    try
                    {
                        PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
                        doc.Open();

                        BaseFont baseFont = BaseFont.CreateFont("c:\\WINDOWS\\fonts\\times.ttf", BaseFont.IDENTITY_H, true);
                        iTextSharp.text.Font titleFont = new iTextSharp.text.Font(baseFont);
                        string Header = "Zdravo Korporacija\nIzveštaj o anamnezi i receptu\n ";

                        iTextSharp.text.Paragraph title;
                        title = new iTextSharp.text.Paragraph(Header, titleFont);
                        title.Alignment = Element.ALIGN_CENTER;

                        doc.Add(title);

                        PdfPTable table = new PdfPTable(2);
                        PdfPCell cell = new PdfPCell(new Phrase("Izveštaj"));

                        cell.Colspan = 2;

                        cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right

                        table.AddCell(cell);

                        table.AddCell("Report date");
                        table.AddCell(ReportDate);
                        table.AddCell("Patient name");
                        table.AddCell(ReportPatientName);
                        table.AddCell("Patient surname");
                        table.AddCell(ReportPatientSurname);
                        table.AddCell("Anamnesis");
                        table.AddCell(ReportAnamnesis);
                        table.AddCell("Diagnosis");
                        table.AddCell(ReportDiagnosis);
                        table.AddCell("Medication name");
                        table.AddCell(SelectedMedicineViewModel.DrugName);
                        table.AddCell("Perscription reason");
                        table.AddCell(ReportPurpose);
                        table.AddCell("Medication amount");
                        table.AddCell(MedicineAmount);
                        table.AddCell("Use medication until");
                        table.AddCell(EndDate.ToShortDateString());
                        table.AddCell("Take medication at");
                        table.AddCell(EndTime);
                        table.AddCell("Additional instructions");
                        table.AddCell(ReportAdditionalInstructions);

                        doc.Add(table);
                    }
                    catch
                    {

                    }
                    finally
                    {

                        doc.Close();
                    }
                }
            }
        }
    }
}
