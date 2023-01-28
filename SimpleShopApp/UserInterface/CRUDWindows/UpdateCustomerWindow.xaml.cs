namespace UserInterface.CRUDWindows
{
    using DataBaseModel.DTOModels;
    using DataBaseModel.ViewModel;
    using System.Windows;
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
