using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Catalog.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Catalog.DAL.Models;
using Catalog.BLL.Interfaces;

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
                if (db.Users.Find(u => u.Email == registrationModel.Email) == null)
                {
                    var user = new User
                    {
                        Name = registrationModel.Name,
                        Email = registrationModel.Email,
                        Password = registrationModel.Password
                    };
                    db.Users.Create(user);
                    db.Save();
                    await Authenticate(user);

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
        public async Task<IActionResult> Login(Login loginModel)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.Find(u => u.Email == loginModel.Email && u.Password == loginModel.Password).First();
                if (user != null)
                {
                    await Authenticate(user);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "invalid Data");
            }

            return View(loginModel);
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Password)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
