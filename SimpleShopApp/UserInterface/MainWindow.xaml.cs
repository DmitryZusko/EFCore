namespace UserInterface
{
    using AutoMapper;
    using DataBaseModel.Data;
    using DataBaseModel.ViewModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using UserInterface.CRUDWindows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private DatabaseContext _dbContext;

        public DBContextViewModel DBContextVM { get; set; }
        public DatabaseItemSourseType ItemSourseType { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            using (_dbContext = new DatabaseContext())
            {
                if (_dbContext.Sellers.Count() == 0 || _dbContext.Sellers == null)
                {
                    _dbContext.PopulateDataBase();
                }

                DBContextVM = new DBContextViewModel();

                dataGrid.ItemsSource = DBContextVM.Sellers;
                ItemSourseType = DatabaseItemSourseType.seller;
            }
        }

        private void sellersButton_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = DBContextVM.Sellers;
            ItemSourseType = DatabaseItemSourseType.seller;
            orderFullInfoButton.Visibility = Visibility.Hidden; orderFullInfoButton.IsEnabled = false;
            updateButton.Visibility = Visibility.Visible; updateButton.IsEnabled = true;
        }

        private void customersButton_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = DBContextVM.Customers;
            ItemSourseType = DatabaseItemSourseType.customer;
            orderFullInfoButton.Visibility = Visibility.Hidden; orderFullInfoButton.IsEnabled = false;
            updateButton.Visibility = Visibility.Visible; updateButton.IsEnabled = true;
        }

        private void ordersButton_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = DBContextVM.Orders;
            ItemSourseType = DatabaseItemSourseType.order;
            orderFullInfoButton.Visibility = Visibility.Visible; orderFullInfoButton.IsEnabled = true;
            updateButton.Visibility = Visibility.Hidden; updateButton.IsEnabled = false;
        }

        private void orderFullInfoButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
            {
                return;
            }
            new OrderDetailedWindow(DBContextVM, dataGrid.SelectedItem).Show();
        }

        private void addNewButton_Click(object sender, RoutedEventArgs e)
        {
            switch (ItemSourseType)
            {
                case DatabaseItemSourseType.seller:
                    new AddNewSellerWindow(DBContextVM).Show();
                    break;
                case DatabaseItemSourseType.customer:
                    new AddNewCustomerWindow(DBContextVM).Show();
                    break;
                case DatabaseItemSourseType.order:
                    new AddNewOrderWindow(DBContextVM).Show();
                    break;
                default:
                    break;
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
            {
                return;
            }

            switch (ItemSourseType)
            {
                case DatabaseItemSourseType.seller:
                    new UpdateSellerWindow(DBContextVM, dataGrid.SelectedItem).Show();
                    break;
                case DatabaseItemSourseType.customer:
                    new UpdateCustomerWindow(DBContextVM, dataGrid.SelectedItem).Show();
                    break;
                default:
                    break;
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
            {
                return;
            }

            switch (ItemSourseType)
            {
                case DatabaseItemSourseType.seller:
                    DBContextVM.DeleteSeller(dataGrid.SelectedItem);
                    break;
                case DatabaseItemSourseType.customer:
                    DBContextVM.DeleteCustomer(dataGrid.SelectedItem);
                    break;
                case DatabaseItemSourseType.order:
                    DBContextVM.DeleteOrder(dataGrid.SelectedItem);
                    break;
                default:
                    break;
            }
        }
    }
}
