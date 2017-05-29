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
        public ActionResult New()
        {
            return View("MovieForm", new MoviesForm
            {   IsNew = true,
                Genres = Database.Session.Query<Genre>().Select(g => new GenreCheckBox
                {
                    Id = g.Id,
                    IsChecked = false,
                    Name = g.GenreName
                }).ToList()
            });
        }

        private void SyncGenres(IList<GenreCheckBox> checkboxes, IList<Genre> genres)
        {
            var selectedGenres = new List<Genre>();

            foreach (var genre in Database.Session.Query<Genre>())
            {
                var checkbox = checkboxes.Single(p => p.Id == genre.Id);
                checkbox.Name = genre.GenreName;

                if (checkbox.IsChecked)
                    selectedGenres.Add(genre);
            }

            foreach (var toAdd in selectedGenres.Where(t => !genres.Contains(t)))
            {
                genres.Add(toAdd);
            }

            foreach (var toRemove in genres.Where(t => !selectedGenres.Contains(t)).ToList())
                genres.Remove(toRemove);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult MovieForm(MoviesForm form)
        {
            form.IsNew = form.MovieId == null;

            if (!ModelState.IsValid)
                return View(form);

            var movie = new Movie();

            if (form.IsNew)
            {
                movie = new Movie
                {
                    CreatedAt = DateTime.UtcNow,
                };
                SyncGenres(form.Genres, movie.Genres);
            }
            else
            {
                movie = Database.Session.Load<Movie>(form.MovieId);
                if (movie == null)
                    return HttpNotFound();
                movie.UpdatedAt = DateTime.UtcNow;
            }
            movie.MovieName = form.MovieName;
            movie.MovieDirector = form.MovieDirector;
            movie.ReleaseDate = form.ReleaseDate;
            movie.Summary = form.Summary;
            movie.LinkText = form.LinkText;
            SyncGenres(form.Genres, movie.Genres);
            Database.Session.SaveOrUpdate(movie);
            Database.Session.Flush();

            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var movie = Database.Session.Load<Movie>(id);
            if (movie == null)
            {
                return HttpNotFound();
            }else
            {
                return View("MovieForm", new MoviesForm {
                IsNew = false,
                MovieId = id,
                MovieName = movie.MovieName,
                MovieDirector = movie.MovieDirector,
                Summary = movie.Summary,
                LinkText = movie.LinkText,
                ReleaseDate=movie.ReleaseDate,
                Genres = Database.Session.Query<Genre>().Select(g => new GenreCheckBox
                {
                    Id = g.Id,
                    IsChecked = movie.Genres.Contains(g),
                    Name = g.GenreName
                    }).ToList()
                });
            }            
        }

        public ActionResult Trash(int id)
        {
            var movie = Database.Session.Load<Movie>(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
                            
            movie.DeletedAt = DateTime.UtcNow;
            Database.Session.Update(movie);
            Database.Session.Flush();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var movie = Database.Session.Load<Movie>(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            Database.Session.Delete(movie);
            Database.Session.Flush();
            return RedirectToAction("Index");
        }

        public ActionResult Restore(int id)
        {
            var movie = Database.Session.Load<Movie>(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            else
            {
                movie.DeletedAt = null;
                Database.Session.Update(movie);
                Database.Session.Flush();
                return RedirectToAction("Index");
            }
        }
    }
}