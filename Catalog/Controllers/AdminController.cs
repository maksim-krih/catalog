using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Catalog.BLL.Interfaces;
using Catalog.DAL.Models;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Views
{
    [Authorize]
    public class AdminController : Controller
    {
        IUnitOfWork db;

        public AdminController(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }

        // GET: Admin
        public IActionResult Index()
        {
            return View(db.Facilities.GetAll());
        }

        // GET: Admin/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilityModel = db.Facilities
                .Get(id.Value);
            if (facilityModel == null)
            {
                return NotFound();
            }

            return View(facilityModel);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Price,Rating,Phone,FacilityType,Address")] Facility facilityModel)
        {
            if (ModelState.IsValid)
            {
                db.Facilities.Create(facilityModel);
                db.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(facilityModel);
        }
        
        // GET: Admin/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilityModel = db.Facilities.Get(id.Value);
            if (facilityModel == null)
            {
                return NotFound();
            }
            return View(facilityModel);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Price,Rating,Phone,FacilityType")] Facility facilityModel)
        {
            if (id != facilityModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Facilities.Update(facilityModel);
                    db.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacilityModelExists(facilityModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(facilityModel);
        }
        
        // GET: Admin/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilityModel = db.Facilities
                .Get(id.Value);
            if (facilityModel == null)
            {
                return NotFound();
            }

            return View(facilityModel);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            db.Facilities.Delete(id);
            db.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool FacilityModelExists(int id)
        {
            return db.Facilities.Get(id) != null;
        }
    }
}
