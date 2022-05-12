using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace ZdravoCorpAppTim22.Model
{
    public class Replacement : IHasID
    {
        public int Id { get; set; }
        [JsonConverter(typeof(MedicineDataToIDConverter))]
        public MedicineData ReplacingMedicine { get; set; }
        [JsonConverter(typeof(MedicineDataToIDConverter))]
        public MedicineData ReplacementToMedicine { get; set; }
    }
}
