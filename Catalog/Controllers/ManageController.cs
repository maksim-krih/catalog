using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Catalog.BLL.Interfaces;
using Catalog.DAL.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult UpdateProfile([Bind("Name,Email,Password")] User user)
        {
            //TODO: EXCEPTION: Database operation expected to affect 1 row(s) but actually affected 0 row(s). Data may have been modified or deleted since entities were loaded.
            //if (user != null)
            //{
            //    user.Id = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);
            //    db.Users.Update(user);
            //    db.Save();
            //}
            //else
            throw new NotImplementedException();

            return RedirectToAction("UserCabinet");
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

            return View("UserCabinet", facility);
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
        public IActionResult Edit(Facility facility)
        {
            if (facility != null)
            {                
                facility.FacilityOwnerId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);
                db.Facilities.Update(facility);
                //db.FacilityAddresses.Update(facility.Address);
                //db.Schedules.Update(facility.Schedule);
                db.Save();
                ViewBag.message = "Facility Updated!";
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