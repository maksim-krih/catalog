using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.DAL.Models;
using Catalog.DAL.Data;
using Catalog.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.BLL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        CatalogContext db;
        public UserRepository(CatalogContext context)
        {
            db = context;
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public void Create(User item)
        {
            db.Users.Add(item);
        }

        public void Update(User item)
        {           
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var item = db.Users.Find(id);
            if (item != null)
                db.Remove(item);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return db.Users.Where(predicate).ToList();
        }

    }
}
