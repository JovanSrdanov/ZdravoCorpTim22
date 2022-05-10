using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.View.Manager.ViewModels.MedicineViewModels;

namespace ZdravoCorpAppTim22.View.Manager.Pages.MedicinePages
{
    public partial class EditMedicineView : Page
    {
        readonly EditMedicineViewModel ViewModel;
        Point StartPoint = new Point();
        public EditMedicineView(Medicine medicine)
        {
            InitializeComponent();
            ViewModel = new EditMedicineViewModel(medicine);
            DataContext = ViewModel;
        }

        private void PreviewMouseLeftButtonDownHandler(object sender, MouseButtonEventArgs e)
        {
            StartPoint = e.GetPosition(null);
        }
        private void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = StartPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed && (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                DataGrid grid = sender as DataGrid;
                DataGridRow gridItem = FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);

                if (gridItem == null) return;
                Ingredient ingredient = (Ingredient)grid.ItemContainerGenerator.ItemFromContainer(gridItem);

                DataObject dragData = new DataObject("myFormat", ingredient);
                DragDrop.DoDragDrop(gridItem, dragData, DragDropEffects.Move);
            }
        }
        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }
        private void DragOverHandler(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || e.Source == sender)
            {
                e.Effects = DragDropEffects.None;
            }
        }
        private void DataGridSelectedIngredients_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                Ingredient ingredient = e.Data.GetData("myFormat") as Ingredient;
                if (ViewModel.SelectedIngredients.Where(x => x.IngredientData.Id == ingredient.IngredientData.Id).FirstOrDefault() == null)
                {
                    ViewModel.AllIngredients.Remove(ingredient);
                    ViewModel.SelectedIngredients.Add(ingredient);
                }
            }
        }
        private void DataGridAllIngredients_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                Ingredient ingredient = e.Data.GetData("myFormat") as Ingredient;
                if (ViewModel.AllIngredients.Where(x => x.IngredientData.Id == ingredient.IngredientData.Id).FirstOrDefault() == null)
                {
                    ViewModel.SelectedIngredients.Remove(ingredient);
                    ViewModel.AllIngredients.Add(ingredient);
                }
            }
        }
    }
}
