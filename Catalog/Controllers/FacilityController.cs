using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Catalog.BLL.Interfaces;
using Catalog.DAL.Models;
using Catalog.BLL.ViewModels.DTO;

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

            if (facility == null)
                return NotFound();


            return View(facility);
        }


        [HttpPost]
        public IActionResult AddFeedback(int id, [Bind("Rating,Price,Message,Author")]FeedbackDTO feedbackDTO)
        {
            try
            {
                var facility = db.Facilities.Get(id);
                if (facility == null)
                    return NotFound();

                var feedback = new Feedback
                {
                    Id = 0,
                    FacilityId = id,
                    Message = feedbackDTO.Message != null
                                ? feedbackDTO.Message
                                : "",
                    Rating = feedbackDTO.Rating,
                    Price = feedbackDTO.Price,
                    Author = feedbackDTO.Author != null
                                ? feedbackDTO.Author
                                : "Guest",
                    Date = DateTime.Now
                };

                UpdateRating(facility, feedbackDTO.Rating);
                UpdatePrice(facility, feedbackDTO.Price);

                db.Feedbacks.Create(feedback);

                db.Save();
            }
            catch (Exception)
            {
                return NotFound();
            }

            return RedirectToAction("Place", id);
        }
                

        /// <summary>
        /// Method to Update Properties of Facility
        /// </summary>
        /// <param name="facility">Facility that we change properties</param>
        /// <param name="rating">field to update</param>
        private void UpdateRating( Facility facility, int rating)
        {
            // Collection of facilities where feedbacks that were left are with rating field.
            var query = facility.Feedbacks.Where(feedback => feedback.Rating != 0);

            if(query.ToList().Count == 0)
            {
                facility.Rating = rating;
            }
            else
            {
                double sumRating = 0;

                foreach (var item in query)
                {
                    sumRating += item.Rating;
                }

                sumRating += rating;

                facility.Rating = Math.Round(sumRating / (query.Count() + 1), 1, MidpointRounding.AwayFromZero);
            }
           
            db.Save();
        }

        /// <summary>
        /// Method to Update Properties of Facility
        /// </summary>
        /// <param name="facility">Facility that we change properties</param>
        /// <param name="price">field to update</param>
        private void UpdatePrice(Facility facility, int price)
        {
            // Collection of facilities where feedbacks that were left are with rating field.
            var query = facility.Feedbacks.Where(feedback => feedback.Price != 0);

            if(query.ToList().Count == 0)
            {
                facility.Price = price;
            }
            else
            {
                double sumPrice = 0;

                foreach (var item in query)
                {
                    sumPrice += item.Rating;
                }

                sumPrice += price;

                facility.Rating = Math.Round(sumPrice / (query.Count() + 1), 1, MidpointRounding.AwayFromZero);
            }
            db.Save();
        }
    }
}