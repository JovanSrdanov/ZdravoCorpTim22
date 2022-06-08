using Model;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository.Generic;

namespace Repository
{
    public class PersonalNoteRepository : GenericRepository<PersonalNote>
    {
        public static string FileName = "PersonalNoteData.json";
        private static PersonalNoteRepository instance;
        private PersonalNoteRepository() : base(FileName) { }
        public static PersonalNoteRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PersonalNoteRepository();

                }

                return instance;
            }
        }
    }
}