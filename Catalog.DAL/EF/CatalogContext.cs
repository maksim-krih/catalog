using Catalog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.DAL.EF
{
    public class CatalogContext : DbContext
    {
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
                
        //TODO: this may cause exception
        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
