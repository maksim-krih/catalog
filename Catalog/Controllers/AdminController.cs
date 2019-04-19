using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Catalog.BLL.Interfaces;
using Catalog.BLL.ViewModels;
using Catalog.DAL.Models;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Views
{
    public class AdminController : Controller
    {
        IUnitOfWork db;

        public AdminController(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }

        // GET: Admin
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var facilitiesAndUsers = new FacilitiesUsersRoles
            {
                Facilities = db.Facilities.GetAll(),
                Users = db.Users.GetAll(),
                Roles = db.Roles.GetAll()
            };

            return View(facilitiesAndUsers);
        }
    }
}
