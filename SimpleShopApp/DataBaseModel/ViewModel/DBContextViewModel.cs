using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataBaseModel.Data;
using DataBaseModel.DatabaseModels;
using DataBaseModel.DTOModels;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace DataBaseModel.ViewModel
{
    public class DBContextViewModel : INotifyPropertyChanged
    {
        private MapperConfiguration _projectionConfig;
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
        public OrderDetailDto DetailedOrderDto { get; set; }


        public DBContextViewModel(DatabaseContext context)
        {
            DBContext = context;

            _projectionConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateProjection<Seller, SellerDto>();
                cfg.CreateProjection<Customer, CustomerDto>();
                cfg.CreateProjection<Order, OrderDto>();
                cfg.CreateProjection<Order, OrderDetailDto>()
                .ForMember(dto => dto.SellerFullName, conf => conf.MapFrom(o => o.Seller.FullName))
                .ForMember(dto => dto.CustomerCompany, conf => conf.MapFrom(o => o.Customer.Company));
            });

            var mapperconfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Seller, SellerDto>().ReverseMap();
                cfg.CreateMap<Customer, CustomerDto>().ReverseMap();
                cfg.CreateMap<Order, OrderDto>().ReverseMap();
                cfg.CreateMap<Order, OrderDetailDto>().ReverseMap();
            });

            _mapper = new Mapper(mapperconfig);

            Sellers = new ObservableCollection<SellerDto>(DBContext.Sellers.ProjectTo<SellerDto>(_projectionConfig));
            Customers = new ObservableCollection<CustomerDto>(DBContext.Customers.ProjectTo<CustomerDto>(_projectionConfig));
            Orders = new ObservableCollection<OrderDto>(DBContext.Orders.ProjectTo<OrderDto>(_projectionConfig));
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public OrderDetailDto ShowDetailedOrder(object selectedItem)
        {
            var detailedOrder = (OrderDto)selectedItem;
            return DBContext.Orders.Where(o => o.Id == detailedOrder.Id).ProjectTo<OrderDetailDto>(_projectionConfig).FirstOrDefault();
        }

        public void AddNewSeller(string newSellerName)
        {
            if (newSellerName == null || newSellerName.Count() < 1)
            {
                return;
            }

            var newSeller = new Seller { FullName = newSellerName };
            DBContext.Sellers.Add(newSeller);
            DBContext.SaveChanges();
            Sellers.Add(_mapper.Map<SellerDto>(DBContext.Sellers.Where(s => s.FullName == newSellerName).FirstOrDefault()));
        }

        public void AddNewCustomer(string newCustomerName)
        {
            if (newCustomerName == null || newCustomerName.Count() <1)
            {
                return;
            }

            var newCustomer = new Customer { Company = newCustomerName };
            DBContext.Customers.Add(newCustomer);
            DBContext.SaveChanges();
            Customers.Add(_mapper.Map<CustomerDto>(DBContext.Customers.Where(c => c.Company == newCustomerName).FirstOrDefault()));
        }

        public void AddNewOrder(string amount, string seller, string customer)
        {
            decimal orderAmount;
            decimal.TryParse(amount, out orderAmount);
            var newOrder = new Order
            {
                Amount = orderAmount,
                OrderDate = DateTime.UtcNow,
                SellerId = DBContext.Sellers.Where(s => s.FullName == seller).FirstOrDefault().Id,
                CustomerId = DBContext.Customers.Where(c => c.Company == customer).FirstOrDefault().Id
            };
            DBContext.Orders.Add(newOrder);
            DBContext.SaveChanges();
            Orders.Add(new ObservableCollection<OrderDto>(DBContext
                .Orders
                .Where(o => o.Id == newOrder.Id)
                .ProjectTo<OrderDto>(_projectionConfig)).FirstOrDefault());
        }

        public void DeleteSeller(object selectedItem)
        {
            var deletedSeller = (SellerDto)selectedItem;
            Sellers.Remove(deletedSeller);
            DBContext.Sellers.Remove(_mapper.Map<Seller>(deletedSeller));
            DBContext.SaveChanges();
        }

        public void DeleteCustomer(object selectedItem)
        {
            var deletedCustomer = (CustomerDto)selectedItem;
            Customers.Remove(deletedCustomer);
            DBContext.Customers.Remove(_mapper.Map<Customer>(deletedCustomer));
            DBContext.SaveChanges();
        }

        public void DeleteOrder(object selectedItem)
        {
            var deletedOrder = (OrderDto)selectedItem;
            Orders.Remove(deletedOrder);
            DBContext.Orders.Remove(_mapper.Map<Order>(deletedOrder));
            DBContext.SaveChanges();
        }
    }
}
