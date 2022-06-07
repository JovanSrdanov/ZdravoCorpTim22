using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.View.Manager.Commands;
using ZdravoCorpAppTim22.View.Manager.Pages.MedicinePages;
using ZdravoCorpAppTim22.View.Manager.Views;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels.MedicineViewModels
{
    public class MedicineViewModel 
    {
        public RelayCommand OpenAddCommand { get; private set; }
        public RelayCommand OpenEditCommand { get; private set; }
        public RelayCommand DeleteMedicineCommand { get; private set; }
        public RelayCommand OpenDetailsCommand { get; private set; }
        public RelayCommand GenerateReportCommand { get; private set; }

        public ObservableCollection<Medicine> MedicineCollection { get; set; }

        public MedicineViewModel()
        {
            OpenAddCommand = new RelayCommand(OpenAdd);
            OpenEditCommand = new RelayCommand(OpenEdit, IsSelected);
            DeleteMedicineCommand = new RelayCommand(DeleteMedicine, IsSelected);
            OpenDetailsCommand = new RelayCommand(OpenDetails, CanOpenDetails);
            GenerateReportCommand = new RelayCommand(GenerateReport);

            MedicineCollection = new ObservableCollection<Medicine>(MedicineController.Instance.GetAllFree());
        }

        public void OpenAdd(object obj)
        {
            ManagerHome.NavigationService.Navigate(new AddMedicineView());
        }
        
        public void OpenEdit(object obj)
        {
            Medicine medicine = (Medicine)obj;
            EditMedicineView editMedicine = new EditMedicineView(medicine);
            ManagerHome.NavigationService.Navigate(editMedicine);
        }

        public void DeleteMedicine(object obj)
        {
            string msg = "Are you sure?";
            if (ManagerHome.CurrentLanguage == 1)
            {
                msg = "Da li ste sigurni?";
            }
            if (ConfirmModal.Show(msg))
            {
                Medicine medicine = (Medicine)obj;
                MedicineCollection.Remove(medicine);

                MedicineDataController.Instance.DeleteByID(medicine.MedicineData.Id);
            }
        }

        public void OpenDetails(object obj)
        {
            MedicineDetailsModal.Show((Medicine)obj);
        }

        public void GenerateReport(object obj)
        {
            PdfDocument doc = new PdfDocument();
            PdfPage page = doc.Pages.Add();

            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 16);
            string txt;

            PdfLightTable pdfLightTable = new PdfLightTable();
            DataTable table = new DataTable();
            if(ManagerHome.CurrentLanguage == 1)
            {
                txt = "Izvještaj Lijekova";
                table.Columns.Add("ID");
                table.Columns.Add("Naziv");
                table.Columns.Add("Količina");
                table.Columns.Add("Pregledao");
                table.Columns.Add("Odobreno");
            }
            else
            {
                txt = "Medicine Report";
                table.Columns.Add("ID");
                table.Columns.Add("Name");
                table.Columns.Add("Amount");
                table.Columns.Add("Reviewed By");
                table.Columns.Add("Approved");
            }
            
            foreach(Medicine m in MedicineCollection)
            {
                string id = m.MedicineData.Id.ToString();
                string name = m.MedicineData.Name;
                string amount = m.Amount.ToString();
                string reviewedBy = "";
                if(m.MedicineData.Approval.Doctor != null)
                {
                    reviewedBy = m.MedicineData.Approval.Doctor.Name;
                }
                string approved = m.MedicineData.Approval.IsApproved.ToString();
                table.Rows.Add(new string[] { id, name, amount, reviewedBy, approved });
            }

            PdfHTMLTextElement richTextElement = new PdfHTMLTextElement(txt, font, PdfBrushes.Black)
            {
                TextAlign = TextAlign.Center
            };
            PdfMetafileLayoutFormat format = new PdfMetafileLayoutFormat
            {
                Layout = PdfLayoutType.Paginate,
                Break = PdfLayoutBreakType.FitPage
            };
            richTextElement.Draw(page, new RectangleF(0, 20, page.GetClientSize().Width, page.GetClientSize().Height), format);

            pdfLightTable.DataSource = table;

            PdfLightTableStyle lightTableStyle = new PdfLightTableStyle
            {
                CellPadding = 5,
                CellSpacing = 0,
                ShowHeader = true
            };

            pdfLightTable.Style = lightTableStyle;
            pdfLightTable.Draw(page, new PointF(0, 75));

            doc.Save("../../Reports/MedicineReport.pdf");

            doc.Close(true);
        }

        private bool CanOpenDetails(object obj)
        {
            Medicine medicine = (Medicine)obj;
            if (medicine == null || medicine.MedicineData == null || medicine.MedicineData.Approval == null || medicine.MedicineData.Approval.Doctor == null)
            {
                return false;
            }
            return true;
        }

        private bool IsSelected(object obj)
        {
            Medicine medicine = (Medicine)obj;
            if (medicine == null || medicine.MedicineData == null)
            {
                return false;
            }
            return true;
        }
    }
}

