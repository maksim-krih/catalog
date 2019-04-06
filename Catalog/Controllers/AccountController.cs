﻿using System;
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
        private DataContext UserContext { get; set; }

        public AccountController(DataContext context)
        {
            UserContext = context;
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
                await UserContext.Database.BeginTransactionAsync();   
                User user = UserContext.Users.FirstOrDefault(u => u.Email == registrationModel.Email);
                if (user == null)
                {
                    user = new User
                    {
                        Name = registrationModel.Name,
                        Email = registrationModel.Email,
                        Password = registrationModel.Password
                    };
                    UserContext.Users.Add(user);
                    await UserContext.SaveChangesAsync();
                    await Authenticate(user);

                    UserContext.Database.CommitTransaction();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("RegistrationError", "There is already user with this email!");
                    UserContext.Database.RollbackTransaction();
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
                User user = UserContext.Users.FirstOrDefault(u => u.Email == loginModel.Email && u.Password == loginModel.Password);
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