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

            if(facility == null)
                throw new ArgumentNullException();


            return View(facility);
        }


        [HttpPost]
        public IActionResult AddFeedback(int id, [Bind("Rating,Price,AuthorId,Message")]FeedbackDTO feedbackDTO)
        {
            try
            {
                var facility = db.Facilities.Get(id);
                var feedback = new Feedback
                {
                    //Id = db.Feedbacks.GetAll().Count() + 1,
                    FacilityId = id,
                    Message = feedbackDTO.Message != null ? feedbackDTO.Message : "",
                    Rating = feedbackDTO.Rating,
                    Price = feedbackDTO.Price,
                    Author = User.Claims.ToList()[2].Value,
                    Date = DateTime.Now
                };
                            

                UpdateRating(ref facility,feedbackDTO.Rating);
                UpdatePrice(ref facility, feedbackDTO.Price);

                db.Feedbacks.Create(feedback);

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
        private void UpdatePrice(ref Facility facility, int price)
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