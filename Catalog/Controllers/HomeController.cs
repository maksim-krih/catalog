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

namespace Catalog.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork db;
        private CatalogContext catalog;

        public HomeController(IUnitOfWork db)
        {
            this.db = db;
        }

        public IActionResult About()
        {
            return View();
        }
       
        
        public async Task<IActionResult> Index(string sortOrder, int page = 1)
        { 
            FacilityRepository facilityRepository = new FacilityRepository(catalog);

            int pageSize = 3;

            ViewData["PriceSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder == "price_desc" ? "price_asc" : "price_desc";
            ViewData["RatingSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder == "rate_desc" ? "rate_asc" : "rate_desc";
            ViewData["Buffer"] = sortOrder;

            var facilities = facilityRepository.Sort(db.Facilities.GetAll().AsQueryable(), sortOrder);
           
            var count = await facilities.CountAsync();
            var items = await facilities.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageView pageViewModel = new PageView(count, page, pageSize);
            IndexView viewModel = new IndexView
            {
                PageViewModel = pageViewModel,
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
