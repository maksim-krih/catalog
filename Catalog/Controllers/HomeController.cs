using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Catalog.DAL.Models;
using Catalog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Catalog.BLL.Interfaces;
using Catalog.BLL.Repositories;
using Catalog.DAL.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Catalog.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork db;

        public HomeController(IUnitOfWork db)
        {
            this.db = db;
        }

        public IActionResult About()
        {
            return View();
        }


        public async Task<IActionResult> Index(string sortOrder, string facilityType, string facilityName, double rating, int page = 1)
        { 
            int pageSize = 3;

            var facilities = db.Facilities.GetAll().AsQueryable();

            ViewData["PriceSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder == "price_desc" ? "price_asc" : "price_desc";
            ViewData["RatingSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder == "rate_desc" ? "rate_asc" : "rate_desc";
            ViewData["Buffer"] = sortOrder;

            if (!String.IsNullOrEmpty(facilityType) && facilityType != "All")
            {
                facilities = facilities.Where(p => p.FacilityType.Contains(facilityType));
            }
            if (!String.IsNullOrEmpty(facilityName))
            {
                facilities = facilities.Where(p => p.Name.Contains(facilityName));
            }
            if (rating != 0)
            {
                facilities = facilities.Where(p => p.Rating > rating);
            }

            switch (sortOrder)
            {
                case "price_desc":
                    facilities = facilities.OrderByDescending(f => f.Price);
                    break;
                case "price_asc":
                    facilities = facilities.OrderBy(s => s.Price);
                    break;
                case "rate_desc":
                    facilities = facilities.OrderByDescending(s => s.Rating);
                    break;
                case "rate_asc":
                    facilities = facilities.OrderByDescending(s => s.Rating);
                    break;
                default:
                    break;
            }
        

            var count = await facilities.CountAsync();
            var items = await facilities.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            
            IndexView viewModel = new IndexView
            {
                PageViewModel = new PageView(count, page, pageSize),
                FilterModel = new FilterModel(facilityType, facilityName, rating),
                FacilityModels = items
            };

            return View(viewModel);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
