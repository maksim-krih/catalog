using Catalog.DAL.Configurations;
using Catalog.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.DAL.EF
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seeding db
            modelBuilder.Entity<Facility>().HasData(
                new Facility[]
                {
                    new Facility
                    {
                        Name = "Name 1",
                        Address = new FacilityAddress
                        {
                            Country = "Country 1",
                            City = "City 1",
                            Street = "Street 1",
                            HouseNumber = "1",
                            ZipCode = 1
                        },
                        Schedule = new Schedule
                        {
                            Open = TimeSpan.FromHours(8),
                            Closed = TimeSpan.FromHours(20),
                            WorkingDays = new DayOfWeek[] {DayOfWeek.Monday,
                                                           DayOfWeek.Tuesday,
                                                           DayOfWeek.Wednesday,
                                                           DayOfWeek.Thursday,
                                                           DayOfWeek.Friday,
                                                           DayOfWeek.Saturday}
                        },
                        Price = 3,
                        Rating = 3.2,
                        Phone = "Phone 1",
                        FacilityType = "Bar",

                        Feedbacks = new List<Feedback>
                        {
                            new Feedback
                            {
                                Author = "feedback author",
                                Date = DateTime.Now,
                                Message = "Lorem Ipsum",
                                Rating = 2
                            }
                        }
                    },

                    new Facility
                    {
                        Name = "Name 2",
                        Address = new FacilityAddress
                        {
                            Country = "Country 2",
                            City = "City 2",
                            Street = "Street 2",
                            HouseNumber = "2",
                            ZipCode = 2
                        },
                        Schedule = new Schedule
                        {
                            Open = TimeSpan.FromHours(8),
                            Closed = TimeSpan.FromHours(20),
                            WorkingDays = new DayOfWeek[] {DayOfWeek.Wednesday,
                                                           DayOfWeek.Thursday,
                                                           DayOfWeek.Friday,
                                                           DayOfWeek.Saturday}
                        },
                        Price = 4.7,
                        Rating = 3.7,
                        Phone = "Phone 2",
                        FacilityType = "Bar"
                    },

                    new Facility
                    {
                        Name = "Name 3",
                        Address = new FacilityAddress
                        {
                            Country = "Country 3",
                            City = "City 3",
                            Street = "Street 3",
                            HouseNumber = "3",
                            ZipCode = 3
                        },
                        Schedule = new Schedule
                        {
                            Open = TimeSpan.FromHours(16),
                            Closed = TimeSpan.FromHours(4),
                            WorkingDays = new DayOfWeek[] {DayOfWeek.Monday,
                                                           DayOfWeek.Friday,
                                                           DayOfWeek.Saturday,
                                                           DayOfWeek.Sunday}
                        },
                        Price = 7.4,
                        Rating = 4.1,
                        Phone = "Phone 3",
                        FacilityType = "Club"
                    },

                    new Facility
                    {
                        Name = "Name 4",
                        Address = new FacilityAddress
                        {
                            Country = "Country 4",
                            City = "City 4",
                            Street = "Street 4",
                            HouseNumber = "4",
                            ZipCode = 4
                        },
                        Schedule = new Schedule
                        {
                            Open = TimeSpan.FromHours(9),
                            Closed = TimeSpan.FromHours(24),
                            WorkingDays = new DayOfWeek[] {DayOfWeek.Monday,
                                                           DayOfWeek.Tuesday,
                                                           DayOfWeek.Wednesday,
                                                           DayOfWeek.Thursday,
                                                           DayOfWeek.Friday,
                                                           DayOfWeek.Saturday,
                                                           DayOfWeek.Sunday}
                        },
                        Price = 8.9,
                        Rating = 4.7,
                        Phone = "Phone 4",
                        FacilityType = "Restaurant"
                    },


                    new Facility
                    {
                        Name = "Name 5",
                        Address = new FacilityAddress
                        {
                            Country = "Country 5",
                            City = "City 5",
                            Street = "Street 5",
                            HouseNumber = "5",
                            ZipCode = 5
                        },
                        Schedule = new Schedule
                        {
                            Open = TimeSpan.FromHours(12),
                            Closed = TimeSpan.FromHours(2),
                            WorkingDays = new DayOfWeek[] {DayOfWeek.Monday,
                                                           DayOfWeek.Tuesday,
                                                           DayOfWeek.Wednesday,
                                                           DayOfWeek.Thursday,
                                                           DayOfWeek.Friday,
                                                           DayOfWeek.Saturday,
                                                           DayOfWeek.Sunday}
                        },
                        Price = 3.5,
                        Rating = 2.8,
                        Phone = "Phone 5",
                        FacilityType = "Pub"
                    }
                });

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new FacilityConfiguraiton());

        }


    }
}
