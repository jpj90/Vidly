using Vidly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek" };

            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1"},
                new Customer { Name = "Customer 2"}
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };
            // this is a time bomb, don't do it
            // ViewData["Movie"] = movie;
            // ViewBag.Movie = movie;

            
            return View(viewModel);


            // an example of some of the different ActionResult types below
            //return new EmptyResult();
            //return HttpNotFound();
            //return Content("<h1 style=\"margin:70px\">Hello World</h1>");
            //return RedirectToAction("Index","Home");

            // sometimes, it is necessary to pass some additional arguments along with the redirect
            // this can be done by using an **anonymous object**, which is turned into a query string
            //return RedirectToAction("Index","Home", new {firstname = "Henry", lastname = "Higgins" });
        }

        //public ActionResult Edit(int id)
        //{
        //    return Content($"Id equals: {id}");
        //}

        // this will become the default action for the Controller Movies
        // the question mark on the int parameter makes it nullable, string already is
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;

            if (string.IsNullOrWhiteSpace(sortBy))
                sortBy = "Name";


            //return View(movies);
            //return Content($"pageIndex={pageIndex}&sortBy={sortBy}");

            var movies = _context.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            return View(movie);
        }

        // in the route decorator, you specify your custom route and any constraints on the parameters
        [Route("movies/released/{year:regex(2015|2018)}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var ViewModel = new MovieFormViewModel
            {
                Genres = genres
            };
            return View("MovieForm",ViewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.Single(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };
            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = DateTime.Now;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
    }
} 