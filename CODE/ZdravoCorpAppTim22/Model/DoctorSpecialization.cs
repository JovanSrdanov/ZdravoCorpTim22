using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model.Generic;

namespace ZdravoCorpAppTim22.Model
{
    public class DoctorSpecialization : IHasID
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonConstructor]
        public DoctorSpecialization() { }

        public DoctorSpecialization(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
