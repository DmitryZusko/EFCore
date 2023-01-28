namespace UserInterface.CRUDWindows
{
    using DataBaseModel.DTOModels;
    using DataBaseModel.ViewModel;
    using System;
    using System.Linq;
    using System.Windows;
    /// <summary>
    /// Interaction logic for UpdateOrderWindow.xaml
    /// </summary>
    public partial class UpdateOrderWindow : Window
    {
        public DBContextViewModel DBContextVM { get; set; }
        public OrderDetailDto OldOrder { get; set; }
        public UpdateOrderWindow(DBContextViewModel context, OrderDetailDto oldOrder)
        {
            InitializeComponent();

            DBContextVM = context;
            OldOrder = oldOrder;
            orderIdTextBlock.Text = $"# {oldOrder.Id}";
            amountTextBlock.Text = oldOrder.Amount.ToString("0.000");
            orderDateTextBlock.Text = oldOrder.OrderDate.ToString();
            sellerListBox.ItemsSource = DBContextVM.Sellers.Select(s => s.FullName);
            sellerListBox.SelectedIndex = 0;
            customerListBox.ItemsSource = DBContextVM.Customers.Select(c => c.Company);
            customerListBox.SelectedIndex = 0;

        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            decimal newAmount;
            decimal.TryParse(amountTextBlock.Text, out newAmount);
            var newSeller = DBContextVM.Sellers.FirstOrDefault(s => s.FullName == sellerListBox.SelectedItem);
            var newCustomer = DBContextVM.Customers.FirstOrDefault(c => c.Company == customerListBox.SelectedItem);

            var updatedOrder = new OrderDetailDto
            {
                Id = OldOrder.Id,
                Amount = newAmount,
                OrderDate = DateTime.UtcNow,
                SellerFullName = newSeller.FullName,
                SellerId = newSeller.Id,
                CustomerCompany = newCustomer.Company,
                CustomerId = newCustomer.Id
            };

            DBContextVM.UpdateOrder(updatedOrder);
            Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
