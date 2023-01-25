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
            customerListBox.ItemsSource = DBContextVM.Customers.Select(c => c.Company);
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            decimal amount;
            decimal.TryParse(amountTextBox.Text, out amount);
            var newOrder = new OrderDetailDto
            {
                OrderDate = DateTime.UtcNow,
                Amount = amount,
                SellerFullName = sellerListBox.SelectedItem.ToString(),
                CustomerCompany = customerListBox.SelectedItem.ToString(),
            };
            DBContextVM.AddNewOrder(newOrder);
            Close();
        }

        private void cancleButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
