using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snacker.Domain.Entities;

namespace Snacker.Infrastructure.Mapping
{
    public class OrderHasProductMap : IEntityTypeConfiguration<OrderHasProduct>
    {
        public void Configure(EntityTypeBuilder<OrderHasProduct> builder)
        {
            builder.ToTable("order_has_product");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Details)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("details")
                .HasColumnType("varchar(255)");

            builder.HasOne(prop => prop.Order).WithMany(a => a.OrderHasProductCollection).HasForeignKey(prop => prop.OrderId);
            builder.Property(prop => prop.OrderId)
                    .IsRequired()
                    .HasColumnName("order_id");

            builder.HasOne(prop => prop.Product).WithMany(a => a.OrderHasProductCollection).HasForeignKey(prop => prop.ProductId);
            builder.Property(prop => prop.ProductId)
                    .IsRequired()
                    .HasColumnName("product_id");

            builder.Property(prop => prop.Quantity)
                .IsRequired()
                .HasColumnName("quantity")
                .HasColumnType("int");
        }
    }
}