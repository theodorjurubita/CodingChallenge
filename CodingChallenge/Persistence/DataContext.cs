using CodingChallenge.Domain;
using Microsoft.EntityFrameworkCore;

namespace CodingChallenge.Persistence
{
    public class DataContext : DbContext
    {
        public string DbPath { get; }

        public DataContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "nftdatabase.db");
        }

        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Token>()
                .HasKey(t => t.TokenId);

            modelBuilder.Entity<Token>()
                .HasOne(t => t.Wallet)
                .WithMany(w => w.Tokens);

            modelBuilder.Entity<Wallet>()
                .HasKey(w => w.Address);

            modelBuilder.Entity<Wallet>()
                .HasMany(w => w.Tokens)
                .WithOne(w => w.Wallet)
                .HasForeignKey(t => t.WalletAddress);
        }
    }
}
