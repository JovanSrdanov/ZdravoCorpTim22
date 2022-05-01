using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Model.Generic;

namespace ZdravoCorpAppTim22.Model
{
    public class Medicine : IHasID
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string Ammount { get; set; }
        public string Ingredients { get; set; }

        [JsonConstructor]
        public Medicine() { }
        public Medicine(string name, string ingredients)
        {
            Name = name;
            //Ammount = ammount;
            Ingredients = ingredients;
        }
    }
}
