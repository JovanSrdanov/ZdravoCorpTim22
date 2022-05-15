﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.View.Manager.Commands;
using ZdravoCorpAppTim22.View.Manager.Pages.MedicinePages;
using ZdravoCorpAppTim22.View.Manager.Views;

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

        public ObservableCollection<MedicineData> SelectedMedicines { get; set; }
        public ObservableCollection<MedicineData> AllMedicines { get; set; }

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

            SelectedMedicines = new ObservableCollection<MedicineData>();
            AllMedicines = new ObservableCollection<MedicineData>(MedicineDataController.Instance.GetAll());
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
            else
            {
                InfoModal.Show("Medicine with that name already exists");
                return;
            }
            
            foreach (Ingredient ingredient in SelectedIngredients)
            {
                ingredient.MedicineData = medicineData;
                IngredientController.Instance.Create(ingredient);
            }

            foreach(MedicineData m in SelectedMedicines)
            {
                medicineData.AddReplacement(m);
                Replacement replacement = new Replacement
                {
                    ReplacementToMedicine = medicineData,
                    ReplacingMedicine = m
                };
                ReplacementController.Instance.Create(replacement);
            }

            Medicine medicine = new Medicine
            {
                MedicineData = medicineData,
                Amount = amount
            };

            MedicineDataController.Instance.Update(medicineData);
            MedicineController.Instance.Create(medicine);
            
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