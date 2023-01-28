namespace UserInterface.CRUDWindows
{
    using DataBaseModel.DTOModels;
    using DataBaseModel.ViewModel;
    using System.Windows;
    /// <summary>
    /// Interaction logic for UpdateSellerWindow.xaml
    /// </summary>
    public partial class UpdateSellerWindow : Window
    {
        public DBContextViewModel DBContextVM { get; set; }
        public SellerDto UpdatedSeller { get; set; }
        public UpdateSellerWindow(DBContextViewModel dBContextVM, object selectedItem)
        {
            InitializeComponent();
            DBContextVM = dBContextVM;
            UpdatedSeller = (SellerDto)selectedItem;

            sellerIdTextBlock.Text = $"# {UpdatedSeller.Id}";
            sellerNameTextBox.Text = UpdatedSeller.FullName;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            UpdatedSeller.FullName = sellerNameTextBox.Text;
            DBContextVM.UpdateSeller(UpdatedSeller);
            Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
