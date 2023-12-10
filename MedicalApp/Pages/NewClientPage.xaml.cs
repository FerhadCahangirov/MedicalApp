using DBConnection;
using MedicalApp.Business;
using MedicalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MedicalApp.Pages
{
    /// <summary>
    /// Interaction logic for NewClientPage.xaml
    /// </summary>
    public partial class NewClientPage : Page
    {
        public NewClientPage()
        {
            InitializeComponent();
        }

        public bool gender;

        private void clientGenderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if(comboBox != null)
            {
                ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;

                if(selectedItem != null)
                {
                    string selectedValue = selectedItem.Content.ToString();
                    if (selectedValue.ToLower() == "male")
                        gender = true;
                    else if (selectedValue.ToLower() == "female")
                        gender = false;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            bool validation = true;

            string[] values =
            {
                clientFullNameTextBox.Text,
                clientBirthDateTextBox.Text,
                clientPhoneNumberTextBox.Text,
                clientComplaintTextBox.Text,
                clientHarmfullHabitatsTextBox.Text,
                clientAdviceTextBox.Text,
                DateTime.Now.ToString("f")
            }; 

            foreach(string value in values)
            {
                if (String.IsNullOrEmpty(value))
                {
                    validation = false;
                }
            }

            if(validation)
            {
                Client client = new Client()
                {
                    ClientID = Guid.NewGuid(),
                    ClientFullName = clientFullNameTextBox.Text,
                    ClientBirthDate = clientBirthDateTextBox.Text,
                    ClientPhoneNumber = clientPhoneNumberTextBox.Text,
                    ClientGender = gender,
                    ClientComplaint = clientComplaintTextBox.Text,
                    ClientHarmfullHabitats = clientHarmfullHabitatsTextBox.Text,
                    ClientAdviceText = clientAdviceTextBox.Text,
                    ClientApplyDate = DateTime.Now.ToString("f")
                };

                try
                {
                    int rowsAffected = ClientBusiness.AddClient(client);
                    MessageBox.Show("Client Added Successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
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
            else
            {
                MessageBox.Show("Please fill all the textboxes!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            
        }
    }
}
