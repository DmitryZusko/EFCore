﻿namespace DataBaseModel.Data
{
    using DataBaseModel.DTOModels;
    using DataBaseModel.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Cryptography.X509Certificates;

    public class DatabaseContext : DbContext
    {
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
            return this.Sellers.Select(s => new SellerDto { Id = s.Id, LastName = s.LastName, FirstName = s.FirstName });
        }

        public IQueryable<CustomerDto> GetCustomers()
        {
            return this.Customers.Select(c => new CustomerDto { Id = c.Id, CompanyName = c.CompanyName, });
        }

        public IQueryable<OrderDto> GetOrders()
        {
            return this.Orders.Select(o => new OrderDto { Id = o.Id, OrderDate = o.OrderDate, Amount = o.Amount });
        }

        public IQueryable<OrderDetailDto> GetOrder(int id)
        {
            var fullOrder = this.Orders
                .Include(o => o.Seller)
                .Include(o => o.Customer)
                .Select(o => new OrderDetailDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    Amount = o.Amount,
                    SellerLastName = o.Seller.LastName,
                    SellerFirstName = o.Seller.FirstName,
                    CustomerCompanyName = o.Customer.CompanyName,
                })
                .Where(o => o.Id == id);

            return fullOrder;
        }
    }
}
