using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snacker.Domain.Entities;

namespace Snacker.Infrastructure.Mapping
{
    public class BillMap : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("bill");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Active)
                .IsRequired()
                .HasColumnName("active")
                .HasColumnType("boolean");

            builder.HasOne(prop => prop.Table).WithMany(a => a.Bills).HasForeignKey(prop => prop.TableId);
            builder.Property(prop => prop.TableId)
                .IsRequired()
                .HasColumnName("table_id");
        }
    }
}
