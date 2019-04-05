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
            
        }
    }
}
