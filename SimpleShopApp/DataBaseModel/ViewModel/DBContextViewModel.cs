namespace DataBaseModel.ViewModel
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using DataBaseModel.Data;
    using DataBaseModel.DatabaseModels;
    using DataBaseModel.DTOModels;
    using System.Collections.ObjectModel;
    public class DBContextViewModel
    {
        private MapperConfiguration _projectionConfig;
        private IMapper _mapper;
        private DatabaseContext _dbContext;


        public ObservableCollection<SellerDto> Sellers { get; set; }
        public ObservableCollection<CustomerDto> Customers { get; set; }
        public ObservableCollection<OrderDto> Orders { get; set; }
        public OrderDetailDto DetailedOrderDto { get; set; }


        public DBContextViewModel()
        {
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
                cfg.CreateMap<OrderDto, OrderDetailDto>().ReverseMap();
            });

            _mapper = new Mapper(mapperconfig);

            using (_dbContext = new DatabaseContext())
            {
                if (_dbContext.Sellers.Count() == 0 || _dbContext.Sellers == null)
                {
                    _dbContext.PopulateDataBase();
                }
                Sellers = new ObservableCollection<SellerDto>(_dbContext.Sellers.ProjectTo<SellerDto>(_projectionConfig));
                Customers = new ObservableCollection<CustomerDto>(_dbContext.Customers.ProjectTo<CustomerDto>(_projectionConfig));
                Orders = new ObservableCollection<OrderDto>(_dbContext.Orders.ProjectTo<OrderDto>(_projectionConfig));
            }
        }

        public OrderDetailDto ShowDetailedOrder(object selectedItem)
        {
            var detailedOrder = (OrderDto)selectedItem;
            using (_dbContext = new DatabaseContext())
            {
                return _dbContext.Orders.Where(o => o.Id == detailedOrder.Id).ProjectTo<OrderDetailDto>(_projectionConfig).FirstOrDefault();
            }
        }

        public void AddNewSeller(string newSellerName)
        {
            if (newSellerName == null)
            {
                return;
            }

            var newSeller = new Seller { FullName = newSellerName };
            using (_dbContext = new DatabaseContext())
            {
                _dbContext.Sellers.Add(newSeller);
                _dbContext.SaveChanges();
                Sellers.Add(_mapper.Map<SellerDto>(_dbContext.Sellers.Where(s => s.FullName == newSellerName).FirstOrDefault()));
            }
        }

        public void AddNewCustomer(string newCustomerName)
        {
            if (newCustomerName == null)
            {
                return;
            }

            var newCustomer = new Customer { Company = newCustomerName };
            using (_dbContext = new DatabaseContext())
            {
                _dbContext.Customers.Add(newCustomer);
                _dbContext.SaveChanges();
                Customers.Add(_mapper.Map<CustomerDto>(_dbContext.Customers.Where(c => c.Company == newCustomerName).FirstOrDefault()));
            }
        }

        public void AddNewOrder(string amount, string seller, string customer)
        {
            decimal orderAmount;
            decimal.TryParse(amount, out orderAmount);
            using (_dbContext = new DatabaseContext())
            {
                var newOrder = new Order
                {
                    Amount = orderAmount,
                    OrderDate = DateTime.UtcNow,
                    SellerId = _dbContext.Sellers.Where(s => s.FullName == seller).FirstOrDefault().Id,
                    CustomerId = _dbContext.Customers.Where(c => c.Company == customer).FirstOrDefault().Id
                };
                _dbContext.Orders.Add(newOrder);
                _dbContext.SaveChanges();
                Orders.Add(new ObservableCollection<OrderDto>(_dbContext
                    .Orders
                    .Where(o => o.Id == newOrder.Id)
                    .ProjectTo<OrderDto>(_projectionConfig)).FirstOrDefault());
            }
        }

        public void UpdateSeller(SellerDto updatedSeller)
        {
            var oldSeller = Sellers.FirstOrDefault(s => s.Id == updatedSeller.Id);
            oldSeller = updatedSeller;
            using (_dbContext = new DatabaseContext())
            {
                var contextUpdateSeller = _dbContext.Sellers.FirstOrDefault(s => s.Id == oldSeller.Id);
                if (contextUpdateSeller != null)
                {
                    contextUpdateSeller.FullName = updatedSeller.FullName;
                }
                _dbContext.SaveChanges();
            }
        }

        public void UpdateCustomer(CustomerDto updatedCustomer)
        {
            var oldCustomer = Customers.FirstOrDefault(c => c.Id == updatedCustomer.Id);
            oldCustomer = updatedCustomer;
            using (_dbContext = new DatabaseContext())
            {
                var contextUpdatedCustomer = _dbContext.Customers.FirstOrDefault(c => c.Id == updatedCustomer.Id);
                if (contextUpdatedCustomer != null)
                {
                    contextUpdatedCustomer.Company = updatedCustomer.Company;
                }
                _dbContext.SaveChanges();
            }
        }

        public void UpdateOrder(OrderDetailDto updatedOrder)
        {
            var oldOrder = Orders.FirstOrDefault(o => o.Id == updatedOrder.Id);
            var oldOrderIndex = Orders.IndexOf(oldOrder);
            Orders[oldOrderIndex] = _mapper.Map<OrderDto>(updatedOrder);
            using (_dbContext = new DatabaseContext())
            {
                var contextOldOrder = _dbContext.Orders.FirstOrDefault(o => o.Id == oldOrder.Id);
                if (contextOldOrder != null)
                {
                    contextOldOrder.OrderDate = updatedOrder.OrderDate;
                    contextOldOrder.Amount = updatedOrder.Amount;
                    contextOldOrder.SellerId = updatedOrder.SellerId;
                    contextOldOrder.CustomerId = updatedOrder.CustomerId;
                }
                _dbContext.SaveChanges();
            }
        }

        public void DeleteSeller(object selectedItem)
        {
            var deletedSeller = (SellerDto)selectedItem;
            Sellers.Remove(deletedSeller);
            using (_dbContext = new DatabaseContext())
            {
                _dbContext.Sellers.Remove(_mapper.Map<Seller>(deletedSeller));
                _dbContext.SaveChanges();
            }
        }

        public void DeleteCustomer(object selectedItem)
        {
            var deletedCustomer = (CustomerDto)selectedItem;
            Customers.Remove(deletedCustomer);
            using (_dbContext = new DatabaseContext())
            {
                _dbContext.Customers.Remove(_mapper.Map<Customer>(deletedCustomer));
                _dbContext.SaveChanges();
            }
        }

        public void DeleteOrder(object selectedItem)
        {
            var deletedOrder = (OrderDto)selectedItem;
            Orders.Remove(deletedOrder);
            using (_dbContext = new DatabaseContext())
            {
                _dbContext.Orders.Remove(_mapper.Map<Order>(deletedOrder));
                _dbContext.SaveChanges();
            }
        }
    }
}
