using DigichList.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigichList.Infrastructure.Configurations
{
    public class TechnicianEntityConfiguration : IEntityTypeConfiguration<Technician>
    {
        public void Configure(EntityTypeBuilder<Technician> builder)
        {
            builder.HasMany(d => d.AssignedDefects)
                .WithOne(t => t.AssignedWorker)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.TechnicianId);
        }
    }
}
