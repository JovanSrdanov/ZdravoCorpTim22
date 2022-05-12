using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Input;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.View.Manager.ViewModels.MedicineViewModels;

namespace ZdravoCorpAppTim22.View.Manager.Pages.MedicinePages
{
    public partial class AddMedicineView : Page
    {
        readonly AddMedicineViewModel ViewModel;
        Point StartPoint = new Point();
        public AddMedicineView()
        {
            InitializeComponent();
            ViewModel = new AddMedicineViewModel();
            DataContext = ViewModel;
        }

        private void PreviewMouseLeftButtonDownHandler(object sender, MouseButtonEventArgs e)
        {
            StartPoint = e.GetPosition(null);
        }

        #region ingredient drag&drop handlers

        private void IngredientMouseMoveHandler(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = StartPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed && (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                DataGrid grid = sender as DataGrid;
                DataGridRow gridItem = ManagerHome.FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);

                if (gridItem == null) return;
                Ingredient ingredient = (Ingredient)grid.ItemContainerGenerator.ItemFromContainer(gridItem);

                DataObject dragData = new DataObject("ingredientFormat", ingredient);
                DragDrop.DoDragDrop(gridItem, dragData, DragDropEffects.Move);
            }
        }
        private void IngredientDragOverHandler(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("ingredientFormat") || e.Source == sender)
            {
                e.Effects = DragDropEffects.None;
            }
        }
        private void DataGridSelectedIngredients_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("ingredientFormat"))
            {
                Ingredient ingredient = e.Data.GetData("ingredientFormat") as Ingredient;
                if (ViewModel.SelectedIngredients.Where(x => x.IngredientData.Id == ingredient.IngredientData.Id).FirstOrDefault() == null)
                {
                    ViewModel.AllIngredients.Remove(ingredient);
                    ViewModel.SelectedIngredients.Add(ingredient);
                }
            }
        }
        private void DataGridAllIngredients_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("ingredientFormat"))
            {
                Ingredient ingredient = e.Data.GetData("ingredientFormat") as Ingredient;
                if(ViewModel.AllIngredients.Where(x => x.IngredientData.Id == ingredient.IngredientData.Id).FirstOrDefault() == null)
                {
                    ViewModel.SelectedIngredients.Remove(ingredient);
                    ViewModel.AllIngredients.Add(ingredient);
                }
            }
        }
        #endregion

        #region medicine drag&drop handlers
        private void MedicineMouseMoveHandler(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = StartPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed && (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                DataGrid grid = sender as DataGrid;
                DataGridRow gridItem = ManagerHome.FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);

                if (gridItem == null) return;
                MedicineData medicine = (MedicineData)grid.ItemContainerGenerator.ItemFromContainer(gridItem);

                DataObject dragData = new DataObject("medicineFormat", medicine);
                DragDrop.DoDragDrop(gridItem, dragData, DragDropEffects.Move);
            }
        }
        private void MedicineDragOverHandler(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("medicineFormat") || e.Source == sender)
            {
                e.Effects = DragDropEffects.None;
            }
        }
        private void DataGridSelectedMedicines_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("medicineFormat"))
            {
                MedicineData medicine = e.Data.GetData("medicineFormat") as MedicineData;
                if (ViewModel.SelectedMedicines.Where(x => x.Id == medicine.Id).FirstOrDefault() == null)
                {
                    ViewModel.AllMedicines.Remove(medicine);
                    ViewModel.SelectedMedicines.Add(medicine);
                }
            }
        }
        private void DataGridAllMedicines_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("medicineFormat"))
            {
                MedicineData medicine = e.Data.GetData("medicineFormat") as MedicineData;
                if (ViewModel.AllMedicines.Where(x => x.Id == medicine.Id).FirstOrDefault() == null)
                {
                    ViewModel.SelectedMedicines.Remove(medicine);
                    ViewModel.AllMedicines.Add(medicine);
                }
            }
        }
        #endregion
    }
}
