using Controller;
using Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ZdravoCorpAppTim22.View.Secretary
{
    public partial class SecretaryAccounts : Window
    {
        private SecretaryHome secretaryHome;

        public SecretaryAccounts(SecretaryHome secretaryHome)
        {
            InitializeComponent();
            this.secretaryHome = secretaryHome;
        }

        public void SetUpAccountsDataGrid()
        {
            if (AccountsDataGrid != null)
            {
                AccountsDataGrid.Columns.Clear();
                AccountsDataGrid.Items.Clear();

                DataGridTextColumn dataGridColumnName = new DataGridTextColumn();
                dataGridColumnName.Header = "Name";
                var binding = new Binding();
                binding.Path = new PropertyPath("Name");
                dataGridColumnName.Binding = binding;
                dataGridColumnName.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                AccountsDataGrid.Columns.Add(dataGridColumnName);

                DataGridTextColumn dataGridColumnSurname = new DataGridTextColumn();
                dataGridColumnSurname.Header = "Surname";
                binding = new Binding();
                binding.Path = new PropertyPath("Surname");
                dataGridColumnSurname.Binding = binding;
                dataGridColumnSurname.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                AccountsDataGrid.Columns.Add(dataGridColumnSurname);
            }
        }

        public void DoctorChecked()
        {
            if (AccountsDataGrid != null)
            {
                SetUpAccountsDataGrid();

                //Podesavanje za specijalizaciju doktora
                {
                    DataGridTextColumn dataGridColumnSpecialisation = new DataGridTextColumn();
                    dataGridColumnSpecialisation.Header = "Specialisation";
                    var binding = new Binding();
                    binding.Path = new PropertyPath("DoctorType");
                    dataGridColumnSpecialisation.Binding = binding;
                    dataGridColumnSpecialisation.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    AccountsDataGrid.Columns.Add(dataGridColumnSpecialisation);
                }

                List<Doctor> doktori = DoctorController.Instance.GetAll();

                for (int i = 0; i < doktori.Count; i++)
                {
                    AccountsDataGrid.Items.Add(doktori[i]);
                }
            }
        }

        private void Doctor_RB_Checked(object sender, RoutedEventArgs e)
        {
            DoctorChecked();

        }

        public void ManagerChecked()
        {
            if (AccountsDataGrid != null)
            {
                SetUpAccountsDataGrid();
                List<ManagerClass> managers = ManagerController.Instance.GetAll();

                for (int i = 0; i < managers.Count; i++)
                {
                    AccountsDataGrid.Items.Add(managers[i]);
                }
            }
        }

        private void Manager_RB_Checked(object sender, RoutedEventArgs e)
        {
            ManagerChecked();
        }

        public void PatientChecked()
        {
            SetUpAccountsDataGrid();
            List<Patient> patients = PatientController.Instance.GetAll();

            for (int i = 0; i < patients.Count; i++)
            {
                if (patients[i].Password == null)
                {
                    patients[i].Surname = "THIS IS EMERGENCY ACCOUNT";
                }
                AccountsDataGrid.Items.Add(patients[i]);
            }
        }

        private void Patient_RB_Checked(object sender, RoutedEventArgs e)
        {
            PatientChecked();
        }

        public void SecretaryChecked()
        {
            SetUpAccountsDataGrid();

            List<SecretaryClass> secretaries = SecretaryController.Instance.GetAll();

            for (int i = 0; i < secretaries.Count; i++)
            {
                AccountsDataGrid.Items.Add(secretaries[i]);
            }
        }

        private void Secretary_RB_Checked(object sender, RoutedEventArgs e)
        {
            SecretaryChecked();
        }

        private void NewAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            SecretaryAccountsNew secretaryAccountsNew = new SecretaryAccountsNew(this);
            this.Hide();
        }

        private void EditAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AccountsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Must select account from list!");
                return;
            }

            //Opening edit account screen
            {
                if ((bool)Patient_RB.IsChecked)
                {
                    SecretaryAccountsEdit secretaryAccountsEdit = new SecretaryAccountsEdit(this, (Patient)AccountsDataGrid.SelectedItem);

                }
                else if ((bool)Doctor_RB.IsChecked)
                {
                    SecretaryAccountsEdit secretaryAccountsEdit = new SecretaryAccountsEdit(this, (Doctor)AccountsDataGrid.SelectedItem);

                }
                else if ((bool)Manager_RB.IsChecked)
                {
                    SecretaryAccountsEdit secretaryAccountsEdit = new SecretaryAccountsEdit(this, (ManagerClass)AccountsDataGrid.SelectedItem);

                }
                else if ((bool)Secretary_RB.IsChecked)
                {
                    SecretaryAccountsEdit secretaryAccountsEdit = new SecretaryAccountsEdit(this, (SecretaryClass)AccountsDataGrid.SelectedItem);

                }
            }

            this.Hide();
        }


        private void Window_Closed(object sender, System.EventArgs e)
        {
            secretaryHome.Show();
        }

        private void Window_Activated(object sender, System.EventArgs e)
        {
            if ((bool)Doctor_RB.IsChecked)
            {
                DoctorChecked();
            }
            else if ((bool)Patient_RB.IsChecked)
            {
                PatientChecked();
            }
            else if ((bool)Manager_RB.IsChecked)
            {
                ManagerChecked();
            }
            else if ((bool)Secretary_RB.IsChecked)
            {
                SecretaryChecked();
            }
        }
    }
}
