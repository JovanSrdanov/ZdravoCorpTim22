using Model;
using Service;
using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Controller;
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

        public void MakeNote(int frequency, string message, string reason, DateTime endOfShowing, DateTime hoursAndMinutes)
        {
            PersonalNote personalNote = new PersonalNote(-1, (Patient)AuthenticationController.Instance.GetLoggedUser(),
                frequency, message, reason, endOfShowing);

            PersonalNoteService.Instance.MakeNote(personalNote, hoursAndMinutes);

        }
    }
}