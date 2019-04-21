using Catalog.DAL.Configurations;
using Catalog.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.DAL.Data
{
    public class CatalogContext : DbContext
    {
        public CatalogContext() { }

        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options)
        { }


        public virtual DbSet<Facility> Facilities { get; set; }
        public DbSet<FacilityAddress> FacilityAddresses { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = "Admin" }, new Role { Id = 2, Name = "User" });

            modelBuilder.Entity<Role>().OwnsMany(c => c.Users);

            //DB seeds in configuration

            modelBuilder.ApplyConfiguration(new FacilityConfiguraiton());
            modelBuilder.ApplyConfiguration(new FeedbackConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new FacilityAddressConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleConfiguration());
            //modelBuilder.ApplyConfiguration(new PhotoConfiguration());




            base.OnModelCreating(modelBuilder);

        }

        

    }
}
