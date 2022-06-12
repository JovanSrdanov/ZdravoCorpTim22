using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Forms;
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

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Patient patient = (Patient)AuthenticationController.Instance.GetLoggedUser();
            DateTime StartDate = (DateTime)StartTherapyReport.SelectedDate;
            DateTime EndDate = StartDate.AddDays(7);

            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "PDF file|*.pdf", ValidateNames = true })
            {

                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4.Rotate());
                    try
                    {
                        PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
                        doc.Open();

                        PdfPTable table = new PdfPTable(7);

                        PdfPCell cell = new PdfPCell(new Phrase("Izvestaj"));

                        cell.Colspan = 7;

                        cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right

                        table.AddCell(cell);

                        table.AddCell("Dan uzmianja");
                        table.AddCell("ID");
                        table.AddCell("Svrha terapije");
                        table.AddCell("Dodatna upustva");
                        table.AddCell("Lekovi");
                        table.AddCell("Vreme uzimanja");
                        table.AddCell("Kraj terapije");

                        var medicalReceipts = patient.medicalRecord.medicalReceipt;

                        for (; StartDate.Date <= EndDate.Date; StartDate = StartDate.Date.AddDays(1))
                        {
                            foreach (MedicalReceipt medicalReceipt in medicalReceipts)
                            {
                                if (medicalReceipt.EndDate.Date >= StartDate.Date)
                                {
                                    table.AddCell(StartDate.ToString("dd-MM-yyyy"));
                                    table.AddCell(medicalReceipt.Id.ToString());
                                    table.AddCell(medicalReceipt.TherapyPurpose.ToString());
                                    table.AddCell(medicalReceipt.AdditionalInstructions.ToString());
                                    string medications = "";
                                    if(medicalReceipt.medicine!=null)
                                    foreach (var med in medicalReceipt.medicine)
                                    {
                                        medications = medications + med.MedicineData.Name + "\n";

                                    }


                                    table.AddCell(medications);
                                    table.AddCell(medicalReceipt.Time);
                                    table.AddCell(medicalReceipt.EndDate.ToString("dd-MM-yyyy"));

                                }
                            }

                        }

                        doc.Add(table);



                    }
                    catch
                    {

                    }
                    finally
                    {
                       
                        doc.Close();

                        ViewPdf viewPdf = new ViewPdf(sfd.FileName);

                        viewPdf.ShowDialog();
                       
                      
                    }


                }


            }
            {
               
              
            }


        }
    }
}
