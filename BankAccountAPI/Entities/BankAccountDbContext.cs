using Microsoft.EntityFrameworkCore;

namespace BankAccountAPI.Entities
{
    public class BankAccountDbContext : DbContext
    {
        public BankAccountDbContext(DbContextOptions<BankAccountDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>(ts =>
            {
                ts.Property(t => t.TransactionAmount).HasPrecision(14,2);
                ts.Property(t => t.TransactionDate).HasDefaultValueSql("getdate()");
                ts.Property(t => t.AccounNumber).IsRequired();
            });
        }
    }
}
