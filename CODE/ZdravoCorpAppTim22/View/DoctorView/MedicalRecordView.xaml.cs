﻿using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using ZdravoCorpAppTim22.Controller;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public partial class MedicalRecordView : Window
    {
        private Patient selectedPatient;
        private DoctorAppointments doctorAppointments;
        private MedicalRecordsScreen medicalRecordsScreen;

        private ObservableCollection<string> medRecCondList;
        private ObservableCollection<string> medRecordAllergyList;

        public static List<MedicalReport> newlyCreatedReports;                //ako idem na back svi kreirani izvestaji i dijagnoze se brisu
        public static List<string> newlyCreatedDiagnosis;

        private int canCreateReport;        //ako doktor iz rasporeda gleda karton prosledjuje -1, u ostalim slucajevima moze proslediti bilo koji drugi broj
        public MedicalRecordView(int canCreateReport, int selectedPatientID, DoctorAppointments doctorAppointments = null, 
            MedicalRecordsScreen medicalRecordsScreen = null)
        {
            InitializeComponent();

            newlyCreatedReports = new List<MedicalReport>();
            newlyCreatedDiagnosis = new List<string>();

            selectedPatient = PatientController.Instance.GetByID(selectedPatientID);
            this.canCreateReport = canCreateReport;
            if (canCreateReport == -1)                                                
            {
                CreateReportBtn.IsEnabled = false;
                CreateReportBtn.Foreground = new SolidColorBrush(Colors.Black);
                FinishReportBtn.Visibility = Visibility.Hidden;
            }

            //privremeni karton

            /*MedicalRecord medRecordTemp = new MedicalRecord(1, BloodType.B_MINUS, selectedPatient, 
                new ObservableCollection<String> { "Peanuts", "Sun", "Milk", "Polen"}, 
                new ObservableCollection<String> { "Arthritis", "Asthma", "Glaucoma"});
            MedicalRecordController.Instance.Create(medRecordTemp);*/

            selectedPatient.medicalRecord = MedicalRecordController.Instance.GetByID(MedicalRecordController.Instance.GetAll().FindIndex(r => r.Patient.Id ==
            selectedPatientID));

            this.doctorAppointments = doctorAppointments;
            this.medicalRecordsScreen = medicalRecordsScreen;

            NameSurnameBlock.Text = selectedPatient.Name + " " + selectedPatient.Surname;
            GenderBlock.Text = selectedPatient.Gender.ToString();
            DateBirthBlock.Text = selectedPatient.Birthday.Date.ToShortDateString();
            JMBGBlock.Text = selectedPatient.Jmbg;

            //medRecCondList = new ObservableCollection<string>(selectedPatient.medicalRecord.ConditionList);
            ProblemsListBox.ItemsSource = selectedPatient.medicalRecord.ConditionList;

            medRecordAllergyList = new ObservableCollection<string>(selectedPatient.medicalRecord.AllergiesList);
            AllergiesListBox.ItemsSource = medRecordAllergyList;

            PastReportsListBox.ItemsSource = selectedPatient.medicalRecord.MedicalReport;
            //MedicationsListBox.ItemsSource = selectedPatient.medicalRecord.medicalReport.
        }

        private void MedRecClosed(object sender, EventArgs e)
        {
            //CancelAndCloseRemove();
            Application.Current.MainWindow.Show();
            this.Close();
        }

        private void CancelAndCloseRemove()
        {
            if (newlyCreatedReports.Count > 0)
            {
                foreach (MedicalReport medicalReport in newlyCreatedReports)
                {
                    MedicalReportController.Instance.DeleteByID(medicalReport.Id);
                    selectedPatient.medicalRecord.RemoveMedicalReport(medicalReport);
                }

                newlyCreatedReports.Clear();
            }

            if (newlyCreatedDiagnosis.Count > 0)
            {
                foreach (string diagnosis in newlyCreatedDiagnosis)
                {
                    selectedPatient.medicalRecord.ConditionList.Remove(diagnosis);
                }

                MedicalRecordController.Instance.Update(selectedPatient.medicalRecord);
                newlyCreatedDiagnosis.Clear();
            }
        }

        private void BackBtn(object sender, RoutedEventArgs e)
        {
            CancelAndCloseRemove();

            if (doctorAppointments == null)
            {
                medicalRecordsScreen.Show();
            }
            else
            {
                doctorAppointments.Show();
            }
            this.Close();
        }

        private void CreateReportBtnClick(object sender, RoutedEventArgs e)
        {
            CreateReport createReport = new CreateReport(selectedPatient, this);
            createReport.Owner = this;
            createReport.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            createReport.Show();

            this.Hide();
        }

        private void OpenReportBtnClick(object sender, RoutedEventArgs e)
        {
            MedicalReport medicalReport = PastReportsListBox.SelectedItem as MedicalReport;
            if (medicalReport == null)
            {
                MessageBox.Show("Please select a report", "Open report", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                OpenReport openReport = new OpenReport(medicalReport, this);
                openReport.Owner = this;
                openReport.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                openReport.Show();

                this.Hide();
            }
        }

        private void FinishReportClick(object sender, RoutedEventArgs e)
        {
            DoctorController.Instance.GetByID(DoctorHome.selectedDoctorId).AddMedicalRecord(selectedPatient.medicalRecord);
            MedicalAppointmentController.Instance.DeleteByID(DoctorAppointments.medicalAppointment.Id);
            DoctorAppointments.CurDocAppointemntsObservable.Remove(DoctorAppointments.medicalAppointment);

            doctorAppointments.Show();
            this.Close();
        }
    }
}
