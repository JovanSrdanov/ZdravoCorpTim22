using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model.Generic;

namespace ZdravoCorpAppTim22.Model
{
    public class IngredientData : IHasID
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IngredientData() { }

        public IngredientData(string name)
        {
            Name = name;
        }

        [JsonIgnore]
        private ObservableCollection<Ingredient> ingredient;
        [JsonIgnore]
        public ObservableCollection<Ingredient> Ingredient
        {
            get
            {
                if (ingredient == null)
                    ingredient = new ObservableCollection<Ingredient>();
                return ingredient;
            }
            set
            {
                RemoveAllIngredient();
                if (value != null)
                {
                    foreach (Ingredient oIngredient in value)
                        AddIngredient(oIngredient);
                }
            }
        }

        public void AddIngredient(Ingredient newIngredient)
        {
            if (newIngredient == null)
                return;
            if (this.ingredient == null)
                this.ingredient = new ObservableCollection<Ingredient>();
            if (!this.ingredient.Contains(newIngredient))
            {
                this.ingredient.Add(newIngredient);
                newIngredient.IngredientData = this;
            }
        }
        public void RemoveIngredient(Ingredient oldIngredient)
        {
            if (oldIngredient == null)
                return;
            if (this.ingredient != null)
                if (this.ingredient.Contains(oldIngredient))
                {
                    this.ingredient.Remove(oldIngredient);
                    oldIngredient.IngredientData = null;
                }
        }
        public void RemoveAllIngredient()
        {
            if (ingredient != null)
            {
                System.Collections.ArrayList tmpIngredient = new System.Collections.ArrayList();
                foreach (Ingredient oldIngredient in ingredient)
                    tmpIngredient.Add(oldIngredient);
                ingredient.Clear();
                foreach (Ingredient oldIngredient in tmpIngredient)
                    oldIngredient.IngredientData = null;
                tmpIngredient.Clear();
            }
        }
    }
}
