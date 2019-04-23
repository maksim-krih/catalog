using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Catalog.BLL.Interfaces;
using Catalog.BLL.ViewModels;
using Catalog.DAL.Models;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Views
{
    [Authorize(Roles = "Admin")]
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
            var facilitiesAndUsers = new FacilitiesUsersRoles
            {
                Facilities = db.Facilities.GetAll(),
                Users = db.Users.GetAll(),
                Roles = db.Roles.GetAll()
            };

            return View(facilitiesAndUsers);
        }
        
        public IActionResult CreateFacility()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFacility(Facility facility)
        {
            if (facility != null)
            {
                db.Facilities.Create(facility);
                db.Save();
                ViewBag.message = "Facility created!";
            }
            else
            {
                ViewBag.message = "Error happened";
                throw new NotImplementedException();
            }
            return RedirectToAction("Index");
        }
        
        public IActionResult EditFacility(int? id)
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
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditFacility([Bind("Id,FacilityOwnerId,Name,Phone,FacilityType,Address,Schedule")]Facility facility)
        {
            if (facility != null)
            {
                try
                {
                    db.Facilities.Update(facility);
                    db.FacilityAddresses.Update(facility.Address);
                    db.Schedules.Update(facility.Schedule);
                    db.Save();
                    ViewBag.message = "Facility Updated!";
                }
                catch (Exception e)
                {
                    throw new NotImplementedException(e.Message, e.InnerException);
                }
            }
            else
            {
                ViewBag.message = "Error happened";
                throw new NotImplementedException();
            }

            return RedirectToAction("Index");
        }

        public IActionResult DeleteFacility(int? id)
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
        
        [HttpPost, ActionName("DeleteFacility")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmedFacility(int id)
        {
            db.Facilities.Delete(id);
            db.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool FacilityModelExists(int id)
        {
            return db.Facilities.Get(id) != null;
        }


        public IActionResult CreateUser()
        {
            ViewData["Roleid"] = new SelectList(db.Roles.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser([Bind("Id,Name,Password,Email,Roleid")] User user)
        {
            if (user == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                db.Users.Create(user);
                db.Save();
                return RedirectToAction("Index");
            }
            ViewData["Roleid"] = new SelectList(db.Roles.GetAll(), "Id", "Name", user.Roleid);
            return View(user);
        }
        
        public IActionResult EditUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = db.Users.Get(id.Value);
            if (userModel == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(db.Roles.GetAll(), "Id", "Name", userModel.Roleid);
            return View(userModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(int id, [Bind("Id,Name,Password,Email,Roleid")] User user)
        {
            if (user == null || id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Users.Update(user);
                    db.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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

            ViewData["Roleid"] = new SelectList(db.Roles.GetAll(), "Id", "Name", user.Roleid);
            return View(user);
        }
        
        public IActionResult DeleteUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = db.Users.Get(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmedUser(int id)
        {
            db.Users.Delete(id);
            db.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return db.Users.Get(id) != null;
        }
    }
}
