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

                    b.ToTable("Roles");

                    b.HasData(
                        new { Id = 1, Name = "Admin" },
                        new { Id = 2, Name = "User" }
                    );
                });

            modelBuilder.Entity("Catalog.DAL.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<int?>("Roleid");

                    b.HasKey("Id");

                    b.HasIndex("Roleid");

                    b.ToTable("Users");

                    b.HasData(
                        new { Id = 1, Email = "admin@gmail.com", Name = "Admin", Password = "1111", Roleid = 1 },
                        new { Id = 2, Email = "user@gmail.com", Name = "User", Password = "1111", Roleid = 2 }
                    );
                });

            modelBuilder.Entity("Catalog.DAL.Models.User", b =>
                {
                    b.HasOne("Catalog.DAL.Models.Role", "UserRole")
                        .WithMany()
                        .HasForeignKey("Roleid");

                    b.OwnsOne("Catalog.DAL.Models.Facility", "Facilities", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<int>("FacilityOwnerId");

                            b1.Property<string>("FacilityType");

                            b1.Property<string>("Name");

                            b1.Property<string>("Phone");

                            b1.Property<double>("Price");

                            b1.Property<double>("Rating");

                            b1.HasIndex("FacilityOwnerId");

                            b1.ToTable("Facility");

                            b1.HasOne("Catalog.DAL.Models.User", "FacilityOwner")
                                .WithMany("Facilities")
                                .HasForeignKey("FacilityOwnerId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne("Catalog.DAL.Models.FacilityAddress", "Address", b2 =>
                                {
                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                    b2.Property<string>("City");

                                    b2.Property<string>("Country");

                                    b2.Property<int>("FacilityId");

                                    b2.Property<string>("HouseNumber");

                                    b2.Property<string>("Street");

                                    b2.Property<int>("ZipCode");

                                    b2.HasIndex("FacilityId")
                                        .IsUnique();

                                    b2.ToTable("FacilityAddress");

                                    b2.HasOne("Catalog.DAL.Models.Facility", "Facility")
                                        .WithOne("Address")
                                        .HasForeignKey("Catalog.DAL.Models.FacilityAddress", "FacilityId")
                                        .OnDelete(DeleteBehavior.Cascade);

                                    b2.HasData(
                                        new { Id = 1, City = "City 1", Country = "Country 1", FacilityId = 1, HouseNumber = "1", Street = "Street 1", ZipCode = 1 }
                                    );
                                });

                            b1.OwnsOne("Catalog.DAL.Models.Feedback", "Feedbacks", b2 =>
                                {
                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                    b2.Property<string>("Author");

                                    b2.Property<DateTime>("Date");

                                    b2.Property<int>("FacilityId");

                                    b2.Property<string>("Message");

                                    b2.Property<int>("Rating");

                                    b2.HasIndex("FacilityId");

                                    b2.ToTable("Feedback");

                                    b2.HasOne("Catalog.DAL.Models.Facility", "Facility")
                                        .WithMany("Feedbacks")
                                        .HasForeignKey("FacilityId")
                                        .OnDelete(DeleteBehavior.Cascade);

                                    b2.HasData(
                                        new { Id = 1, Author = "Anonynous", Date = new DateTime(2019, 4, 13, 0, 15, 10, 536, DateTimeKind.Local), FacilityId = 1, Message = "Feedback message", Rating = 4 },
                                        new { Id = 2, Author = "Anonynous 2", Date = new DateTime(2019, 4, 13, 0, 15, 10, 539, DateTimeKind.Local), FacilityId = 1, Message = "Feedback message 2", Rating = 3 }
                                    );
                                });

                            b1.OwnsOne("Catalog.DAL.Models.Photo", "Photos", b2 =>
                                {
                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                    b2.Property<int>("FacilityId");

                                    b2.Property<string>("Path");

                                    b2.HasIndex("FacilityId");

                                    b2.ToTable("Photo");

                                    b2.HasOne("Catalog.DAL.Models.Facility", "Facility")
                                        .WithMany("Photos")
                                        .HasForeignKey("FacilityId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });

                            b1.OwnsOne("Catalog.DAL.Models.Schedule", "Schedule", b2 =>
                                {
                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                    b2.Property<TimeSpan>("Closed");

                                    b2.Property<int>("FacilityId");

                                    b2.Property<TimeSpan>("Open");

                                    b2.HasIndex("FacilityId")
                                        .IsUnique();

                                    b2.ToTable("Schedule");

                                    b2.HasOne("Catalog.DAL.Models.Facility", "Facility")
                                        .WithOne("Schedule")
                                        .HasForeignKey("Catalog.DAL.Models.Schedule", "FacilityId")
                                        .OnDelete(DeleteBehavior.Cascade);

                                    b2.HasData(
                                        new { Id = 1, Closed = new TimeSpan(0, 20, 0, 0, 0), FacilityId = 1, Open = new TimeSpan(0, 8, 0, 0, 0) }
                                    );
                                });

                            b1.HasData(
                                new { Id = 1, FacilityOwnerId = 2, FacilityType = "Bar", Name = "Name 1", Phone = "012345678", Price = 3.0, Rating = 3.2 },
                                new { Id = 2, FacilityOwnerId = 2, FacilityType = "Bar", Name = "Name 2", Phone = "012345678", Price = 3.0, Rating = 3.2 },
                                new { Id = 3, FacilityOwnerId = 2, FacilityType = "Bar", Name = "Name 1", Phone = "012345678", Price = 3.0, Rating = 3.2 }
                            );
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
