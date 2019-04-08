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
        public IActionResult Place (int id) //(string name)
        {
            //if (string.IsNullOrEmpty(name))
            //    throw new ArgumentNullException();

            //var facility = db.Facilities.Find(name);

            var facility = db.Facilities.Get(id);

            if(facility == null)
                throw new ArgumentNullException();


            return View(facility);
        }
    }
}