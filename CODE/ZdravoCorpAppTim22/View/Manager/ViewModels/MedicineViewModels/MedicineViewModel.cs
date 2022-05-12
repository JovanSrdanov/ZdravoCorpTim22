using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

            MedicineCollection = new ObservableCollection<Medicine>(MedicineController.Instance.GetAllFree());
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
            Medicine medicine = (Medicine)obj;
            MedicineCollection.Remove(medicine);
            List<Medicine> medicineToRemove = new List<Medicine>();
            List<Ingredient> ingredientToRemove = new List<Ingredient>();

            foreach (Ingredient ingredient in medicine.MedicineData.Ingredient)
            {
                ingredientToRemove.Add(ingredient);
            }

            foreach (Medicine m in MedicineController.Instance.GetAll())
            {
                if(m.MedicineData.Id == medicine.MedicineData.Id)
                {
                    medicineToRemove.Add(m);
                    
                }
            }
            foreach(Ingredient ingredient in ingredientToRemove)
            {
                ingredient.MedicineData = null;
                IngredientController.Instance.DeleteByID(ingredient.Id);
            }
            foreach(Medicine m in medicineToRemove)
            {
                MedicineController.Instance.DeleteByID(m.Id);
            }
            medicine.MedicineData.RemoveAllIngredient();
            MedicineDataController.Instance.DeleteByID(medicine.MedicineData.Id);
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

