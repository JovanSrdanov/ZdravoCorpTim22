using Controller;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using ZdravoCorpAppTim22.Controller;

namespace ZdravoCorpAppTim22.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretaryAccountsMedicalRecord.xaml
    /// </summary>
    public partial class SecretaryAccountsMedicalRecord : Window
    {
        private MedicalRecord MedicalRecord = new MedicalRecord();

        public SecretaryAccountsMedicalRecord(MedicalRecord medicalRecord)
        {
            InitializeComponent();
            MedicalRecord = medicalRecord;
            comboBoxAllergies.ItemsSource = MedicineController.Instance.GetAll();
            if (MedicalRecord.AllergiesList != null)
            {
                for (int i = 0; i < MedicalRecord.AllergiesList.Count; i++)
                {
                    listBoxAllergies.Items.Add(MedicalRecord.AllergiesList[i]);
                }
            }

            comboBoxBloodType.SelectedItem = MedicalRecord.BloodType;

        }

        private void AddAllergieBtn_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxAllergies.Items.Contains(comboBoxAllergies.SelectedItem))
            {
                MessageBox.Show("Allergie is already in list!");
            }
            else
            {
                listBoxAllergies.Items.Add(comboBoxAllergies.SelectedItem);
            }
        }

        private void DeleteAllergieButton_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxAllergies.SelectedItem != null)
            {
                listBoxAllergies.Items.Remove(listBoxAllergies.SelectedItem);
            }
            else
            {
                MessageBox.Show("Must select allergie first!");
            }
        }


        private void SaveMedicalRecord()
        {
            if (MedicalRecord.AllergiesList != null)
            {
                MedicalRecord.AllergiesList.Clear();
            }
            else
            {
                MedicalRecord.AllergiesList = new List<string>();       //STEFAN MENJAO
            }

            for (int i = 0; i < listBoxAllergies.Items.Count; i++)
            {
                MedicalRecord.AllergiesList.Add(listBoxAllergies.Items[i].ToString());
            }

            MedicalRecord.BloodType = (BloodType)comboBoxBloodType.SelectedItem;
            MedicalRecordController.Instance.Update(MedicalRecord);
        }

        private void comboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Save medical record", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    SaveMedicalRecord();
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    return;
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Cancel medical record", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    return;
            }
        }
    }
}
