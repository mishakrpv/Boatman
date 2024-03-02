using Boatman.Entities.Models.ChatAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boatman.DataAccess.Implementations.EntityFramework.Identity.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasOne<Chat>()
            .WithMany(c => c.Messages)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(m => m.Content)
            .HasMaxLength(300);
        
        builder.Property(m => m.FromUserName)
            .HasMaxLength(70);
    }
}