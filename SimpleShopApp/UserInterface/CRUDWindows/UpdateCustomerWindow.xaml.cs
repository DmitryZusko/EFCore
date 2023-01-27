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
    /// Interaction logic for UpdateCustomerWindow.xaml
    /// </summary>
    public partial class UpdateCustomerWindow : Window
    {
        public DBContextViewModel DBContextVM { get; set; }
        public CustomerDto UpdatedCustomer { get; set; }
        public UpdateCustomerWindow(DBContextViewModel context, object selectedItem)
        {
            InitializeComponent();
            DBContextVM = context;
            UpdatedCustomer = (CustomerDto)selectedItem;
            customerIdTextBlock.Text = $"# {UpdatedCustomer.Id}";
            customerNameTextBox.Text = UpdatedCustomer.Company;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            UpdatedCustomer.Company = customerNameTextBox.Text;
            DBContextVM.UpdateCustomer(UpdatedCustomer);
            Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
