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

            builder.Property(prop => prop.Color)
             .HasConversion(prop => prop.ToString(), prop => prop)
             .IsRequired()
             .HasColumnName("color")
             .HasColumnType("varchar(45)");

            builder.Property(prop => prop.SecondaryColor)
             .HasConversion(prop => prop.ToString(), prop => prop)
             .IsRequired()
             .HasColumnName("secondary_color")
             .HasColumnType("varchar(45)");

            builder.Property(prop => prop.FontColor)
             .HasConversion(prop => prop.ToString(), prop => prop)
             .IsRequired()
             .HasColumnName("font_color")
             .HasColumnType("varchar(45)");

            builder.Property(prop => prop.SecondaryFontColor)
             .HasConversion(prop => prop.ToString(), prop => prop)
             .IsRequired()
             .HasColumnName("secondary_font_color")
             .HasColumnType("varchar(45)");

            builder.Property(prop => prop.TertiaryFontColor)
            .HasConversion(prop => prop.ToString(), prop => prop)
            .IsRequired()
            .HasColumnName("tertiary_font_color")
            .HasColumnType("varchar(45)");

            builder.Property(prop => prop.Icon)
             .HasConversion(prop => prop.ToString(), prop => prop)
             .IsRequired()
             .HasColumnName("icon")
             .HasColumnType("LONGTEXT");
        }
    }
}
