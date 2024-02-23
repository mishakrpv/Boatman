using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boatman.DataAccess.Implementations.EntityFramework.Identity.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(c => c.FirstName)
            .HasMaxLength(50);

        builder.Property(c => c.MiddleName)
            .HasMaxLength(50);

        builder.Property(c => c.LastName)
            .HasMaxLength(50);

        builder.Property(c => c.Bio)
            .HasMaxLength(250);
    }
}