using System;
using System.Collections.Generic;
using Catalog.DAL.Models;

namespace Catalog.BLL.Interfaces
{
    public interface IUnitOfWork :IDisposable  
    {
        IRepository<Facility> Facilities { get; }
        IRepository<Feedback> Feedbacks { get; }
        void Save();
    }
}
