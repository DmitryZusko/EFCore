namespace UserInterface.CRUDWindows
{
    using DataBaseModel.ViewModel;
    using System.Windows;
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
