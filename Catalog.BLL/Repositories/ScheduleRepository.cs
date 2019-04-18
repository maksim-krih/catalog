using Catalog.BLL.Interfaces;
using Catalog.DAL.Data;
using Catalog.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.BLL.Repositories
{
    class ScheduleRepository : IRepository<Schedule>
    {
        private CatalogContext db;

        public ScheduleRepository(CatalogContext context)
        {
            db = context;
        }

        public void Create(Schedule item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Schedule> Find(Func<Schedule, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Schedule Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Schedule> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Schedule item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
