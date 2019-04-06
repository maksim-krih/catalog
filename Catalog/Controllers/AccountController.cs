using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Catalog.Data;
using Catalog.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    public class AccountController : Controller
    {
        private DataContext Context { get; set; }

        public AccountController(DataContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult Registrate()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrate(RegistrationModel registrationModel)
        {
            if (ModelState.IsValid)
            {
                await Context.Database.BeginTransactionAsync();   
                User user = Context.Users.FirstOrDefault(u => u.Email == registrationModel.Email);
                if (user == null)
                {
                    user = new User
                    {
                        Name = registrationModel.Name,
                        Email = registrationModel.Email,
                        Password = registrationModel.Password,
                        Roleid = 2,
                    };
                    Context.Users.Add(user);
                    await Context.SaveChangesAsync();
                    await Authenticate(user, "User");

                    Context.Database.CommitTransaction();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("RegistrationError", "There is already user with this email!");
                    Context.Database.RollbackTransaction();
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
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                User user = Context.Users.FirstOrDefault(u => u.Email == loginModel.Email && u.Password == loginModel.Password);
                if (user != null)
                {
                    string role = Context.Roles.Find(user.Roleid).Name;
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
            return RedirectToAction("Login", "Account");
        }

    }
}
