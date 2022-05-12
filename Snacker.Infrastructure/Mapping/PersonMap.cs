using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snacker.Domain.Entities;

namespace Snacker.Infrastructure.Mapping
{
    public class PersonMap : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("person");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Name)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType("varchar(255)");

            builder.Property(prop => prop.BirthDate)
                .IsRequired()
                .HasColumnName("birth_date")
                .HasColumnType("date");

            builder.Property(prop => prop.Phone)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("phone")
                .HasColumnType("varchar(255)");

            builder.Property(prop => prop.Document)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .HasColumnName("document")
                .HasColumnType("varchar(255)");

            builder.HasOne(prop => prop.Address).WithOne(a => a.Person).HasForeignKey<Person>(prop => prop.AddressId);
            builder.Property(prop => prop.AddressId)
               .IsRequired()
               .HasColumnName("address_id");

            builder.HasOne(prop => prop.Restaurant).WithMany(a => a.Persons).HasForeignKey(prop => prop.RestaurantId);
            builder.Property(prop => prop.RestaurantId)
               .IsRequired()
               .HasColumnName("restaurant_id");
        }
    }
}
