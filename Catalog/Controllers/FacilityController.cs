using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Catalog.Models.Facility;
using Catalog.ViewModels;

namespace Catalog.Controllers
{
    public class FacilityController : Controller
    {
        //List<FacilityModel> facilities;
        // DataContext dc;
        FacilityModel facility;

        //TODO: get models from db;
        public FacilityController()
        {
            var place1 = new FacilityModel
            {
                Id = 1,
                Name = "name1",
                Address = new FacilityAddress { City = "lviv" }, 
                FacilityType = FacilityType.bar,
                Phone = "phone1",
                Price = Price.low,
                Rating = 3,
                Schedule = new Schedule { WorkingDays = DayOfWeek.Friday }
            };

            //facilities = new List<FacilityModel> { place1 };
            facility = place1;
        }

        [HttpGet]
        public IActionResult Place(string name)
        {
            //TODO: get list from db in constructor and find necessary
            //var facilityModels = facilities
            //        .Where(f => f.Name == name)
            //        .ToList();

            facility.Name = name;

            var fvm = new FacilityViewModel { facility = facility };

            return View(fvm);
        }
    }
}