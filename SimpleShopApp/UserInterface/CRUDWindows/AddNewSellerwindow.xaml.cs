namespace UserInterface.CRUDWindows
{
    using DataBaseModel.ViewModel;
    using System.Windows;
    /// <summary>
    /// Interaction logic for AddNewSellerwindow.xaml
    /// </summary>
    public partial class AddNewSellerWindow : Window
    {
        public DBContextViewModel DBContextViewModel { get; set; }
        public AddNewSellerWindow(DBContextViewModel dbContext)
        {
            InitializeComponent();
            DBContextViewModel = dbContext;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            DBContextViewModel.AddNewSeller(nameTextBox.Text);
            Close();
        }

        private void cancleButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
