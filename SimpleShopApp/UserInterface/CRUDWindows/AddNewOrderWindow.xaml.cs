using DataBaseModel.DTOModels;
using DataBaseModel.ViewModel;
using System;
using System.Linq;
using System.Windows;

namespace UserInterface.CRUDWindows
{
    /// <summary>
    /// Interaction logic for AddNewOrderWindow.xaml
    /// </summary>
    public partial class AddNewOrderWindow : Window
    {
        public DBContextViewModel DBContextVM { get; set; }
        public AddNewOrderWindow(DBContextViewModel context)
        {
            InitializeComponent();

            DBContextVM = context;
            sellerListBox.ItemsSource = DBContextVM.Sellers.Select(s => s.FullName);
            sellerListBox.SelectedIndex = 0;
            customerListBox.ItemsSource = DBContextVM.Customers.Select(c => c.Company);
            customerListBox.SelectedIndex = 0;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            DBContextVM.AddNewOrder(amountTextBox.Text, sellerListBox.SelectedItem.ToString(), customerListBox.SelectedItem.ToString());
            Close();
        }

        private void cancleButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
