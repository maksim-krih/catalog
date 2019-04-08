using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.DAL.Models;
using Catalog.DAL.Data;
using Catalog.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.BLL.Repositories
{
    public class FeedbackRepository : IRepository<Feedback>
    {
        CatalogContext db;
        public FeedbackRepository(CatalogContext context)
        {
            db = context;
        }

        public IEnumerable<Feedback> GetAll()
        {
            return db.Feedbacks;
        }

        public Feedback Get(int id)
        {
            return db.Feedbacks.Find(id);
        }

        public void Create(Feedback item)
        {
            db.Feedbacks.Add(item);
        }

        public void Update(Feedback item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var item = db.Feedbacks.Find(id);
            if (item != null)
                db.Remove(item);
        }

        public IEnumerable<Feedback> Find(Func<Feedback, bool> predicate)
        {
            return db.Feedbacks.Where(predicate).ToList();
        }

    }
}
