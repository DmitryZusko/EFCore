using DataBaseModel.ViewModel;
using System.Windows;

namespace UserInterface.CRUDWindows
{
    /// <summary>
    /// Interaction logic for OrderDetailedWindow.xaml
    /// </summary>
    public partial class OrderDetailedWindow : Window
    {
        public OrderDetailedWindow(DBContextViewModel dBContextVM, object selectedItem)
        {
            InitializeComponent();
            var detailedOrder = dBContextVM.ShowDetailedOrder(selectedItem);

            orderIdTextBlock.Text = $"# {detailedOrder.Id}";
            amountTextBlock.Text = detailedOrder.Amount.ToString();
            orderDateTextBlock.Text = $"{detailedOrder.OrderDate.ToShortDateString()} {detailedOrder.OrderDate.ToShortTimeString()}";
            sellerIdTextBlock.Text = $"# {detailedOrder.SellerId}";
            sellerTextBlock.Text = detailedOrder.SellerFullName.ToString();
            customerIdTextBlock.Text = $"# {detailedOrder.CustomerId}";
            customerTextBlock.Text = detailedOrder.CustomerCompany.ToString();
        }
    }
}
