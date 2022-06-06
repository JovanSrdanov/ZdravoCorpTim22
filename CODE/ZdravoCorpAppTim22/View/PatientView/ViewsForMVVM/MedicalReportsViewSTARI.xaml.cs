using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZdravoCorpAppTim22.View.PatientView.ViewsForMVVM
{
    /// <summary>
    /// Interaction logic for MedicalReportsViewSTARI.xaml
    /// </summary>
    public partial class MedicalReportsViewSTARI : UserControl
    {
        public MedicalReportsViewSTARI()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
            ReviewTheReport reviewTheReport = new ReviewTheReport();
            reviewTheReport.ShowDialog();

        }
    }
}
