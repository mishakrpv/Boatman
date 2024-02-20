using Boatman.Entities.Models.OwnerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boatman.DataAccess.Domain.Implementations.Configurations;

public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder.Property(o => o.FirstName)
            .HasMaxLength(50);

        builder.Property(o => o.MiddleName)
            .HasMaxLength(50);

        builder.Property(o => o.LastName)
            .HasMaxLength(50);

        builder.Property(o => o.Bio)
            .HasMaxLength(250);
    }
}