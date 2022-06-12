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

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM
{
    /// <summary>
    /// Interaction logic for ViewPdf.xaml
    /// </summary>
    public partial class ViewPdf : Window
    {
        public ViewPdf(string fileName)
        {
            InitializeComponent();
            pdfWebViewer.Navigate(fileName);
        }
    }
}
