using System.Reflection;
using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.Entities.Models.CustomerAggregate;
using Boatman.Entities.Models.OwnerAggregate;
using Boatman.Entities.Models.WishlistAggregate;
using Microsoft.EntityFrameworkCore;

namespace Boatman.DataAccess.Domain.Implementations;

public class DomainContext : DbContext
{
    public DomainContext(DbContextOptions<DomainContext> options) : base(options)
    {
    }

    public DbSet<Apartment> Apartments { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<Viewing> Viewings { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }
    public DbSet<WishlistItem> WishlistItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}