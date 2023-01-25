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

        public DBContextViewModel DBContextViewModel { get; set; }
        public DatabaseItemSourseType ItemSourseType { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            _dbContext = new DatabaseContext();

            if (_dbContext.Sellers.Count() == 0 || _dbContext.Sellers == null)
            {
                _dbContext.PopulateDataBase();
            }

            DBContextViewModel = new DBContextViewModel(_dbContext);

            dataGrid.ItemsSource = DBContextViewModel.Sellers;
            ItemSourseType = DatabaseItemSourseType.seller;

        }

        private void sellersButton_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = DBContextViewModel.Sellers;
            ItemSourseType = DatabaseItemSourseType.seller;
        }

        private void customersButton_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = DBContextViewModel.Customers;
            ItemSourseType = DatabaseItemSourseType.customer;
        }

        private void ordersButton_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = DBContextViewModel.Orders;
            ItemSourseType = DatabaseItemSourseType.order;
        }

        private void addNewButton_Click(object sender, RoutedEventArgs e)
        {
            switch (ItemSourseType)
            {
                case DatabaseItemSourseType.seller:
                    new AddNewSellerWindow(DBContextViewModel).Show();
                    break;
                case DatabaseItemSourseType.customer:
                    new AddNewCustomerWindow(DBContextViewModel).Show();
                    break;
                case DatabaseItemSourseType.order:
                    new AddNewOrderWindow(DBContextViewModel).Show();
                    break;
                default:
                    break;
            }
        }
    }
}
