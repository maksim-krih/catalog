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


        public async Task<IActionResult> Index(string sortOrder, string facilityType, string facilityName, double rating, double price, int page = 1)
        { 
            int pageSize = 3;

            var facilities = db.Facilities.GetAll().AsQueryable();

            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) || sortOrder == "name_desc" ? "name_asc" : "name_desc";
            ViewData["PriceSortParam"] = String.IsNullOrEmpty(sortOrder) || sortOrder == "price_desc" ? "price_asc" : "price_desc";
            ViewData["RatingSortParam"] = String.IsNullOrEmpty(sortOrder) || sortOrder == "rate_desc" ? "rate_asc" : "rate_desc";
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
            if (rating != 0)
            {
                facilities = facilities.Where(p => p.Price > price);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    facilities = facilities.OrderByDescending(p => p.Name);
                    break;
                case "name_asc":
                    facilities = facilities.OrderBy(p => p.Name);
                    break;
                case "price_desc":
                    facilities = facilities.OrderByDescending(p => p.Price);
                    break;
                case "price_asc":
                    facilities = facilities.OrderBy(p => p.Price);
                    break;
                case "rate_desc":
                    facilities = facilities.OrderByDescending(p => p.Rating);
                    break;
                case "rate_asc":
                    facilities = facilities.OrderBy(p => p.Rating);
                    break;
                default:
                    break;
            }


            var count = await facilities.CountAsync();
            var items = await facilities.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            
            IndexView viewModel = new IndexView
            {
                PageViewModel = new PageView(count, page, pageSize),
                FilterModel = new FilterView(facilityType, facilityName, rating, price),
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
