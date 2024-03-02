using Boatman.Entities.Models.ChatAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boatman.DataAccess.Implementations.EntityFramework.Identity.Configurations;

public class AttachedFileConfiguration : IEntityTypeConfiguration<AttachedFile>
{
    public void Configure(EntityTypeBuilder<AttachedFile> builder)
    {
        builder.HasOne<Message>()
            .WithMany(m => m.Files)
            .OnDelete(DeleteBehavior.Cascade);
    }
}