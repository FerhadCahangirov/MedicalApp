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

namespace MedicalApp.Dialogs
{
    /// <summary>
    /// Interaction logic for ClientDeleteDialog.xaml
    /// </summary>
    public partial class ClientDeleteDialog : UserControl
    {

        public ClientDeleteDialog()
        {
            InitializeComponent();
        }

        public event EventHandler DeleteClicked;
        public event EventHandler CancelClicked;

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteClicked?.Invoke(this, EventArgs.Empty);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            CancelClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
