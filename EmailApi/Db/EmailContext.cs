using EmailApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailApi.Db
{
    public class EmailContext : DbContext
    {
        public EmailContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Template> Templates { get; set; }
        public DbSet<EmailHistory> EmailHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}