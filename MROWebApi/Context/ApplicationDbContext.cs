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

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Field> Field { get; set; }
        public DbSet<FieldCustomerMap> FieldCustomerMap { get; set; }
        public DbSet<Wizard> Wizard { get; set; }
        public DbSet<WizardCustomerMap> WizardCustomerMap { get; set; }
        public DbSet<AdminUser> AdminUser { get; set; }

    }
}