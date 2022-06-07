using Model;
using Repository;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service.Generic;

namespace Service
{
    public class PersonalNoteService : GenericService<PersonalNoteRepository, PersonalNote>
    {
        private PersonalNoteService() : base(PersonalNoteRepository.Instance) { }
        public static PersonalNoteService Instance
        {
            get
            {
                return new PersonalNoteService();
            }
        }
    }
}