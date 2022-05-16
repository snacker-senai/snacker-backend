using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snacker.Domain.Entities;

namespace Snacker.Infrastructure.Mapping
{
    public class BillHasOrderMap : IEntityTypeConfiguration<BillHasOrder>
    {
        public void Configure(EntityTypeBuilder<BillHasOrder> builder)
        {
            builder.ToTable("bill_has_order");

            builder.HasKey(prop => new { prop.BillId, prop.OrderId });

            builder.HasOne(prop => prop.Order).WithMany(a => a.BillHasOrderCollection).HasForeignKey(prop => prop.OrderId);
            builder.Property(prop => prop.OrderId)
                .IsRequired()
                .HasColumnName("order_id");

            builder.HasOne(prop => prop.Bill).WithMany(a => a.BillHasOrderCollection).HasForeignKey(prop => prop.BillId);
            builder.Property(prop => prop.BillId)
                .IsRequired()
                .HasColumnName("bill_id");
        }
    }
}