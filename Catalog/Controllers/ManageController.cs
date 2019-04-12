using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    public class ManageController : Controller
    {
        [HttpGet]
        public IActionResult UserCabinet()
        {
            return View();
        }
    }
}