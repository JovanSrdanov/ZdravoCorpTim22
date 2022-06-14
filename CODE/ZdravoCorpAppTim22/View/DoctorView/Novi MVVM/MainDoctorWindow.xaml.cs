using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.ViewModels;
using ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.Views;

namespace ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM
{
    public partial class MainDoctorWindow : Window
    {
        public MainWindowViewModel _ViewModel { get; set; }
        public MainWindow mainWindow;
        public static MainDoctorWindow MainDoctorWindowInstance { get; set; }       //DODAO
        public MainDoctorWindow()
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            InitializeComponent();

            MainDoctorWindowInstance = this;        //DODAO

            this._ViewModel = new MainWindowViewModel(this.frame.NavigationService, this, mainWindow);
            this.DataContext = this._ViewModel;
        }
    }
}
