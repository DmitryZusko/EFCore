using DataBaseModel.DTOModels;
using DataBaseModel.ViewModel;
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
using System.Windows.Shapes;

namespace UserInterface.CRUDWindows
{
    /// <summary>
    /// Interaction logic for AddNewCustomerWindow.xaml
    /// </summary>
    public partial class AddNewCustomerWindow : Window
    {
        public DBContextViewModel DBContext { get; set; }
        public AddNewCustomerWindow(DBContextViewModel context)
        {
            InitializeComponent();
            DBContext = context;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            DBContext.AddNewCustomer(companyNameTextBox.Text);
            Close();
        }
        private void cancleButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
