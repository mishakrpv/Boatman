using Boatman.Entities.Models.ApartmentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boatman.DataAccess.Implementations.EntityFramework.Identity.Configurations;

public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.HasOne<Apartment>()
            .WithMany(a => a.Photos)
            .OnDelete(DeleteBehavior.Cascade);
    }
}