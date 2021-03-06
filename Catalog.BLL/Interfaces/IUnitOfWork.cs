﻿using System;
using System.Collections.Generic;
using Catalog.DAL.Models;

namespace Catalog.BLL.Interfaces
{
    public interface IUnitOfWork :IDisposable  
    {
        IRepository<Facility> Facilities { get; }
        IRepository<Feedback> Feedbacks { get; }
        IRepository<User> Users { get; }
        IRepository<Role> Roles { get; }
        IRepository<FacilityAddress> FacilityAddresses { get; }
        IRepository<Schedule> Schedules { get; }
        void Save();
    }
}
