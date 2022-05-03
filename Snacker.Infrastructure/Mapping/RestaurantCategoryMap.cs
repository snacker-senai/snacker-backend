using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snacker.Domain.Entities;

namespace Snacker.Infrastructure.Mapping
{
    public class RestaurantCategoryMap : IEntityTypeConfiguration<RestaurantCategory>
    {
        public void Configure(EntityTypeBuilder<RestaurantCategory> builder)
        {
            builder.ToTable("restaurant_category");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Name)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType("varchar(45)");
        }
    }
}
