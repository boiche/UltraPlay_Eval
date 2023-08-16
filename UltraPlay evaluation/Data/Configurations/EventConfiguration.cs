using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UltraPlay_evaluation.Data.Entities;

namespace UltraPlay_evaluation.Data.Configurations
{
    public partial class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> entity)
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.Sport)
                .WithMany(p => p.Events)
                .HasForeignKey(d => d.SportID)
                .HasConstraintName("FK_Events_Sports")
                .OnDelete(DeleteBehavior.ClientSetNull);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Event> entity);
    }
}
