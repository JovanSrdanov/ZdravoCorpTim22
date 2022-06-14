using Model;
using System.Windows;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretaryNewEquipement.xaml
    /// </summary>
    public partial class SecretaryNewEquipement : Window
    {
        SecretaryMedicalOrder SecretaryMedicalOrder;
        public SecretaryNewEquipement(SecretaryMedicalOrder secretaryMedicalOrder)
        {
            InitializeComponent();
            SecretaryMedicalOrder = secretaryMedicalOrder;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EquipmentData equipmentData = EquipmentDataController.Instance.GetByName(textBox.Text);
            if (equipmentData == null)
            {
                equipmentData = new EquipmentData(-1, textBox.Text, EquipmentType.consumable);

                EquipmentDataController.Instance.Create(equipmentData);

                Equipment equipment = new Equipment
                {
                    EquipmentData = equipmentData,
                    Amount = 0
                };
                EquipmentController.Instance.AddWarehouseEquipment(equipment);
                MessageBox.Show("Equipement created");
                SecretaryMedicalOrder.comboBox.ItemsSource = EquipmentDataController.Instance.GetAllConsumable();
                SecretaryMedicalOrder.comboBox.Items.Refresh();
                SecretaryMedicalOrder.Hide();
                SecretaryMedicalOrder.Show();
            }
            else
            {
                MessageBox.Show("That equipement already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Close();
        }
    }
}


