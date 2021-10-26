using Core.DataAccess;
using Core.Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Configurations;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class CarRentalContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionService.connectionString, npgsqlOptions =>
            {
                npgsqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: System.TimeSpan.FromSeconds(30), errorCodesToAdd: null);
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BrandEntityConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarEntityConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarImageEntityConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ColorEntityConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerEntityConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OperationClaimEntityConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RentalEntityConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEntityConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserOperationClaimEntityConfiguration).Assembly);
        }
    }
}
