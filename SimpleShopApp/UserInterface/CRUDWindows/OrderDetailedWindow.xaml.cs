using DataBaseModel.DTOModels;
using DataBaseModel.ViewModel;
using System;
using System.Linq;
using System.Windows;

namespace UserInterface.CRUDWindows
{
    /// <summary>
    /// Interaction logic for OrderDetailedWindow.xaml
    /// </summary>
    public partial class OrderDetailedWindow : Window
    {
        public DBContextViewModel DBContextVM { get; set; }
        public OrderDetailDto DetailedOrder { get; set; }
        public OrderDetailedWindow(DBContextViewModel context, object selectedItem)
        {
            InitializeComponent();
            DBContextVM= context;
            DetailedOrder = DBContextVM.ShowDetailedOrder(selectedItem);

            orderIdTextBlock.Text = $"# {DetailedOrder.Id}";
            amountTextBlock.Text = DetailedOrder.Amount.ToString();
            orderDateTextBlock.Text = $"{DetailedOrder.OrderDate.ToShortDateString()} {DetailedOrder.OrderDate.ToShortTimeString()}";
            sellerIdTextBlock.Text = $"# {DetailedOrder.SellerId}";
            sellerTextBlock.Text = DetailedOrder.SellerFullName.ToString();
            customerIdTextBlock.Text = $"# {DetailedOrder.CustomerId}";
            customerTextBlock.Text = DetailedOrder.CustomerCompany.ToString();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            new UpdateOrderWindow(DBContextVM, DetailedOrder).Show();
            Close();
        }
    }
}
