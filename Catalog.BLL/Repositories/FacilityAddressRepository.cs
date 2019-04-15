using Catalog.BLL.Interfaces;
using Catalog.DAL.Data;
using Catalog.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.BLL.Repositories
{
    class FacilityAddressRepository : IRepository<FacilityAddress>
    {
        private CatalogContext db;

        public FacilityAddressRepository(CatalogContext context)
        {
            db = context;
        }

        public void Create(FacilityAddress item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FacilityAddress> Find(Func<FacilityAddress, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public FacilityAddress Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FacilityAddress> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(FacilityAddress item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
