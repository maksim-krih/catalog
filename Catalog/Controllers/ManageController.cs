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
        public IActionResult UserCabinet(int? id)
        {
            //var id = HttpContext.User.Claims.ToList()[0].Value;
            if (!id.HasValue)
                return NotFound();
            
            var user = db.Users.Get(id.Value);
            if (user == null)
                return NotFound();
            
            return View(user);
        }


        [HttpPost]        
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProfile(int id, [Bind("Id,Name,Email,Password,Roleid")]  User user)
        {
            if (user == null)
                return NotFound();

            else if (id != user.Id)
                return NotFound();


            if (ModelState.IsValid)
            {
                try
                {
                    db.Users.Update(user);
                    db.Save();
                }
                catch (DbUpdateConcurrencyException ex)
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
                catch(Exception e)
                {
                    throw new NotSupportedException(e.Message);
                }

                return RedirectToAction(nameof(UserCabinet), new {id});
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
        public IActionResult Add(int? userId, Facility facility)
        {
            if (!userId.HasValue)
                return NotFound();

            if (facility != null)
            {
                try
                {
                    db.Facilities.Create(facility);
                    db.Save();
                }
                catch (Exception e)
                {
                    throw new NotImplementedException(e.Message, e.InnerException);
                }
                
            }
            else
            {
                return NotFound();
            }
            //Undone: notfoundResult!!!!
            //Todo: Fix Issue when there can be many facilities with same address
            return RedirectToAction("UserCabinet", new { userId.Value });
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
        public IActionResult Edit([Bind("Id,Name,Phone,FacilityType,Address,Schedule")]Facility facility)
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
                catch (Exception)
                {
                    throw new NotImplementedException();
                }
            }
            else
            {
                return NotFound();
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

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            //TODO: try-catchs
            db.Facilities.Delete(id);
            db.Save();
            return RedirectToAction("UserCabinet");
        }

    }
}