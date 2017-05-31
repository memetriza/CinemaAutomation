using CinemaAutomation.Models;
using CinemaAutomation.ViewModels;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
            FormsAuthentication.SetAuthCookie(user.Username, true);
            return RedirectToRoute("Home");
        }

        public ActionResult Edit(int id)
        {
            var user = Database.Session.Load<User>(id);

            if (user != null)
            {
                if (User.Identity.Name == user.Username)
                {
                    return View(new AccountEdit
                    {
                        Username = user.Username,
                        Email = user.Email,
                        Name = user.Name,
                        Surname = user.SurName,
                        Tcno = user.TcNo
                    });
                }
                else
                {
                    return HttpNotFound();
                }
                
            }
            else
            {
                return HttpNotFound();
            }
            
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AccountEdit form)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            if (Database.Session.Query<User>().Any(u => u.Username == form.Username && u.Id != id))
                ModelState.AddModelError("Username", "Kullanıcı adı eşsiz olmalıdır.");

            if (!ModelState.IsValid)
                return View(form);

            user.Name = form.Name;
            user.SurName = form.Surname;
            user.TcNo = form.Tcno;
            user.SetPassword(form.Password);

            Database.Session.Update(user);
            Database.Session.Flush();
            return RedirectToRoute("Home");
        }
    }
}