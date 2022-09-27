using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snacker.Domain.Entities;

namespace Snacker.Infrastructure.Mapping
{
    public class ThemeMap : IEntityTypeConfiguration<Theme>
    {
        public void Configure(EntityTypeBuilder<Theme> builder)
        {
            builder.ToTable("theme");

            builder.HasKey(prop => prop.Id);

            builder.HasOne(prop => prop.Restaurant).WithOne(a => a.Theme).HasForeignKey<Theme>(prop => prop.RestaurantId);
            builder.Property(prop => prop.RestaurantId)
                .IsRequired()
                .HasColumnName("restaurant_id");
        }
    }
}
