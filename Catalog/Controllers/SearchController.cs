using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.BLL.Interfaces;
using Catalog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    public class SearchController : Controller
    {
        private IUnitOfWork db;
        public SearchController(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }


        [HttpGet]
        public IActionResult SearchResult(string searchString)
        {
            //if (searchString == null)
            //    return View(new ErrorViewModel());

            var result = db.Facilities.Find(f => f.Name == searchString);

            //if(result == null)
            //    return View(new ErrorViewModel());


            return View(result);
        }
    }
}