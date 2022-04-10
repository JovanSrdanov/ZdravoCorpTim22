// File:    Doctor.cs
// Author:  rade-
// Created: 8 April, 2022 14:34:22
// Purpose: Definition of Class Doctor

using System;

namespace Model
{
    public class Doctor : User
    {
        public DoctorSpecialisationType DoctorType { get; set; }
        public System.Collections.Generic.List<MedicalAppointment> medicalAppointment;



        public Doctor()
        {
        }

        public Doctor(string name, string surname, string email, string jmbg, string password, DateTime birthday, string phone, Gender gender, int iD, Address address, DoctorSpecialisationType doctorType, System.Collections.Generic.List<MedicalAppointment> medicalAppointment) : base(name, surname, email, jmbg, password, birthday, phone, gender, iD, address)
        {
            DoctorType = doctorType;
            this.medicalAppointment = medicalAppointment;
        }

        public System.Collections.Generic.List<MedicalAppointment> MedicalAppointment
        {
            get
            {
                if (medicalAppointment == null)
                    medicalAppointment = new System.Collections.Generic.List<MedicalAppointment>();
                return medicalAppointment;
            }
            set
            {
                RemoveAllMedicalAppointment();
                if (value != null)
                {
                    foreach (MedicalAppointment oMedicalAppointment in value)
                        AddMedicalAppointment(oMedicalAppointment);
                }
            }
        }

        public void AddMedicalAppointment(MedicalAppointment newMedicalAppointment)
        {
            if (newMedicalAppointment == null)
                return;
            if (this.medicalAppointment == null)
                this.medicalAppointment = new System.Collections.Generic.List<MedicalAppointment>();
            if (!this.medicalAppointment.Contains(newMedicalAppointment))
            {
                this.medicalAppointment.Add(newMedicalAppointment);
                newMedicalAppointment.Doctor = this;
            }
        }


        public void RemoveMedicalAppointment(MedicalAppointment oldMedicalAppointment)
        {
            if (oldMedicalAppointment == null)
                return;
            if (this.medicalAppointment != null)
                if (this.medicalAppointment.Contains(oldMedicalAppointment))
                {
                    this.medicalAppointment.Remove(oldMedicalAppointment);
                    oldMedicalAppointment.Doctor = null;
                }
        }


        public void RemoveAllMedicalAppointment()
        {
            if (medicalAppointment != null)
            {
                System.Collections.ArrayList tmpMedicalAppointment = new System.Collections.ArrayList();
                foreach (MedicalAppointment oldMedicalAppointment in medicalAppointment)
                    tmpMedicalAppointment.Add(oldMedicalAppointment);
                medicalAppointment.Clear();
                foreach (MedicalAppointment oldMedicalAppointment in tmpMedicalAppointment)
                    oldMedicalAppointment.Doctor = null;
                tmpMedicalAppointment.Clear();
            }
        }

    }
}