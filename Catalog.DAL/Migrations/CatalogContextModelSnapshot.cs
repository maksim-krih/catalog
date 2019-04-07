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

            modelBuilder.Entity("Catalog.DAL.Models.Facility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FacilityType");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<double>("Price");

                    b.Property<double>("Rating");

                    b.HasKey("Id");

                    b.ToTable("Facility");

                    b.HasData(
                        new { Id = 1, FacilityType = "Bar", Name = "Name 1", Phone = "012345678", Price = 3.0, Rating = 3.2 },
                        new { Id = 2, FacilityType = "Bar", Name = "Name 1", Phone = "012345678", Price = 3.0, Rating = 3.2 },
                        new { Id = 3, FacilityType = "Bar", Name = "Name 1", Phone = "012345678", Price = 3.0, Rating = 3.2 }
                    );
                });

            modelBuilder.Entity("Catalog.DAL.Models.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author");

                    b.Property<DateTime>("Date");

                    b.Property<int>("FacilityId");

                    b.Property<string>("Message");

                    b.Property<int>("Rating");

                    b.HasKey("Id");

                    b.HasIndex("FacilityId");

                    b.ToTable("Feedback");

                    b.HasData(
                        new { Id = 1, Author = "Anonynous", Date = new DateTime(2019, 4, 8, 2, 30, 45, 495, DateTimeKind.Local), FacilityId = 1, Message = "Feedback message", Rating = 4 }
                    );
                });

            modelBuilder.Entity("Catalog.DAL.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FacilityId");

                    b.Property<string>("Path");

                    b.HasKey("Id");

                    b.HasIndex("FacilityId");

                    b.ToTable("Photo");
                });

            modelBuilder.Entity("Catalog.DAL.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Catalog.DAL.Models.Facility", b =>
                {
                    b.OwnsOne("Catalog.DAL.Models.FacilityAddress", "Address", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("City");

                            b1.Property<string>("Country");

                            b1.Property<int>("FacilityId");

                            b1.Property<string>("HouseNumber");

                            b1.Property<string>("Street");

                            b1.Property<int>("ZipCode");

                            b1.HasIndex("FacilityId")
                                .IsUnique();

                            b1.ToTable("FacilityAddress");

                            b1.HasOne("Catalog.DAL.Models.Facility", "Facility")
                                .WithOne("Address")
                                .HasForeignKey("Catalog.DAL.Models.FacilityAddress", "FacilityId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.HasData(
                                new { Id = 1, City = "City 1", Country = "Country 1", FacilityId = 1, HouseNumber = "1", Street = "Street 1", ZipCode = 1 }
                            );
                        });

                    b.OwnsOne("Catalog.DAL.Models.Schedule", "Schedule", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<TimeSpan>("Closed");

                            b1.Property<int>("FacilityId");

                            b1.Property<TimeSpan>("Open");

                            b1.HasIndex("FacilityId")
                                .IsUnique();

                            b1.ToTable("Schedule");

                            b1.HasOne("Catalog.DAL.Models.Facility", "Facility")
                                .WithOne("Schedule")
                                .HasForeignKey("Catalog.DAL.Models.Schedule", "FacilityId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.HasData(
                                new { Id = 1, Closed = new TimeSpan(0, 20, 0, 0, 0), FacilityId = 1, Open = new TimeSpan(0, 8, 0, 0, 0) }
                            );
                        });
                });

            modelBuilder.Entity("Catalog.DAL.Models.Feedback", b =>
                {
                    b.HasOne("Catalog.DAL.Models.Facility", "Facility")
                        .WithMany("Feedbacks")
                        .HasForeignKey("FacilityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Catalog.DAL.Models.Photo", b =>
                {
                    b.HasOne("Catalog.DAL.Models.Facility", "Facility")
                        .WithMany("Photos")
                        .HasForeignKey("FacilityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
