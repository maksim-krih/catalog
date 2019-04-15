using Catalog.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.DAL.Configurations
{
    public class FacilityAddressConfiguration : IEntityTypeConfiguration<FacilityAddress>
    {
        public void Configure(EntityTypeBuilder<FacilityAddress> builder)
        {
            builder.HasData(
                new FacilityAddress
                {
                    Id = 1,
                    FacilityId = 1,
                    Country = "Country 1",
                    City = "City 1",
                    Street = "Street 1",
                    HouseNumber = "1",
                    ZipCode = 1
                },
                new FacilityAddress
                {
                    Id = 2,
                    FacilityId = 2,
                    Country = "Country 2",
                    City = "City 2",
                    Street = "Street 2",
                    HouseNumber = "2",
                    ZipCode = 2
                },
                new FacilityAddress
                {
                    Id = 3,
                    FacilityId = 3,
                    Country = "Country 3",
                    City = "City 3",
                    Street = "Street 3",
                    HouseNumber = "3",
                    ZipCode = 3
                }

                );
        }
    }
}
