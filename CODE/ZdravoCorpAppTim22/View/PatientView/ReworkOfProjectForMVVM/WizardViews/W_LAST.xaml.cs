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

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.WizardViews
{
    /// <summary>
    /// Interaction logic for W_LAST.xaml
    /// </summary>
    public partial class W_LAST : Page
    {
        public W_LAST()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
