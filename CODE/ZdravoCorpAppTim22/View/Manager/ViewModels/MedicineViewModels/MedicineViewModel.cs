using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.View.Manager.Commands;
using ZdravoCorpAppTim22.View.Manager.Pages.MedicinePages;
using ZdravoCorpAppTim22.View.Manager.Views;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels.MedicineViewModels
{
    public class MedicineViewModel 
    {
        public RelayCommand OpenAddCommand { get; private set; }
        public RelayCommand OpenEditCommand { get; private set; }
        public RelayCommand DeleteMedicineCommand { get; private set; }
        public RelayCommand OpenDetailsCommand { get; private set; }

        public ObservableCollection<Medicine> MedicineCollection { get; set; }

        public MedicineViewModel()
        {
            OpenAddCommand = new RelayCommand(OpenAdd);
            OpenEditCommand = new RelayCommand(OpenEdit, IsSelected);
            DeleteMedicineCommand = new RelayCommand(DeleteMedicine, IsSelected);
            OpenDetailsCommand = new RelayCommand(OpenDetails, CanOpenDetails);

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
            if (ConfirmModal.Show("Are you sure?"))
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
                if(medicine.MedicineData.Approval != null)
                {
                    ApprovalController.Instance.DeleteByID(medicine.MedicineData.Approval.Id);
                }
                MedicineDataController.Instance.DeleteByID(medicine.MedicineData.Id);
            }
        }

        public void OpenDetails(object obj)
        {
            MedicineDetailsModal.Show((Medicine)obj);
        }

        private bool CanOpenDetails(object obj)
        {
            Medicine medicine = (Medicine)obj;
            if (medicine == null || medicine.MedicineData == null || medicine.MedicineData.Approval == null || medicine.MedicineData.Approval.Doctor == null)
            {
                return false;
            }
            return true;
        }

        private bool IsSelected(object obj)
        {
            Medicine medicine = (Medicine)obj;
            if (medicine == null || medicine.MedicineData == null)
            {
                return false;
            }
            return true;
        }
    }
}

