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
        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options)
        { }


        public DbSet<Facility> Facilities { get; set; }
        public DbSet<FacilityAddress> FacilityAddresses { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = "Admin" }, new Role { Id = 2, Name = "User" });
            modelBuilder.Entity<User>().HasData(new User { Id = 1, Name = "Admin", Roleid = 1, Email = "admin@gmail.com", Password = "1111" });

            //Seeding db
            //TODO: move this to configurations?            

            modelBuilder.Entity<FacilityAddress>().HasData(
                new FacilityAddress
                {
                    Id = 1,
                    FacilityId = 1,
                    Country = "Country 1",
                    City = "City 1",
                    Street = "Street 1",
                    HouseNumber = "1",
                    ZipCode = 1
                });

            modelBuilder.Entity<Feedback>().HasData(
                new Feedback
                {
                    Id = 1,
                    FacilityId = 1,
                    Author = "Anonynous",
                    Date = DateTime.Now,
                    Rating = 4,
                    Message = "Feedback message"                    
                });

            modelBuilder.Entity<Schedule>().HasData(
                new Schedule
                {
                    Id = 1,
                    FacilityId = 1,
                    Open = TimeSpan.FromHours(8),
                    Closed = TimeSpan.FromHours(20),
                    WorkingDays = new DayOfWeek[] { DayOfWeek.Wednesday,
                                                    DayOfWeek.Thursday,
                                                    DayOfWeek.Friday,
                                                    DayOfWeek.Saturday}
                });

            modelBuilder.ApplyConfiguration(new FacilityConfiguraiton());

            base.OnModelCreating(modelBuilder);

            //TODO: Apply configurations or make migrations?
            
            //modelBuilder.ApplyConfiguration(new FacilityAddressConfiguration());
            //modelBuilder.ApplyConfiguration(new FeedbackConfiguration());
            //modelBuilder.ApplyConfiguration(new PhotoConfiguration());
            //modelBuilder.ApplyConfiguration(new ScheduleConfiguration());

        }

        

    }
}
