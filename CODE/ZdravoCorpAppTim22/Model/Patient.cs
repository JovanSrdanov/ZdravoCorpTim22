using System;

namespace Model
{
   public class Patient : User
   {
      public MedicalRecord medicalRecord;
      public MedicalAppointment[] medicalAppointment;
   
   }
}