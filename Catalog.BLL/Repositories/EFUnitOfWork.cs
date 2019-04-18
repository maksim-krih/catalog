using Catalog.DAL.Data;
using Catalog.DAL.Models;
using Catalog.BLL.Interfaces;
using Catalog.BLL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.BLL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private CatalogContext db;
        private FacilityRepository facilityRepository;
        private FacilityAddressRepository facilityAddressRepository;
        private ScheduleRepository scheduleRepository;
        private FeedbackRepository feedbackRepository;
        private UserRepository userRepository;
        private RoleRepository roleRepository;

        public EFUnitOfWork(CatalogContext catalogContext)
        {
            db = catalogContext;
        }

        public IRepository<Facility> Facilities
        {
            get
            {
                if (facilityRepository == null)
                    facilityRepository = new FacilityRepository(db);
                return facilityRepository;
            }
        }

        public IRepository<FacilityAddress> FacilityAddresses
        {
            get
            {
                if (facilityAddressRepository == null)
                    facilityAddressRepository = new FacilityAddressRepository(db);
                return facilityAddressRepository;
            }
        }

        public IRepository<Schedule> Schedules
        {
            get
            {
                if (scheduleRepository == null)
                    scheduleRepository = new ScheduleRepository(db);
                return scheduleRepository;
            }
        }

        public IRepository<Feedback> Feedbacks
        {
            get
            {
                if (feedbackRepository == null)
                    feedbackRepository = new FeedbackRepository(db);
                return feedbackRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public IRepository<Role> Roles
        {
            get
            {
                if (roleRepository == null)
                    roleRepository = new RoleRepository(db);
                return roleRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        
    }
}
