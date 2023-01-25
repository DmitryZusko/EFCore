using AutoMapper;
using DataBaseModel.Data;
using DataBaseModel.DatabaseModels;
using DataBaseModel.DTOModels;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DataBaseModel.ViewModel
{
    public class DBContextViewModel : INotifyPropertyChanged
    {
        private IMapper _mapper;
        private ObservableCollection<SellerDto> _sellers;
        private ObservableCollection<CustomerDto> _customers;
        private ObservableCollection<OrderDto> _orders;
        private ObservableCollection<OrderDetailDto> _ordersDetailedInfo;

        public event PropertyChangedEventHandler? PropertyChanged;

        public DatabaseContext DBContext { get; set; }

        public ObservableCollection<SellerDto> Sellers
        {
            get => _sellers;
            set
            {
                _sellers = value;
                OnPropertyChanged("Sellers");
            }
        }
        public ObservableCollection<CustomerDto> Customers
        {
            get => _customers;
            set
            {
                _customers = value;
                OnPropertyChanged("Customers");
            }
        }
        public ObservableCollection<OrderDto> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged("Orders");
            }
        }
        public ObservableCollection<OrderDetailDto> OrderDetailedInfo { get; set; }


        public DBContextViewModel(DatabaseContext context)
        {
            DBContext = context;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Seller, SellerDto>().ReverseMap();
                cfg.CreateMap<Customer, CustomerDto>().ReverseMap();
                cfg.CreateMap<Order, OrderDto>().ReverseMap();
                cfg.CreateMap<Order, OrderDetailDto>().ReverseMap();
            });

            _mapper = new Mapper(config);

            Sellers = new ObservableCollection<SellerDto>(DBContext.Sellers.Select(s => _mapper.Map<SellerDto>(s)));
            Customers = new ObservableCollection<CustomerDto>(DBContext.Customers.Select(c => _mapper.Map<CustomerDto>(c)));
            Orders = new ObservableCollection<OrderDto>(DBContext.Orders.Select(o => _mapper.Map<OrderDto>(o)));
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void AddNewSeller(SellerDto newSeller)
        {
            Sellers.Add(newSeller);
            DBContext.Add(_mapper.Map<Seller>(newSeller));
            DBContext.SaveChanges();
        }

        public void AddNewCustomer(CustomerDto newCustomer)
        {
            Customers.Add(newCustomer);
            DBContext.Add(_mapper.Map<Customer>(newCustomer));
            DBContext.SaveChanges();
        }

        public void AddNewOrder(OrderDetailDto order)
        {
            var newOrder = new Order
            {
                Amount = order.Amount,
                OrderDate = order.OrderDate,
                SellerId = DBContext.Sellers.Where(s => s.FullName == order.SellerFullName).FirstOrDefault().Id,
                CustomerId = DBContext.Customers.Where(s => s.Company == order.CustomerCompany).FirstOrDefault().Id
            };
            Orders.Add(_mapper.Map<OrderDto>(newOrder));
            DBContext.Orders.Add(newOrder);
            DBContext.SaveChanges();
        }
    }
}
