using ChronoPost.Core.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChronoPost.Infrastructure.Persistence.Data.EntityConfigurations;

public sealed class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(p => p.Id);

        builder.OwnsOne(p => p.UserCredentials, p =>
        {
            p.Property(pp => pp.Username).HasColumnName("UserCredential_Username");
            p.Property(pp => pp.Password).HasColumnName("UserCredential_Password");
            p.HasIndex(pp => new { pp.Username, pp.Password })
                .HasDatabaseName("IX_User_UserCredentials");
        });

        builder.Metadata.FindNavigation(nameof(User.UserCredentials))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}