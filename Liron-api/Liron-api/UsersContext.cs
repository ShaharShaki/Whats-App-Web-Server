using Microsoft.EntityFrameworkCore;
using Liron_api.Models;


namespace WebShop
{
    public class UsersContext: DbContext
    {
        private const string connectionString = "server=localhost;port=3305;database=Users;user=root;password=123456";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, MariaDbServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the Name property as the primary
            // key of the Items table
         modelBuilder.Entity<User>().HasKey(e => e.Name);
         //modelBuilder.Entity<Conversation>().HasKey(e => e.MessageId);

        }

        public DbSet<User> Users { get; set; }
    
    }
}


