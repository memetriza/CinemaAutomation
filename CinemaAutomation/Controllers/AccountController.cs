using CinemaAutomation.Models;
using CinemaAutomation.ViewModels;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaAutomation.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult New()
        {
            return View(new Account {

            });
        }
        [HttpPost]
        public ActionResult New(Account form)
        {
            if (Database.Session.Query<User>().Any(u => u.Username == form.Username))
                ModelState.AddModelError("Username", "Kullanıcı adı eşsiz olmalıdır.");

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
            return RedirectToRoute("Home");
        }
    }
}