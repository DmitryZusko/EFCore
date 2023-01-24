namespace DataBaseModel.Data
{
    using AutoMapper;
    using DataBaseModel.DTOModels;
    using DataBaseModel.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Cryptography.X509Certificates;

    public class DatabaseContext : DbContext
    {
        private readonly IMapper _maper;
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Customer> Customers{ get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=SimpleShop;Integrated Security=True; Encrypt=false");
        }

        public void PopulateDataBase()
        {
            Sellers.AddRangeAsync(new Seller { LastName = "Joestar", FirstName = "George" },
                new Seller { LastName = "Rodrigez", FirstName = "Michelle" });
            Customers.AddRangeAsync(new Customer { CompanyName = "Target" },
                new Customer { CompanyName = "Hamlin, Hanlin & McGill" });
            SaveChangesAsync();
            Orders.AddRangeAsync(
                new Order
                {
                    OrderDate = DateTime.UtcNow,
                    Amount = 87.225M,
                    SellerId = 1,
                    CustomerId = 1
                },
                new Order
                {
                    OrderDate = DateTime.UtcNow,
                    Amount = 8.2M,
                    SellerId = 1,
                    CustomerId = 2
                },
                new Order
                {
                    OrderDate = DateTime.UtcNow,
                    Amount = 231321.0M,
                    SellerId = 2,
                    CustomerId = 2
                });
            SaveChangesAsync();
        }

        public IQueryable<SellerDto> GetSellers()
        {
            return this.Sellers.Select(s => _maper.Map<SellerDto>(s));
        }

        public IQueryable<CustomerDto> GetCustomers()
        {
            return this.Customers.Select(c => _maper.Map<CustomerDto>(c));
        }

        public IQueryable<OrderDto> GetOrders()
        {
            return this.Orders.Select(o => _maper.Map<OrderDto>(o));
        }

        public IQueryable<OrderDetailDto> GetOrder(int id)
        {
            return this.Orders.Select(o => _maper.Map<OrderDetailDto>(o));
        }
    }
}
