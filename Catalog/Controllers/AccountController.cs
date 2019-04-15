using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Catalog.DAL.Models;
using Catalog.BLL.Interfaces;
using Catalog.BLL.ViewModels.DTO;
using Catalog.BLL.ViewModels;

namespace Catalog.Controllers
{
    public class AccountController : Controller
    {
        IUnitOfWork db;

        public AccountController(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }
        
        [HttpGet]
        public IActionResult Registrate()
        {
            return View();
        }
        
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrate(Registration registrationModel)
        {
            if (ModelState.IsValid)
            {
                var users = db.Users.Find(u => u.Email == registrationModel.Email);
                if (users.Count()==0)
                {
                    var user = new User
                    {
                        Name = registrationModel.Name,
                        Email = registrationModel.Email,
                        Password = registrationModel.Password,
                        Roleid = 2,
                    };

                    db.Users.Create(user);
                    db.Save();
                    await Authenticate(user, "User");

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("RegistrationError", "There is already user with this email!");
                }
            }

            return View(registrationModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Id,Email,Password")] Login loginModel)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.Find(u => u.Email == loginModel.Email && u.Password == loginModel.Password).First();
                if (user != null)
                {
                    string role = db.Roles.Get(user.Roleid).Name;
                    await Authenticate(user, role);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "invalid Data");
            }

            return View(loginModel);
        }

        private async Task Authenticate(User user, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Password),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
