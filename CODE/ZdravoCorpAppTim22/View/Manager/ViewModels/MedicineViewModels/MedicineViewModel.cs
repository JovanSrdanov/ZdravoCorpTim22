using System.Collections.ObjectModel;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.View.Manager.Commands;
using ZdravoCorpAppTim22.View.Manager.Pages.MedicinePages;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels.MedicineViewModels
{
    public class MedicineViewModel 
    {
        public RelayCommand OpenAddCommand { get; private set; }
        public RelayCommand OpenEditCommand { get; private set; }
        public RelayCommand DeleteMedicineCommand { get; private set; }

        public ObservableCollection<Medicine> MedicineCollection { get; set; }

        public MedicineViewModel()
        {
            OpenAddCommand = new RelayCommand(OpenAdd);
            OpenEditCommand = new RelayCommand(OpenEdit, IsSelected);
            DeleteMedicineCommand = new RelayCommand(DeleteMedicine, IsSelected);
            MedicineCollection = MedicineController.Instance.GetAll();
        }

        public void OpenAdd(object obj)
        {
            ManagerHome.NavigationService.Navigate(new AddMedicineView());
        }
        
        public void OpenEdit(object obj)
        {
            Medicine medicine = (Medicine)obj;
            EditMedicineView editMedicine = new EditMedicineView(medicine);
            ManagerHome.NavigationService.Navigate(editMedicine);
        }

        public void DeleteMedicine(object obj)
        {

        }

        private bool IsSelected(object obj)
        {
            if ((Medicine)obj == null)
            {
                return false;
            }
            return true;
        }
    }
}

