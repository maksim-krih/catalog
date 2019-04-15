﻿// <auto-generated />
using System;
using Catalog.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Catalog.DAL.Migrations
{
    [DbContext(typeof(CatalogContext))]
    partial class CatalogContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Catalog.DAL.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new { Id = 1, Name = "Admin" },
                        new { Id = 2, Name = "User" }
                    );
                });

            modelBuilder.Entity("Catalog.DAL.Models.Role", b =>
                {
                    b.OwnsOne("Catalog.DAL.Models.User", "Users", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Email");

                            b1.Property<string>("Name");

                            b1.Property<string>("Password");

                            b1.Property<int>("Roleid");

                            b1.HasIndex("Roleid");

                            b1.ToTable("User");

                            b1.HasOne("Catalog.DAL.Models.Role", "UserRole")
                                .WithMany("Users")
                                .HasForeignKey("Roleid")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne("Catalog.DAL.Models.Facility", "Facilities", b2 =>
                                {
                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                    b2.Property<int>("FacilityOwnerId");

                                    b2.Property<string>("FacilityType");

                                    b2.Property<string>("Name");

                                    b2.Property<string>("Phone");

                                    b2.Property<double>("Price");

                                    b2.Property<double>("Rating");

                                    b2.HasIndex("FacilityOwnerId");

                                    b2.ToTable("Facility");

                                    b2.HasOne("Catalog.DAL.Models.User", "FacilityOwner")
                                        .WithMany("Facilities")
                                        .HasForeignKey("FacilityOwnerId")
                                        .OnDelete(DeleteBehavior.Cascade);

                                    b2.OwnsOne("Catalog.DAL.Models.FacilityAddress", "Address", b3 =>
                                        {
                                            b3.Property<int>("Id")
                                                .ValueGeneratedOnAdd()
                                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                            b3.Property<string>("City");

                                            b3.Property<string>("Country");

                                            b3.Property<int>("FacilityId");

                                            b3.Property<string>("HouseNumber");

                                            b3.Property<string>("Street");

                                            b3.Property<int>("ZipCode");

                                            b3.HasIndex("FacilityId")
                                                .IsUnique();

                                            b3.ToTable("FacilityAddress");

                                            b3.HasOne("Catalog.DAL.Models.Facility", "Facility")
                                                .WithOne("Address")
                                                .HasForeignKey("Catalog.DAL.Models.FacilityAddress", "FacilityId")
                                                .OnDelete(DeleteBehavior.Cascade);

                                            b3.HasData(
                                                new { Id = 1, City = "City 1", Country = "Country 1", FacilityId = 1, HouseNumber = "1", Street = "Street 1", ZipCode = 1 },
                                                new { Id = 2, City = "City 2", Country = "Country 2", FacilityId = 2, HouseNumber = "2", Street = "Street 2", ZipCode = 2 },
                                                new { Id = 3, City = "City 3", Country = "Country 3", FacilityId = 3, HouseNumber = "3", Street = "Street 3", ZipCode = 3 }
                                            );
                                        });

                                    b2.OwnsOne("Catalog.DAL.Models.Feedback", "Feedbacks", b3 =>
                                        {
                                            b3.Property<int>("Id")
                                                .ValueGeneratedOnAdd()
                                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                            b3.Property<string>("Author");

                                            b3.Property<DateTime>("Date");

                                            b3.Property<int>("FacilityId");

                                            b3.Property<string>("Message");

                                            b3.Property<double>("Price");

                                            b3.Property<double>("Rating");

                                            b3.HasIndex("FacilityId");

                                            b3.ToTable("Feedback");

                                            b3.HasOne("Catalog.DAL.Models.Facility", "Facility")
                                                .WithMany("Feedbacks")
                                                .HasForeignKey("FacilityId")
                                                .OnDelete(DeleteBehavior.Cascade);

                                            b3.HasData(
                                                new { Id = 1, Author = "Anonynous", Date = new DateTime(2019, 4, 16, 0, 7, 17, 58, DateTimeKind.Local), FacilityId = 1, Message = "Feedback message", Price = 0.0, Rating = 4.0 },
                                                new { Id = 2, Author = "Anonynous 2", Date = new DateTime(2019, 4, 16, 0, 7, 17, 61, DateTimeKind.Local), FacilityId = 1, Message = "Feedback message 2", Price = 0.0, Rating = 3.0 }
                                            );
                                        });

                                    b2.OwnsOne("Catalog.DAL.Models.Photo", "Photos", b3 =>
                                        {
                                            b3.Property<int>("Id")
                                                .ValueGeneratedOnAdd()
                                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                            b3.Property<int>("FacilityId");

                                            b3.Property<string>("Path");

                                            b3.HasIndex("FacilityId");

                                            b3.ToTable("Photo");

                                            b3.HasOne("Catalog.DAL.Models.Facility", "Facility")
                                                .WithMany("Photos")
                                                .HasForeignKey("FacilityId")
                                                .OnDelete(DeleteBehavior.Cascade);
                                        });

                                    b2.OwnsOne("Catalog.DAL.Models.Schedule", "Schedule", b3 =>
                                        {
                                            b3.Property<int>("Id")
                                                .ValueGeneratedOnAdd()
                                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                            b3.Property<TimeSpan>("Closed");

                                            b3.Property<int>("FacilityId");

                                            b3.Property<TimeSpan>("Open");

                                            b3.HasIndex("FacilityId")
                                                .IsUnique();

                                            b3.ToTable("Schedule");

                                            b3.HasOne("Catalog.DAL.Models.Facility", "Facility")
                                                .WithOne("Schedule")
                                                .HasForeignKey("Catalog.DAL.Models.Schedule", "FacilityId")
                                                .OnDelete(DeleteBehavior.Cascade);

                                            b3.HasData(
                                                new { Id = 1, Closed = new TimeSpan(0, 20, 0, 0, 0), FacilityId = 1, Open = new TimeSpan(0, 8, 0, 0, 0) },
                                                new { Id = 2, Closed = new TimeSpan(0, 20, 0, 0, 0), FacilityId = 2, Open = new TimeSpan(0, 8, 0, 0, 0) },
                                                new { Id = 3, Closed = new TimeSpan(0, 20, 0, 0, 0), FacilityId = 3, Open = new TimeSpan(0, 8, 0, 0, 0) }
                                            );
                                        });

                                    b2.HasData(
                                        new { Id = 1, FacilityOwnerId = 2, FacilityType = "Bar", Name = "Name 1", Phone = "012345678", Price = 3.0, Rating = 3.2 },
                                        new { Id = 2, FacilityOwnerId = 2, FacilityType = "Bar", Name = "Name 2", Phone = "012345678", Price = 3.0, Rating = 3.2 },
                                        new { Id = 3, FacilityOwnerId = 2, FacilityType = "Bar", Name = "Name 1", Phone = "012345678", Price = 3.0, Rating = 3.2 }
                                    );
                                });

                            b1.HasData(
                                new { Id = 1, Email = "admin@gmail.com", Name = "Admin", Password = "1111", Roleid = 1 },
                                new { Id = 2, Email = "user@gmail.com", Name = "User", Password = "1111", Roleid = 2 }
                            );
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
