using Microsoft.EntityFrameworkCore;
using FinanceBackend.Models;

namespace FinanceBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<FinancialRecord> FinancialRecords { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FinancialRecord>()
                .HasOne(fr => fr.User)
                .WithMany()
                .HasForeignKey(fr => fr.userId);

            modelBuilder.Entity<FinancialRecord>()
                .HasOne(fr=>fr.Category)
                .WithMany()
                .HasForeignKey(fr=>fr.categoryId);
        }
    }
}
