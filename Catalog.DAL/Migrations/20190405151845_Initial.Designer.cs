﻿// <auto-generated />
using System;
using Catalog.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Catalog.DAL.Migrations
{
    [DbContext(typeof(CatalogContext))]
    [Migration("20190405151845_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                        new { Id = 1, FacilityType = "Bar", Name = "Name 1", Phone = "012345678", Price = 3.0, Rating = 3.2 }
                    );
                });

            modelBuilder.Entity("Catalog.DAL.Models.FacilityAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<int>("FacilityId");

                    b.Property<string>("HouseNumber");

                    b.Property<string>("Street");

                    b.Property<int>("ZipCode");

                    b.HasKey("Id");

                    b.HasIndex("FacilityId")
                        .IsUnique();

                    b.ToTable("FacilityAddress");

                    b.HasData(
                        new { Id = 1, City = "City 1", Country = "Country 1", FacilityId = 1, HouseNumber = "1", Street = "Street 1", ZipCode = 1 }
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
                        new { Id = 1, Author = "Anonynous", Date = new DateTime(2019, 4, 5, 18, 18, 44, 765, DateTimeKind.Local), FacilityId = 1, Message = "Feedback message", Rating = 4 }
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

            modelBuilder.Entity("Catalog.DAL.Models.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<TimeSpan>("Closed");

                    b.Property<int>("FacilityId");

                    b.Property<TimeSpan>("Open");

                    b.HasKey("Id");

                    b.HasIndex("FacilityId")
                        .IsUnique();

                    b.ToTable("Schedule");

                    b.HasData(
                        new { Id = 1, Closed = new TimeSpan(0, 20, 0, 0, 0), FacilityId = 1, Open = new TimeSpan(0, 8, 0, 0, 0) }
                    );
                });

            modelBuilder.Entity("Catalog.DAL.Models.FacilityAddress", b =>
                {
                    b.HasOne("Catalog.DAL.Models.Facility", "Facility")
                        .WithOne("Address")
                        .HasForeignKey("Catalog.DAL.Models.FacilityAddress", "FacilityId")
                        .OnDelete(DeleteBehavior.Cascade);
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

            modelBuilder.Entity("Catalog.DAL.Models.Schedule", b =>
                {
                    b.HasOne("Catalog.DAL.Models.Facility", "Facility")
                        .WithOne("Schedule")
                        .HasForeignKey("Catalog.DAL.Models.Schedule", "FacilityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
