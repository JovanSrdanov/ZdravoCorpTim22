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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZdravoCorpAppTim22.View.PatientView.ViewsForMVVM
{
    /// <summary>
    /// Interaction logic for ReportReviewViewxaml.xaml
    /// </summary>
    public partial class ReportReviewViewxaml : UserControl
    {
        public ReportReviewViewxaml()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
          

        }
    }
}
