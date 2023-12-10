using MedicalApp.Business;
using MedicalApp.Dialogs;
using MedicalApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    /// 

    //<Button Style = "{StaticResource pagingButton}" Content="1"/>
    //                <Button Style = "{StaticResource activePagingButton}" Content="2"/>
    //                <Button Style = "{StaticResource pagingButton}" Content="3"/>

    //                <TextBlock Text = "..." Foreground="#6C7682" VerticalAlignment="Center" Margin="10 0" FontSize="15" Cursor="Hand"/>

    //                <Button Style = "{StaticResource pagingButton}" Content="7"/>
    //                <Button Style = "{StaticResource pagingButton}" Content="8"/>
    //                <Button Style = "{StaticResource pagingButton}" Content="9"/>

    public partial class MainPage : Page
    {

        private List<Button> paginationButtons = new List<Button>();
        private readonly Style paginationButtonStyle = (Style)Application.Current.FindResource("pagingButton");
        private readonly Style activePaginationButtonStyle = (Style)Application.Current.FindResource("activePagingButton");
        private readonly int size = 15;

        public MainPage()
        {
            InitializeComponent();

            //paginationButtonsContainer

            try
            {
                (List<Client> clients, int countOfClients) = ClientBusiness.GetClients(0, 15);
                string totalClients = ClientBusiness.GetNumberOfClients();
                clientDataGrid.ItemsSource = clients;
                clientNumberHeading.Text = String.Format("{0}/{1}", countOfClients, totalClients);

                for (int i = 0; i <= int.Parse(totalClients) / countOfClients; i++)
                {
                    if(i == 0)
                    {
                        Button button = new Button();
                        button.Style = activePaginationButtonStyle;
                        button.Content = i.ToString();
                        button.Click += paginationButton_Click;
                        paginationButtons.Add(button);
                    }
                    else
                    {
                        Button button = new Button();
                        button.Style = paginationButtonStyle;
                        button.Content = i.ToString();
                        button.Click += paginationButton_Click;
                        paginationButtons.Add(button);
                    }
                }

                foreach(Button button in paginationButtons)
                {
                    paginationButtonsContainer.Children.Add(button);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void paginationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            clearPagination_Activation();
            button.Style = activePaginationButtonStyle;

            (List<Client> clients, int countOfClients) = ClientBusiness.GetClients(int.Parse((string)button.Content), 15);
            string totalClients = ClientBusiness.GetNumberOfClients();
            clientDataGrid.ItemsSource = clients;
            clientNumberHeading.Text = String.Format("{0}/{1}", countOfClients, totalClients);

        }

        private void clearPagination_Activation()
        {
            foreach (Button paginationButton in paginationButtons)
            {
                if (paginationButton.Style == activePaginationButtonStyle)
                {
                    paginationButton.Style = paginationButtonStyle;
                }
            }
        }

        private void addClientBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

            mainWindow.mainFrame.Source = new Uri("Pages/NewClientPage.xaml", UriKind.Relative);
            mainWindow.clearActivation();
            mainWindow.newClientBtn.Style = (Style)Application.Current.Resources["activenavButton"];
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null && button.CommandParameter != null)
            {
                ClientDeleteDialog dialog = new ClientDeleteDialog();
                string id = button.CommandParameter.ToString();
                dialog.Tag = id;
                dialog.DeleteClicked += Delete_Clicked;
                dialog.CancelClicked += Cancel_Clicked;
                dialog.Visibility = Visibility.Visible;
                dialog.SetValue(Panel.ZIndexProperty, 999);
                mainGrid.Children.Add(dialog);
            }
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            ClientDeleteDialog dialog = sender as ClientDeleteDialog;
            dialog.Visibility = Visibility.Collapsed;
            mainGrid.Children.Remove(dialog);
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            ClientDeleteDialog dialog = sender as ClientDeleteDialog;
            string id = dialog.Tag.ToString();

            try
            {
                ClientBusiness.DeleteClient(id);
                MessageBox.Show("The client deleted successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                int page = 0;

                foreach(Button paginationButton in paginationButtons)
                {
                    if(paginationButton.Style == activePaginationButtonStyle)
                    {
                        page = (int)paginationButton.Content;
                    }
                }

                (List<Client> clients, int countOfClients) = ClientBusiness.GetClients(page, size);
                clientDataGrid.ItemsSource = clients;
                clientNumberHeading.Text = String.Format("{0}/{1}", countOfClients, ClientBusiness.GetNumberOfClients());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                dialog.Visibility = Visibility.Collapsed;
                mainGrid.Children.Remove(dialog);
            }

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string id = button.CommandParameter.ToString();

            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

            UpdateClientPage updateClientPage = new UpdateClientPage(id);
            mainWindow.mainFrame.Content = updateClientPage;
            mainWindow.clearActivation();
        }

        private void filterByFullNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string content = textBox.Text;

            if(content == "")
            {
                try
                {
                    (List<Client> clients, int countOfClients) = ClientBusiness.GetClients(0, 15);
                    string totalClients = ClientBusiness.GetNumberOfClients();
                    clientDataGrid.ItemsSource = clients;
                    clientNumberHeading.Text = String.Format("{0}/{1}", countOfClients, totalClients);
                    paginationButtonsContainer.Children.Clear();
                    paginationButtons.Clear();

                    for (int i = 0; i <= int.Parse(totalClients) / countOfClients; i++)
                    {
                        if (i == 0)
                        {
                            Button button = new Button();
                            button.Style = activePaginationButtonStyle;
                            button.Content = i.ToString();
                            button.Click += paginationButton_Click;
                            paginationButtons.Add(button);
                        }
                        else
                        {
                            Button button = new Button();
                            button.Style = paginationButtonStyle;
                            button.Content = i.ToString();
                            button.Click += paginationButton_Click;
                            paginationButtons.Add(button);
                        }
                    }

                    foreach (Button button in paginationButtons)
                    {
                        paginationButtonsContainer.Children.Add(button);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                int totalCountOfClients = int.Parse(ClientBusiness.GetClientsTotalCountByFilteredSearch(content));
                paginationButtonsContainer.Children.Clear();
                paginationButtons.Clear();


                for (int i = 0; i <= totalCountOfClients / size; i++)
                {
                    if (i == 0)
                    {
                        Button button = new Button();
                        button.Style = activePaginationButtonStyle;
                        button.Content = i.ToString();
                        button.Click += filteredPaginationButton_Click;
                        paginationButtons.Add(button);
                    }
                    else
                    {
                        Button button = new Button();
                        button.Style = paginationButtonStyle;
                        button.Content = i.ToString();
                        button.Click += filteredPaginationButton_Click;
                        paginationButtons.Add(button);
                    }
                }

                foreach (Button paginationButton in paginationButtons)
                {
                    paginationButtonsContainer.Children.Add(paginationButton);
                }

                (List<Client> clients, int countOfClients) = ClientBusiness.GetClientByFilteredSearch(content, 0, size);
                string totalClients = ClientBusiness.GetNumberOfClients();

                clientDataGrid.ItemsSource = clients;
                clientNumberHeading.Text = String.Format("{0}/{1}", countOfClients, totalClients);
            }

            
        }

        private void filteredPaginationButton_Click(object sender, EventArgs e)
        {
            clearPagination_Activation();

            Button button = sender as Button;
            button.Style = activePaginationButtonStyle;
            int page = int.Parse((string)button.Content);
            string totalClients = ClientBusiness.GetNumberOfClients();

            (List<Client> clients, int countOfClients) = ClientBusiness.GetClientByFilteredSearch((string)filterByFullNameBox.Text, page, size);
            clientDataGrid.ItemsSource = clients;
            clientNumberHeading.Text = String.Format("{0}/{1}", countOfClients, totalClients);

        }
    }

    public class BoolToGenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool genderBit = (bool)value;
            if (genderBit) return "Male";
            else return "Female";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ReduceStringCount : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string complainText = value.ToString();
            int textLength = complainText.Length;

            if (textLength >= 10) return String.Format("{0}...", complainText.Substring(0, 10));
            else return complainText;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
