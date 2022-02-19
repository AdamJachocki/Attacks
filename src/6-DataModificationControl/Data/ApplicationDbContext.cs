using Common.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _6_DataModificationControl.Data
{
    public class ApplicationDbContext : IdentityDbContext<SystemUser, UserRole, int>
    {
        public DbSet<TodoItem> Items { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TodoItem>().ToTable("items");
            builder.Entity<TodoItem>().HasKey(x => x.Id);
        }
    }
}