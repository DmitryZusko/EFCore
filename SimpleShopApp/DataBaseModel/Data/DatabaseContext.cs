namespace DataBaseModel.Data
{
    using DataBaseModel.DatabaseModels;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class DatabaseContext : DbContext
    {
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=SimpleShop;Integrated Security=True; Encrypt=false");
        }

        public void PopulateDataBase()
        {
            Sellers.AddRange(new Seller { FullName = "George Joestar" },
                new Seller { FullName = "Michelle Rodrigez" });
            Customers.AddRange(new Customer { Company = "Target" },
                new Customer { Company = "Hamlin, Hamlin & McGill" });
            SaveChanges();
            Orders.AddRange(
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
            SaveChanges();
        }
    }
}
