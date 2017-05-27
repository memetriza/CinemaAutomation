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
    [Authorize(Roles = "admin")]
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
        private void SyncRoles(IList<RoleCheckBox> checkboxes, IList<Role> roles)
        {
            var selectedRoles = new List<Role>();

            foreach (var role in Database.Session.Query<Role>())
            {
                var checkbox = checkboxes.Single(p => p.Id == role.Id);
                checkbox.Name = role.Name;

                if (checkbox.IsChecked)
                    selectedRoles.Add(role);
            }

            foreach (var toAdd in selectedRoles.Where(t => !roles.Contains(t)))
            {
                roles.Add(toAdd);
            }

            foreach (var toRemove in roles.Where(t => !selectedRoles.Contains(t)).ToList())
                roles.Remove(toRemove);
        }
        public ActionResult New()
        {
            return View(new UsersNew {
            Roles=Database.Session.Query<Role>().Select(r=>new RoleCheckBox {
                Id=r.Id,
                IsChecked=false,
                Name=r.Name
            }).ToList()
            });
        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult New(UsersNew form)
        {
            var user = new User();
            SyncRoles(form.Roles, user.Roles);

            if (Database.Session.Query<User>().Any(u => u.Username == form.Username))
                ModelState.AddModelError("Username","Kullanıcı adı eşsiz olmalıdır.");

            if (!ModelState.IsValid)
                return View(form);


            user.Email = form.Email;
            user.Username = form.Username;
            user.Name = form.Name;
            user.SurName = form.Surname;
            user.TcNo = form.Tcno;
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
                Tcno = user.TcNo,
                Roles=Database.Session.Query<Role>().Select(r=> new RoleCheckBox
                {
                    Id=r.Id,
                    Name=r.Name,
                    IsChecked=user.Roles.Contains(r)
                }).ToList()
            });
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Edit(int id,UsersEdit form)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            SyncRoles(form.Roles, user.Roles);

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