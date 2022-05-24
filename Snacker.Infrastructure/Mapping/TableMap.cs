using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snacker.Domain.Entities;

namespace Snacker.Infrastructure.Mapping
{
    public class TableMap : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.ToTable("table");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Number)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("number")
                .HasColumnType("varchar(255)");

            builder.HasOne(prop => prop.Restaurant).WithMany(a => a.Tables).HasForeignKey(prop => prop.RestaurantId);
            builder.Property(prop => prop.RestaurantId)
               .IsRequired()
               .HasColumnName("restaurant_id");
        }
    }
}