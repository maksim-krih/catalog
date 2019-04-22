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
                        Name = "Hashtag",
                        Price = 3,
                        Rating = 5,
                        Phone = "0997513669",
                        FacilityType = "Hookah",
                        FacilityOwnerId = 2
                    },
                    new Facility
                    {
                        Id = 2,
                        Name = "Pizza town",
                        Price = 2,
                        Rating = 4.1,
                        Phone = "0997075751",
                        FacilityType = "Restaurant",
                        FacilityOwnerId = 2
                    },
                    new Facility
                    {
                        Id = 3,
                        Name = "Karuzo",
                        Price = 3,
                        Rating = 4.1,
                        Phone = "0685951553",
                        FacilityType = "Restaurant",
                        FacilityOwnerId = 2
                    }
                }
                );
        }
    }
}
