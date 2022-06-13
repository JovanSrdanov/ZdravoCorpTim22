using Repository;
using System;
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

        public void MakeNote(PersonalNote personalNote, DateTime hoursAndMinutes)
        {
            int hoursDifference = 24 / personalNote.Frequency;

            for (int i = 0; i < personalNote.Frequency; i++)
            {
                DateTime dateTimeForAdd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                    hoursAndMinutes.Hour, hoursAndMinutes.Minute, 0);

                dateTimeForAdd= dateTimeForAdd.AddHours(hoursDifference*i);

                personalNote.NextNotification.Add(dateTimeForAdd);


            }

            Instance.Create(personalNote);

            

        }
    }
}