using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snacker.Domain.Entities;

namespace Snacker.Infrastructure.Mapping
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("product");

            builder.HasKey(prop => prop.Id);

            builder.HasOne(prop => prop.ProductCategory).WithMany(a => a.Products).HasForeignKey(prop => prop.ProductCategoryId);
            builder.Property(prop => prop.ProductCategoryId)
                .IsRequired()
                .HasColumnName("produtct_category_id");

            builder.HasOne(prop => prop.Restaurant).WithMany(a => a.Products).HasForeignKey(prop => prop.RestaurantId);
            builder.Property(prop => prop.RestaurantId)
                .IsRequired()
                .HasColumnName("restaurant_id");

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

            builder.Property(prop => prop.Price)
                .IsRequired()
                .HasColumnName("price")
                .HasColumnType("decimal(10,0)");

            builder.Property(prop => prop.Image)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("image")
                .HasColumnType("varchar(255)");

            builder.Property(prop => prop.Active)
                .IsRequired()
                .HasColumnName("active")
                .HasColumnType("boolean");
        }
    }
}
