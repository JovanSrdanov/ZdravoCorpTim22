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

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.PresonalNoteTab
{
    /// <summary>
    /// Interaction logic for SuccessPersonalNoteCreated.xaml
    /// </summary>
    public partial class SuccessPersonalNoteCreated : Page
    {
        public SuccessPersonalNoteCreated()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PersonalNotesPageView());
        }
    }
}
