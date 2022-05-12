using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
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

        public ObservableCollection<MedicineData> SelectedMedicines { get; set; }
        public ObservableCollection<MedicineData> AllMedicines { get; set; }

        public Medicine OldMedicine { get; private set; }

        public EditMedicineViewModel(Medicine medicine)
        {
            EditMedicineCommand = new RelayCommand(EditMedicine, CanEditMedicine);
            NavigateBackCommand = new RelayCommand(NavigateBack);

            Amount = medicine.Amount;
            MedicineName = medicine.MedicineData.Name;
            OldMedicine = medicine;

            AllIngredients = new ObservableCollection<Ingredient>();
            SelectedIngredients = new ObservableCollection<Ingredient>(medicine.MedicineData.Ingredient);

            foreach(IngredientData ingredientData in IngredientDataController.Instance.GetAll())
            {
                Ingredient temp = SelectedIngredients.Where(x => x.IngredientData.Id == ingredientData.Id).FirstOrDefault();
                if(temp == null)
                {
                    AllIngredients.Add(new Ingredient(ingredientData));
                }
            }

            AllMedicines = new ObservableCollection<MedicineData>();
            SelectedMedicines = new ObservableCollection<MedicineData>(medicine.MedicineData.Replacements);
            foreach(MedicineData medicineData in MedicineDataController.Instance.GetAll())
            {
                if (medicineData.Id == OldMedicine.MedicineData.Id) continue;
                MedicineData temp = SelectedMedicines.Where(x => x.Id == medicineData.Id).FirstOrDefault();
                if(temp == null)
                {
                    AllMedicines.Add(medicineData);
                }
            }
        }

        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void EditMedicine(object obj)
        {
            if (!OldMedicine.MedicineData.Name.Equals(name) && MedicineDataController.Instance.GetByName(name) != null)
            {
                MessageBox.Show("Medicine with that name already exists");
                return;
            }

            OldMedicine.MedicineData.Name = MedicineName;
            OldMedicine.Amount = Amount;

            List<Ingredient> ingredientsToRemove = new List<Ingredient>();
            List<Ingredient> ingredientsToAdd = new List<Ingredient>();

            foreach (Ingredient ingredient in SelectedIngredients)
            {
                Ingredient temp = OldMedicine.MedicineData.Ingredient.Where(x => x.IngredientData.Id == ingredient.IngredientData.Id).FirstOrDefault();
                if (temp == null)
                {
                    ingredientsToAdd.Add(ingredient);

                }
            }

            foreach (Ingredient ingredient in OldMedicine.MedicineData.Ingredient)
            {
                Ingredient temp = SelectedIngredients.Where(x => x.IngredientData.Id == ingredient.IngredientData.Id).FirstOrDefault();
                if(temp == null)
                {
                    ingredientsToRemove.Add(ingredient);
                }
            }

            foreach (Ingredient ingredient in ingredientsToRemove)
            {
                OldMedicine.MedicineData.RemoveIngredient(ingredient);
                IngredientController.Instance.DeleteByID(ingredient.Id);
            }

            foreach(Ingredient ingredient in ingredientsToAdd)
            {
                OldMedicine.MedicineData.AddIngredient(ingredient);
                IngredientController.Instance.Create(ingredient);
            }

            ReplacementController.Instance.DeleteByReplacementTarget(OldMedicine.MedicineData);
            OldMedicine.MedicineData.RemoveAllReplacements();

            foreach (MedicineData m in SelectedMedicines)
            {
                OldMedicine.MedicineData.AddReplacement(m);
                Replacement replacement = new Replacement
                {
                    ReplacementToMedicine = OldMedicine.MedicineData,
                    ReplacingMedicine = m
                };
                ReplacementController.Instance.Create(replacement);
            }

            MedicineDataController.Instance.Update(OldMedicine.MedicineData);
            MedicineController.Instance.Update(OldMedicine);

            ManagerHome.NavigationService.Navigate(new MedicineView());
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
