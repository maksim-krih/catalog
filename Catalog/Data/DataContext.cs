using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Models;

namespace Catalog.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<FacilityModel> Facilities { get; set; }
        public DbSet<FeedbackModel> Feedbacks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = "Admin" }, new Role { Id = 2, Name = "User"});
            modelBuilder.Entity<User>().HasData(new User { Id = 1, Name = "Admin", Roleid = 1, Email = "admin@gmail.com", Password = "1111" });
            base.OnModelCreating(modelBuilder);
        }
    }
}
