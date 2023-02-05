using Microsoft.EntityFrameworkCore;
using MusicMarketKursach.Models.Entitys;
using System.Configuration;

namespace MusicMarketKursach.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext() { }
        public DbSet<User> Users { get; set; }
        public DbSet<MusicRecord> MusicRecords { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<AddedToCart> AddedToCart { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string connectionString = ConfigurationManager.ConnectionStrings["mssql"].ConnectionString;
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
