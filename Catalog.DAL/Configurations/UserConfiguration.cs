using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Catalog.DAL.Models;

namespace Catalog.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.OwnsMany(c => c.Facilities);

            builder.HasData(
                new User
                {
                    Id = 1,
                    Name = "Admin",
                    Roleid = 1,
                    Email = "admin@gmail.com",
                    Password = "1111"                    
                },
                new User
                {
                    Id = 2,
                    Name = "User",
                    Roleid = 2,
                    Email = "user@gmail.com",
                    Password = "1111"
                });
        }
    }
}
