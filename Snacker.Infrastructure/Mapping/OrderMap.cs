using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snacker.Domain.Entities;

namespace Snacker.Infrastructure.Mapping
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("order");

            builder.HasKey(prop => prop.Id);

            builder.HasOne(prop => prop.Table).WithMany(a => a.Orders).HasForeignKey(prop => prop.TableId);
            builder.Property(prop => prop.TableId)
                .IsRequired()
                .HasColumnName("table_id");

            builder.HasOne(prop => prop.OrderStatus).WithMany(a => a.Orders).HasForeignKey(prop => prop.OrderStatusId);
            builder.Property(prop => prop.OrderStatusId)
                .IsRequired()
                .HasColumnName("order_status_id");

            builder.HasOne(prop => prop.Bill).WithMany(a => a.Orders).HasForeignKey(prop => prop.BillId);
            builder.Property(prop => prop.BillId)
                .IsRequired()
                .HasColumnName("bill_id");
        }
    }
}