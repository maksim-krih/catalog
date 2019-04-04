using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Catalog.DAL.Entities;
using Catalog.DAL.Enums;
using Catalog.ViewModels;

namespace Catalog.Controllers
{
    public class FacilityController : Controller
    {
        

        [HttpGet]
        public IActionResult Place(string name)
        {
            //TODO: get list from db in constructor and find necessary
            //var facilityModels = facilities
            //        .Where(f => f.Name == name)
            //        .ToList();

            //facility.Name = name;

            //var fvm = new FacilityViewModel { facility = facility };

            //return View(fvm);
            return View();
        }
    }
}