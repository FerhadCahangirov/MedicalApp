using MedicalApp.Business;
using MedicalApp.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MedicalApp.Pages
{
    /// <summary>
    /// Interaction logic for UpdateClientPage.xaml
    /// </summary>
    public partial class UpdateClientPage : Page
    {
        private readonly string id;
        private bool gender;


        public UpdateClientPage(string id)
        {
            InitializeComponent();
            this.id = id;
            Client client = ClientBusiness.GetClient(id);

            clientFullNameTextBox.Text = client.ClientFullName;
            clientBirthDateTextBox.Text = client.ClientBirthDate;
            clientPhoneNumberTextBox.Text = client.ClientPhoneNumber;
            clientComplaintTextBox.Text = client.ClientComplaint;
            clientHarmfullHabitatsTextBox.Text = client.ClientHarmfullHabitats;
            clientAdviceTextBox.Text = client.ClientAdviceText;

            if(client.ClientGender)
                clientGenderComboBox.SelectedIndex = 0;
            else
                clientGenderComboBox.SelectedIndex = 1;
        }


        private void clientGenderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;

                if (selectedItem != null)
                {
                    string selectedValue = selectedItem.Content.ToString();
                    if (selectedValue.ToLower() == "male")
                        gender = true;
                    else if (selectedValue.ToLower() == "female")
                        gender = false;
                }
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Client client = new Client
            {
                ClientID = Guid.Parse(id),
                ClientFullName = clientFullNameTextBox.Text,
                ClientBirthDate = clientBirthDateTextBox.Text,
                ClientPhoneNumber = clientPhoneNumberTextBox.Text,
                ClientGender = gender,
                ClientComplaint = clientComplaintTextBox.Text,
                ClientHarmfullHabitats= clientHarmfullHabitatsTextBox.Text,
                ClientAdviceText= clientAdviceTextBox.Text,
            };

            try
            {
                ClientBusiness.UpdateClient(client);
                MessageBox.Show("Client Updated Succesfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }catch(Exception ex) 
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.mainFrame.Source = new Uri("Pages/MainPage.xaml", UriKind.Relative);
                mainWindow.clearActivation();
                mainWindow.dashboardBtn.Style = (Style)Application.Current.Resources["activenavButton"];
            }

        }
    }
}
