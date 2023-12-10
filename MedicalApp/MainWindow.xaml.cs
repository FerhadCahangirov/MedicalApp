using MedicalApp.Business;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MedicalApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int counter = 0;


        public MainWindow()
        {
            InitializeComponent();
        } 

        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            if (counter % 2 == 0)
            {
                navBox.Visibility = Visibility.Hidden;
            }
            else
            {
                navBox.Visibility = Visibility.Visible;
            }

            counter++;
        }

        public void clearActivation()
        {
            Button[] buttons = {dashboardBtn, newClientBtn, accountInfoBtn, settingsBtn };

            foreach (Button button in buttons)
            {
                if (button.Style == (Style)Application.Current.Resources["activenavButton"])
                    button.Style = (Style)Application.Current.Resources["navButton"];
            }
        }

        private void dashboardBtn_Click(object sender, RoutedEventArgs e)
        {
            this.mainFrame.Source = new Uri("Pages/MainPage.xaml", UriKind.Relative);
            clearActivation();
            dashboardBtn.Style = (Style)Application.Current.Resources["activenavButton"];
        }

        private void newClientBtn_Click(object sender, RoutedEventArgs e)
        {
            this.mainFrame.Source = new Uri("Pages/NewClientPage.xaml", UriKind.Relative);
            clearActivation();
            newClientBtn.Style = (Style)Application.Current.Resources["activenavButton"];
        }

        private void accountInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            this.mainFrame.Source = new Uri("Pages/AccountInfoPage.xaml", UriKind.Relative);
            clearActivation();
            accountInfoBtn.Style = (Style)Application.Current.Resources["activenavButton"];
        }

        private void settingsBtn_Click(object sender, RoutedEventArgs e) 
        {
            this.mainFrame.Source = new Uri("Pages/SettingsPage.xaml", UriKind.Relative);
            clearActivation();
            settingsBtn.Style = (Style)Application.Current.Resources["activenavButton"];
        }

    }

    public class ConvertIsEmptyToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (String.IsNullOrEmpty(value.ToString())) return true;
            else return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}

