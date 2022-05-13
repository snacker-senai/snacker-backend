using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snacker.Domain.Entities;

namespace Snacker.Infrastructure.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.HasKey(prop => prop.Id);

            builder.HasOne(prop => prop.Person).WithOne(a => a.User).HasForeignKey<User>(prop => prop.PersonId);
            builder.Property(prop => prop.PersonId)
               .IsRequired()
               .HasColumnName("person_id");

            builder.HasOne(prop => prop.UserType).WithMany(a => a.Users).HasForeignKey(prop => prop.UserTypeId);
            builder.Property(prop => prop.UserTypeId)
                .IsRequired()
                .HasColumnName("user_type_id");

            builder.Property(prop => prop.Email)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("email")
                .HasColumnType("varchar(255)");

            builder.Property(prop => prop.Password)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("password")
                .HasColumnType("varchar(255)");
        }
    }
}
