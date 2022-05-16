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

            builder.HasOne(prop => prop.Order).WithMany(a => a.OrderHasProductCollection).HasForeignKey(prop => prop.OrderId);

            builder.HasOne(prop => prop.Product).WithMany(a => a.OrderHasProductCollection).HasForeignKey(prop => prop.ProductId);
        }
    }
}