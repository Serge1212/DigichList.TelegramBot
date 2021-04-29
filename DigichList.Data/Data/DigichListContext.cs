using DigichList.Core.Entities;
using DigichList.Core.Entities.Base;
using DigichList.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DigichList.Infrastructure.Data
{
    public class DigichListContext : DbContext
    {
        public DigichListContext(DbContextOptions<DigichListContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Technician> Technicians { get;set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Defect> Defects { get; set; }
        public DbSet<DefectImage> DefectImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DefectEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RegularUserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DefectImageEntityConfiguration());
        }

    }
}
