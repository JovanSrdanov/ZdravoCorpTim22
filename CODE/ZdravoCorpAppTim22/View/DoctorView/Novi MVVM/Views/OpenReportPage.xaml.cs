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
using ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.ViewModels;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;

namespace ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.Views
{
    /// <summary>
    /// Interaction logic for OpenReportPage.xaml
    /// </summary>
    public partial class OpenReportPage : Page
    {
        public OpenReportPage(OpenReportViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
