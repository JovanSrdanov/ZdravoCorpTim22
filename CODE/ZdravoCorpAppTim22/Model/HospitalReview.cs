using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model.Generic;


namespace ZdravoCorpAppTim22.Model
{
    public class HospitalReview: IHasID

    {
        public int Id { get; set; }
        public int StaffHospitality { get; set; }
        public int Accessibility { get; set; }
        public int Hygiene { get; set; }
        public int Appearance { get; set; }
        public int Application { get; set; }

        [JsonConstructor]
        public HospitalReview()
        {
        }

        public HospitalReview(int id, int staffHospitality, int accessibility, int hygiene, int appearance, int application)
        {
            Id = id;
            StaffHospitality = staffHospitality;
            Accessibility = accessibility;
            Hygiene = hygiene;
            Appearance = appearance;
            Application = application;
        }


    }
}
