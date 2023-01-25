﻿using DataBaseModel.DTOModels;
using DataBaseModel.ViewModel;
using System.Windows;

namespace UserInterface.CRUDWindows
{
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
            var newSeller = new SellerDto { FullName = nameTextBox.Text };
            DBContextViewModel.AddNewSeller(newSeller);
            Close();
        }

        private void cancleButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
