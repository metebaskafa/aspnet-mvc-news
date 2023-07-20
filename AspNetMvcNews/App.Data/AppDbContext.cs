using App.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryNews> CategoryNews { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsComment> Comments { get; set; }
        public DbSet<NewsImage> Images { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewsComment>()
                .HasOne(c => c.News)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
            ;
            base.OnModelCreating(modelBuilder);
        }
    }
}
