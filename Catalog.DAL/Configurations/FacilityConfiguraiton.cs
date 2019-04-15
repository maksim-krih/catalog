using Catalog.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.DAL.Configurations
{
    public class FacilityConfiguraiton : IEntityTypeConfiguration<Facility>
    {
        public void Configure(EntityTypeBuilder<Facility> builder)
        {
            builder.OwnsOne(a => a.Address);
            builder.OwnsOne(s => s.Schedule);

            builder.OwnsMany(a => a.Feedbacks);
            builder.OwnsMany(p => p.Photos);
            
            builder
                .HasData(
                new Facility[]
                {
                    new Facility
                    {
                        Id = 1,
                        Name = "Name 1",
                        Price = 3,
                        Rating = 3.2,
                        Phone = "012345678",
                        FacilityType = "Bar",
                        FacilityOwnerId = 2
                    },
                    new Facility
                    {
                        Id = 2,
                        Name = "Name 2",
                        Price = 3,
                        Rating = 3.2,
                        Phone = "012345678",
                        FacilityType = "Bar",
                        FacilityOwnerId = 2
                    },
                    new Facility
                    {
                        Id = 3,
                        Name = "Name 1",
                        Price = 3,
                        Rating = 3.2,
                        Phone = "012345678",
                        FacilityType = "Bar",
                        FacilityOwnerId = 2
                    }
                }
                );
        }
    }
}
