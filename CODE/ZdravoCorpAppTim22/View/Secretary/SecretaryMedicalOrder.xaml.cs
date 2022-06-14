﻿using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;

namespace ZdravoCorpAppTim22.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretaryMedicalOrder.xaml
    /// </summary>
    public partial class SecretaryMedicalOrder : Window
    {
        SecretaryHome SecretaryHome;
        ObservableCollection<Equipment> observableListaEquipmenta = new ObservableCollection<Equipment>();
        public SecretaryMedicalOrder(SecretaryHome SecretaryHome)
        {
            InitializeComponent();
            this.SecretaryHome = SecretaryHome;
            comboBox.ItemsSource = EquipmentDataController.Instance.GetAllConsumable();
            dataGrid.ItemsSource = observableListaEquipmenta;
        }

        private void btnNewEquipement_Click(object sender, RoutedEventArgs e)
        {
            SecretaryNewEquipement secretaryNewEquipement = new SecretaryNewEquipement(this);
            secretaryNewEquipement.ShowDialog();
        }

        private void btnAddEquipementToOrder_Click(object sender, RoutedEventArgs e)
        {
            int amount = 0;
            try
            {
                amount = Convert.ToInt32(txtAmount.Text);
                if (amount <= 0)
                {
                    MessageBox.Show("Amount must be positive integer number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;

                }
            }
            catch
            {
                MessageBox.Show("Amount must be positive integer number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (comboBox.SelectedItem != null)
            {
                int indexFound = -1;
                for (int i = 0; i < observableListaEquipmenta.Count; i++)
                {

                    if (observableListaEquipmenta[i].EquipmentData == (EquipmentData)comboBox.SelectedItem)
                    {
                        indexFound = i;
                        break;
                    }
                }

                if (indexFound != -1)
                {
                    observableListaEquipmenta[indexFound].Amount += amount;
                    dataGrid.Items.Refresh();
                }
                else
                {
                    Equipment equipmentTemp = new Equipment()
                    {
                        EquipmentData = (EquipmentData)comboBox.SelectedItem,
                        Amount = amount
                    };
                    observableListaEquipmenta.Add(equipmentTemp);

                }
            }
            else
            {
                MessageBox.Show("Must select equipment from combo box!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }


        private void Window_Closed(object sender, System.EventArgs e)
        {
            SecretaryHome.Show();
        }

        private void btnSendOrderClick(object sender, RoutedEventArgs e)
        {
            if (observableListaEquipmenta.Count > 0)
            {
                Interval intervalTemp = new Interval();
                intervalTemp.End = DateTime.Now.AddSeconds(30);
                intervalTemp.Start = DateTime.Now;

                List<Equipment> ListaEquipmenta = new List<Equipment>();
                for (int i = 0; i < observableListaEquipmenta.Count; i++)
                {
                    ListaEquipmenta.Add(observableListaEquipmenta[i]);
                }

                EquipmentRelocationController.Instance.Create(null, null, intervalTemp, ListaEquipmenta);
                MessageBox.Show("Order sent!");
                SecretaryHome.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Order is empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRemoveFromOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                observableListaEquipmenta.Remove((Equipment)dataGrid.SelectedItem);
                dataGrid.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Must select item in table that you want to delete", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            SecretaryHome.Show();
            this.Close();
        }

        private void Accounts_Click(object sender, RoutedEventArgs e)
        {
            SecretaryAccounts secretaryAccounts = new SecretaryAccounts(SecretaryHome);
            secretaryAccounts.Show();
            this.Close();
        }

        private void Schedule_Click(object sender, RoutedEventArgs e)
        {
            SecretaryScheduleOptions secretaryScheduleOptions = new SecretaryScheduleOptions(SecretaryHome);
            secretaryScheduleOptions.Show();
            this.Close();
        }

        private void Medical_order_Click(object sender, RoutedEventArgs e)
        {
            SecretaryMedicalOrder secretaryMedicalOrder = new SecretaryMedicalOrder(SecretaryHome);
            secretaryMedicalOrder.Show();
            this.Close();
        }

        private void Emergency_Click(object sender, RoutedEventArgs e)
        {
            SecretaryEmergencyOptions secretaryEmergency = new SecretaryEmergencyOptions(SecretaryHome);
            secretaryEmergency.Show();
            this.Close();
        }

        private void Requests_Click(object sender, RoutedEventArgs e)
        {
            SecretaryMVVMfolder.SecretaryMVVMwindow secretaryMVVMwindow = new SecretaryMVVMfolder.SecretaryMVVMwindow(SecretaryHome, true);
            secretaryMVVMwindow.Show();
            this.Close();
        }

        private void btnGraph_Click(object sender, RoutedEventArgs e)
        {
            SecretaryGraph secretaryGraph = new SecretaryGraph(this);
            secretaryGraph.ShowDialog();
            this.Close();
        }
    }
}
