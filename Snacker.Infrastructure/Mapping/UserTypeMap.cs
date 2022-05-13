using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snacker.Domain.Entities;

namespace Snacker.Infrastructure.Mapping
{
    public class UserTypeMap : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.ToTable("user_type");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Name)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType("varchar(255)");
        }
    }
}