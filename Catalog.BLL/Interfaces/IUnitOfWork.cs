using System;
using System.Collections.Generic;
using Catalog.DAL.Entities;

namespace Catalog.DAL.Interfaces
{
    public interface IUnitOfWork :IDisposable  
    {
        IRepository<Facility> Facilities { get; }
        IRepository<Feedback> Feedbacks { get; }
        void Save();
    }
}
