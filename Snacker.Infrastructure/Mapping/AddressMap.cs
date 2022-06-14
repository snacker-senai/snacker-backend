using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snacker.Domain.Entities;

namespace Snacker.Infrastructure.Mapping
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("address");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.CEP)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("cep")
                .HasColumnType("varchar(45)");

            builder.Property(prop => prop.Street)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("street")
                .HasColumnType("varchar(255)");

            builder.Property(prop => prop.State)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("state")
                .HasColumnType("varchar(255)");

            builder.Property(prop => prop.District)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("district")
                .HasColumnType("varchar(255)");

            builder.Property(prop => prop.City)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("city")
                .HasColumnType("varchar(255)");

            builder.Property(prop => prop.Number)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("number")
                .HasColumnType("varchar(45)");

            builder.Property(prop => prop.Country)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("country")
                .HasColumnType("varchar(45)");
        }
    }
}
