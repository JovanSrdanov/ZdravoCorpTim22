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
    /// Interaction logic for PersonalNotesPageView.xaml
    /// </summary>
    public partial class PersonalNotesPageView : Page
    {
        public PersonalNotesPageView()
        {
            InitializeComponent();

            this.DataContext = new PersonalNotesPageViewModel();
        }

        private void MakeNote_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChooseAnamnesisPageView());
        }
    }
}
