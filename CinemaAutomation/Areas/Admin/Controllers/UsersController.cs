using CinemaAutomation.Areas.Admin.ViewModels;
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
        public ActionResult Edit(int id)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            return View(new UsersEdit {
                Username = user.Username,
                Email = user.Email,
                Name = user.Name,
                Surname = user.SurName,
                Tcno = user.TcNo
            });
        }

        [HttpPost]
        public ActionResult Edit(int id,UsersEdit form)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            if (Database.Session.Query<User>().Any(u => u.Username == form.Username && u.Id != id))
                ModelState.AddModelError("Username", "Kullanıcı adı eşsiz olmalıdır.");

            if (!ModelState.IsValid)
                return View(form);

            user.Username = form.Username;
            user.Email = form.Email;
            user.Name = form.Name;
            user.SurName = form.Surname;
            user.TcNo = form.Tcno;

            Database.Session.Update(user);
            Database.Session.Flush();
            return RedirectToAction("Index");
        }

        public ActionResult ResetPassword(int id)
        {
            var user = Database.Session.Query<User>().FirstOrDefault(p => p.Id == id);
            if (user == null)
                return HttpNotFound();

            return View(new UsersResetPassword
            {
                Username = user.Username,
            });
        }
        [HttpPost]
        public ActionResult ResetPassword(int id,UsersResetPassword form)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();
            form.Username = user.Username;

            if (!ModelState.IsValid)
                return View(form);

            user.SetPassword(form.Password);
            Database.Session.Update(user);
            Database.Session.Flush();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            Database.Session.Delete(user);
            Database.Session.Flush();
            return RedirectToAction("Index");
        }
    }
}