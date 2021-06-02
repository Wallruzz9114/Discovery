using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Messaging;

namespace Data.Configurations
{
    internal class StoredMessageConfiguration : IEntityTypeConfiguration<StoredEvent>
    {
        public void Configure(EntityTypeBuilder<StoredEvent> builder)
        {
            builder.ToTable("StoredEvents");
            builder.HasKey(se => se.Id);
            builder.Property(se => se.CreatedAt).IsRequired();
            builder.Property(se => se.ProcessedAt);
            builder.Property(se => se.MessageType).HasMaxLength(200).IsRequired();
            builder.Property(se => se.Payload).IsRequired();
        }
    }
}