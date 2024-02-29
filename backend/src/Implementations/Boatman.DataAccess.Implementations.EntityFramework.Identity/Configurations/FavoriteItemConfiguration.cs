using Boatman.Entities.Models.FavoritesAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boatman.DataAccess.Implementations.EntityFramework.Identity.Configurations;

public class FavoriteItemConfiguration : IEntityTypeConfiguration<FavoriteItem>
{
    public void Configure(EntityTypeBuilder<FavoriteItem> builder)
    {
        builder.HasOne<Favorites>()
            .WithMany(f => f.Items)
            .OnDelete(DeleteBehavior.Cascade);
    }
}