using Microsoft.EntityFrameworkCore;
using MROPOC.Context;
using MROWebAPI.Context;

namespace CodeFirstMigration.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.seed();
        }

        public DbSet<Facility> Facility { get; set; }
        public DbSet<Field> Field { get; set; }
        public DbSet<FieldFacilityMap> FieldFacilityMap { get; set; }
        public DbSet<Wizard> Wizard { get; set; }
        public DbSet<AdminUser> AdminUser { get; set; }

    }
}