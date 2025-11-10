using AccountingSystem.Models;
using AccountingSystem.Data.Configs;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Example: using a local SQL Server database
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-Q4HRE44;Database=AccountingDB;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        }
    }
}
