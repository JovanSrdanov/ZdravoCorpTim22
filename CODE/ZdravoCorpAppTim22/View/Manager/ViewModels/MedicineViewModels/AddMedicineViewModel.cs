using System.Collections.ObjectModel;
using System.ComponentModel;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.View.Manager.Commands;
using ZdravoCorpAppTim22.View.Manager.Pages.MedicinePages;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels.MedicineViewModels
{
    public class AddMedicineViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand AddMedicineCommand { get; private set; }
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

        public AddMedicineViewModel()
        {
            AddMedicineCommand = new RelayCommand(AddMedicine, CanAddMedicine);
            NavigateBackCommand = new RelayCommand(NavigateBack);

            AllIngredients = new ObservableCollection<Ingredient>();
            SelectedIngredients = new ObservableCollection<Ingredient>();
            foreach (IngredientData ingredientData in IngredientDataController.Instance.GetAll())
            {
                AllIngredients.Add(new Ingredient(ingredientData));
            }
        }

        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddMedicine(object obj)
        {
            MedicineData medicineData = MedicineDataController.Instance.GetByName(name);
            if(medicineData == null)
            {
                medicineData = new MedicineData(-1, name);
                MedicineDataController.Instance.Create(medicineData);
            }
            Medicine medicine = new Medicine
            {
                MedicineData = medicineData,
                Amount = amount
            };

            MedicineController.Instance.Create(medicine);

            foreach (Ingredient ingredient in SelectedIngredients)
            {
                ingredient.Medicine = medicine;
                IngredientController.Instance.Create(ingredient);
            }

            MedicineController.Instance.Update(medicine);
            ManagerHome.NavigationService.Navigate(new MedicineView());
        }
        public void NavigateBack(object obj)
        {
            ManagerHome.NavigationService.Navigate(new MedicineView());
        }
        private bool CanAddMedicine(object obj)
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
