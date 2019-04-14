using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.DAL.Models;
using Catalog.DAL.Data;
using Catalog.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.BLL.Repositories
{
    public class FacilityRepository : IRepository<Facility>
    {
        private CatalogContext db;

        public FacilityRepository(CatalogContext context)
        {
            db = context;
        }

        public Facility Get(int id)
        {
            return db.Facilities.Find(id);
        }

        public IEnumerable<Facility> GetAll()
        {
            return db.Facilities;
        }

        public void Create(Facility item)
        {
            db.Add(item);
        }

        public void Update(Facility item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Facility facility = db.Facilities.Find(id);
            if (facility != null)
                db.Facilities.Remove(facility);
        }

        public IEnumerable<Facility> Find(Func<Facility, bool> predicate)
        {
            return db.Facilities.Where(predicate).ToList();
        }      
        
    }
}
