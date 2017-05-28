using CinemaAutomation.Areas.Admin.ViewModels;
using CinemaAutomation.Infrastructure;
using CinemaAutomation.Models;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaAutomation.Areas.Admin.Controllers
{
    [Authorize(Roles ="admin")]
    public class MovieController : Controller
    {
        private const int MoviesPerPage = 5;
        // GET: Admin/Movie
        public ActionResult Index(int page=1)
        {
            var totalMovieCount = Database.Session.Query<Movie>().Count();
            var currentMoviePage = Database.Session.Query<Movie>()
                .OrderByDescending(c => c.CreatedAt)
                .Skip((page - 1) * MoviesPerPage)
                .Take(MoviesPerPage)
                .ToList();

            return View(new MoviesIndex {
                Movies=new PagedData<Movie>(currentMoviePage,totalMovieCount,page,MoviesPerPage)
            });
        }
    }
}