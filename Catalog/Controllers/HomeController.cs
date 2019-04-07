using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Catalog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Catalog.BLL.Interfaces;
using Catalog.BLL.Repositories;

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

        public IActionResult Index()
        {
            return View(db.Facilities.GetAll());
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
