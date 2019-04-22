using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.DAL.Models;
using Catalog.DAL.Data;
using Catalog.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.BLL.Repositories
{
    public class RoleRepository : IRepository<Role>
    {
        CatalogContext db;
        public RoleRepository(CatalogContext context)
        {
            db = context;
        }

        public IEnumerable<Role> GetAll()
        {
            return db.Roles;
        }

        public Role Get(int id)
        {
            return db.Roles.Find(id);
        }

        public void Create(Role item)
        {
            db.Roles.Add(item);
        }

        public void Update(Role item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var item = db.Roles.Find(id);
            if (item != null)
                db.Roles.Remove(item);
        }

        public IEnumerable<Role> Find(Func<Role, bool> predicate)
        {
            return db.Roles.Where(predicate).ToList();
        }

    }
}
