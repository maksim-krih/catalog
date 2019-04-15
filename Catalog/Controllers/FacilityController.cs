using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Catalog.BLL.Interfaces;
using Catalog.DAL.Models;

namespace Catalog.Controllers
{
    public class FacilityController : Controller
    {
        IUnitOfWork db;

        public FacilityController(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }

        [HttpGet]
        [Route("[controller]/{id}")]
        public IActionResult Place (int id)
        {
            var facility = db.Facilities.Get(id);

            if(facility == null)
                throw new ArgumentNullException();


            return View(facility);
        }


        [HttpPost]
        public IActionResult AddFeedback(int id)
        {
            try
            {
                var facility = db.Facilities.Get(id);

               

                var feedback = new Feedback
                {
                    FacilityId = id,
                    Message = Request.Form["Message"],
                    Rating = Convert.ToInt32(Request.Form["Rating"]),
                    Price = Convert.ToInt32(Request.Form["Price"]),
                    Date = DateTime.Now,
                    Author = db.Users.Get(Convert.ToInt32(Request.Form["AuthorId"])).Name
                };
                                

                UpdateRating(ref facility, Convert.ToInt32(Request.Form["Rating"]));
                UpdatePrice(ref facility, Convert.ToInt32(Request.Form["Price"]));
                db.Save();
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction("Place", id);
        }
                

        /// <summary>
        /// Method to Update Properties of Facility
        /// </summary>
        /// <param name="facility">Facility that we change properties</param>
        /// <param name="rating">field to update</param>
        private void UpdateRating(ref Facility facility, int rating)
        {
            // Collection of facilities where feedbacks that were left are with rating field.
            var query = facility.Feedbacks.Where(feedback => feedback.Rating != 0);

            double sumRating = 0;

            foreach (var item in query)
            {
                sumRating += item.Rating;
            }

            facility.Rating = sumRating / query.Count();
            db.Save();
        }

        /// <summary>
        /// Method to Update Properties of Facility
        /// </summary>
        /// <param name="facility">Facility that we change properties</param>
        /// <param name="price">field to update</param>
        private void UpdatePrice(ref Facility facility, int price)
        {
            // Collection of facilities where feedbacks that were left are with rating field.
            var query = facility.Feedbacks.Where(feedback => feedback.Price != 0);

            double sumPrice = 0;

            foreach (var item in query)
            {
                sumPrice += item.Rating;
            }

            sumPrice += price;

            facility.Rating = sumPrice / query.Count();
        }
    }
}