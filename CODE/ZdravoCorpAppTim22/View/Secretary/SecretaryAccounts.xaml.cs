using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ZdravoCorpAppTim22.View.Secretary
{
    public partial class SecretaryAccounts : Window
    {
        private SecretaryHome secretaryHome;
        public List<Model.Doctor> listaDoktora = new List<Model.Doctor>();

        Model.Doctor dr1 = new Model.Doctor("Mirko", "Vlaskovic", "adsa@fgma.com", "121", "kuracPalac", System.DateTime.Now, "31231311", Model.Gender.male, 0, new Address(), Model.DoctorSpecialisationType.regular, null);
        Model.Doctor dr2 = new Model.Doctor("Boban", "Antonic", "adsa@fgma.com", "121", "kuracPalac", System.DateTime.Now, "31231311", Model.Gender.male, 1, new Address(), Model.DoctorSpecialisationType.regular, null);
        Model.Doctor dr3 = new Model.Doctor("Slavko", "Malinovic", "adsa@fgma.com", "121", "kuracPalac", System.DateTime.Now, "31231311", Model.Gender.male, 2, new Address(), Model.DoctorSpecialisationType.specialist, null);

        public SecretaryAccounts(SecretaryHome secretaryHome)
        {
            InitializeComponent();
            this.secretaryHome = secretaryHome;
            listaDoktora.Add(dr1);
            listaDoktora.Add(dr2);
            listaDoktora.Add(dr3);
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


        private void Doctor_RB_Checked(object sender, RoutedEventArgs e)
        {
            if (AccountsDataGrid != null)
            {
                SetUpAccountsDataGrid();

                DataGridTextColumn dataGridColumnSpecialisation = new DataGridTextColumn();
                dataGridColumnSpecialisation.Header = "Specialisation";
                var binding = new Binding();
                binding.Path = new PropertyPath("DoctorType");
                dataGridColumnSpecialisation.Binding = binding;
                dataGridColumnSpecialisation.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                AccountsDataGrid.Columns.Add(dataGridColumnSpecialisation);

                for (int i = 0; i < listaDoktora.Count; i++)
                {
                    AccountsDataGrid.Items.Add(listaDoktora[i]);
                }
            }
        }

        private void Manager_RB_Checked(object sender, RoutedEventArgs e)
        {
            SetUpAccountsDataGrid();
        }

        private void Patient_RB_Checked(object sender, RoutedEventArgs e)
        {
            SetUpAccountsDataGrid();
        }

        private void Secretary_RB_Checked(object sender, RoutedEventArgs e)
        {
            SetUpAccountsDataGrid();
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

            SecretaryAccountsEdit secretaryAccountsEdit = new SecretaryAccountsEdit(this);
            this.Hide();
        }


        private void Window_Closed(object sender, System.EventArgs e)
        {
            secretaryHome.Show();
        }
    }
}
