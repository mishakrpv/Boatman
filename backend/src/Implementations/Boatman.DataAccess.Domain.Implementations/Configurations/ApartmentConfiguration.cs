using Boatman.Entities.Models.ApartmentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boatman.DataAccess.Domain.Implementations.Configurations;

public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
{
    public void Configure(EntityTypeBuilder<Apartment> builder)
    {
        builder.Property(a => a.Rent)
            .HasPrecision(19, 4);

        builder.Property(a => a.Description)
            .HasMaxLength(300);
    }
}