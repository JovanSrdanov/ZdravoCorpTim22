using Model;
using Service;
using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;

namespace Controller
{
    public class PersonalNoteController : GenericController<PersonalNoteService, PersonalNote>
    {
        private static PersonalNoteController instance;
        private PersonalNoteController() : base(PersonalNoteService.Instance) { }
        public static PersonalNoteController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PersonalNoteController();
                }

                return instance;
            }
        }
    }
}