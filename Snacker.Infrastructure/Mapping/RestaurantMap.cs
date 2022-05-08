using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snacker.Domain.Entities;

namespace Snacker.Infrastructure.Mapping
{
    public class RestaurantMap : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.ToTable("restaurant");

            builder.HasKey(prop => prop.Id);

            builder.HasOne(prop => prop.Address).WithOne(a => a.Restaurant).HasForeignKey<Restaurant>(prop => prop.AddressId);
            builder.Property(prop => prop.AddressId)
               .IsRequired()
               .HasColumnName("address_id");

            builder.HasOne(prop => prop.RestaurantCategory).WithMany(a => a.Restaurants).HasForeignKey(prop => prop.RestaurantCategoryId);
            builder.Property(prop => prop.RestaurantCategoryId)
                .IsRequired()
                .HasColumnName("restaurant_category_id");

            builder.Property(prop => prop.Name)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType("varchar(255)");

            builder.Property(prop => prop.Description)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("description")
                .HasColumnType("varchar(255)");
        }
    }
}
