using DataBaseModel.DTOModels;
using DataBaseModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UserInterface.CRUDWindows
{
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
