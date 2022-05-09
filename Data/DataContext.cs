using Data.Domain;
using Data.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=SQL5080.site4now.net;Database=db_a7d36c_blogdemo;User Id=db_a7d36c_blogdemo_admin;Password=matehujin12;MultipleActiveResultSets=true");
        }
        public DataContext()
        {
        }
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Post> Post { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var admin = new User()
			{
				Id = 1,
				Name = "Admin",
                LastName = "Admin",
				Email = "123",
				Password = "123",
				Role = "Admin"
			};
			modelBuilder.Entity<User>().HasData(admin);
			base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new PostMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
        }
    }
}