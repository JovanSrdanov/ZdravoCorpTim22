using Controller;
using Model;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;
using System.Data;
using System.Drawing;
using System.Windows;

namespace ZdravoCorpAppTim22.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretaryGenerateReport.xaml
    /// </summary>
    public partial class SecretaryGenerateReport : Window
    {
        SecretaryHome secretaryHome;
        public SecretaryGenerateReport(SecretaryHome secretaryHome)
        {
            InitializeComponent();
            this.secretaryHome = secretaryHome;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (dateStart.SelectedDate == null || DateEnd.SelectedDate == null)
            {
                MessageBox.Show("Must select start and end date!");
                return;
            }
            GenerateReport();
        }

        public void GenerateReport()
        {
            PdfDocument doc = new PdfDocument();
            PdfPage page = doc.Pages.Add();

            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 16);
            string txt;

            PdfLightTable pdfLightTable = new PdfLightTable();
            DataTable table = new DataTable();

            txt = "Izvještaj zakazanih operacija i pregleda";
            table.Columns.Add("ID");
            table.Columns.Add("Tip");
            table.Columns.Add("Pocetak termina");
            table.Columns.Add("Kraj termina");
            table.Columns.Add("Hitan");


            foreach (MedicalAppointment m in MedicalAppointmentController.Instance.GetAll())
            {
                if (m.Interval.Start >= dateStart.SelectedDate && m.Interval.Start <= DateEnd.SelectedDate)
                {
                    string id = m.Id.ToString();
                    string type = m.Type.ToString();
                    string start = m.Interval.Start.ToString();
                    string end = m.Interval.End.ToString();
                    string urgent = m.isUrgent.ToString();

                    table.Rows.Add(new string[] { id, type, start, end, urgent });
                }
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

            doc.Save("../../Reports/SecretaryReport.pdf");

            doc.Close(true);
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            secretaryHome.Show();
        }
    }
}
