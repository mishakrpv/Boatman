using System.Reflection;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Entities.Models.FavoritesAggregate;
using Boatman.Entities.Models.ProfilePhotoAggregate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Boatman.DataAccess.Implementations.EntityFramework.Identity;

public class ApplicationContext  : IdentityDbContext<ApplicationUser>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<Apartment> Apartments { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Favorites> Favorites { get; set; }
    public DbSet<FavoriteItem> FavoriteItems { get; set; }
    public DbSet<ProfilePhoto> ProfilePhotos { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}