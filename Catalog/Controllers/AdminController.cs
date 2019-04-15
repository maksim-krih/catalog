using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Catalog.BLL.Interfaces;
using Catalog.DAL.Models;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Views
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
