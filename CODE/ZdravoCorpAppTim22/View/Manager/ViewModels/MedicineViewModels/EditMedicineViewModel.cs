using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.View.Manager.Commands;
using ZdravoCorpAppTim22.View.Manager.Pages.MedicinePages;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels.MedicineViewModels
{
    public class EditMedicineViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand EditMedicineCommand { get; private set; }
        public RelayCommand NavigateBackCommand { get; private set; }
        private int amount;
        private string name;
        public int Amount
        {
            get => amount;
            set
            {
                amount = value;
                OnPropertyChanged("Amount");
            }
        }
        public string MedicineName
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("EquipmentName");
            }
        }
        public ObservableCollection<Ingredient> SelectedIngredients { get; set; }
        public ObservableCollection<Ingredient> AllIngredients { get; set; }

        public Medicine OldMedicine { get; private set; }

        public EditMedicineViewModel(Medicine medicine)
        {
            EditMedicineCommand = new RelayCommand(EditMedicine, CanEditMedicine);
            NavigateBackCommand = new RelayCommand(NavigateBack);

            Amount = medicine.Amount;
            MedicineName = medicine.MedicineData.Name;
            OldMedicine = medicine;

            AllIngredients = new ObservableCollection<Ingredient>();
            SelectedIngredients = new ObservableCollection<Ingredient>(medicine.Ingredient);

            foreach(IngredientData ingredientData in IngredientDataController.Instance.GetAll())
            {
                Ingredient temp = SelectedIngredients.Where(x => x.IngredientData.Id == ingredientData.Id).FirstOrDefault();
                if(temp == null)
                {
                    AllIngredients.Add(new Ingredient(ingredientData));
                }
            }
        }

        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void EditMedicine(object obj)
        {
            /*
            OldMedicine.MedicineData.Name = MedicineName;
            OldMedicine.Amount = Amount;

            List<Ingredient> ingredientsToRemove = new List<Ingredient>();
            List<Ingredient> ingredientsToAdd = new List<Ingredient>();

            foreach (Ingredient ingredient in SelectedIngredients)
            {
                Ingredient temp = OldMedicine.Ingredient.Where(x => x.IngredientData.Id == ingredient.IngredientData.Id).FirstOrDefault();
                if (temp == null)
                {
                    ingredientsToRemove.Add(ingredient);
                }
            }

            foreach (Ingredient ingredient in ingredientsToRemove)
            {
                OldMedicine.RemoveIngredient(ingredient);
                IngredientController.Instance.DeleteByID(ingredient.Id);
            }

            
            MedicineDataController.Instance.Update(OldMedicine.MedicineData);
            MedicineController.Instance.Update(OldMedicine);
            */
        }
        public void NavigateBack(object obj)
        {
            ManagerHome.NavigationService.Navigate(new MedicineView());
        }
        private bool CanEditMedicine(object obj)
        {
            if (name == null || name.Equals(""))
            {
                return false;
            }
            if (Amount < 0)
            {
                return false;
            }
            return true;
        }
    }
}
