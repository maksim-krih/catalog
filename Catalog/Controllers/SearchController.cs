using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.BLL.Interfaces;
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
            ViewData["SearchString"] = searchString;
            if (searchString == null)
                return PartialView("SearchFailPartial");

            var result = db.Facilities.Find(f => f.Name.Contains(searchString));

            if(result.ToList().Count == 0)
                return PartialView("SearchFailPartial");


            return View(result);
        }
    }
}