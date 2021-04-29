using DigichList.Core.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigichList.Infrastructure.Configurations
{
    public class RegularUserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasMany(d => d.Defects)
                .WithOne(u => u.Publisher)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
