﻿using CinemaAutomation.Areas.Admin.ViewModels;
using CinemaAutomation.Models;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaAutomation.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        // GET: Admin/Users
        public ActionResult Index()
        {
            return View(new UsersIndex
            {
                Users = Database.Session.Query<User>().ToList()
            });
        }
        public ActionResult New()
        {
            return View(new UsersNew {

            });
        }
        [HttpPost]
        public ActionResult New(UsersNew form)
        {
            if (Database.Session.Query<User>().Any(u => u.Username == form.Username))
                ModelState.AddModelError("Username","Kullanıcı adı eşsiz olmalıdır.");

            if (!ModelState.IsValid)
                return View(form);

            var user = new User
            {
                Email = form.Email,
                Username = form.Username,
                Name = form.Name,
                SurName = form.Surname,
                TcNo = form.Tcno
            };
            user.SetPassword(form.Password);
            Database.Session.Save(user);
            return RedirectToAction("Index");
        }
    }
}