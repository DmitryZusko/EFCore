namespace DataBaseModel.DTOModels
{
    public class OrderDetailDto : PropertyChangeNotifier
    {

        private int _id;
        private DateTime _orderDate;
        private decimal _amount;
        private string _sellerFullName;
        private int _sellerId;
        private string _customerCompany;
        private int _customerId;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public DateTime OrderDate
        {
            get => _orderDate;
            set
            {
                _orderDate = value;
                OnPropertyChanged(nameof(OrderDate));
            }
        }
        public decimal Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }
        public string SellerFullName
        {
            get => _sellerFullName;
            set
            {
                _sellerFullName = value;
                OnPropertyChanged(nameof(SellerFullName));
            }
        }
        public int SellerId
        {
            get => _sellerId;
            set
            {
                _sellerId = value;
                OnPropertyChanged(nameof(SellerId));
            }
        }
        public string CustomerCompany
        {
            get => _customerCompany;
            set
            {
                _customerCompany = value;
                OnPropertyChanged(nameof(CustomerCompany));
            }
        }
        public int CustomerId
        {
            get => _customerId;
            set
            {
                _customerId = value;
                OnPropertyChanged(nameof(CustomerId));
            }
        }

    }
}
