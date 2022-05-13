using Microsoft.EntityFrameworkCore;
using Snacker.Domain.Entities;
using Snacker.Infrastructure.Mapping;

namespace Snacker.Infrastructure.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {

        }

        public DbSet<Address> Adresses { get; set; }
        public DbSet<RestaurantCategory> RestaurantCategories { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Address>(new AddressMap().Configure);
            modelBuilder.Entity<RestaurantCategory>(new RestaurantCategoryMap().Configure);
            modelBuilder.Entity<Restaurant>(new RestaurantMap().Configure);
            modelBuilder.Entity<Person>(new PersonMap().Configure);
            modelBuilder.Entity<Table>(new TableMap().Configure);
            modelBuilder.Entity<ProductCategory>(new ProductCategoryMap().Configure);
            modelBuilder.Entity<UserType>(new UserTypeMap().Configure);
            modelBuilder.Entity<User>(new UserMap().Configure);
            modelBuilder.Entity<Product>(new ProductMap().Configure);
        }
    }
}