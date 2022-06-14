using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms.DataVisualization;

using ZdravoCorpAppTim22.Controller;
using Model;

namespace ZdravoCorpAppTim22.View.Secretary
{
    public partial class SecretaryGraph : Window
    {
        SecretaryMedicalOrder secretaryMedicalOrder;
        public SecretaryGraph(SecretaryMedicalOrder secretaryMedicalOrder)
        {
            InitializeComponent();
            this.secretaryMedicalOrder = secretaryMedicalOrder;
        }

        private void rbColumn_Click(object sender, RoutedEventArgs e)
        {
            switch(((RadioButton)sender).Name)
            {
                case "RbPie":
                    Chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                    break;
                case "rbColumn":
                    Chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    break;
                case "rbLine":
                    Chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    break;
                case "rbBar":
                    Chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
                    break;
            }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox.SelectedIndex == 0)
            {
                Chart1.Series[0].Points.Clear();
                Chart1.Series[0].Points.Add(DoctorController.Instance.GetAll().Count).AxisLabel = "Doctors";
                Chart1.Series[0].Points.Add(SecretaryController.Instance.GetAll().Count).AxisLabel = "Secretaries";
                Chart1.Series[0].Points.Add(ManagerController.Instance.GetAll().Count).AxisLabel = "Managers";


            }
            else
            {
                List<Equipment> equipment = EquipmentController.Instance.GetWarehouseEquipment();
                Chart1.Series[0].Points.Clear();
                foreach (var eq in equipment)
                {

                    Chart1.Series[0].Points.Add(eq.Amount).AxisLabel = eq.equipmentData.Name;
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            secretaryMedicalOrder.Show();
        }
    }
}
