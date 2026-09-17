using MovieTicketBookingSystem.Models;
using System.Web.Mvc;

namespace MovieTicketBookingSystem.Controllers
{
    public class MovieController : Controller
    {
        MovieDBHandle movieDB = new MovieDBHandle();

        MovieCategoryDBHandle categoryDB =
            new MovieCategoryDBHandle();

        // GET: Movie
        public ActionResult Index()
        {
            return View();
        }

        // GET: Movie/AddMovie
        public ActionResult AddMovie()
        {
            ViewBag.CategoryList = new SelectList(
                movieDB.GetCategories(),
                "Category_id",
                "Type"
            );

            return View();
        }

        // POST: Movie/AddMovie
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                bool result = movieDB.AddMovie(movie);

                if (result)
                {
                    return RedirectToAction("DisplayMovies");
                }
            }

            return View(movie);
        }
        // Book Ticket
        public ActionResult BookTicket(int id)
        {
            return RedirectToAction("AllBookings", "Booking", new { id = id });
        }
        // GET: Movie/DisplayMovies
        public ActionResult DisplayMovies()
        {
            var movies = movieDB.GetMovies();

            return View(movies);
        }

        // GET: Movie/DeleteMovie/5
        [HttpGet]
        public ActionResult DeleteMovie(int id)
        {
            movieDB.DeleteMovie(id);

            return RedirectToAction("DisplayMovies");
        }
        [HttpGet]
        public ActionResult SearchMovie()
        {
            ViewBag.CategoryList = new SelectList(
                movieDB.GetCategories(),
                "Category_id",
                "Type"
            );

            return View();
        }
        [HttpPost]
        public ActionResult SearchMovie(int categoryId)
        {
            var movies =
                movieDB.SearchMoviesByCategory(categoryId);

            ViewBag.CategoryList = new SelectList(
                movieDB.GetCategories(),
                "Category_id",
                "Type",
                categoryId
            );

            return View(movies);
        }
    }
}