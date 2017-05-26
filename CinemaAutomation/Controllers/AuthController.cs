﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CinemaAutomation.ViewModels;

namespace CinemaAutomation.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Login()
        {
            return View(new AuthLogin());
        }
        [HttpPost]
        public ActionResult Login(AuthLogin user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            FormsAuthentication.SetAuthCookie(user.UserName, true);
            return Content("Valid");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("Home");
        }
    }
}