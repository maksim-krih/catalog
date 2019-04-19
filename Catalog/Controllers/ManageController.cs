using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Catalog.BLL.Interfaces;
using Catalog.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Controllers
{
    public class ManageController : Controller
    {
        private IUnitOfWork db;

        public ManageController(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }
        

        [HttpGet]
        public IActionResult UserCabinet()
        {
            var _id = HttpContext.User.Claims.ToList()[0].Value;
            var user = db.Users.Get(Convert.ToInt32(_id));

            if (user == null)
                throw new NotImplementedException();

            ViewBag.ActiveTab = "Profile";

            return View(user);
        }


        [HttpPost]        
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProfile(int id, [Bind("Id,Name,Email,Password,Roleid")]  User user)
        {
            if (id != user.Id)
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
                        throw new NotSupportedException();
                    }
                }
                return RedirectToAction(nameof(UserCabinet));
            }
            return View(user);
                        
        }

        private bool UserExists(int id)
        {
            return db.Users.Get(id) != null;
        }

        [HttpGet]
        public IActionResult Add(int id)
        {            
            return View("AddFacility");
        }
        
        [HttpPost]
        public IActionResult Add(Facility facility)
        {
            if (facility != null)
            {
                facility.FacilityOwnerId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);
                db.Facilities.Create(facility);
                db.Save();
                ViewBag.message = "Facility created!";
            }
            else
            {
                ViewBag.message = "Error happened";
                throw new NotImplementedException();
            }

            return RedirectToAction("UserCabinet");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var facilityToEdit = db.Facilities.Get(id);
            if (facilityToEdit == null)
            {
                return NotFound();
            }

            return View(facilityToEdit);
        }

        [HttpPost]
        public IActionResult Edit([Bind("Id,FacilityOwnerId,Name,Phone,FacilityType,Address,Schedule")]Facility facility)
        {
            if (facility != null)
            {
                try
                {
                    //facility.FacilityOwnerId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);
                    db.Facilities.Update(facility);
                    db.FacilityAddresses.Update(facility.Address);
                    db.Schedules.Update(facility.Schedule);
                    db.Save();
                    ViewBag.message = "Facility Updated!";
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                ViewBag.message = "Error happened";
                throw new NotImplementedException();
            }

            return RedirectToAction("UserCabinet", facility);

        }

        [HttpGet]
        public IActionResult Delete(int? id)
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
        public IActionResult Delete(int id)
        {
            db.Facilities.Delete(id);
            db.Save();
            return RedirectToAction("UserCabinet");
        }

    }
}