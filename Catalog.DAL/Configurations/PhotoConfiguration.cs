using Catalog.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.DAL.Configurations
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasData(
                new List<Photo>
                {
                    new Photo
                    {
                        Id = 1,
                        FacilityId = 1,
                        Path = "C:/Users/pampa/Desktop/catalog/Catalog/wwwroot/images/bulldozer.jpg"
                    },
                    new Photo
                    {
                        Id = 2,
                        FacilityId = 1,
                        Path = "C:/Users/pampa/Desktop/catalog/Catalog/wwwroot/images/bulldozer.jpg"
                    },
                    new Photo
                    {
                        Id = 3,
                        FacilityId = 1,
                        Path = "C:/Users/pampa/Desktop/catalog/Catalog/wwwroot/images/bulldozer.jpg"
                    }
                }
                );
        }
    }
}
