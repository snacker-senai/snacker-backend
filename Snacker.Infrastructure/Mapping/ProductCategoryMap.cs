using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snacker.Domain.Entities;

namespace Snacker.Infrastructure.Mapping
{
    public class ProductCategoryMap : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("product_category");

            builder.HasKey(prop => prop.Id);

            builder.HasOne(prop => prop.Restaurant).WithMany(a => a.ProductCategories).HasForeignKey(prop => prop.RestaurantId);
            builder.Property(prop => prop.RestaurantId)
               .IsRequired()
               .HasColumnName("restaurant_id");

            builder.Property(prop => prop.Name)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType("varchar(255)");
        }
    }
}
